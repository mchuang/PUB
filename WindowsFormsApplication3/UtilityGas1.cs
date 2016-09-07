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
        public decimal[][] allowanceRates;
        public decimal medicalAllowance;
        public bool committed = false;

        public UtilityGas1(decimal[][] allowance, decimal medical)
        {
            InitializeComponent();
            this.allowanceRates = allowance;
            this.medicalAllowance = medical;
        }

        private void UtilityGas1_Load(object sender, EventArgs e)
        {
            displayData();
            medicalBox.Text = medicalAllowance.ToString();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveData();
            medicalAllowance = Convert.ToDecimal(medicalBox.Text);
            committed = true;
            this.Close();
        }

        private void displayData()
        {
            for (int i = 0; i < allowance.RowCount; i++)
            {
                for (int j = 0; j < allowance.ColumnCount; j++)
                {
                    allowance.GetControlFromPosition(j, i).Text = allowanceRates[j][i].ToString();
                }
            }
        }

        private void saveData()
        {
            decimal[][] rates = new decimal[3][];
            rates[0] = new decimal[10];
            rates[1] = new decimal[10];
            rates[2] = new decimal[10];
            try {
                for (int i = 0; i < allowance.RowCount; i++) {
                    for (int j = 0; j < allowance.ColumnCount; j++) {
                        rates[j][i] = Convert.ToDecimal(allowance.GetControlFromPosition(j, i).Text);
                    }
                }
            } catch {
                MessageBox.Show("Invalid value for allowance rate.");
                return;
            }
            allowanceRates = rates;
        }
    }
}
