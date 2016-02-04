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
        public ParkInfo()
        {
            InitializeComponent();
        }

        private void ParkInfo_Load(object sender, EventArgs e)
        {
            options = new List<CommonTools.Option>();
            DatabaseControl.populateComboBox(ref optionDesc, DatabaseControl.optionsTable, "ChargeItemDescription", "ChargeItemID");
            DatabaseControl.populateComboBox(ref parkNumberList, DatabaseControl.parkTable, "ParkNumber", "ParkID");
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
        }

        private void fillParkInfo(int parkID)
        {
            Object[] values = DatabaseControl.getSingleRecord(DatabaseControl.parkColumns, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { parkID });
            parkNumberList.SelectedIndex = parkNumberList.FindStringExact(values[0].ToString());
            parkList.SelectedIndex = parkList.FindStringExact(values[1].ToString());
            addrLbl.Text = values[2].ToString();
            cityLbl.Text = values[3].ToString();
            stateLbl.Text = values[4].ToString();
            zipLbl.Text = values[5].ToString();
            phoneLbl.Text = values[6].ToString();
            faxLbl.Text = values[7].ToString();

            eleZone.Text = values[10].ToString();
            gasZone.Text = values[12].ToString();

            if (values[8] != DBNull.Value)
            {
                ownerList.SelectedIndex = ownerList.FindStringExact(DatabaseControl.getSingleRecord(new String[] { "CompanyName" },
                    DatabaseControl.ownerTable, "OwnerID=@value0", new Object[] { (int)values[8] })[0].ToString());
            }
            if (values[9] != DBNull.Value)
            {
                eleList.SelectedIndex = eleList.FindStringExact(DatabaseControl.getSingleRecord(new String[] { "CompanyName" },
                    DatabaseControl.utilCompanyTable, "UtilityCompanyID=@value0", new Object[] { (int)values[9] })[0].ToString());
            }
            if (values[11] != DBNull.Value)
            {
                gasList.SelectedIndex = gasList.FindStringExact(DatabaseControl.getSingleRecord(new String[] { "CompanyName" },
                    DatabaseControl.utilCompanyTable, "UtilityCompanyID=@value0", new Object[] { (int)values[11] })[0].ToString());
            }
            if (values[13] != DBNull.Value)
            {
                watList.SelectedIndex = watList.FindStringExact(DatabaseControl.getSingleRecord(new String[] { "CompanyName" },
                    DatabaseControl.utilCompanyTable, "UtilityCompanyID=@value0", new Object[] { (int)values[13] })[0].ToString());
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
                    "ParkID=@value0 AND UtilityType=@value1 ORDER BY TaxRateID DESC", taxValues);
                for (int j = 0; j < 5; j++)
                {
                    taxRates.GetControlFromPosition(j, i).Text = taxes[j].ToString();
                }
            }

            options = new List<CommonTools.Option>();
            importOptions(parkID);
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
                                owner, ele, eleZone.Text, gas, gasZone.Text, wat };

            if (parkList.SelectedItem is CommonTools.Item)
            {
                int parkId=((CommonTools.Item)parkList.SelectedItem).Value;
                DatabaseControl.executeUpdateQuery(DatabaseControl.parkTable, DatabaseControl.parkColumns, values,
                    "ParkID=" + parkId);
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
                try { opt.charge = Convert.ToDecimal(optionCharge.Text); } catch { }
                if (options.Contains(opt)) options.Remove(opt); 
                options.Add(opt);
            } else {
                int optId = DatabaseControl.executeInsertQuery(DatabaseControl.optionsTable, new String[] { "ChargeItemDescription" }, new Object[] { optionDesc.Text });
                CommonTools.Option opt = new CommonTools.Option(optionDesc.Text, optId);
                try { opt.charge = Convert.ToDecimal(asdlf.Text); } catch { }
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

        private void importOptions(int parkId) {
            options = new List<CommonTools.Option>();
            String[] fields = { "ChargeItemDescription", "ChargeItem.ChargeItemID", "ChargeItemValue" };
            String table = DatabaseControl.parkOptionsTable + " JOIN " + DatabaseControl.optionsTable +
                " ON ChargeItem.ChargeItemID=ParkChargeItem.ChargeItemID";
            ArrayList opts = DatabaseControl.getMultipleRecord(fields, table, "ParkID=@value0", new Object[] { parkId });
            foreach (Object[] opt in opts) {
                options.Add(new CommonTools.Option(opt[0].ToString(), (int)opt[1], (decimal)opt[2]));
            }
            refreshOptionList();
        }

        private void exportOptions(int parkId) {
            foreach (CommonTools.Option opt in options) {
                Object[] values = { parkId, opt.id, opt.charge };
                DatabaseControl.executeInsertQuery(DatabaseControl.parkOptionsTable, DatabaseControl.parkOptionsColumns, values);
            }
        }
    }
}
