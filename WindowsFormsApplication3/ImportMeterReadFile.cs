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

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    if (fields.Length == 1) break; 
                    int count =0;
                    foreach (string field in fields)
                    {
                        if (field == "") { fields[count] = null; }
                        count = count +1;
                    }
                    int SpaceID = Convert.ToInt32(fields[0]);
                    int gasReadValue = Convert.ToInt32(fields[1]);
                    int EleReadValue = Convert.ToInt32(fields[2]);
                    int WatReadValue = Convert.ToInt32(fields[3]);
                    int GasReadValueOld = Convert.ToInt32(fields[4]);
                    int EleReadValueOld = Convert.ToInt32(fields[5]);
                    int WatReadValueOld = Convert.ToInt32(fields[6]);
                    int GasPrevMonthUsage = Convert.ToInt32(fields[7]);
                    int ElePrevMonthUsage = Convert.ToInt32(fields[8]);
                    int WatPrevMonthUsage = Convert.ToInt32(fields[9]);
                    int GasPrevYearUsage = Convert.ToInt32(fields[10]);
                    int ElePrevYearUsage = Convert.ToInt32(fields[11]);
                    int WatPrevYearUsage = Convert.ToInt32(fields[12]);
                    string GasMeterLoc = fields[13];
                    string EleMeterLoc = fields[14];
                    string WatMeterLoc = fields[15];
                    string SpecialAccess = fields[16];
                    int MeterReadEmployeeID = Convert.ToInt32(fields[17]);
                    DateTime MeterReadTime = Convert.ToDateTime(fields[18]);
                    Object[] meterValues = 
                    {   this.parkID, 
                        SpaceID,
                        readDate.Value,
                        gasReadValue,
                        EleReadValue,
                        WatReadValue,
                        GasReadValueOld,
                        EleReadValueOld,
                        WatReadValueOld,
                        GasPrevMonthUsage,
                        ElePrevMonthUsage,
                        WatPrevMonthUsage,
                        GasPrevYearUsage,
                        ElePrevYearUsage,
                        WatPrevYearUsage,
                        GasMeterLoc,
                        EleMeterLoc,
                        WatMeterLoc,
                        SpecialAccess,
                        MeterReadEmployeeID,
                        MeterReadTime};
                    DatabaseControl.executeInsertQuery(DatabaseControl.meterReadsTable,DatabaseControl.meterReadsColumns,meterValues);
                }
                MessageBox.Show("File imported");
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
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkNumber", "ParkID");
            inputFile.Text = "";
        }

        private void parkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.parkID = ((CommonTools.Item)parkList.SelectedItem).Value;
        }
    }
}
