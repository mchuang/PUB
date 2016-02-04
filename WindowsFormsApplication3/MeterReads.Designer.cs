namespace PUB
{
    partial class MeterReads
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
            this.prevBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nextBtn = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.watValue = new System.Windows.Forms.NumericUpDown();
            this.gasValue = new System.Windows.Forms.NumericUpDown();
            this.eleValue = new System.Windows.Forms.NumericUpDown();
            this.readDate = new System.Windows.Forms.DateTimePicker();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.parkList = new System.Windows.Forms.ComboBox();
            this.spaceList = new System.Windows.Forms.ComboBox();
            this.newBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.watValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gasValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eleValue)).BeginInit();
            this.SuspendLayout();
            // 
            // prevBtn
            // 
            this.prevBtn.Location = new System.Drawing.Point(158, 257);
            this.prevBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.prevBtn.Name = "prevBtn";
            this.prevBtn.Size = new System.Drawing.Size(32, 28);
            this.prevBtn.TabIndex = 0;
            this.prevBtn.Text = "<<";
            this.prevBtn.UseVisualStyleBackColor = true;
            this.prevBtn.Click += new System.EventHandler(this.prevBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 71);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "SPACE NO.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 144);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "GAS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 174);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "ELE";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(73, 206);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "WAT";
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(303, 257);
            this.nextBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(32, 28);
            this.nextBtn.TabIndex = 14;
            this.nextBtn.Text = ">>";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(73, 38);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 17);
            this.label12.TabIndex = 16;
            this.label12.Text = "PARK";
            // 
            // watValue
            // 
            this.watValue.Location = new System.Drawing.Point(159, 203);
            this.watValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.watValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.watValue.Name = "watValue";
            this.watValue.Size = new System.Drawing.Size(176, 22);
            this.watValue.TabIndex = 26;
            // 
            // gasValue
            // 
            this.gasValue.Location = new System.Drawing.Point(159, 139);
            this.gasValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gasValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.gasValue.Name = "gasValue";
            this.gasValue.Size = new System.Drawing.Size(176, 22);
            this.gasValue.TabIndex = 25;
            // 
            // eleValue
            // 
            this.eleValue.Location = new System.Drawing.Point(159, 171);
            this.eleValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.eleValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.eleValue.Name = "eleValue";
            this.eleValue.Size = new System.Drawing.Size(176, 22);
            this.eleValue.TabIndex = 24;
            // 
            // readDate
            // 
            this.readDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.readDate.Location = new System.Drawing.Point(159, 105);
            this.readDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.readDate.Name = "readDate";
            this.readDate.Size = new System.Drawing.Size(176, 22);
            this.readDate.TabIndex = 32;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(158, 320);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(82, 28);
            this.saveBtn.TabIndex = 39;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(248, 320);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(87, 28);
            this.cancelBtn.TabIndex = 40;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // parkList
            // 
            this.parkList.FormattingEnabled = true;
            this.parkList.Location = new System.Drawing.Point(159, 34);
            this.parkList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.parkList.Name = "parkList";
            this.parkList.Size = new System.Drawing.Size(176, 24);
            this.parkList.TabIndex = 41;
            this.parkList.SelectedIndexChanged += new System.EventHandler(this.parkNumber_SelectedIndexChanged);
            // 
            // spaceList
            // 
            this.spaceList.FormattingEnabled = true;
            this.spaceList.Location = new System.Drawing.Point(159, 68);
            this.spaceList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.spaceList.Name = "spaceList";
            this.spaceList.Size = new System.Drawing.Size(176, 24);
            this.spaceList.TabIndex = 42;
            this.spaceList.SelectedIndexChanged += new System.EventHandler(this.spaceNumber_SelectedIndexChanged);
            // 
            // newBtn
            // 
            this.newBtn.Location = new System.Drawing.Point(195, 257);
            this.newBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(100, 28);
            this.newBtn.TabIndex = 43;
            this.newBtn.Text = "New";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 17);
            this.label3.TabIndex = 44;
            this.label3.Tag = "DATE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 45;
            this.label4.Text = "DATE";
            // 
            // MeterReads
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 380);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.newBtn);
            this.Controls.Add(this.spaceList);
            this.Controls.Add(this.parkList);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.readDate);
            this.Controls.Add(this.watValue);
            this.Controls.Add(this.gasValue);
            this.Controls.Add(this.eleValue);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prevBtn);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MeterReads";
            this.Text = "MeterReads";
            this.Load += new System.EventHandler(this.MeterReads_Load);
            ((System.ComponentModel.ISupportInitialize)(this.watValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gasValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eleValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button prevBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown watValue;
        private System.Windows.Forms.NumericUpDown gasValue;
        private System.Windows.Forms.NumericUpDown eleValue;
        private System.Windows.Forms.DateTimePicker readDate;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.ComboBox parkList;
        private System.Windows.Forms.ComboBox spaceList;
        private System.Windows.Forms.Button newBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}