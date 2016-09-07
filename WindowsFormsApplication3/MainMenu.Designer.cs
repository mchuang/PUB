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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataMigrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.importMeterReadsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meterReadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meterReadInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCollectionsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
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
            this.menuStrip1.Size = new System.Drawing.Size(531, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserToolStripMenuItem,
            this.resetPasswordToolStripMenuItem,
            this.disableUserToolStripMenuItem,
            this.dataMigrationToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.addUserToolStripMenuItem.Text = "Add User";
            this.addUserToolStripMenuItem.Click += new System.EventHandler(this.addUserToolStripMenuItem_Click);
            // 
            // resetPasswordToolStripMenuItem
            // 
            this.resetPasswordToolStripMenuItem.Name = "resetPasswordToolStripMenuItem";
            this.resetPasswordToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.resetPasswordToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.resetPasswordToolStripMenuItem.Text = "Reset Password";
            this.resetPasswordToolStripMenuItem.Click += new System.EventHandler(this.resetPasswordToolStripMenuItem_Click);
            // 
            // disableUserToolStripMenuItem
            // 
            this.disableUserToolStripMenuItem.Name = "disableUserToolStripMenuItem";
            this.disableUserToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.disableUserToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.disableUserToolStripMenuItem.Text = "Disable User";
            this.disableUserToolStripMenuItem.Click += new System.EventHandler(this.disableUserToolStripMenuItem_Click);
            // 
            // dataMigrationToolStripMenuItem
            // 
            this.dataMigrationToolStripMenuItem.Name = "dataMigrationToolStripMenuItem";
            this.dataMigrationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.dataMigrationToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.dataMigrationToolStripMenuItem.Text = "Data Migration";
            this.dataMigrationToolStripMenuItem.Click += new System.EventHandler(this.dataMigrationToolStripMenuItem_Click);
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
            this.editToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // ownerToolStripMenuItem
            // 
            this.ownerToolStripMenuItem.Name = "ownerToolStripMenuItem";
            this.ownerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.ownerToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.ownerToolStripMenuItem.Text = "Owner";
            this.ownerToolStripMenuItem.Click += new System.EventHandler(this.ownerToolStripMenuItem_Click);
            // 
            // parkToolStripMenuItem
            // 
            this.parkToolStripMenuItem.Name = "parkToolStripMenuItem";
            this.parkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.parkToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.parkToolStripMenuItem.Text = "Park";
            this.parkToolStripMenuItem.Click += new System.EventHandler(this.parkToolStripMenuItem_Click);
            // 
            // tenantToolStripMenuItem
            // 
            this.tenantToolStripMenuItem.Name = "tenantToolStripMenuItem";
            this.tenantToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.tenantToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.tenantToolStripMenuItem.Text = "ParkSpace";
            this.tenantToolStripMenuItem.Click += new System.EventHandler(this.parkSpaceToolStripMenuItem_Click);
            // 
            // tenantToolStripMenuItem1
            // 
            this.tenantToolStripMenuItem1.Name = "tenantToolStripMenuItem1";
            this.tenantToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.tenantToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
            this.tenantToolStripMenuItem1.Text = "Tenant";
            this.tenantToolStripMenuItem1.Click += new System.EventHandler(this.tenantToolStripMenuItem1_Click);
            // 
            // tierToolStripMenuItem
            // 
            this.tierToolStripMenuItem.Name = "tierToolStripMenuItem";
            this.tierToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.tierToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
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
            this.utilityToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Y)));
            this.utilityToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.utilityToolStripMenuItem.Text = "Utility";
            this.utilityToolStripMenuItem.Click += new System.EventHandler(this.utilityToolStripMenuItem_Click);
            // 
            // gasToolStripMenuItem
            // 
            this.gasToolStripMenuItem.Name = "gasToolStripMenuItem";
            this.gasToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.gasToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.gasToolStripMenuItem.Text = "Gas";
            this.gasToolStripMenuItem.Click += new System.EventHandler(this.gasToolStripMenuItem_Click);
            // 
            // electricityToolStripMenuItem
            // 
            this.electricityToolStripMenuItem.Name = "electricityToolStripMenuItem";
            this.electricityToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.electricityToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.electricityToolStripMenuItem.Text = "Electricity";
            this.electricityToolStripMenuItem.Click += new System.EventHandler(this.electricityToolStripMenuItem_Click);
            // 
            // waterToolStripMenuItem
            // 
            this.waterToolStripMenuItem.Name = "waterToolStripMenuItem";
            this.waterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
            this.waterToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.waterToolStripMenuItem.Text = "Water";
            this.waterToolStripMenuItem.Click += new System.EventHandler(this.waterToolStripMenuItem_Click);
            // 
            // billingToolStripMenuItem
            // 
            this.billingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importMeterReadsFileToolStripMenuItem,
            this.meterReadsToolStripMenuItem,
            this.meterReadInputToolStripMenuItem,
            this.exportCollectionsFileToolStripMenuItem,
            this.createBillToolStripMenuItem});
            this.billingToolStripMenuItem.Name = "billingToolStripMenuItem";
            this.billingToolStripMenuItem.ShortcutKeyDisplayString = "Alt+B";
            this.billingToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.billingToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.billingToolStripMenuItem.Text = "Billing";
            // 
            // importMeterReadsFileToolStripMenuItem
            // 
            this.importMeterReadsFileToolStripMenuItem.Name = "importMeterReadsFileToolStripMenuItem";
            this.importMeterReadsFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.importMeterReadsFileToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.importMeterReadsFileToolStripMenuItem.Text = "Import Meter Reads File";
            this.importMeterReadsFileToolStripMenuItem.Click += new System.EventHandler(this.importMeterReadsFileToolStripMenuItem_Click);
            // 
            // meterReadsToolStripMenuItem
            // 
            this.meterReadsToolStripMenuItem.Name = "meterReadsToolStripMenuItem";
            this.meterReadsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.meterReadsToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.meterReadsToolStripMenuItem.Text = "Export Meter Reads File";
            this.meterReadsToolStripMenuItem.Click += new System.EventHandler(this.meterReadsToolStripMenuItem_Click);
            // 
            // meterReadInputToolStripMenuItem
            // 
            this.meterReadInputToolStripMenuItem.Name = "meterReadInputToolStripMenuItem";
            this.meterReadInputToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.meterReadInputToolStripMenuItem.Text = "Meter Read Input";
            this.meterReadInputToolStripMenuItem.Click += new System.EventHandler(this.meterReadInputToolStripMenuItem_Click);
            // 
            // exportCollectionsFileToolStripMenuItem
            // 
            this.exportCollectionsFileToolStripMenuItem.Name = "exportCollectionsFileToolStripMenuItem";
            this.exportCollectionsFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.exportCollectionsFileToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.exportCollectionsFileToolStripMenuItem.Text = "Export Collections File";
            this.exportCollectionsFileToolStripMenuItem.Click += new System.EventHandler(this.exportCollectionsFileToolStripMenuItem_Click);
            // 
            // createBillToolStripMenuItem
            // 
            this.createBillToolStripMenuItem.Name = "createBillToolStripMenuItem";
            this.createBillToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.createBillToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.createBillToolStripMenuItem.Text = "Create Bill";
            this.createBillToolStripMenuItem.Click += new System.EventHandler(this.createBillToolStripMenuItem_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(205, 280);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(211, 27);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(320, 280);
            this.richTextBox2.TabIndex = 2;
            this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 307);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
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
        private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataMigrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCollectionsFileToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ToolStripMenuItem meterReadInputToolStripMenuItem;
    }
}