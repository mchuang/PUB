using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUB {
    public partial class SelectDate : Form {
        int parkId;
        public DateTime readDateSelected;
        public SelectDate(int parkId) {
            InitializeComponent();
            this.parkId = parkId;
        }

        private void SelectDate_Load(object sender, EventArgs e) {
            List<Object[]> items = DatabaseControl.getMultipleRecord(new String[] { "DueDate" }, DatabaseControl.periodTable,
                "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId });
            foreach (Object[] item in items) readDate.Items.Add(item[0]);
        }

        private void readDate_SelectedIndexChanged(object sender, EventArgs e) {
            this.readDateSelected = Convert.ToDateTime(readDate.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
