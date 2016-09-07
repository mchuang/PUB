using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PUB {
    public partial class ManualMeterReadInput : Form {
        public ManualMeterReadInput() {
            InitializeComponent();
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e) {
            CommonTools.Item park = (CommonTools.Item)parkList.SelectedItem;
            input.Columns.Clear();
            Object[] info = DatabaseControl.getSingleRecord(new String[] { "GasCompanyID", "ElectricityCompanyID", "WaterCompanyID" }, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { park.Value });
            //List<Object[]> items = DatabaseControl.getMultipleRecord(new String[] { "DISTINCT OrderID" }, DatabaseControl.meterReadsTable, "ParkID=@value0 ORDER BY OrderID ASC", new Object[] { park.Value });
            input.Columns.Add("order", "Order");
            if (info[0] != DBNull.Value) input.Columns.Add("gasInput", "GasRead");
            if (info[1] != DBNull.Value) input.Columns.Add("eleInput", "EleRead");
            if (info[2] != DBNull.Value) input.Columns.Add("watInput", "WatRead");
            input.AllowUserToAddRows = true;
            /*input.Rows.Add(items.Count);
            int i = 0;
            foreach (Object[] item in items) {
                DataGridViewRow row = input.Rows[i];
                //DataGridViewRow row = new DataGridViewRow();
                row.Cells["order"].Value = item[0];
                //input.Rows.Add(row);
                i++;
            }*/
        }

        private void ManualMeterReadInput_Load(object sender, EventArgs e) {
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkNumber", "ParkName", "ParkID", "1=1", new Object[] { });
        }

        private void button1_Click(object sender, EventArgs e) {
            CommonTools.Item park = (CommonTools.Item)parkList.SelectedItem;
            Object[] info = DatabaseControl.getSingleRecord(new String[] { "GasCompanyID", "ElectricityCompanyID", "WaterCompanyID" }, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { park.Value });

            foreach (DataGridViewRow row in input.Rows) {
                Object id = null;
                try {
                    id = Convert.ToInt32(employeeId.Text);
                } catch {
                    id = null;
                }

                try {
                    Object[] exists = DatabaseControl.getSingleRecord(new String[] { "MeterReadID" }, DatabaseControl.meterReadsTable,
                        "ParkID=@value0 AND OrderID=@value1 AND DueDate=@value2",
                        new Object[] { park.Value, Convert.ToInt32(row.Cells["order"].Value), dueDate.Value.Date });
                    Object gas, ele, wat;
                    if (info[0] != DBNull.Value) gas = Convert.ToInt32(row.Cells["gasInput"].Value);
                    else gas = null;
                    if (info[1] != DBNull.Value) ele = Convert.ToInt32(row.Cells["eleInput"].Value);
                    else ele = null;
                    if (info[2] != DBNull.Value) wat = Convert.ToInt32(row.Cells["watInput"].Value);
                    else wat = null;
                    if (exists == null) {
                        DatabaseControl.executeInsertQuery(DatabaseControl.meterReadsTable, DatabaseControl.meterReadsColumns,
                            new Object[] { park.Value, Convert.ToInt32(row.Cells["order"].Value), startDate.Value, endDate.Value, 
                            gas, ele, wat, id, null, dueDate.Value, Convert.ToDecimal(thermX.Text) });
                    } else {
                        DatabaseControl.executeUpdateQuery(DatabaseControl.meterReadsTable, new String[] { "GasReadValue", "EleReadValue", "WatReadValue" },
                            new Object[] { gas, ele, wat }, "MeterReadID=@value0", new Object[] { exists[0] });
                    }
                } catch {
                    MessageBox.Show("Invalid values");
                }
            }
        }
    }
}
