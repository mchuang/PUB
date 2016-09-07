namespace PUB
{
    partial class ImportMeterReadFile
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
            this.cancelBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.readDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inputFile = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.prevReadDate = new System.Windows.Forms.DateTimePicker();
            this.dueDateInput = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.thermXInput = new System.Windows.Forms.TextBox();
            this.prevThermX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.billingDays = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // parkList
            // 
            this.parkList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.parkList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.parkList.FormattingEnabled = true;
            this.parkList.Location = new System.Drawing.Point(173, 33);
            this.parkList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.parkList.Name = "parkList";
            this.parkList.Size = new System.Drawing.Size(176, 24);
            this.parkList.TabIndex = 1;
            this.parkList.SelectedIndexChanged += new System.EventHandler(this.parkList_SelectedIndexChanged);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(263, 281);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(87, 28);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(173, 281);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(83, 28);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "Import";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // readDate
            // 
            this.readDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.readDate.Location = new System.Drawing.Point(173, 82);
            this.readDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.readDate.Name = "readDate";
            this.readDate.Size = new System.Drawing.Size(176, 22);
            this.readDate.TabIndex = 2;
            this.readDate.ValueChanged += new System.EventHandler(this.readDate_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(57, 37);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 17);
            this.label12.TabIndex = 46;
            this.label12.Text = "PARK";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 52;
            this.label1.Text = "INPUT FILE";
            // 
            // inputFile
            // 
            this.inputFile.Location = new System.Drawing.Point(173, 206);
            this.inputFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputFile.Name = "inputFile";
            this.inputFile.Size = new System.Drawing.Size(331, 22);
            this.inputFile.TabIndex = 4;
            this.inputFile.Click += new System.EventHandler(this.inputFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(57, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 42);
            this.label2.TabIndex = 55;
            this.label2.Text = "PREV METER READ DATE";
            // 
            // prevReadDate
            // 
            this.prevReadDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.prevReadDate.Location = new System.Drawing.Point(173, 124);
            this.prevReadDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.prevReadDate.Name = "prevReadDate";
            this.prevReadDate.Size = new System.Drawing.Size(176, 22);
            this.prevReadDate.TabIndex = 3;
            this.prevReadDate.ValueChanged += new System.EventHandler(this.readDate_ValueChanged);
            // 
            // dueDateInput
            // 
            this.dueDateInput.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dueDateInput.Location = new System.Drawing.Point(523, 165);
            this.dueDateInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dueDateInput.Name = "dueDateInput";
            this.dueDateInput.Size = new System.Drawing.Size(176, 22);
            this.dueDateInput.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 57;
            this.label3.Text = "DUE DATE";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(57, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 42);
            this.label5.TabIndex = 58;
            this.label5.Text = "NEW METER READ DATE";
            // 
            // thermXInput
            // 
            this.thermXInput.Location = new System.Drawing.Point(523, 82);
            this.thermXInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.thermXInput.Name = "thermXInput";
            this.thermXInput.Size = new System.Drawing.Size(176, 22);
            this.thermXInput.TabIndex = 59;
            this.thermXInput.Text = "0";
            // 
            // prevThermX
            // 
            this.prevThermX.Enabled = false;
            this.prevThermX.Location = new System.Drawing.Point(523, 124);
            this.prevThermX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.prevThermX.Name = "prevThermX";
            this.prevThermX.Size = new System.Drawing.Size(176, 22);
            this.prevThermX.TabIndex = 60;
            this.prevThermX.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 86);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 61;
            this.label4.Text = "THERM-X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(380, 128);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 17);
            this.label6.TabIndex = 62;
            this.label6.Text = "PREV THERM-X";
            // 
            // billingDays
            // 
            this.billingDays.Enabled = false;
            this.billingDays.Location = new System.Drawing.Point(173, 165);
            this.billingDays.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.billingDays.Name = "billingDays";
            this.billingDays.Size = new System.Drawing.Size(176, 22);
            this.billingDays.TabIndex = 63;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(57, 169);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 17);
            this.label7.TabIndex = 64;
            this.label7.Text = "BILLING DAYS";
            // 
            // ImportMeterReadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 334);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.billingDays);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.prevThermX);
            this.Controls.Add(this.thermXInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dueDateInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.prevReadDate);
            this.Controls.Add(this.inputFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.parkList);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.readDate);
            this.Controls.Add(this.label12);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ImportMeterReadFile";
            this.Text = "ImportMeterReadFile";
            this.Load += new System.EventHandler(this.ImportMeterReadFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox parkList;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.DateTimePicker readDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker prevReadDate;
        private System.Windows.Forms.DateTimePicker dueDateInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox thermXInput;
        private System.Windows.Forms.TextBox prevThermX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox billingDays;
        private System.Windows.Forms.Label label7;

    }
}