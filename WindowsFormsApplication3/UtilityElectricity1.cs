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
    public partial class UtilityElectricity1 : Form
    {
        public decimal[][] summerAllowance, winterAllowance;
        public decimal medicalRate;
        public bool committed = false;

        public UtilityElectricity1(ref decimal[][] summer, ref decimal[][] winter, ref decimal medical, bool hasWinter)
        {
            InitializeComponent();
            summerAllowance = summer;
            winterAllowance = winter;
            medicalRate = medical;
            if (hasWinter) { seasonBox.Enabled = true; }
            else { seasonBox.Enabled = false; }
        }

        private void UtilityElectricity1_Load(object sender, EventArgs e)
        {
            seasonBox.Text = "Summer";
            seasonBox.Items.Add(new CommonTools.Season("Summer"));
            seasonBox.Items.Add(new CommonTools.Season("Winter"));
            displayData(this.summerAllowance);
            medical.Text = medicalRate.ToString();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (seasonBox.Text == "Summer" || seasonBox.Enabled == false) { saveAllowance(ref summerAllowance); }
            else if (seasonBox.Text == "Winter") { saveAllowance(ref winterAllowance); }
            medicalRate = Convert.ToDecimal(medical.Text);
            committed = true;
            this.Close();
        }

        private void saveAllowance(ref decimal[][] rates)
        {
            rates = new decimal[2][];
            rates[0] = new decimal[10];
            rates[1] = new decimal[10];
            for (int i = 0; i < allowance1.RowCount; i++)
            {
                rates[0][i] = Convert.ToDecimal(allowance1.GetControlFromPosition(0, i).Text);
            }

            for (int i = 0; i < allowance2.RowCount; i++)
            {
                rates[1][i] = Convert.ToDecimal(allowance2.GetControlFromPosition(0, i).Text);
            }
        }

        private void displayData(decimal[][] allowance)
        {
            for (int i = 0; i < 10; i++)
            {
                allowance1.GetControlFromPosition(0, i).Text = allowance[0][i].ToString();
                allowance2.GetControlFromPosition(0, i).Text = allowance[1][i].ToString();
            }
        }

        private void seasonBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (seasonBox.Text == "Summer") { saveAllowance(ref winterAllowance); displayData(summerAllowance); }
            else if (seasonBox.Text == "Winter") { saveAllowance(ref summerAllowance); displayData(winterAllowance); }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
