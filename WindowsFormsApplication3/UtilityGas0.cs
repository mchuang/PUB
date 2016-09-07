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
    public partial class UtilityGas0 : Form
    {

        private int currUtilityRateID;
        public char utility = 'G';
        decimal[][] allowance;
        decimal medicalAllowance;
        public List<CommonTools.CustomerCharge> custCharges;
        public List<CommonTools.Tier> tiers;
        public List<CommonTools.Surcharge> surcharges;

        public UtilityGas0()
        {
            InitializeComponent();
        }

        private void UtilityGas_Load(object sender, EventArgs e)
        {
            DatabaseControl.populateComboBox(ref utilNameList, DatabaseControl.utilCompanyTable, "CompanyName", "UtilityCompanyID",
                "IsGas=@value0", new Object[] { true });
            DatabaseControl.populateComboBox(ref tierSetId, DatabaseControl.tierTable, "TierSetName", "TierSetID");

            serviceType.Items.Add("All Services");
            serviceType.Items.Add("Cooking Only");
            serviceType.Items.Add("Space Heating Only");
            serviceType.Items.Add("Minimum Usage Charge");
            usageSurcharge.Items.Add(new CommonTools.Item(1, "Flat Credit/Charge"));
            usageSurcharge.Items.Add(new CommonTools.Item(2, "By Usage"));
            usageSurcharge.Items.Add(new CommonTools.Item(3, "By Days"));
            usageSurcharge.Items.Add(new CommonTools.Item(4, "By Percentage"));

            resetData();
        }

        private void resetData()
        {
            custCharges = new List<CommonTools.CustomerCharge>();
            tiers = new List<CommonTools.Tier>();
            surcharges = new List<CommonTools.Surcharge>();

            statusCustCharge.Items.Clear();
            statusCustCharge.Items.Add("");
            descSurcharge.Items.Clear();
            descSurcharge.Items.Add("");
            statusSurcharge.Items.Clear();
            statusSurcharge.Items.Add("");
            statusSurcharge.Items.Add("All");
            statusSurcharge.Items.Add("Regular");
            tierStatus.Items.Clear();
            tierStatus.Items.Add("");
            tierStatus.Items.Add("Regular");
            tiers.Add(new CommonTools.Tier('G', "Regular"));

            allowance = new decimal[3][];
            medicalAllowance = 0.00000M;
            for (int i = 0; i < 3; i++)
            {
                allowance[i] = new decimal[] { 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M };
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            UtilityGas1 next = new UtilityGas1(allowance, medicalAllowance);
            next.ShowDialog();
            if (next.committed)
            {
                this.allowance = next.allowanceRates;
                this.medicalAllowance = next.medicalAllowance;
            }
        }

        private void fillUtilityInfo(Object[] values)
        {
            String[] fields = new String[] { "UtilityRateID", "EffectiveDate", "Method", "Unit" };
            Object[] rateIdInfo = DatabaseControl.getSingleRecord(fields, DatabaseControl.utilRateTable, "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND DIRTY=0 ORDER BY EffectiveDate DESC", new Object[]{values[0], 'G'});
            effDate.Value = (DateTime)rateIdInfo[1];
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            if (!(tierSetId.SelectedItem is CommonTools.Item)) { MessageBox.Show("Please select a tier set."); return; }
            statusCheck();
            int utilCompanyId, utilRateId;
            Object[] values = { utilNameList.Text,  false, true, false};
            if (utilNameList.SelectedItem is CommonTools.Item) {
                utilCompanyId = ((CommonTools.Item)utilNameList.SelectedItem).Value;
                //String condition = "UtilityCompanyID=" + utilCompanyId;
                //DatabaseControl.executeUpdateQuery(DatabaseControl.utilCompanyTable, DatabaseControl.utilCompanyColumns, values, condition);
            }
            else {
                utilCompanyId = DatabaseControl.executeInsertQuery(DatabaseControl.utilCompanyTable, DatabaseControl.utilCompanyColumns, values);
            }
            DatabaseControl.executeUpdateQuery(DatabaseControl.utilRateTable, new String[] { "Dirty" }, new Object[] { 1 }, "UtilityCompanyID=" + utilCompanyId + " AND EffectiveDate='" + effDate.Text + "' AND UtilityServiceType='G'");
            utilRateId = DatabaseControl.executeInsertQuery(DatabaseControl.utilRateTable, DatabaseControl.utilRateColumns,
                new Object[] { utilCompanyId, 'G', effDate.Text, method.Text, measure.Text, ((CommonTools.Item)tierSetId.SelectedItem).Value, 0 });
            exportCustomerCharges(utilRateId);
            exportSurcharges(utilRateId);
            exportTiers(utilRateId);
            exportAllowanceRates(utilRateId);
            this.Close();
        }

        private void utilNameList_SelectedIndexChanged(object sender, EventArgs e) {
            CommonTools.Item item = (CommonTools.Item)utilNameList.SelectedItem;

            fillUtilityInfo(DatabaseControl.getSingleRecord(new String[] { "*" },
                DatabaseControl.utilCompanyTable, "UtilityCompanyID=" + item.Value));
            DatabaseControl.populateComboBox(ref prevEffDates, DatabaseControl.utilRateTable, "EffectiveDate", "UtilityRateID",
                "Dirty=@value0 and UtilityCompanyID=@value1 and utilityservicetype = 'G' ORDER BY EffectiveDate DESC", new Object[] { 0, item.Value });
            prevEffDates.SelectedIndex = 0;
        }

        private void exportCustomerCharges(int utilRateId) {
            foreach (CommonTools.CustomerCharge custCharge in custCharges) {
                Object[] values = { utilRateId, custCharge.status, custCharge.service, custCharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, values);
            }
        }

        private void exportAllowanceRates(int utilRateId) {
            char serviceType, climateZone;
            for (int i = 0; i < allowance.Length; i++) {
                serviceType = (char)(i+1+48);
                for (int j = 0; j < allowance[i].Length; j++) {
                    climateZone = (char)(j + 48);
                    Object[] values = { utilRateId, serviceType, climateZone, allowance[i][j] };
                    DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, values);
                }
            }
            DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns,
                new Object[] { utilRateId, 'M', 'M', medicalAllowance });
        }

        public void exportSurcharges(int utilRateId) {
            foreach (CommonTools.Surcharge surcharge in surcharges) {
                Object[] values = { utilRateId, surcharge.info, surcharge.status, surcharge.charge, surcharge.usage, surcharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
            }
        }

        private void exportTiers(int utilRateId) {
            foreach (CommonTools.Tier tierSet in tiers) {
                Object[] values = { utilRateId, tierSet.chargeType, tierSet.status, tierSet.getTier(1), tierSet.getTier(2), tierSet.getTier(3), tierSet.getTier(4), tierSet.getTier(5) };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, values);
            }
        }

        private void importCustomerCharges(int utilRateId) {
            custCharges = new List<CommonTools.CustomerCharge>();
            String condition = "UtilityRateId=@value0";
            String[] fields = { "Status", "Service", "Rate" };
            List<Object[]> items = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilBasicRatesTable, condition, new Object[] { utilRateId });
            foreach (Object[] item in items) {
                custCharges.Add(new CommonTools.CustomerCharge(item[0].ToString(), Convert.ToChar(item[1]), (decimal)item[2]));
            }

            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (!tierStatus.Items.Contains(item.status)) { tierStatus.Items.Add(item.status); }
                if (!statusCustCharge.Items.Contains(item.status)) { statusCustCharge.Items.Add(item.status); }
            }
        }

        private void importAllowanceRates(int utilRateId) {
            char serviceType, climateZone;
            String condition = "UtilityRateID=@value0 AND ServiceType=@value1 AND ClimateZone=@value2";
            this.allowance = new decimal[3][];

            for (int i = 0; i < 3; i++) {
                this.allowance[i] = new decimal[10];
                serviceType = (char)(i + 1 + 48);
                for (int j = 0; j < 10; j++) {
                    climateZone = (char)(j + 48);
                    Object[] values = { utilRateId, serviceType, climateZone };
                    Object[] records = DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, values);
                    if (records == null) continue;
                    this.allowance[i][j] = (decimal)records[0];
                }
            }
            this.medicalAllowance = (decimal)DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition,
                new Object[] { utilRateId, 'M', 'M'} )[0];
        }

        private void importSurcharges(int utilRateId) {
            surcharges = new List<CommonTools.Surcharge>();
            String[] fields = { "Description", "RateType", "ChargeType", "Usage", "Rate" };
            String condition = "UtilityRateID=@value0";
            List<Object[]> surchargeItems = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilSurchargeTable, condition, new Object[] { utilRateId });
            foreach (Object[] item in surchargeItems) {
                CommonTools.Surcharge temp = new CommonTools.Surcharge(item[0].ToString(), item[1].ToString(), Convert.ToChar(item[2]), (int)item[3], Convert.ToDecimal(item[4]));
                surcharges.Add(temp);
                descSurcharge.Items.Add(item[0].ToString());
                if (!statusSurcharge.Items.Contains(item[1].ToString())) { statusSurcharge.Items.Add(item[1].ToString()); }
                if (!statusCustCharge.Items.Contains(item[1].ToString()) && item[1].ToString() != "All") { statusCustCharge.Items.Add(item[1].ToString()); }
                if (!tierStatus.Items.Contains(item[1].ToString()) && item[1].ToString() != "All") { tierStatus.Items.Add(item[1].ToString()); }
            }
        }

        private void importTiers(int utilRateId) {
            tiers = new List<CommonTools.Tier>();
            String[] fields = { "RateType", "Rate1", "Rate2", "Rate3", "Rate4", "Rate5" };
            String condition = "UtilityRateID=@value0";
            List<Object[]> tierSets = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilTierRatesTable, condition, new Object[] { utilRateId });
            foreach (Object[] set in tierSets) {
                CommonTools.Tier temp = new CommonTools.Tier('G', set[0].ToString());
                for (int i = 1; i <= 5; i++) { temp.setTier(i, (decimal)set[i]); }
                tiers.Add(temp);
                if (!tierStatus.Items.Contains(set[0].ToString())) { tierStatus.Items.Add(set[0].ToString()); }
                if (!statusSurcharge.Items.Contains(set[0].ToString())) { statusSurcharge.Items.Add(set[0].ToString()); }
                if (!statusCustCharge.Items.Contains(set[0].ToString())) { statusCustCharge.Items.Add(set[0].ToString()); }
            }  
        }

        private void tierStatus_SelectedIndexChanged(object sender, EventArgs e) {
            displayTier(tierStatus.Text);
        }

        private void displayCustCharge(String status, char service = ' ') {
            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (item.status == status && (service == ' ' || item.service == service)) {
                    statusCustCharge.Text = item.status;
                    if (item.service == '1') {
                        serviceType.Text = "All Services";
                    } else if (item.service == '2') {
                        serviceType.Text = "Cooking Only";
                    } else if (item.service == '3') {
                        serviceType.Text = "Space Heating Only";
                    } else if (item.service == '0') {
                        serviceType.Text = "Minimum Usage Charge";
                    }
                    custChargeRate.Text = item.rate.ToString();
                    return;
                }
            }
            statusCustCharge.Text = status;
            serviceType.Text = "";
            custChargeRate.Text = "0.0000";
        }

        private void displaySurcharge(CommonTools.Surcharge item) {
            descSurcharge.Text = item.info;
            usageSurcharge.Text = CommonTools.Item.getString(ref usageSurcharge, item.usage);
            statusSurcharge.Text = item.status;
            rateSurcharge.Text = item.rate.ToString();
        }

        private void displayTier(String description) {
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(new CommonTools.Tier('G', description))) {
                    tierRatesInput.GetControlFromPosition(0, 0).Text = description;
                    for (int i = 1; i <= 5; i++) {
                        tierRatesInput.GetControlFromPosition(i, 0).Text = set.getTier(i).ToString();
                    }
                    return;
                }
            }
            for (int i = 1; i <= 5; i++) {
                tierRatesInput.GetControlFromPosition(i, 0).Text = "0.00000";
            }
        }

        private void serviceType_SelectedIndexChanged(object sender, EventArgs e) {
            char service = mapServiceType(serviceType.Text);
            displayCustCharge(statusCustCharge.Text, service);
        }

        private void statusCustCharge_SelectedIndexChanged(object sender, EventArgs e) {
            displayCustCharge(statusCustCharge.Text);
        }

        private void descSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (CommonTools.Surcharge item in surcharges) {
                if (item.info == descSurcharge.Text) { displaySurcharge(item); return; };
            }
            statusSurcharge.Text = "";
            usageSurcharge.Text = "";
            rateSurcharge.Text = "";
        }

        private void statusSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (CommonTools.Surcharge item in surcharges) {
                if (item.info == descSurcharge.Text && item.status == statusSurcharge.Text) { displaySurcharge(item); return; };
            }
            usageSurcharge.Text = "";
            rateSurcharge.Text = "";
        }

        private void saveSurBtn_Click(object sender, EventArgs e) {
            if (descSurcharge.Text == "") { MessageBox.Show("Please enter a label for surcharge."); return; }
            if (statusSurcharge.Text == "") { MessageBox.Show("Please enter a status for surcharge."); return; }
            if (usageSurcharge.Text == "") { MessageBox.Show("Please select a usage for surcharge."); return; }
            CommonTools.Surcharge surcharge = new CommonTools.Surcharge(descSurcharge.Text, statusSurcharge.Text, 'G', ((CommonTools.Item)usageSurcharge.SelectedItem).Value, Convert.ToDecimal(rateSurcharge.Text));
            surcharges.RemoveAll(item => item.Equals(surcharge));
            surcharges.Add(surcharge);
            if (!descSurcharge.Items.Contains(descSurcharge.Text)) { descSurcharge.Items.Add(descSurcharge.Text); }
            statusBox();
        }

        private void saveCustBtn_Click(object sender, EventArgs e) {
            if (statusCustCharge.Text == "" || !serviceType.Items.Contains(serviceType.Text)) { 
                MessageBox.Show("Please enter a label for customer charge."); return; 
            }

            char service = mapServiceType(serviceType.Text);
            CommonTools.CustomerCharge custCharge = new CommonTools.CustomerCharge(statusCustCharge.Text, service, Convert.ToDecimal(custChargeRate.Text));
            custCharges.RemoveAll(item => item.Equals(custCharge));
            custCharges.Add(custCharge);
            statusBox();
        }

        private void saveTierBtn_Click(object sender, EventArgs e) {
            if (tierStatus.Text == "") { MessageBox.Show("Please enter a label for tier set."); return; }
            CommonTools.Tier tierSet = new CommonTools.Tier('G', tierStatus.Text);
            for (int i = 1; i <= 5; i++) {
                try { tierSet.setTier(i, Convert.ToDecimal(tierRatesInput.GetControlFromPosition(i, 0).Text)); } catch { MessageBox.Show("Please enter valid tier rate."); return; }
            }
            tiers.RemoveAll(set => set.Equals(tierSet));
            tiers.Add(tierSet);
            statusBox();
        }

        private char mapServiceType(String service) {
            char type = ' ';
            if (serviceType.Text == "All Services") {
                type = '1';
            } else if (serviceType.Text == "Cooking Only") {
                type = '2';
            } else if (serviceType.Text == "Space Heating Only") {
                type = '3';
            } else if (serviceType.Text == "Minimum Usage Charge") {
                type = '0';
            }
            return type;
        }

        private void removeCustBtn_Click(object sender, EventArgs e) {
            char service = mapServiceType(serviceType.Text);
            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (item.Equals(new CommonTools.CustomerCharge(statusCustCharge.Text, service, 0.0M))) {
                    custCharges.Remove(item);
                    statusCustCharge.Items.Remove(statusCustCharge.Text);
                    statusBox();
                    statusCustCharge.Text = "";
                    serviceType.Text = "";
                    custChargeRate.Text = "0.00000";
                    break;
                }
            }
        }

        private void removeSurBtn_Click(object sender, EventArgs e) {
            foreach (CommonTools.Surcharge set in surcharges) {
                if (set.info == descSurcharge.Text && set.status == statusSurcharge.Text) {
                    surcharges.Remove(set);
                    descSurcharge.Items.Remove(set.info);
                    statusSurcharge.Items.Remove(set.status);
                    statusBox();
                    descSurcharge.Text = "";
                    statusSurcharge.Text = "";
                    usageSurcharge.Text = "";
                    rateSurcharge.Text = "";
                    break;
                }
            }
        }

        private void removeTierBtn_Click(object sender, EventArgs e) {
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(new CommonTools.Tier('G', tierStatus.Text))) {
                    tiers.Remove(set);
                    tierStatus.Items.Remove(tierStatus.Text);
                    tierRatesInput.GetControlFromPosition(0, 0).Text = "";
                    displayTier(tierStatus.Text);
                    ((ComboBox)tierRatesInput.GetControlFromPosition(0, 0)).Items.Remove(set.status);
                    statusBox();
                    break;
                }
            }
        }

        private void statusBox() {
            List<String> statuses = new List<String>();
            foreach (CommonTools.Tier tier in tiers) if (!statuses.Contains(tier.status)) statuses.Add(tier.status);
            foreach (CommonTools.Surcharge surcharge in surcharges) if (!surcharge.status.Equals("All") && !statuses.Contains(surcharge.status)) statuses.Add(surcharge.status);
            foreach (CommonTools.CustomerCharge custCharge in custCharges) if (!statuses.Contains(custCharge.status)) statuses.Add(custCharge.status);

            foreach (String status in statuses) {
                if (!tierStatus.Items.Contains(status)) tierStatus.Items.Add(status);
                if (!statusSurcharge.Items.Contains(status)) statusSurcharge.Items.Add(status);
                if (!statusCustCharge.Items.Contains(status)) statusCustCharge.Items.Add(status);
            }
        }

        private void statusCheck() {
            List<String> statuses = new List<String>();
            foreach (CommonTools.Tier tier in tiers) if (!statuses.Contains(tier.status)) statuses.Add(tier.status);
            foreach (CommonTools.Surcharge surcharge in surcharges) if (!surcharge.status.Equals("All") && !statuses.Contains(surcharge.status)) statuses.Add(surcharge.status);
            foreach (CommonTools.CustomerCharge custCharge in custCharges) if (!statuses.Contains(custCharge.status)) statuses.Add(custCharge.status);

            foreach (String status in statuses) {
                //if (!tiers.Contains(new CommonTools.Tier('D', status))) tiers.Add(new CommonTools.Tier('D', status));
                if (!tiers.Contains(new CommonTools.Tier('G', status))) tiers.Add(new CommonTools.Tier('G', status));
                if (!custCharges.Contains(new CommonTools.CustomerCharge(status, '1', 0.0M))) custCharges.Add(new CommonTools.CustomerCharge(status, '1', 0.0M));
                if (!custCharges.Contains(new CommonTools.CustomerCharge(status, '2', 0.0M))) custCharges.Add(new CommonTools.CustomerCharge(status, '2', 0.0M));
                if (!custCharges.Contains(new CommonTools.CustomerCharge(status, '3', 0.0M))) custCharges.Add(new CommonTools.CustomerCharge(status, '3', 0.0M));
                if (!custCharges.Contains(new CommonTools.CustomerCharge(status, '0', 0.0M))) custCharges.Add(new CommonTools.CustomerCharge(status, '0', 0.0M));
            }
        }

        private void effDate_ValueChanged(object sender, EventArgs e) {
            if (!(utilNameList.SelectedItem is CommonTools.Item)) return;
            else {
                int companyId = ((CommonTools.Item)utilNameList.SelectedItem).Value;
                String[] fields = new String[] { "UtilityRateID", "EffectiveDate", "Method", "Unit", "TierSetID" };
                Object[] rateIdInfo = DatabaseControl.getSingleRecord(fields, DatabaseControl.utilRateTable, "UtilityCompanyID=@value0 AND EffectiveDate=@value1 AND DIRTY=0 AND UtilityServiceType='G'", new Object[] { companyId, effDate.Value.Date });
                if (rateIdInfo == null) { return; }
                effDate.Value = (DateTime)rateIdInfo[1];
                method.Text = rateIdInfo[2].ToString();
                measure.Text = rateIdInfo[3].ToString();
                tierSetId.Text = CommonTools.Item.getString(ref tierSetId, (int)rateIdInfo[4]);

                displayRateToolStripMenuItem.Visible = true;
                descSurcharge.Items.Clear();
                statusSurcharge.Items.Clear();
                tierStatus.Items.Clear();
                statusCustCharge.Items.Clear();
                statusCustCharge.Items.Add("");
                descSurcharge.Items.Add("");
                statusSurcharge.Items.Add("");
                statusSurcharge.Items.Add("All");
                tierStatus.Items.Add("");
                descSurcharge.Refresh();

                int utilRateId = (int)rateIdInfo[0];
                currUtilityRateID = (int)rateIdInfo[0];
                importCustomerCharges(utilRateId);
                importSurcharges(utilRateId);
                importTiers(utilRateId);
                importAllowanceRates(utilRateId);
                statusBox();
            }
        }

        private void displayRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewUtilityRateForm vf = new ViewUtilityRateForm(this, currUtilityRateID, ref custCharges, ref tiers, ref surcharges);
            vf.Show();
        }

        private void usageSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (CommonTools.Surcharge item in surcharges) {
                if (item.info == descSurcharge.Text && item.status == statusSurcharge.Text && 
                    item.usage == ((CommonTools.Item)usageSurcharge.SelectedItem).Value) { displaySurcharge(item); return; };
            }
            rateSurcharge.Text = "";
        }
    }
}
