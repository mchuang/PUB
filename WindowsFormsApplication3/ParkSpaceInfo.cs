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
        bool newTenant = false;
        int parkId;
        

        public ParkSpaceInfo() {
            InitializeComponent();
        }

        private void TenantInfo_Load(object sender, EventArgs e) {
            DatabaseControl.populateComboBox(ref parkNumberList, DatabaseControl.parkTable, "ParkNumber", "ParkID");
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkName", "ParkID");
            if (newTenant) button1.Enabled = true;
        }

        private void parkNumberList_SelectedIndexChanged(object sender, EventArgs e) {
            CommonTools.Item item = (CommonTools.Item)parkNumberList.SelectedItem;
            parkId = item.Value;
            String condition = "ParkID=@value0";
            parkList.Text = DatabaseControl.getSingleRecord(new String[] { "ParkName" }, DatabaseControl.parkTable, condition, new Object[] { parkId })[0].ToString();
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e) {
            CommonTools.Item item = (CommonTools.Item)parkList.SelectedItem;
            parkId = item.Value;
            String condition = "ParkID=@value0";
            parkNumberList.Text = DatabaseControl.getSingleRecord(new String[] { "ParkNumber" }, DatabaseControl.parkTable, condition, new Object[] { parkId })[0].ToString();
            fillParkSpaceInfo(item.Value);
        }

        private void fillParkSpaceInfo(int parkId) {
            parkList.Enabled = parkNumberList.Enabled = false;
            DataTable parkTenants = new DataTable();
            String[] fields = { "ParkSpaceID", "OrderID", "SpaceID", "Tenant", "MoveInDate", "GasStatus", "GasType", "EleStatus", "EleType", "WatStatus", "GasMedical", "EleMedical",
                                "DontBillForGas", "DontBillForEle", "DontBillForWat", "Concession" };
            DatabaseControl.populateDataTable(ref parkTenants, String.Join(",", fields),
                DatabaseControl.spaceTable, "ParkID=@value0 AND MoveOutDate IS NULL", new Object[] { parkId });
            infoRows = new DataRow[parkTenants.Rows.Count];
            parkTenants.Rows.CopyTo(infoRows, 0);
            createListOfTenants();
            for (int i = 0; i < infoRows.Length; i++) {
                ListOfTenants.Rows.Add(1);
                DataGridViewRow row = ListOfTenants.Rows[i];
                DataRow infoRow = infoRows[i];
                row.Cells["Order"].Value = infoRow["OrderID"];
                row.Cells["Space"].Value = infoRow["SpaceID"];
                row.Cells["Name"].Value = infoRow["Tenant"];
                row.Cells["gasStatus"].Value = infoRow["GasStatus"];
                row.Cells["gasType"].Value = infoRow["GasType"];
                row.Cells["eleStatus"].Value = infoRow["EleStatus"];
                row.Cells["eleType"].Value = infoRow["EleType"];
                row.Cells["watStatus"].Value = infoRow["WatStatus"];
                row.Cells["eleMed"].Value = infoRow["EleMedical"];
                row.Cells["gasMed"].Value = infoRow["GasMedical"];
                row.Cells["dontBillGas"].Value = infoRow["DontBillForGas"];
                row.Cells["dontBillEle"].Value = infoRow["DontBillForEle"];
                row.Cells["dontBillWat"].Value = infoRow["DontBillForWat"];
                row.Cells["concession"].Value = infoRow["Concession"];
                row.Cells["Move In Date"].Value = infoRow["MoveInDate"];
            }
            this.ListOfTenants.Sort(ListOfTenants.Columns["Order"], ListSortDirection.Ascending);
            this.ListOfTenants.AllowUserToAddRows = true;
            this.ListOfTenants.AllowUserToDeleteRows = false;
            this.ListOfTenants.ClearSelection();
            newTenant = false;
            button1.Enabled = true;
            moveoutBtn.Visible = true;
        }

        public void fillParkSpaceInfo(int parkId, int orderId, String spaceId) {
            parkList.Enabled = parkNumberList.Enabled = false;
            this.parkId = parkId;
            createListOfTenants();
            ListOfTenants.Rows.Add();
            DataGridViewRow row = ListOfTenants.Rows[0];
            row.Cells["Order"].Value = orderId;
            row.Cells["Space"].Value = spaceId;
            row.Cells["gasStatus"].Value = "";
            row.Cells["gasType"].Value = "1";
            row.Cells["eleStatus"].Value = "";
            row.Cells["eleType"].Value = "1";
            row.Cells["watStatus"].Value = "";
            row.Cells["gasMed"].Value = 0;
            row.Cells["eleMed"].Value = 0;
            row.Cells["dontBillGas"].Value = false;
            row.Cells["dontBillEle"].Value = false;
            row.Cells["dontBillWat"].Value = false;
            row.Cells["concession"].Value = false;
            row.Cells["Move In Date"].Value = DateTime.Today.Date;
            this.ListOfTenants.AllowUserToDeleteRows = false;
            this.ListOfTenants.AllowUserToAddRows = false;
            this.ListOfTenants.ClearSelection();
            newTenant = true;
            button1.Enabled = true;
            moveoutBtn.Visible = false;
        }
        
        private void saveBtn_Click(object sender, EventArgs e) {
            if (newTenant) {
                DataGridViewRow row = ListOfTenants.Rows[0];
                Object[] parkValues = DatabaseControl.getSingleRecord(new String[] { "Address", "City", "State", "ZipCode" }, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { parkId });
                Object[] spaceValues = { parkId, row.Cells["Space"].Value, row.Cells["Name"].Value, row.Cells["Move In Date"].Value, null, row.Cells["gasStatus"].Value, row.Cells["gasType"].Value,
                                             row.Cells["eleStatus"].Value, row.Cells["eleType"].Value, row.Cells["watStatus"].Value, row.Cells["gasMed"].Value, row.Cells["eleMed"], 
                                             row.Cells["dontBillGas"].Value, row.Cells["dontBillEle"].Value, row.Cells["dontBillWat"].Value, 
                                             parkValues[0], row.Cells["Space"].Value ,parkValues[1], parkValues[2], parkValues[3], "", "", "", "", "", "", "", "", "", row.Cells["Order"].Value, row.Cells["concession"].Value };
                DatabaseControl.executeInsertQuery(DatabaseControl.spaceTable, DatabaseControl.spaceColumns, spaceValues);
                fillParkSpaceInfo(parkId);
            } else {
                for (int i = 0; i < ListOfTenants.Rows.Count - 1; i++) {
                    DataGridViewRow row = ListOfTenants.Rows[i];
                    if (i < infoRows.Length) {
                        String[] fields = { "OrderID", "SpaceID", "Tenant", "MoveInDate", "GasStatus", "GasType", "EleStatus", "EleType", "WatStatus", "GasMedical", "EleMedical", 
                                            "DontBillForGas", "DontBillForEle", "DontBillForWat", "Address2", "Concession" };
                        Object[] spaceValues = { row.Cells["Order"].Value, row.Cells["Space"].Value, row.Cells["Name"].Value, row.Cells["Move In Date"].Value, row.Cells["gasStatus"].Value, row.Cells["gasType"].Value,
                                             row.Cells["eleStatus"].Value, row.Cells["eleType"].Value, row.Cells["watStatus"].Value, row.Cells["gasMed"].Value, row.Cells["eleMed"].Value, 
                                             row.Cells["dontBillGas"].Value, row.Cells["dontBillEle"].Value, row.Cells["dontBillWat"].Value, row.Cells["Space"].Value, row.Cells["concession"].Value };
                        String condition = "ParkSpaceID=" + infoRows[i]["ParkSpaceID"];
                        DatabaseControl.executeUpdateQuery(DatabaseControl.spaceTable, fields, spaceValues, condition);
                    } else {
                        Object[] parkValues = DatabaseControl.getSingleRecord(new String[] { "Address", "City", "State", "ZipCode" }, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { parkId });
                        Object[] spaceValues = { parkId, row.Cells["Space"].Value, row.Cells["Name"].Value, row.Cells["Move In Date"].Value, null, row.Cells["gasStatus"].Value, row.Cells["gasType"].Value,
                                             row.Cells["eleStatus"].Value, row.Cells["eleType"].Value, row.Cells["watStatus"].Value, row.Cells["gasMed"].Value, row.Cells["eleMed"], 
                                             row.Cells["dontBillGas"].Value, row.Cells["dontBillEle"].Value, row.Cells["dontBillWat"].Value,
                                             parkValues[0], row.Cells["Space"].Value ,parkValues[1], parkValues[2], parkValues[3], "", "", "", "", "", "", "", "", "", row.Cells["Order"].Value, row.Cells["concession"].Value };
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
                        new Object[] { parkId })[0];
                    int utilRateId = (int)DatabaseControl.getSingleRecord(new String[] { "UtilityRateID" }, DatabaseControl.utilRateTable,
                        "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { gasId })[0];
                    List<Object[]> obj = DatabaseControl.getMultipleRecord(new String[] { "RateType" }, DatabaseControl.utilTierRatesTable,
                        "UtilityRateID=@value0", new Object[] { utilRateId });
                    foreach (Object[] status in obj) {
                        if (!(gasStatus.Items.Contains(status[0].ToString()))) { gasStatus.Items.Add(status[0].ToString()); }
                    }
                }
                catch {
                }
                gasStatus.Width = 100;
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
                        new Object[] { parkId })[0];
                    int utilRateId = (int)DatabaseControl.getSingleRecord(new String[] { "UtilityRateID" }, DatabaseControl.utilRateTable,
                        "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { eleId })[0];
                    List<Object[]> obj = DatabaseControl.getMultipleRecord(new String[] { "Status" }, DatabaseControl.utilBasicRatesTable,
                        "UtilityRateID=@value0", new Object[] { utilRateId });
                    foreach (Object[] status in obj) {
                        if (!(eleStatus.Items.Contains(status[0].ToString()))) { eleStatus.Items.Add(status[0].ToString()); }
                    }
                }
                catch {
                }
                eleStatus.Width = 100;
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
                watStatus.Items.Add("");
                try {
                    int watId = (int)DatabaseControl.getSingleRecord(new String[] { "WaterCompanyID" }, DatabaseControl.parkTable, "ParkID=@value0",
                        new Object[] { parkId })[0];
                    int utilRateId = (int)DatabaseControl.getSingleRecord(new String[] { "UtilityRateID" }, DatabaseControl.utilRateTable,
                        "UtilityCompanyID=@value0 ORDER BY UtilityRateID DESC", new Object[] { watId })[0];
                    List<Object[]> obj = DatabaseControl.getMultipleRecord(new String[] { "Status" }, DatabaseControl.utilBasicRatesTable,
                        "UtilityRateID=@value0 AND Service=@value1", new Object[] { utilRateId, 'W' });
                    foreach (Object[] status in obj) {
                        if (!(watStatus.Items.Contains(status[0].ToString()))) { watStatus.Items.Add(status[0].ToString()); }
                    }
                } catch {
                }
                watStatus.Width = 100;
            }
            DataGridViewComboBoxColumn gasMed = new DataGridViewComboBoxColumn(); {
                gasMed.HeaderText = "MEDGAS";
                gasMed.Name = "gasMed";
                gasMed.ValueType = typeof(int);
                gasMed.Items.Add(0);
                gasMed.Items.Add(1);
                gasMed.Width = 60;
            }
            DataGridViewComboBoxColumn eleMed = new DataGridViewComboBoxColumn();
            {
                eleMed.HeaderText = "MEDELE";
                eleMed.Name = "eleMed";
                eleMed.ValueType = typeof(int);
                for (int i = 0; i < 9; i++) { eleMed.Items.Add(i); }
                eleMed.Width = 60;
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
            DataGridViewCheckBoxColumn concession = new DataGridViewCheckBoxColumn();
            {
                concession.HeaderText = "Concession";
                concession.Name = "concession";
                concession.Width = 50;
            }
            
            CommonTools.CalendarColumn movein = new CommonTools.CalendarColumn(); {
                movein.Name = "Move In Date";
            }

            DataGridViewTextBoxColumn orderNumber = new DataGridViewTextBoxColumn();
            {
                orderNumber.HeaderText = "Order Number";
                orderNumber.Name = "Order";
                orderNumber.Width = 50;
            }

            this.ListOfTenants.Columns.Add(orderNumber);
            this.ListOfTenants.Columns.Add(spaceNumber);
            this.ListOfTenants.Columns.Add(name);
            this.ListOfTenants.Columns.Add(movein);
            this.ListOfTenants.Columns.Add(gasStatus);
            this.ListOfTenants.Columns.Add(gasType);
            this.ListOfTenants.Columns.Add(eleStatus);
            this.ListOfTenants.Columns.Add(eleType);
            this.ListOfTenants.Columns.Add(watStatus);
            this.ListOfTenants.Columns.Add(gasMed);
            this.ListOfTenants.Columns.Add(eleMed);
            this.ListOfTenants.Columns.Add(dontBillGas);
            this.ListOfTenants.Columns.Add(dontBillEle);
            this.ListOfTenants.Columns.Add(dontBillWat);
            this.ListOfTenants.Columns.Add(concession);
            this.ListOfTenants.Sort(ListOfTenants.Columns["Order"], ListSortDirection.Ascending);
        }

        private void ListOfTenants_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e) {
            e.Row.Cells["gasStatus"].Value = "";
            e.Row.Cells["gasType"].Value = "1";
            e.Row.Cells["eleStatus"].Value = "";
            e.Row.Cells["eleType"].Value = "1";
            e.Row.Cells["watStatus"].Value = "";
            e.Row.Cells["gasMed"].Value = 0;
            e.Row.Cells["eleMed"].Value = 0;
            e.Row.Cells["dontBillGas"].Value = false;
            e.Row.Cells["dontBillEle"].Value = false;
            e.Row.Cells["dontBillWat"].Value = false;
            e.Row.Cells["concession"].Value = false;
            e.Row.Cells["Move In Date"].Value = DateTime.Today.Date;
        }

        private void moveoutBtn_Click(object sender, EventArgs e) {
            MoveOut move = new MoveOut(parkId);
            move.ShowDialog();
            if (move.newTenant) {
                fillParkSpaceInfo(parkId, move.orderId, move.spaceId);
            } else {
                fillParkSpaceInfo(parkId);
            }
        }
    }
}
