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
using System.Data.SqlClient;

namespace PUB
{
    public partial class MeterReads : Form
    {
        int parkID, parkSpaceID;
        string spaceID;
        List<CommonTools.Read> newReads;
        List<Object[]> oldReads;
        private int readIndex;

        public MeterReads()
        {
            InitializeComponent();
        }

        private void MeterReads_Load(object sender, EventArgs e)
        {
            DatabaseControl.populateComboBox(ref parkList, DatabaseControl.parkTable, "ParkNumber", "ParkID");
            spaceList.Enabled = false;
            gasValue.Enabled = false;
            eleValue.Enabled = false;
            watValue.Enabled = false;
            readDate.Enabled = false;
            prevReadDate.Enabled = false;
        }
        
        private void parkNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.parkID = ((CommonTools.Item)parkList.SelectedItem).Value;
            spaceList.Items.Clear();
            DatabaseControl.populateComboBox(ref spaceList, DatabaseControl.spaceTable, "SpaceID", "Tenant", "ParkSpaceID", "ParkID=@value0 ORDER BY SpaceID ASC", new Object[] { this.parkID });
            spaceList.Enabled = true;
        }

        private void spaceNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newReads != null && newReads.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Do you wish to save your changes?", "Save Changes", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    commitReads();
                }
            }
            this.parkSpaceID = ((CommonTools.Item)spaceList.SelectedItem).Value;
            this.spaceID = DatabaseControl.getSingleRecord(new String[] { "SpaceID" }, DatabaseControl.spaceTable,
            "ParkSpaceID=@value0", new Object[] { this.parkSpaceID })[0].ToString();
            newReads = new List<CommonTools.Read>();
            loadMeterReads();
            gasValue.Enabled = true;
            eleValue.Enabled = true;
            watValue.Enabled = true;
            readDate.Enabled = true;
            prevReadDate.Enabled = true;          
        }

        private void loadMeterReads()
        {
            oldReads = DatabaseControl.getMultipleRecord(new String[] { "MeterReadID", "ParkID", "SpaceID", "MeterReadDate", "GasReadValue",  "EleReadValue",  "WatReadValue","StartDate" }, DatabaseControl.meterReadsTable,
            "ParkID=@value0 and SpaceID=@value1", new Object[] {this.parkID, this.spaceID });
            readIndex = oldReads.Count - 1;
            displayRead();
        }

        private void displayRead()
        {
            if (readIndex >= 0 && readIndex < oldReads.Count)
            {
                Object[] read = (Object[])oldReads[readIndex];
                this.parkID    = (int)read[1];
                this.spaceID   = (string)read[2];
                readDate.Value = (DateTime)read[3];
                gasValue.Value = (int)read[4];
                eleValue.Value = (int)read[5];
                watValue.Value = (int)read[6];
                prevReadDate.Value = (DateTime)read[7];
            }
            else
            {
                readDate.Value = DateTime.Today.Date;
                gasValue.Value = 0; 
                eleValue.Value = 0; 
                watValue.Value = 0;
                prevReadDate.Value = DateTime.Today.Date;
            }
        }

        private void saveTempReads() 
        {
            if (readIndex >= oldReads.Count)
            {
                newReads.Add(new CommonTools.Read((int)parkID, (string)spaceID, (DateTime)readDate.Value, (int)gasValue.Value, (int)eleValue.Value, (int)watValue.Value, (DateTime)prevReadDate.Value));
            }
            else
            {
                Object[] read = (Object[])oldReads[readIndex];
                int readId = (int)read[0];
                newReads.Add(new CommonTools.Read((int)parkID, (string)spaceID, (DateTime)readDate.Value, (int)gasValue.Value, (int)eleValue.Value, (int)watValue.Value, (DateTime)prevReadDate.Value, readId));
            }
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            saveTempReads();
            if (readIndex < oldReads.Count-1)
            {
                readIndex += 1;
                displayRead();
            }
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            saveTempReads();
            if (readIndex > 0)
            {
                readIndex -= 1;
                displayRead();
            }
        }

        private void commitReads()
        {
            foreach (CommonTools.Read read in newReads)
            {
                if (read.readId == -1)
                {
                    DatabaseControl.executeInsertQuery(DatabaseControl.meterReadsTable, new String[] { "ParkID", "SpaceID", "MeterReadDate", "GasReadValue", "EleReadValue", "WatReadValue", "StartDate" },
                        new Object[] { read.parkID, read.spaceID, read.readDate, read.gasValue, read.eleValue,  read.watValue, read.prevReadDate});
                }
                else
                {
                    DatabaseControl.executeUpdateQuery(DatabaseControl.meterReadsTable, new String[] { "MeterReadDate", "GasReadValue", "EleReadValue", "WatReadValue", "StartDate" },
                        new Object[] { read.readDate, read.gasValue, read.eleValue, read.watValue, read.prevReadDate }, "MeterReadId=" + read.readId);
                }
            }
            newReads = new List<CommonTools.Read>();
            MessageBox.Show("Data saved");
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveTempReads();
            commitReads();
            loadMeterReads();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            readIndex = oldReads.Count;
            readDate.Value = DateTime.Today.Date;
            gasValue.Value = 0; 
            eleValue.Value = 0; 
            watValue.Value = 0; 
        }
    }
}
