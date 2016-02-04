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
        public char utility = 'G';
        decimal[][] summerAllowance, winterAllowance;
        decimal medicalAllowance;
        List<CommonTools.CustomerCharge> custCharges;
        List<CommonTools.Tier> tiers;
        List<CommonTools.Surcharge> surcharges;

        public UtilityGas0()
        {
            InitializeComponent();
        }

        private void UtilityGas_Load(object sender, EventArgs e)
        {
            DatabaseControl.populateComboBox(ref utilNameList, DatabaseControl.utilCompanyTable, "CompanyName", "UtilityCompanyID",
                "IsGas=@value0", new Object[] { true });
            resetData();

            serviceType.Items.Add("All Services");
            serviceType.Items.Add("Cooking Only");
            serviceType.Items.Add("Space Heating Only");

            usageSurcharge.Items.Add(new CommonTools.Item(1, "Flat Credit/Charge"));
            usageSurcharge.Items.Add(new CommonTools.Item(2, "By Usage"));
            usageSurcharge.Items.Add(new CommonTools.Item(3, "By Days"));
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
            tiers.Add(new CommonTools.Tier('G', ' ', "Regular"));

            summerAllowance = new decimal[3][];
            winterAllowance = new decimal[3][];
            medicalAllowance = 0.00000M;
            for (int i = 0; i < 3; i++)
            {
                summerAllowance[i] = new decimal[] { 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M };
                winterAllowance[i] = new decimal[] { 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M, 0.00000M };
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            UtilityGas1 next = new UtilityGas1(ref summerAllowance, ref winterAllowance, ref medicalAllowance);
            next.ShowDialog();
            if (next.committed)
            {
                summerAllowance = next.summerAllowance;
                winterAllowance = next.winterAllowance;
                medicalAllowance = next.medicalAllowance;
            }
        }

        private void fillUtilityInfo(Object[] values)
        {
            String[] fields = new String[] { "EffectiveDate", "Method", "Unit", "HasWinter", "WinterStartDate", "WinterEndDate" };
            Object[] rateIdInfo = DatabaseControl.getSingleRecord(fields, DatabaseControl.utilRateTable, "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[]{values[0]});
            effDate.Value = (DateTime)rateIdInfo[0];
            method.Text = rateIdInfo[1].ToString();
            measure.Text = rateIdInfo[2].ToString();
            hasWinter.Checked = (bool)rateIdInfo[3];
            winterStartDate.Value = (DateTime)rateIdInfo[4];
            winterEndDate.Value = (DateTime)rateIdInfo[5];
            
            int utilRateId = (int)(DatabaseControl.getSingleRecord(new String[] { "UtilityRateID" }, DatabaseControl.utilRateTable,
                "UtilityCompanyID=@value0 AND UtilityServiceType = @value1 ORDER BY UtilityRateID DESC", new Object[] { values[0], 'G' })[0]);
            importCustomerCharges(utilRateId);
            importSurcharges(utilRateId);
            importTiers(utilRateId);
            importAllowanceRates(utilRateId);
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            int utilCompanyId, utilRateId;
            Object[] values = { utilNameList.Text,  false, true, false};
            if (utilNameList.SelectedItem is CommonTools.Item) {
                utilCompanyId = ((CommonTools.Item)utilNameList.SelectedItem).Value;
                String condition = "UtilityCompanyID=" + utilCompanyId;
                DatabaseControl.executeUpdateQuery(DatabaseControl.utilCompanyTable, DatabaseControl.utilCompanyColumns, values, condition);
            }
            else {
                utilCompanyId = DatabaseControl.executeInsertQuery(DatabaseControl.utilCompanyTable, DatabaseControl.utilCompanyColumns, values);
            }
            utilRateId = DatabaseControl.executeInsertQuery(DatabaseControl.utilRateTable, DatabaseControl.utilRateColumns,
                new Object[] { utilCompanyId, 'G', effDate.Text, method.Text, measure.Text, hasWinter.Checked, winterStartDate.Value.Date, winterEndDate.Value.Date, 1 });
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
        }

        private void exportCustomerCharges(int utilRateId) {
            foreach (CommonTools.CustomerCharge custCharge in custCharges) {
                Object[] values = { utilRateId, custCharge.status, custCharge.service, custCharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilBasicRatesTable, DatabaseControl.utilBasicRatesColumns, values);
            }
        }

        private void exportAllowanceRates(int utilRateId) {
            char serviceType, climateZone;
            if (hasWinter.Checked) {
                char season = 'S';
                for (int i = 0; i < summerAllowance.Length; i++) {
                    serviceType = (char)(i+1+48);
                    for (int j = 0; j < summerAllowance[i].Length; j++) {
                        climateZone = (char)(j+48);
                        Object[] values = { utilRateId, serviceType, season, climateZone, summerAllowance[i][j] };
                        DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, values);
                    }
                }
                season = 'W';
                for (int i = 0; i < winterAllowance.Length; i++) {
                    serviceType = (char)(i+1+48);
                    for (int j = 0; j < winterAllowance[i].Length; j++) {
                        climateZone = (char)(j + 48);
                        Object[] values = { utilRateId, serviceType, season, climateZone, winterAllowance[i][j] };
                        DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, values);
                    }
                }
            }
            else {
                char season = ' ';
                for (int i = 0; i < summerAllowance.Length; i++) {
                    serviceType = (char)(i+1+48);
                    for (int j = 0; j < summerAllowance[i].Length; j++) {
                        climateZone = (char)(j + 48);
                        Object[] values = { utilRateId, serviceType, season, climateZone, summerAllowance[i][j] };
                        DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns, values);
                    }
                }
            }

            DatabaseControl.executeInsertQuery(DatabaseControl.baselineAllowanceTable, DatabaseControl.baselineAllowanceColumns,
                new Object[] { utilRateId, 'M', 'M', 'M', medicalAllowance });
        }

        public void exportSurcharges(int utilRateId) {
            foreach (CommonTools.Surcharge surcharge in surcharges) {
                Object[] values = { utilRateId, surcharge.info, surcharge.status, surcharge.usage, surcharge.rate };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilSurchargeTable, DatabaseControl.utilSurchargeColumns, values);
            }
        }

        private void exportTiers(int utilRateId) {
            foreach (CommonTools.Tier tierSet in tiers) {
                Object[] values = { utilRateId, tierSet.season, tierSet.chargeType, tierSet.status, tierSet.getTier(1), tierSet.getTier(2), tierSet.getTier(3), tierSet.getTier(4), tierSet.getTier(5) };
                DatabaseControl.executeInsertQuery(DatabaseControl.utilTierRatesTable, DatabaseControl.utilTierRatesColumns, values);
            }
        }

        private void importCustomerCharges(int utilRateId) {
            custCharges = new List<CommonTools.CustomerCharge>();
            String condition = "UtilityRateId=@value0";
            String[] fields = { "Status", "Service", "Rate" };
            ArrayList items = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilBasicRatesTable, condition, new Object[] { utilRateId });
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
            String condition = "UtilityRateID=@value0 AND ServiceType=@value1 AND Season=@value2 AND ClimateZone=@value3";
            this.summerAllowance = new decimal[3][];
            this.winterAllowance = new decimal[3][];

            for (int i = 0; i < 3; i++) {
                this.summerAllowance[i] = new decimal[10];
                this.winterAllowance[i] = new decimal[10];
                serviceType = (char)(i + 1 + 48);
                for (int j = 0; j < 10; j++) {
                    climateZone = (char)(j + 48);
                    if (hasWinter.Checked) {
                        Object[] valS = { utilRateId, serviceType, 'S', climateZone };
                        Object[] recordS = DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, valS);
                        Object[] valW = { utilRateId, serviceType, 'W', climateZone };
                        Object[] recordW = DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, valW);
                        this.summerAllowance[i][j] = (decimal)recordS[0];
                        this.winterAllowance[i][j] = (decimal)recordW[0];
                    }
                    else {
                        Object[] valS = { utilRateId, serviceType, ' ', climateZone };
                        Object[] recordS = DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition, valS);
                        this.summerAllowance[i][j] = (decimal)recordS[0];
                    }
                }
            }
            this.medicalAllowance = (decimal)DatabaseControl.getSingleRecord(new String[] { "BaselineAllowanceRate" }, DatabaseControl.baselineAllowanceTable, condition,
                new Object[] { utilRateId, 'M', 'M', 'M'} )[0];
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

        private void importTiers(int utilRateId) {
            tiers = new List<CommonTools.Tier>();
            String[] fields = { "RateType", "Rate1", "Rate2", "Rate3", "Rate4", "Rate5" };
            String condition = "UtilityRateID=@value0";
            ArrayList tierSets = DatabaseControl.getMultipleRecord(fields, DatabaseControl.utilTierRatesTable, condition, new Object[] { utilRateId });
            foreach (Object[] set in tierSets) {
                CommonTools.Tier temp = new CommonTools.Tier('G', ' ', set[0].ToString());
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
                    }
                    custChargeRate.Text = item.rate.ToString();
                    return;
                }
            }
            statusCustCharge.Text = status;
            serviceType.Text = "";
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
            displaySurcharge(descSurcharge.Text);
        }

        private void statusSurcharge_SelectedIndexChanged(object sender, EventArgs e) {
            displaySurcharge(descSurcharge.Text, statusSurcharge.Text);
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

        private void saveCustBtn_Click(object sender, EventArgs e) {
            if (statusCustCharge.Text == "" || (serviceType.Text != "All Services" && serviceType.Text != "Cooking Only" && serviceType.Text != "Space Heating Only")) { 
                MessageBox.Show("Please enter a label for customer charge."); return; 
            }

            char service = mapServiceType(serviceType.Text);
            CommonTools.CustomerCharge custCharge = new CommonTools.CustomerCharge(statusCustCharge.Text, service, Convert.ToDecimal(custChargeRate.Text));
            foreach (CommonTools.CustomerCharge item in custCharges) {
                if (item.Equals(custCharge)) { custCharges.Remove(item); break; }
            }
            custCharges.Add(custCharge);

            if (!tierStatus.Items.Contains(statusCustCharge.Text)) { tierStatus.Items.Add(statusCustCharge.Text); }
            if (!statusSurcharge.Items.Contains(statusCustCharge.Text)) { statusSurcharge.Items.Add(statusCustCharge.Text); }
            if (!statusCustCharge.Items.Contains(statusCustCharge.Text)) { statusCustCharge.Items.Add(statusCustCharge.Text); }
        }

        private void saveTierBtn_Click(object sender, EventArgs e) {
            if (tierStatus.Text == "") { MessageBox.Show("Please enter a label for tier set."); return; }
            CommonTools.Tier tierSet = new CommonTools.Tier('G', ' ', tierStatus.Text);
            for (int i = 1; i <= 5; i++) {
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

        private char mapServiceType(String service) {
            char type = ' ';
            if (serviceType.Text == "All Services") {
                type = '1';
            } else if (serviceType.Text == "Cooking Only") {
                type = '2';
            } else if (serviceType.Text == "Space Heating Only") {
                type = '3';
            }
            return type;
        }

        private void removeCustBtn_Click(object sender, EventArgs e) {
            char service = mapServiceType(serviceType.Text);
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
                    ((ComboBox)tierRatesInput.GetControlFromPosition(0, 0)).Items.Remove(set.status);
                    break;
                }
            }
        }
    }
}
