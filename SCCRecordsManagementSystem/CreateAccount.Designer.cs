namespace SCCRecordsManagementSystem
{
    partial class CreateAccount
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlAdminPassword = new System.Windows.Forms.Panel();
            this.txtAdminPassword = new System.Windows.Forms.TextBox();
            this.lblAdminPassword = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCreateAccount = new System.Windows.Forms.Button();
            this.txtConfrmPassword = new System.Windows.Forms.TextBox();
            this.txtCreatePassword = new System.Windows.Forms.TextBox();
            this.txtCreateUsername = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlAdminPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.txtCreateUsername);
            this.panel1.Location = new System.Drawing.Point(176, 196);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 24);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.txtCreatePassword);
            this.panel2.Location = new System.Drawing.Point(176, 256);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 24);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.AliceBlue;
            this.panel3.Controls.Add(this.txtConfrmPassword);
            this.panel3.Location = new System.Drawing.Point(176, 323);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 24);
            this.panel3.TabIndex = 3;
            // 
            // btnCreateAccount
            // 
            this.btnCreateAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(65)))), ((int)(((byte)(116)))));
            this.btnCreateAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateAccount.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateAccount.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnCreateAccount.Location = new System.Drawing.Point(224, 362);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(189, 36);
            this.btnCreateAccount.TabIndex = 5;
            this.btnCreateAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateAccount.Text = "CREATE ACCOUNT";
            this.btnCreateAccount.UseVisualStyleBackColor = false;
            this.btnCreateAccount.Click += new System.EventHandler(this.btnCreateAccount_Click);
            // 
            // txtConfrmPassword
            // 
            this.txtConfrmPassword.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtConfrmPassword.Location = new System.Drawing.Point(0, -1);
            this.txtConfrmPassword.MaxLength = 100;
            this.txtConfrmPassword.Name = "txtConfrmPassword";
            this.txtConfrmPassword.Size = new System.Drawing.Size(304, 22);
            this.txtConfrmPassword.TabIndex = 2;
            this.txtConfrmPassword.UseSystemPasswordChar = true;
            // 
            // txtCreatePassword
            // 
            this.txtCreatePassword.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCreatePassword.Location = new System.Drawing.Point(0, -1);
            this.txtCreatePassword.MaxLength = 100;
            this.txtCreatePassword.Name = "txtCreatePassword";
            this.txtCreatePassword.Size = new System.Drawing.Size(304, 22);
            this.txtCreatePassword.TabIndex = 1;
            this.txtCreatePassword.UseSystemPasswordChar = true;
            // 
            // txtCreateUsername
            // 
            this.txtCreateUsername.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCreateUsername.Location = new System.Drawing.Point(0, 0);
            this.txtCreateUsername.MaxLength = 50;
            this.txtCreateUsername.Name = "txtCreateUsername";
            this.txtCreateUsername.Size = new System.Drawing.Size(304, 22);
            this.txtCreateUsername.TabIndex = 2;
            // 
            // lblAdminPassword
            // 
            this.lblAdminPassword.AutoSize = true;
            this.lblAdminPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblAdminPassword.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminPassword.ForeColor = System.Drawing.Color.White;
            this.lblAdminPassword.Location = new System.Drawing.Point(124, 428);
            this.lblAdminPassword.Name = "lblAdminPassword";
            this.lblAdminPassword.Size = new System.Drawing.Size(132, 20);
            this.lblAdminPassword.TabIndex = 6;
            this.lblAdminPassword.Text = "Admin Password *";
            // 
            // pnlAdminPassword
            // 
            this.pnlAdminPassword.BackColor = System.Drawing.Color.AliceBlue;
            this.pnlAdminPassword.Controls.Add(this.txtAdminPassword);
            this.pnlAdminPassword.Location = new System.Drawing.Point(262, 425);
            this.pnlAdminPassword.Name = "pnlAdminPassword";
            this.pnlAdminPassword.Size = new System.Drawing.Size(244, 24);
            this.pnlAdminPassword.TabIndex = 4;
            // 
            // txtAdminPassword
            // 
            this.txtAdminPassword.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtAdminPassword.Location = new System.Drawing.Point(0, 0);
            this.txtAdminPassword.MaxLength = 100;
            this.txtAdminPassword.Name = "txtAdminPassword";
            this.txtAdminPassword.Size = new System.Drawing.Size(244, 22);
            this.txtAdminPassword.TabIndex = 0;
            this.txtAdminPassword.UseSystemPasswordChar = true;
            // 
            // CreateAccount
            // 
            this.AcceptButton = this.btnCreateAccount;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources.createAcc;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(634, 472);
            this.Controls.Add(this.lblAdminPassword);
            this.Controls.Add(this.pnlAdminPassword);
            this.Controls.Add(this.btnCreateAccount);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CreateAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Admin Account";
            this.Load += new System.EventHandler(this.CreateAccount_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlAdminPassword.ResumeLayout(false);
            this.pnlAdminPassword.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlAdminPassword;
        private System.Windows.Forms.TextBox txtAdminPassword;
        private System.Windows.Forms.Label lblAdminPassword;
        private System.Windows.Forms.Button btnCreateAccount;
        private System.Windows.Forms.TextBox txtCreateUsername;
        private System.Windows.Forms.TextBox txtCreatePassword;
        private System.Windows.Forms.TextBox txtConfrmPassword;
    }
}