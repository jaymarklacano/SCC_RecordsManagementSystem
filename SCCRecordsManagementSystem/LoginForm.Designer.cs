namespace SCCRecordsManagementSystem
{
    partial class LoginForm
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
            this.windowButtons = new WindowButtons();
            this.pnlUsername = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.pnlPassword = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblForgotPassword = new System.Windows.Forms.Label();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.lnkCreateAccount = new System.Windows.Forms.LinkLabel();
            this.pnlUsername.SuspendLayout();
            this.pnlPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowButtons
            // 
            this.windowButtons.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            this.windowButtons.Location = new System.Drawing.Point(1000, 0);
            this.windowButtons.Size = new System.Drawing.Size(90, 34);
            this.windowButtons.Name = "windowButtons";
            this.windowButtons.TabIndex = 0;
            // 
            // pnlUsername
            // 
            this.pnlUsername.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlUsername.Location = new System.Drawing.Point(759, 241);
            this.pnlUsername.Size = new System.Drawing.Size(282, 47);
            this.pnlUsername.Controls.Add(this.txtUsername);
            this.pnlUsername.Name = "pnlUsername";
            this.pnlUsername.TabIndex = 1;
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(6, 15);
            this.txtUsername.Size = new System.Drawing.Size(270, 24);
            this.txtUsername.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.MaxLength = 30;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.TabIndex = 0;
            // 
            // pnlPassword
            // 
            this.pnlPassword.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlPassword.Location = new System.Drawing.Point(759, 360);
            this.pnlPassword.Size = new System.Drawing.Size(282, 47);
            this.pnlPassword.Controls.Add(this.txtPassword);
            this.pnlPassword.Name = "pnlPassword";
            this.pnlPassword.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(6, 15);
            this.txtPassword.Size = new System.Drawing.Size(270, 24);
            this.txtPassword.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.TabIndex = 0;
            // 
            // lblForgotPassword
            // 
            this.lblForgotPassword.AutoSize = true;
            this.lblForgotPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblForgotPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblForgotPassword.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForgotPassword.ForeColor = System.Drawing.Color.Firebrick;
            this.lblForgotPassword.Location = new System.Drawing.Point(840, 413);
            this.lblForgotPassword.Text = "Forgot Password?";
            this.lblForgotPassword.Name = "lblForgotPassword";
            this.lblForgotPassword.TabIndex = 3;
            this.lblForgotPassword.Click += new System.EventHandler(this.lblForgotPassword_Click);
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogIn.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.ForeColor = System.Drawing.Color.Black;
            this.btnLogIn.Location = new System.Drawing.Point(784, 451);
            this.btnLogIn.Size = new System.Drawing.Size(171, 33);
            this.btnLogIn.Text = "Log In";
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.TabIndex = 4;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // lnkCreateAccount
            // 
            this.lnkCreateAccount.AutoSize = true;
            this.lnkCreateAccount.BackColor = System.Drawing.Color.Transparent;
            this.lnkCreateAccount.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCreateAccount.LinkColor = System.Drawing.Color.FromArgb(0, 192, 0);
            this.lnkCreateAccount.Location = new System.Drawing.Point(821, 495);
            this.lnkCreateAccount.TabStop = true;
            this.lnkCreateAccount.Text = "Create Account";
            this.lnkCreateAccount.Name = "lnkCreateAccount";
            this.lnkCreateAccount.TabIndex = 5;
            this.lnkCreateAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCreateAccount_LinkClicked);
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._890410457_28463085843365401_1086666007497239010_n;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1090, 591);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCC Records Management System";
            this.AcceptButton = this.btnLogIn;
            this.Controls.Add(this.windowButtons);
            this.Controls.Add(this.pnlUsername);
            this.Controls.Add(this.pnlPassword);
            this.Controls.Add(this.lblForgotPassword);
            this.Controls.Add(this.btnLogIn);
            this.Controls.Add(this.lnkCreateAccount);
            this.Name = "LoginForm";
            this.pnlUsername.ResumeLayout(false);
            this.pnlUsername.PerformLayout();
            this.pnlPassword.ResumeLayout(false);
            this.pnlPassword.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WindowButtons windowButtons;
        private System.Windows.Forms.Panel pnlUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel pnlPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblForgotPassword;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.LinkLabel lnkCreateAccount;
    }
}
