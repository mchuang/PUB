namespace PUB {
    partial class ReadHistory {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.historyTable = new System.Windows.Forms.DataGridView();
            this.currGas = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.currEle = new System.Windows.Forms.TextBox();
            this.currWat = new System.Windows.Forms.TextBox();
            this.prevWat = new System.Windows.Forms.TextBox();
            this.prevEle = new System.Windows.Forms.TextBox();
            this.prevGas = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.currReadDate = new System.Windows.Forms.DateTimePicker();
            this.prevReadDate = new System.Windows.Forms.DateTimePicker();
            this.prevStartDate = new System.Windows.Forms.DateTimePicker();
            this.currStartDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.historyTable)).BeginInit();
            this.SuspendLayout();
            // 
            // historyTable
            // 
            this.historyTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.historyTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.historyTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.historyTable.Location = new System.Drawing.Point(0, 0);
            this.historyTable.Name = "historyTable";
            this.historyTable.Size = new System.Drawing.Size(1062, 264);
            this.historyTable.TabIndex = 0;
            // 
            // currGas
            // 
            this.currGas.Location = new System.Drawing.Point(566, 329);
            this.currGas.Name = "currGas";
            this.currGas.Size = new System.Drawing.Size(100, 20);
            this.currGas.TabIndex = 4;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(723, 361);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 8;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(804, 361);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 332);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Current Read";
            // 
            // currEle
            // 
            this.currEle.Location = new System.Drawing.Point(672, 329);
            this.currEle.Name = "currEle";
            this.currEle.Size = new System.Drawing.Size(100, 20);
            this.currEle.TabIndex = 10;
            // 
            // currWat
            // 
            this.currWat.Location = new System.Drawing.Point(778, 329);
            this.currWat.Name = "currWat";
            this.currWat.Size = new System.Drawing.Size(100, 20);
            this.currWat.TabIndex = 11;
            // 
            // prevWat
            // 
            this.prevWat.Location = new System.Drawing.Point(778, 303);
            this.prevWat.Name = "prevWat";
            this.prevWat.Size = new System.Drawing.Size(100, 20);
            this.prevWat.TabIndex = 14;
            // 
            // prevEle
            // 
            this.prevEle.Location = new System.Drawing.Point(672, 303);
            this.prevEle.Name = "prevEle";
            this.prevEle.Size = new System.Drawing.Size(100, 20);
            this.prevEle.TabIndex = 13;
            // 
            // prevGas
            // 
            this.prevGas.Location = new System.Drawing.Point(566, 303);
            this.prevGas.Name = "prevGas";
            this.prevGas.Size = new System.Drawing.Size(100, 20);
            this.prevGas.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Previous Read";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(563, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Gas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(669, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Ele";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(775, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Wat";
            // 
            // currReadDate
            // 
            this.currReadDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.currReadDate.Location = new System.Drawing.Point(454, 329);
            this.currReadDate.Name = "currReadDate";
            this.currReadDate.Size = new System.Drawing.Size(106, 20);
            this.currReadDate.TabIndex = 19;
            // 
            // prevReadDate
            // 
            this.prevReadDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.prevReadDate.Location = new System.Drawing.Point(454, 303);
            this.prevReadDate.Name = "prevReadDate";
            this.prevReadDate.Size = new System.Drawing.Size(106, 20);
            this.prevReadDate.TabIndex = 20;
            // 
            // prevStartDate
            // 
            this.prevStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.prevStartDate.Location = new System.Drawing.Point(342, 303);
            this.prevStartDate.Name = "prevStartDate";
            this.prevStartDate.Size = new System.Drawing.Size(106, 20);
            this.prevStartDate.TabIndex = 21;
            // 
            // currStartDate
            // 
            this.currStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.currStartDate.Location = new System.Drawing.Point(342, 329);
            this.currStartDate.Name = "currStartDate";
            this.currStartDate.Size = new System.Drawing.Size(106, 20);
            this.currStartDate.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(343, 287);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Start Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(451, 287);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Meter Read Date";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(642, 361);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "New Read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReadHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 397);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.currStartDate);
            this.Controls.Add(this.prevStartDate);
            this.Controls.Add(this.prevReadDate);
            this.Controls.Add(this.currReadDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.prevWat);
            this.Controls.Add(this.prevEle);
            this.Controls.Add(this.prevGas);
            this.Controls.Add(this.currWat);
            this.Controls.Add(this.currEle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.currGas);
            this.Controls.Add(this.historyTable);
            this.Name = "ReadHistory";
            this.Text = "History";
            this.Load += new System.EventHandler(this.ReadHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.historyTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView historyTable;
        private System.Windows.Forms.TextBox currGas;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox currEle;
        private System.Windows.Forms.TextBox currWat;
        private System.Windows.Forms.TextBox prevWat;
        private System.Windows.Forms.TextBox prevEle;
        private System.Windows.Forms.TextBox prevGas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker currReadDate;
        private System.Windows.Forms.DateTimePicker prevReadDate;
        private System.Windows.Forms.DateTimePicker prevStartDate;
        private System.Windows.Forms.DateTimePicker currStartDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
    }
}