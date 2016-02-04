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
            this.moveOutDate.Location = new System.Drawing.Point(34, 78);
            this.moveOutDate.Name = "moveOutDate";
            this.moveOutDate.Size = new System.Drawing.Size(121, 20);
            this.moveOutDate.TabIndex = 0;
            // 
            // tenantList
            // 
            this.tenantList.FormattingEnabled = true;
            this.tenantList.Location = new System.Drawing.Point(34, 36);
            this.tenantList.Name = "tenantList";
            this.tenantList.Size = new System.Drawing.Size(121, 21);
            this.tenantList.TabIndex = 1;
            this.tenantList.SelectedIndexChanged += new System.EventHandler(this.tenantList_SelectedIndexChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(12, 120);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 33);
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(108, 120);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 33);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select a tenant and move out date";
            // 
            // MoveOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 165);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.tenantList);
            this.Controls.Add(this.moveOutDate);
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