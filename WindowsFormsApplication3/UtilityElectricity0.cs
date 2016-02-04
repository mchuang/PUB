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
        public decimal[][] flatRates, summerAllowanceRates, winterAllowanceRates;
        public List<CommonTools.CustomerCharge> custCharges;
        public List<CommonTools.Tier> tiers;
        public List<CommonTools.Surcharge> surcharges;
        public char season;
        public decimal medicalAllowance;

        public UtilityElectricity0() {
            InitializeComponent();
        }

        private void UtilityElectricity_Load(object sender, EventArgs e) {
            DatabaseControl.populateComboBox(ref utilNameList, DatabaseControl.utilCompanyTable, "CompanyName", "UtilityCompanyID",
                "IsElectricity=@value0", new Object[] { true });
            DatabaseControl.populateComboBox(ref tierSetId, DatabaseControl.tierTable, "TierSetName", "TierSetID");
            seasonBox.Items.Add("Summer");
            seasonBox.Items.Add("Winter");
            resetData();

            winterStartDate.Enabled = false;
            winterEndDate.Enabled = false;
            seasonBox.Enabled = false;
            season = ' ';

            serviceType.Items.Add("All Services");
            serviceType.Items.Add("All Electric");

            usageSurcharge.Items.Add(new CommonTools.Item(1, "Flat Credit/Charge"));
            usageSurcharge.Items.Add(new CommonTools.Item(2, "By Usage"));
            usageSurcharge.Items.Add(new CommonTools.Item(3, "By Days"));
        }

        private void resetData() {
            this.flatRates = new decimal[3][];
            for (int i = 0; i < 3; i++) {
                flatRates[i] = new decimal[] { 0.0000M, 0.0000M, 0.0000M, 0.0000M };
            }

            this.summerAllowanceRates = new decimal[2][];
            this.winterAllowanceRates = new decimal[2][];
            for (int i = 0; i < 2; i++) {
                summerAllowanceRates[i] = new decimal[] { 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M };
                winterAllowanceRates[i] = new decimal[] { 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M };
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
            tiers.Add(new CommonTools.Tier('G', season, "Regular"));
            tiers.Add(new CommonTools.Tier('D', season, "Regular"));

            descSurcharge.Text = "";
            statusSurcharge.Text = "";
            usageSurcharge.Text = "";
            rateSurcharge.Text = "0.0000";
            for (int i = 1; i <= 5; i++) {
                generationRatesInput.GetControlFromPosition(i, 0).Text = "0.0000";
            }
            for (int i = 1; i <= 5; i++) {
                deliveryRatesInput.GetControlFromPosition(i, 0).Text = "0.0000";
            }
        }

        private void saveData(ref decimal[][] flat) {
            flat = new decimal[3][];
        }

        private void displayData(decimal[][] flat, decimal[][] generation, decimal[][] delivery) {

        }

        private void nextBtn_Click(object sender, EventArgs e) {
            UtilityElectricity1 next = new UtilityElectricity1(ref this.summerAllowanceRates, ref this.winterAllowanceRates, ref medicalAllowance, hasWinter.Checked);
            next.ShowDialog();
            if (next.committed) {
                this.summerAllowanceRates = next.summerAllowance;
                this.winterAllowanceRates = next.winterAllowance;
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
            if (hasWinter.Checked) {
                Object[] values = { utilCompanyId, utility, effDate.Text, method.Text, measure.Text, hasWinter.Checked, winterStartDate.Value.Date, winterEndDate.Value.Date, ((CommonTools.Item)tierSetId.SelectedItem).Value };
                utilRateId = DatabaseControl.executeInsertQuery(DatabaseControl.utilRateTable, DatabaseControl.utilRateColumns, values);
                exportBasicRates(utilRateId);
                exportTiers(utilRateId);
                exportSurcharges(utilRateId);
                exportAllowance(utilRateId, summerAllowanceRates, 'S');
                exportAllowance(utilRateId, winterAllowanceRates, 'W');
            } else {
                Object[] values = { utilCompanyId, utility, effDate.Text, method.Text, measure.Text, hasWinter.Checked, null, null, ((CommonTools.Item)tierSetId.SelectedItem).Value };
                utilRateId = DatabaseControl.executeInsertQuery(DatabaseControl.utilRateTable, DatabaseControl.utilRateColumns, values);
                exportBasicRates(utilRateId);
                exportSurcharges(utilRateId);
                exportTiers(utilRateId);
                exportAllowance(utilRateId, summerAllowanceRates, ' ');
            }
            DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns,
                new Object[] { utilRateId, 'M', 'M', 'M', medicalAllowance });
        }

        private void exportBasicRates(int utilRateId) {
            foreach (CommonTools.CustomerCharge custCharge in custCharges) {
                Object[] values = { utilRateId, custCharge.status, custCharge.service, custCharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, values);
            }
        }

        private void exportAllowance(int utilRateId, decimal[][] allowance, char season) {
            decimal rate; char serviceType; int climateZone;

            serviceType = '1';
            for (int i = 0; i < allowance[0].Length; i++) {
                rate = allowance[0][i];
                climateZone = i;
                Object[] values = { utilRateId, serviceType, season, climateZone, rate};
                DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, values);
            }

            serviceType = '2';
            for (int i = 0; i < allowance[1].Length; i++) {
                rate = allowance[1][i];
                climateZone = i;
                Object[] values = { utilRateId, serviceType, season, climateZone, rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, values);
            }
        }

        public void exportTiers(int utilRateId) {
            foreach (CommonTools.Tier tierSet in tiers) {
                Object[] values = { utilRateId, tierSet.season, tierSet.chargeType, tierSet.status, tierSet.getTier(1), tierSet.getTier(2), tierSet.getTier(3), tierSet.getTier(4), tierSet.getTier(5) };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, values);
            }
        }

        public void exportSurcharges(int utilRateId) {
            foreach (CommonTools.Surcharge surcharge in surcharges) {
                Object[] values = { utilRateId, surcharge.info, surcharge.status, surcharge.usage, surcharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
            }
        }

        private void utilNameList_SelectedIndexChanged(object sender, EventArgs e) {
            CommonTools.Item item = (CommonTools.Item) utilNameList.SelectedItem;
            importUtilityInfo(item.Value);
        }

        private void importUtilityInfo(int utilCompanyId) {
            String[] fields = new String[] { "UtilityRateID", "EffectiveDate", "Method", "Unit", "HasWinter", "WinterStartDate", "WinterEndDate", "TierSetID" };
            String condition = "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC";
            Object[] values = DatabaseControl.getSingleRecord(fields, DatabaseControl.utilRateTable, condition, new Object[] { utilCompanyId });
            currUtilityRateId = (int) values[0];
            
            effDate.Value = (DateTime)values[1];
            method.Text = values[2].ToString();
            measure.Text = values[3].ToString();
            hasWinter.Checked = (bool)values[4];
            tierSetId.Text = CommonTools.Item.getString(ref tierSetId, (int)values[7]);

            descSurcharge.Items.Clear();
            statusSurcharge.Items.Clear();
            generationTierStatus.Items.Clear();
            deliveryTierStatus.Items.Clear();
            statusCustCharge.Items.Clear();
            descSurcharge.Items.Add("");
            statusSurcharge.Items.Add("");
            statusSurcharge.Items.Add("All");
            generationTierStatus.Items.Add("");
            deliveryTierStatus.Items.Add("");
            descSurcharge.Refresh();

            if (hasWinter.Checked) {
                season = seasonBox.Text.ToCharArray()[0];
                winterStartDate.Value = (DateTime)values[5];
                winterEndDate.Value = (DateTime)values[6];
                seasonBox.SelectedIndex = 0;
            } else {
                season = ' ';
                winterStartDate.Enabled = false;
                winterEndDate.Enabled = false;
                seasonBox.Enabled = false;
            }

            importBasicRates(currUtilityRateId);
            importTiers(currUtilityRateId);
            importSurcharges(currUtilityRateId);
            importAllowanceRates(currUtilityRateId);
            //displayTier(generationRatesInput, 'G', "Regular");
            //displayTier(deliveryRatesInput, 'D', "Regular");
            statusBox();
        }

        private void importBasicRates(int currUtilityRateId) {
            custCharges = new List<CommonTools.CustomerCharge>();
            String condition = "UtilityRateId=@value0";
            String[] fields = { "Status", "Service", "Rate" };
            ArrayList items = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilBasicRatesTable, condition, new Object[] { currUtilityRateId });
            foreach (Object[] item in items) {
                custCharges.Add(new CommonTools.CustomerCharge(item[0].ToString(), Convert.ToChar(item[1]), (decimal)item[2]));
            }
        }

        private void importTiers(int utilRateId) {
            tiers = new List<CommonTools.Tier>();
            String[] fields = { "ChargeType", "Season", "RateType", "Rate1", "Rate2", "Rate3", "Rate4", "Rate5" };
            String condition = "UtilityRateID=@value0";
            ArrayList tierSets = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilTierRatesTable, condition, new Object[] {utilRateId});
            foreach (Object[] set in tierSets) {
                CommonTools.Tier temp = new CommonTools.Tier(Convert.ToChar(set[0]), Convert.ToChar(set[1]), set[2].ToString());
                for (int i = 1; i <= 5; i++) { temp.setTier(i, (decimal)set[i + 2]); }
                tiers.Add(temp);
            }
        }

        private void importSurcharges(int utilRateId) {
            surcharges = new List<CommonTools.Surcharge>();
            String[] fields = { "Description", "RateType", "Usage", "Rate" };
            String condition = "UtilityRateID=@value0";
            ArrayList surchargeItems = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilSurchargeTable, condition, new Object[] { utilRateId });
            foreach (Object[] item in surchargeItems) {
                CommonTools.Surcharge temp = new CommonTools.Surcharge(item[0].ToString(), item[1].ToString(), (int)item[2], Convert.ToDecimal(item[3]));
                surcharges.Add(temp);
                descSurcharge.Items.Add(item[0].ToString());
            }
        }

        private void importAllowanceRates(int currUtilityRateId) {
            char season;
            char serviceType, climateZone;
            String condition = "UtilityRateId=@value0 AND ServiceType=@value1 AND Season=@value2 AND ClimateZone=@value3 ORDER BY BaselineAllowanceID DESC";
            this.summerAllowanceRates = new decimal[2][];
            this.winterAllowanceRates = new decimal[2][];

            for (int i = 0; i < 2; i++) {
                this.summerAllowanceRates[i] = new decimal[10];
                this.winterAllowanceRates[i] = new decimal[10];
                serviceType = (char)(i+1+48);
                for (int j = 0; j < 10; j++) {
                    climateZone = (char)(j+48);
                    if (hasWinter.Checked) {
                        Object[] valS = { currUtilityRateId, serviceType, 'S', climateZone };
                        Object[] recordS = DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, valS);
                        Object[] valW = { currUtilityRateId, serviceType, 'W', climateZone };
                        Object[] recordW = DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, valW);
                        this.summerAllowanceRates[i][j] = (decimal)recordS[0];
                        this.winterAllowanceRates[i][j] = (decimal)recordW[0];
                    }
                    else {
                        Object[] valS = { currUtilityRateId, serviceType, ' ', climateZone };
                        Object[] recordS = DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, valS);
                        this.summerAllowanceRates[i][j] = (decimal)recordS[0];
                    }
                }
            }
            condition = "UtilityRateId=@value0 AND ServiceType=@value1";
            this.medicalAllowance = (decimal)DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition,
                new Object[] {currUtilityRateId, 'M'})[0];
        }

        private void seasonBox_SelectedIndexChanged(object sender, EventArgs e) {
            season = seasonBox.Text.ToCharArray()[0];
        }

        private void hasWinter_CheckedChanged(object sender, EventArgs e) {
            if (hasWinter.Checked) { season = seasonBox.Text.ToCharArray()[0]; winterStartDate.Enabled = true; winterEndDate.Enabled = true; seasonBox.Enabled = true; } else { season = ' '; winterStartDate.Enabled = false; winterEndDate.Enabled = false; seasonBox.Enabled = false; }
            resetData();
        }

        private void saveCustBtn_Click(object sender, EventArgs e) {
            if (statusCustCharge.Text == "" || (serviceType.Text != "All Services" && serviceType.Text != "All Electric")) { MessageBox.Show("Please enter a label for customer charge."); return; }

            char service = ' ';
            if (serviceType.Text == "All Services") { service = '1'; } else if (serviceType.Text == "All Electric") { service = '2'; }
            CommonTools.CustomerCharge custCharge = new CommonTools.CustomerCharge(statusCustCharge.Text, service, Convert.ToDecimal(custChargeRate.Text));
            foreach (CommonTools.CustomerCharge item in custCharges) { if (item.Equals(custCharge)) { custCharges.Remove(item); break; } }
            custCharges.Add(custCharge);
            statusBox();
        }

        private void saveGenBtn_Click(object sender, EventArgs e) {
            if (generationTierStatus.Text == "") { MessageBox.Show("Please enter a label for tier set."); return; }
            CommonTools.Tier tierSet = new CommonTools.Tier('G', season, generationTierStatus.Text);
            for (int i = 1; i <= 5; i++) {
                try { tierSet.setTier(i, Convert.ToDecimal(generationRatesInput.GetControlFromPosition(i, 0).Text)); }
                catch { MessageBox.Show("Please enter valid tier rate."); return; }
            }
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(tierSet)) { tiers.Remove(set); break; }
            }
            tiers.Add(tierSet);
            statusBox();
        }

        private void saveDelBtn_Click(object sender, EventArgs e) {
            if (deliveryTierStatus.Text == "") { MessageBox.Show("Please enter a label for tier set."); return; }
            CommonTools.Tier tierSet = new CommonTools.Tier('D', season, deliveryTierStatus.Text);
            for (int i = 1; i <= 5; i++) {
                try { tierSet.setTier(i, Convert.ToDecimal(deliveryRatesInput.GetControlFromPosition(i, 0).Text)); }
                catch { MessageBox.Show("Please enter valid tier rate."); return; }
            }
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(tierSet)) { tiers.Remove(set); break; }
            }
            tiers.Add(tierSet);
            statusBox();
        }

        private void saveSurBtn_Click(object sender, EventArgs e) {
            if (descSurcharge.Text == "") { MessageBox.Show("Please enter a label for surcharge."); return; }
            if (statusSurcharge.Text == "") { MessageBox.Show("Please enter a status for surcharge."); return; } 
            if (usageSurcharge.Text == "") { MessageBox.Show("Please select a usage for surcharge."); return; }
            CommonTools.Surcharge surcharge = new CommonTools.Surcharge(descSurcharge.Text, statusSurcharge.Text, ((CommonTools.Item)usageSurcharge.SelectedItem).Value,
                Convert.ToDecimal(rateSurcharge.Text));
            foreach (CommonTools.Surcharge item in surcharges) {
                if (item.Equals(surcharge)) { surcharges.Remove(item); break; }
            }
            surcharges.Add(surcharge);
            if (!descSurcharge.Items.Contains(descSurcharge.Text)) { descSurcharge.Items.Add(descSurcharge.Text); }
            statusBox();
        }

        private void removeGenBtn_Click(object sender, EventArgs e) {
            removeTier(generationRatesInput, 'G', generationTierStatus.Text);
        }

        private void removeTier(TableLayoutPanel table, char charge, String description) {
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(new CommonTools.Tier(charge, season, description))) { 
                    tiers.Remove(set);
                    table.GetControlFromPosition(0, 0).Text = "";
                    for (int i = 1; i <= 5; i++) { table.GetControlFromPosition(i, 0).Text = "0.0000"; }
                    ((ComboBox)table.GetControlFromPosition(0, 0)).Items.Remove(set.status);
                    break; 
                }
            }
        }

        private void removeCustBtn_Click(object sender, EventArgs e) {
            char service = ' ';
            if (serviceType.Text == "All Services") { service = '1'; } else if (serviceType.Text == "All Electric") { service = '2'; }
            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (item.Equals(new CommonTools.CustomerCharge(statusCustCharge.Text, service, 0.0M))) {
                    custCharges.Remove(item);
                    statusCustCharge.Text = "";
                    serviceType.Text = "";
                    custChargeRate.Text = "0.00000";
                    break;
                }
            }
        }

        private void removeSurcharge(String description, String status) {
            foreach (CommonTools.Surcharge set in surcharges) {
                if (set.info == description && set.status == status) { 
                    surcharges.Remove(set);
                    descSurcharge.Items.Remove(set.info);
                    displaySurcharge("");
                    statusSurcharge.Text = "";
                    usageSurcharge.Text = "";
                    break; 
                }
            }
        }

        private void removeDelBtn_Click(object sender, EventArgs e) {
            removeTier(deliveryRatesInput, 'D', deliveryTierStatus.Text);
        }

        private void removeSurBtn_Click(object sender, EventArgs e) {
            try { removeSurcharge(descSurcharge.Text, statusSurcharge.Text); }
            catch { }
        }

        private void displayCustCharge(String status, char service = ' ') {
            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (item.status == status && (service == ' ' || item.service == service)) {
                    statusCustCharge.Text = item.status;
                    if (item.service == '1') { 
                        serviceType.Text = "All Services";
                    } else if (item.service == '2') { 
                        serviceType.Text = "All Electric";
                    }
                    custChargeRate.Text = item.rate.ToString();
                    return;
                }
            }
            statusCustCharge.Text = status;
            serviceType.Text = "";
            custChargeRate.Text = "0.0000";
        }

        private void displayTier(TableLayoutPanel table, char charge, String description) {
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(new CommonTools.Tier(charge, season, description))) {
                    table.GetControlFromPosition(0, 0).Text = description;
                    for (int i = 1; i <= 5; i++) { table.GetControlFromPosition(i, 0).Text = set.getTier(i).ToString(); }
                    return;
                }
            }
            for (int i = 1; i <= 5; i++) { table.GetControlFromPosition(i, 0).Text = "0.0000"; }
        }

        private void displaySurcharge(String description, String status = "") {
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
            rateSurcharge.Text = display.rate.ToString();
        }

        private void generationTierSet_SelectedIndexChanged(object sender, EventArgs e) {
            displayTier(generationRatesInput, 'G', generationTierStatus.Text);
        }

        private void deliveryTierSet_SelectedIndexChanged(object sender, EventArgs e) {
            displayTier(deliveryRatesInput, 'D', deliveryTierStatus.Text);
        }

        private void surchargeList_SelectedIndexChanged(object sender, EventArgs e) {
            displaySurcharge(descSurcharge.Text);
        }

        private void statusSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            displaySurcharge(descSurcharge.Text, statusSurcharge.Text);
        }

        private void statusCustCharge_SelectedIndexChanged(object sender, EventArgs e) {
            displayCustCharge(statusCustCharge.Text);
        }

        private void serviceType_SelectedIndexChanged(object sender, EventArgs e) {
            char service = ' ';
            if (serviceType.Text == "All Services") { service = '1'; } else if (serviceType.Text == "All Electric") { service = '2'; }
            displayCustCharge(statusCustCharge.Text, service);
        }

        private void tierSetId_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void statusCheck() {
            List<String> statuses = new List<String>();
            foreach (CommonTools.Tier tier in tiers) if (!statuses.Contains(tier.status)) statuses.Add(tier.status);
            foreach (CommonTools.Surcharge surcharge in surcharges) if (!surcharge.status.Equals("All") && !statuses.Contains(surcharge.status)) statuses.Add(surcharge.status);
            foreach (CommonTools.CustomerCharge custCharge in custCharges) if (!statuses.Contains(custCharge.status)) statuses.Add(custCharge.status);

            foreach (String status in statuses) {
                if (!tiers.Contains(new CommonTools.Tier('D', season, status))) tiers.Add(new CommonTools.Tier('D', season, status));
                if (!tiers.Contains(new CommonTools.Tier('G', season, status))) tiers.Add(new CommonTools.Tier('G', season, status));
                if (!custCharges.Contains(new CommonTools.CustomerCharge(status, '1', 0.0M))) custCharges.Add(new CommonTools.CustomerCharge(status, '1', 0.0M));
                if (!custCharges.Contains(new CommonTools.CustomerCharge(status, '2', 0.0M))) custCharges.Add(new CommonTools.CustomerCharge(status, '2', 0.0M));
                if (hasWinter.Checked) { 
                    if (season == 'S') season = 'W'; else if (season == 'W') season = 'S';
                    if (!tiers.Contains(new CommonTools.Tier('D', season, status))) tiers.Add(new CommonTools.Tier('D', season, status));
                    if (!tiers.Contains(new CommonTools.Tier('G', season, status))) tiers.Add(new CommonTools.Tier('G', season, status));
                }
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
    }
}
