namespace PUB
{
    partial class ParkSpaceInfo
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
            this.label9 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.parkList = new System.Windows.Forms.ComboBox();
            this.parkNumberList = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ListOfTenants = new System.Windows.Forms.DataGridView();
            this.moveoutBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ListOfTenants)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(574, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 17);
            this.label9.TabIndex = 58;
            this.label9.Text = "Order";
            // 
            // textBox10
            // 
            this.textBox10.Enabled = false;
            this.textBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.Location = new System.Drawing.Point(625, 7);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(85, 23);
            this.textBox10.TabIndex = 57;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(197, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 17);
            this.label3.TabIndex = 62;
            this.label3.Text = "Park";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(942, 419);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 35);
            this.button2.TabIndex = 64;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(808, 419);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 35);
            this.button1.TabIndex = 63;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // parkList
            // 
            this.parkList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.parkList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.parkList.FormattingEnabled = true;
            this.parkList.Location = new System.Drawing.Point(240, 12);
            this.parkList.Name = "parkList";
            this.parkList.Size = new System.Drawing.Size(203, 21);
            this.parkList.TabIndex = 88;
            this.parkList.SelectedIndexChanged += new System.EventHandler(this.parkList_SelectedIndexChanged);
            // 
            // parkNumberList
            // 
            this.parkNumberList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.parkNumberList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.parkNumberList.FormattingEnabled = true;
            this.parkNumberList.Location = new System.Drawing.Point(108, 12);
            this.parkNumberList.Name = "parkNumberList";
            this.parkNumberList.Size = new System.Drawing.Size(70, 21);
            this.parkNumberList.TabIndex = 182;
            this.parkNumberList.SelectedIndexChanged += new System.EventHandler(this.parkNumberList_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(44, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 17);
            this.label15.TabIndex = 181;
            this.label15.Text = "Park No.";
            // 
            // ListOfTenants
            // 
            this.ListOfTenants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListOfTenants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListOfTenants.Location = new System.Drawing.Point(0, 59);
            this.ListOfTenants.Name = "ListOfTenants";
            this.ListOfTenants.Size = new System.Drawing.Size(1080, 350);
            this.ListOfTenants.TabIndex = 183;
            this.ListOfTenants.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.ListOfTenants_DefaultValuesNeeded);
            // 
            // moveoutBtn
            // 
            this.moveoutBtn.Location = new System.Drawing.Point(674, 419);
            this.moveoutBtn.Name = "moveoutBtn";
            this.moveoutBtn.Size = new System.Drawing.Size(128, 35);
            this.moveoutBtn.TabIndex = 184;
            this.moveoutBtn.Text = "Move Out";
            this.moveoutBtn.UseVisualStyleBackColor = true;
            this.moveoutBtn.Click += new System.EventHandler(this.moveoutBtn_Click);
            // 
            // ParkSpaceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1080, 466);
            this.Controls.Add(this.moveoutBtn);
            this.Controls.Add(this.ListOfTenants);
            this.Controls.Add(this.parkNumberList);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.parkList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox10);
            this.Name = "ParkSpaceInfo";
            this.Text = "TenantInfo";
            this.Load += new System.EventHandler(this.TenantInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListOfTenants)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox parkList;
        private System.Windows.Forms.ComboBox parkNumberList;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView ListOfTenants;
        private System.Windows.Forms.Button moveoutBtn;
    }
}