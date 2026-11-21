namespace SCCRecordsManagementSystem
{
    partial class ForgotPassword
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
            this.btnForgotPass = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.txtNewConfirmPass = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlAdminPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Location = new System.Drawing.Point(176, 196);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 24);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.txtNewPass);
            this.panel2.Location = new System.Drawing.Point(176, 256);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 24);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.AliceBlue;
            this.panel3.Controls.Add(this.txtNewConfirmPass);
            this.panel3.Location = new System.Drawing.Point(176, 324);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 24);
            this.panel3.TabIndex = 2;
            // 
            // btnForgotPass
            // 
            this.btnForgotPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(65)))), ((int)(((byte)(116)))));
            this.btnForgotPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForgotPass.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForgotPass.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnForgotPass.Location = new System.Drawing.Point(224, 363);
            this.btnForgotPass.Name = "btnForgotPass";
            this.btnForgotPass.Size = new System.Drawing.Size(189, 36);
            this.btnForgotPass.TabIndex = 5;
            this.btnForgotPass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnForgotPass.Text = "RESET PASSWORD";
            this.btnForgotPass.UseVisualStyleBackColor = false;
            this.btnForgotPass.Click += new System.EventHandler(this.btnForgotPass_Click);
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtUser.Location = new System.Drawing.Point(-3, 0);
            this.txtUser.MaxLength = 50;
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(304, 22);
            this.txtUser.TabIndex = 3;
            // 
            // txtNewPass
            // 
            this.txtNewPass.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtNewPass.Location = new System.Drawing.Point(-3, 0);
            this.txtNewPass.MaxLength = 100;
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Size = new System.Drawing.Size(304, 22);
            this.txtNewPass.TabIndex = 6;
            this.txtNewPass.UseSystemPasswordChar = true;
            // 
            // txtNewConfirmPass
            // 
            this.txtNewConfirmPass.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtNewConfirmPass.Location = new System.Drawing.Point(0, -1);
            this.txtNewConfirmPass.MaxLength = 100;
            this.txtNewConfirmPass.Name = "txtNewConfirmPass";
            this.txtNewConfirmPass.Size = new System.Drawing.Size(304, 22);
            this.txtNewConfirmPass.TabIndex = 6;
            this.txtNewConfirmPass.UseSystemPasswordChar = true;
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
            // ForgotPassword
            // 
            this.AcceptButton = this.btnForgotPass;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources.forgotPass;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(634, 472);
            this.Controls.Add(this.lblAdminPassword);
            this.Controls.Add(this.pnlAdminPassword);
            this.Controls.Add(this.btnForgotPass);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reset Password";
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
        private System.Windows.Forms.Button btnForgotPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.TextBox txtNewConfirmPass;
    }
}