using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUB
{
    public partial class MoveOut : Form
    {
        int parkId, parkSpaceId;
        public bool newTenant = false;
        public String spaceId;
        public int orderId;
        public MoveOut(int parkId)
        {
            InitializeComponent();
            this.parkId = parkId;
            this.parkSpaceId = -1;
        }

        public MoveOut(int parkId, int parkSpaceId) {
            InitializeComponent();
            this.parkId = parkId;
            this.parkSpaceId = parkSpaceId;
        }

        private void MoveOut_Load(object sender, EventArgs e)
        {
            DatabaseControl.populateComboBox(ref tenantList, DatabaseControl.spaceTable, "SpaceID", "Tenant", "ParkSpaceID", "ParkID=@value0 AND MoveOutDate IS NULL", new Object[] { parkId });
            if (parkSpaceId == -1) {
                saveBtn.Enabled = false;
                label1.Visible = true;
            } else {
                Object[] info = DatabaseControl.getSingleRecord(new String[] { "SpaceID", "Tenant" }, DatabaseControl.spaceTable, "ParkSpaceID=@value0", new Object[] { parkSpaceId });
                tenantList.Text = info[0].ToString() + ':' + info[1].ToString();
                tenantList.Enabled = false;
                label1.Visible = false;
                saveBtn.Enabled = true;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            DatabaseControl.executeUpdateQuery(DatabaseControl.spaceTable, new String[] { "MoveOutDate" }, new Object[] { moveOutDate.Value.Date },
                "ParkSpaceID="+parkSpaceId);
            DialogResult result = MessageBox.Show("Do you want to input a new tenant into the space?", "Confirmation", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes) {
                Object[] tempInfo = DatabaseControl.getSingleRecord(new String[] { "SpaceID", "OrderID" }, DatabaseControl.spaceTable, 
                    "ParkSpaceID=@value0", new Object[] { parkSpaceId });
                spaceId = tempInfo[0].ToString();
                orderId = (int)tempInfo[1];
                newTenant = true;
            } else {
            } 
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            parkSpaceId = ((CommonTools.Item)tenantList.SelectedItem).Value;
            saveBtn.Enabled = true;
        }
    }
}
