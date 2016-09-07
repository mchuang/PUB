using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUB
{
    public partial class TenantBill : Form
    {
        int parkId, parkSpaceId, meterReadId, orderId;
        Object[] parkInfo;
        Object[] parkMessage;
        DateTime dueDate;

        public TenantBill()
        {
            InitializeComponent();
            parkReportToolStripMenuItem.Visible = false;
        }

        private void TenantBill_Load(object sender, EventArgs e)
        {
            tenantToolStripMenuItem.Visible = false;
            tenantList.Enabled = false;
            readDate.Enabled = false;
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkNumber", "ParkID", "1=1 ORDER BY ParkNumber", new Object[] {});
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tenantList.Enabled = true;
            readDate.Enabled = false;
            historyBtn.Visible = false;
            billBtn.Visible = false;
            moveOut.Visible = false;
            tenantList.Items.Clear();
            spaceList.Items.Clear();
            parkReportToolStripMenuItem.Visible = true;

            parkId = ((CommonTools.Item)parkList.SelectedItem).Value;
            Object[] read;
            try
            {
                read = DatabaseControl.getSingleRecord(new String[] { "StartDate", "MeterReadDate" }, DatabaseControl.meterReadsTable, "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId });
                if (read == null) throw(null);
            }
            catch
            {
                MessageBox.Show("No billing info is available!");
                return;
            }
            DatabaseControl.populateComboBox(ref tenantList, DatabaseControl.spaceTable, "Tenant", "ParkSpaceID", "ParkID=@value0 AND (MoveOutDate IS NULL OR MoveOutDate > @value1) AND MoveInDate < @value2 ORDER BY OrderID ASC", new Object[] { parkId, read[0], read[1] });
            DatabaseControl.populateComboBox(ref spaceList, DatabaseControl.spaceTable, "OrderID", "ParkSpaceID", "ParkID=@value0 AND (MoveOutDate IS NULL OR MoveOutDate > @value1) AND MoveInDate < @value2 ORDER BY OrderID ASC", new Object[] { parkId, read[0], read[1] });
            String[] fields = { "ParkName", "Address", "City", "State", "ZipCode", "ParkNumber" };
            String condition = "ParkID=@value0";
            parkInfo = DatabaseControl.getSingleRecord(fields, DatabaseControl.parkTable, condition, new Object[] { this.parkId });
            clerkInfo.Text = String.Format("{0}\n{1}\n{2}, {3} {4}", parkInfo);
            tenantInfo.Text = usageInfo.Text = readInfo.Text = "";
            eleInfo.Text = gasInfo.Text = watInfo.Text = "";
            tenantList.Text = readDate.Text = "";
            spaceList.Text = "";
            summaryOfCharges.Rows.Clear();
            summaryOfCharges.Refresh();
            tempCharges.Rows.Clear();
            tempCharges.Refresh();
            this.Size = new Size(970, 150);
        }


        private void spaceList_SelectedIndexChanged(object sender, EventArgs e) {
            parkReportToolStripMenuItem.Visible = false;
            tenantList.Text = CommonTools.Item.getString(ref tenantList, ((CommonTools.Item)spaceList.SelectedItem).Value);
        }

        private void tenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            parkReportToolStripMenuItem.Visible = false;
            tenantToolStripMenuItem.Visible = true;
            historyBtn.Visible = true;
            billBtn.Visible = true;
            readDate.Enabled = true;
            readDate.Items.Clear();
            parkId = ((CommonTools.Item)parkList.SelectedItem).Value;
            parkSpaceId = ((CommonTools.Item)tenantList.SelectedItem).Value;
            spaceList.Text = CommonTools.Item.getString(ref spaceList, ((CommonTools.Item)tenantList.SelectedItem).Value);
            orderId = (int)DatabaseControl.getSingleRecord(new String[] { "OrderID" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];
            String[] fields = { "Tenant", "Address1", "Address2", "City", "State", "Zip" };
            String condition = "ParkSpaceID=@value0";
            object[] tenant = DatabaseControl.getSingleRecord(fields, DatabaseControl.spaceTable, condition, new Object[] { this.parkSpaceId });
            Object movedOut = DatabaseControl.getSingleRecord(new String[] { "MoveOutDate" }, DatabaseControl.spaceTable, condition, new Object[] { this.parkSpaceId })[0];
            if (movedOut == DBNull.Value) { moveOut.Visible = true; } else { moveOut.Visible = false; }
            tenantInfo.Text = String.Format("{0}\tSpace # {2}\n{1}\n{3}, {4} {5}", tenant);
            DatabaseControl.populateComboBox(ref readDate, DatabaseControl.meterReadsTable, "DueDate", "MeterReadID",
                "ParkID=@value0 AND OrderID=@value1 ORDER BY DueDate DESC, MeterReadDate DESC", new Object[] { parkId, orderId });
            readDate.SelectedIndex = 0;
            this.Size = new Size(970, 750);
        }

        private void readDate_SelectedIndexChanged(object sender, EventArgs e) {
            meterReadId = ((CommonTools.Item)readDate.SelectedItem).Value;
            Object[] read = DatabaseControl.getSingleRecord(new String[] { "DueDate", "StartDate", "MeterReadDate" }, 
                DatabaseControl.meterReadsTable, "MeterReadID=@value0", new Object[] { meterReadId });
            BillCalculation bill = new BillCalculation(parkId, parkSpaceId, (DateTime)read[0]);
            dueDate = (DateTime)read[0];
            Object[] previous = DatabaseControl.getSingleRecord(new String[] { "ParkSpaceID" }, 
                DatabaseControl.spaceTable, "ParkID=@value0 AND OrderID=@value1 AND MoveInDate<=@value2 AND MoveOutDate>=@value3", 
                new Object[] { parkId, orderId, read[1], read[2] });
            if (previous != null) {
                parkSpaceId = (int)previous[0];
                String[] fields = { "Tenant", "Address1", "Address2", "City", "State", "Zip" };
                String condition = "ParkSpaceID=@value0";
                object[] tenant = DatabaseControl.getSingleRecord(fields, DatabaseControl.spaceTable, condition, new Object[] { this.parkSpaceId });
                Object movedOut = DatabaseControl.getSingleRecord(new String[] { "MoveOutDate" }, DatabaseControl.spaceTable, condition, new Object[] { this.parkSpaceId })[0];
                if (movedOut == DBNull.Value) { moveOut.Visible = true; } else { moveOut.Visible = false; }
                tenantInfo.Text = String.Format("{0}\tSpace # {6}\n{1}\n{3}, {4} {5}", tenant, orderId);
            }

            parkMessage = DatabaseControl.getSingleRecord(new String[] { "TopMessage", "BotMessage" }, DatabaseControl.messageTable, 
                "ParkID=@value0 AND DueDate=@value1", new Object[] { parkId, dueDate });
            if (parkMessage == null) parkMessage = DatabaseControl.getSingleRecord(new String[] { "TopMessage", "BotMessage" }, DatabaseControl.messageTable,
                "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId });
            topMessageInput.Text = parkMessage[0].ToString();
            botMessageInput.Text = parkMessage[1].ToString();
            displayBill(bill.generateBill(), (DateTime)read[0]);
        }

        private void billBtn_Click(object sender, EventArgs e) {
            List<Object[]> spaces = new List<Object[]>();
            DateTime dueDate;
            if (parkList.SelectedIndex != -1 && tenantList.SelectedIndex != -1) {
                dueDate = (DateTime)DatabaseControl.getSingleRecord(new String[] { "DueDate" }, DatabaseControl.meterReadsTable, "MeterReadID=@value0",
                    new Object[] { meterReadId })[0];
                saveSummaryOfCharges(parkSpaceId, dueDate);
                saveTempCharges(parkSpaceId, dueDate);
                parkMessage[0] = topMessageInput.Text;
                parkMessage[1] = botMessageInput.Text;
                billComputation(new Object[] { parkSpaceId }, dueDate);
                MessageBox.Show("Done generating Pdf!");
                return;
            } else if (parkList.SelectedIndex != -1) {
                Object[] dateInfo = DatabaseControl.getSingleRecord(new String[] { "StartDate", "MeterReadDate", "DueDate" }, DatabaseControl.periodTable,
                    "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId });
                dueDate = (DateTime)dateInfo[2];
                parkMessage = DatabaseControl.getSingleRecord(new String[] { "TopMessage", "BotMessage" }, DatabaseControl.messageTable, 
                    "ParkID=@value0 AND DueDate=@value1", new Object[] { parkId, dueDate });
                if (parkMessage == null) {
                    parkMessage = DatabaseControl.getSingleRecord(new String[] { "TopMessage", "BotMessage" }, DatabaseControl.messageTable,
                        "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId });
                    DatabaseControl.executeInsertQuery(DatabaseControl.messageTable, new String[] { "ParkID", "DueDate", "TopMessage", "BotMessage" },
                        new Object[] { parkId, dueDate, parkMessage[0], parkMessage[1] });
                }
                //String query = "SELECT ParkSpaceID FROM ParkSpaceTenant JOIN (SELECT OrderID, MAX(MoveInDate) MaxDate FROM MyTable GROUP BY OrderID) t ON ParkSpaceTenant.OrderID = t.OrderID AND ParkSpaceTenant.MoveInDate = t.MaxDate";
                spaces = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID" }, "ParkSpaceTenant JOIN (SELECT OrderID, MAX(MoveInDate) MaxDate FROM ParkSpaceTenant WHERE ParkID=@value0 AND MoveInDate<@value1 GROUP BY OrderID) t ON ParkSpaceTenant.OrderID = t.OrderID AND ParkSpaceTenant.MoveInDate = t.MaxDate", 
                    "ParkID=@value0", new Object[] { parkId, dateInfo[1] });
                //spaces = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID" }, DatabaseControl.spaceTable,
                    //"ParkID=@value0 AND MoveInDate<@value2 AND (MoveOutDate IS NULL OR MoveOutDate>@value1) ORDER BY OrderID", new Object[] { parkId, dateInfo[0], dateInfo[1] });
            } else {
                return;
            }
            //DatabaseControl.deleteRecords(DatabaseControl.spaceBillTable, "DueDate=@value0", new Object[] { dueDate });
            //DatabaseControl.deleteRecords(DatabaseControl.spaceTempChargeTable, "DueDate=@value0", new Object[] { dueDate });
            //DatabaseControl.deleteRecords(DatabaseControl.spaceChargeTable, "DueDate=@value0", new Object[] { dueDate });
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            Parallel.ForEach(spaces.AsEnumerable(), delegate(Object[] space) { billComputation(space, dueDate); });
            timer.Stop();
            MessageBox.Show("Done generating Pdf. Time(seconds):" + timer.Elapsed.TotalSeconds);
        }

        private void billComputation(Object[] space, DateTime dueDate) {
            BillCalculation bill = new BillCalculation(parkId, (int)space[0], dueDate);
            Object[] info = bill.generateBill();
            decimal gasTotal, eleTotal, watTotal;
            gasTotal = (decimal)((Object[])info[5])[2];
            eleTotal = (decimal)((Object[])info[6])[2];
            watTotal = (decimal)((Object[])info[7])[2];
            List<Object[]> summary = getSummaryOfCharges((int)space[0], dueDate);
            List<Object[]> temporary = getTempCharges((int)space[0], bill.start, bill.end, bill.dueDate);
            List<Object[]> charges = new List<Object[]>();
            foreach (Object[] charge in summary) charges.Add(charge);
            foreach (Object[] charge in temporary) charges.Add(charge);
            charges.Add(new Object[] { "Utilities", gasTotal + eleTotal + watTotal });
            info[3] = parkInfo;
            info[8] = charges;
            info[11] = parkMessage;

            //PdfControl.createBillPdf((int)space[0], info);
            //PdfControl.createBillPrint((int)space[0], info);
            Object[] usage = (Object[])info[4];

            String condition = "ParkSpaceID=@value0 AND DueDate=@value1";
            DatabaseControl.deleteRecords(DatabaseControl.spaceBillTable, condition,
                new Object[] { (int)space[0], bill.dueDate });
            DatabaseControl.deleteRecords(DatabaseControl.spaceChargeTable, condition,
                new Object[] { (int)space[0], bill.dueDate});
            DatabaseControl.deleteRecords(DatabaseControl.spaceTempChargeTable, condition,
                new Object[] { (int)space[0], bill.dueDate });

            Object[] utilUsage = (Object[])usage[0];
            Object[] gasReads = (Object[])usage[1];
            Object[] eleReads = (Object[])usage[2];
            Object[] watReads = (Object[])usage[3];

            Object[] values = { (int)space[0], bill.dueDate, gasReads[3], gasReads[4], utilUsage[1], gasTotal, eleReads[3], eleReads[4], utilUsage[4], eleTotal, watReads[3], watReads[4], utilUsage[7], watTotal };
            DatabaseControl.executeInsertQuery(DatabaseControl.spaceBillTable, DatabaseControl.spaceBillColumns, values);
            foreach (Object[] item in summary) {
                values = new Object[] { (int)space[0], bill.dueDate, item[0], item[1], item[2] };
                DatabaseControl.executeInsertQuery(DatabaseControl.spaceChargeTable, DatabaseControl.spaceChargeColumns, values);
            }
            foreach (Object[] item in temporary) {
                if (item[0].ToString().Equals("Utilities")) continue;
                values = new Object[] { (int)space[0], bill.dueDate, item[0], item[1] };
                DatabaseControl.executeInsertQuery(DatabaseControl.spaceTempChargeTable, DatabaseControl.spaceTempChargeColumns, values);
            }
        }
        
        private void displayBill(Object[] info, DateTime dueDate) {
            tenantInfo.Text = clerkInfo.Text = usageInfo.Text = readInfo.Text = "";
            gasInfo.Text = watInfo.Text = eleInfo.Text = "";
            displayTenantInfo((Object[])info[2]);
            displayClerkInfo(parkInfo);
            displayUsageInfo((Object[])((Object[])info[4])[0]);
            displayReadInfo((Object[])info[4]);
            displayGasInfo((Object[])info[5]);
            displayEleInfo((Object[])info[6]);
            displayWatInfo((Object[])info[7]);
            decimal gasTotal, eleTotal, watTotal;
            if (info[5] != null) gasTotal = (decimal)((Object[])info[5])[2]; else gasTotal = 0;
            if (info[6] != null) eleTotal = (decimal)((Object[])info[6])[2]; else eleTotal = 0;
            if (info[7] != null) watTotal = (decimal)((Object[])info[7])[2]; else watTotal = 0;
            decimal utilities = gasTotal + eleTotal + watTotal;
            displaySummaryOfCharges(dueDate);
            displayTempCharges(utilities, dueDate);
        }

        private void displayTenantInfo(Object[] info) {
            tenantInfo.Text = String.Format("{0}\tSpace # {2}\n{1}\n{3}, {4} {5}", info);
        }

        private void displayClerkInfo(Object[] info) {
            clerkInfo.Text = String.Format("{0}\n{1}\n{2}, {3} {4}", info);
        }

        private void displayUsageInfo(Object[] info) {
            for (int i = 0; i < info.Length; i++) if ((int)info[i] == -1) info[i] = "";
            usageInfo.Text = String.Format("(GAS)Days:{0, -6}Usage:{1, -6}Last Year:{2}|(ELE)Days:{3, -6}Usage:{4, -6}Last Year:{5}|(WAT)Days:{6, -6}Usage:{7, -6}Last Year:{8}", info);
        }

        private void displayReadInfo(Object[] info) {
            for (int i = 1; i <= 3; i++) {
                Object[] item = (Object[])info[i];
                readInfo.Text += String.Format("{0, -3}{1, 15}{2, 15}{3, 15}{4, 15}{5, 15}{6, 15}{7, 15}", item);
                if (i != 3) readInfo.Text += "\n";
            }
        }

        private void displayGasInfo(Object[] info) {
            String gas = "";
            if (info == null) {
                gas += String.Format("Gas{0, 35}\n", 0);
                gas += String.Format("\nTotal: {0, 25}", (0.00M).ToString());
                gasInfo.Text = gas;
                return;
            }

            gas += String.Format("Gas{0, 35}\n", info[0]);

            List<Object[]> details = (List<Object[]>)info[1];
            foreach (Object[] item in details) {
                String description = item[0].ToString();
                if (description.Length >= 25) description += String.Format("\n  {0, -25}", "");

                if (item.Length == 1) gas += description + "\n";
                else if ((decimal)item[1] != 0.0M) gas += String.Format("  {0, -25} | {1, 8}\n", description, item[1]);
            }

            gas += String.Format("\nTotal: {0, 25}", info[2]);

            gasInfo.Text = gas;
        }

        private void displayEleInfo(Object[] info) {
            String ele = "";
            if (info == null) {
                ele += String.Format("Ele{0, 35}\n", 0);
                ele += String.Format("\nTotal: {0, 25}", (0.00M).ToString());
                eleInfo.Text = ele;
                return;
            }

            ele += String.Format("Ele{0, 35}\n", info[0]);

            List<Object[]> details = (List<Object[]>)info[1];
            foreach (Object[] item in details) {
                String description = item[0].ToString();
                if (item.Length == 1) { ele += description + "\n"; continue; } 
                if (description.Length >= 25) description += String.Format("\n  {0, -25}", "");
                if ((decimal)item[1] != 0.0M) ele += String.Format("  {0, -25} | {1, 8}\n", description, item[1]);
            }

            ele += String.Format("\nTotal: {0, 25}", info[2]);

            eleInfo.Text = ele;
        }

        private void displayWatInfo(Object[] info) {
            String wat = "";
            if (info == null) {
                wat += String.Format("Wat{0, 35}\n", 0);
                wat += String.Format("\nTotal: {0, 25}", (0.00M).ToString());
                watInfo.Text = wat;
                return;
            }

            wat += String.Format("Wat{0, 35}\n", info[0]);

            List<Object[]> details = (List<Object[]>)info[1];
            foreach (Object[] item in details) {
                String description = item[0].ToString();
                if (description.Length >= 25) description += String.Format("\n  {0, -25}", "");
                if ((decimal)item[1] != 0.0M) wat += String.Format("  {0, -25} | {1, 8}\n", description, item[1]);
            }

            wat += String.Format("\nTotal: {0, 25}", info[2]);

            watInfo.Text = wat;
        }

        private void displaySummaryOfCharges(DateTime dueDate) {
            summaryOfCharges.Columns.Clear();
            summaryOfCharges.Columns.Add("optionName", "Option");
            summaryOfCharges.Columns["optionName"].Width = 180;
            summaryOfCharges.Columns.Add("optionCharge", "Charge");
            summaryOfCharges.Columns["optionCharge"].Width = 80;
            summaryOfCharges.Columns.Add("optionCode", "Code");
            summaryOfCharges.Columns["optionCode"].Width = 40;

            //String condition = "ParkSpaceID=@value0 AND DueDate=@value1";
            String condition = "ParkSpaceID=@value0 AND DueDate=@value1";
            List<Object[]> items = DatabaseControl.getMultipleRecord(new String[] { "Description", "ChargeItemValue", "ChargeItemID" }, 
                DatabaseControl.spaceChargeTable, condition, new Object[] { parkSpaceId, dueDate });
            if (items.Count == 0) {
                String table = DatabaseControl.parkOptionsTable + " JOIN " + DatabaseControl.optionsTable + " ON ChargeItem.ChargeItemID=ParkChargeItem.ChargeItemID";
                List<Object[]> parkItems = DatabaseControl.getMultipleRecord(new String[] { "ChargeItemDescription, ChargeItemValue", "ChargeItem.ChargeItemID" }, table,
                        "ParkId=@value0 ORDER BY ChargeItem.ChargeItemID ASC", new Object[] { parkId });

                condition = "ParkSpaceID=@value0 AND DueDate=(SELECT MAX(DueDate) FROM ParkSpaceCharge WHERE ParkSpaceID=@value0)";
                items = DatabaseControl.getMultipleRecord(new String[] { "Description", "ChargeItemValue", "ChargeItemID" },
                    DatabaseControl.spaceChargeTable, condition, new Object[] { parkSpaceId, dueDate });
                List<String> temp = new List<String>();
                foreach (Object[] item in items) temp.Add(item[0].ToString());

                decimal offset = 0M;
                foreach (Object[] parkItem in parkItems) if (!temp.Contains(parkItem[0].ToString())) items.Add(parkItem);

                foreach (Object[] item in items) if (item[0].ToString() == "Balance Forward" || item[0].ToString() == "Late Charge" || item[0].ToString() == "Concession") item[1] = 0M; else offset -= (decimal)item[1];
                bool concession = (bool)DatabaseControl.getSingleRecord(new String[] { "Concession" }, DatabaseControl.spaceTable, "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];
                foreach (Object[] item in items) { if (item[0].ToString() == "Concession" && concession) item[1] = offset; }
            }

            if (!items.Exists(delegate(Object[] obj) { return obj[0].ToString() == "Balance Forward"; })) items.Add(new Object[] { "Balance Forward", 0.00M, 1 });
            if (!items.Exists(delegate(Object[] obj) { return obj[0].ToString() == "Late Charge"; })) items.Add(new Object[] { "Late Charge", 0.00M, 2 });

            for (int i = 0; i < items.Count; i++) {
                Object[] item = items[i];
                summaryOfCharges.Rows.Add();
                summaryOfCharges.Rows[i].Cells["optionName"].Value = item[0].ToString();
                summaryOfCharges.Rows[i].Cells["optionCharge"].Value = ((decimal)item[1]).ToString("N2");
                summaryOfCharges.Rows[i].Cells["optionCode"].Value = (int)item[2];
            }

            summaryOfCharges.Columns["optionName"].ReadOnly = true;
            summaryOfCharges.Columns["optionCode"].ReadOnly = true;
            summaryOfCharges.Sort(summaryOfCharges.Columns["optionCode"], ListSortDirection.Ascending);
            summaryOfCharges.AllowUserToAddRows = false;
            summaryOfCharges.AllowUserToDeleteRows = false;
            this.ActiveControl = summaryOfCharges;
            summaryOfCharges.ClearSelection();
            if (summaryOfCharges.Rows.Count > 0) summaryOfCharges.CurrentCell = summaryOfCharges[1, 0];
        }

        private void displayTempCharges(decimal utilTotal, DateTime dueDate) {
            tempCharges.Columns.Clear();
            tempCharges.Columns.Add("optionName", "Temporary Option");
            tempCharges.Columns["optionName"].Width = 180;
            tempCharges.Columns.Add("optionCharge", "Charge");
            tempCharges.Columns["optionCharge"].Width = 80;
            ArrayList temp;

            String condition = "ParkSpaceID=@value0 AND DueDate=@value1";
            temp = DatabaseControl.getMultipleRecordDict(new String[] { "Description", "ChargeItemValue" }, DatabaseControl.spaceTempChargeTable, condition, 
                new Object[] { parkSpaceId, dueDate });

            /*if (temp.Count == 0) {
                condition = "OrderID=@value0 AND DueDate=@value1";
                int orderId = (int)DatabaseControl.getSingleRecord(new String[] { "OrderID" }, DatabaseControl.spaceTable, "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];
                Object[] readDates = DatabaseControl.getSingleRecord(new String[] { "StartDate", "MeterReadDate" },
                    DatabaseControl.meterReadsTable, condition, new Object[] { orderId, dueDate });
                temp = DatabaseControl.getMultipleRecordDict(new String[] { "Description", "Charge" }, DatabaseControl.tempChargeTable,
                    "ParkID=@value0 AND DateAssigned>=@value1 AND DateAssigned<@value2", new Object[] { parkId, readDates[0], readDates[1] });
            }*/
            
            for(int i = 0; i < temp.Count; i++ ) {
                Dictionary<String, Object> charge = (Dictionary<String, Object>)temp[i];
                tempCharges.Rows.Add();
                tempCharges.Rows[i].Cells["optionName"].Value = charge["Description"].ToString();
                tempCharges.Rows[i].Cells["optionCharge"].Value = ((decimal)charge["ChargeItemValue"]).ToString("N2");
            }

            tempCharges.Rows.Add();
            tempCharges.Rows[tempCharges.RowCount - 2].Cells["optionName"].Value = "Utilities";
            tempCharges.Rows[tempCharges.RowCount - 2].Cells["optionCharge"].Value = utilTotal.ToString("N2");

            tempCharges.ClearSelection();
            tempCharges.CurrentCell = tempCharges[1, 0];
        }

        public List<Object[]> getSummaryOfCharges(int parkSpaceId, DateTime dueDate) {
            List<Object[]> previousItems = DatabaseControl.getMultipleRecord(new String[] { "Description", "ChargeItemValue", "ChargeItemID" }, DatabaseControl.spaceChargeTable,
                "ParkSpaceID=@value0 AND DueDate=@value1 ORDER BY ChargeItemID ASC", new Object[] { parkSpaceId, dueDate });
            if (previousItems.Count == 0) {
                String table = DatabaseControl.parkOptionsTable + " JOIN " + DatabaseControl.optionsTable + " ON ChargeItem.ChargeItemID=ParkChargeItem.ChargeItemID";
                List<Object[]> items = DatabaseControl.getMultipleRecord(new String[] { "ChargeItemDescription", "ChargeItemValue", "ChargeItem.ChargeItemID" }, table, "ParkId=@value0 ORDER BY ChargeItem.ChargeItemID ASC", new Object[] { parkId });

                previousItems = DatabaseControl.getMultipleRecord(new String[] { "Description", "ChargeItemValue", "ChargeItemID" }, DatabaseControl.spaceChargeTable,
                    "ParkSpaceID=@value0 AND DueDate=(SELECT MAX(DueDate) FROM ParkSpaceCharge WHERE ParkSpaceID=@value0) ORDER BY ChargeItemID ASC", new Object[] { parkSpaceId });
                List<String> temp = new List<String>();
                foreach (Object[] previous in previousItems) temp.Add(previous[0].ToString());

                foreach (Object[] item in items) if (!temp.Contains(item[0].ToString())) previousItems.Add(item);
                decimal offset = 0M;
                foreach (Object[] item in previousItems) if ((int)item[2] == 1 || (int)item[2] == 2) item[1] = 0M; else offset -= (decimal)item[1];

                bool concession = (bool)DatabaseControl.getSingleRecord(new String[] { "Concession" }, DatabaseControl.spaceTable, "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];
                foreach (Object[] item in items) { if ((int)item[2] == 8 && concession) item[1] = offset; }
            }
            return previousItems;
        }

        private void saveSummaryOfCharges(int parkSpaceId, DateTime dueDate) {
            String condition = "ParkSpaceID=@value0 AND DueDate=@value1";
            DatabaseControl.deleteRecords(DatabaseControl.spaceChargeTable, condition,
                new Object[] { parkSpaceId, dueDate });
            foreach (DataGridViewRow row in summaryOfCharges.Rows) {
                Object[] values = new Object[] { parkSpaceId, dueDate, row.Cells["optionName"].Value, Decimal.Round(Convert.ToDecimal(row.Cells["optionCharge"].Value), 2), Convert.ToInt32(row.Cells["optionCode"].Value) };
                DatabaseControl.executeInsertQuery(DatabaseControl.spaceChargeTable, DatabaseControl.spaceChargeColumns, values);
            }
        }

        private void saveTempCharges(int parkSpaceId, DateTime dueDate) {
            String condition = "ParkSpaceID=@value0 AND DueDate=@value1";
            DatabaseControl.deleteRecords(DatabaseControl.spaceTempChargeTable, condition, new Object[] { parkSpaceId, dueDate });

            foreach (DataGridViewRow row in tempCharges.Rows) {
                if (row.IsNewRow || row.Cells["optionName"].Value.ToString() == "Utilities") continue;
                Object[] values = new Object[] { parkSpaceId, dueDate, row.Cells["optionName"].Value, Convert.ToDecimal(row.Cells["optionCharge"].Value) };
                DatabaseControl.executeInsertQuery(DatabaseControl.spaceTempChargeTable, DatabaseControl.spaceTempChargeColumns, values);
            }
        }

        public List<Object[]> getTempCharges(int parkSpaceId, DateTime start, DateTime end, DateTime dueDate) {
            List<Object[]> temp = DatabaseControl.getMultipleRecord(new String[] { "Description", "ChargeItemValue" }, DatabaseControl.spaceTempChargeTable,
                "ParkSpaceID=@value0 AND DueDate=@value1", new Object[] { parkSpaceId, dueDate });
            /*if (temp.Count == 0) {
                temp = DatabaseControl.getMultipleRecord(new String[] { "Description", "Charge"}, DatabaseControl.tempChargeTable,
                    "ParkID=@value0 AND DateAssigned>=@value1 AND DateAssigned<=@value2", new Object[] { parkId, start, end });
            }*/
            return temp;
        }

        private void historyBtn_Click(object sender, EventArgs e) {
            if (parkList.SelectedIndex != -1) { parkId = ((CommonTools.Item)parkList.SelectedItem).Value; }
            if (tenantList.SelectedIndex != -1) { parkSpaceId = ((CommonTools.Item)tenantList.SelectedItem).Value; }
            if (parkId != -1 && parkSpaceId != -1) {
                ReadHistory history = new ReadHistory(parkId, parkSpaceId);
                history.ShowDialog();
                readDate.SelectedIndex = 0;
                readDate_SelectedIndexChanged(sender, e);
            }
        }

        private void reportBtn_Click(object sender, EventArgs e) {
            if (parkList.SelectedIndex != -1) {
                ParkReport report = new ParkReport(parkId);
                Object[] read = DatabaseControl.getSingleRecord(new String[] { "DueDate" }, DatabaseControl.periodTable,
                    "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId });
                report.generateSummReport((DateTime)read[0]);
                MessageBox.Show("Park Billing Report is generated!");
            }
        }

        private void utilBtn_Click(object sender, EventArgs e) {
            if (parkList.SelectedIndex != -1) {
                ParkReport report = new ParkReport(parkId);
                Object[] read = DatabaseControl.getSingleRecord(new String[] { "DueDate" }, DatabaseControl.meterReadsTable,
                    "ParkID=@value0 ORDER BY MeterReadDate DESC", new Object[] { parkId });
                report.generateUtilReport((DateTime)read[0]);

                MessageBox.Show("Park Utility Report is generated!");
            }
        }

        private void parkBillsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (parkList.SelectedIndex != -1) {
                billBtn_Click(sender, e);
            }
        }

        private void moveOut_Click(object sender, EventArgs e) {
            MoveOut move = new MoveOut(parkId, parkSpaceId);
            Object[] tempInfo = DatabaseControl.getSingleRecord(new String[] { "OrderID", "SpaceID" }, DatabaseControl.spaceTable, "ParkSpaceID=@value0",
                new Object[] { parkSpaceId });
            move.ShowDialog();
            if (move.newTenant) {
                ParkSpaceInfo newTenant = new ParkSpaceInfo();
                newTenant.fillParkSpaceInfo(parkId, (int)tempInfo[0], tempInfo[1].ToString());
                newTenant.ShowDialog();
            }
            tenantList_SelectedIndexChanged(sender, e);
        }

        private void eCReportToolStripMenuItem_Click(object sender, EventArgs e) {
            if (parkList.SelectedIndex != -1) {
                ParkReport report = new ParkReport(parkId);
                Object[] read = DatabaseControl.getSingleRecord(new String[] { "StartDate", "MeterReadDate" }, DatabaseControl.meterReadsTable,
                    "ParkID=@value0 ORDER BY MeterReadDate DESC", new Object[] { parkId });
                report.generateExtraChargesReport((DateTime)read[0], (DateTime)read[1]);
                MessageBox.Show("Extra Charges Report is generated!");
            }
        }

        private void readSheetToolStripMenuItem_Click(object sender, EventArgs e) {
            if (parkList.SelectedIndex != -1) {
                ParkReport report = new ParkReport(parkId);
                SelectDate obj = new SelectDate(parkId);
                obj.ShowDialog();
                
                report.generateReadSheet(obj.readDateSelected);
                MessageBox.Show("Read Sheet generated!");
            }
        }

        private void collectionsToolStripMenuItem_Click(object sender, EventArgs e) {
            Object dueDate = DatabaseControl.getSingleRecord(new String[] { "DueDate" }, DatabaseControl.periodTable, "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId })[0];
            String santiago = DatabaseControl.getSingleRecord(new String[] { "CsvId" }, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { parkId })[0].ToString();
            if (santiago == "") {
                Object[] meterReadDates = DatabaseControl.getSingleRecord(new String[] { "StartDate", "MeterReadDate" }, DatabaseControl.periodTable,
                        "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId });
                List<Object[]> spaces = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID" }, DatabaseControl.spaceTable,
                        "ParkID=@value0 AND (MoveOutDate IS NULL OR MoveOutDate > @value1) AND MoveInDate < @value2", new Object[] { parkId, (DateTime)meterReadDates[0], (DateTime)meterReadDates[1] });
                List<Object[]> rows = new List<Object[]>();
                String table = DatabaseControl.parkOptionsTable + " JOIN " + DatabaseControl.optionsTable + " ON ChargeItem.ChargeItemID=ParkChargeItem.ChargeItemID";
                List<Object[]> parkCharge = DatabaseControl.getMultipleRecord(new String[] { "ChargeItemDescription" }, table, "ParkID=@value0 ORDER BY ChargeItemCode", new Object[] { parkId });
                foreach (Object[] space in spaces) {
                    Object[] startend = new Object[] { meterReadDates[0], meterReadDates[1] };
                    Dictionary<String, Object> moveDates = DatabaseControl.getSingleRecordDict(new String[] { "MoveInDate", "MoveOutDate" },
                        DatabaseControl.spaceTable, "ParkSpaceID=@value0", new Object[] { (int)space[0] });
                    if (moveDates["MoveOutDate"] != DBNull.Value) startend[1] = moveDates["MoveOutDate"];
                    if ((DateTime)moveDates["MoveInDate"] > (DateTime)meterReadDates[0]) startend[0] = moveDates["MoveInDate"];
                    String[] tenantFields = new String[] { "SpaceID", "Tenant", "GasStatus", "GasType", "EleStatus", "EleType", "WatStatus", "GasMedical", "EleMedical", "DontBillForGas", "DontBillForEle", "DontBillForWat", "OrderID" };
                    Dictionary<String, Object> tenantInfo = DatabaseControl.getSingleRecordDict(DatabaseControl.spaceColumns, DatabaseControl.spaceTable,
                        "ParkID=@value0 AND ParkSpaceID=@value1", new Object[] { parkId, space[0] });
                    String[] billFields = new String[] { "GasPreviousRead", "GasCurrentRead", "GasUsage", "GasBill", "ElePreviousRead", "EleCurrentRead", "EleUsage", "EleBill", "WatPreviousRead", "WatCurrentRead", "WatUsage", "WatBill" };
                    Dictionary<String, Object> billInfo = DatabaseControl.getSingleRecordDict(DatabaseControl.spaceBillColumns, DatabaseControl.spaceBillTable,
                        "ParkSpaceID=@value0 AND DueDate=@value1", new Object[] { space[0], dueDate });
                    if (billInfo == null || billInfo.Count == 0) { continue; }
                    List<Object[]> summary = DatabaseControl.getMultipleRecord(new String[] { "Description", "ChargeItemValue", "ParkSpaceCharge.ChargeItemID" }, 
                        DatabaseControl.spaceChargeTable + " JOIN " + DatabaseControl.optionsTable + " ON ChargeItem.ChargeItemID=ParkSpaceCharge.ChargeItemID",
                        "ParkSpaceID=@value0 AND DueDate=@value1 ORDER BY ChargeItemCode ASC", new Object[] { (int)space[0], dueDate });
                    List<Object[]> temporary = getTempCharges((int)space[0], (DateTime)startend[0], (DateTime)startend[1], (DateTime)dueDate);
                    decimal balFw = 0M;
                    decimal lateCh = 0M;
                    foreach (Object[] item in summary) {
                        if ((int)item[2] == 1) {
                            balFw = (decimal)item[1];
                        } else if ((int)item[2] == 2) {
                            lateCh = (decimal)item[1];
                        }
                    }

                    rows.Add(new Object[] { tenantInfo, startend[1], startend[0], balFw, lateCh, summary, temporary, billInfo });
                }
                CommonTools.createCollectionsCSV(parkId, (DateTime)dueDate, rows, parkCharge);
            } else {
                String table = DatabaseControl.spaceTable + " JOIN " + DatabaseControl.spaceChargeTable + " ON ParkSpaceTenant.ParkSpaceID=ParkSpaceCharge.ParkSpaceID JOIN " +
                DatabaseControl.optionsTable + " ON ParkSpaceCharge.Description=ChargeItem.ChargeItemDescription";
                String[] fields = new String[] { "SpaceID", "Description", "DueDate", "ChargeItemSantiago", "ChargeItemValue" };
                String condition = "DueDate=@value0 AND ParkID=@value1 ORDER BY SpaceID";
                ArrayList info = DatabaseControl.getMultipleRecordDict(fields, table, condition, new Object[] { dueDate, parkId });
                
                fields = new String[] { "SpaceID", "GasBill", "EleBill", "WatBill" };
                table = DatabaseControl.spaceTable + " JOIN " + DatabaseControl.spaceBillTable + 
                    " ON ParkSpaceTenant.ParkSpaceID=ParkSpaceBill.ParkSpaceID";
                ArrayList items = DatabaseControl.getMultipleRecordDict(fields, table, condition, new Object[] { dueDate, parkId });
                foreach (Dictionary<String, Object> item in items) {
                    info.Add(new Dictionary<String, Object>() { 
                        {"SpaceID", item["SpaceID"]},
                        {"Description", "Electric"},
                        {"DueDate", dueDate},
                        {"ChargeItemSantiago", "/E"},
                        {"ChargeItemValue", item["EleBill"]},
                    });
                    info.Add(new Dictionary<String, Object>() { 
                        {"SpaceID", item["SpaceID"]},
                        {"Description", "Gas"},
                        {"DueDate", dueDate},
                        {"ChargeItemSantiago", "/G"},
                        {"ChargeItemValue", item["GasBill"]},
                    });
                    info.Add(new Dictionary<String, Object>() { 
                        {"SpaceID", item["SpaceID"]},
                        {"Description", "Water"},
                        {"DueDate", dueDate},
                        {"ChargeItemSantiago", "/W"},
                        {"ChargeItemValue", item["WatBill"]},
                    });
                }
                CSVGenerator.santiagoFormat(santiago, info);
            }
            MessageBox.Show("Collections file generated!");
        }

        private void TenantBill_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control) {
                this.ActiveControl = spaceList;
            } else if (e.KeyCode == Keys.Right && e.Modifiers == Keys.Control) {
                this.ActiveControl = tempCharges;
            } else if (e.KeyCode == Keys.Left && e.Modifiers == Keys.Control) {
                this.ActiveControl = summaryOfCharges;
            }
        }

        private void tenantToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void gasHistoryToolStripMenuItem_Click(object sender, EventArgs e) {
            String[] fields = new String[] { "DueDate", "StartDate", "MeterReadDate", "GasReadValue" };
            String condition = "ParkID=@value0 AND OrderID=@value1 ORDER BY DueDate DESC";
            List<Object[]> readInfo = DatabaseControl.getMultipleRecord(fields, DatabaseControl.meterReadsTable, condition, new Object[] { parkId, orderId });
            DateTime end = (DateTime)readInfo[0][2];
            DateTime start;
            if (readInfo.Count < 25) {
                start = (DateTime)readInfo[readInfo.Count-1][1];
            } else {
                start = (DateTime)readInfo[24][1];
            }
            List<Object[]> tenants = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID", "Tenant", "MoveInDate", "MoveOutDate" },
                DatabaseControl.spaceTable, "ParkID=@value0 AND OrderID=@value1 AND MoveInDate>=@value2 AND (MoveOutDate<=@value3 OR MoveOutDate is NULL) ORDER BY MoveInDate DESC",
                new Object[] { parkId, orderId, start, end });
            List<Object[]> bills = new List<Object[]>();
            foreach (Object[] tenant in tenants) {
                List<Object[]> tempBills = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID", "DueDate", "GasUsage", "GasBill" }, DatabaseControl.spaceBillTable,
                    "ParkSpaceID=@value0 ORDER BY DueDate DESC", new Object[] { tenant[0] });
                foreach (Object[] temp in tempBills) {
                    temp[0] = tenant[1];
                    bills.Add(temp);
                    if (bills.Count > 24) break;
                }
                if (bills.Count > 24) break;
            }
            PdfControl.historyReport("Gas Report", parkInfo, orderId, bills, readInfo);
        }

        private void eleHistoryToolStripMenuItem_Click(object sender, EventArgs e) {
            String[] fields = new String[] { "DueDate", "StartDate", "MeterReadDate", "EleReadValue" };
            String condition = "ParkID=@value0 AND OrderID=@value1 ORDER BY DueDate DESC";
            List<Object[]> readInfo = DatabaseControl.getMultipleRecord(fields, DatabaseControl.meterReadsTable, condition, new Object[] { parkId, orderId });
            DateTime end = (DateTime)readInfo[0][2];
            DateTime start;
            if (readInfo.Count < 25) {
                start = (DateTime)readInfo[readInfo.Count - 1][1];
            } else {
                start = (DateTime)readInfo[24][1];
            }
            List<Object[]> tenants = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID", "Tenant", "MoveInDate", "MoveOutDate" },
                DatabaseControl.spaceTable, "ParkID=@value0 AND OrderID=@value1 AND MoveInDate>=@value2 AND (MoveOutDate<=@value3 OR MoveOutDate is NULL) ORDER BY MoveInDate DESC",
                new Object[] { parkId, orderId, start, end });
            List<Object[]> bills = new List<Object[]>();
            foreach (Object[] tenant in tenants) {
                List<Object[]> tempBills = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID", "DueDate", "EleUsage", "EleBill" }, DatabaseControl.spaceBillTable,
                    "ParkSpaceID=@value0 ORDER BY DueDate DESC", new Object[] { tenant[0] });
                foreach (Object[] temp in tempBills) {
                    temp[0] = tenant[1];
                    bills.Add(temp);
                    if (bills.Count > 24) break;
                }
                if (bills.Count > 24) break;
            }
            PdfControl.historyReport("Electricty Report", parkInfo, orderId, bills, readInfo);
        }

        private void watHistoryToolStripMenuItem_Click(object sender, EventArgs e) {
            String[] fields = new String[] { "DueDate", "StartDate", "MeterReadDate", "WatReadValue" };
            String condition = "ParkID=@value0 AND OrderID=@value1 ORDER BY DueDate DESC";
            List<Object[]> readInfo = DatabaseControl.getMultipleRecord(fields, DatabaseControl.meterReadsTable, condition, new Object[] { parkId, orderId });
            DateTime end = (DateTime)readInfo[0][2];
            DateTime start;
            if (readInfo.Count < 25) {
                start = (DateTime)readInfo[readInfo.Count - 1][1];
            } else {
                start = (DateTime)readInfo[24][1];
            }
            List<Object[]> tenants = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID", "Tenant", "MoveInDate", "MoveOutDate" },
                DatabaseControl.spaceTable, "ParkID=@value0 AND OrderID=@value1 AND MoveInDate>=@value2 AND (MoveOutDate<=@value3 OR MoveOutDate is NULL) ORDER BY MoveInDate DESC",
                new Object[] { parkId, orderId, start, end });
            List<Object[]> bills = new List<Object[]>();
            foreach (Object[] tenant in tenants) {
                List<Object[]> tempBills = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID", "DueDate", "WatUsage", "WatBill" }, DatabaseControl.spaceBillTable,
                    "ParkSpaceID=@value0 ORDER BY DueDate DESC", new Object[] { tenant[0] });
                foreach (Object[] temp in tempBills) {
                    temp[0] = tenant[1];
                    bills.Add(temp);
                    if (bills.Count > 24) break;
                }
                if (bills.Count > 24) break;
            }
            PdfControl.historyReport("Water Report", parkInfo, orderId, bills, readInfo);
        }

        private void allHistoryToolStripMenuItem_Click(object sender, EventArgs e) {
            watHistoryToolStripMenuItem_Click(sender, e);
            eleHistoryToolStripMenuItem_Click(sender, e);
            gasHistoryToolStripMenuItem_Click(sender, e);
        }

        private void readIssuesToolStripMenuItem_Click(object sender, EventArgs e) {
            List<Object[]> gasIssues = new List<Object[]>();
            List<Object[]> eleIssues = new List<Object[]>();
            List<Object[]> watIssues = new List<Object[]>();

            List<Object[]> spaces = DatabaseControl.getMultipleRecord(new String[] { "DISTINCT OrderID" }, DatabaseControl.spaceTable, "ParkID=@value0 Order BY OrderID", new Object[] { parkId });
            Object[] period = DatabaseControl.getSingleRecord(new String[] { "DueDate" }, DatabaseControl.periodTable, "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId });
            foreach (Object[] space in spaces) {
                Object[] tenant = DatabaseControl.getSingleRecord(new String[] { "Tenant" }, DatabaseControl.spaceTable,
                    "ParkID=@value0 AND OrderID=@value1 AND MoveOutDate IS NULL", new Object[] { parkId, space[0] });
                List<Object[]> records = DatabaseControl.getMultipleRecord(DatabaseControl.meterReadsColumns, DatabaseControl.meterReadsTable, 
                    "ParkID=@value0 AND OrderID=@value1 AND DueDate<=@value2 ORDER BY DueDate DESC", new Object[] { parkId, space[0], period[0] });
                if (records.Count < 5) continue;
                Object[] recent0 = records[0];
                Object[] recent1  = records[1];
                Object[] past0 = records[1];
                Object[] past1 = records[4];
                int month = ((DateTime)recent0[9]).Date.Month;
                bool hasOneYear = false;
                for (int i = 1; i < 25; i++) {
                    int pastMonth = ((DateTime)records[i][9]).Date.Month;
                    if (pastMonth == month && records.Count > i + 2) {
                        past0 = records[i - 1];
                        past1 = records[i + 2];
                        hasOneYear = true;
                        break;
                    }
                }

                int currDays = (int)((DateTime)recent0[3] - (DateTime)recent1[3]).TotalDays;
                int pastDays = (int)((DateTime)past0[3] - (DateTime)past1[3]).TotalDays;

                double currGasUsage = (int)recent0[4] - (int)recent1[4];
                double currEleUsage = (int)recent0[5] - (int)recent1[5];
                double currWatUsage = (int)recent0[6] - (int)recent1[6];

                double pastGasUsage = (int)past0[4] - (int)past1[4];
                double pastEleUsage = (int)past0[5] - (int)past1[5];
                double pastWatUsage = (int)past0[6] - (int)past1[6];

                double comparisonValue = 0.5;
                if (hasOneYear) comparisonValue = 0.25;

                double currGasAvg = currGasUsage / currDays;
                double currEleAvg = currEleUsage / currDays;
                double currWatAvg = currWatUsage / currDays;

                double pastGasAvg = pastGasUsage / pastDays;
                double pastEleAvg = pastEleUsage / pastDays;
                double pastWatAvg = pastWatUsage / pastDays;

                if (currGasUsage >= 0 && Math.Abs((currGasAvg - pastGasAvg) / pastGasAvg) > comparisonValue) {
                    gasIssues.Add(new Object[] { space[0], tenant[0], recent0[2], recent0[3], recent1[4], recent0[4], Math.Round(pastGasAvg, 3), Math.Round(currGasAvg, 3) });
                }
                if (currEleUsage >= 0 && Math.Abs((currEleAvg - pastEleAvg) / pastEleAvg) > comparisonValue) {
                    eleIssues.Add(new Object[] { space[0], tenant[0], recent0[2], recent0[3], recent1[5], recent0[5], Math.Round(pastEleAvg, 3), Math.Round(currEleAvg, 3) });
                }
                if (currWatUsage >= 0 && Math.Abs((currWatAvg - pastWatAvg) / pastWatAvg) > comparisonValue) {
                    watIssues.Add(new Object[] { space[0], tenant[0], recent0[2], recent0[3], recent1[6], recent0[6], Math.Round(pastWatAvg, 3), Math.Round(currWatAvg, 3) });
                }
            }
            PdfControl.readIssues(parkInfo, (DateTime)period[0], gasIssues, eleIssues, watIssues);
        }

    }
}
