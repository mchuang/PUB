using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUB
{
    public partial class TenantBill : Form
    {
        int parkId, parkSpaceId, meterReadId;
        public TenantBill()
        {
            InitializeComponent();
        }

        private void TenantBill_Load(object sender, EventArgs e)
        {
            tenantList.Enabled = false;
            readDate.Enabled = false;
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkNumber", "ParkID");
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tenantList.Enabled = true;
            readDate.Enabled = false;
            tenantList.Items.Clear();
            int parkId = ((CommonTools.Item)parkList.SelectedItem).Value;
            DatabaseControl.populateComboBox(ref tenantList, DatabaseControl.spaceTable, "Tenant", "ParkSpaceID", "ParkID=@value0 AND MoveOutDate IS NULL", new Object[] { parkId });
        }

        private void tenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            readDate.Enabled = true;
            readDate.Items.Clear();
            parkId = ((CommonTools.Item)parkList.SelectedItem).Value;
            parkSpaceId = ((CommonTools.Item)tenantList.SelectedItem).Value;
            TenantBilling bill = new TenantBilling(parkId, parkSpaceId);
            displayBill(bill.generateBill());
            String spaceId = DatabaseControl.getSingleRecord(new String[] { "SpaceID" }, DatabaseControl.spaceTable,
                "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0].ToString();
            DatabaseControl.populateComboBox(ref readDate, DatabaseControl.meterReadsTable, "MeterReadDate", "MeterReadID",
                "ParkID=@value0 AND SpaceID=@value1", new Object[] { parkId, spaceId });
        }

        private void readDate_SelectedIndexChanged(object sender, EventArgs e) {
            meterReadId = ((CommonTools.Item)readDate.SelectedItem).Value;
            Object[] read = DatabaseControl.getSingleRecord(DatabaseControl.meterReadsColumns, DatabaseControl.meterReadsTable, "MeterReadID=@value0", 
                new Object[] { meterReadId });
            TenantBilling bill = new TenantBilling(parkId, parkSpaceId, read);
            displayBill(bill.generateBill());
        }

        private void billBtn_Click(object sender, EventArgs e) {
            if (parkId != -1 && parkSpaceId != -1) {
                TenantBilling bill = new TenantBilling(parkId, parkSpaceId);
                Object[] info = bill.generateBill();
                info[8] = getSummaryOfCharges();
                PdfControl.createBillPdf(info);
            }
        }

        private void displayBill(Object[] info) {
            tenantInfo.Text = "";
            clerkInfo.Text = "";
            usageInfo.Text = "";
            readInfo.Text = "";
            gasInfo.Text = "";
            watInfo.Text = "";
            eleInfo.Text = "";
            displayTenantInfo((Object[])info[2]);
            displayClerkInfo((Object[])info[3]);
            displayUsageInfo((Object[])((Object[])info[4])[0]);
            displayReadInfo((Object[])info[4]);
            displayGasInfo((Object[])info[5]);
            displayEleInfo((Object[])info[6]);
            displayWatInfo((Object[])info[7]);
            decimal utilities = (decimal)((Object[])info[5])[7] + (decimal)((Object[])info[6])[9] + (decimal)((Object[])info[7])[6];
            displaySummaryOfCharges(parkId, utilities);
        }

        private void displayTenantInfo(Object[] info) {
            tenantInfo.Text = String.Format("{0}\tSpace # {2}\n{1}\n{3}, {4} {5}", info);
        }

        private void displayClerkInfo(Object[] info) {
            clerkInfo.Text = String.Format("{0}\n{1}\n{2}, {3} {4}", info);
        }

        private void displayUsageInfo(Object[] info) {
            usageInfo.Text = String.Format("Days:{0}\tUsage:{1}\tLast Year:{2}\tDays:{3}\tUsage:{4}\tLast Year:{5}\tDays:{6}\tUsage:{7}\tLast Year:{8}", info);
        }

        private void displayReadInfo(Object[] info) {
            foreach (Object[] item in info) {
                if (item.Length > 8) continue;
                readInfo.Text += String.Format("{0}\t\t{1}\t\t{2}\t\t{3}\t\t{4}\t\t{5}\t\t{6}\t\t{7}\n", item);
            }
        }

        private void displayGasInfo(Object[] info) {
            String gas = "";
            gas += String.Format("Gas\t\t\t\t\t{0}\n", ((decimal)info[0]).ToString("G29"));

            Object[] custCharge = (Object[])info[1];
            if ((decimal)custCharge[1] != 0.0M) gas += String.Format("\n{0, -27} | {1, 8}\n", custCharge[0], ((decimal)custCharge[1]).ToString("C2"));

            if ((decimal)info[3] != 0.0M) {
                List<Object[]> gen = (List<Object[]>)info[2];
                foreach (Object[] item in gen) {
                    if ((decimal)item[1] != 0.0M) gas += String.Format("\n  {0, -25} | {1, 8}", item[0], ((decimal)item[1]).ToString("C2"));
                }
                gas += String.Format("\nGeneration Total:\t\t{0}\n", ((decimal)info[3]).ToString("C2"));
            }

            List<Object[]> surcharge = (List<Object[]>)info[4];
            foreach (Object[] item in surcharge) {
                if ((decimal)item[1] != 0.0M) gas += String.Format("\n  {0, -25} | {1, 8}", item[0], ((decimal)item[1]).ToString("C2"));
            }

            List<Object[]> tax = (List<Object[]>)info[5];
            foreach (Object[] item in tax) {
                if ((decimal)item[1] != 0.0M) gas += String.Format("\n  {0, -25} | {1, 8}", item[0], ((decimal)item[1]).ToString("C2"));
            }

            gas += String.Format("\n{0}", info[6]);
            gas += String.Format("\nTotal: \t\t{0, 10}", ((decimal)info[7]).ToString("C2"));

            gasInfo.Text = gas;
        }

        private void displayEleInfo(Object[] info) {
            String ele = "";
            ele += String.Format("Ele\t\t\t\t\t{0}", ((decimal)info[0]).ToString("G29"));

            Object[] custCharge = (Object[])info[1];
            if ((decimal)custCharge[1] != 0.0M) ele += String.Format("\n{0, -27} {1, 8}\n", custCharge[0], ((decimal)custCharge[1]).ToString("G29"));

            if ((decimal)info[3] != 0.0M) {
                List<Object[]> gen = (List<Object[]>)info[2];
                foreach (Object[] item in gen) {
                    if ((decimal)item[1] != 0.0M) ele += String.Format("\n  {0, -25} | {1, 8}", item[0], ((decimal)item[1]).ToString("C2"));
                }
                ele += String.Format("\nDelivery Total:\t{0, 10}\n", ((decimal)info[3]).ToString("C2"));
            }

            if ((decimal)info[5] != 0.0M) {
                List<Object[]> gen = (List<Object[]>)info[4];
                foreach (Object[] item in gen) {
                    if ((decimal)item[1] != 0.0M) ele += String.Format("\n  {0, -25} | {1, 8}", item[0], ((decimal)item[1]).ToString("C2"));
                }
                ele += String.Format("\nGeneration Total:\t{0, 10}\n", ((decimal)info[5]).ToString("C2"));
            }

            List<Object[]> surcharge = (List<Object[]>)info[6];
            foreach (Object[] item in surcharge) {
                if ((decimal)item[1] != 0.0M) ele += String.Format("\n  {0, -25} | {1, 8}", item[0], ((decimal)item[1]).ToString("C2"));
            }

            List<Object[]> tax = (List<Object[]>)info[7];
            foreach (Object[] item in tax) {
                if ((decimal)item[1] != 0.0M) ele += String.Format("\n  {0, -25} | {1, 8}", item[0], ((decimal)item[1]).ToString("C2"));
            }

            ele += String.Format("\n{0}", info[8]);
            ele += String.Format("\nTotal: \t\t{0, 8}", ((decimal)info[9]).ToString("C2"));

            eleInfo.Text = ele;
        }

        private void displayWatInfo(Object[] info) {
            String wat = "";
            wat += String.Format("Wat\t\t\t\t\t{0}\n", ((decimal)info[0]).ToString("G29"));

            Object[] custCharge = (Object[])info[1];
            if ((decimal)custCharge[1] != 0.0M) wat += String.Format("{0, -27} | {1, 8}\n\n", custCharge[0], ((decimal)custCharge[1]).ToString("C2"));

            if ((decimal)info[3] != 0.0M) {
                List<Object[]> gen = (List<Object[]>)info[2];
                foreach (Object[] item in gen) {
                    if ((decimal)item[1] != 0.0M) wat += String.Format("  {0, -25} | {1, 8}\n", item[0], ((decimal)item[1]).ToString("C2"));
                }
                wat += String.Format("Generation Total:\t{0, 10}\n", ((decimal)info[3]).ToString("C2"));
            }

            List<Object[]> surcharge = (List<Object[]>)info[4];
            foreach (Object[] item in surcharge) {
                if ((decimal)item[1] != 0.0M) wat += String.Format("\n  {0, -25} | {1, 8}\n", item[0], ((decimal)item[1]).ToString("C2"));
            }

            List<Object[]> tax = (List<Object[]>)info[5];
            foreach (Object[] item in tax) {
                if ((decimal)item[1] != 0.0M) wat += String.Format("\n  {0, -25} | {1, 8}", item[0], ((decimal)item[1]).ToString("C2"));
            }

            wat += String.Format("\nTotal: \t\t{0, 10}", ((decimal)info[6]).ToString("C2"));

            watInfo.Text = wat;
        }

        private void displaySummaryOfCharges(int parkId, decimal utilTotal) {
            summaryOfCharges.Columns.Clear();
            DataTable data = new DataTable();
            String table = DatabaseControl.parkOptionsTable + " JOIN " + DatabaseControl.optionsTable + " ON ChargeItem.ChargeItemID=ParkChargeItem.ChargeItemID";
            DatabaseControl.populateDataTable(ref data, "ChargeItemDescription, ChargeItemValue", table, "ParkId=@value0", new Object[] { parkId });

            summaryOfCharges.Columns.Add("optionName", "Option");
            summaryOfCharges.Columns["optionName"].Width = 180;
            summaryOfCharges.Columns.Add("optionCharge", "Charge");
            summaryOfCharges.Columns["optionCharge"].Width = 80;

            for (int i = 0; i < data.Rows.Count; i++ ) {
                summaryOfCharges.Rows.Add(1);
                summaryOfCharges.Rows[i].Cells["optionName"].Value = data.Rows[i]["ChargeItemDescription"];
                summaryOfCharges.Rows[i].Cells["optionCharge"].Value = ((decimal)data.Rows[i]["ChargeItemValue"]).ToString("N2");
            }
            summaryOfCharges.Rows.Add(1);
            summaryOfCharges.Rows[summaryOfCharges.RowCount - 2].Cells["optionName"].Value = "Utilities";
            summaryOfCharges.Rows[summaryOfCharges.RowCount - 2].Cells["optionCharge"].Value = utilTotal.ToString("N2");
        }

        private Object[] getSummaryOfCharges() {
            Object[] summary = new Object[summaryOfCharges.RowCount - 1];
            for (int i = 0; i < summaryOfCharges.RowCount - 1; i++) {
                DataGridViewRow row = summaryOfCharges.Rows[i];
                summary[i] = new Object[] { row.Cells["optionName"].Value, Convert.ToDecimal(row.Cells["optionCharge"].Value) };
            }
            return summary;
        }

        private void historyBtn_Click(object sender, EventArgs e) {
            if (parkList.SelectedIndex != -1) { parkId = ((CommonTools.Item)parkList.SelectedItem).Value; }
            if (tenantList.SelectedIndex != -1) { parkSpaceId = ((CommonTools.Item)tenantList.SelectedItem).Value; }
            if (parkId != -1 && parkSpaceId != -1) {
                ReadHistory history = new ReadHistory(parkId, parkSpaceId);
                history.ShowDialog();
            }
        }

        public class TenantBilling {
            int parkId, parkSpaceId, parkNumber;
            String spaceId;
            Object[] meterRead;

            private static int minTier = 1;
            private static int maxTier = 5;

            public TenantBilling(int parkId, int parkSpaceId) {
                this.parkId = parkId;
                this.parkSpaceId = parkSpaceId;
                this.parkNumber = (int)DatabaseControl.getSingleRecord(new String[] { "ParkNumber" }, DatabaseControl.parkTable, "ParkID=@value0",
                new Object[] { parkId })[0];
                this.spaceId = DatabaseControl.getSingleRecord(new String[] { "SpaceID" }, DatabaseControl.spaceTable, "ParkID=@value0 AND ParkSpaceID=@value1",
                new Object[] { parkId, parkSpaceId })[0].ToString();

                ArrayList reads = DatabaseControl.getMultipleRecord(DatabaseControl.meterReadsColumns, DatabaseControl.meterReadsTable,
                    "ParkID=@value0 AND SpaceID=@value1 ORDER BY MeterReadID DESC", new Object[] { parkId, spaceId });
                meterRead = (Object[])reads[0];
            }

            public TenantBilling(int parkId, int parkSpaceId, Object[] currRead) {
                this.parkId = parkId;
                this.parkSpaceId = parkSpaceId;
                meterRead = currRead;
            }

            public Object[] generateBill() {
                Object[] tenant = getTenantAddr();
                Object[] park = getParkAddr();
                Object[] ele = calculateEleTotalCost();
                Object[] gas = calculateGasTotalCost();
                Object[] wat = calculateWatTotalCost();
                Object[] usage = getReadData(0.0M, (decimal)ele[ele.Length - 1], 0.0M);
                //PdfControl.createBillPdf(parkNumber, spaceId, tenant, park, usage, gas, ele, wat);
                return new Object[] { parkNumber, spaceId, tenant, park, usage, gas, ele, wat, null };
            }

            public Object[] getTenantAddr() {
                String[] fields = { "Tenant", "Address1", "Address2", "City", "State", "Zip" };
                String condition = "ParkSpaceID=@value0";
                return DatabaseControl.getSingleRecord(fields, DatabaseControl.spaceTable, condition, new Object[] { this.parkSpaceId });
            }

            public Object[] getParkAddr() {
                String[] fields = { "ParkName", "Address", "City", "State", "ZipCode" };
                String condition = "ParkID=@value0";
                return DatabaseControl.getSingleRecord(fields, DatabaseControl.parkTable, condition, new Object[] { this.parkId });
            }

            public Object[] getReadData(decimal gasTotal, decimal eleTotal, decimal watTotal) {
                Object[] usages = new Object[] { null, (int)meterRead[3] - (int)meterRead[6], meterRead[9],
                                             null, (int)meterRead[4] - (int)meterRead[7], meterRead[10],
                                             null, (int)meterRead[5] - (int)meterRead[8], meterRead[11] };
                Object[] gas = new Object[] { "GAS", ((DateTime)meterRead[2]).ToString("MM/dd"), ((DateTime)meterRead[2]).ToString("MM/dd"), meterRead[6], meterRead[3], null,
                (int)meterRead[3] - (int)meterRead[6], gasTotal.ToString("C2") };
                Object[] ele = new Object[] { "ELE", ((DateTime)meterRead[2]).ToString("MM/dd"), ((DateTime)meterRead[2]).ToString("MM/dd"), meterRead[7], meterRead[4], null,
                (int)meterRead[4] - (int)meterRead[7], eleTotal.ToString("C2") };
                Object[] wat = new Object[] { "WAT", ((DateTime)meterRead[2]).ToString("MM/dd"), ((DateTime)meterRead[2]).ToString("MM/dd"), meterRead[8], meterRead[5], null,
                (int)meterRead[5] - (int)meterRead[8], watTotal.ToString("C2") };
                return new Object[] { usages, gas, ele, wat };
            }

            public Object[] calculateGasTotalCost() {
                int utilRateId, tierSetId, numDays, numMed;
                char season, zone, serviceType;
                String rateType;
                decimal usage, baseline, medAllowance;

                int gasCompanyId = (int)DatabaseControl.getSingleRecord(new String[] { "GasCompanyID" }, DatabaseControl.parkTable,
                    "ParkID=@value0", new Object[] { parkId })[0];

                Object[] utilCompany = DatabaseControl.getSingleRecord(new String[] { "UtilityRateID", "HasWinter", "WinterStartDate", "WinterEndDate", "TierSetID" }, DatabaseControl.utilRateTable,
                    "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { gasCompanyId });

                utilRateId = (int)utilCompany[0];
                tierSetId = (int)utilCompany[4];
                if ((bool)utilCompany[1]) {
                    DateTime winterStart = (DateTime)utilCompany[1];
                    DateTime winterEnd = (DateTime)utilCompany[2];
                    if (winterStart < DateTime.Today && winterEnd > DateTime.Today) { season = 'W'; } else { season = 'S'; }
                } else {
                    season = ' ';
                }
                zone = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "GasZone" }, DatabaseControl.parkTable,
                    "ParkID=@value0", new Object[] { parkId })[0]);
                rateType = DatabaseControl.getSingleRecord(new String[] { "GasStatus" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0].ToString();
                if (rateType == "") { rateType = "Regular"; }
                serviceType = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "GasType" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0]);
                numMed = (int)DatabaseControl.getSingleRecord(new String[] { "MedStatus" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];

                usage = (int)meterRead[3] - (int)meterRead[6];
                numDays = (int)(((DateTime)meterRead[2]).Date - ((DateTime)meterRead[2]).Date).TotalDays;
                baseline = getBaselineAllowance(utilRateId, season, serviceType, zone);
                medAllowance = getBaselineAllowance(utilRateId, 'M', 'M', 'M');

                Object[] custCharge = calculateBasicCost(utilRateId, serviceType, rateType, numDays);

                List<Object[]> generation = calculateTierCost(utilRateId, 'G', rateType, ' ', tierSetId, usage, baseline * numDays + numMed * medAllowance);
                decimal generationTotal = 0.0M;
                foreach (Object[] item in generation) {
                    generationTotal += (decimal)item[1];
                }

                List<Object[]> surcharge = calculateSurcharges(utilRateId, "All", numDays, usage);
                surcharge.AddRange(calculateSurcharges(utilRateId, rateType, numDays, usage));
                decimal surchargeTotal = 0.0M;
                foreach (Object[] item in surcharge) {
                    surchargeTotal += (decimal)item[1];
                }

                List<Object[]> tax = calculateTaxCost('G', new String[] { "LocalTax", "CountyTax", "StateTax", "RegTax" }, usage);
                decimal taxTotal = 0.0M;
                foreach (Object[] item in tax) {
                    taxTotal += (decimal)item[1];
                }

                String careRates;
                if (numMed > 0) { careRates = "CARE RATES APPLIED"; } else { careRates = ""; }
                Object[] pdfGas = new Object[] { (baseline * numDays + numMed * medAllowance), custCharge, generation, generationTotal, 
                surcharge, tax, careRates, (decimal)custCharge[1] + generationTotal + surchargeTotal + taxTotal };

                return pdfGas;
            }

            public Object[] calculateEleTotalCost() {
                int utilRateId, tierSetId, numDays, usage, numMed; char serviceType, season, zone; String rateType; decimal baseline, medAllowance;

                int eleCompanyId = (int)DatabaseControl.getSingleRecord(new String[] { "ElectricityCompanyID" }, DatabaseControl.parkTable,
                    "ParkID=@value0", new Object[] { parkId })[0];

                Object[] utilCompany = DatabaseControl.getSingleRecord(new String[] { "UtilityRateID", "HasWinter", "WinterStartDate", "WinterEndDate", "TierSetID" }, DatabaseControl.utilRateTable,
                    "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { eleCompanyId });

                utilRateId = (int)utilCompany[0];
                tierSetId = (int)utilCompany[4];
                if ((bool)utilCompany[1]) {
                    DateTime winterStart = (DateTime)utilCompany[2];
                    DateTime winterEnd = (DateTime)utilCompany[3];
                    if (winterStart < DateTime.Today && winterEnd > DateTime.Today) { season = 'W'; } else { season = 'S'; }
                } else {
                    season = ' ';
                }

                zone = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "EleZone" }, DatabaseControl.parkTable,
                    "ParkID=@value0", new Object[] { parkId })[0]);
                rateType = DatabaseControl.getSingleRecord(new String[] { "EleStatus" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0].ToString();
                if (rateType == "") { rateType = "Regular"; }
                serviceType = Convert.ToChar(DatabaseControl.getSingleRecord(new String[] { "EleType" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0]);
                numMed = (int)DatabaseControl.getSingleRecord(new String[] { "MedStatus" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];

                usage = (int)meterRead[4] - (int)meterRead[7];
                numDays = (int)(((DateTime)meterRead[2]).Date - ((DateTime)meterRead[2]).Date).TotalDays + 1;
                baseline = getBaselineAllowance(utilRateId, season, serviceType, zone);
                medAllowance = getBaselineAllowance(utilRateId, 'M', 'M', 'M');

                Console.WriteLine("Electric  \r\nUsage:{0}  Days:{1}  Baseline:{2}  MedAllowance:{3}", usage, numDays,
                    (baseline * numDays + numMed * medAllowance).ToString("G29"), numMed);
                Console.WriteLine("Status: {0}   Zone: {1}   Season: {2}  Service: {3}", rateType, zone, season, serviceType);
                Console.WriteLine("PrevDate: {0}   CurrDate: {1}   PrevRead: {2}   CurrRead: {3}\r\n", ((DateTime)meterRead[2]).Date.ToShortDateString(), ((DateTime)meterRead[2]).Date.ToShortDateString(),
                    meterRead[4], meterRead[7]);

                Object[] custCharge = calculateBasicCost(utilRateId, serviceType, rateType, numDays);

                List<Object[]> delivery = calculateTierCost(utilRateId, 'D', rateType, season, tierSetId, usage, baseline * numDays + numMed * medAllowance);
                decimal deliveryTotal = 0.0M;
                foreach (Object[] item in delivery) {
                    deliveryTotal += (decimal)item[1];
                }

                List<Object[]> generation = calculateTierCost(utilRateId, 'G', rateType, season, tierSetId, usage, baseline * numDays + numMed * medAllowance);
                decimal generationTotal = 0.0M;
                foreach (Object[] item in generation) {
                    generationTotal += (decimal)item[1];
                }

                List<Object[]> surcharge = calculateSurcharges(utilRateId, "All", numDays, usage);
                surcharge.AddRange(calculateSurcharges(utilRateId, rateType, numDays, usage));
                decimal surchargeTotal = 0.0M;
                foreach (Object[] item in surcharge) {
                    surchargeTotal += (decimal)item[1];
                }

                List<Object[]> tax = calculateTaxCost('E', new String[] { "LocalTax", "CountyTax", "StateTax", "RegTax" }, usage);
                decimal taxTotal = 0.0M;
                foreach (Object[] item in tax) {
                    taxTotal += (decimal)item[1];
                }

                String careRates;
                if (numMed > 0) { careRates = "CARE RATES APPLIED"; } else { careRates = ""; }
                Object[] pdfEle = new Object[] { (baseline * numDays + numMed * medAllowance), custCharge, delivery, deliveryTotal, generation, generationTotal, 
                surcharge, tax, careRates, (decimal)custCharge[1] + deliveryTotal + generationTotal + surchargeTotal + taxTotal };
                return pdfEle;
            }

            public Object[] calculateWatTotalCost() {
                int utilRateId, tierSetId, numDays, usage, numMed; char serviceType, season, zone; String rateType; decimal baseline, medAllowance;

                int watCompanyId = (int)DatabaseControl.getSingleRecord(new String[] { "WaterCompanyID" }, DatabaseControl.parkTable,
                    "ParkID=@value0", new Object[] { parkId })[0];

                Object[] utilCompany = DatabaseControl.getSingleRecord(new String[] { "UtilityRateID", "TierSetID" }, DatabaseControl.utilRateTable,
                    "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { watCompanyId });

                utilRateId = (int)utilCompany[0];
                tierSetId = (int)utilCompany[1];
                season = ' ';

                rateType = DatabaseControl.getSingleRecord(new String[] { "WatStatus" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0].ToString();
                if (rateType == "") { rateType = "Regular"; }
                serviceType = 'W';
                numMed = (int)DatabaseControl.getSingleRecord(new String[] { "MedStatus" }, DatabaseControl.spaceTable,
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];

                usage = (int)meterRead[5] - (int)meterRead[8];
                numDays = (int)(((DateTime)meterRead[2]).Date - ((DateTime)meterRead[2]).Date).TotalDays + 1;
                baseline = getBaselineAllowance(utilRateId, 'S', serviceType, ' ');

                Object[] custCharge = calculateBasicCost(utilRateId, serviceType, rateType, numDays);

                List<Object[]> generation = calculateTierCost(utilRateId, 'G', rateType, season, tierSetId, usage, baseline * numDays);
                decimal generationTotal = 0.0M;
                foreach (Object[] item in generation) {
                    generationTotal += (decimal)item[1];
                }

                List<Object[]> surcharge = calculateSurcharges(utilRateId, "All", numDays, usage);
                surcharge.AddRange(calculateSurcharges(utilRateId, rateType, numDays, usage));
                decimal surchargeTotal = 0.0M;
                foreach (Object[] item in surcharge) {
                    surchargeTotal += (decimal)item[1];
                }

                List<Object[]> tax = calculateTaxCost('E', new String[] { "LocalTax", "CountyTax", "StateTax", "RegTax" }, usage);
                decimal taxTotal = 0.0M;
                foreach (Object[] item in tax) {
                    taxTotal += (decimal)item[1];
                }

                Object[] pdfWat = new Object[] { baseline * numDays, custCharge, generation, generationTotal, surcharge, tax, (decimal)custCharge[1] + generationTotal + surchargeTotal + taxTotal };
                return pdfWat;
            }

            public Object[] calculateBasicCost(int utilRateId, char serviceType, String rateType, int numDays) {
                decimal rate = getBasicRate(utilRateId, rateType, serviceType);
                return new Object[] { String.Format("Cust Charge: {0}x{1}", numDays, rate.ToString("G29")), numDays * rate };
            }

            public List<Object[]> calculateTierCost(int utilRateId, char chargeType, String rateType, char season, int tierSetId, decimal usage, decimal baseline) {
                List<Object[]> obj = new List<Object[]>();
                //decimal totalCost = 0.0M;
                for (int i = minTier; i <= maxTier; i++) {
                    Object[] tierPercentages = getTierPercentage(tierSetId);
                    decimal tierLimit = baseline * (decimal)tierPercentages[i];
                    Object[] rate = getTierRate(utilRateId, chargeType, rateType, season);
                    if (usage > tierLimit && (decimal)tierPercentages[i] != -1.00M) {
                        //totalCost += ((decimal)rate[i-1] * tierLimit);
                        usage -= tierLimit;
                        obj.Add(new Object[] { String.Format("Tier{0}: {1}x{2}", i, tierLimit.ToString("G29"), ((decimal)rate[i - 1]).ToString("G29")), tierLimit * (decimal)rate[i - 1] });
                    } else {
                        //totalCost += ((decimal)rate[i-1] * usage);
                        obj.Add(new Object[] { String.Format("Tier{0}: {1}x{2}", i, usage.ToString("G29"), ((decimal)rate[i - 1]).ToString("G29")), usage * (decimal)rate[i - 1] });
                        break;
                    }
                }
                return obj;
            }

            public List<Object[]> calculateTaxCost(char utilType, String[] tax, decimal usage) {
                List<Object[]> obj = new List<Object[]>();
                //decimal total = 0.0M;
                foreach (String taxType in tax) {
                    decimal rate = getTaxRate(parkId, utilType, taxType);
                    obj.Add(new Object[] { String.Format(taxType + " {0}x{1}", usage.ToString("G29"), rate.ToString("G29")), (usage * rate) });
                    //total += usage * rate;
                }
                return obj;
            }

            public List<Object[]> calculateSurcharges(int utilRateId, String rateType, int days, decimal quantity) {
                List<Object[]> obj = new List<Object[]>();
                ArrayList surcharges = getSurcharge(utilRateId, rateType);

                foreach (Object[] surcharge in surcharges) {
                    decimal rate = (decimal)surcharge[2];
                    int usage = (int)surcharge[1];
                    String description = surcharge[0].ToString();

                    switch (usage) {
                        case 1:
                            obj.Add(new Object[] { description, rate });
                            break;
                        case 2:
                            obj.Add(new Object[] { String.Format(description + " {0}x{1}", quantity.ToString("G29"), rate.ToString("G29")), quantity * rate });
                            break;
                        case 3:
                            obj.Add(new Object[] { String.Format(description + " {0}x{1}", quantity.ToString("G29"), rate.ToString("G29")), days * rate });
                            break;
                    }
                }
                return obj;
            }

            private decimal getBaselineAllowance(int utilRateId, char season, char serviceType, char zone) {
                String condition = "UtilityRateID=@value0 AND Season=@value1 AND ServiceType=@value2 AND ClimateZone=@value3";
                Object[] values = { utilRateId, season, serviceType, zone };
                return (decimal)DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, values)[0];
            }

            private Object[] getTierPercentage(int tierSetId) {
                return DatabaseControl.getSingleRecord(new String[] { "TierSetName", "Tier1", "Tier2", "Tier3", "Tier4", "Tier5" }, DatabaseControl.tierTable, "TierSetID=@value0",
                    new Object[] { tierSetId });
            }

            private decimal getBasicRate(int utilRateId, String status, char service) {
                String condition = "UtilityRateID=@value0 AND Status=@value1 AND Service=@value2";
                Object[] values = { utilRateId, status, service };
                return (decimal)DatabaseControl.getSingleRecord(new String[] { "Rate" }, DatabaseControl.utilBasicRatesTable, condition, values)[0];
            }

            private String getBasicDescription(int utilRateId, char serviceType, String rateType) {
                String condition = "UtilityRateID=@value0 AND ServiceType=@value1 AND RateType=@value2";
                Object[] values = { utilRateId, serviceType, rateType };
                return DatabaseControl.getSingleRecord(new String[] { "Description" }, DatabaseControl.utilBasicRatesTable, condition, values)[0].ToString();
            }

            private ArrayList getSurcharge(int utilRateId, String rateType) {
                String condition = "UtilityRateID=@value0 AND RateType=@value1";
                Object[] values = { utilRateId, rateType };
                return DatabaseControl.getMultipleRecord(new String[] { "Description", "Usage", "Rate" }, DatabaseControl.utilSurchargeTable, condition, values);
            }

            private Object[] getTierRate(int utilRateId, char chargeType, String rateType, char season) {
                String condition = "UtilityRateID=@value0 AND ChargeType=@value1 AND RateType=@value2 AND Season=@value3";
                Object[] values = { utilRateId, chargeType, rateType, season };
                return DatabaseControl.getSingleRecord(new String[] { "Rate1", "Rate2", "Rate3", "Rate4", "Rate5" },
                    DatabaseControl.utilTierRatesTable, condition, values);
            }

            private decimal getTaxRate(int parkId, char utilType, String tax) {
                return Convert.ToDecimal(DatabaseControl.getSingleRecord(new String[] { tax }, DatabaseControl.taxTable,
                    "ParkID=@value0 AND UtilityType=@value1 ORDER BY TaxRateID DESC", new Object[] { parkId, utilType })[0]);
            }
        }
    }
}
