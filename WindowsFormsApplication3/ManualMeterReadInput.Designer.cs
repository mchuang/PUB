namespace PUB {
    partial class ManualMeterReadInput {
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
            this.input = new System.Windows.Forms.DataGridView();
            this.parkList = new System.Windows.Forms.ComboBox();
            this.dueDate = new System.Windows.Forms.DateTimePicker();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.thermX = new System.Windows.Forms.TextBox();
            this.employeeId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.input)).BeginInit();
            this.SuspendLayout();
            // 
            // input
            // 
            this.input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.input.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.input.Location = new System.Drawing.Point(16, 180);
            this.input.Margin = new System.Windows.Forms.Padding(4);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(744, 346);
            this.input.TabIndex = 0;
            // 
            // parkList
            // 
            this.parkList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.parkList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.parkList.FormattingEnabled = true;
            this.parkList.Location = new System.Drawing.Point(108, 39);
            this.parkList.Margin = new System.Windows.Forms.Padding(4);
            this.parkList.Name = "parkList";
            this.parkList.Size = new System.Drawing.Size(227, 24);
            this.parkList.TabIndex = 1;
            this.parkList.SelectedIndexChanged += new System.EventHandler(this.parkList_SelectedIndexChanged);
            // 
            // dueDate
            // 
            this.dueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dueDate.Location = new System.Drawing.Point(108, 73);
            this.dueDate.Margin = new System.Windows.Forms.Padding(4);
            this.dueDate.Name = "dueDate";
            this.dueDate.Size = new System.Drawing.Size(227, 22);
            this.dueDate.TabIndex = 2;
            // 
            // startDate
            // 
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDate.Location = new System.Drawing.Point(108, 105);
            this.startDate.Margin = new System.Windows.Forms.Padding(4);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(227, 22);
            this.startDate.TabIndex = 3;
            // 
            // endDate
            // 
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDate.Location = new System.Drawing.Point(108, 137);
            this.endDate.Margin = new System.Windows.Forms.Padding(4);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(227, 22);
            this.endDate.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Due Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Start Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 144);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "End Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Park";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(532, 118);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 42);
            this.button1.TabIndex = 9;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // thermX
            // 
            this.thermX.Location = new System.Drawing.Point(532, 39);
            this.thermX.Margin = new System.Windows.Forms.Padding(4);
            this.thermX.Name = "thermX";
            this.thermX.Size = new System.Drawing.Size(227, 22);
            this.thermX.TabIndex = 10;
            // 
            // employeeId
            // 
            this.employeeId.Location = new System.Drawing.Point(532, 71);
            this.employeeId.Margin = new System.Windows.Forms.Padding(4);
            this.employeeId.Name = "employeeId";
            this.employeeId.Size = new System.Drawing.Size(227, 22);
            this.employeeId.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(423, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "ThermX";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(421, 80);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "EmployeeID";
            // 
            // ManualMeterReadInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 540);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.employeeId);
            this.Controls.Add(this.thermX);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.dueDate);
            this.Controls.Add(this.parkList);
            this.Controls.Add(this.input);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ManualMeterReadInput";
            this.Text = "ManualMeterReadInput";
            this.Load += new System.EventHandler(this.ManualMeterReadInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.input)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView input;
        private System.Windows.Forms.ComboBox parkList;
        private System.Windows.Forms.DateTimePicker dueDate;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox thermX;
        private System.Windows.Forms.TextBox employeeId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
    }
}