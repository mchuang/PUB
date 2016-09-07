namespace PUB
{
    partial class DataMigration
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
            this.components = new System.ComponentModel.Container();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inputFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.headerComboBox = new System.Windows.Forms.ComboBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.parkList = new System.Windows.Forms.ComboBox();
            this.parkLabel = new System.Windows.Forms.Label();
            this.extractButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.dirText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.importOneMonth = new System.Windows.Forms.CheckBox();
            this.UtilcomboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.utilityButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(957, 174);
            this.typeComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(121, 24);
            this.typeComboBox.TabIndex = 0;
            this.typeComboBox.Visible = false;
            this.typeComboBox.SelectedValueChanged += new System.EventHandler(this.typeComboBox1_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(851, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "FILE TYPE";
            this.label1.Visible = false;
            // 
            // inputFile
            // 
            this.inputFile.Location = new System.Drawing.Point(957, 214);
            this.inputFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputFile.Name = "inputFile";
            this.inputFile.Size = new System.Drawing.Size(391, 22);
            this.inputFile.TabIndex = 55;
            this.inputFile.Visible = false;
            this.inputFile.Click += new System.EventHandler(this.inputFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(845, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 54;
            this.label2.Text = "INPUT FILE";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(780, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 17);
            this.label3.TabIndex = 57;
            this.label3.Text = "FIRST ROW HEADER";
            this.label3.Visible = false;
            // 
            // headerComboBox
            // 
            this.headerComboBox.FormattingEnabled = true;
            this.headerComboBox.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.headerComboBox.Location = new System.Drawing.Point(957, 254);
            this.headerComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.headerComboBox.Name = "headerComboBox";
            this.headerComboBox.Size = new System.Drawing.Size(121, 24);
            this.headerComboBox.TabIndex = 56;
            this.headerComboBox.Text = "Yes";
            this.headerComboBox.Visible = false;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(187, 225);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(87, 28);
            this.cancelBtn.TabIndex = 59;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(957, 302);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(83, 28);
            this.saveBtn.TabIndex = 58;
            this.saveBtn.Text = "Import";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Visible = false;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // parkList
            // 
            this.parkList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.parkList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.parkList.FormattingEnabled = true;
            this.parkList.Location = new System.Drawing.Point(75, 78);
            this.parkList.Margin = new System.Windows.Forms.Padding(4);
            this.parkList.Name = "parkList";
            this.parkList.Size = new System.Drawing.Size(121, 24);
            this.parkList.TabIndex = 61;
            this.parkList.SelectedIndexChanged += new System.EventHandler(this.parkList_SelectedIndexChanged);
            // 
            // parkLabel
            // 
            this.parkLabel.AutoSize = true;
            this.parkLabel.Location = new System.Drawing.Point(71, 57);
            this.parkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.parkLabel.Name = "parkLabel";
            this.parkLabel.Size = new System.Drawing.Size(45, 17);
            this.parkLabel.TabIndex = 60;
            this.parkLabel.Text = "PARK";
            // 
            // extractButton
            // 
            this.extractButton.Location = new System.Drawing.Point(75, 225);
            this.extractButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(96, 28);
            this.extractButton.TabIndex = 62;
            this.extractButton.Text = "Extract Park";
            this.extractButton.UseVisualStyleBackColor = true;
            this.extractButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // dirText
            // 
            this.dirText.Location = new System.Drawing.Point(75, 137);
            this.dirText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dirText.Name = "dirText";
            this.dirText.Size = new System.Drawing.Size(391, 22);
            this.dirText.TabIndex = 63;
            this.dirText.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 17);
            this.label4.TabIndex = 64;
            this.label4.Text = "DIRECTORY FOR EXTRACTION";
            // 
            // importOneMonth
            // 
            this.importOneMonth.AutoSize = true;
            this.importOneMonth.Location = new System.Drawing.Point(75, 178);
            this.importOneMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.importOneMonth.Name = "importOneMonth";
            this.importOneMonth.Size = new System.Drawing.Size(204, 21);
            this.importOneMonth.TabIndex = 65;
            this.importOneMonth.Text = "Import only one Month Data";
            this.importOneMonth.UseVisualStyleBackColor = true;
            // 
            // UtilcomboBox1
            // 
            this.UtilcomboBox1.FormattingEnabled = true;
            this.UtilcomboBox1.Items.AddRange(new object[] {
            "ELECTRICITY",
            "GAS",
            "WATER"});
            this.UtilcomboBox1.Location = new System.Drawing.Point(344, 78);
            this.UtilcomboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.UtilcomboBox1.Name = "UtilcomboBox1";
            this.UtilcomboBox1.Size = new System.Drawing.Size(121, 24);
            this.UtilcomboBox1.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(341, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 66;
            this.label5.Text = "UTILITY";
            // 
            // utilityButton
            // 
            this.utilityButton.Location = new System.Drawing.Point(344, 225);
            this.utilityButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.utilityButton.Name = "utilityButton";
            this.utilityButton.Size = new System.Drawing.Size(121, 28);
            this.utilityButton.TabIndex = 68;
            this.utilityButton.Text = "Extract Utility";
            this.utilityButton.UseVisualStyleBackColor = true;
            this.utilityButton.Click += new System.EventHandler(this.utilityButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
            // 
            // DataMigration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 395);
            this.Controls.Add(this.utilityButton);
            this.Controls.Add(this.UtilcomboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.importOneMonth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dirText);
            this.Controls.Add(this.extractButton);
            this.Controls.Add(this.parkList);
            this.Controls.Add(this.parkLabel);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.headerComboBox);
            this.Controls.Add(this.inputFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typeComboBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DataMigration";
            this.Text = "DataMigration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox headerComboBox;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox parkList;
        private System.Windows.Forms.Label parkLabel;
        private System.Windows.Forms.Button extractButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox dirText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox importOneMonth;
        private System.Windows.Forms.ComboBox UtilcomboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button utilityButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}