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
            this.gasBtn = new System.Windows.Forms.Button();
            this.eleBtn = new System.Windows.Forms.Button();
            this.watBtn = new System.Windows.Forms.Button();
            this.currentRead = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.historyTable)).BeginInit();
            this.SuspendLayout();
            // 
            // historyTable
            // 
            this.historyTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.historyTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.historyTable.Location = new System.Drawing.Point(0, 0);
            this.historyTable.Name = "historyTable";
            this.historyTable.Size = new System.Drawing.Size(484, 151);
            this.historyTable.TabIndex = 0;
            // 
            // gasBtn
            // 
            this.gasBtn.Location = new System.Drawing.Point(221, 155);
            this.gasBtn.Name = "gasBtn";
            this.gasBtn.Size = new System.Drawing.Size(75, 23);
            this.gasBtn.TabIndex = 1;
            this.gasBtn.Text = "Gas";
            this.gasBtn.UseVisualStyleBackColor = true;
            this.gasBtn.Click += new System.EventHandler(this.gasBtn_Click);
            // 
            // eleBtn
            // 
            this.eleBtn.Location = new System.Drawing.Point(302, 155);
            this.eleBtn.Name = "eleBtn";
            this.eleBtn.Size = new System.Drawing.Size(75, 23);
            this.eleBtn.TabIndex = 2;
            this.eleBtn.Text = "Electric";
            this.eleBtn.UseVisualStyleBackColor = true;
            this.eleBtn.Click += new System.EventHandler(this.eleBtn_Click);
            // 
            // watBtn
            // 
            this.watBtn.Location = new System.Drawing.Point(383, 155);
            this.watBtn.Name = "watBtn";
            this.watBtn.Size = new System.Drawing.Size(75, 23);
            this.watBtn.TabIndex = 3;
            this.watBtn.Text = "Water";
            this.watBtn.UseVisualStyleBackColor = true;
            this.watBtn.Click += new System.EventHandler(this.watBtn_Click);
            // 
            // currentRead
            // 
            this.currentRead.Location = new System.Drawing.Point(115, 157);
            this.currentRead.Name = "currentRead";
            this.currentRead.Size = new System.Drawing.Size(100, 20);
            this.currentRead.TabIndex = 4;
            this.currentRead.TextChanged += new System.EventHandler(this.currentRead_TextChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(302, 198);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(383, 198);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Current Read";
            // 
            // ReadHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 233);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.currentRead);
            this.Controls.Add(this.watBtn);
            this.Controls.Add(this.eleBtn);
            this.Controls.Add(this.gasBtn);
            this.Controls.Add(this.historyTable);
            this.Name = "ReadHistory";
            this.Text = "ReadHistory";
            this.Load += new System.EventHandler(this.ReadHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.historyTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView historyTable;
        private System.Windows.Forms.Button gasBtn;
        private System.Windows.Forms.Button eleBtn;
        private System.Windows.Forms.Button watBtn;
        private System.Windows.Forms.TextBox currentRead;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label1;
    }
}