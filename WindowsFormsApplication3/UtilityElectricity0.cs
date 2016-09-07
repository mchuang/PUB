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
    public partial class UtilityElectricity0 : Form {
        public char utility = 'E';
        private int currUtilityRateId;
        public decimal[][] allowanceRates;
        public List<CommonTools.CustomerCharge> custCharges;
        public List<CommonTools.Tier> tiers;
        public List<CommonTools.Surcharge> surcharges;
        public decimal medicalAllowance;

        public UtilityElectricity0() {
            InitializeComponent();
        }

        private void UtilityElectricity_Load(object sender, EventArgs e) {
            DatabaseControl.populateComboBox(ref utilNameList, DatabaseControl.utilCompanyTable, "CompanyName", "UtilityCompanyID",
                "IsElectricity=@value0", new Object[] { true });
            DatabaseControl.populateComboBox(ref tierSetId, DatabaseControl.tierTable, "TierSetName", "TierSetID");

            serviceCustCharge.Items.Add("All Services");
            serviceCustCharge.Items.Add("All Electric");
            serviceCustCharge.Items.Add("Minimum Usage Charge");
            usageSurcharge.Items.Add(new CommonTools.Item(1, "Flat Credit/Charge"));
            usageSurcharge.Items.Add(new CommonTools.Item(2, "By Usage"));
            usageSurcharge.Items.Add(new CommonTools.Item(3, "By Days"));
            usageSurcharge.Items.Add(new CommonTools.Item(4, "By Percentage"));
            chargeSurcharge.Items.Add('G');
            chargeSurcharge.Items.Add('D');
            chargeSurcharge.Items.Add(' ');
            resetData();
        }

        private void resetData() {
            this.allowanceRates = new decimal[2][];
            for (int i = 0; i < 2; i++) {
                allowanceRates[i] = new decimal[] { 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M };
            }
            this.medicalAllowance = 0.0000M;

            custCharges = new List<CommonTools.CustomerCharge>();
            tiers = new List<CommonTools.Tier>();
            surcharges = new List<CommonTools.Surcharge>();
            statusCustCharge.Items.Clear();
            descSurcharge.Items.Clear();
            statusSurcharge.Items.Clear();
            generationTierStatus.Items.Clear();
            deliveryTierStatus.Items.Clear();

            descSurcharge.Items.Add("");
            statusSurcharge.Items.Add("");
            statusSurcharge.Items.Add("All");
            generationTierStatus.Items.Add("");
            deliveryTierStatus.Items.Add("");
            statusCustCharge.Items.Add("Regular");
            generationTierStatus.Items.Add("Regular");
            deliveryTierStatus.Items.Add("Regular");
            tiers.Add(new CommonTools.Tier('G', "Regular"));
            tiers.Add(new CommonTools.Tier('D', "Regular"));

            descSurcharge.Text = "";
            statusSurcharge.Text = "";
            usageSurcharge.Text = "";
            rateSurcharge.Text = "0.0000";
            statusCustCharge.Text = "";
            serviceCustCharge.Text = "";
            custChargeRate.Text = "0.0000";

            deliveryTierStatus.Text = "";
            generationTierStatus.Text = "";
            for (int i = 1; i <= 5; i++) {
                generationRatesInput.GetControlFromPosition(i, 0).Text = "0.0000";
            }
            for (int i = 1; i <= 5; i++) {
                deliveryRatesInput.GetControlFromPosition(i, 0).Text = "0.0000";
            }
        }

        private void allowanceBtn_Click(object sender, EventArgs e) {
            UtilityElectricity1 next = new UtilityElectricity1(this.allowanceRates, medicalAllowance);
            next.ShowDialog();
            if (next.committed) {
                this.allowanceRates = next.allowanceRates;
                this.medicalAllowance = next.medicalRate;
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.Close();
        }
        
        private void saveBtn_Click(object sender, EventArgs e) {
            if (!(tierSetId.SelectedItem is CommonTools.Item)) { MessageBox.Show("Please select a tier set."); return; }
            statusCheck();
            int companyId = getElecUtilityId();
            newUtilityRate(companyId);
            this.Close();
        }

        private int getElecUtilityId() {
            int companyId;
            if (utilNameList.SelectedItem is CommonTools.Item) { companyId = ((CommonTools.Item)utilNameList.SelectedItem).Value; }
            else { companyId = DatabaseControl.executeInsertQuery(DatabaseControl.utilCompanyTable, DatabaseControl.utilCompanyColumns, new Object[] { utilNameList.Text, true, false, false }); }
            return companyId;
        }

        private void newUtilityRate(int utilCompanyId) {
            int utilRateId;
            DatabaseControl.executeUpdateQuery(DatabaseControl.utilRateTable, new String[] { "Dirty" }, new Object[] { 1 }, "UtilityCompanyID=" + utilCompanyId + " AND EffectiveDate='" + effDate.Text + "' AND UtilityServiceType='E'");
            Object[] values = { utilCompanyId, utility, effDate.Text, method.Text, measure.Text, ((CommonTools.Item)tierSetId.SelectedItem).Value, 0 };
            utilRateId = DatabaseControl.executeInsertQuery(DatabaseControl.utilRateTable, DatabaseControl.utilRateColumns, values);
            exportBasicRates(utilRateId);
            exportSurcharges(utilRateId);
            exportTiers(utilRateId);
            exportAllowance(utilRateId, allowanceRates);
            
            DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns,
                new Object[] { utilRateId, 'M', 'M', medicalAllowance });
        }

        private void exportBasicRates(int utilRateId) {
            foreach (CommonTools.CustomerCharge custCharge in custCharges) {
                Object[] values = { utilRateId, custCharge.status, custCharge.service, custCharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, values);
            }
        }

        private void exportAllowance(int utilRateId, decimal[][] allowance) {
            decimal rate; char serviceType, climateZone;

            serviceType = '1';
            for (int i = 0; i < allowance[0].Length; i++) {
                rate = allowance[0][i];
                climateZone = (char)(i + 48);
                Object[] values = { utilRateId, serviceType, climateZone, rate};
                DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, values);
            }

            serviceType = '2';
            for (int i = 0; i < allowance[1].Length; i++) {
                rate = allowance[1][i];
                climateZone = (char)(i + 48);
                Object[] values = { utilRateId, serviceType, climateZone, rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, values);
            }
        }

        public void exportTiers(int utilRateId) {
            foreach (CommonTools.Tier tierSet in tiers) {
                Object[] values = { utilRateId, tierSet.chargeType, tierSet.status, tierSet.getTier(1), tierSet.getTier(2), tierSet.getTier(3), tierSet.getTier(4), tierSet.getTier(5) };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, values);
            }
        }

        public void exportSurcharges(int utilRateId) {
            foreach (CommonTools.Surcharge surcharge in surcharges) {
                Object[] values = { utilRateId, surcharge.info, surcharge.status, surcharge.charge, surcharge.usage, surcharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
            }
        }

        private void utilNameList_SelectedIndexChanged(object sender, EventArgs e) {
            CommonTools.Item item = (CommonTools.Item) utilNameList.SelectedItem;
            importUtilityInfo(item.Value);
            DatabaseControl.populateComboBox(ref prevEffDates, DatabaseControl.utilRateTable, "EffectiveDate", "UtilityRateID",
                "Dirty=@value0 and UtilityCompanyID=@value1 and UtilityServiceType='E' ORDER BY EffectiveDate DESC", new Object[] { 0, item.Value });
            prevEffDates.SelectedIndex = 0;
        }

        private void importUtilityInfo(int utilCompanyId) {
            String[] fields = new String[] { "UtilityRateID", "EffectiveDate", "Method", "Unit", "TierSetID" };
            String condition = "UtilityCompanyID=@value0 AND UtilityServiceType=@value1 AND DIRTY = 0 ORDER BY EffectiveDate DESC";
            Object[] values = DatabaseControl.getSingleRecord(fields, DatabaseControl.utilRateTable, condition, new Object[] { utilCompanyId, 'E' });
            currUtilityRateId = (int) values[0];
            
            effDate.Value = (DateTime)values[1];
        }

        private void importBasicRates(int currUtilityRateId) {
            custCharges = new List<CommonTools.CustomerCharge>();
            String condition = "UtilityRateId=@value0";
            String[] fields = { "Status", "Service", "Rate" };
            List<Object[]> items = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilBasicRatesTable, condition, new Object[] { currUtilityRateId });
            foreach (Object[] item in items) {
                custCharges.Add(new CommonTools.CustomerCharge(item[0].ToString(), Convert.ToChar(item[1]), (decimal)item[2]));
            }
        }

        private void importTiers(int utilRateId) {
            tiers = new List<CommonTools.Tier>();
            String[] fields = { "ChargeType", "RateType", "Rate1", "Rate2", "Rate3", "Rate4", "Rate5" };
            String condition = "UtilityRateID=@value0";
            List<Object[]> tierSets = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilTierRatesTable, condition, new Object[] { utilRateId });
            foreach (Object[] set in tierSets) {
                CommonTools.Tier temp = new CommonTools.Tier(Convert.ToChar(set[0]), set[1].ToString());
                for (int i = 1; i <= 5; i++) { temp.setTier(i, (decimal)set[i + 1]); }
                tiers.Add(temp);
            }
        }

        private void importSurcharges(int utilRateId) {
            surcharges = new List<CommonTools.Surcharge>();
            String[] fields = { "Description", "RateType", "ChargeType", "Usage", "Rate" };
            String condition = "UtilityRateID=@value0";
            List<Object[]> surchargeItems = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilSurchargeTable, condition, new Object[] { utilRateId });
            foreach (Object[] item in surchargeItems) {
                CommonTools.Surcharge temp = new CommonTools.Surcharge(item[0].ToString(), item[1].ToString(), Convert.ToChar(item[2]), (int)item[3], Convert.ToDecimal(item[4]));
                surcharges.Add(temp);
                if (!descSurcharge.Items.Contains(item[0].ToString())) descSurcharge.Items.Add(item[0].ToString());
            }
        }

        private void importAllowanceRates(int currUtilityRateId) {
            char serviceType, climateZone;
            String condition = "UtilityRateId=@value0 AND ServiceType=@value1 AND ClimateZone=@value2";
            this.allowanceRates = new decimal[2][];

            for (int i = 0; i < 2; i++) {
                this.allowanceRates[i] = new decimal[10];
                serviceType = (char)(i+1+48);
                for (int j = 0; j < 10; j++) {
                    climateZone = (char)(j+48);
                    Object[] values = { currUtilityRateId, serviceType, climateZone };
                    Object[] records= DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, values);
                    this.allowanceRates[i][j] = (decimal)records[0];
                }
            }
            this.medicalAllowance = (decimal)DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition,
                new Object[] {currUtilityRateId, 'M', 'M'})[0];
        }

        private void saveCustBtn_Click(object sender, EventArgs e) {
            if (statusCustCharge.Text == "" || !serviceCustCharge.Items.Contains(serviceCustCharge.Text)) { MessageBox.Show("Please enter appropriate values for customer charge."); return; }

            char service = ' ';
            if (serviceCustCharge.Text == "All Services") { service = '1'; } 
            else if (serviceCustCharge.Text == "All Electric") { service = '2'; } 
            else if (serviceCustCharge.Text == "Minimum Usage Charge") { service = '0'; }
            CommonTools.CustomerCharge custCharge = new CommonTools.CustomerCharge(statusCustCharge.Text, service, Convert.ToDecimal(custChargeRate.Text));
            custCharges.RemoveAll(item => item.Equals(custCharge));
            custCharges.Add(custCharge);
            storeremoveCC.Text = String.Format("{0}:{1} stored", statusCustCharge.Text, serviceCustCharge.Text);
            statusBox();
        }

        private void saveGenBtn_Click(object sender, EventArgs e) {
            if (generationTierStatus.Text == "") { MessageBox.Show("Please enter a status for tier set."); return; }
            CommonTools.Tier tierSet = new CommonTools.Tier('G', generationTierStatus.Text);
            for (int i = 1; i <= 5; i++) {
                try { tierSet.setTier(i, Convert.ToDecimal(generationRatesInput.GetControlFromPosition(i, 0).Text)); }
                catch { MessageBox.Show("Please enter valid tier rate."); return; }
            }
            tiers.RemoveAll(item => item.Equals(tierSet));
            tiers.Add(tierSet);
            storeremoveG.Text = generationTierStatus.Text + " stored";
            statusBox();
        }

        private void saveDelBtn_Click(object sender, EventArgs e) {
            if (deliveryTierStatus.Text == "") { MessageBox.Show("Please enter a status for tier set."); return; }
            CommonTools.Tier tierSet = new CommonTools.Tier('D', deliveryTierStatus.Text);
            for (int i = 1; i <= 5; i++) {
                try { tierSet.setTier(i, Convert.ToDecimal(deliveryRatesInput.GetControlFromPosition(i, 0).Text)); }
                catch { MessageBox.Show("Please enter valid tier rate."); return; }
            }
            tiers.RemoveAll(item => item.Equals(tierSet));
            tiers.Add(tierSet);
            storeremoveD.Text = deliveryTierStatus.Text + " stored";
            statusBox();
        }

        private void saveSurBtn_Click(object sender, EventArgs e) {
            if (descSurcharge.Text == "") { MessageBox.Show("Please enter a label for surcharge."); return; }
            if (statusSurcharge.Text == "") { MessageBox.Show("Please enter a status for surcharge."); return; } 
            if (usageSurcharge.Text == "") { MessageBox.Show("Please select a usage for surcharge."); return; }
            if (chargeSurcharge.Text == "") { chargeSurcharge.Text = " "; }
            CommonTools.Surcharge surcharge;
            try {
                surcharge = new CommonTools.Surcharge(descSurcharge.Text, statusSurcharge.Text, Convert.ToChar(chargeSurcharge.Text), ((CommonTools.Item)usageSurcharge.SelectedItem).Value, Convert.ToDecimal(rateSurcharge.Text));
            } catch {
                storeremoveS.Text = "Fail! Try again.";
                return;
            }

            surcharges.RemoveAll(item => item.Equals(surcharge));
            surcharges.Add(surcharge);
            if (!descSurcharge.Items.Contains(descSurcharge.Text)) { descSurcharge.Items.Add(descSurcharge.Text); }
            statusBox();
            storeremoveS.Text = String.Format("{0}:{1}:{2} stored", descSurcharge.Text, statusSurcharge.Text, usageSurcharge.Text);
        }

        private void removeGenBtn_Click(object sender, EventArgs e) {
            removeTier(generationRatesInput, 'G', generationTierStatus.Text);
            storeremoveG.Text = generationTierStatus.Text + " removed";
        }

        private void removeDelBtn_Click(object sender, EventArgs e) {
            removeTier(deliveryRatesInput, 'D', deliveryTierStatus.Text);
            storeremoveG.Text = deliveryTierStatus.Text + " removed";
        }

        private void removeTier(TableLayoutPanel table, char charge, String description) {
            tiers.RemoveAll(set => set.Equals(new CommonTools.Tier(charge, description)));
            table.GetControlFromPosition(0, 0).Text = "";
            for (int i = 1; i <= 5; i++) { table.GetControlFromPosition(i, 0).Text = "0.0000"; }
            ((ComboBox)table.GetControlFromPosition(0, 0)).Items.Remove(description);
            statusBox();
        }

        private void removeCustBtn_Click(object sender, EventArgs e) {
            char service = ' ';
            if (serviceCustCharge.Text == "All Services") { service = '1'; } 
            else if (serviceCustCharge.Text == "All Electric") { service = '2'; } 
            else if (serviceCustCharge.Text == "Minimum Usage Charge") { service = '0'; }
            custCharges.RemoveAll(item => item.Equals(new CommonTools.CustomerCharge(statusCustCharge.Text, service, 0.0M)));
            statusCustCharge.Items.Remove(statusCustCharge.Text);
            statusBox();
            statusCustCharge.Text = "";
            serviceCustCharge.Text = "";
            custChargeRate.Text = "0.00000";
            storeremoveCC.Text = String.Format("{0}:{1} removed", statusCustCharge.Text, serviceCustCharge.Text);
        }

        private void removeSurBtn_Click(object sender, EventArgs e) {
            removeSurcharge(descSurcharge.Text, statusSurcharge.Text, Convert.ToChar(chargeSurcharge.Text), ((CommonTools.Item)usageSurcharge.SelectedItem).Value);
            storeremoveS.Text = String.Format("{0}:{1} removed", descSurcharge.Text, statusSurcharge.Text);
        }

        private void removeSurcharge(String description, String status, char charge, int usage) {
            surcharges.RemoveAll(set => set.Equals(new CommonTools.Surcharge(description, status, charge, usage, 0M)));
            descSurcharge.Items.Remove(description);
            statusSurcharge.Items.Remove(status);
            statusBox();
            foreach (CommonTools.Surcharge item in surcharges) if (!descSurcharge.Items.Contains(item.info)) descSurcharge.Items.Add(item.info);
            descSurcharge.Text = "";
            statusSurcharge.Text = "";
            usageSurcharge.Text = "";
            chargeSurcharge.Text = "";
            rateSurcharge.Text = "0.00000";
        }

        private void displayCustCharge(String status, char service = ' ') {
            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (item.status == status && (service == ' ' || item.service == service)) {
                    statusCustCharge.Text = item.status;
                    if (item.service == '1') { 
                        serviceCustCharge.Text = "All Services";
                    } else if (item.service == '2') { 
                        serviceCustCharge.Text = "All Electric";
                    } else if (item.service == '0') {
                        serviceCustCharge.Text = "Minimum Usage Charge";
                    }
                    custChargeRate.Text = item.rate.ToString();
                    return;
                }
            }
            statusCustCharge.Text = status;
            serviceCustCharge.Text = "";
            custChargeRate.Text = "0.0000";
        }

        private void displayTier(TableLayoutPanel table, char charge, String description) {
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(new CommonTools.Tier(charge, description))) {
                    table.GetControlFromPosition(0, 0).Text = description;
                    for (int i = 1; i <= 5; i++) { table.GetControlFromPosition(i, 0).Text = set.getTier(i).ToString(); }
                    return;
                }
            }
            for (int i = 1; i <= 5; i++) { table.GetControlFromPosition(i, 0).Text = "0.0000"; }
        }

        private void displaySurcharge(CommonTools.Surcharge item) {
            descSurcharge.Text = item.info;
            usageSurcharge.Text = CommonTools.Item.getString(ref usageSurcharge, item.usage);
            statusSurcharge.Text = item.status;
            rateSurcharge.Text = item.rate.ToString();
            chargeSurcharge.Text = item.charge.ToString();
            /*
            CommonTools.Surcharge display = null;
            foreach (CommonTools.Surcharge surcharge in surcharges) {
                if (surcharge.info == description && (status == "" || surcharge.status == status)) { display = surcharge; break; }
            }
            if (display == null) {
                rateSurcharge.Text = "0.0000";
                usageSurcharge.Text = "";
                return;
            }
            descSurcharge.Text = display.info;
            statusSurcharge.Text = display.status.ToString();
            foreach (CommonTools.Item item in usageSurcharge.Items) {
                if (item.Value == display.usage) { usageSurcharge.SelectedItem = item; usageSurcharge.Text = item.Text; break; }
            }
            rateSurcharge.Text = display.rate.ToString();*/
        }

        private void generationTierSet_SelectedIndexChanged(object sender, EventArgs e) {
            displayTier(generationRatesInput, 'G', generationTierStatus.Text);
        }

        private void deliveryTierSet_SelectedIndexChanged(object sender, EventArgs e) {
            displayTier(deliveryRatesInput, 'D', deliveryTierStatus.Text);
        }

        private void surchargeList_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (CommonTools.Surcharge item in surcharges) {
                if (item.info == descSurcharge.Text) { displaySurcharge(item); return; };
            }
            statusSurcharge.Text = "";
            chargeSurcharge.Text = "";
            usageSurcharge.Text = "";
            rateSurcharge.Text = "";
        }

        private void statusSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (CommonTools.Surcharge item in surcharges) {
                if (item.info == descSurcharge.Text && item.status == statusSurcharge.Text) { displaySurcharge(item); return; };
            }
            chargeSurcharge.Text = "";
            usageSurcharge.Text = "";
            rateSurcharge.Text = "";
        }

        private void statusCustCharge_SelectedIndexChanged(object sender, EventArgs e) {
            displayCustCharge(statusCustCharge.Text);
        }

        private void serviceType_SelectedIndexChanged(object sender, EventArgs e) {
            char service = ' ';
            if (serviceCustCharge.Text == "All Services") { service = '1'; } 
            else if (serviceCustCharge.Text == "All Electric") { service = '2'; } 
            else if (serviceCustCharge.Text == "Minimum Usage Charge") { service = '0'; }
            displayCustCharge(statusCustCharge.Text, service);
        }

        private void statusCheck() {
            List<String> statuses = new List<String>();
            foreach (CommonTools.Tier tier in tiers) if (!statuses.Contains(tier.status)) statuses.Add(tier.status);
            foreach (CommonTools.Surcharge surcharge in surcharges) if (!surcharge.status.Equals("All") && !statuses.Contains(surcharge.status)) statuses.Add(surcharge.status);
            foreach (CommonTools.CustomerCharge custCharge in custCharges) if (!statuses.Contains(custCharge.status)) statuses.Add(custCharge.status);

            foreach (String status in statuses) {
                if (!tiers.Contains(new CommonTools.Tier('D', status))) tiers.Add(new CommonTools.Tier('D', status));
                if (!tiers.Contains(new CommonTools.Tier('G', status))) tiers.Add(new CommonTools.Tier('G', status));
                if (!custCharges.Contains(new CommonTools.CustomerCharge(status, '1', 0.0M))) custCharges.Add(new CommonTools.CustomerCharge(status, '1', 0.0M));
                if (!custCharges.Contains(new CommonTools.CustomerCharge(status, '2', 0.0M))) custCharges.Add(new CommonTools.CustomerCharge(status, '2', 0.0M));
                if (!custCharges.Contains(new CommonTools.CustomerCharge(status, '0', 0.0M))) custCharges.Add(new CommonTools.CustomerCharge(status, '0', 0.0M));
            }
        }

        private void statusBox() {
            List<String> statuses = new List<String>();
            foreach (CommonTools.Tier tier in tiers) if (!statuses.Contains(tier.status)) statuses.Add(tier.status);
            foreach (CommonTools.Surcharge surcharge in surcharges) if (!surcharge.status.Equals("All") && !statuses.Contains(surcharge.status)) statuses.Add(surcharge.status);
            foreach (CommonTools.CustomerCharge custCharge in custCharges) if (!statuses.Contains(custCharge.status)) statuses.Add(custCharge.status);

            foreach (String status in statuses) {
                if (!generationTierStatus.Items.Contains(status)) generationTierStatus.Items.Add(status);
                if (!deliveryTierStatus.Items.Contains(status)) deliveryTierStatus.Items.Add(status);
                if (!statusSurcharge.Items.Contains(status)) statusSurcharge.Items.Add(status);
                if (!statusCustCharge.Items.Contains(status)) statusCustCharge.Items.Add(status);
            }
        }

        private void effDate_ValueChanged(object sender, EventArgs e) {
            if (!(utilNameList.SelectedItem is CommonTools.Item)) return;
            else {
                int companyId = ((CommonTools.Item)utilNameList.SelectedItem).Value;
                String[] fields = new String[] { "UtilityRateID", "EffectiveDate", "Method", "Unit", "TierSetID" };
                String condition = "UtilityCompanyID=@value0 AND EffectiveDate=@value1 AND DIRTY  = 0 AND UtilityServiceType='E'";
                Object[] values = DatabaseControl.getSingleRecord(fields, DatabaseControl.utilRateTable, condition, new Object[] { companyId, effDate.Value.Date });
                if (values == null) { return; }
                currUtilityRateId = (int)values[0];

                method.Text = values[2].ToString();
                measure.Text = values[3].ToString();
                tierSetId.Text = CommonTools.Item.getString(ref tierSetId, (int)values[4]);

                displayRateToolStripMenuItem.Visible = true;
                descSurcharge.Items.Clear();
                statusSurcharge.Items.Clear();
                generationTierStatus.Items.Clear();
                deliveryTierStatus.Items.Clear();
                statusCustCharge.Items.Clear();
                statusCustCharge.Items.Add("");
                descSurcharge.Items.Add("");
                statusSurcharge.Items.Add("");
                statusSurcharge.Items.Add("All");
                generationTierStatus.Items.Add("");
                deliveryTierStatus.Items.Add("");
                descSurcharge.Refresh();

                importBasicRates(currUtilityRateId);
                importTiers(currUtilityRateId);
                importSurcharges(currUtilityRateId);
                importAllowanceRates(currUtilityRateId);
                statusBox();
            }
        }

        private void displayRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewUtilityRateForm vf = new ViewUtilityRateForm(this, currUtilityRateId, ref custCharges, ref tiers, ref surcharges);
            vf.Show();
        }

        private void chargeSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (CommonTools.Surcharge item in surcharges) {
                if (item.info == descSurcharge.Text && item.status == statusSurcharge.Text && 
                    item.charge.ToString() == chargeSurcharge.Text) { displaySurcharge(item); return; };
            }
            usageSurcharge.Text = "";
            rateSurcharge.Text = "";
        }

        private void usageSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (CommonTools.Surcharge item in surcharges) {
                if (item.info == descSurcharge.Text && item.status == statusSurcharge.Text && 
                    item.charge.ToString() == chargeSurcharge.Text && item.usage == ((CommonTools.Item)usageSurcharge.SelectedItem).Value) { 
                    displaySurcharge(item); return; 
                };
            }
            rateSurcharge.Text = "";
        }
    }
}
