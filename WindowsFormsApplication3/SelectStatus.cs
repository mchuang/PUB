using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUB {
    public partial class SelectStatus : Form {
        int parkId;
        public List<String> eleList, gasList;

        public SelectStatus(int parkId) {
            InitializeComponent();
            this.parkId = parkId;
        }

        private void SelectStatus_Load(object sender, EventArgs e) {
            Object[] utilCompany = DatabaseControl.getSingleRecord(new String[] { "GasCompanyID", "ElectricityCompanyID" }, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { parkId });
            Object[] gasRate = DatabaseControl.getSingleRecord(new String[] { "UtilityRateID" }, DatabaseControl.utilRateTable, "UtilityCompanyID=@value0 AND DIRTY = 0 ORDER BY EffectiveDate DESC", new Object[] { utilCompany[0] });
            Object[] eleRate = DatabaseControl.getSingleRecord(new String[] { "UtilityRateID" }, DatabaseControl.utilRateTable, "UtilityCompanyID=@value0 AND DIRTY = 0 ORDER BY EffectiveDate DESC", new Object[] { utilCompany[1] });
            List<Object[]> gasStatus = DatabaseControl.getMultipleRecord(new String[] { "DISTINCT RateType" }, DatabaseControl.utilTierRatesTable, "UtilityRateID=@value0", gasRate);
            List<Object[]> eleStatus = DatabaseControl.getMultipleRecord(new String[] { "DISTINCT RateType" }, DatabaseControl.utilTierRatesTable, "UtilityRateID=@value0", eleRate);
            foreach (Object[] status in gasStatus) {
                CheckBox box = new CheckBox();
                box.Text = status[0].ToString();
                box.Visible = true;
                gasPanel.Controls.Add(box);
            }
            foreach (Object[] status in eleStatus) {
                CheckBox box = new CheckBox();
                box.Text = status[0].ToString();
                box.Visible = true;
                elePanel.Controls.Add(box);
            }
        }

        private void SelectStatus_FormClosing(object sender, FormClosingEventArgs e) {
            eleList = new List<String>();
            gasList = new List<String>();
            foreach (CheckBox box in gasPanel.Controls) {
                if (box.Checked) gasList.Add(box.Text);
            }
            foreach (CheckBox box in elePanel.Controls) {
                if (box.Checked) eleList.Add(box.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
