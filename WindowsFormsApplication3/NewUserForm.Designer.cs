namespace PUB
{
    partial class NewUserForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userIDTextBox = new System.Windows.Forms.TextBox();
            this.NewUser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.securityList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "User ID";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(153, 132);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(183, 22);
            this.passwordTextBox.TabIndex = 2;
            // 
            // userIDTextBox
            // 
            this.userIDTextBox.Location = new System.Drawing.Point(153, 94);
            this.userIDTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.userIDTextBox.Name = "userIDTextBox";
            this.userIDTextBox.Size = new System.Drawing.Size(183, 22);
            this.userIDTextBox.TabIndex = 1;
            // 
            // NewUser
            // 
            this.NewUser.Location = new System.Drawing.Point(153, 234);
            this.NewUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NewUser.Name = "NewUser";
            this.NewUser.Size = new System.Drawing.Size(96, 49);
            this.NewUser.TabIndex = 3;
            this.NewUser.Text = "Create New User";
            this.NewUser.UseVisualStyleBackColor = true;
            this.NewUser.Click += new System.EventHandler(this.NewUser_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Security";
            // 
            // securityList
            // 
            this.securityList.FormattingEnabled = true;
            this.securityList.Location = new System.Drawing.Point(153, 167);
            this.securityList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.securityList.Name = "securityList";
            this.securityList.Size = new System.Drawing.Size(183, 24);
            this.securityList.TabIndex = 15;
            this.securityList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // NewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 353);
            this.Controls.Add(this.securityList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userIDTextBox);
            this.Controls.Add(this.NewUser);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "NewUserForm";
            this.Text = "NewUserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox userIDTextBox;
        private System.Windows.Forms.Button NewUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox securityList;
    }
}