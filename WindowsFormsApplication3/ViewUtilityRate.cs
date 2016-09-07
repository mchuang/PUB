using System;
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
    public partial class ViewUtilityRateForm : Form
    {
        private int currentUtilityRateID;
        public Form form;
        public List<CommonTools.CustomerCharge> basicRates;
        public List<CommonTools.Tier> tierRates;
        public List<CommonTools.Surcharge> surcharges;
        public ViewUtilityRateForm
            ( Form form, int currUtilityRateID, 
            ref List<CommonTools.CustomerCharge> custCharges, 
            ref List<CommonTools.Tier> tiers,
            ref List<CommonTools.Surcharge> Surcharges)
        {
            this.form = form;
            currentUtilityRateID = currUtilityRateID;
            basicRates = custCharges;
            tierRates = tiers;
            surcharges = Surcharges;
            InitializeComponent();
            showBasicRateFromObject();
            showTierRateFromObject();
            showSurchargeFromObject();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showBasicRateFromObject();
            showTierRateFromObject();
            showSurchargeFromObject();
        }
        private void showBasicRate()
        // This method displays the Uitlity Basic Rate in a dataGridView using datatable that is populated from database
        {
            String fields = "Service As [Order1], Status As [Tenant Type], Service  =  CASE " +
            "WHEN service = 0 THEN 'Minimum Rate' " +
            "WHEN service = 1 THEN 'All Service Rate' " +
            "WHEN service = 2 THEN 'Cooking Only Rate' " +
            "WHEN service = 3 THEN 'Space Heating Only Rate' " +
            "ELSE '' END , Rate  ";
            String table = DatabaseControl.utilBasicRatesTable;
            String condition = "UtilityRateID=@value0 order by Status desc, Order1 ";
            DataTable basicRatesTable = new DataTable();
            DatabaseControl.populateDataTable(ref basicRatesTable, fields, table, condition, new Object[] { currentUtilityRateID });
            basicRateDataGridView.DataSource = basicRatesTable;
            basicRateDataGridView.Columns[0].Visible = false;
            basicRateDataGridView.AllowUserToAddRows = false;
            basicRateDataGridView.ClearSelection();

            basicRateDataGridView.Rows[0].Selected = true;
        }
        private void showBasicRateFromObject()
        // This method displays the Uitlity Basic Rate in a dataGridView using datatable that is populated from objects built already
        {
            DataTable basicRatesTable = new DataTable();
            basicRateDataGridView.DataSource = basicRatesTable;
            basicRatesTable.Columns.Add("Tenant Type", typeof(string));
            basicRatesTable.Columns.Add("Service Type", typeof(string));
            basicRatesTable.Columns.Add("Rate", typeof(double));
            
            foreach (CommonTools.CustomerCharge basicRate in basicRates)
            {

                string serviceType= "";
                switch (basicRate.service)
                {
                    case '0':
                        serviceType = "Minimum Rate";
                        break;
                    case '1':
                        serviceType = "All Service Rate";
                        break;
                    case '2':
                        serviceType = "Cooking Only Rate";
                        break;
                    case '3':
                        serviceType = "Space Heater Only Rate";
                        break;
                    default:
                        break;
                }
                DataRow dr = basicRatesTable.NewRow();
                basicRatesTable.Rows.Add(new Object[] { basicRate.status, serviceType, basicRate.rate });
            }
        }
        private void showTierRateFromObject ()
        // This method displays the Uitlity Tier Rate in a dataGridView using datatable that is populated from database
        {
            DataTable tierRatesTable = new DataTable();
            tierRateDataGridView1.DataSource = tierRatesTable;
            tierRatesTable.Columns.Add("Charge Type", typeof(string));
            tierRatesTable.Columns.Add("Tenant Type", typeof(string));
            tierRatesTable.Columns.Add("Tier1", typeof(double));
            tierRatesTable.Columns.Add("Tier2", typeof(double));
            tierRatesTable.Columns.Add("Tier3", typeof(double));
            tierRatesTable.Columns.Add("Tier4", typeof(double));
            tierRatesTable.Columns.Add("Tier5", typeof(double));
           
            foreach (CommonTools.Tier tier in tierRates)
            {

                string chargeType = "";
                switch (tier.chargeType)
                {
                    case 'G':
                       chargeType = "Generation";
                        break;
                    case 'D':
                        chargeType = "Delivery";
                        break;
                    default:
                        break;
                }
                DataRow dr = tierRatesTable.NewRow();
                tierRatesTable.Rows.Add(new Object[] { chargeType, tier.status, tier.rates[0], tier.rates[1], tier.rates[2], tier.rates[3], tier.rates[4], });
            }
        }
        private void showSurchargeFromObject()
        // This method displays the Uitlity Surcharge in a dataGridView using datatable that is populated from objects build already
        {
            DataTable surchargeTable = new DataTable();
            surchargeDataGridView1.DataSource = surchargeTable;
            surchargeTable.Columns.Add("Surcharge", typeof(string));
            surchargeTable.Columns.Add("Charge Type", typeof(string));
            surchargeTable.Columns.Add("Tenant Type", typeof(string));
            surchargeTable.Columns.Add("Usage", typeof(string));
            surchargeTable.Columns.Add("Rate", typeof(double));
            
            foreach (CommonTools.Surcharge surcharge in surcharges)
            {

                string chargeType = "";
                switch (surcharge.charge)
                {
                    case 'G':
                        chargeType = "Generation";
                        break;
                    case 'D':
                        chargeType = "Delivery";
                        break;
                    default:
                        break;
                }
                string usage = "";
                switch (surcharge.usage)
                {
                    case 1:
                        usage = "Flat Credit/Charge";
                        break;
                    case 2:
                        usage = "By Usage";
                        break;
                    case 3:
                        usage = "By Days";
                        break;
                    case 4:
                        usage = "By Percentage";
                        break;
                    default:
                        break;
                }
                
                DataRow dr = surchargeTable.NewRow();
                surchargeTable.Rows.Add(new Object[] { surcharge.info, chargeType, surcharge.status, usage, surcharge.rate}) ;
            }
        }
        private void showTierRate ()
        // This method displays the Uitlity Tier Rate in a dataGridView using datatable that is populated from objects built already
        {
            String fields = " ChargeType =  CASE " +
            "WHEN ChargeType = 'G' THEN 'Generation' " +
            "WHEN ChargeType = 'D' THEN 'Delivery' " +
            "ELSE '' END, RateType As [Tenant Type], Rate1 as Tier1,Rate2 as Tier2,Rate3 as Tier3,Rate4 as Tier4,Rate5 as Tier5 ";
            String table = DatabaseControl.utilTierRatesTable;
            String condition = "UtilityRateID=@value0 order by ChargeType, RateType desc";
            DataTable tierRatesTable = new DataTable();
            DatabaseControl.populateDataTable(ref tierRatesTable, fields, table, condition, new Object[] { currentUtilityRateID });
            tierRateDataGridView1.DataSource = tierRatesTable;

            //tierRateDataGridView1.AllowUserToAddRows = false;
            tierRateDataGridView1.ClearSelection();

            basicRateDataGridView.Rows[0].Selected = true;
        }

        private void saveRates() {
            basicRates = new List<CommonTools.CustomerCharge>();
            tierRates = new List<CommonTools.Tier>();
            
            surcharges = new List<CommonTools.Surcharge>();
            foreach (DataGridViewRow row in basicRateDataGridView.Rows) {
                if (row.IsNewRow) continue;
                char serviceType= '1';
                switch (row.Cells["Service Type"].Value.ToString())
                {
                    case "Minimum Rate":
                        serviceType = '0';
                        break;
                    case "All Service Rate":
                        serviceType = '1';
                        break;
                    case "Cooking Only Rate":
                        serviceType = '2';
                        break;
                    case "Space Heater Only Rate":
                        serviceType = '3';
                        break;
                    default:
                        serviceType = 'W';
                        break;
                }
                CommonTools.CustomerCharge rate = new CommonTools.CustomerCharge(row.Cells["Tenant Type"].Value.ToString(), serviceType, Convert.ToDecimal(row.Cells["Rate"].Value));
                basicRates.Add(rate);
            }

            foreach (DataGridViewRow row in surchargeDataGridView1.Rows) {
                if (row.IsNewRow) continue;
                int usage = 1;
                char chargeType = ' ';
                switch (row.Cells["Usage"].Value.ToString())
                {
                    case "Flat Credit/Charge":
                        usage = 1;
                        break;
                    case "By Usage":
                        usage = 2;
                        break;
                    case "By Days":
                        usage = 3;
                        break;
                    case "By Percentage":
                        usage = 4 ;
                        break;
                    default:
                        break;
                }
                switch (row.Cells["Charge Type"].Value.ToString()) {
                    case "Generation":
                        chargeType = 'G';
                        break;
                    case "Delivery":
                        chargeType = 'D';
                        break;
                    default:
                        break;
                }
                CommonTools.Surcharge surcharge = new CommonTools.Surcharge(row.Cells["Surcharge"].Value.ToString(), row.Cells["Tenant Type"].Value.ToString(),
                    chargeType, usage, Convert.ToDecimal(row.Cells["Rate"].Value));
                surcharges.Add(surcharge);
            }

            foreach (DataGridViewRow row in tierRateDataGridView1.Rows) {
                if (row.IsNewRow) continue;
                char chargeType = ' ';
                switch (row.Cells["Charge Type"].Value.ToString()) {
                    case "Generation":
                        chargeType = 'G';
                        break;
                    case "Delivery":
                        chargeType = 'D';
                        break;
                    default:
                        break;
                }
                CommonTools.Tier tier = new CommonTools.Tier(chargeType, row.Cells["Tenant Type"].Value.ToString());
                tier.setTier(1, Convert.ToDecimal(row.Cells["Tier1"].Value));
                tier.setTier(2, Convert.ToDecimal(row.Cells["Tier2"].Value));
                tier.setTier(3, Convert.ToDecimal(row.Cells["Tier3"].Value));
                tier.setTier(4, Convert.ToDecimal(row.Cells["Tier4"].Value));
                tier.setTier(5, Convert.ToDecimal(row.Cells["Tier5"].Value));
                tierRates.Add(tier);
            }

            switch (this.form.Text) {
                case "UtilityElectricity":
                    ((UtilityElectricity0)this.form).custCharges = basicRates;
                    ((UtilityElectricity0)this.form).tiers = tierRates;
                    ((UtilityElectricity0)this.form).surcharges = surcharges;
                    break;
                case "UtilityGas":
                    ((UtilityGas0)this.form).custCharges = basicRates;
                    ((UtilityGas0)this.form).tiers = tierRates;
                    ((UtilityGas0)this.form).surcharges = surcharges;
                    break;
                case "UtilityWater":
                    ((UtilityWater)this.form).custCharges = basicRates;
                    ((UtilityWater)this.form).tiers = tierRates;
                    ((UtilityWater)this.form).surcharges = surcharges;
                    break;
            }
        }

        private void button1_Click_1(object sender, EventArgs e) {
            saveRates();
        }
        
    }
}
