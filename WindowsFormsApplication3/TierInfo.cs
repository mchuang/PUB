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
    public partial class TierInfo : Form
    {
        public TierInfo()
        {
            InitializeComponent();
        }

        private void TierInfo_Load(object sender, EventArgs e)
        {
            DatabaseControl.populateComboBox(ref tierList, DatabaseControl.tierTable, "TierSetName", "TierSetID");
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tierList.SelectedIndex != -1)
            {
                String condition = "TierSetID=" + ((CommonTools.Item)tierList.SelectedItem).Value;
                String[] columns = { "Tier1", "Tier2", "Tier3", "Tier4", "Tier5" };
                Object[] values = { Convert.ToDecimal(tier1.Text), Convert.ToDecimal(tier2.Text), Convert.ToDecimal(tier3.Text),
                                      Convert.ToDecimal(tier4.Text), Convert.ToDecimal(tier5.Text) };
                DatabaseControl.executeUpdateQuery(DatabaseControl.tierTable, columns, values, condition);
            }
            else
            {
                String[] fields = { "TierSetName", "Tier1", "Tier2", "Tier3", "Tier4", "Tier5" };
                Object[] values = { tierList.Text, Convert.ToDecimal(tier1.Text), Convert.ToDecimal(tier2.Text), Convert.ToDecimal(tier3.Text),
                                      Convert.ToDecimal(tier4.Text), Convert.ToDecimal(tier5.Text) };
                DatabaseControl.executeInsertQuery(DatabaseControl.tierTable, fields, values);
            }
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tierList_SelectedIndexChanged(object sender, EventArgs e)
        {
            String condition = "TierSetID=" + ((CommonTools.Item)tierList.SelectedItem).Value;
            String[] fields = { "Tier1", "Tier2", "Tier3", "Tier4", "Tier5" };
            Object[] vals = DatabaseControl.getSingleRecord(fields, DatabaseControl.tierTable, condition);
            tier1.Text = vals[0].ToString();
            tier2.Text = vals[1].ToString();
            tier3.Text = vals[2].ToString();
            tier4.Text = vals[3].ToString();
            tier5.Text = vals[4].ToString();
        }
    }
}
