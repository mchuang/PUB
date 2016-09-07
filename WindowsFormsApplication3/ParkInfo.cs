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
    public partial class ParkInfo : Form
    {
        List<CommonTools.Option> options;
        List<CommonTools.Temp> temps;
        public ParkInfo()
        {
            InitializeComponent();
        }

        private void ParkInfo_Load(object sender, EventArgs e)
        {
            options = new List<CommonTools.Option>();
            temps = new List<CommonTools.Temp>();
            DatabaseControl.populateComboBox(ref optionDesc, DatabaseControl.optionsTable, "ChargeItemDescription", "ChargeItemID");
            DatabaseControl.populateComboBox(ref parkNumberList, DatabaseControl.parkTable, "ParkNumber", "ParkID", "1=1 ORDER BY ParkNumber", new Object[] {});
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkName", "ParkID");
            DatabaseControl.populateComboBox(ref ownerList, DatabaseControl.ownerTable, "CompanyName", "OwnerID");
            DatabaseControl.populateComboBox(ref gasList, DatabaseControl.utilCompanyTable, "CompanyName", "UtilityCompanyID", "IsGas=@value0", new Object[] {true});
            DatabaseControl.populateComboBox(ref eleList, DatabaseControl.utilCompanyTable, "CompanyName", "UtilityCompanyID", "IsElectricity=@value0", new Object[] { true });
            DatabaseControl.populateComboBox(ref watList, DatabaseControl.utilCompanyTable, "CompanyName", "UtilityCompanyID", "IsWater=@value0", new Object[] { true });
        }

        private void parkNumberList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (parkNumberList.FindString(parkNumberList.Text) == -1)
            {
                return;
            }
            fillParkInfo(((CommonTools.Item)parkNumberList.SelectedItem).Value);
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (parkList.FindString(parkList.Text) == -1)
            {
                return;
            }
            CommonTools.Item park = (CommonTools.Item)parkList.SelectedItem;
            fillParkInfo(park.Value);
        }

        private void clearParkInfo()
        {
            parkNumberList.Refresh();
            parkList.Refresh();
            addrLbl.Refresh();
            cityLbl.Refresh();
            stateLbl.Refresh();
            zipLbl.Refresh();
            phoneLbl.Refresh();
            faxLbl.Refresh();

            eleZone.Refresh();
            gasZone.Refresh();

            eleList.Refresh();
            gasList.Refresh();
            watList.Refresh();
            eleList.Text = gasList.Text = watList.Text = "";
        }

        private void fillParkInfo(int parkID)
        {
            clearParkInfo();
            Object[] values = DatabaseControl.getSingleRecord(DatabaseControl.parkColumns, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { parkID });
            parkNumberList.SelectedIndex = parkNumberList.FindStringExact(values[0].ToString());
            parkList.SelectedIndex = parkList.FindStringExact(values[1].ToString());
            addrLbl.Text = values[2].ToString();
            cityLbl.Text = values[3].ToString();
            stateLbl.Text = values[4].ToString();
            zipLbl.Text = values[5].ToString();
            phoneLbl.Text = values[6].ToString();
            faxLbl.Text = values[7].ToString();
            csvId.Text = values[14].ToString();
            topMessage.Text = values[15].ToString();
            botMessage.Text = values[16].ToString();
            clerkInfo.Text = values[17].ToString();

            eleZone.Text = values[10].ToString();
            gasZone.Text = values[12].ToString();

            if (values[8] != DBNull.Value)
            {
                ownerList.SelectedIndex = ownerList.FindStringExact(CommonTools.Item.getString(ref ownerList, (int)values[8]));
            }
            if (values[9] != DBNull.Value)
            {
                eleList.SelectedIndex = eleList.FindStringExact(CommonTools.Item.getString(ref eleList, (int)values[9]));
            }
            if (values[11] != DBNull.Value)
            {
                gasList.SelectedIndex = gasList.FindStringExact(CommonTools.Item.getString(ref gasList, (int)values[11]));
            }
            if (values[13] != DBNull.Value)
            {
                watList.SelectedIndex = watList.FindStringExact(CommonTools.Item.getString(ref watList, (int)values[13]));
            }

            for (int i = 0; i < 3; i++)
            {
                char utilityType = ' ';
                switch (i)
                {
                    case 0: utilityType = 'G'; break;
                    case 1: utilityType = 'E'; break;
                    case 2: utilityType = 'W'; break;
                }
                Object[] taxValues = { parkID, utilityType };
                Object[] taxes = DatabaseControl.getSingleRecord(new String[] { "InspectionFee", "CountyTax", "LocalTax", "StateTax", "RegTax" }, DatabaseControl.taxTable,
                    "ParkID=@value0 AND UtilityType=@value1", taxValues);
                if (taxes == null) continue;
                for (int j = 0; j < 5; j++)
                {
                    taxRates.GetControlFromPosition(j, i).Text = taxes[j].ToString();
                }
            }

            options = new List<CommonTools.Option>();
            temps = new List<CommonTools.Temp>();
            importOptions(parkID);
            importTemps(parkID);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            String parkName;
            if (parkList.SelectedItem is CommonTools.Item) { parkName = ((CommonTools.Item)parkList.SelectedItem).Text; }
            else { parkName = parkList.Text; }

            Object owner, ele, gas, wat;
            if (ownerList.SelectedItem == null) { owner = null; }
            else { owner = ((CommonTools.Item)ownerList.SelectedItem).Value; }
            if (eleList.SelectedItem == null) { ele = null; }
            else { ele = ((CommonTools.Item)eleList.SelectedItem).Value; }
            if (gasList.SelectedItem == null) { gas = null; }
            else { gas = ((CommonTools.Item)gasList.SelectedItem).Value; }
            if (watList.SelectedItem == null) { wat = null; }
            else { wat = ((CommonTools.Item)watList.SelectedItem).Value; }
            Object[] values = { parkNumberList.Text, parkName, addrLbl.Text, cityLbl.Text, stateLbl.Text, zipLbl.Text, phoneLbl.Text, faxLbl.Text,
                                owner, ele, eleZone.Text.Trim(), gas, gasZone.Text.Trim(), wat, csvId.Text, topMessage.Text, botMessage.Text, clerkInfo.Text };

            if (parkList.SelectedItem is CommonTools.Item)
            {
                int parkId=((CommonTools.Item)parkList.SelectedItem).Value;
                DatabaseControl.executeUpdateQuery(DatabaseControl.parkTable, DatabaseControl.parkColumns, values,
                    "ParkID=" + parkId);
                DatabaseControl.deleteRecords(DatabaseControl.taxTable, "ParkID=@value0", new Object[] { parkId });
                for (int i = 0; i < 3; i++)
                {
                    char utilityType = ' ';
                    switch (i)
                    {
                        case 0: utilityType = 'G'; break;
                        case 1: utilityType = 'E'; break;
                        case 2: utilityType = 'W'; break;
                    }
                    Object[] taxValues = { ((CommonTools.Item)parkList.SelectedItem).Value, utilityType, taxRates.GetControlFromPosition(0, i).Text, taxRates.GetControlFromPosition(1, i).Text,
                                             taxRates.GetControlFromPosition(2, i).Text, taxRates.GetControlFromPosition(3, i).Text, taxRates.GetControlFromPosition(4, i).Text};
                    DatabaseControl.executeInsertQuery(DatabaseControl.taxTable, DatabaseControl.taxColumns, taxValues);
                }
                exportOptions(parkId);
                exportTemps(parkId);
            }
            else
            {
                int parkId = DatabaseControl.executeInsertQuery(DatabaseControl.parkTable, DatabaseControl.parkColumns, values);
                for (int i = 0; i < 3; i++) 
                {
                    char utilityType = ' ';
                    switch (i) {
                        case 0: utilityType = 'G'; break;
                        case 1: utilityType = 'E'; break;
                        case 2: utilityType = 'W'; break;
                    }
                    Object[] taxValues = { parkId, utilityType, taxRates.GetControlFromPosition(0, i).Text, taxRates.GetControlFromPosition(1, i).Text,
                                             taxRates.GetControlFromPosition(2, i).Text, taxRates.GetControlFromPosition(3, i).Text, taxRates.GetControlFromPosition(4, i).Text};
                    DatabaseControl.executeInsertQuery(DatabaseControl.taxTable, DatabaseControl.taxColumns, taxValues);
                }
                exportOptions(parkId);
                exportTemps(parkId);
            }
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void insertOption_Click(object sender, EventArgs e) {
            if (optionDesc.SelectedItem is CommonTools.Item) {
                int optId = ((CommonTools.Item)optionDesc.SelectedItem).Value;
                String description = ((CommonTools.Item)optionDesc.SelectedItem).Text;
                CommonTools.Option opt = new CommonTools.Option(description, optId);
                try { opt.charge = Convert.ToDecimal(optionCharge.Text); } catch { return; }
                if (options.Contains(opt)) options.Remove(opt); 
                options.Add(opt);
            } else {
                int optId = DatabaseControl.executeInsertQuery(DatabaseControl.optionsTable, new String[] { "ChargeItemDescription" }, new Object[] { optionDesc.Text });
                CommonTools.Option opt = new CommonTools.Option(optionDesc.Text, optId);
                try { opt.charge = Convert.ToDecimal(optionCharge.Text); } catch { return; }
                options.Add(opt);
                optionDesc.Items.Add(new CommonTools.Item(optId, optionDesc.Text));
            }
            refreshOptionList();
        }

        private void removeOption_Click(object sender, EventArgs e) {
            foreach (CommonTools.Option item in optionList.SelectedItems) {
                options.Remove(new CommonTools.Option(item.description, item.id));
            }
            refreshOptionList();
        }

        private void refreshOptionList() {
            optionList.Items.Clear();
            foreach (CommonTools.Option item in options) {
                optionList.Items.Add(item);
            }
        }

        private void refreshTempList() {
            tempList.Items.Clear();
            foreach (CommonTools.Temp item in temps) {
                tempList.Items.Add(item);
            }
        }

        private void importOptions(int parkId) {
            options = new List<CommonTools.Option>();
            String[] fields = { "ChargeItemDescription", "ChargeItem.ChargeItemID", "ChargeItemValue" };
            String table = DatabaseControl.parkOptionsTable + " JOIN " + DatabaseControl.optionsTable +
                " ON ChargeItem.ChargeItemID=ParkChargeItem.ChargeItemID";
            List<Object[]> opts = DatabaseControl.getMultipleRecord(fields, table, "ParkID=@value0", new Object[] { parkId });
            foreach (Object[] opt in opts) {
                options.Add(new CommonTools.Option(opt[0].ToString(), (int)opt[1], (decimal)opt[2]));
            }
            refreshOptionList();
        }

        private void exportOptions(int parkId) {
            DatabaseControl.deleteRecords(DatabaseControl.parkOptionsTable, "ParkID=@value0", new Object[] { parkId });
            foreach (CommonTools.Option opt in options) {
                Object[] values = { parkId, opt.id, opt.charge };
                DatabaseControl.executeInsertQuery(DatabaseControl.parkOptionsTable, DatabaseControl.parkOptionsColumns, values);
            }
        }

        private void removeTemp_Click(object sender, EventArgs e) {
            foreach (CommonTools.Temp item in tempList.SelectedItems) {
                temps.Remove(item);
            }
            refreshTempList();
        }

        private void insertTemp_Click(object sender, EventArgs e) {
            CommonTools.Temp opt = new CommonTools.Temp(tempDescription.Text, dateAssigned.Value.Date);
            try { opt.charge = Convert.ToDecimal(tempCharge.Text); } catch { MessageBox.Show("Invalid value for temporary charge."); return; }
            temps.Add(opt);
            tempList.Items.Add(new CommonTools.Item(0, tempDescription.Text));

            refreshTempList();
        }

        private void importTemps(int parkId) {
            temps = new List<CommonTools.Temp>();
            String[] fields = { "Description", "Charge", "DateAssigned" };
            List<Object[]> opts = DatabaseControl.getMultipleRecord(fields, DatabaseControl.tempChargeTable, 
                "ParkID=@value0 AND DateAssigned>=@value1", new Object[] { parkId, DateTime.Now.Date.AddDays(-7) });
            foreach (Object[] opt in opts) {
                CommonTools.Temp newTemp = new CommonTools.Temp(opt[0].ToString(), (DateTime)opt[2]);
                newTemp.charge = (decimal)opt[1];
                temps.Add(newTemp);
            }
            refreshTempList();
        }

        private void exportTemps(int parkId) {
            DatabaseControl.deleteRecords(DatabaseControl.tempChargeTable, "ParkID=@value0 AND DateAssigned>=@value1",
                new Object[] { parkId, DateTime.Now.Date.AddDays(-7) });
            foreach (CommonTools.Temp opt in temps) {
                Object[] values = { parkId, opt.description, opt.charge, opt.dateAssigned };
                DatabaseControl.executeInsertQuery(DatabaseControl.tempChargeTable, DatabaseControl.tempChargeColumns, values);
            }
        }
    }
}
