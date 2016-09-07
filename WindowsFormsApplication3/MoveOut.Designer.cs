namespace PUB
{
    partial class MoveOut
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
            this.moveOutDate = new System.Windows.Forms.DateTimePicker();
            this.tenantList = new System.Windows.Forms.ComboBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // moveOutDate
            // 
            this.moveOutDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.moveOutDate.Location = new System.Drawing.Point(45, 96);
            this.moveOutDate.Margin = new System.Windows.Forms.Padding(4);
            this.moveOutDate.Name = "moveOutDate";
            this.moveOutDate.Size = new System.Drawing.Size(160, 22);
            this.moveOutDate.TabIndex = 2;
            // 
            // tenantList
            // 
            this.tenantList.FormattingEnabled = true;
            this.tenantList.Location = new System.Drawing.Point(45, 44);
            this.tenantList.Margin = new System.Windows.Forms.Padding(4);
            this.tenantList.Name = "tenantList";
            this.tenantList.Size = new System.Drawing.Size(160, 24);
            this.tenantList.TabIndex = 1;
            this.tenantList.SelectedIndexChanged += new System.EventHandler(this.tenantList_SelectedIndexChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(16, 148);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(100, 41);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(144, 148);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(100, 41);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select a tenant and move out date";
            // 
            // MoveOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 203);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.tenantList);
            this.Controls.Add(this.moveOutDate);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MoveOut";
            this.Text = "MoveOut";
            this.Load += new System.EventHandler(this.MoveOut_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker moveOutDate;
        private System.Windows.Forms.ComboBox tenantList;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label1;
    }
}