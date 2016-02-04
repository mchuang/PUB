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
        int parkId;
        public MoveOut(int parkId)
        {
            InitializeComponent();
            this.parkId = parkId;
        }

        private void MoveOut_Load(object sender, EventArgs e)
        {
            DatabaseControl.populateComboBox(ref tenantList, DatabaseControl.spaceTable, "ParkSpaceID", "SpaceID", "Tenant", "ParkID=@value0", new Object[] {parkId});
            saveBtn.Enabled = false;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            CommonTools.Item tenant = (CommonTools.Item)tenantList.SelectedItem;
            DatabaseControl.executeUpdateQuery(DatabaseControl.spaceTable, new String[] { "MoveOutDate" }, new Object[] { moveOutDate.Value.Date },
                "ParkSpaceID="+tenant.Value);
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tenantList_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveBtn.Enabled = true;
        }
    }
}
