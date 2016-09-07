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
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();
            securityList.Items.Add(new CommonTools.Item(0, "Admin"));
            securityList.Items.Add(new CommonTools.Item(1, "User"));
        }

        private void NewUser_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseControl.executeInsertQuery(DatabaseControl.userTable, new string[] {  "LoginName", "Password", "Security" },
                new object[] { userIDTextBox.Text, passwordTextBox.Text, ((CommonTools.Item)securityList.SelectedItem).Value });
                MessageBox.Show("New user ID / password " + userIDTextBox.Text + "/" + passwordTextBox.Text +  " is created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("User creation failed ! " + ex);
            }

            userIDTextBox.Text = "";
            passwordTextBox.Text = "";
            securityList.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
