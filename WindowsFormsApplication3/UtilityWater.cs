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
    public partial class UtilityWater : Form
    {
        List<CommonTools.CustomerCharge> custCharges;
        List<CommonTools.Tier> tiers;
        List<CommonTools.Surcharge> surcharges;

        public UtilityWater()
        {
            InitializeComponent();
        }

        private void UtilityWater_Load(object sender, EventArgs e)
        {
            DatabaseControl.populateComboBox(ref utilNameList, DatabaseControl.utilCompanyTable, "CompanyName", "UtilityCompanyID",
                "IsWater=@value0", new Object[] { true });
            DatabaseControl.populateComboBox(ref tierSetId, DatabaseControl.tierTable, "TierSetName", "TierSetID");
            resetData();

            usageSurcharge.Items.Add(new CommonTools.Item(1, "Flat Credit/Charge"));
            usageSurcharge.Items.Add(new CommonTools.Item(2, "By Usage"));
            usageSurcharge.Items.Add(new CommonTools.Item(3, "By Days"));
        }

        private void resetData() {
            custCharges = new List<CommonTools.CustomerCharge>();
            tiers = new List<CommonTools.Tier>();
            surcharges = new List<CommonTools.Surcharge>();

            descSurcharge.Items.Clear();
            descSurcharge.Items.Add("");
            statusSurcharge.Items.Clear();
            statusSurcharge.Items.Add("");
            statusSurcharge.Items.Add("All");
            statusSurcharge.Items.Add("Regular");
            tierStatus.Items.Clear();
            tierStatus.Items.Add("");
            tierStatus.Items.Add("Regular");
            tiers.Add(new CommonTools.Tier('G', ' ', "Regular"));
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Object[] values = { utilNameList.Text, false, false, true };
            int utilCompanyId, utilRateId;
            if (utilNameList.SelectedItem is CommonTools.Item)
            {
                utilCompanyId = ((CommonTools.Item)utilNameList.SelectedItem).Value;
                String condition = "UtilityCompanyID=" + utilCompanyId;
                DatabaseControl.executeUpdateQuery(DatabaseControl.utilCompanyTable, DatabaseControl.utilCompanyColumns, values, condition);
            }
            else
            {
                utilCompanyId = DatabaseControl.executeInsertQuery(DatabaseControl.utilCompanyTable, DatabaseControl.utilCompanyColumns, values);
            }
            utilRateId = DatabaseControl.executeInsertQuery(DatabaseControl.utilRateTable, DatabaseControl.utilRateColumns,
                new Object[] { utilCompanyId, 'W', effDate.Text, method.Text, measure.Text, null, null, null, ((CommonTools.Item)tierSetId.SelectedItem).Value });
            exportCustomerCharges(utilRateId);
            exportSurcharges(utilRateId);
            exportWaterRates(utilRateId);
            exportSewerRates(utilRateId);
            exportAllowanceRates(utilRateId);
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void utilNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonTools.Item item = (CommonTools.Item)utilNameList.SelectedItem;
            int id = (int)DatabaseControl.getSingleRecord(new String[] { "*" },
                DatabaseControl.utilCompanyTable, "UtilityCompanyID=@value0", new Object[] { item.Value })[0];
            fillUtilityInfo(id);
        }

        private void fillUtilityInfo(int id)
        {
            String[] fields = new String[] { "EffectiveDate", "Method", "Unit", "TierSetId" };
            Object[] rateIdInfo = DatabaseControl.getSingleRecord(fields, DatabaseControl.utilRateTable, "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { id });
            effDate.Value = (DateTime)rateIdInfo[0];
            method.Text = rateIdInfo[1].ToString();
            measure.Text = rateIdInfo[2].ToString();
            tierSetId.Text = CommonTools.Item.getString(ref tierSetId, (int)rateIdInfo[3]);

            int utilRateId = (int)(DatabaseControl.getSingleRecord(new String[] {"UtilityRateID"}, DatabaseControl.utilRateTable,
                "UtilityCompanyID=@value0 AND UtilityServiceType = @value1 ORDER BY UtilityRateID DESC", new Object[] {id, 'W'})[0]);
            importCustomerCharges(utilRateId);
            importSurcharges(utilRateId);
            importWaterRates(utilRateId);
            importSewerRates(utilRateId);
            importAllowanceRates(utilRateId);
        }

        private void exportCustomerCharges(int utilRateId) {
            foreach (CommonTools.CustomerCharge custCharge in custCharges) {
                Object[] values = { utilRateId, custCharge.status, custCharge.service, custCharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, values);
            }
        }

        public void exportSurcharges(int utilRateId) {
            foreach (CommonTools.Surcharge surcharge in surcharges) {
                Object[] values = { utilRateId, surcharge.info, surcharge.status, surcharge.usage, surcharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
            }
        }

        private void exportWaterRates(int utilRateId) {
            foreach (CommonTools.Tier tierSet in tiers) {
                Object[] values = { utilRateId, tierSet.season, tierSet.chargeType, tierSet.status, tierSet.getTier(1), tierSet.getTier(2), tierSet.getTier(3), tierSet.getTier(4), tierSet.getTier(5) };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, values);
            }
        }

        private void exportSewerRates(int utilRateId) {
            decimal tier1 = Convert.ToDecimal(sewerRates.GetControlFromPosition(1, 0).Text);
            decimal tier2 = Convert.ToDecimal(sewerRates.GetControlFromPosition(2, 0).Text);
            decimal tier3 = Convert.ToDecimal(sewerRates.GetControlFromPosition(3, 0).Text);
            Object[] values = new Object[] { utilRateId, ' ', 'S', "All", tier1, tier2, tier3, 0.0M, 0.0M };
            DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, values);

            values = new Object[] { utilRateId, "All", '3', Convert.ToDecimal(sewerRates.GetControlFromPosition(0, 0).Text) };
            DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, values);
        }

        private void exportAllowanceRates(int utilRateId) {
            DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, 
                new Object[] { utilRateId, 'W', 'S', ' ', Convert.ToDecimal(waterSummer.Text) });
            DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns,
                new Object[] { utilRateId, 'W', 'W', ' ', Convert.ToDecimal(waterWinter.Text) });
        }

        private void importCustomerCharges(int utilRateId) {
            custCharges = new List<CommonTools.CustomerCharge>();
            String condition = "UtilityRateId=@value0 AND Service=@value1";
            String[] fields = { "Status", "Rate" };
            ArrayList items = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilBasicRatesTable, condition, new Object[] { utilRateId, 'W' });
            foreach (Object[] item in items) {
                custCharges.Add(new CommonTools.CustomerCharge(item[0].ToString(), 'W', (decimal)item[1]));
            }

            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (!tierStatus.Items.Contains(item.status)) { tierStatus.Items.Add(item.status); }
                if (!statusCustCharge.Items.Contains(item.status)) { statusCustCharge.Items.Add(item.status); }
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
                if (!statusSurcharge.Items.Contains(item[1].ToString())) { statusSurcharge.Items.Add(item[1].ToString()); }
                if (!statusCustCharge.Items.Contains(item[1].ToString()) && item[1].ToString() != "All") { statusCustCharge.Items.Add(item[1].ToString()); }
                if (!tierStatus.Items.Contains(item[1].ToString()) && item[1].ToString() != "All") { tierStatus.Items.Add(item[1].ToString()); }
            }
        }

        private void importWaterRates(int utilRateId)
        {
            tiers = new List<CommonTools.Tier>();
            String[] fields = { "RateType", "Rate1", "Rate2", "Rate3" };
            String condition = "UtilityRateID=@value0 AND ChargeType=@value1";
            ArrayList tierSets = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilTierRatesTable, condition, new Object[] { utilRateId, 'G' });
            foreach (Object[] set in tierSets) {
                CommonTools.Tier temp = new CommonTools.Tier('G', ' ', set[0].ToString());
                for (int i = 1; i <= 3; i++) { temp.setTier(i, (decimal)set[i]); }
                tiers.Add(temp);
                if (!tierStatus.Items.Contains(set[0].ToString())) { tierStatus.Items.Add(set[0].ToString()); }
                if (!statusSurcharge.Items.Contains(set[0].ToString())) { statusSurcharge.Items.Add(set[0].ToString()); }
                if (!statusCustCharge.Items.Contains(set[0].ToString())) { statusCustCharge.Items.Add(set[0].ToString()); }
            }
        }

        private void importSewerRates(int utilRateId) {
            String[] fields = { "RateType", "Rate1", "Rate2", "Rate3" };
            String condition = "UtilityRateID=@value0 AND ChargeType=@value1";
            Object[] sewer = DatabaseControl.getSingleRecord(fields, DatabaseControl.utilTierRatesTable, condition, new Object[] { utilRateId, 'S' });
            for (int i = 1; i <= 3; i++) {
                sewerRates.GetControlFromPosition(i, 0).Text = sewer[i].ToString();
            }

            condition = "UtilityRateID=@value0 AND Service=@value1";
            DatabaseControl.getSingleRecord(new String[] { "Rate" }, DatabaseControl.utilBasicRatesTable, condition, new Object[] { utilRateId, '3' });
        }

        private void importAllowanceRates(int utilRateId) {
            String[] fields = { "BaselineAllowanceRate" };
            String condition = "UtilityRateID=@value0 AND ServiceType=@value1 AND Season=@value2";
            decimal waterS = (decimal)DatabaseControl.getSingleRecord(fields, DatabaseControl.baselineAllowanceTable, condition, new Object[] { utilRateId, 'W', 'S' })[0];
            decimal waterW = (decimal)DatabaseControl.getSingleRecord(fields, DatabaseControl.baselineAllowanceTable, condition, new Object[] { utilRateId, 'W', 'W' })[0];
            waterSummer.Text = waterS.ToString();
            waterWinter.Text = waterW.ToString();
        }

        private void descSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            displaySurcharge(descSurcharge.Text);
        }

        private void statusSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            displaySurcharge(descSurcharge.Text, statusSurcharge.Text);
        }

        private void tierStatus_SelectedIndexChanged(object sender, EventArgs e) {
            displayTier(tierStatus.Text);
        }

        private void statusCustCharge_SelectedIndexChanged(object sender, EventArgs e) {
            displayCustCharge(statusCustCharge.Text);
        }

        private void displayCustCharge(String status, char service = 'W') {
            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (item.status == status) {
                    statusCustCharge.Text = item.status;
                    custChargeRate.Text = item.rate.ToString();
                    return;
                }
            }
            statusCustCharge.Text = status;
            custChargeRate.Text = "0.0000";
        }

        private void displaySurcharge(String description, String status = "") {
            CommonTools.Surcharge display = null;
            foreach (CommonTools.Surcharge surcharge in surcharges) {
                if (surcharge.info == description && (status == "" || surcharge.status == status)) { display = surcharge; break; }
            }
            if (display == null) {
                rateSurcharge.Text = "0.00000";
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

        private void displayTier(String description) {
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(new CommonTools.Tier('G', ' ', description))) {
                    tierRatesInput.GetControlFromPosition(0, 0).Text = description;
                    for (int i = 1; i <= 3; i++) {
                        tierRatesInput.GetControlFromPosition(i, 0).Text = set.getTier(i).ToString();
                    }
                    return;
                }
            }
            for (int i = 1; i <= 3; i++) {
                tierRatesInput.GetControlFromPosition(i, 0).Text = "0.00000";
            }
        }

        private void saveCustBtn_Click(object sender, EventArgs e) {
            if (statusCustCharge.Text == "") {
                MessageBox.Show("Please enter a label for customer charge."); return;
            }

            char service = 'W';
            CommonTools.CustomerCharge custCharge = new CommonTools.CustomerCharge(statusCustCharge.Text, service, Convert.ToDecimal(custChargeRate.Text));
            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (item.Equals(custCharge)) { custCharges.Remove(item); break; }
            }
            custCharges.Add(custCharge);

            if (!tierStatus.Items.Contains(statusCustCharge.Text)) { tierStatus.Items.Add(statusCustCharge.Text); }
            if (!statusSurcharge.Items.Contains(statusCustCharge.Text)) { statusSurcharge.Items.Add(statusCustCharge.Text); }
            if (!statusCustCharge.Items.Contains(statusCustCharge.Text)) { statusCustCharge.Items.Add(statusCustCharge.Text); }
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
            if (!tierStatus.Items.Contains(statusSurcharge.Text) && statusSurcharge.Text != "All") { tierStatus.Items.Add(statusSurcharge.Text); }
            if (!statusCustCharge.Items.Contains(statusSurcharge.Text) && statusSurcharge.Text != "All") { statusCustCharge.Items.Add(statusSurcharge.Text); }
        }

        private void saveTierBtn_Click(object sender, EventArgs e) {
            if (tierStatus.Text == "") { MessageBox.Show("Please enter a label for tier set."); return; }
            CommonTools.Tier tierSet = new CommonTools.Tier('G', ' ', tierStatus.Text);
            for (int i = 1; i <= 3; i++) {
                try { tierSet.setTier(i, Convert.ToDecimal(tierRatesInput.GetControlFromPosition(i, 0).Text)); } catch { MessageBox.Show("Please enter valid tier rate."); return; }
            }
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(tierSet)) { tiers.Remove(set); break; }
            }
            tiers.Add(tierSet);
            if (!tierStatus.Items.Contains(tierStatus.Text)) { tierStatus.Items.Add(tierStatus.Text); }
            if (!statusSurcharge.Items.Contains(tierStatus.Text)) { statusSurcharge.Items.Add(tierStatus.Text); }
            if (!statusCustCharge.Items.Contains(tierStatus.Text)) { statusCustCharge.Items.Add(tierStatus.Text); }
        }

        private void removeCustBtn_Click(object sender, EventArgs e) {
            char service = 'W';
            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (item.Equals(new CommonTools.CustomerCharge(statusCustCharge.Text, service, 0.0M))) {
                    custCharges.Remove(item);
                    statusCustCharge.Text = "";
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
                    displaySurcharge("");
                    statusSurcharge.Text = "";
                    usageSurcharge.Text = "";
                    break;
                }
            }
        }

        private void removeTierBtn_Click(object sender, EventArgs e) {
            foreach (CommonTools.Tier set in tiers) {
                if (set.Equals(new CommonTools.Tier('G', ' ', tierStatus.Text))) {
                    tiers.Remove(set);
                    tierRatesInput.GetControlFromPosition(0, 0).Text = "";
                    displayTier(tierStatus.Text);
                    tierStatus.Items.Remove(set.status);
                    break;
                }
            }
        }

        private void tierSetId_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
