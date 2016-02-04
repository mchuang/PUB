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
            ((System.ComponentModel.ISupportInitialize)(this.summaryOfCharges)).BeginInit();
            this.SuspendLayout();
            // 
            // parkList
            // 
            this.parkList.FormattingEnabled = true;
            this.parkList.Location = new System.Drawing.Point(94, 16);
            this.parkList.Name = "parkList";
            this.parkList.Size = new System.Drawing.Size(156, 21);
            this.parkList.TabIndex = 156;
            this.parkList.SelectedIndexChanged += new System.EventHandler(this.parkList_SelectedIndexChanged);
            // 
            // tenantList
            // 
            this.tenantList.FormattingEnabled = true;
            this.tenantList.Location = new System.Drawing.Point(407, 16);
            this.tenantList.Name = "tenantList";
            this.tenantList.Size = new System.Drawing.Size(156, 21);
            this.tenantList.TabIndex = 155;
            this.tenantList.SelectedIndexChanged += new System.EventHandler(this.tenantList_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 17);
            this.label8.TabIndex = 158;
            this.label8.Text = "Park";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(325, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 157;
            this.label1.Text = "Tenant";
            // 
            // billBtn
            // 
            this.billBtn.Location = new System.Drawing.Point(808, 574);
            this.billBtn.Name = "billBtn";
            this.billBtn.Size = new System.Drawing.Size(116, 37);
            this.billBtn.TabIndex = 159;
            this.billBtn.Text = "Generate PDF";
            this.billBtn.UseVisualStyleBackColor = true;
            this.billBtn.Click += new System.EventHandler(this.billBtn_Click);
            // 
            // eleInfo
            // 
            this.eleInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eleInfo.Location = new System.Drawing.Point(318, 211);
            this.eleInfo.Name = "eleInfo";
            this.eleInfo.ReadOnly = true;
            this.eleInfo.Size = new System.Drawing.Size(300, 400);
            this.eleInfo.TabIndex = 167;
            this.eleInfo.Text = "";
            // 
            // gasInfo
            // 
            this.gasInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gasInfo.Location = new System.Drawing.Point(12, 211);
            this.gasInfo.Name = "gasInfo";
            this.gasInfo.ReadOnly = true;
            this.gasInfo.Size = new System.Drawing.Size(300, 400);
            this.gasInfo.TabIndex = 168;
            this.gasInfo.Text = "";
            // 
            // watInfo
            // 
            this.watInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.watInfo.Location = new System.Drawing.Point(624, 211);
            this.watInfo.Name = "watInfo";
            this.watInfo.ReadOnly = true;
            this.watInfo.Size = new System.Drawing.Size(300, 200);
            this.watInfo.TabIndex = 169;
            this.watInfo.Text = "";
            // 
            // tenantInfo
            // 
            this.tenantInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenantInfo.Location = new System.Drawing.Point(12, 43);
            this.tenantInfo.Name = "tenantInfo";
            this.tenantInfo.ReadOnly = true;
            this.tenantInfo.Size = new System.Drawing.Size(450, 65);
            this.tenantInfo.TabIndex = 170;
            this.tenantInfo.Text = "";
            // 
            // clerkInfo
            // 
            this.clerkInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clerkInfo.Location = new System.Drawing.Point(468, 43);
            this.clerkInfo.Name = "clerkInfo";
            this.clerkInfo.ReadOnly = true;
            this.clerkInfo.Size = new System.Drawing.Size(450, 65);
            this.clerkInfo.TabIndex = 171;
            this.clerkInfo.Text = "";
            // 
            // usageInfo
            // 
            this.usageInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usageInfo.Location = new System.Drawing.Point(12, 114);
            this.usageInfo.Name = "usageInfo";
            this.usageInfo.ReadOnly = true;
            this.usageInfo.Size = new System.Drawing.Size(912, 25);
            this.usageInfo.TabIndex = 172;
            this.usageInfo.Text = "";
            // 
            // readInfo
            // 
            this.readInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readInfo.Location = new System.Drawing.Point(12, 145);
            this.readInfo.Name = "readInfo";
            this.readInfo.ReadOnly = true;
            this.readInfo.Size = new System.Drawing.Size(912, 60);
            this.readInfo.TabIndex = 173;
            this.readInfo.Text = "";
            // 
            // historyBtn
            // 
            this.historyBtn.Location = new System.Drawing.Point(686, 574);
            this.historyBtn.Name = "historyBtn";
            this.historyBtn.Size = new System.Drawing.Size(116, 37);
            this.historyBtn.TabIndex = 174;
            this.historyBtn.Text = "History";
            this.historyBtn.UseVisualStyleBackColor = true;
            this.historyBtn.Click += new System.EventHandler(this.historyBtn_Click);
            // 
            // summaryOfCharges
            // 
            this.summaryOfCharges.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.summaryOfCharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.summaryOfCharges.Location = new System.Drawing.Point(624, 417);
            this.summaryOfCharges.Name = "summaryOfCharges";
            this.summaryOfCharges.Size = new System.Drawing.Size(300, 142);
            this.summaryOfCharges.TabIndex = 175;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(620, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 177;
            this.label2.Text = "Read Date";
            // 
            // readDate
            // 
            this.readDate.FormattingEnabled = true;
            this.readDate.Location = new System.Drawing.Point(702, 16);
            this.readDate.Name = "readDate";
            this.readDate.Size = new System.Drawing.Size(100, 21);
            this.readDate.TabIndex = 176;
            this.readDate.SelectedIndexChanged += new System.EventHandler(this.readDate_SelectedIndexChanged);
            // 
            // TenantBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 619);
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
            this.Name = "TenantBill";
            this.Text = "TenantBill";
            this.Load += new System.EventHandler(this.TenantBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.summaryOfCharges)).EndInit();
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
    }
}