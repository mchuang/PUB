namespace PUB
{
    partial class UtilityGas0
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
            this.measure = new System.Windows.Forms.ComboBox();
            this.method = new System.Windows.Forms.ComboBox();
            this.effDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.utilNameList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nextBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.tierRatesInput = new System.Windows.Forms.TableLayoutPanel();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.tierStatus = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.removeTierBtn = new System.Windows.Forms.Button();
            this.saveTierBtn = new System.Windows.Forms.Button();
            this.removeSurBtn = new System.Windows.Forms.Button();
            this.saveSurBtn = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.usageSurcharge = new System.Windows.Forms.ComboBox();
            this.statusSurcharge = new System.Windows.Forms.ComboBox();
            this.rateSurcharge = new System.Windows.Forms.TextBox();
            this.descSurcharge = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.removeCustBtn = new System.Windows.Forms.Button();
            this.saveCustBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.serviceType = new System.Windows.Forms.ComboBox();
            this.statusCustCharge = new System.Windows.Forms.ComboBox();
            this.custChargeRate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tierSetId = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.displayRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prevEffDates = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tierRatesInput.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // measure
            // 
            this.measure.FormattingEnabled = true;
            this.measure.Items.AddRange(new object[] {
            "Cubic Feet"});
            this.measure.Location = new System.Drawing.Point(236, 63);
            this.measure.Name = "measure";
            this.measure.Size = new System.Drawing.Size(101, 21);
            this.measure.TabIndex = 4;
            this.measure.Text = "Cubic Feet";
            // 
            // method
            // 
            this.method.FormattingEnabled = true;
            this.method.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly",
            "Yearly"});
            this.method.Location = new System.Drawing.Point(63, 63);
            this.method.Name = "method";
            this.method.Size = new System.Drawing.Size(101, 21);
            this.method.TabIndex = 3;
            // 
            // effDate
            // 
            this.effDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.effDate.Location = new System.Drawing.Point(446, 24);
            this.effDate.Name = "effDate";
            this.effDate.Size = new System.Drawing.Size(101, 20);
            this.effDate.TabIndex = 2;
            this.effDate.ValueChanged += new System.EventHandler(this.effDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(391, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Eff. Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Method";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Measure";
            // 
            // utilNameList
            // 
            this.utilNameList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.utilNameList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.utilNameList.FormattingEnabled = true;
            this.utilNameList.Location = new System.Drawing.Point(63, 23);
            this.utilNameList.Name = "utilNameList";
            this.utilNameList.Size = new System.Drawing.Size(321, 21);
            this.utilNameList.TabIndex = 1;
            this.utilNameList.SelectedIndexChanged += new System.EventHandler(this.utilNameList_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 157;
            this.label6.Text = "Name";
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(432, 303);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(128, 35);
            this.nextBtn.TabIndex = 40;
            this.nextBtn.Text = "Allowance";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(566, 303);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(128, 35);
            this.saveBtn.TabIndex = 41;
            this.saveBtn.Text = "Commit";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(700, 303);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(128, 35);
            this.cancelBtn.TabIndex = 42;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(9, 212);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 13);
            this.label17.TabIndex = 240;
            this.label17.Text = "Tier Set";
            // 
            // tierRatesInput
            // 
            this.tierRatesInput.ColumnCount = 6;
            this.tierRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tierRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tierRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tierRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tierRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tierRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tierRatesInput.Controls.Add(this.textBox6, 3, 0);
            this.tierRatesInput.Controls.Add(this.textBox10, 2, 0);
            this.tierRatesInput.Controls.Add(this.textBox18, 5, 0);
            this.tierRatesInput.Controls.Add(this.textBox22, 4, 0);
            this.tierRatesInput.Controls.Add(this.textBox23, 1, 0);
            this.tierRatesInput.Controls.Add(this.tierStatus, 0, 0);
            this.tierRatesInput.Location = new System.Drawing.Point(12, 228);
            this.tierRatesInput.Name = "tierRatesInput";
            this.tierRatesInput.RowCount = 1;
            this.tierRatesInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tierRatesInput.Size = new System.Drawing.Size(600, 25);
            this.tierRatesInput.TabIndex = 239;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(303, 3);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(94, 20);
            this.textBox6.TabIndex = 33;
            this.textBox6.Text = "0.0000";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(203, 3);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(94, 20);
            this.textBox10.TabIndex = 32;
            this.textBox10.Text = "0.0000";
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(503, 3);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(94, 20);
            this.textBox18.TabIndex = 35;
            this.textBox18.Text = "0.0000";
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(403, 3);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(94, 20);
            this.textBox22.TabIndex = 34;
            this.textBox22.Text = "0.0000";
            // 
            // textBox23
            // 
            this.textBox23.Location = new System.Drawing.Point(103, 3);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(94, 20);
            this.textBox23.TabIndex = 31;
            this.textBox23.Text = "0.0000";
            // 
            // tierStatus
            // 
            this.tierStatus.FormattingEnabled = true;
            this.tierStatus.Location = new System.Drawing.Point(3, 3);
            this.tierStatus.Name = "tierStatus";
            this.tierStatus.Size = new System.Drawing.Size(94, 21);
            this.tierStatus.TabIndex = 30;
            this.tierStatus.SelectedIndexChanged += new System.EventHandler(this.tierStatus_SelectedIndexChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(312, 212);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(34, 13);
            this.label35.TabIndex = 238;
            this.label35.Text = "Tier 3";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(412, 212);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(34, 13);
            this.label34.TabIndex = 237;
            this.label34.Text = "Tier 4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(512, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 236;
            this.label7.Text = "Tier 5";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(211, 212);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(34, 13);
            this.label33.TabIndex = 235;
            this.label33.Text = "Tier 2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(112, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 234;
            this.label8.Text = "Tier 1";
            // 
            // removeTierBtn
            // 
            this.removeTierBtn.Location = new System.Drawing.Point(728, 228);
            this.removeTierBtn.Name = "removeTierBtn";
            this.removeTierBtn.Size = new System.Drawing.Size(100, 23);
            this.removeTierBtn.TabIndex = 37;
            this.removeTierBtn.Text = "Remove";
            this.removeTierBtn.UseVisualStyleBackColor = true;
            this.removeTierBtn.Click += new System.EventHandler(this.removeTierBtn_Click);
            // 
            // saveTierBtn
            // 
            this.saveTierBtn.Location = new System.Drawing.Point(622, 229);
            this.saveTierBtn.Name = "saveTierBtn";
            this.saveTierBtn.Size = new System.Drawing.Size(100, 23);
            this.saveTierBtn.TabIndex = 36;
            this.saveTierBtn.Text = "Store";
            this.saveTierBtn.UseVisualStyleBackColor = true;
            this.saveTierBtn.Click += new System.EventHandler(this.saveTierBtn_Click);
            // 
            // removeSurBtn
            // 
            this.removeSurBtn.Location = new System.Drawing.Point(728, 180);
            this.removeSurBtn.Name = "removeSurBtn";
            this.removeSurBtn.Size = new System.Drawing.Size(100, 23);
            this.removeSurBtn.TabIndex = 24;
            this.removeSurBtn.Text = "Remove";
            this.removeSurBtn.UseVisualStyleBackColor = true;
            this.removeSurBtn.Click += new System.EventHandler(this.removeSurBtn_Click);
            // 
            // saveSurBtn
            // 
            this.saveSurBtn.Location = new System.Drawing.Point(622, 180);
            this.saveSurBtn.Name = "saveSurBtn";
            this.saveSurBtn.Size = new System.Drawing.Size(100, 23);
            this.saveSurBtn.TabIndex = 23;
            this.saveSurBtn.Text = "Store";
            this.saveSurBtn.UseVisualStyleBackColor = true;
            this.saveSurBtn.Click += new System.EventHandler(this.saveSurBtn_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(212, 161);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 13);
            this.label20.TabIndex = 248;
            this.label20.Text = "Usage";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(312, 161);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(30, 13);
            this.label19.TabIndex = 247;
            this.label19.Text = "Rate";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(112, 161);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 246;
            this.label18.Text = "Status";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(9, 161);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 244;
            this.label21.Text = "Surcharges";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.usageSurcharge, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusSurcharge, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.rateSurcharge, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.descSurcharge, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 177);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 25);
            this.tableLayoutPanel1.TabIndex = 243;
            // 
            // usageSurcharge
            // 
            this.usageSurcharge.FormattingEnabled = true;
            this.usageSurcharge.Location = new System.Drawing.Point(203, 3);
            this.usageSurcharge.Name = "usageSurcharge";
            this.usageSurcharge.Size = new System.Drawing.Size(94, 21);
            this.usageSurcharge.TabIndex = 22;
            this.usageSurcharge.SelectedIndexChanged += new System.EventHandler(this.usageSurcharge_SelectedIndexChanged);
            // 
            // statusSurcharge
            // 
            this.statusSurcharge.FormattingEnabled = true;
            this.statusSurcharge.Location = new System.Drawing.Point(103, 3);
            this.statusSurcharge.Name = "statusSurcharge";
            this.statusSurcharge.Size = new System.Drawing.Size(94, 21);
            this.statusSurcharge.TabIndex = 21;
            this.statusSurcharge.SelectedIndexChanged += new System.EventHandler(this.statusSurcharge_SelectedIndexChanged);
            // 
            // rateSurcharge
            // 
            this.rateSurcharge.Location = new System.Drawing.Point(303, 3);
            this.rateSurcharge.Name = "rateSurcharge";
            this.rateSurcharge.Size = new System.Drawing.Size(94, 20);
            this.rateSurcharge.TabIndex = 182;
            this.rateSurcharge.Text = "0.0000";
            // 
            // descSurcharge
            // 
            this.descSurcharge.FormattingEnabled = true;
            this.descSurcharge.Location = new System.Drawing.Point(3, 3);
            this.descSurcharge.Name = "descSurcharge";
            this.descSurcharge.Size = new System.Drawing.Size(94, 21);
            this.descSurcharge.TabIndex = 20;
            this.descSurcharge.SelectedIndexChanged += new System.EventHandler(this.descSurcharge_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(112, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 257;
            this.label10.Text = "Service";
            // 
            // removeCustBtn
            // 
            this.removeCustBtn.Location = new System.Drawing.Point(728, 133);
            this.removeCustBtn.Name = "removeCustBtn";
            this.removeCustBtn.Size = new System.Drawing.Size(100, 23);
            this.removeCustBtn.TabIndex = 10;
            this.removeCustBtn.Text = "Remove";
            this.removeCustBtn.UseVisualStyleBackColor = true;
            this.removeCustBtn.Click += new System.EventHandler(this.removeCustBtn_Click);
            // 
            // saveCustBtn
            // 
            this.saveCustBtn.Location = new System.Drawing.Point(622, 133);
            this.saveCustBtn.Name = "saveCustBtn";
            this.saveCustBtn.Size = new System.Drawing.Size(100, 23);
            this.saveCustBtn.TabIndex = 9;
            this.saveCustBtn.Text = "Store";
            this.saveCustBtn.UseVisualStyleBackColor = true;
            this.saveCustBtn.Click += new System.EventHandler(this.saveCustBtn_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.serviceType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.statusCustCharge, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.custChargeRate, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 130);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(300, 25);
            this.tableLayoutPanel2.TabIndex = 253;
            // 
            // serviceType
            // 
            this.serviceType.FormattingEnabled = true;
            this.serviceType.Location = new System.Drawing.Point(103, 3);
            this.serviceType.Name = "serviceType";
            this.serviceType.Size = new System.Drawing.Size(94, 21);
            this.serviceType.TabIndex = 7;
            this.serviceType.SelectedIndexChanged += new System.EventHandler(this.serviceType_SelectedIndexChanged);
            // 
            // statusCustCharge
            // 
            this.statusCustCharge.FormattingEnabled = true;
            this.statusCustCharge.Location = new System.Drawing.Point(3, 3);
            this.statusCustCharge.Name = "statusCustCharge";
            this.statusCustCharge.Size = new System.Drawing.Size(94, 21);
            this.statusCustCharge.TabIndex = 6;
            this.statusCustCharge.SelectedIndexChanged += new System.EventHandler(this.statusCustCharge_SelectedIndexChanged);
            // 
            // custChargeRate
            // 
            this.custChargeRate.Location = new System.Drawing.Point(203, 3);
            this.custChargeRate.Name = "custChargeRate";
            this.custChargeRate.Size = new System.Drawing.Size(94, 20);
            this.custChargeRate.TabIndex = 8;
            this.custChargeRate.Text = "0.0000";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(9, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 251;
            this.label13.Text = "Customer Charge";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(391, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 259;
            this.label11.Text = "Tier Set";
            // 
            // tierSetId
            // 
            this.tierSetId.FormattingEnabled = true;
            this.tierSetId.Location = new System.Drawing.Point(446, 63);
            this.tierSetId.Name = "tierSetId";
            this.tierSetId.Size = new System.Drawing.Size(104, 21);
            this.tierSetId.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayRateToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(842, 24);
            this.menuStrip1.TabIndex = 260;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // displayRateToolStripMenuItem
            // 
            this.displayRateToolStripMenuItem.Name = "displayRateToolStripMenuItem";
            this.displayRateToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.displayRateToolStripMenuItem.Text = "Display Rate";
            this.displayRateToolStripMenuItem.Visible = false;
            this.displayRateToolStripMenuItem.Click += new System.EventHandler(this.displayRateToolStripMenuItem_Click);
            // 
            // prevEffDates
            // 
            this.prevEffDates.FormattingEnabled = true;
            this.prevEffDates.Location = new System.Drawing.Point(689, 24);
            this.prevEffDates.Margin = new System.Windows.Forms.Padding(2);
            this.prevEffDates.Name = "prevEffDates";
            this.prevEffDates.Size = new System.Drawing.Size(92, 21);
            this.prevEffDates.TabIndex = 261;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(608, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 262;
            this.label3.Text = "Prev Eff. Dates";
            // 
            // UtilityGas0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 363);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.prevEffDates);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tierSetId);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.removeCustBtn);
            this.Controls.Add(this.saveCustBtn);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.removeSurBtn);
            this.Controls.Add(this.saveSurBtn);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.removeTierBtn);
            this.Controls.Add(this.saveTierBtn);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.tierRatesInput);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.utilNameList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.measure);
            this.Controls.Add(this.method);
            this.Controls.Add(this.effDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UtilityGas0";
            this.Text = "UtilityGas";
            this.Load += new System.EventHandler(this.UtilityGas_Load);
            this.tierRatesInput.ResumeLayout(false);
            this.tierRatesInput.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox measure;
        private System.Windows.Forms.ComboBox method;
        private System.Windows.Forms.DateTimePicker effDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox utilNameList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TableLayoutPanel tierRatesInput;
        private System.Windows.Forms.ComboBox tierStatus;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button removeTierBtn;
        private System.Windows.Forms.Button saveTierBtn;
        private System.Windows.Forms.Button removeSurBtn;
        private System.Windows.Forms.Button saveSurBtn;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox usageSurcharge;
        private System.Windows.Forms.ComboBox statusSurcharge;
        private System.Windows.Forms.TextBox rateSurcharge;
        private System.Windows.Forms.ComboBox descSurcharge;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button removeCustBtn;
        private System.Windows.Forms.Button saveCustBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox serviceType;
        private System.Windows.Forms.ComboBox statusCustCharge;
        private System.Windows.Forms.TextBox custChargeRate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox tierSetId;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem displayRateToolStripMenuItem;
        private System.Windows.Forms.ComboBox prevEffDates;
        private System.Windows.Forms.Label label3;

    }
}