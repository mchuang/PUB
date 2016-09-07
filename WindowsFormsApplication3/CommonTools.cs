using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUB {
    public class CommonTools {
        public class Item {
            public string Text;
            public int Value;

            public Item(int id, String name) {
                this.Value = id;
                this.Text = name;
            }

            public override string ToString() {
                return Text.Replace("12:00:00 AM", "");
            }

            public static String getString(ref ComboBox box, int value) {
                foreach (Item item in box.Items) {
                    if (item.Value == value) { return item.Text; }
                }
                return "";
            }
        }

        public class Season {
            public string Text;
            public char Value;

            public Season(String s) {
                this.Text = s;
                this.Value = s[0];
            }

            public override string ToString() {
                return Text;
            }
        }

        public class Option {
            public decimal charge { set; get; }
            public int id { set; get; }
            public String description { set; get; }
            public Option(String description, int id = -1, decimal charge = 0.0M) {
                this.id = id;
                this.charge = charge;
                this.description = description;
            }

            public override bool Equals(object obj) {
                try { return ((Option)obj).id == this.id; } catch { return false; }
                
            }

            public override string ToString() {
                return description + ":" + charge.ToString("C2");
            }
        }

        public class Temp {
            public decimal charge { set; get; }
            public String description { set; get; }
            public DateTime dateAssigned { set; get; }
            public Temp(String description, DateTime date, decimal charge = 0.0M) {
                this.charge = charge;
                this.description = description;
                this.dateAssigned = date;
            }

            public override bool Equals(object obj) {
                try { return ((Temp)obj).description == this.description; } catch { return false; }

            }

            public override string ToString() {
                return String.Format("{0}({1}):{2}", description, dateAssigned.Date.ToString("d"), charge.ToString("C2"));
            }
        }

        public class Read {
            public int readId{set; get;}
            public int parkID{set; get;}
            public string spaceID { set; get; }
            public int gasValue{set; get;}
            public int eleValue{set; get;}
            public int watValue {set; get;}
            public DateTime readDate{set; get;}
            public DateTime prevReadDate { set; get; }

            public Read(int parkID, string spaceID,  DateTime readDate, int gasValue, int eleValue, int watValue, DateTime prevReadDate, int id = -1) {
                this.readId = id;
                this.parkID = parkID;
                this.spaceID = spaceID;
                this.gasValue = gasValue;
                this.eleValue = eleValue;
                this.watValue = watValue;
                this.readDate = readDate;
                this.prevReadDate = prevReadDate;
            }
        }

        public class Tier {
            public char chargeType { set; get; }
            public String status { set; get; }
            public decimal[] rates;

            public Tier(char charge, String description) {
                this.chargeType = charge;
                this.status = description;
                rates = new decimal[] { 0.0000M, 0.0000M, 0.0000M, 0.0000M, 0.0000M };
            }

            public void setTier(int tier, decimal rate) {
                rates[tier - 1] = rate;
            }

            public decimal getTier(int tier) {
                return rates[tier - 1];
            }

            public override bool Equals(Object obj) {
                Tier set = (Tier)obj;
                return set.chargeType == this.chargeType && this.status == set.status;
            }
        }

        public class Surcharge {
            public String info { set; get; }
            public String status { set; get; }
            public decimal rate { set; get; }
            public int usage { set; get; }
            public char charge { set; get; }

            public Surcharge(String description, String status, char charge, int usage, decimal rate) {
                this.info = description;
                this.status = status;
                this.rate = rate;
                this.usage = usage;
                this.charge = charge;
            }

            public override bool Equals(Object obj) {
                Surcharge set = (Surcharge)obj;
                return set.info == this.info && this.status == set.status && this.charge == set.charge;
            }
        }

        public class CustomerCharge {
            public String status { set; get; }
            public char service { set; get; }
            public decimal rate { set; get; }

            public CustomerCharge(String status, char service, decimal rate) {
                this.status = status;
                this.service = service;
                this.rate = rate;
            }

            public override bool Equals(Object obj) {
                CustomerCharge set = (CustomerCharge)obj;
                return this.status == set.status && this.service == set.service;
            }
        }

        public static void createBatchCSV() {
            List<Object[]> parks = DatabaseControl.getMultipleRecord(new String[] { "ParkID" }, DatabaseControl.parkTable, "ParkID IS NOT NULL");
            foreach (Object[] park in parks) {
                createMeterReadCSV((int)park[0]);
            }
            MessageBox.Show("Park Meter Reads Files have been exported");
        }

        public static void CSVtoDB(String filename) {
            String path = "C:\\paradox\\local2\\";
            Directory.CreateDirectory(path + "Export");
            String strCmdText = String.Format("/C C:\\paradox\\local2\\LoadTICS.exe {0}", path + filename);
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            String copy = String.Format("/C copy /y {0} {1}", path + "TICSDATA.DB", path + filename.Replace(".csv", ".DB"));
            System.Diagnostics.Process.Start("CMD.exe", copy);
        }

        public static void CSVtoDB(String filename, StringBuilder csv) {
            String path = "C:\\paradox\\local2\\";
            Directory.CreateDirectory(path + "Export");
            File.WriteAllText(filename, csv.ToString());
            String strCmdText = String.Format("/C LoadTICS.exe {0}", path + filename);
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            String copy = String.Format("/C copy /y {0} {1}", path + "TICSDATA.DB", path + filename.Replace(".csv", ".DB"));
            System.Diagnostics.Process.Start("CMD.exe", copy);
            //MessageBox.Show(Directory.GetCurrentDirectory());
            //MessageBox.Show(copy);
        }

        public static void createMeterReadCSV(int parkId) {
            Object date;
            try {
                date = DatabaseControl.getSingleRecord(new String[] { "DueDate" }, DatabaseControl.periodTable, "ParkID=@value0 ORDER BY DueDate DESC", new Object[] { parkId })[0];
            } catch {
                return;
            }
            if (date == null) { return; }
            List<Object[]> tenants = DatabaseControl.getMultipleRecord(new String[] { "OrderID" }, DatabaseControl.meterReadsTable,
                "ParkID=@value0 AND DueDate=@value1 ORDER BY OrderID", new Object[] { parkId, date });
            StringBuilder csv = new StringBuilder();
            foreach (Object[] tenant in tenants) {
                int spaceId = (int)tenant[0];
                String[] fields = new String[] { "GasReadValue", "EleReadValue", "WatReadValue" };
                String condition = "ParkID=@value0 AND OrderID=@value1 ORDER BY MeterReadDate DESC";
                ArrayList reads = DatabaseControl.getMultipleRecordDict(fields, DatabaseControl.meterReadsTable, condition, new Object[] { parkId, spaceId });
                Dictionary<String, Object> meterInfo = DatabaseControl.getSingleRecordDict(new String[] { "GasMeterLoc", "EleMeterLoc", "WatMeterLoc", "SpecialAccess" }, 
                    DatabaseControl.meterInfoTable, "ParkID=@value0 AND OrderID=@value1", new Object[] { parkId, spaceId });

                Object[] row = new Object[19];
                row[0] = tenant[0];
                row[1] = row[2] = row[3] = null;
                if (reads.Count > 0) {
                    Dictionary<String, Object> month0 = (Dictionary<String, Object>)reads[0];
                    row[4] = (int)month0["EleReadValue"];
                    row[5] = (int)month0["GasReadValue"];
                    row[6] = (int)month0["WatReadValue"];
                } else {
                    row[4] = row[5] = row[6] = null;
                }

                if (reads.Count > 1) {
                    Dictionary<String, Object> month0 = (Dictionary<String, Object>)reads[0];
                    Dictionary<String, Object> month1 = (Dictionary<String, Object>)reads[1];
                    row[7] = (int)month0["EleReadValue"] - (int)month1["EleReadValue"];
                    row[8] = (int)month0["GasReadValue"] - (int)month1["GasReadValue"];
                    row[9] = (int)month0["WatReadValue"] - (int)month1["WatReadValue"];
                } else {
                    row[7] = row[8] = row[9] = null;
                }

                if (reads.Count >= 12) {
                    Dictionary<String, Object> month0 = (Dictionary<String, Object>)reads[11];
                    Dictionary<String, Object> month1 = (Dictionary<String, Object>)reads[12];
                    row[10] = (int)month0["EleReadValue"] - (int)month1["EleReadValue"];
                    row[11] = (int)month0["GasReadValue"] - (int)month1["GasReadValue"];
                    row[12] = (int)month0["WatReadValue"] - (int)month1["WatReadValue"];
                } else {
                    row[10] = row[11] = row[12] = null;
                }
                
                row[13] = meterInfo["EleMeterLoc"];
                row[14] = meterInfo["GasMeterLoc"];
                row[15] = meterInfo["WatMeterLoc"]; 
                row[16] = meterInfo["SpecialAccess"]; 
                row[17] = null; row[18] = null;
                csv.AppendLine(String.Join(",", row));
            }
            Object parkNumber = DatabaseControl.getSingleRecord(new String[] { "ParkNumber" }, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { parkId })[0];
            Directory.CreateDirectory("Export");
            File.WriteAllText(String.Format("Export\\{0}.csv", parkNumber), csv.ToString());
            string strCmdText;
            strCmdText = String.Format("/C CSVConverter\\csvcnv.exe Export\\{0}.csv Export\\{1}.DB", parkNumber, parkNumber);
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        public static void createCollectionsCSV(int parkId, DateTime dueDate, List<Object[]> rows, List<Object[]> parkCharge) {
            StringBuilder csv = new StringBuilder();
            String[] headers = new String[] { "Tenant Space", "Tenant Name", "Due Date", "Gas Status", "Ele Status", "Wat Status", "New Date", "Old Date", 
                "Old Gas Number", "Old Ele Number", "Old Wat Number", "New Gas Number", "New Ele Number", "New Wat Number", "Gas Usage", "Ele Usage", "Wat Usage",
                "Balance Forward", "Late Charge", "Charge1", "Charge2", "Charge3", "Charge4", "Charge5", "Charge6", "Charge7", "Charge8", "GasTotal", "EleTotal", "WatTotal", 
                "Charge9 Mess","Charge9", "Charge10 Mess", "Charge10", "Total" };
            csv.AppendLine(String.Join(",", headers));
            foreach (Object[] row in rows) {
                List<String> values = new List<String>();
                Dictionary<String, Object> tenantInfo = (Dictionary<String, Object>)row[0];
                values.Add(tenantInfo["OrderID"].ToString());
                values.Add("\""+tenantInfo["Tenant"].ToString()+"\"");
                values.Add(dueDate.ToString("d"));
                values.Add(tenantInfo["GasStatus"].ToString());
                values.Add(tenantInfo["EleStatus"].ToString());
                values.Add(tenantInfo["WatStatus"].ToString());
                values.Add(((DateTime)row[1]).ToString("d"));
                values.Add(((DateTime)row[2]).ToString("d"));
                Dictionary<String, Object> billInfo = (Dictionary<String, Object>)row[7];
                values.Add(billInfo["GasPreviousRead"].ToString());
                values.Add(billInfo["ElePreviousRead"].ToString());
                values.Add(billInfo["WatPreviousRead"].ToString());
                values.Add(billInfo["GasCurrentRead"].ToString());
                values.Add(billInfo["WatCurrentRead"].ToString());
                values.Add(billInfo["EleCurrentRead"].ToString());
                values.Add(billInfo["GasUsage"].ToString());
                values.Add(billInfo["EleUsage"].ToString());
                values.Add(billInfo["WatUsage"].ToString());
                values.Add(((decimal)row[3]).ToString());
                values.Add(((decimal)row[4]).ToString());

                decimal total = (decimal)row[3] + (decimal)row[4];
                List<Object[]> summary = (List<Object[]>)row[5];
                int j = 0;
                foreach (Object[] item in summary) {
                    if (j >= summary.Count) {
                        values.Add(null);
                        j++;
                    } else if (j >= 8) { 
                        break;
                    } else if ((int)item[2] == 1 || (int)item[2] == 2) {
                        continue;
                    } else {
                        values.Add(((decimal)item[1]).ToString());
                        total += (decimal)item[1];
                        j++;
                    }
                }

                values.Add(billInfo["GasBill"].ToString());
                values.Add(billInfo["EleBill"].ToString());
                values.Add(billInfo["WatBill"].ToString());

                List<Object[]> temporary = (List<Object[]>)row[6];
                for (int i = 0; i < 2; i++) {
                    if (i >= temporary.Count) {
                        values.Add(null);
                        values.Add(0M.ToString());
                    } else {
                        Object[] item = (Object[])temporary[i];
                        values.Add(item[0].ToString());
                        values.Add(((decimal)item[1]).ToString());
                        total += (decimal)item[1];
                    }
                }

                total += (decimal)billInfo["GasBill"];
                total += (decimal)billInfo["EleBill"];
                total += (decimal)billInfo["WatBill"];
                values.Add(total.ToString());
                csv.AppendLine(String.Join(",", values));
            }
            Object parkNumber = DatabaseControl.getSingleRecord(new String[] { "ParkNumber" }, DatabaseControl.parkTable, "ParkID=@value0", new Object[] { parkId })[0];
            Directory.CreateDirectory("Export");
            File.WriteAllText(String.Format("Export\\Collections{0}.csv", parkNumber), csv.ToString());
            //string strCmdText;
            //strCmdText = String.Format("/C CSVConverter\\csvcnv.exe Export\\Collections{0}.csv Export\\Collections{0}.dbf /SRCHDR", parkNumber);
            //System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            CSVtoDB(String.Format("Export\\Collections{0}.csv", parkNumber), csv);
        }

        public class CalendarColumn : DataGridViewColumn
        {
            public CalendarColumn()
                : base(new CalendarCell())
            {
            }

            public override DataGridViewCell CellTemplate
            {
                get
                {
                    return base.CellTemplate;
                }
                set
                {
                    // Ensure that the cell used for the template is a CalendarCell.
                    if (value != null &&
                        !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                    {
                        throw new InvalidCastException("Must be a CalendarCell");
                    }
                    base.CellTemplate = value;
                }
            }
        }

        public class CalendarCell : DataGridViewTextBoxCell
        {
            public CalendarCell()
                : base()
            {
                // Use the short date format.
                this.Style.Format = "d";
            }

            public override void InitializeEditingControl(int rowIndex, object
                initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
            {
                // Set the value of the editing control to the current cell value.
                base.InitializeEditingControl(rowIndex, initialFormattedValue,
                    dataGridViewCellStyle);
                CalendarEditingControl ctl =
                    DataGridView.EditingControl as CalendarEditingControl;
                // Use the default row value when Value property is null.
                if (this.Value == null)
                {
                    ctl.Value = (DateTime)this.DefaultNewRowValue;
                }
                else
                {
                    ctl.Value = (DateTime)this.Value;
                }
            }

            public override Type EditType
            {
                get
                {
                    // Return the type of the editing control that CalendarCell uses.
                    return typeof(CalendarEditingControl);
                }
            }

            public override Type ValueType
            {
                get
                {
                    // Return the type of the value that CalendarCell contains.

                    return typeof(DateTime);
                }
            }

            public override object DefaultNewRowValue
            {
                get
                {
                    // Use the current date and time as the default value.
                    return DateTime.Now.Date;
                }
            }
        }

        class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
        {
            DataGridView dataGridView;
            private bool valueChanged = false;
            int rowIndex;

            public CalendarEditingControl()
            {
                this.Format = DateTimePickerFormat.Short;
            }

            // Implements the IDataGridViewEditingControl.EditingControlFormattedValue 
            // property.
            public object EditingControlFormattedValue
            {
                get
                {
                    return this.Value.ToShortDateString();
                }
                set
                {
                    if (value is String)
                    {
                        try
                        {
                            // This will throw an exception of the string is 
                            // null, empty, or not in the format of a date.
                            this.Value = DateTime.Parse((String)value);
                        }
                        catch
                        {
                            // In the case of an exception, just use the 
                            // default value so we're not left with a null
                            // value.
                            this.Value = DateTime.Now;
                        }
                    }
                }
            }

            // Implements the 
            // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
            public object GetEditingControlFormattedValue(
                DataGridViewDataErrorContexts context)
            {
                return EditingControlFormattedValue;
            }

            // Implements the 
            // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
            public void ApplyCellStyleToEditingControl(
                DataGridViewCellStyle dataGridViewCellStyle)
            {
                this.Font = dataGridViewCellStyle.Font;
                this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
                this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
            }

            // Implements the IDataGridViewEditingControl.EditingControlRowIndex 
            // property.
            public int EditingControlRowIndex
            {
                get
                {
                    return rowIndex;
                }
                set
                {
                    rowIndex = value;
                }
            }

            // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey 
            // method.
            public bool EditingControlWantsInputKey(
                Keys key, bool dataGridViewWantsInputKey)
            {
                // Let the DateTimePicker handle the keys listed.
                switch (key & Keys.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Right:
                    case Keys.Home:
                    case Keys.End:
                    case Keys.PageDown:
                    case Keys.PageUp:
                        return true;
                    default:
                        return !dataGridViewWantsInputKey;
                }
            }

            // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit 
            // method.
            public void PrepareEditingControlForEdit(bool selectAll)
            {
                // No preparation needs to be done.
            }

            // Implements the IDataGridViewEditingControl
            // .RepositionEditingControlOnValueChange property.
            public bool RepositionEditingControlOnValueChange
            {
                get
                {
                    return false;
                }
            }

            // Implements the IDataGridViewEditingControl
            // .EditingControlDataGridView property.
            public DataGridView EditingControlDataGridView
            {
                get
                {
                    return dataGridView;
                }
                set
                {
                    dataGridView = value;
                }
            }

            // Implements the IDataGridViewEditingControl
            // .EditingControlValueChanged property.
            public bool EditingControlValueChanged
            {
                get
                {
                    return valueChanged;
                }
                set
                {
                    valueChanged = value;
                }
            }

            // Implements the IDataGridViewEditingControl
            // .EditingPanelCursor property.
            public Cursor EditingPanelCursor
            {
                get
                {
                    return base.Cursor;
                }
            }

            protected override void OnValueChanged(EventArgs eventargs)
            {
                // Notify the DataGridView that the contents of the cell
                // have changed.
                valueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
                base.OnValueChanged(eventargs);
            }
        }
    }
}
