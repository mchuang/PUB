namespace PUB
{
    partial class TierInfo
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
            this.tier1 = new System.Windows.Forms.TextBox();
            this.tier2 = new System.Windows.Forms.TextBox();
            this.tier3 = new System.Windows.Forms.TextBox();
            this.tier4 = new System.Windows.Forms.TextBox();
            this.tier5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tierList = new System.Windows.Forms.ComboBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tier1
            // 
            this.tier1.Location = new System.Drawing.Point(95, 47);
            this.tier1.Name = "tier1";
            this.tier1.Size = new System.Drawing.Size(121, 20);
            this.tier1.TabIndex = 0;
            this.tier1.Text = "0";
            // 
            // tier2
            // 
            this.tier2.Location = new System.Drawing.Point(95, 73);
            this.tier2.Name = "tier2";
            this.tier2.Size = new System.Drawing.Size(121, 20);
            this.tier2.TabIndex = 1;
            this.tier2.Text = "0";
            // 
            // tier3
            // 
            this.tier3.Location = new System.Drawing.Point(95, 99);
            this.tier3.Name = "tier3";
            this.tier3.Size = new System.Drawing.Size(121, 20);
            this.tier3.TabIndex = 2;
            this.tier3.Text = "0";
            // 
            // tier4
            // 
            this.tier4.Location = new System.Drawing.Point(95, 125);
            this.tier4.Name = "tier4";
            this.tier4.Size = new System.Drawing.Size(121, 20);
            this.tier4.TabIndex = 3;
            this.tier4.Text = "0";
            // 
            // tier5
            // 
            this.tier5.Location = new System.Drawing.Point(95, 151);
            this.tier5.Name = "tier5";
            this.tier5.Size = new System.Drawing.Size(121, 20);
            this.tier5.TabIndex = 4;
            this.tier5.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tier Set Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tier 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Tier 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tier 3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Tier 4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Tier 5";
            // 
            // tierList
            // 
            this.tierList.FormattingEnabled = true;
            this.tierList.Location = new System.Drawing.Point(95, 21);
            this.tierList.Name = "tierList";
            this.tierList.Size = new System.Drawing.Size(121, 21);
            this.tierList.TabIndex = 12;
            this.tierList.SelectedIndexChanged += new System.EventHandler(this.tierList_SelectedIndexChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(38, 197);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(86, 32);
            this.saveBtn.TabIndex = 13;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(130, 197);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(86, 32);
            this.cancelBtn.TabIndex = 14;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // TierInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 241);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.tierList);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tier5);
            this.Controls.Add(this.tier4);
            this.Controls.Add(this.tier3);
            this.Controls.Add(this.tier2);
            this.Controls.Add(this.tier1);
            this.Name = "TierInfo";
            this.Text = "TierInfo";
            this.Load += new System.EventHandler(this.TierInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tier1;
        private System.Windows.Forms.TextBox tier2;
        private System.Windows.Forms.TextBox tier3;
        private System.Windows.Forms.TextBox tier4;
        private System.Windows.Forms.TextBox tier5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox tierList;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}