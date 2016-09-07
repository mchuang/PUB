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
        int orderId;
        DataTable data;
        DataRow current, previous;
        List<Object[]> meterReads;
        public ReadHistory(int parkId, int parkSpaceId) {
            InitializeComponent();
            this.parkId = parkId;
            this.orderId = (int)DatabaseControl.getSingleRecord(new String[] { "OrderID" }, DatabaseControl.spaceTable, "ParkSpaceID=@value0", new Object[] { parkSpaceId })[0];
        }

        private void ReadHistory_Load(object sender, EventArgs e) {
            String fields = "MeterReadID, StartDate, MeterReadDate, GasReadValue, EleReadValue, WatReadValue, MeterReadEmployeeID, MeterReadTime, DueDate, ThermX";
            String condition = "ParkId=@value0 AND OrderID=@value1 ORDER BY DueDate ASC";
            data = new DataTable();
            DatabaseControl.populateDataTable(ref data, fields, DatabaseControl.meterReadsTable, condition, new Object[] { parkId, orderId });
            current = data.Rows[(data.Rows.Count - 1)];
            previous = data.Rows[(data.Rows.Count - 2)];
            currStartDate.Value = (DateTime)current["StartDate"];
            currReadDate.Value = (DateTime)current["MeterReadDate"];
            currGas.Text = current["GasReadValue"].ToString();
            currEle.Text = current["EleReadValue"].ToString();
            currWat.Text = current["WatReadValue"].ToString();
            prevStartDate.Value = (DateTime)previous["StartDate"];
            prevReadDate.Value = (DateTime)previous["MeterReadDate"];
            prevGas.Text = previous["GasReadValue"].ToString();
            prevEle.Text = previous["EleReadValue"].ToString();
            prevWat.Text = previous["WatReadValue"].ToString();
            historyTable.DataSource = data;
            historyTable.Columns.Add("gasUsage", "Gas Usage");
            historyTable.Columns.Add("eleUsage", "Ele Usage");
            historyTable.Columns.Add("watUsage", "Wat Usage");
            historyTable.Columns["MeterReadEmployeeID"].Visible = false;
            historyTable.Columns["MeterReadTime"].Visible = false;
            historyTable.AllowUserToAddRows = false;
            for (int i = 1; i < historyTable.Rows.Count; i++ ) {
                DataGridViewRow row1 = historyTable.Rows[i - 1];
                DataGridViewRow row2 = historyTable.Rows[i];
                row2.Cells["gasUsage"].Value = (int)row2.Cells["GasReadValue"].Value - (int)row1.Cells["GasReadValue"].Value;
                row2.Cells["eleUsage"].Value = (int)row2.Cells["EleReadValue"].Value - (int)row1.Cells["EleReadValue"].Value;
                row2.Cells["watUsage"].Value = (int)row2.Cells["WatReadValue"].Value - (int)row1.Cells["WatReadValue"].Value; 
            }
            foreach (DataGridViewColumn col in historyTable.Columns) {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void createHistoryTable() {
            historyTable.Columns.Clear();
            DataGridViewTextBoxColumn startDate = new DataGridViewTextBoxColumn();
            {
                startDate.HeaderText = "Start Date";
                startDate.Name = "startDate";
                startDate.Width = 80;
            }
            DataGridViewTextBoxColumn readDate = new DataGridViewTextBoxColumn();
            {
                readDate.HeaderText = "Read Date";
                readDate.Name = "readDate";
                readDate.Width = 80;
            }
            DataGridViewTextBoxColumn gasRead = new DataGridViewTextBoxColumn();
            {
                gasRead.HeaderText = "Gas Read";
                gasRead.Name = "gasRead";
                gasRead.Width = 80;
            }
            DataGridViewTextBoxColumn eleRead = new DataGridViewTextBoxColumn();
            {
                eleRead.HeaderText = "Ele Read";
                eleRead.Name = "eleRead";
                eleRead.Width = 80;
            }
            DataGridViewTextBoxColumn watRead = new DataGridViewTextBoxColumn();
            {
                watRead.HeaderText = "Wat Read";
                watRead.Name = "watRead";
                watRead.Width = 80;
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
            historyTable.Columns.Add(startDate);
            historyTable.Columns.Add(readDate);
            historyTable.Columns.Add(gasRead);
            historyTable.Columns.Add(eleRead);
            historyTable.Columns.Add(watRead);
            historyTable.Columns.Add(days);
            historyTable.Columns.Add(usage);
            historyTable.Columns.Add(avg);
            historyTable.AllowUserToAddRows = false;
            foreach (DataGridViewColumn col in historyTable.Columns) {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void gasBtn_Click(object sender, EventArgs e) {
            ReadHistory.ActiveForm.Text = "Gas History";
            historyTable.Rows.Clear();
            for (int i = 0; i < data.Rows.Count; i++) {
                DataRow row = data.Rows[i];
                historyTable.Rows.Add(1);
                historyTable.Rows[i].Cells["startDate"].Value = ((DateTime)row["StartDate"]).ToString("d");
                historyTable.Rows[i].Cells["readDate"].Value = ((DateTime)row["MeterReadDate"]).ToString("d");
                historyTable.Rows[i].Cells["meterRead"].Value = row["GasReadValue"];
                if (i > 0) {
                    int usage = (int)data.Rows[i]["GasReadValue"] - (int)data.Rows[i - 1]["GasReadValue"];
                    double days = ((DateTime)data.Rows[i]["MeterReadDate"] - (DateTime)data.Rows[i]["StartDate"]).TotalDays;
                    historyTable.Rows[i].Cells["usage"].Value = usage;
                    historyTable.Rows[i].Cells["days"].Value = days;
                    historyTable.Rows[i].Cells["avg"].Value = usage / days;
                }
            }
        }

        private void eleBtn_Click(object sender, EventArgs e) {
            ReadHistory.ActiveForm.Text = "Electric History";
            historyTable.Rows.Clear();
            for (int i = 0; i < data.Rows.Count; i++) {
                DataRow row = data.Rows[i];
                historyTable.Rows.Add(1);
                historyTable.Rows[i].Cells["startDate"].Value = ((DateTime)row["StartDate"]).ToString("d");
                historyTable.Rows[i].Cells["readDate"].Value = ((DateTime)row["MeterReadDate"]).ToString("d"); ;
                historyTable.Rows[i].Cells["meterRead"].Value = row["EleReadValue"];
                if (i > 0) {
                    int usage = (int)data.Rows[i]["EleReadValue"] - (int)data.Rows[i - 1]["EleReadValue"];
                    double days = ((DateTime)data.Rows[i]["MeterReadDate"] - (DateTime)data.Rows[i]["StartDate"]).TotalDays;
                    historyTable.Rows[i].Cells["usage"].Value = usage;
                    historyTable.Rows[i].Cells["days"].Value = days;
                    historyTable.Rows[i].Cells["avg"].Value = usage / days;
                }
            }
        }

        private void watBtn_Click(object sender, EventArgs e) {
            ReadHistory.ActiveForm.Text = "Water History";
            historyTable.Rows.Clear();
            for (int i = 0; i < data.Rows.Count; i++) {
                DataRow row = data.Rows[i];
                historyTable.Rows.Add(1);
                historyTable.Rows[i].Cells["startDate"].Value = ((DateTime)row["StartDate"]).ToString("d");
                historyTable.Rows[i].Cells["readDate"].Value = ((DateTime)row["MeterReadDate"]).ToString("d"); ;
                historyTable.Rows[i].Cells["meterRead"].Value = row["WatReadValue"];
                if (i > 0) {
                    int usage = (int)data.Rows[i]["WatReadValue"] - (int)data.Rows[i - 1]["WatReadValue"];
                    double days = ((DateTime)data.Rows[i]["MeterReadDate"] - (DateTime)data.Rows[i]["StartDate"]).TotalDays;
                    historyTable.Rows[i].Cells["usage"].Value = usage;
                    historyTable.Rows[i].Cells["days"].Value = days;
                    historyTable.Rows[i].Cells["avg"].Value = usage / days;
                }
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            if (current == null) {
                DatabaseControl.executeInsertQuery(DatabaseControl.meterReadsTable, DatabaseControl.meterReadsColumns,
                    new Object[] { parkId, orderId, currStartDate.Value, currReadDate.Value, Convert.ToInt32(currGas.Text), Convert.ToInt32(currEle.Text), Convert.ToInt32(currWat.Text),
                    null, null, previous["DueDate"], previous["ThermX"] });
                this.Close();
            } else {
                String[] fields = { "StartDate", "MeterReadDate", "GasReadValue", "EleReadValue", "WatReadValue" };
                DatabaseControl.executeUpdateQuery(DatabaseControl.meterReadsTable, fields,
                    new Object[] { currStartDate.Value, currReadDate.Value, Convert.ToInt32(currGas.Text), Convert.ToInt32(currEle.Text), Convert.ToInt32(currWat.Text) },
                    "MeterReadID=" + current["MeterReadID"]);
                DatabaseControl.executeUpdateQuery(DatabaseControl.meterReadsTable, fields,
                    new Object[] { prevStartDate.Value, prevReadDate.Value, Convert.ToInt32(prevGas.Text), Convert.ToInt32(prevEle.Text), Convert.ToInt32(prevWat.Text) },
                    "MeterReadID=" + previous["MeterReadID"]);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            previous = data.Rows[(data.Rows.Count - 1)];
            current = null;
            currStartDate.Value = (DateTime)previous["MeterReadDate"];
            currReadDate.Value = DateTime.Today;
            currGas.Text = "";
            currEle.Text = "";
            currWat.Text = "";
            prevStartDate.Value = (DateTime)previous["StartDate"];
            prevReadDate.Value = (DateTime)previous["MeterReadDate"];
            prevGas.Text = previous["GasReadValue"].ToString();
            prevEle.Text = previous["EleReadValue"].ToString();
            prevWat.Text = previous["WatReadValue"].ToString();
        }
    }
}
