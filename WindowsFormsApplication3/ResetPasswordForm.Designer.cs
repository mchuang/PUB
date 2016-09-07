namespace PUB
{
    partial class ResetPasswordForm
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
            this.resetPassword = new System.Windows.Forms.Button();
            this.userIDComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "User ID";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(170, 124);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(182, 22);
            this.passwordTextBox.TabIndex = 2;
            // 
            // resetPassword
            // 
            this.resetPassword.Location = new System.Drawing.Point(170, 199);
            this.resetPassword.Name = "resetPassword";
            this.resetPassword.Size = new System.Drawing.Size(96, 49);
            this.resetPassword.TabIndex = 3;
            this.resetPassword.Text = "Reset Password";
            this.resetPassword.UseVisualStyleBackColor = true;
            this.resetPassword.Click += new System.EventHandler(this.resetPassword_Click);
            // 
            // userIDComboBox
            // 
            this.userIDComboBox.FormattingEnabled = true;
            this.userIDComboBox.Location = new System.Drawing.Point(170, 86);
            this.userIDComboBox.Name = "userIDComboBox";
            this.userIDComboBox.Size = new System.Drawing.Size(182, 24);
            this.userIDComboBox.TabIndex = 1;
            this.userIDComboBox.SelectedIndexChanged += new System.EventHandler(this.userIDComboBox_SelectedIndexChanged);
            // 
            // ResetPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 327);
            this.Controls.Add(this.userIDComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.resetPassword);
            this.Name = "ResetPasswordForm";
            this.Text = "ResetPasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button resetPassword;
        private System.Windows.Forms.ComboBox userIDComboBox;
    }
}