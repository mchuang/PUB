using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUB {
    class ParkReport {
        int parkId;
        int parkNumber;
        String parkName;

        public ParkReport(int parkId) {
            this.parkId = parkId;
            Object[] parkInfo = DatabaseControl.getSingleRecord(new String[] { "ParkNumber", "ParkName" }, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { parkId });
            parkNumber = (int)parkInfo[0];
            parkName = parkInfo[1].ToString();
        }

        public void generateSummReport(DateTime dueDate) {
            List<Object[]> spaces = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceTenant.ParkSpaceID", "OrderID", "Tenant", "GasStatus", "EleStatus", "WatStatus", "GasMedical", "EleMedical" }, 
                DatabaseControl.spaceTable + " JOIN " + DatabaseControl.spaceBillTable + " ON ParkSpaceTenant.ParkSpaceID=ParkSpaceBill.ParkSpaceID", "ParkID=@value0 AND DueDate=@value1 ORDER BY OrderID ASC", new Object[] { parkId, dueDate });
            ArrayList reportInfo = new ArrayList();

            spaces = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID", "OrderID", "Tenant", "GasStatus", "EleStatus", "WatStatus", "GasMedical", "EleMedical" },
                DatabaseControl.spaceTable, "ParkID=@value0 AND MoveOutDate IS NULL ORDER BY OrderID ASC", new Object[] { parkId });
            //String table = DatabaseControl.optionsTable + " AS t1 JOIN " + DatabaseControl.parkOptionsTable +
            //    " AS t2 ON t1.ChargeItemID=t2.ChargeItemID";
            //List<Object[]> items = DatabaseControl.getMultipleRecord(new String[] { "ChargeItemDescription" }, table, "ParkID=@value0", new Object[] { parkId });
            List<String> tempCharge = new List<String>();
            String table = DatabaseControl.parkOptionsTable + " JOIN " + DatabaseControl.optionsTable + " ON ChargeItem.ChargeItemID=ParkChargeItem.ChargeItemID";
            List<Object[]> parkCharge = DatabaseControl.getMultipleRecord(new String[] { "ChargeItemDescription" }, table, "ParkID=@value0 ORDER BY ChargeItemCode", new Object[] { parkId });

            foreach (Object[] space in spaces) {
                String[] fieldsUtil = new String[] { "GasBill", "EleBill", "WatBill" };
                String[] fieldsSumm = new String[] { "Description", "ChargeItemValue" };
                String[] fieldsTemp = new String[] { "Description", "ChargeItemValue" };
                String condition = "ParkSpaceID=@value0 AND DueDate=@value1";
                Dictionary<String, Object> util = DatabaseControl.getSingleRecordDict(fieldsUtil, DatabaseControl.spaceBillTable, 
                    condition, new Object[] { (int)space[0], dueDate });
                if (util.Keys.Count == 0) {
                    util["GasBill"] = util["EleBill"] = util["WatBill"] = 0M;
                }
                List<Object[]> summ = DatabaseControl.getMultipleRecord(fieldsSumm, DatabaseControl.spaceChargeTable,
                    condition, new Object[] { (int)space[0], dueDate });
                List<Object[]> temp = DatabaseControl.getMultipleRecord(fieldsTemp, DatabaseControl.spaceTempChargeTable,
                    condition, new Object[] { (int)space[0], dueDate });
                //foreach (Object[] obj in summ) { util[(String)obj[0]] = obj[1]; if (!parkCharge.Contains((String)obj[0])) parkCharge.Add((String)obj[0]); }
                foreach (Object[] obj in temp) { if (!tempCharge.Contains((String)obj[0])) tempCharge.Add((String)obj[0]); }
                util["OrderID"] = space[1];
                util["Tenant"] = space[2];
                util["GasStatus"] = space[3];
                util["EleStatus"] = space[4];
                util["WatStatus"] = space[5];
                util["GasMedical"] = space[6];
                util["EleMedical"] = space[7];
                foreach (Object[] item in summ) util[item[0].ToString()] = item[1];
                foreach (Object[] item in temp) util[item[0].ToString()] = item[1];
                reportInfo.Add(util);
            }
            PdfControl.createParkSummReport(parkNumber, parkName, dueDate, reportInfo, parkCharge, tempCharge);
            //CSVGenerator.totalListFormat(parkNumber, parkName, reportInfo, parkCharge);
        }

        public void generateUtilReport(DateTime dueDate) {
            List<Object[]> spaces = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID" }, DatabaseControl.spaceTable,
                "ParkID=@value0 AND MoveOutDate IS NULL ORDER BY OrderID", new Object[] { parkId });
            List<Object[]> gas, ele, wat;
            gas = new List<Object[]>(); ele = new List<Object[]>(); wat = new List<Object[]>();
            Parallel.ForEach(spaces.AsEnumerable(), delegate(Object[] space) { billComputation(space, dueDate, ref gas, ref ele, ref wat); });
            gas.Sort(delegate(Object[] obj1, Object[] obj2) { return ((int)obj1[0]).CompareTo((int)obj2[0]); });
            ele.Sort(delegate(Object[] obj1, Object[] obj2) { return ((int)obj1[0]).CompareTo((int)obj2[0]); });
            wat.Sort(delegate(Object[] obj1, Object[] obj2) { return ((int)obj1[0]).CompareTo((int)obj2[0]); });
            Object[] startend = DatabaseControl.getSingleRecord(new String[] { "StartDate", "MeterReadDate" }, DatabaseControl.meterReadsTable,
                "ParkID=@value0 AND DueDate=@value1", new Object[] { parkId, dueDate });
            PdfControl.createParkUtilReport(parkNumber, parkName, (DateTime)startend[0], (DateTime)startend[1], gas, ele, wat);
        }

        public void billComputation(Object[] space, DateTime dueDate, ref List<Object[]> gas, ref List<Object[]> ele, ref List<Object[]> wat) {
            BillCalculation bill = new BillCalculation(parkId, (int)space[0], dueDate);
            Object[] info = bill.generateUtilReport();
            Dictionary<String, int> read = bill.meterRead;
            gas.Add(new Object[] { bill.tenantInfo["OrderID"], read["PrevGasReadValue"], read["GasReadValue"], info[0] });
            ele.Add(new Object[] { bill.tenantInfo["OrderID"], read["PrevEleReadValue"], read["EleReadValue"], info[1] });
            wat.Add(new Object[] { bill.tenantInfo["OrderID"], read["PrevWatReadValue"], read["WatReadValue"], info[2] });
        }

        public void generateExtraChargesReport(DateTime start, DateTime end) {
            SelectStatus status = new SelectStatus(parkId);
            status.ShowDialog();

            List<Object[]> spaces = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID", "Tenant" }, DatabaseControl.spaceTable,
                "ParkID=@value0 AND (MoveOutDate IS NULL OR MoveOutDate<=@value1)", new Object[] { parkId, start });
            List<Object[]> extras = new List<Object[]>();
            foreach (Object[] space in spaces) {
                Object tenant = space[1];
                DateTime[] startend = new DateTime[] { start, end };
                Dictionary<String, Object> moveDates = DatabaseControl.getSingleRecordDict(new String[] { "MoveInDate", "MoveOutDate" },
                    DatabaseControl.spaceTable, "ParkSpaceID=@value0", new Object[] { (int)space[0] });
                if (moveDates["MoveOutDate"] != DBNull.Value) startend[1] = (DateTime)moveDates["MoveOutDate"];
                if ((DateTime)moveDates["MoveInDate"] > start) startend[0] = (DateTime)moveDates["MoveInDate"];
                List<Object[]> temps = DatabaseControl.getMultipleRecord(new String[] { "ChargeItemValue" }, DatabaseControl.spaceTempChargeTable, "ParkSpaceID=@value0", new Object[] { (int)space[0] });
                String table = DatabaseControl.spaceChargeTable + " AS t1 JOIN " + DatabaseControl.optionsTable + " AS t2 ON t1.Description=t2.ChargeItemDescription";
                Object[] balanceFw = DatabaseControl.getSingleRecord(new String[] { "ChargeItemValue" }, table, 
                    "ParkSpaceID=@value0 AND ChargeItemID=@value1", new Object[] { space[0], 1 });
                Object[] lateChrge = DatabaseControl.getSingleRecord(new String[] { "ChargeItemValue" }, table,
                    "ParkSpaceID=@value0 AND ChargeItemID=@value1", new Object[] { space[0], 2 });
                Object[] tenantStatus = DatabaseControl.getSingleRecord(new String[] { "GasStatus", "EleStatus", "GasMedical", "EleMedical" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { (int)space[0] });
                String gasStatus = "";
                String eleStatus = "";
                int medStatus = (int)tenantStatus[2];
                if (status.gasList.Contains(tenantStatus[0].ToString())) gasStatus = tenantStatus[0].ToString();
                if (status.eleList.Contains(tenantStatus[1].ToString())) eleStatus = tenantStatus[1].ToString();
                if (balanceFw == null) balanceFw = new Object[] { 0M };
                if (lateChrge == null) lateChrge = new Object[] { 0M };
                if (temps.Count != 0 || (decimal)balanceFw[0] != 0M || (decimal)lateChrge[0] != 0M || medStatus != 0 || gasStatus != "" || eleStatus != "") {
                    extras.Add(new Object[] { space[0], tenant, balanceFw[0], lateChrge[0], temps, gasStatus, eleStatus, medStatus });
                }
            }
            List<String> rateTypes = new List<String>();
            foreach (String item in status.gasList) { rateTypes.Add(item); }
            foreach (String item in status.eleList) { if (!rateTypes.Contains(item)) rateTypes.Add(item); }
            PdfControl.createParkExtraChargeReport(parkNumber, parkName, start, end, extras, rateTypes);
        }

        public void generateReadSheet(DateTime readDate) {
            ArrayList reads = DatabaseControl.getMultipleRecordDict(new String[] { "OrderID", "GasReadValue", "EleReadValue", "WatReadValue" }, DatabaseControl.meterReadsTable,
                "ParkID=@value0 AND DueDate=@value1 ORDER BY OrderID", new Object[] { parkId, readDate });
            PdfControl.generateReadSheet(parkNumber, parkName, reads, readDate);
        }

        public void generateReadSheet2(DateTime readDate1, DateTime readDate2) {
            ArrayList reads1 = DatabaseControl.getMultipleRecordDict(new String[] { "OrderID", "GasReadValue", "EleReadValue", "WatReadValue" }, DatabaseControl.meterReadsTable,
                "ParkID=@value0 AND DueDate=@value1 ORDER BY OrderID", new Object[] { parkId, readDate1 });
            ArrayList reads2 = DatabaseControl.getMultipleRecordDict(new String[] { "OrderID", "GasReadValue", "EleReadValue", "WatReadValue" }, DatabaseControl.meterReadsTable,
                "ParkID=@value0 AND DueDate=@value1 ORDER BY OrderID", new Object[] { parkId, readDate2 });
            PdfControl.generateReadSheet2(parkNumber, parkName, reads1, reads2, readDate1);
        }
    }
}
