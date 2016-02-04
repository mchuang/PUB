namespace PUB
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ownerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.electricityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meterReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMeterReadsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.billingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(708, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ownerToolStripMenuItem,
            this.parkToolStripMenuItem,
            this.tenantToolStripMenuItem,
            this.tenantToolStripMenuItem1,
            this.tierToolStripMenuItem,
            this.utilityToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // ownerToolStripMenuItem
            // 
            this.ownerToolStripMenuItem.Name = "ownerToolStripMenuItem";
            this.ownerToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.ownerToolStripMenuItem.Text = "Owner";
            this.ownerToolStripMenuItem.Click += new System.EventHandler(this.ownerToolStripMenuItem_Click);
            // 
            // parkToolStripMenuItem
            // 
            this.parkToolStripMenuItem.Name = "parkToolStripMenuItem";
            this.parkToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.parkToolStripMenuItem.Text = "Park";
            this.parkToolStripMenuItem.Click += new System.EventHandler(this.parkToolStripMenuItem_Click);
            // 
            // tenantToolStripMenuItem
            // 
            this.tenantToolStripMenuItem.Name = "tenantToolStripMenuItem";
            this.tenantToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.tenantToolStripMenuItem.Text = "ParkSpace";
            this.tenantToolStripMenuItem.Click += new System.EventHandler(this.parkSpaceToolStripMenuItem_Click);
            // 
            // tenantToolStripMenuItem1
            // 
            this.tenantToolStripMenuItem1.Name = "tenantToolStripMenuItem1";
            this.tenantToolStripMenuItem1.Size = new System.Drawing.Size(145, 24);
            this.tenantToolStripMenuItem1.Text = "Tenant";
            this.tenantToolStripMenuItem1.Click += new System.EventHandler(this.tenantToolStripMenuItem1_Click);
            // 
            // tierToolStripMenuItem
            // 
            this.tierToolStripMenuItem.Name = "tierToolStripMenuItem";
            this.tierToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.tierToolStripMenuItem.Text = "Tier";
            this.tierToolStripMenuItem.Click += new System.EventHandler(this.tierToolStripMenuItem_Click);
            // 
            // utilityToolStripMenuItem
            // 
            this.utilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gasToolStripMenuItem,
            this.electricityToolStripMenuItem,
            this.waterToolStripMenuItem});
            this.utilityToolStripMenuItem.Name = "utilityToolStripMenuItem";
            this.utilityToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.utilityToolStripMenuItem.Text = "Utility";
            // 
            // gasToolStripMenuItem
            // 
            this.gasToolStripMenuItem.Name = "gasToolStripMenuItem";
            this.gasToolStripMenuItem.Size = new System.Drawing.Size(142, 24);
            this.gasToolStripMenuItem.Text = "Gas";
            this.gasToolStripMenuItem.Click += new System.EventHandler(this.gasToolStripMenuItem_Click);
            // 
            // electricityToolStripMenuItem
            // 
            this.electricityToolStripMenuItem.Name = "electricityToolStripMenuItem";
            this.electricityToolStripMenuItem.Size = new System.Drawing.Size(142, 24);
            this.electricityToolStripMenuItem.Text = "Electricity";
            this.electricityToolStripMenuItem.Click += new System.EventHandler(this.electricityToolStripMenuItem_Click);
            // 
            // waterToolStripMenuItem
            // 
            this.waterToolStripMenuItem.Name = "waterToolStripMenuItem";
            this.waterToolStripMenuItem.Size = new System.Drawing.Size(142, 24);
            this.waterToolStripMenuItem.Text = "Water";
            this.waterToolStripMenuItem.Click += new System.EventHandler(this.waterToolStripMenuItem_Click);
            // 
            // billingToolStripMenuItem
            // 
            this.billingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importMeterReadsFileToolStripMenuItem,
            this.meterReadsToolStripMenuItem,
            this.createBillToolStripMenuItem});
            this.billingToolStripMenuItem.Name = "billingToolStripMenuItem";
            this.billingToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.billingToolStripMenuItem.Text = "Billing";
            // 
            // meterReadsToolStripMenuItem
            // 
            this.meterReadsToolStripMenuItem.Name = "meterReadsToolStripMenuItem";
            this.meterReadsToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.meterReadsToolStripMenuItem.Text = "Meter Reads";
            this.meterReadsToolStripMenuItem.Click += new System.EventHandler(this.meterReadsToolStripMenuItem_Click);
            // 
            // createBillToolStripMenuItem
            // 
            this.createBillToolStripMenuItem.Name = "createBillToolStripMenuItem";
            this.createBillToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.createBillToolStripMenuItem.Text = "Create Bill";
            this.createBillToolStripMenuItem.Click += new System.EventHandler(this.createBillToolStripMenuItem_Click);
            // 
            // importMeterReadsFileToolStripMenuItem
            // 
            this.importMeterReadsFileToolStripMenuItem.Name = "importMeterReadsFileToolStripMenuItem";
            this.importMeterReadsFileToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.importMeterReadsFileToolStripMenuItem.Text = "Import Meter Reads File";
            this.importMeterReadsFileToolStripMenuItem.Click += new System.EventHandler(this.importMeterReadsFileToolStripMenuItem_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 378);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ownerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenantToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem electricityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem billingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meterReadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createBillToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMeterReadsFileToolStripMenuItem;
    }
}