using System;
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
    public partial class MainMenu : Form
    {

        public MainMenu()
        {
            InitializeComponent();
        }

        private void newUtility_Click(object sender, EventArgs e)
        {
            this.Hide();
            UtilityGas0 newUtility = new UtilityGas0();
            newUtility.ShowDialog();
            this.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            /*
            cnn = DatabaseControl.Connect(); 
            try { 
                cnn.Open(); 
                MessageBox.Show ("Connection Established!");
                cnn.Close();
            } catch (Exception ex) { 
                MessageBox.Show(ex.ToString()); 
            }*/
        }

        private void ownerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            OwnerInfo newOwner = new OwnerInfo();
            newOwner.ShowDialog();
            this.Show();
        }

        private void parkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ParkInfo newPark = new ParkInfo();
            newPark.ShowDialog();
            this.Show();
        }

        private void parkSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ParkSpaceInfo newTenant = new ParkSpaceInfo();
            newTenant.ShowDialog();
            this.Show();
        }

        private void tenantToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TenantInfo newTenant = new TenantInfo();
            newTenant.ShowDialog();
            this.Show();
        }

        private void gasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            UtilityGas0 newTenant = new UtilityGas0();
            newTenant.ShowDialog();
            this.Show();
        }

        private void electricityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            UtilityElectricity0 newTenant = new UtilityElectricity0();
            newTenant.ShowDialog();
            this.Show();
        }

        private void waterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            UtilityWater newTenant = new UtilityWater();
            newTenant.ShowDialog();
            this.Show();
        }

        private void meterReadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            MeterReads reads = new MeterReads();
            reads.ShowDialog();
            this.Show();
        }

        private void createBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TenantBill bill = new TenantBill();
            bill.ShowDialog();
        }

        private void tierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TierInfo tier = new TierInfo();
            tier.ShowDialog();
        }

        private void importMeterReadsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ImportMeterReadFile import = new ImportMeterReadFile();
            import.ShowDialog();
            this.Show();
        }

    }
}
