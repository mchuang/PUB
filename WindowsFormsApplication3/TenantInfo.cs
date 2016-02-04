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
    public partial class TenantInfo : Form
    {
        public TenantInfo()
        {
            InitializeComponent();
        }

        private void TenantInfo_Load(object sender, EventArgs e)
        {
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkName", "ParkID");
            tenantList.Enabled = false;
            //DatabaseControl.populateComboBox(ref tenantList, DatabaseControl.spaceTable, "Name", "ParkSpaceID");
        }

        private void tenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonTools.Item tenant = (CommonTools.Item)tenantList.SelectedItem;
            String condition = "ParkSpaceID=@value0";
            String[] fields = { "Tenant", "Email", "PhoneOffice", "PhoneHome", "PhoneCellular", "Address1", "Address2", "City", "State", "Zip",
                                "BillingAddress1", "BillingAddress2", "BillingCity", "BillingState", "BillingZip"};
            Object[] values = DatabaseControl.getSingleRecord(fields, DatabaseControl.spaceTable, condition, new Object[] { tenant.Value });
            nameLbl.Text = values[0].ToString();
            emailLbl.Text = values[1].ToString();
            phoneOfficeLbl.Text = values[2].ToString();
            phoneHomeLbl.Text = values[3].ToString();
            phoneMobileLbl.Text = values[4].ToString();
            addrLbl0.Text = values[5].ToString();
            addrLbl1.Text = values[6].ToString();
            cityLbl.Text = values[7].ToString();
            stateLbl.Text = values[8].ToString();
            zipLbl.Text = values[9].ToString();
            billAddrLbl0.Text = values[10].ToString();
            billAddrLbl1.Text = values[11].ToString();
            billCityLbl.Text = values[12].ToString();
            billStateLbl.Text = values[13].ToString();
            billZipLbl.Text = values[14].ToString();
        }

        private void sameAsStreet_CheckedChanged(object sender, EventArgs e)
        {
            if (sameAsStreet.Checked)
            {
                billAddrLbl0.Text = addrLbl0.Text;
                billAddrLbl1.Text = addrLbl1.Text;
                billCityLbl.Text = cityLbl.Text;
                billStateLbl.Text = stateLbl.Text;
                billZipLbl.Text = zipLbl.Text;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            String tenantName;
            if (tenantList.SelectedItem is CommonTools.Item) { tenantName = ((CommonTools.Item)tenantList.SelectedItem).Text; }
            else { tenantName = tenantList.Text; }

            String[] values = {tenantName, emailLbl.Text, phoneOfficeLbl.Text, phoneHomeLbl.Text, phoneMobileLbl.Text,
                addrLbl0.Text, addrLbl1.Text, cityLbl.Text, stateLbl.Text, zipLbl.Text,
                billAddrLbl0.Text, billAddrLbl0.Text, billCityLbl.Text, billStateLbl.Text, billZipLbl.Text};
            String[] fields = { "Tenant", "Email", "PhoneOffice", "PhoneHome", "PhoneCellular", "Address1", "Address2", "City", "State", "Zip",
                                "BillingAddress1", "BillingAddress2", "BillingCity", "BillingState", "BillingZip"};

            if (tenantList.SelectedItem is CommonTools.Item)
            {
                DatabaseControl.executeUpdateQuery(DatabaseControl.spaceTable, fields, values,
                    "ParkSpaceID=" + ((CommonTools.Item)tenantList.SelectedItem).Value.ToString());
            }
            else
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.spaceTable, fields, values);
            }
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int parkID = ((CommonTools.Item)parkList.SelectedItem).Value;
            tenantList.Enabled = true;
            DatabaseControl.populateComboBox(ref tenantList, DatabaseControl.spaceTable, "ParkSpaceID", "SpaceID", "Tenant", "ParkID=@value0", new Object[] { parkID });
        }
    }
}
