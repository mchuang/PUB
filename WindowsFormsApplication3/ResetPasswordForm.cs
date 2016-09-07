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
    public partial class ResetPasswordForm : Form
    {
        int uID = 1;
        int security = -1;
        public ResetPasswordForm()
        {
            InitializeComponent();
            populateUserComboBox();
        }

        public ResetPasswordForm(int userId, int security) {
            InitializeComponent();
            this.security = security;
            this.uID = userId;
            populateUserComboBox();
            userIDComboBox.Text = CommonTools.Item.getString(ref userIDComboBox, userId);
            userIDComboBox.Enabled = false;
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

        private void resetPassword_Click(object sender, EventArgs e)
        {
            DatabaseControl.executeUpdateQuery(
                DatabaseControl.userTable,
                new string[] { "LoginName", "Password",  },
                new object[] { userIDComboBox.Text, passwordTextBox.Text },
                "userLoginID=" + uID);
            populateUserComboBox();
            MessageBox.Show("The Password is updated successfully!");
        }
    }
}
