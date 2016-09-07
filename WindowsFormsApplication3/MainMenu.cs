using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace PUB
{
    public partial class MainMenu : Form
    {
        int security = -1;
        int userId = -1;
        public MainMenu()
        {
            InitializeComponent();
        }

        public MainMenu(int userId, int security) {
            InitializeComponent();
            switch (security) {
                case 0: 
                    utilityToolStripMenuItem.Visible = true; 
                    tierToolStripMenuItem.Visible = true; 
                    addUserToolStripMenuItem.Visible = true;
                    disableUserToolStripMenuItem.Visible = true;
                    break;
                case 1: 
                    utilityToolStripMenuItem.Visible = false; 
                    tierToolStripMenuItem.Visible = false; 
                    addUserToolStripMenuItem.Visible = false;
                    disableUserToolStripMenuItem.Visible = false;
                    break;
            }
            this.security = security;
            this.userId = userId;
        }

        private void newUtility_Click(object sender, EventArgs e)
        {
            this.Hide();
            UtilityGas0 newUtility = new UtilityGas0();
            newUtility.ShowDialog();
            this.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
        }

        private void ownerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerInfo newOwner = new OwnerInfo();
            newOwner.ShowDialog();
            this.Show();
        }

        private void parkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ParkInfo newPark = new ParkInfo();
            newPark.ShowDialog();
            this.Show();
        }

        private void parkSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ParkSpaceInfo newForm = new ParkSpaceInfo();
            newForm.ShowDialog();
            this.Show();
        }

        private void tenantToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TenantInfo newForm = new TenantInfo();
            newForm.ShowDialog();
            this.Show();
        }

        private void gasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            UtilityGas0 newForm = new UtilityGas0();
            newForm.Show();
            //newForm.ShowDialog();
            //this.Show();
        }

        private void electricityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide();
            UtilityElectricity0 newForm = new UtilityElectricity0();
            //newForm.Visible = true;
            newForm.Show();
            //newForm.Visible = true;
            //newForm.ShowDialog();
            //this.Show();
            
        }

        private void waterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            UtilityWater newForm = new UtilityWater();
            newForm.ShowDialog();
            this.Show();
        }

        private void meterReadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonTools.createBatchCSV();
        }

        private void createBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TenantBill bill = new TenantBill();
            bill.ShowDialog();
        }

        private void tierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TierInfo tier = new TierInfo();
            tier.ShowDialog();
        }

        private void importMeterReadsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ImportMeterReadFile import = new ImportMeterReadFile();
            import.ShowDialog();
            this.Show();
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewUserForm nu = new NewUserForm();
            nu.Show();
        }

        private void resetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetPasswordForm ru;
            switch (security) {
                case 0: ru = new ResetPasswordForm(); ru.Show(); break;
                case 1: ru = new ResetPasswordForm(userId, security); ru.Show(); break;
            }
        }

        private void disableUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableUserForm du = new DisableUserForm();
            du.Show();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void dataMigrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataMigration dm = new DataMigration();
            dm.Show();
        }
        
        private void exportCollectionsFileToolStripMenuItem_Click(object sender, EventArgs e) {
            List<Object[]> parkId = DatabaseControl.getMultipleRecord(new String[] { "ParkID" }, DatabaseControl.parkTable, "1=1");
            foreach (object[] item in parkId) {
                int id = (int)item[0];
                Object[] meterReadDates = DatabaseControl.getSingleRecord(new String[] { "StartDate", "MeterReadDate" }, DatabaseControl.meterReadsTable,
                        "ParkID=@value0 ORDER BY MeterReadDate DESC", new Object[] { id });
                if (meterReadDates == null) continue;
                List<Object[]> spaces = DatabaseControl.getMultipleRecord(new String[] { "ParkSpaceID" }, DatabaseControl.spaceTable,
                        "ParkID=@value0 AND (MoveOutDate IS NULL OR MoveOutDate > @value1) AND MoveInDate < @value2", new Object[] { id, (DateTime)meterReadDates[0], (DateTime)meterReadDates[1] });
                List<Object[]> rows = new List<Object[]>();
                foreach (Object[] space in spaces) {
                    Object[] startend = new Object[] { meterReadDates[0], meterReadDates[1] };
                    Dictionary<String, Object> moveDates = DatabaseControl.getSingleRecordDict(new String[] { "MoveInDate", "MoveOutDate" },
                        DatabaseControl.spaceTable, "ParkSpaceID=@value0", new Object[] { (int)space[0] });
                    if (moveDates["MoveOutDate"] != DBNull.Value) startend[1] = moveDates["MoveOutDate"];
                    if ((DateTime)moveDates["MoveInDate"] > (DateTime)meterReadDates[0]) startend[0] = moveDates["MoveInDate"];
                    String[] tenantFields = new String[] { "SpaceID", "Tenant", "GasStatus", "GasType", "EleStatus", "EleType", "WatStatus", "MedStatus", "DontBillForGas", "DontBillForEle", "DontBillForWat"};
                    Dictionary<String, Object> tenantInfo = DatabaseControl.getSingleRecordDict(tenantFields, DatabaseControl.spaceTable,
                        "ParkID=@value0 AND ParkSpaceID=@value1", new Object[] { id, space[0] });
                    String[] billFields = new String[] { "GasPreviousRead", "GasCurrentRead", "GasUsage", "GasBill", "ElePreviousRead", "EleCurrentRead", "EleUsage", "EleBill", "WatPreviousRead", "WatCurrentRead", "WatUsage", "WatBill" };
                    Dictionary<String, Object> billInfo = DatabaseControl.getSingleRecordDict(billFields, DatabaseControl.spaceBillTable,
                        "ParkSpaceID=@value0 AND StartDate=@value1 AND EndDate=@value2", new Object[] { space[0], startend[0], startend[1] });
                    if (billInfo == null) { continue; }
                    List<Object[]> summary = getSummaryOfCharges(id, (int)space[0], (DateTime)startend[0], (DateTime)startend[1]);
                    List<Object[]> temporary = getTempCharges(id, (DateTime)startend[0], (DateTime)startend[1]);
                    List<Object[]> charges = new List<Object[]>();
                    foreach (Object[] charge in summary) charges.Add(item);
                    foreach (Object[] charge in temporary) charges.Add(item);

                    rows.Add(new Object[] { tenantInfo, startend[1], startend[0], charges, billInfo });
                }

                //CommonTools.createCollectionsCSV(id, rows);

            }
            MessageBox.Show("Collections files generated!");
        }

        public List<Object[]> getTempCharges(int parkId, DateTime start, DateTime end) {
            List<Object[]> temp = DatabaseControl.getMultipleRecord(new String[] { "Description", "Charge" }, DatabaseControl.tempChargeTable,
                "ParkID=@value0 AND DateAssigned>=@value1 AND DateAssigned<@value2", new Object[] { parkId, start, end });
            return temp;
        }

        public List<Object[]> getSummaryOfCharges(int parkId, int parkSpaceId, DateTime start, DateTime end) {
            String table;
            table = DatabaseControl.spaceChargeTable + " JOIN " + DatabaseControl.optionsTable + " ON ParkSpaceCharge.Description=ChargeItem.ChargeItemDescription";
            List<Object[]> items = DatabaseControl.getMultipleRecord(new String[] { "Description", "ChargeItemValue" }, table,
                "ParkSpaceID=@value0 AND StartDate=@value1 AND EndDate=@value2 ORDER BY ChargeItemID ASC", new Object[] { parkSpaceId, start, end });
            if (items.Count == 0) {
                table = DatabaseControl.parkOptionsTable + " JOIN " + DatabaseControl.optionsTable + " ON ChargeItem.ChargeItemID=ParkChargeItem.ChargeItemID";
                items = DatabaseControl.getMultipleRecord(new String[] { "ChargeItemDescription", "ChargeItemValue" }, table, "ParkId=@value0 ORDER BY ChargeItem.ChargeItemID ASC", new Object[] { parkId });
            }
            return items;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e) {
            PdfControl.testFont();
        }

        private void viewUtilityRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void utilityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e) {

        }

        private void meterReadInputToolStripMenuItem_Click(object sender, EventArgs e) {
            ManualMeterReadInput man = new ManualMeterReadInput();
            man.ShowDialog();
        }

        private void testToolStripMenuItem_Click_1(object sender, EventArgs e) {
            CommonTools.CSVtoDB("Export\\Collections156.csv");
        }

    }
}
