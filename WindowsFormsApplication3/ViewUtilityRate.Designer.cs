namespace PUB
{
    partial class ViewUtilityRateForm
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
            this.basicRateDataGridView = new System.Windows.Forms.DataGridView();
            this.refreshButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tierRateDataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.surchargeDataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.basicRateDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tierRateDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.surchargeDataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // basicRateDataGridView
            // 
            this.basicRateDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.basicRateDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.basicRateDataGridView.Location = new System.Drawing.Point(12, 35);
            this.basicRateDataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.basicRateDataGridView.Name = "basicRateDataGridView";
            this.basicRateDataGridView.RowTemplate.Height = 24;
            this.basicRateDataGridView.Size = new System.Drawing.Size(386, 230);
            this.basicRateDataGridView.TabIndex = 0;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(11, 11);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(2);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(91, 28);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Refresh Rates";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 11);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(115, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Basic Rates";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(12, 280);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(115, 21);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "Tier Rates";
            // 
            // tierRateDataGridView1
            // 
            this.tierRateDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tierRateDataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.tierRateDataGridView1.Location = new System.Drawing.Point(12, 304);
            this.tierRateDataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.tierRateDataGridView1.Name = "tierRateDataGridView1";
            this.tierRateDataGridView1.RowTemplate.Height = 24;
            this.tierRateDataGridView1.Size = new System.Drawing.Size(863, 238);
            this.tierRateDataGridView1.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(402, 11);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(115, 21);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "Surcharges";
            // 
            // surchargeDataGridView1
            // 
            this.surchargeDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.surchargeDataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.surchargeDataGridView1.Location = new System.Drawing.Point(402, 35);
            this.surchargeDataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.surchargeDataGridView1.Name = "surchargeDataGridView1";
            this.surchargeDataGridView1.RowTemplate.Height = 24;
            this.surchargeDataGridView1.Size = new System.Drawing.Size(473, 230);
            this.surchargeDataGridView1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.basicRateDataGridView);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.surchargeDataGridView1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.tierRateDataGridView1);
            this.panel1.Location = new System.Drawing.Point(11, 46);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(893, 566);
            this.panel1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 11);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "Save Rates";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // ViewUtilityRateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(942, 601);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.refreshButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ViewUtilityRateForm";
            this.Text = "ViewUtilityRate";
            ((System.ComponentModel.ISupportInitialize)(this.basicRateDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tierRateDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.surchargeDataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView basicRateDataGridView;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView tierRateDataGridView1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.DataGridView surchargeDataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}