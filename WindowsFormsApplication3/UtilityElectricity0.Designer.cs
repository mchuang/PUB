namespace PUB
{
    partial class UtilityElectricity0
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
            this.label6 = new System.Windows.Forms.Label();
            this.utilNameList = new System.Windows.Forms.ComboBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.deliveryRatesInput = new System.Windows.Forms.TableLayoutPanel();
            this.deliveryTierStatus = new System.Windows.Forms.ComboBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.tierSetId = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.generationRatesInput = new System.Windows.Forms.TableLayoutPanel();
            this.generationTierStatus = new System.Windows.Forms.ComboBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chargeSurcharge = new System.Windows.Forms.ComboBox();
            this.statusSurcharge = new System.Windows.Forms.ComboBox();
            this.descSurcharge = new System.Windows.Forms.ComboBox();
            this.rateSurcharge = new System.Windows.Forms.TextBox();
            this.usageSurcharge = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.saveGenBtn = new System.Windows.Forms.Button();
            this.saveDelBtn = new System.Windows.Forms.Button();
            this.saveSurBtn = new System.Windows.Forms.Button();
            this.removeGenBtn = new System.Windows.Forms.Button();
            this.removeDelBtn = new System.Windows.Forms.Button();
            this.removeSurBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.serviceCustCharge = new System.Windows.Forms.ComboBox();
            this.statusCustCharge = new System.Windows.Forms.ComboBox();
            this.custChargeRate = new System.Windows.Forms.TextBox();
            this.removeCustBtn = new System.Windows.Forms.Button();
            this.saveCustBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.storeremoveCC = new System.Windows.Forms.Label();
            this.storeremoveS = new System.Windows.Forms.Label();
            this.storeremoveG = new System.Windows.Forms.Label();
            this.storeremoveD = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.displayRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.prevEffDates = new System.Windows.Forms.ComboBox();
            this.deliveryRatesInput.SuspendLayout();
            this.generationRatesInput.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // measure
            // 
            this.measure.FormattingEnabled = true;
            this.measure.Items.AddRange(new object[] {
            "kWh"});
            this.measure.Location = new System.Drawing.Point(254, 61);
            this.measure.Name = "measure";
            this.measure.Size = new System.Drawing.Size(121, 21);
            this.measure.TabIndex = 4;
            this.measure.Text = "kWh";
            // 
            // method
            // 
            this.method.FormattingEnabled = true;
            this.method.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly",
            "Yearly"});
            this.method.Location = new System.Drawing.Point(66, 58);
            this.method.Name = "method";
            this.method.Size = new System.Drawing.Size(101, 21);
            this.method.TabIndex = 3;
            // 
            // effDate
            // 
            this.effDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.effDate.Location = new System.Drawing.Point(457, 30);
            this.effDate.Name = "effDate";
            this.effDate.Size = new System.Drawing.Size(101, 20);
            this.effDate.TabIndex = 2;
            this.effDate.ValueChanged += new System.EventHandler(this.effDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(402, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Eff. Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Method";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Measure";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Name";
            // 
            // utilNameList
            // 
            this.utilNameList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.utilNameList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.utilNameList.FormattingEnabled = true;
            this.utilNameList.Location = new System.Drawing.Point(65, 29);
            this.utilNameList.Name = "utilNameList";
            this.utilNameList.Size = new System.Drawing.Size(310, 21);
            this.utilNameList.TabIndex = 1;
            this.utilNameList.SelectedIndexChanged += new System.EventHandler(this.utilNameList_SelectedIndexChanged);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(716, 324);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(128, 35);
            this.cancelBtn.TabIndex = 52;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(582, 324);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(128, 35);
            this.saveBtn.TabIndex = 51;
            this.saveBtn.Text = "Commit";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(448, 324);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(128, 35);
            this.nextBtn.TabIndex = 50;
            this.nextBtn.Text = "Allowance";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.allowanceBtn_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(17, 112);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 159;
            this.label13.Text = "Customer Charge";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(17, 267);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(66, 13);
            this.label24.TabIndex = 161;
            this.label24.Text = "Delivery Tier";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(520, 267);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(34, 13);
            this.label41.TabIndex = 180;
            this.label41.Text = "Tier 5";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(420, 267);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(34, 13);
            this.label42.TabIndex = 179;
            this.label42.Text = "Tier 4";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(320, 267);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(34, 13);
            this.label28.TabIndex = 176;
            this.label28.Text = "Tier 3";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(219, 267);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(34, 13);
            this.label30.TabIndex = 175;
            this.label30.Text = "Tier 2";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(120, 267);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(34, 13);
            this.label40.TabIndex = 174;
            this.label40.Text = "Tier 1";
            // 
            // deliveryRatesInput
            // 
            this.deliveryRatesInput.ColumnCount = 6;
            this.deliveryRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.deliveryRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.deliveryRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.deliveryRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.deliveryRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.deliveryRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.deliveryRatesInput.Controls.Add(this.deliveryTierStatus, 0, 0);
            this.deliveryRatesInput.Controls.Add(this.textBox22, 4, 0);
            this.deliveryRatesInput.Controls.Add(this.textBox21, 3, 0);
            this.deliveryRatesInput.Controls.Add(this.textBox13, 2, 0);
            this.deliveryRatesInput.Controls.Add(this.textBox18, 5, 0);
            this.deliveryRatesInput.Controls.Add(this.textBox16, 1, 0);
            this.deliveryRatesInput.Location = new System.Drawing.Point(20, 283);
            this.deliveryRatesInput.Name = "deliveryRatesInput";
            this.deliveryRatesInput.RowCount = 1;
            this.deliveryRatesInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.deliveryRatesInput.Size = new System.Drawing.Size(600, 25);
            this.deliveryRatesInput.TabIndex = 173;
            // 
            // deliveryTierStatus
            // 
            this.deliveryTierStatus.FormattingEnabled = true;
            this.deliveryTierStatus.Location = new System.Drawing.Point(3, 3);
            this.deliveryTierStatus.Name = "deliveryTierStatus";
            this.deliveryTierStatus.Size = new System.Drawing.Size(94, 21);
            this.deliveryTierStatus.TabIndex = 40;
            this.deliveryTierStatus.SelectedIndexChanged += new System.EventHandler(this.deliveryTierSet_SelectedIndexChanged);
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(403, 3);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(94, 20);
            this.textBox22.TabIndex = 44;
            this.textBox22.Text = "0.0000";
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(303, 3);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(94, 20);
            this.textBox21.TabIndex = 43;
            this.textBox21.Text = "0.0000";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(203, 3);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(94, 20);
            this.textBox13.TabIndex = 42;
            this.textBox13.Text = "0.0000";
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(503, 3);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(94, 20);
            this.textBox18.TabIndex = 45;
            this.textBox18.Text = "0.0000";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(103, 3);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(94, 20);
            this.textBox16.TabIndex = 41;
            this.textBox16.Text = "0.0000";
            // 
            // tierSetId
            // 
            this.tierSetId.FormattingEnabled = true;
            this.tierSetId.Location = new System.Drawing.Point(457, 61);
            this.tierSetId.Name = "tierSetId";
            this.tierSetId.Size = new System.Drawing.Size(104, 21);
            this.tierSetId.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(402, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Tier Set";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(120, 214);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(34, 13);
            this.label27.TabIndex = 124;
            this.label27.Text = "Tier 1";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(219, 214);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(34, 13);
            this.label33.TabIndex = 163;
            this.label33.Text = "Tier 2";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(520, 214);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(34, 13);
            this.label31.TabIndex = 164;
            this.label31.Text = "Tier 5";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(420, 214);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(34, 13);
            this.label34.TabIndex = 165;
            this.label34.Text = "Tier 4";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(320, 214);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(34, 13);
            this.label35.TabIndex = 166;
            this.label35.Text = "Tier 3";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(17, 214);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 13);
            this.label23.TabIndex = 160;
            this.label23.Text = "Generation Tier";
            // 
            // generationRatesInput
            // 
            this.generationRatesInput.ColumnCount = 6;
            this.generationRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.generationRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.generationRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.generationRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.generationRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.generationRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.generationRatesInput.Controls.Add(this.generationTierStatus, 0, 0);
            this.generationRatesInput.Controls.Add(this.textBox4, 3, 0);
            this.generationRatesInput.Controls.Add(this.textBox5, 2, 0);
            this.generationRatesInput.Controls.Add(this.textBox14, 5, 0);
            this.generationRatesInput.Controls.Add(this.textBox27, 4, 0);
            this.generationRatesInput.Controls.Add(this.textBox10, 1, 0);
            this.generationRatesInput.Location = new System.Drawing.Point(20, 230);
            this.generationRatesInput.Name = "generationRatesInput";
            this.generationRatesInput.RowCount = 1;
            this.generationRatesInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.generationRatesInput.Size = new System.Drawing.Size(600, 25);
            this.generationRatesInput.TabIndex = 173;
            // 
            // generationTierStatus
            // 
            this.generationTierStatus.FormattingEnabled = true;
            this.generationTierStatus.Location = new System.Drawing.Point(3, 3);
            this.generationTierStatus.Name = "generationTierStatus";
            this.generationTierStatus.Size = new System.Drawing.Size(94, 21);
            this.generationTierStatus.TabIndex = 30;
            this.generationTierStatus.SelectedIndexChanged += new System.EventHandler(this.generationTierSet_SelectedIndexChanged);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(303, 3);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(94, 20);
            this.textBox4.TabIndex = 33;
            this.textBox4.Text = "0.0000";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(203, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(94, 20);
            this.textBox5.TabIndex = 32;
            this.textBox5.Text = "0.0000";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(503, 3);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(94, 20);
            this.textBox14.TabIndex = 35;
            this.textBox14.Text = "0.0000";
            // 
            // textBox27
            // 
            this.textBox27.Location = new System.Drawing.Point(403, 3);
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(94, 20);
            this.textBox27.TabIndex = 34;
            this.textBox27.Text = "0.0000";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(103, 3);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(94, 20);
            this.textBox10.TabIndex = 31;
            this.textBox10.Text = "0.0000";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.Controls.Add(this.chargeSurcharge, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusSurcharge, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.descSurcharge, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rateSurcharge, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.usageSurcharge, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 178);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 25);
            this.tableLayoutPanel1.TabIndex = 181;
            // 
            // chargeSurcharge
            // 
            this.chargeSurcharge.FormattingEnabled = true;
            this.chargeSurcharge.Location = new System.Drawing.Point(203, 3);
            this.chargeSurcharge.Name = "chargeSurcharge";
            this.chargeSurcharge.Size = new System.Drawing.Size(94, 21);
            this.chargeSurcharge.TabIndex = 209;
            this.chargeSurcharge.Text = " ";
            this.chargeSurcharge.SelectedIndexChanged += new System.EventHandler(this.chargeSurcharge_SelectedIndexChanged);
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
            // descSurcharge
            // 
            this.descSurcharge.FormattingEnabled = true;
            this.descSurcharge.Location = new System.Drawing.Point(3, 3);
            this.descSurcharge.Name = "descSurcharge";
            this.descSurcharge.Size = new System.Drawing.Size(94, 21);
            this.descSurcharge.TabIndex = 20;
            this.descSurcharge.SelectedIndexChanged += new System.EventHandler(this.surchargeList_SelectedIndexChanged);
            // 
            // rateSurcharge
            // 
            this.rateSurcharge.Location = new System.Drawing.Point(403, 3);
            this.rateSurcharge.Name = "rateSurcharge";
            this.rateSurcharge.Size = new System.Drawing.Size(94, 20);
            this.rateSurcharge.TabIndex = 23;
            this.rateSurcharge.Text = "0.0000";
            // 
            // usageSurcharge
            // 
            this.usageSurcharge.FormattingEnabled = true;
            this.usageSurcharge.Location = new System.Drawing.Point(303, 3);
            this.usageSurcharge.Name = "usageSurcharge";
            this.usageSurcharge.Size = new System.Drawing.Size(94, 21);
            this.usageSurcharge.TabIndex = 22;
            this.usageSurcharge.SelectedIndexChanged += new System.EventHandler(this.usageSurcharge_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 162);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 182;
            this.label15.Text = "Surcharges";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(120, 162);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 187;
            this.label18.Text = "Status";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(420, 162);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(30, 13);
            this.label19.TabIndex = 188;
            this.label19.Text = "Rate";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(320, 162);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 13);
            this.label20.TabIndex = 189;
            this.label20.Text = "Usage";
            // 
            // saveGenBtn
            // 
            this.saveGenBtn.Location = new System.Drawing.Point(626, 230);
            this.saveGenBtn.Name = "saveGenBtn";
            this.saveGenBtn.Size = new System.Drawing.Size(100, 23);
            this.saveGenBtn.TabIndex = 36;
            this.saveGenBtn.Text = "Store";
            this.saveGenBtn.UseVisualStyleBackColor = true;
            this.saveGenBtn.Click += new System.EventHandler(this.saveGenBtn_Click);
            // 
            // saveDelBtn
            // 
            this.saveDelBtn.Location = new System.Drawing.Point(626, 285);
            this.saveDelBtn.Name = "saveDelBtn";
            this.saveDelBtn.Size = new System.Drawing.Size(100, 23);
            this.saveDelBtn.TabIndex = 46;
            this.saveDelBtn.Text = "Store";
            this.saveDelBtn.UseVisualStyleBackColor = true;
            this.saveDelBtn.Click += new System.EventHandler(this.saveDelBtn_Click);
            // 
            // saveSurBtn
            // 
            this.saveSurBtn.Location = new System.Drawing.Point(626, 179);
            this.saveSurBtn.Name = "saveSurBtn";
            this.saveSurBtn.Size = new System.Drawing.Size(100, 23);
            this.saveSurBtn.TabIndex = 24;
            this.saveSurBtn.Text = "Store";
            this.saveSurBtn.UseVisualStyleBackColor = true;
            this.saveSurBtn.Click += new System.EventHandler(this.saveSurBtn_Click);
            // 
            // removeGenBtn
            // 
            this.removeGenBtn.Location = new System.Drawing.Point(732, 230);
            this.removeGenBtn.Name = "removeGenBtn";
            this.removeGenBtn.Size = new System.Drawing.Size(100, 23);
            this.removeGenBtn.TabIndex = 37;
            this.removeGenBtn.Text = "Remove";
            this.removeGenBtn.UseVisualStyleBackColor = true;
            this.removeGenBtn.Click += new System.EventHandler(this.removeGenBtn_Click);
            // 
            // removeDelBtn
            // 
            this.removeDelBtn.Location = new System.Drawing.Point(732, 283);
            this.removeDelBtn.Name = "removeDelBtn";
            this.removeDelBtn.Size = new System.Drawing.Size(100, 23);
            this.removeDelBtn.TabIndex = 47;
            this.removeDelBtn.Text = "Remove";
            this.removeDelBtn.UseVisualStyleBackColor = true;
            this.removeDelBtn.Click += new System.EventHandler(this.removeDelBtn_Click);
            // 
            // removeSurBtn
            // 
            this.removeSurBtn.Location = new System.Drawing.Point(732, 180);
            this.removeSurBtn.Name = "removeSurBtn";
            this.removeSurBtn.Size = new System.Drawing.Size(100, 23);
            this.removeSurBtn.TabIndex = 25;
            this.removeSurBtn.Text = "Remove";
            this.removeSurBtn.UseVisualStyleBackColor = true;
            this.removeSurBtn.Click += new System.EventHandler(this.removeSurBtn_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.serviceCustCharge, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.statusCustCharge, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.custChargeRate, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(20, 128);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(300, 25);
            this.tableLayoutPanel2.TabIndex = 196;
            // 
            // serviceCustCharge
            // 
            this.serviceCustCharge.FormattingEnabled = true;
            this.serviceCustCharge.Location = new System.Drawing.Point(103, 3);
            this.serviceCustCharge.Name = "serviceCustCharge";
            this.serviceCustCharge.Size = new System.Drawing.Size(94, 21);
            this.serviceCustCharge.TabIndex = 12;
            this.serviceCustCharge.SelectedIndexChanged += new System.EventHandler(this.serviceType_SelectedIndexChanged);
            // 
            // statusCustCharge
            // 
            this.statusCustCharge.FormattingEnabled = true;
            this.statusCustCharge.Location = new System.Drawing.Point(3, 3);
            this.statusCustCharge.Name = "statusCustCharge";
            this.statusCustCharge.Size = new System.Drawing.Size(94, 21);
            this.statusCustCharge.TabIndex = 11;
            this.statusCustCharge.SelectedIndexChanged += new System.EventHandler(this.statusCustCharge_SelectedIndexChanged);
            // 
            // custChargeRate
            // 
            this.custChargeRate.Location = new System.Drawing.Point(203, 3);
            this.custChargeRate.Name = "custChargeRate";
            this.custChargeRate.Size = new System.Drawing.Size(94, 20);
            this.custChargeRate.TabIndex = 13;
            this.custChargeRate.Text = "0.0000";
            // 
            // removeCustBtn
            // 
            this.removeCustBtn.Location = new System.Drawing.Point(732, 128);
            this.removeCustBtn.Name = "removeCustBtn";
            this.removeCustBtn.Size = new System.Drawing.Size(100, 23);
            this.removeCustBtn.TabIndex = 15;
            this.removeCustBtn.Text = "Remove";
            this.removeCustBtn.UseVisualStyleBackColor = true;
            this.removeCustBtn.Click += new System.EventHandler(this.removeCustBtn_Click);
            // 
            // saveCustBtn
            // 
            this.saveCustBtn.Location = new System.Drawing.Point(626, 128);
            this.saveCustBtn.Name = "saveCustBtn";
            this.saveCustBtn.Size = new System.Drawing.Size(100, 23);
            this.saveCustBtn.TabIndex = 14;
            this.saveCustBtn.Text = "Store";
            this.saveCustBtn.UseVisualStyleBackColor = true;
            this.saveCustBtn.Click += new System.EventHandler(this.saveCustBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(120, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 203;
            this.label10.Text = "Service";
            // 
            // storeremoveCC
            // 
            this.storeremoveCC.AutoSize = true;
            this.storeremoveCC.Location = new System.Drawing.Point(623, 112);
            this.storeremoveCC.Name = "storeremoveCC";
            this.storeremoveCC.Size = new System.Drawing.Size(0, 13);
            this.storeremoveCC.TabIndex = 204;
            // 
            // storeremoveS
            // 
            this.storeremoveS.AutoSize = true;
            this.storeremoveS.Location = new System.Drawing.Point(623, 162);
            this.storeremoveS.Name = "storeremoveS";
            this.storeremoveS.Size = new System.Drawing.Size(0, 13);
            this.storeremoveS.TabIndex = 205;
            // 
            // storeremoveG
            // 
            this.storeremoveG.AutoSize = true;
            this.storeremoveG.Location = new System.Drawing.Point(623, 214);
            this.storeremoveG.Name = "storeremoveG";
            this.storeremoveG.Size = new System.Drawing.Size(0, 13);
            this.storeremoveG.TabIndex = 206;
            // 
            // storeremoveD
            // 
            this.storeremoveD.AutoSize = true;
            this.storeremoveD.Location = new System.Drawing.Point(623, 267);
            this.storeremoveD.Name = "storeremoveD";
            this.storeremoveD.Size = new System.Drawing.Size(0, 13);
            this.storeremoveD.TabIndex = 207;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayRateToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(849, 24);
            this.menuStrip1.TabIndex = 208;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 209;
            this.label3.Text = "Gen/Del";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(578, 34);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 264;
            this.label5.Text = "Prev Eff. Dates";
            // 
            // prevEffDates
            // 
            this.prevEffDates.FormattingEnabled = true;
            this.prevEffDates.Location = new System.Drawing.Point(659, 32);
            this.prevEffDates.Margin = new System.Windows.Forms.Padding(2);
            this.prevEffDates.Name = "prevEffDates";
            this.prevEffDates.Size = new System.Drawing.Size(92, 21);
            this.prevEffDates.TabIndex = 263;
            // 
            // UtilityElectricity0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 373);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.prevEffDates);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.storeremoveD);
            this.Controls.Add(this.storeremoveG);
            this.Controls.Add(this.storeremoveS);
            this.Controls.Add(this.storeremoveCC);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.removeCustBtn);
            this.Controls.Add(this.saveCustBtn);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.removeSurBtn);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.removeDelBtn);
            this.Controls.Add(this.removeGenBtn);
            this.Controls.Add(this.saveSurBtn);
            this.Controls.Add(this.saveDelBtn);
            this.Controls.Add(this.saveGenBtn);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label42);
            this.Controls.Add(this.generationRatesInput);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.deliveryRatesInput);
            this.Controls.Add(this.tierSetId);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.utilNameList);
            this.Controls.Add(this.measure);
            this.Controls.Add(this.method);
            this.Controls.Add(this.effDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "UtilityElectricity0";
            this.Text = "UtilityElectricity";
            this.Load += new System.EventHandler(this.UtilityElectricity_Load);
            this.deliveryRatesInput.ResumeLayout(false);
            this.deliveryRatesInput.PerformLayout();
            this.generationRatesInput.ResumeLayout(false);
            this.generationRatesInput.PerformLayout();
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox utilNameList;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TableLayoutPanel deliveryRatesInput;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.ComboBox tierSetId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox deliveryTierStatus;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TableLayoutPanel generationRatesInput;
        private System.Windows.Forms.ComboBox generationTierStatus;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox rateSurcharge;
        private System.Windows.Forms.ComboBox descSurcharge;
        private System.Windows.Forms.ComboBox statusSurcharge;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox usageSurcharge;
        private System.Windows.Forms.Button saveGenBtn;
        private System.Windows.Forms.Button saveDelBtn;
        private System.Windows.Forms.Button saveSurBtn;
        private System.Windows.Forms.Button removeGenBtn;
        private System.Windows.Forms.Button removeDelBtn;
        private System.Windows.Forms.Button removeSurBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox custChargeRate;
        private System.Windows.Forms.ComboBox statusCustCharge;
        private System.Windows.Forms.Button removeCustBtn;
        private System.Windows.Forms.Button saveCustBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox serviceCustCharge;
        private System.Windows.Forms.Label storeremoveCC;
        private System.Windows.Forms.Label storeremoveS;
        private System.Windows.Forms.Label storeremoveG;
        private System.Windows.Forms.Label storeremoveD;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem displayRateToolStripMenuItem;
        private System.Windows.Forms.ComboBox chargeSurcharge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox prevEffDates;
    }
}