using System;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Sets a new password for an existing account.
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
            pnlUsername.Visible = false;
            pnlPassword.Visible = false;
            pnlConfirmPassword.Visible = false;
            btnResetPassword.Visible = false;
            Label message = new Label
            {
                AutoSize = false,
                Font = new System.Drawing.Font("Cambria", 14F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.MidnightBlue,
                Location = new System.Drawing.Point(60, 170),
                Size = new System.Drawing.Size(510, 100),
                Text = "Please contact the administrator to reset your account password.",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            Controls.Add(message);

            Button close = new Button
            {
                BackColor = System.Drawing.Color.FromArgb(0, 29, 57),
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(220, 300),
                Size = new System.Drawing.Size(190, 38),
                Text = "CLOSE"
            };
            close.Click += delegate { Close(); };
            Controls.Add(close);
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact the administrator to reset your account password.",
                "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
