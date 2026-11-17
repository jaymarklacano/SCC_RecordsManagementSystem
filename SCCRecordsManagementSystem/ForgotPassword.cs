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
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (Validator.IsEmpty(username) || newPassword.Length == 0 || confirmPassword.Length == 0)
            {
                ShowWarning("Please fill in all the fields.");
                return;
            }

            if (!Validator.IsValidPassword(newPassword))
            {
                ShowWarning("The new password must have at least 6 characters.");
                txtNewPassword.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                ShowWarning("The passwords do not match.");
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                if (!AccountService.ResetPassword(username, newPassword))
                {
                    ShowWarning("No account was found with that username.");
                    txtUsername.Focus();
                    return;
                }

                MessageBox.Show("Your password was changed. You can now log in with the new password.",
                    "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Forgot Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
