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
        public decimal[][] allowanceRates;
        public decimal medicalRate;
        public bool committed = false;

        public UtilityElectricity1(decimal[][] allowance, decimal medical)
        {
            InitializeComponent();
            this.allowanceRates = allowance;
            this.medicalRate = medical;
        }

        private void UtilityElectricity1_Load(object sender, EventArgs e)
        {
            displayData();
            medical.Text = medicalRate.ToString();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveAllowance();
            medicalRate = Convert.ToDecimal(medical.Text);
            committed = true;
            this.Close();
        }

        private void saveAllowance()
        {
            decimal[][] rates = new decimal[2][];
            rates[0] = new decimal[10];
            rates[1] = new decimal[10];
            try {
                for (int i = 0; i < allowance1.RowCount; i++) {
                    rates[0][i] = Convert.ToDecimal(allowance1.GetControlFromPosition(0, i).Text);
                }

                for (int i = 0; i < allowance2.RowCount; i++) {
                    rates[1][i] = Convert.ToDecimal(allowance2.GetControlFromPosition(0, i).Text);
                }
            } catch {
                MessageBox.Show("Invalid value for allowance rate.");
                return;
            }
            allowanceRates = rates;
        }

        private void displayData()
        {
            for (int i = 0; i < 10; i++)
            {
                allowance1.GetControlFromPosition(0, i).Text = allowanceRates[0][i].ToString();
                allowance2.GetControlFromPosition(0, i).Text = allowanceRates[1][i].ToString();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
