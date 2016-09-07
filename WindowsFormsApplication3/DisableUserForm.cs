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
    public partial class DisableUserForm : Form
    {
        int uID = 1;
        public DisableUserForm()
        {
            InitializeComponent();
            populateUserComboBox();
        }

        private void disableUser_Click(object sender, EventArgs e)
        {
            DatabaseControl.executeUpdateQuery(
                DatabaseControl.userTable,
                new string[] { "LoginName", "Password", "Active" },
                new object[] { userIDComboBox.Text, "xyz", "N" },
                "userLoginID=" + uID);
            populateUserComboBox();
            MessageBox.Show("The userID is disableded successfully!");
            

        }
        private void populateUserComboBox()
        {
            userIDComboBox.Items.Clear();
            DatabaseControl.populateComboBox(ref userIDComboBox, DatabaseControl.userTable, "LoginName", "userLoginID",
                "Active =@value0", new Object[] { "Y" });
            userIDComboBox.Enabled = true;
            userIDComboBox.Focus(); 
        }

        private void userIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            uID = ((CommonTools.Item)userIDComboBox.SelectedItem).Value;
        }
    }
}
