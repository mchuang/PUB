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
    public partial class ReadHistory : Form {
        int parkId;
        String spaceId;
        DataTable data;
        int eleRead, gasRead, watRead;
        public ReadHistory(int parkId, int parkSpaceId) {
            InitializeComponent();
            this.parkId = parkId;
            this.spaceId = DatabaseControl.getSingleRecord(new String[] { "SpaceID" }, DatabaseControl.spaceTable, "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0].ToString();
        }

        private void ReadHistory_Load(object sender, EventArgs e) {
            String fields = "MeterReadID, MeterReadDate, GasReadValue, EleReadValue, WatReadValue";
            String condition = "ParkId=@value0 AND SpaceID=@value1";
            data = new DataTable();
            DatabaseControl.populateDataTable(ref data, fields, DatabaseControl.meterReadsTable, condition, new Object[] { parkId, spaceId });
            DataRow present = data.Rows[(data.Rows.Count - 1)];
            eleRead = (int)present["EleReadValue"];
            gasRead = (int)present["GasReadValue"];
            watRead = (int)present["WatReadValue"];

            createHistoryTable();
            gasBtn_Click(sender, e);
        }

        private void createHistoryTable() {
            historyTable.Columns.Clear();
            DataGridViewTextBoxColumn readDate = new DataGridViewTextBoxColumn();
            {
                readDate.HeaderText = "Read Date";
                readDate.Name = "readDate";
                readDate.Width = 80;
            }
            DataGridViewTextBoxColumn meterRead = new DataGridViewTextBoxColumn();
            {
                meterRead.HeaderText = "New Read";
                meterRead.Name = "meterRead";
                meterRead.Width = 80;
            }
            DataGridViewTextBoxColumn days = new DataGridViewTextBoxColumn();
            {
                days.HeaderText = "Days";
                days.Name = "days";
                days.Width = 80;
            }
            DataGridViewTextBoxColumn usage = new DataGridViewTextBoxColumn();
            {
                usage.HeaderText = "Usage";
                usage.Name = "usage";
                usage.Width = 80;
            }
            DataGridViewTextBoxColumn avg = new DataGridViewTextBoxColumn();
            {
                avg.HeaderText = "Average Usage";
                avg.Name = "avg";
                avg.Width = 80;
            }
            historyTable.Columns.Add(readDate);
            historyTable.Columns.Add(meterRead);
            historyTable.Columns.Add(days);
            historyTable.Columns.Add(usage);
            historyTable.Columns.Add(avg);
        }

        private void gasBtn_Click(object sender, EventArgs e) {
            ReadHistory.ActiveForm.Text = "Gas History";
            historyTable.Rows.Clear();
            for (int i = 0; i < data.Rows.Count; i++) {
                DataRow row = data.Rows[i];
                historyTable.Rows.Add(1);
                historyTable.Rows[i].Cells["readDate"].Value = ((DateTime)row["MeterReadDate"]).ToString("d"); ;
                historyTable.Rows[i].Cells["meterRead"].Value = row["GasReadValue"];
                if (i > 0) {
                    int usage = (int)row["GasReadValue"] - (int)data.Rows[i - 1]["GasReadValue"];
                    double days = ((DateTime)row["MeterReadDate"] - (DateTime)data.Rows[i - 1]["MeterReadDate"]).TotalDays;
                    historyTable.Rows[i].Cells["usage"].Value = (int)row["GasReadValue"] - (int)data.Rows[i - 1]["GasReadValue"];
                    historyTable.Rows[i].Cells["days"].Value = ((DateTime)row["MeterReadDate"] - (DateTime)data.Rows[i - 1]["MeterReadDate"]).TotalDays;
                    historyTable.Rows[i].Cells["avg"].Value = usage / days;
                }
            }
            currentRead.Text = gasRead.ToString();
        }

        private void eleBtn_Click(object sender, EventArgs e) {
            ReadHistory.ActiveForm.Text = "Electric History";
            historyTable.Rows.Clear();
            for (int i = 0; i < data.Rows.Count; i++) {
                DataRow row = data.Rows[i];
                historyTable.Rows.Add(1);
                historyTable.Rows[i].Cells["readDate"].Value = ((DateTime)row["MeterReadDate"]).ToString("d"); ;
                historyTable.Rows[i].Cells["meterRead"].Value = row["EleReadValue"];
                if (i > 0) {
                    int usage = (int)row["EleReadValue"] - (int)data.Rows[i - 1]["EleReadValue"];
                    double days = ((DateTime)row["MeterReadDate"] - (DateTime)data.Rows[i - 1]["MeterReadDate"]).TotalDays;
                    historyTable.Rows[i].Cells["usage"].Value = (int)row["EleReadValue"] - (int)data.Rows[i - 1]["EleReadValue"];
                    historyTable.Rows[i].Cells["days"].Value = ((DateTime)row["MeterReadDate"] - (DateTime)data.Rows[i - 1]["MeterReadDate"]).TotalDays;
                    historyTable.Rows[i].Cells["avg"].Value = usage / days;
                }
            }
            currentRead.Text = eleRead.ToString();
        }

        private void watBtn_Click(object sender, EventArgs e) {
            ReadHistory.ActiveForm.Text = "Water History";
            historyTable.Rows.Clear();
            for (int i = 0; i < data.Rows.Count; i++) {
                DataRow row = data.Rows[i];
                historyTable.Rows.Add(1);
                historyTable.Rows[i].Cells["readDate"].Value = ((DateTime)row["MeterReadDate"]).ToString("d"); ;
                historyTable.Rows[i].Cells["meterRead"].Value = row["WatReadValue"];
                if (i > 0) {
                    int usage = (int)row["WatReadValue"] - (int)data.Rows[i - 1]["WatReadValue"];
                    double days = ((DateTime)row["MeterReadDate"] - (DateTime)data.Rows[i - 1]["MeterReadDate"]).TotalDays;
                    historyTable.Rows[i].Cells["usage"].Value = (int)row["WatReadValue"] - (int)data.Rows[i - 1]["WatReadValue"];
                    historyTable.Rows[i].Cells["days"].Value = ((DateTime)row["MeterReadDate"] - (DateTime)data.Rows[i - 1]["MeterReadDate"]).TotalDays;
                    historyTable.Rows[i].Cells["avg"].Value = usage / days;
                }
            }
            currentRead.Text = watRead.ToString();
        }

        private void currentRead_TextChanged(object sender, EventArgs e) {
            switch (ReadHistory.ActiveForm.Text) {
                case "Water History": watRead = Convert.ToInt32(currentRead.Text); break;
                case "Gas History": gasRead = Convert.ToInt32(currentRead.Text); break;
                case "Electric History": eleRead = Convert.ToInt32(currentRead.Text); break;
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
