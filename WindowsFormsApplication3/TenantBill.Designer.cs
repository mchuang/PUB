namespace PUB
{
    partial class TenantBill
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
            this.parkList = new System.Windows.Forms.ComboBox();
            this.tenantList = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.billBtn = new System.Windows.Forms.Button();
            this.eleInfo = new System.Windows.Forms.RichTextBox();
            this.gasInfo = new System.Windows.Forms.RichTextBox();
            this.watInfo = new System.Windows.Forms.RichTextBox();
            this.tenantInfo = new System.Windows.Forms.RichTextBox();
            this.clerkInfo = new System.Windows.Forms.RichTextBox();
            this.usageInfo = new System.Windows.Forms.RichTextBox();
            this.readInfo = new System.Windows.Forms.RichTextBox();
            this.historyBtn = new System.Windows.Forms.Button();
            this.summaryOfCharges = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.readDate = new System.Windows.Forms.ComboBox();
            this.tempCharges = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.parkReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parkBillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parkReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.utilityReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.readSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eCReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gasHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eleHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.watHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveOut = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.spaceList = new System.Windows.Forms.ComboBox();
            this.topMessageInput = new System.Windows.Forms.RichTextBox();
            this.botMessageInput = new System.Windows.Forms.RichTextBox();
            this.readIssuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.summaryOfCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempCharges)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // parkList
            // 
            this.parkList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.parkList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.parkList.FormattingEnabled = true;
            this.parkList.Location = new System.Drawing.Point(96, 29);
            this.parkList.Name = "parkList";
            this.parkList.Size = new System.Drawing.Size(156, 21);
            this.parkList.TabIndex = 1;
            this.parkList.SelectedIndexChanged += new System.EventHandler(this.parkList_SelectedIndexChanged);
            // 
            // tenantList
            // 
            this.tenantList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tenantList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tenantList.FormattingEnabled = true;
            this.tenantList.Location = new System.Drawing.Point(555, 30);
            this.tenantList.Name = "tenantList";
            this.tenantList.Size = new System.Drawing.Size(156, 21);
            this.tenantList.TabIndex = 3;
            this.tenantList.SelectedIndexChanged += new System.EventHandler(this.tenantList_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 17);
            this.label8.TabIndex = 158;
            this.label8.Text = "Park";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(496, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 157;
            this.label1.Text = "Tenant";
            // 
            // billBtn
            // 
            this.billBtn.Location = new System.Drawing.Point(810, 662);
            this.billBtn.Name = "billBtn";
            this.billBtn.Size = new System.Drawing.Size(116, 37);
            this.billBtn.TabIndex = 159;
            this.billBtn.Text = "Tenant Bill";
            this.billBtn.UseVisualStyleBackColor = true;
            this.billBtn.Visible = false;
            this.billBtn.Click += new System.EventHandler(this.billBtn_Click);
            // 
            // eleInfo
            // 
            this.eleInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eleInfo.Location = new System.Drawing.Point(320, 199);
            this.eleInfo.Name = "eleInfo";
            this.eleInfo.ReadOnly = true;
            this.eleInfo.Size = new System.Drawing.Size(300, 244);
            this.eleInfo.TabIndex = 167;
            this.eleInfo.Text = "";
            // 
            // gasInfo
            // 
            this.gasInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gasInfo.Location = new System.Drawing.Point(12, 199);
            this.gasInfo.Name = "gasInfo";
            this.gasInfo.ReadOnly = true;
            this.gasInfo.Size = new System.Drawing.Size(300, 244);
            this.gasInfo.TabIndex = 168;
            this.gasInfo.Text = "";
            // 
            // watInfo
            // 
            this.watInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.watInfo.Location = new System.Drawing.Point(626, 199);
            this.watInfo.Name = "watInfo";
            this.watInfo.ReadOnly = true;
            this.watInfo.Size = new System.Drawing.Size(300, 244);
            this.watInfo.TabIndex = 169;
            this.watInfo.Text = "";
            // 
            // tenantInfo
            // 
            this.tenantInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenantInfo.Location = new System.Drawing.Point(14, 56);
            this.tenantInfo.Name = "tenantInfo";
            this.tenantInfo.ReadOnly = true;
            this.tenantInfo.Size = new System.Drawing.Size(450, 50);
            this.tenantInfo.TabIndex = 170;
            this.tenantInfo.Text = "";
            // 
            // clerkInfo
            // 
            this.clerkInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clerkInfo.Location = new System.Drawing.Point(476, 56);
            this.clerkInfo.Name = "clerkInfo";
            this.clerkInfo.ReadOnly = true;
            this.clerkInfo.Size = new System.Drawing.Size(450, 50);
            this.clerkInfo.TabIndex = 171;
            this.clerkInfo.Text = "";
            // 
            // usageInfo
            // 
            this.usageInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usageInfo.Location = new System.Drawing.Point(14, 112);
            this.usageInfo.Name = "usageInfo";
            this.usageInfo.ReadOnly = true;
            this.usageInfo.Size = new System.Drawing.Size(912, 25);
            this.usageInfo.TabIndex = 172;
            this.usageInfo.Text = "";
            // 
            // readInfo
            // 
            this.readInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readInfo.Location = new System.Drawing.Point(14, 143);
            this.readInfo.Name = "readInfo";
            this.readInfo.ReadOnly = true;
            this.readInfo.Size = new System.Drawing.Size(912, 50);
            this.readInfo.TabIndex = 173;
            this.readInfo.Text = "";
            // 
            // historyBtn
            // 
            this.historyBtn.Location = new System.Drawing.Point(810, 619);
            this.historyBtn.Name = "historyBtn";
            this.historyBtn.Size = new System.Drawing.Size(116, 37);
            this.historyBtn.TabIndex = 174;
            this.historyBtn.Text = "History";
            this.historyBtn.UseVisualStyleBackColor = true;
            this.historyBtn.Visible = false;
            this.historyBtn.Click += new System.EventHandler(this.historyBtn_Click);
            // 
            // summaryOfCharges
            // 
            this.summaryOfCharges.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.summaryOfCharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.summaryOfCharges.Location = new System.Drawing.Point(12, 543);
            this.summaryOfCharges.Name = "summaryOfCharges";
            this.summaryOfCharges.Size = new System.Drawing.Size(300, 165);
            this.summaryOfCharges.TabIndex = 175;
            this.summaryOfCharges.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TenantBill_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(744, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 177;
            this.label2.Text = "Due Date";
            // 
            // readDate
            // 
            this.readDate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.readDate.FormattingEnabled = true;
            this.readDate.Location = new System.Drawing.Point(826, 30);
            this.readDate.Name = "readDate";
            this.readDate.Size = new System.Drawing.Size(100, 21);
            this.readDate.TabIndex = 4;
            this.readDate.SelectedIndexChanged += new System.EventHandler(this.readDate_SelectedIndexChanged);
            // 
            // tempCharges
            // 
            this.tempCharges.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tempCharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tempCharges.Location = new System.Drawing.Point(318, 543);
            this.tempCharges.Name = "tempCharges";
            this.tempCharges.Size = new System.Drawing.Size(300, 165);
            this.tempCharges.TabIndex = 178;
            this.tempCharges.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TenantBill_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parkReportToolStripMenuItem,
            this.tenantToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(954, 24);
            this.menuStrip1.TabIndex = 181;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // parkReportToolStripMenuItem
            // 
            this.parkReportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parkBillsToolStripMenuItem,
            this.parkReportToolStripMenuItem1,
            this.utilityReportToolStripMenuItem1,
            this.readSheetToolStripMenuItem,
            this.eCReportToolStripMenuItem,
            this.collectionsToolStripMenuItem,
            this.readIssuesToolStripMenuItem});
            this.parkReportToolStripMenuItem.Name = "parkReportToolStripMenuItem";
            this.parkReportToolStripMenuItem.ShortcutKeyDisplayString = "Alt+0";
            this.parkReportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D0)));
            this.parkReportToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.parkReportToolStripMenuItem.Text = "Report";
            this.parkReportToolStripMenuItem.Visible = false;
            // 
            // parkBillsToolStripMenuItem
            // 
            this.parkBillsToolStripMenuItem.Name = "parkBillsToolStripMenuItem";
            this.parkBillsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
            this.parkBillsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.parkBillsToolStripMenuItem.Text = "Park Bills";
            this.parkBillsToolStripMenuItem.Click += new System.EventHandler(this.parkBillsToolStripMenuItem_Click);
            // 
            // parkReportToolStripMenuItem1
            // 
            this.parkReportToolStripMenuItem1.Name = "parkReportToolStripMenuItem1";
            this.parkReportToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
            this.parkReportToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.parkReportToolStripMenuItem1.Text = "Park Report";
            this.parkReportToolStripMenuItem1.Click += new System.EventHandler(this.reportBtn_Click);
            // 
            // utilityReportToolStripMenuItem1
            // 
            this.utilityReportToolStripMenuItem1.Name = "utilityReportToolStripMenuItem1";
            this.utilityReportToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D3)));
            this.utilityReportToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.utilityReportToolStripMenuItem1.Text = "Utility Report";
            this.utilityReportToolStripMenuItem1.Click += new System.EventHandler(this.utilBtn_Click);
            // 
            // readSheetToolStripMenuItem
            // 
            this.readSheetToolStripMenuItem.Name = "readSheetToolStripMenuItem";
            this.readSheetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D4)));
            this.readSheetToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.readSheetToolStripMenuItem.Text = "Read Sheet";
            this.readSheetToolStripMenuItem.Click += new System.EventHandler(this.readSheetToolStripMenuItem_Click);
            // 
            // eCReportToolStripMenuItem
            // 
            this.eCReportToolStripMenuItem.Name = "eCReportToolStripMenuItem";
            this.eCReportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D5)));
            this.eCReportToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.eCReportToolStripMenuItem.Text = "EC Report";
            this.eCReportToolStripMenuItem.Click += new System.EventHandler(this.eCReportToolStripMenuItem_Click);
            // 
            // collectionsToolStripMenuItem
            // 
            this.collectionsToolStripMenuItem.Name = "collectionsToolStripMenuItem";
            this.collectionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D6)));
            this.collectionsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.collectionsToolStripMenuItem.Text = "Collections";
            this.collectionsToolStripMenuItem.Click += new System.EventHandler(this.collectionsToolStripMenuItem_Click);
            // 
            // tenantToolStripMenuItem
            // 
            this.tenantToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gasHistoryToolStripMenuItem,
            this.eleHistoryToolStripMenuItem,
            this.watHistoryToolStripMenuItem,
            this.allHistoryToolStripMenuItem});
            this.tenantToolStripMenuItem.Name = "tenantToolStripMenuItem";
            this.tenantToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.tenantToolStripMenuItem.Text = "Tenant";
            this.tenantToolStripMenuItem.Click += new System.EventHandler(this.tenantToolStripMenuItem_Click);
            // 
            // gasHistoryToolStripMenuItem
            // 
            this.gasHistoryToolStripMenuItem.Name = "gasHistoryToolStripMenuItem";
            this.gasHistoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gasHistoryToolStripMenuItem.Text = "Gas History";
            this.gasHistoryToolStripMenuItem.Click += new System.EventHandler(this.gasHistoryToolStripMenuItem_Click);
            // 
            // eleHistoryToolStripMenuItem
            // 
            this.eleHistoryToolStripMenuItem.Name = "eleHistoryToolStripMenuItem";
            this.eleHistoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.eleHistoryToolStripMenuItem.Text = "Ele History";
            this.eleHistoryToolStripMenuItem.Click += new System.EventHandler(this.eleHistoryToolStripMenuItem_Click);
            // 
            // watHistoryToolStripMenuItem
            // 
            this.watHistoryToolStripMenuItem.Name = "watHistoryToolStripMenuItem";
            this.watHistoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.watHistoryToolStripMenuItem.Text = "Wat History";
            this.watHistoryToolStripMenuItem.Click += new System.EventHandler(this.watHistoryToolStripMenuItem_Click);
            // 
            // allHistoryToolStripMenuItem
            // 
            this.allHistoryToolStripMenuItem.Name = "allHistoryToolStripMenuItem";
            this.allHistoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.allHistoryToolStripMenuItem.Text = "All History";
            this.allHistoryToolStripMenuItem.Click += new System.EventHandler(this.allHistoryToolStripMenuItem_Click);
            // 
            // moveOut
            // 
            this.moveOut.Location = new System.Drawing.Point(810, 577);
            this.moveOut.Margin = new System.Windows.Forms.Padding(2);
            this.moveOut.Name = "moveOut";
            this.moveOut.Size = new System.Drawing.Size(116, 37);
            this.moveOut.TabIndex = 182;
            this.moveOut.Text = "Move Out";
            this.moveOut.UseVisualStyleBackColor = true;
            this.moveOut.Visible = false;
            this.moveOut.Click += new System.EventHandler(this.moveOut_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(322, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 184;
            this.label3.Text = "Space";
            // 
            // spaceList
            // 
            this.spaceList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.spaceList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.spaceList.FormattingEnabled = true;
            this.spaceList.Location = new System.Drawing.Point(381, 30);
            this.spaceList.Name = "spaceList";
            this.spaceList.Size = new System.Drawing.Size(83, 21);
            this.spaceList.TabIndex = 2;
            this.spaceList.SelectedIndexChanged += new System.EventHandler(this.spaceList_SelectedIndexChanged);
            this.spaceList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TenantBill_KeyDown);
            // 
            // topMessageInput
            // 
            this.topMessageInput.Location = new System.Drawing.Point(12, 449);
            this.topMessageInput.Name = "topMessageInput";
            this.topMessageInput.Size = new System.Drawing.Size(450, 88);
            this.topMessageInput.TabIndex = 185;
            this.topMessageInput.Text = "";
            // 
            // botMessageInput
            // 
            this.botMessageInput.Location = new System.Drawing.Point(476, 449);
            this.botMessageInput.Name = "botMessageInput";
            this.botMessageInput.Size = new System.Drawing.Size(450, 88);
            this.botMessageInput.TabIndex = 186;
            this.botMessageInput.Text = "";
            // 
            // readIssuesToolStripMenuItem
            // 
            this.readIssuesToolStripMenuItem.Name = "readIssuesToolStripMenuItem";
            this.readIssuesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D7)));
            this.readIssuesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.readIssuesToolStripMenuItem.Text = "Read Issues";
            this.readIssuesToolStripMenuItem.Click += new System.EventHandler(this.readIssuesToolStripMenuItem_Click);
            // 
            // TenantBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(954, 112);
            this.Controls.Add(this.botMessageInput);
            this.Controls.Add(this.topMessageInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.spaceList);
            this.Controls.Add(this.moveOut);
            this.Controls.Add(this.tempCharges);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.readDate);
            this.Controls.Add(this.summaryOfCharges);
            this.Controls.Add(this.historyBtn);
            this.Controls.Add(this.readInfo);
            this.Controls.Add(this.usageInfo);
            this.Controls.Add(this.clerkInfo);
            this.Controls.Add(this.tenantInfo);
            this.Controls.Add(this.watInfo);
            this.Controls.Add(this.gasInfo);
            this.Controls.Add(this.eleInfo);
            this.Controls.Add(this.billBtn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.parkList);
            this.Controls.Add(this.tenantList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TenantBill";
            this.Text = "TenantBill";
            this.Load += new System.EventHandler(this.TenantBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.summaryOfCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempCharges)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox parkList;
        private System.Windows.Forms.ComboBox tenantList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button billBtn;
        private System.Windows.Forms.RichTextBox eleInfo;
        private System.Windows.Forms.RichTextBox gasInfo;
        private System.Windows.Forms.RichTextBox watInfo;
        private System.Windows.Forms.RichTextBox tenantInfo;
        private System.Windows.Forms.RichTextBox clerkInfo;
        private System.Windows.Forms.RichTextBox usageInfo;
        private System.Windows.Forms.RichTextBox readInfo;
        private System.Windows.Forms.Button historyBtn;
        private System.Windows.Forms.DataGridView summaryOfCharges;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox readDate;
        private System.Windows.Forms.DataGridView tempCharges;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem parkReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilityReportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem parkReportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem parkBillsToolStripMenuItem;
        private System.Windows.Forms.Button moveOut;
        private System.Windows.Forms.ToolStripMenuItem eCReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readSheetToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox spaceList;
        private System.Windows.Forms.ToolStripMenuItem collectionsToolStripMenuItem;
        private System.Windows.Forms.RichTextBox topMessageInput;
        private System.Windows.Forms.RichTextBox botMessageInput;
        private System.Windows.Forms.ToolStripMenuItem tenantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gasHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eleHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem watHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readIssuesToolStripMenuItem;
    }
}