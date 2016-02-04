using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUB {
    public partial class ParkSpaceInfo : Form {
        public static DataRow[] infoRows;

        public ParkSpaceInfo() {
            InitializeComponent();
        }

        private void TenantInfo_Load(object sender, EventArgs e) {
            DatabaseControl.populateComboBox(ref parkNumberList, DatabaseControl.parkTable, "ParkNumber", "ParkID");
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkName", "ParkID");
        }

        private void parkNumberList_SelectedIndexChanged(object sender, EventArgs e) {
            CommonTools.Item item = (CommonTools.Item)parkNumberList.SelectedItem;
            String condition = "ParkID=@value0";
            parkList.Text = DatabaseControl.getSingleRecord(new String[] { "ParkName" }, DatabaseControl.parkTable, condition, new Object[] { item.Value })[0].ToString();
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e) {
            CommonTools.Item item = (CommonTools.Item)parkList.SelectedItem;
            String condition = "ParkID=@value0";
            parkNumberList.Text = DatabaseControl.getSingleRecord(new String[] { "ParkNumber" }, DatabaseControl.parkTable, condition, new Object[] { item.Value })[0].ToString();
            fillParkSpaceInfo(item.Value);
        }

        private void fillParkSpaceInfo(int parkId) {
            parkList.Enabled = parkNumberList.Enabled = false;
            DataTable parkTenants = new DataTable();
            String[] fields = { "ParkSpaceID", "SpaceID", "Tenant", "MoveInDate", "GasStatus", "GasType", "EleStatus", "EleType","WatStatus", "MedStatus",
                                "DontBillForGas", "DontBillForEle", "DontBillForWat" };
            DatabaseControl.populateDataTable(ref parkTenants, String.Join(",", fields),
                DatabaseControl.spaceTable, "ParkID=@value0 AND MoveOutDate IS NULL", new Object[] { parkId });
            infoRows = new DataRow[parkTenants.Rows.Count];
            parkTenants.Rows.CopyTo(infoRows, 0);
            createListOfTenants();
            for (int i = 0; i < infoRows.Length; i++) {
                ListOfTenants.Rows.Add(1);
                DataGridViewRow row = ListOfTenants.Rows[i];
                DataRow infoRow = infoRows[i];
                row.Cells["Space"].Value = infoRow["SpaceID"];
                row.Cells["Name"].Value = infoRow["Tenant"];
                row.Cells["gasStatus"].Value = infoRow["GasStatus"];
                row.Cells["gasType"].Value = infoRow["GasType"];
                row.Cells["eleStatus"].Value = infoRow["EleStatus"];
                row.Cells["eleType"].Value = infoRow["EleType"];
                row.Cells["watStatus"].Value = infoRow["WatStatus"];
                row.Cells["numMedical"].Value = infoRow["MedStatus"];
                row.Cells["dontBillGas"].Value = infoRow["DontBillForGas"];
                row.Cells["dontBillEle"].Value = infoRow["DontBillForEle"];
                row.Cells["dontBillWat"].Value = infoRow["DontBillForWat"];
                row.Cells["Move In Date"].Value = infoRow["MoveInDate"];
            }
            this.ListOfTenants.ClearSelection();
        }
        
        private void saveBtn_Click(object sender, EventArgs e) {
            if (parkList.SelectedItem == null || parkNumberList.SelectedItem == null) {
                MessageBox.Show("Must select a park");
            } else {
                for (int i = 0; i < ListOfTenants.Rows.Count - 1; i++) {
                    if (i < infoRows.Length) {
                        DataGridViewRow row = ListOfTenants.Rows[i];
                        CommonTools.Item park = (CommonTools.Item)parkList.SelectedItem;
                        String[] fields = { "SpaceID", "Tenant", "MoveInDate", "GasStatus", "GasType", "EleStatus", "EleType", "WatStatus", "MedStatus", 
                                            "DontBillForGas", "DontBillForEle", "DontBillForWat", "Address2" };
                        Object[] spaceValues = { row.Cells["Space"].Value, row.Cells["Name"].Value, row.Cells["Move In Date"].Value, row.Cells["gasStatus"].Value, row.Cells["gasType"].Value,
                                             row.Cells["eleStatus"].Value, row.Cells["eleType"].Value, row.Cells["watStatus"].Value, row.Cells["numMedical"].Value, 
                                             row.Cells["dontBillGas"].Value, row.Cells["dontBillEle"].Value, row.Cells["dontBillWat"].Value, row.Cells["Space"].Value };
                        String condition = "ParkSpaceID=" + infoRows[i]["ParkSpaceID"];
                        DatabaseControl.executeUpdateQuery(DatabaseControl.spaceTable, fields, spaceValues, condition);
                    } else {
                        DataGridViewRow row = ListOfTenants.Rows[i];
                        CommonTools.Item park = (CommonTools.Item)parkList.SelectedItem;
                        Object[] parkValues = DatabaseControl.getSingleRecord(new String[] {"Address", "City", "State", "ZipCode"} ,DatabaseControl.parkTable, "ParkID=@value0", new Object[] { park.Value });
                        Object[] spaceValues = { park.Value, row.Cells["Space"].Value, row.Cells["Name"].Value, row.Cells["Move In Date"].Value, null, row.Cells["gasStatus"].Value, row.Cells["gasType"].Value,
                                             row.Cells["eleStatus"].Value, row.Cells["eleType"].Value, row.Cells["watStatus"].Value, row.Cells["numMedical"].Value, 
                                             row.Cells["dontBillGas"].Value, row.Cells["dontBillEle"].Value, row.Cells["dontBillWat"].Value,
                                             parkValues[0], row.Cells["Space"].Value ,parkValues[1], parkValues[2], parkValues[3], "", "", "", "", "", "", "", "", "" };
                        DatabaseControl.executeInsertQuery(DatabaseControl.spaceTable, DatabaseControl.spaceColumns, spaceValues);
                    }
                }
                this.Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void createListOfTenants() {
            this.ListOfTenants.Rows.Clear();
            this.ListOfTenants.Columns.Clear();
            DataGridViewTextBoxColumn spaceNumber = new DataGridViewTextBoxColumn(); {
                spaceNumber.HeaderText = "Space Number";
                spaceNumber.Name = "Space";
                spaceNumber.Width = 50;
            }
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn(); {
                name.HeaderText = "Name";
                name.Name = "Name";
                name.Width = 200;
            }
            DataGridViewComboBoxColumn gasStatus = new DataGridViewComboBoxColumn(); {
                gasStatus.HeaderText = "GAS Status";
                gasStatus.Name = "gasStatus";
                gasStatus.Items.Add("");
                try {
                    int gasId = (int)DatabaseControl.getSingleRecord(new String[] { "GasCompanyID" }, DatabaseControl.parkTable, "ParkID=@value0",
                        new Object[] { ((CommonTools.Item)parkList.SelectedItem).Value })[0];
                    int utilRateId = (int)DatabaseControl.getSingleRecord(new String[] { "UtilityRateID" }, DatabaseControl.utilRateTable,
                        "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { gasId })[0];
                    ArrayList obj = DatabaseControl.getMultipleRecord(new String[] { "RateType" }, DatabaseControl.utilTierRatesTable,
                        "UtilityRateID=@value0", new Object[] { utilRateId });
                    foreach (Object[] status in obj) {
                        if (!(gasStatus.Items.Contains(status[0].ToString()))) { gasStatus.Items.Add(status[0].ToString()); }
                    }
                }
                catch {
                }
                gasStatus.Width = 80;
            }
            DataGridViewComboBoxColumn gasType = new DataGridViewComboBoxColumn(); {
                gasType.HeaderText = "GAS Type";
                gasType.Name = "gasType";
                gasType.Items.Add("1");
                gasType.Items.Add("2");
                gasType.Items.Add("3");
                gasType.Width = 40;
            }
            DataGridViewComboBoxColumn eleStatus = new DataGridViewComboBoxColumn(); {
                eleStatus.HeaderText = "ELE Status";
                eleStatus.Name = "eleStatus";
                eleStatus.Items.Add("");
                try {
                    int eleId = (int)DatabaseControl.getSingleRecord(new String[] { "ElectricityCompanyID" }, DatabaseControl.parkTable, "ParkID=@value0",
                        new Object[] { ((CommonTools.Item)parkList.SelectedItem).Value })[0];
                    int utilRateId = (int)DatabaseControl.getSingleRecord(new String[] { "UtilityRateID" }, DatabaseControl.utilRateTable,
                        "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { eleId })[0];
                    ArrayList obj = DatabaseControl.getMultipleRecord(new String[] { "Status" }, DatabaseControl.utilBasicRatesTable,
                        "UtilityRateID=@value0", new Object[] { utilRateId });
                    foreach (Object[] status in obj) {
                        if (!(eleStatus.Items.Contains(status[0].ToString()))) { eleStatus.Items.Add(status[0].ToString()); }
                    }
                }
                catch {
                }
                eleStatus.Width = 80;
            }
            DataGridViewComboBoxColumn eleType = new DataGridViewComboBoxColumn(); {
                eleType.HeaderText = "ELE Type";
                eleType.Name = "eleType";
                eleType.Items.Add("1");
                eleType.Items.Add("2");
                eleType.Width = 40;
            }
            DataGridViewComboBoxColumn watStatus = new DataGridViewComboBoxColumn(); {
                watStatus.HeaderText = "WAT Status";
                watStatus.Name = "watStatus";
                watStatus.Items.Add("R");
                watStatus.Items.Add("L");
                watStatus.Items.Add("S");
                try {
                    int watId = (int)DatabaseControl.getSingleRecord(new String[] { "WaterCompanyID" }, DatabaseControl.parkTable, "ParkID=@value0",
                        new Object[] { ((CommonTools.Item)parkList.SelectedItem).Value })[0];
                    int utilRateId = (int)DatabaseControl.getSingleRecord(new String[] { "UtilityRateID" }, DatabaseControl.utilRateTable,
                        "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { watId })[0];
                    ArrayList obj = DatabaseControl.getMultipleRecord(new String[] { "Status" }, DatabaseControl.utilBasicRatesTable,
                        "UtilityRateID=@value0 AND Service=@value1", new Object[] { utilRateId, 'W' });
                    foreach (Object[] status in obj) {
                        if (!(watStatus.Items.Contains(status[0].ToString()))) { watStatus.Items.Add(status[0].ToString()); }
                    }
                } catch {
                }
                watStatus.Width = 80;
            }
            DataGridViewComboBoxColumn medNum = new DataGridViewComboBoxColumn(); {
                medNum.HeaderText = "MED";
                medNum.Name = "numMedical";
                for (int i = 0; i < 9; i++) { medNum.Items.Add(i); }
                medNum.Width = 40;
            }
            DataGridViewCheckBoxColumn dontBillGas = new DataGridViewCheckBoxColumn(); {
                dontBillGas.HeaderText = "Don't Bill Gas";
                dontBillGas.Name = "dontBillGas";
                dontBillGas.Width = 50;
            }
            DataGridViewCheckBoxColumn dontBillEle = new DataGridViewCheckBoxColumn(); {
                dontBillEle.HeaderText = "Don't Bill Ele";
                dontBillEle.Name = "dontBillEle";
                dontBillEle.Width = 50;
            }
            DataGridViewCheckBoxColumn dontBillWat = new DataGridViewCheckBoxColumn(); {
                dontBillWat.HeaderText = "Don't Bill Wat";
                dontBillWat.Name = "dontBillWat";
                dontBillWat.Width = 50;
            }
            DataGridViewTextBoxColumn concession = new DataGridViewTextBoxColumn(); {
                concession.HeaderText = "Rent Concession";
                concession.Name = "Rent Concession";
                concession.Width = 50;
            }
            
            CommonTools.CalendarColumn movein = new CommonTools.CalendarColumn(); {
                movein.Name = "Move In Date";
            }
            this.ListOfTenants.Columns.Add(spaceNumber);
            this.ListOfTenants.Columns.Add(name);
            this.ListOfTenants.Columns.Add(movein);
            this.ListOfTenants.Columns.Add(gasStatus);
            this.ListOfTenants.Columns.Add(gasType);
            this.ListOfTenants.Columns.Add(eleStatus);
            this.ListOfTenants.Columns.Add(eleType);
            this.ListOfTenants.Columns.Add(watStatus);
            this.ListOfTenants.Columns.Add(medNum);
            this.ListOfTenants.Columns.Add(dontBillGas);
            this.ListOfTenants.Columns.Add(dontBillEle);
            this.ListOfTenants.Columns.Add(dontBillWat);
            //this.ListOfTenants.Columns.Add(concession);
            this.ListOfTenants.Sort(ListOfTenants.Columns["Space"], ListSortDirection.Ascending);
        }

        private void ListOfTenants_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e) {
            e.Row.Cells["gasStatus"].Value = "";
            e.Row.Cells["gasType"].Value = "1";
            e.Row.Cells["eleStatus"].Value = "";
            e.Row.Cells["eleType"].Value = "All Services";
            e.Row.Cells["watStatus"].Value = "";
            e.Row.Cells["numMedical"].Value = 0;
            e.Row.Cells["dontBillGas"].Value = false;
            e.Row.Cells["dontBillEle"].Value = false;
            e.Row.Cells["dontBillWat"].Value = false;
            e.Row.Cells["Move In Date"].Value = DateTime.Today.Date;
        }

        private void moveoutBtn_Click(object sender, EventArgs e) {
            int parkId = ((CommonTools.Item)parkList.SelectedItem).Value;
            MoveOut move = new MoveOut(parkId);
            move.ShowDialog();
            fillParkSpaceInfo(parkId);
        }
    }
}
