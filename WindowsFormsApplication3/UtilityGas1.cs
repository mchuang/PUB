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
    public partial class UtilityGas1 : Form
    {
        public decimal[][] summerAllowance, winterAllowance;
        public decimal medicalAllowance;
        public bool committed = false;

        public UtilityGas1(ref decimal[][] summer, ref decimal[][] winter, ref decimal medical)
        {
            InitializeComponent();
            summerAllowance = summer;
            winterAllowance = winter;
            medicalAllowance = medical;
        }

        private void UtilityGas1_Load(object sender, EventArgs e)
        {
            seasonBox.Text = "Summer";
            seasonBox.Items.Add("Summer");
            seasonBox.Items.Add("Winter");
            displayData(summerAllowance);
            medicalBox.Text = medicalAllowance.ToString();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (seasonBox.Text == "Summer") { saveData(ref summerAllowance); }
            else if (seasonBox.Text == "Winter") { saveData(ref winterAllowance); }
            medicalAllowance = Convert.ToDecimal(medicalBox.Text);
            committed = true;
            this.Close();
        }

        private void displayData(decimal[][] data)
        {
            for (int i = 0; i < allowance.RowCount; i++)
            {
                for (int j = 0; j < allowance.ColumnCount; j++)
                {
                    allowance.GetControlFromPosition(j, i).Text = data[j][i].ToString();
                }
            }
        }

        private void saveData(ref decimal[][] data)
        {
            for (int i = 0; i < allowance.RowCount; i++)
            {
                for (int j = 0; j < allowance.ColumnCount; j++)
                {
                     data[j][i] = Convert.ToDecimal(allowance.GetControlFromPosition(j, i).Text);
                }
            }
        }

        private void seasonBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (seasonBox.Text == "Summer")
            {
                saveData(ref this.winterAllowance);
                displayData(summerAllowance);
            }
            else if (seasonBox.Text == "Winter")
            {
                saveData(ref this.summerAllowance);
                displayData(winterAllowance);
            }
        }
    }
}
