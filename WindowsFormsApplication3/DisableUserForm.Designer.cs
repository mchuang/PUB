namespace PUB
{
    partial class DisableUserForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.disableUser = new System.Windows.Forms.Button();
            this.userIDComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "User ID";
            // 
            // disableUser
            // 
            this.disableUser.Location = new System.Drawing.Point(151, 144);
            this.disableUser.Name = "disableUser";
            this.disableUser.Size = new System.Drawing.Size(96, 49);
            this.disableUser.TabIndex = 2;
            this.disableUser.Text = "Disable User";
            this.disableUser.UseVisualStyleBackColor = true;
            this.disableUser.Click += new System.EventHandler(this.disableUser_Click);
            // 
            // userIDComboBox
            // 
            this.userIDComboBox.FormattingEnabled = true;
            this.userIDComboBox.Location = new System.Drawing.Point(177, 79);
            this.userIDComboBox.Name = "userIDComboBox";
            this.userIDComboBox.Size = new System.Drawing.Size(182, 24);
            this.userIDComboBox.TabIndex = 1;
            this.userIDComboBox.SelectedIndexChanged += new System.EventHandler(this.userIDComboBox_SelectedIndexChanged);
            // 
            // DisableUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 320);
            this.Controls.Add(this.userIDComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.disableUser);
            this.Name = "DisableUserForm";
            this.Text = "DisableUserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button disableUser;
        private System.Windows.Forms.ComboBox userIDComboBox;
    }
}