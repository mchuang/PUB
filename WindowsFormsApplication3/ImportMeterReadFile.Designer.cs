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
            this.label4 = new System.Windows.Forms.Label();
            this.parkList = new System.Windows.Forms.ComboBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.readDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inputFile = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 51;
            this.label4.Text = "DATE";
            // 
            // parkList
            // 
            this.parkList.FormattingEnabled = true;
            this.parkList.Location = new System.Drawing.Point(174, 48);
            this.parkList.Margin = new System.Windows.Forms.Padding(4);
            this.parkList.Name = "parkList";
            this.parkList.Size = new System.Drawing.Size(176, 24);
            this.parkList.TabIndex = 50;
            this.parkList.SelectedIndexChanged += new System.EventHandler(this.parkList_SelectedIndexChanged);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(263, 334);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(87, 28);
            this.cancelBtn.TabIndex = 49;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(173, 334);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(82, 28);
            this.saveBtn.TabIndex = 48;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // readDate
            // 
            this.readDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.readDate.Location = new System.Drawing.Point(174, 80);
            this.readDate.Margin = new System.Windows.Forms.Padding(4);
            this.readDate.Name = "readDate";
            this.readDate.Size = new System.Drawing.Size(176, 22);
            this.readDate.TabIndex = 47;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(57, 55);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 17);
            this.label12.TabIndex = 46;
            this.label12.Text = "PARK";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 52;
            this.label1.Text = "INPUT FILE";
            // 
            // inputFile
            // 
            this.inputFile.Location = new System.Drawing.Point(173, 127);
            this.inputFile.Name = "inputFile";
            this.inputFile.Size = new System.Drawing.Size(331, 22);
            this.inputFile.TabIndex = 53;
            this.inputFile.Click += new System.EventHandler(this.inputFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ImportMeterReadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 444);
            this.Controls.Add(this.inputFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.parkList);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.readDate);
            this.Controls.Add(this.label12);
            this.Name = "ImportMeterReadFile";
            this.Text = "ImportMeterReadFile";
            this.Load += new System.EventHandler(this.ImportMeterReadFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox parkList;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.DateTimePicker readDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

    }
}