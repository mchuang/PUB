using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;




namespace PUB
{
    public partial class ImportMeterReadFile : Form
    {
        int parkID;
        public ImportMeterReadFile()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            using (TextFieldParser csvParser = new TextFieldParser(inputFile.Text))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row if the first row has the column names
                //csvParser.ReadLine();
                DateTime dueDate = dueDateInput.Value.Date;
                decimal thermX = Convert.ToDecimal(thermXInput.Text);
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    if (fields.Length == 1) continue; 
                    int count =0;
                    foreach (string field in fields)
                    {
                        if (field == "") { fields[count] = null; }
                        count = count +1;
                    }
                    try
                    {
                        int SpaceID = Convert.ToInt32(fields[0]);
                        int gasReadValue = Convert.ToInt32(fields[2]);
                        int EleReadValue = Convert.ToInt32(fields[1]);
                        int WatReadValue = Convert.ToInt32(fields[3]);

                        int MeterReadEmployeeID = Convert.ToInt32(fields[17]);
                        DateTime MeterReadTime = Convert.ToDateTime(fields[18]);

                        Object[] meterValues = 
                        {this.parkID, 
                        SpaceID,
                        prevReadDate.Value,
                        readDate.Value,
                        gasReadValue,
                        EleReadValue,
                        WatReadValue,
                       
                        MeterReadEmployeeID,
                        MeterReadTime,
                        dueDate,
                        thermX};
                        DatabaseControl.executeInsertQueryNoID(DatabaseControl.meterReadsTable, DatabaseControl.meterReadsColumns, meterValues);
                        
                        
                      
                    }
                        
                    catch { }
                    
                }
                DatabaseControl.executeInsertQueryNoID(DatabaseControl.periodTable, DatabaseControl.periodColumns,
                        new Object[] { this.parkID, prevReadDate.Value, readDate.Value, dueDate });
                MessageBox.Show("File imported");
                inputFile.Text = "";
            }
        }

        private void inputFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inputFile.Text = openFileDialog1.FileName;
            }
        }

        private void ImportMeterReadFile_Load(object sender, EventArgs e)
        {
            //DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkNumber", "ParkID");
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable + " ORDER BY ParkNumber", "ParkNumber", "ParkID");
            inputFile.Text = "";
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.parkID = ((CommonTools.Item)parkList.SelectedItem).Value;
            
            Object[] values = DatabaseControl.getSingleRecord(DatabaseControl.meterReadsColumns, DatabaseControl.meterReadsTable, "ParkID=" + ((CommonTools.Item)parkList.SelectedItem).Value+
                " order by meterReadDate desc");
            try
            {
                prevReadDate.Text = values[3].ToString();
                prevThermX.Text = values[10].ToString();
            }
            catch
            {
                DateTime thisDay  = DateTime.Today;
                prevReadDate.Text = thisDay.ToString ();
            }
         }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void readDate_ValueChanged(object sender, EventArgs e) {
            billingDays.Text = ((int)(readDate.Value - prevReadDate.Value).TotalDays).ToString();
        }
    }
}
