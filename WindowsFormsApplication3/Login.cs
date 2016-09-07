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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (DatabaseControl.userLogin(username.Text, password.Text))
            {
                this.Hide();
                Object[] info = DatabaseControl.getSingleRecord(new String[] { "UserLoginID", "Security" }, DatabaseControl.userTable, "LoginName=@value0 AND Password=@value1",
                    new Object[] { username.Text, password.Text });
                new MainMenu((int)info[0], (int)info[1]).ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials or UserID has been disabled!");
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e) {

        }
    }
        
}
