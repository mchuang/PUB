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
            this.winterEndDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.winterStartDate = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
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
            this.seasonBox = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.hasWinter = new System.Windows.Forms.CheckBox();
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
            this.usageSurcharge = new System.Windows.Forms.ComboBox();
            this.statusSurcharge = new System.Windows.Forms.ComboBox();
            this.descSurcharge = new System.Windows.Forms.ComboBox();
            this.rateSurcharge = new System.Windows.Forms.TextBox();
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
            this.serviceType = new System.Windows.Forms.ComboBox();
            this.statusCustCharge = new System.Windows.Forms.ComboBox();
            this.custChargeRate = new System.Windows.Forms.TextBox();
            this.removeCustBtn = new System.Windows.Forms.Button();
            this.saveCustBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.deliveryRatesInput.SuspendLayout();
            this.generationRatesInput.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.measure.TabIndex = 64;
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
            this.method.TabIndex = 63;
            // 
            // effDate
            // 
            this.effDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.effDate.Location = new System.Drawing.Point(457, 30);
            this.effDate.Name = "effDate";
            this.effDate.Size = new System.Drawing.Size(101, 20);
            this.effDate.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(402, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 61;
            this.label1.Text = "Eff. Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 60;
            this.label4.Text = "Method";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Measure";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 58;
            this.label6.Text = "Name";
            // 
            // winterEndDate
            // 
            this.winterEndDate.CustomFormat = "MM/dd";
            this.winterEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.winterEndDate.Location = new System.Drawing.Point(614, 62);
            this.winterEndDate.Name = "winterEndDate";
            this.winterEndDate.Size = new System.Drawing.Size(97, 20);
            this.winterEndDate.TabIndex = 132;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(579, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 131;
            this.label5.Text = "End";
            // 
            // winterStartDate
            // 
            this.winterStartDate.CustomFormat = "MM/dd";
            this.winterStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.winterStartDate.Location = new System.Drawing.Point(614, 32);
            this.winterStartDate.Name = "winterStartDate";
            this.winterStartDate.Size = new System.Drawing.Size(97, 20);
            this.winterStartDate.TabIndex = 130;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(579, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 13);
            this.label14.TabIndex = 129;
            this.label14.Text = "Start";
            // 
            // utilNameList
            // 
            this.utilNameList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.utilNameList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.utilNameList.FormattingEnabled = true;
            this.utilNameList.Location = new System.Drawing.Point(65, 29);
            this.utilNameList.Name = "utilNameList";
            this.utilNameList.Size = new System.Drawing.Size(310, 21);
            this.utilNameList.TabIndex = 154;
            this.utilNameList.SelectedIndexChanged += new System.EventHandler(this.utilNameList_SelectedIndexChanged);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(716, 324);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(128, 35);
            this.cancelBtn.TabIndex = 157;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(582, 324);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(128, 35);
            this.saveBtn.TabIndex = 156;
            this.saveBtn.Text = "Commit";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(448, 324);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(128, 35);
            this.nextBtn.TabIndex = 158;
            this.nextBtn.Text = "Allowance";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
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
            this.deliveryRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
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
            this.deliveryTierStatus.TabIndex = 181;
            this.deliveryTierStatus.SelectedIndexChanged += new System.EventHandler(this.deliveryTierSet_SelectedIndexChanged);
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(403, 3);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(94, 20);
            this.textBox22.TabIndex = 174;
            this.textBox22.Text = "0.0000";
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(303, 3);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(94, 20);
            this.textBox21.TabIndex = 174;
            this.textBox21.Text = "0.0000";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(203, 3);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(94, 20);
            this.textBox13.TabIndex = 73;
            this.textBox13.Text = "0.0000";
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(503, 3);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(94, 20);
            this.textBox18.TabIndex = 71;
            this.textBox18.Text = "0.0000";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(103, 3);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(94, 20);
            this.textBox16.TabIndex = 70;
            this.textBox16.Text = "0.0000";
            // 
            // seasonBox
            // 
            this.seasonBox.FormattingEnabled = true;
            this.seasonBox.Location = new System.Drawing.Point(457, 61);
            this.seasonBox.Name = "seasonBox";
            this.seasonBox.Size = new System.Drawing.Size(101, 21);
            this.seasonBox.TabIndex = 172;
            this.seasonBox.Text = "Summer";
            this.seasonBox.SelectedIndexChanged += new System.EventHandler(this.seasonBox_SelectedIndexChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(402, 64);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(43, 13);
            this.label38.TabIndex = 173;
            this.label38.Text = "Season";
            // 
            // hasWinter
            // 
            this.hasWinter.AutoSize = true;
            this.hasWinter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.hasWinter.Location = new System.Drawing.Point(614, 9);
            this.hasWinter.Name = "hasWinter";
            this.hasWinter.Size = new System.Drawing.Size(96, 17);
            this.hasWinter.TabIndex = 174;
            this.hasWinter.Text = "Winter Season";
            this.hasWinter.UseVisualStyleBackColor = true;
            this.hasWinter.CheckedChanged += new System.EventHandler(this.hasWinter_CheckedChanged);
            // 
            // tierSetId
            // 
            this.tierSetId.FormattingEnabled = true;
            this.tierSetId.Location = new System.Drawing.Point(728, 31);
            this.tierSetId.Name = "tierSetId";
            this.tierSetId.Size = new System.Drawing.Size(104, 21);
            this.tierSetId.TabIndex = 177;
            this.tierSetId.SelectedIndexChanged += new System.EventHandler(this.tierSetId_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(725, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 178;
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
            this.generationRatesInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
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
            this.generationTierStatus.TabIndex = 182;
            this.generationTierStatus.SelectedIndexChanged += new System.EventHandler(this.generationTierSet_SelectedIndexChanged);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(303, 3);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(94, 20);
            this.textBox4.TabIndex = 72;
            this.textBox4.Text = "0.0000";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(203, 3);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(94, 20);
            this.textBox5.TabIndex = 73;
            this.textBox5.Text = "0.0000";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(503, 3);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(94, 20);
            this.textBox14.TabIndex = 71;
            this.textBox14.Text = "0.0000";
            // 
            // textBox27
            // 
            this.textBox27.Location = new System.Drawing.Point(403, 3);
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(94, 20);
            this.textBox27.TabIndex = 104;
            this.textBox27.Text = "0.0000";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(103, 3);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(94, 20);
            this.textBox10.TabIndex = 70;
            this.textBox10.Text = "0.0000";
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
            this.tableLayoutPanel1.Controls.Add(this.descSurcharge, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rateSurcharge, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 178);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 25);
            this.tableLayoutPanel1.TabIndex = 181;
            // 
            // usageSurcharge
            // 
            this.usageSurcharge.FormattingEnabled = true;
            this.usageSurcharge.Location = new System.Drawing.Point(203, 3);
            this.usageSurcharge.Name = "usageSurcharge";
            this.usageSurcharge.Size = new System.Drawing.Size(94, 21);
            this.usageSurcharge.TabIndex = 190;
            // 
            // statusSurcharge
            // 
            this.statusSurcharge.FormattingEnabled = true;
            this.statusSurcharge.Location = new System.Drawing.Point(103, 3);
            this.statusSurcharge.Name = "statusSurcharge";
            this.statusSurcharge.Size = new System.Drawing.Size(94, 21);
            this.statusSurcharge.TabIndex = 183;
            this.statusSurcharge.SelectedIndexChanged += new System.EventHandler(this.statusSurcharge_SelectedIndexChanged);
            // 
            // descSurcharge
            // 
            this.descSurcharge.FormattingEnabled = true;
            this.descSurcharge.Location = new System.Drawing.Point(3, 3);
            this.descSurcharge.Name = "descSurcharge";
            this.descSurcharge.Size = new System.Drawing.Size(94, 21);
            this.descSurcharge.TabIndex = 182;
            this.descSurcharge.SelectedIndexChanged += new System.EventHandler(this.surchargeList_SelectedIndexChanged);
            // 
            // rateSurcharge
            // 
            this.rateSurcharge.Location = new System.Drawing.Point(303, 3);
            this.rateSurcharge.Name = "rateSurcharge";
            this.rateSurcharge.Size = new System.Drawing.Size(94, 20);
            this.rateSurcharge.TabIndex = 182;
            this.rateSurcharge.Text = "0.0000";
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
            this.label19.Location = new System.Drawing.Point(320, 162);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(30, 13);
            this.label19.TabIndex = 188;
            this.label19.Text = "Rate";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(220, 162);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 13);
            this.label20.TabIndex = 189;
            this.label20.Text = "Usage";
            // 
            // saveGenBtn
            // 
            this.saveGenBtn.Location = new System.Drawing.Point(626, 231);
            this.saveGenBtn.Name = "saveGenBtn";
            this.saveGenBtn.Size = new System.Drawing.Size(100, 23);
            this.saveGenBtn.TabIndex = 190;
            this.saveGenBtn.Text = "Store";
            this.saveGenBtn.UseVisualStyleBackColor = true;
            this.saveGenBtn.Click += new System.EventHandler(this.saveGenBtn_Click);
            // 
            // saveDelBtn
            // 
            this.saveDelBtn.Location = new System.Drawing.Point(626, 285);
            this.saveDelBtn.Name = "saveDelBtn";
            this.saveDelBtn.Size = new System.Drawing.Size(100, 23);
            this.saveDelBtn.TabIndex = 191;
            this.saveDelBtn.Text = "Store";
            this.saveDelBtn.UseVisualStyleBackColor = true;
            this.saveDelBtn.Click += new System.EventHandler(this.saveDelBtn_Click);
            // 
            // saveSurBtn
            // 
            this.saveSurBtn.Location = new System.Drawing.Point(424, 179);
            this.saveSurBtn.Name = "saveSurBtn";
            this.saveSurBtn.Size = new System.Drawing.Size(100, 23);
            this.saveSurBtn.TabIndex = 192;
            this.saveSurBtn.Text = "Store";
            this.saveSurBtn.UseVisualStyleBackColor = true;
            this.saveSurBtn.Click += new System.EventHandler(this.saveSurBtn_Click);
            // 
            // removeGenBtn
            // 
            this.removeGenBtn.Location = new System.Drawing.Point(732, 230);
            this.removeGenBtn.Name = "removeGenBtn";
            this.removeGenBtn.Size = new System.Drawing.Size(100, 23);
            this.removeGenBtn.TabIndex = 193;
            this.removeGenBtn.Text = "Remove";
            this.removeGenBtn.UseVisualStyleBackColor = true;
            this.removeGenBtn.Click += new System.EventHandler(this.removeGenBtn_Click);
            // 
            // removeDelBtn
            // 
            this.removeDelBtn.Location = new System.Drawing.Point(732, 283);
            this.removeDelBtn.Name = "removeDelBtn";
            this.removeDelBtn.Size = new System.Drawing.Size(100, 23);
            this.removeDelBtn.TabIndex = 194;
            this.removeDelBtn.Text = "Remove";
            this.removeDelBtn.UseVisualStyleBackColor = true;
            this.removeDelBtn.Click += new System.EventHandler(this.removeDelBtn_Click);
            // 
            // removeSurBtn
            // 
            this.removeSurBtn.Location = new System.Drawing.Point(530, 180);
            this.removeSurBtn.Name = "removeSurBtn";
            this.removeSurBtn.Size = new System.Drawing.Size(100, 23);
            this.removeSurBtn.TabIndex = 195;
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
            this.tableLayoutPanel2.Controls.Add(this.serviceType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.statusCustCharge, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.custChargeRate, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(20, 128);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(300, 25);
            this.tableLayoutPanel2.TabIndex = 196;
            // 
            // serviceType
            // 
            this.serviceType.FormattingEnabled = true;
            this.serviceType.Location = new System.Drawing.Point(103, 3);
            this.serviceType.Name = "serviceType";
            this.serviceType.Size = new System.Drawing.Size(94, 21);
            this.serviceType.TabIndex = 204;
            this.serviceType.SelectedIndexChanged += new System.EventHandler(this.serviceType_SelectedIndexChanged);
            // 
            // statusCustCharge
            // 
            this.statusCustCharge.FormattingEnabled = true;
            this.statusCustCharge.Location = new System.Drawing.Point(3, 3);
            this.statusCustCharge.Name = "statusCustCharge";
            this.statusCustCharge.Size = new System.Drawing.Size(94, 21);
            this.statusCustCharge.TabIndex = 198;
            this.statusCustCharge.SelectedIndexChanged += new System.EventHandler(this.statusCustCharge_SelectedIndexChanged);
            // 
            // custChargeRate
            // 
            this.custChargeRate.Location = new System.Drawing.Point(203, 3);
            this.custChargeRate.Name = "custChargeRate";
            this.custChargeRate.Size = new System.Drawing.Size(94, 20);
            this.custChargeRate.TabIndex = 70;
            this.custChargeRate.Text = "0.0000";
            // 
            // removeCustBtn
            // 
            this.removeCustBtn.Location = new System.Drawing.Point(432, 128);
            this.removeCustBtn.Name = "removeCustBtn";
            this.removeCustBtn.Size = new System.Drawing.Size(100, 23);
            this.removeCustBtn.TabIndex = 201;
            this.removeCustBtn.Text = "Remove";
            this.removeCustBtn.UseVisualStyleBackColor = true;
            this.removeCustBtn.Click += new System.EventHandler(this.removeCustBtn_Click);
            // 
            // saveCustBtn
            // 
            this.saveCustBtn.Location = new System.Drawing.Point(326, 128);
            this.saveCustBtn.Name = "saveCustBtn";
            this.saveCustBtn.Size = new System.Drawing.Size(100, 23);
            this.saveCustBtn.TabIndex = 200;
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
            // UtilityElectricity0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 373);
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
            this.Controls.Add(this.hasWinter);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.seasonBox);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.utilNameList);
            this.Controls.Add(this.winterEndDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.winterStartDate);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.measure);
            this.Controls.Add(this.method);
            this.Controls.Add(this.effDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Name = "UtilityElectricity0";
            this.Text = "Electricity Company";
            this.Load += new System.EventHandler(this.UtilityElectricity_Load);
            this.deliveryRatesInput.ResumeLayout(false);
            this.deliveryRatesInput.PerformLayout();
            this.generationRatesInput.ResumeLayout(false);
            this.generationRatesInput.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
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
        private System.Windows.Forms.DateTimePicker winterEndDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker winterStartDate;
        private System.Windows.Forms.Label label14;
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
        private System.Windows.Forms.ComboBox seasonBox;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.CheckBox hasWinter;
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
        private System.Windows.Forms.ComboBox serviceType;
    }
}