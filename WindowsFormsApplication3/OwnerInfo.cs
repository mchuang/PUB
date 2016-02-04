using System;
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
    public partial class OwnerInfo : Form
    {

        public OwnerInfo()
        {
            InitializeComponent();
        }

        private void OwnerInfo_Load(object sender, EventArgs e)
        {
            DatabaseControl.populateComboBox(ref ownerList, DatabaseControl.ownerTable, "CompanyName", "OwnerID");
        }

        private void ownerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ownerList.SelectedIndex = ownerList.FindString(ownerList.Text);
            if (ownerList.SelectedIndex == -1)
            {
                MessageBox.Show("If choosing an existing owner, please enter a valid owner.");
                return;
            }
            CommonTools.Item owner = (CommonTools.Item) ownerList.SelectedItem;
            Object[] values = DatabaseControl.getSingleRecord(DatabaseControl.ownerColumns, DatabaseControl.ownerTable, "OwnerID=" + owner.Value);
            contactLbl.Text = values[1].ToString();
            emailLbl.Text = values[2].ToString();
            phoneOfficeLbl.Text = values[3].ToString();
            phoneHomeLbl.Text = values[4].ToString();
            phoneMobileLbl.Text = values[5].ToString();
            addrLbl0.Text = values[6].ToString();
            addrLbl1.Text = values[7].ToString();
            cityLbl.Text = values[8].ToString();
            stateLbl.Text = values[9].ToString();
            zipLbl.Text = values[10].ToString();
            billAddrLbl0.Text = values[11].ToString();
            billAddrLbl1.Text = values[12].ToString();
            billCityLbl.Text = values[13].ToString();
            billStateLbl.Text = values[14].ToString();
            billZipLbl.Text = values[15].ToString();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            String ownerName;
            if (ownerList.SelectedItem is CommonTools.Item) {ownerName = ((CommonTools.Item)ownerList.SelectedItem).Text;}
            else{ownerName = ownerList.Text;}

            String[] values = {ownerName, contactLbl.Text, emailLbl.Text, phoneOfficeLbl.Text, phoneHomeLbl.Text, phoneMobileLbl.Text,
                addrLbl0.Text, addrLbl1.Text, cityLbl.Text, stateLbl.Text, zipLbl.Text,
                billAddrLbl0.Text, billAddrLbl1.Text, billCityLbl.Text, billStateLbl.Text, billZipLbl.Text};

            if (ownerList.SelectedItem is CommonTools.Item)
            {
                DatabaseControl.executeUpdateQuery(DatabaseControl.ownerTable, DatabaseControl.ownerColumns, values,
                    "OwnerID="+((CommonTools.Item)ownerList.SelectedItem).Value.ToString());
            }
            else
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.ownerTable, DatabaseControl.ownerColumns, values);
            }
            this.Close();
        }

        private void sameAddr_CheckedChanged(object sender, EventArgs e)
        {
            if (sameAddr.Checked)
            {
                billAddrLbl0.Text = addrLbl0.Text;
                billAddrLbl1.Text = addrLbl1.Text;
                billCityLbl.Text = cityLbl.Text;
                billStateLbl.Text = stateLbl.Text;
                billZipLbl.Text = zipLbl.Text;
            }
        }
    }
}
