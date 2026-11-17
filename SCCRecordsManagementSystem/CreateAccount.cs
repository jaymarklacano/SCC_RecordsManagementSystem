using System;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Creates a new login account (the password is saved as a BCrypt hash).
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (Validator.IsEmpty(username) || password.Length == 0 || confirmPassword.Length == 0)
            {
                ShowWarning("Please fill in all the fields.");
                return;
            }

            if (!Validator.IsValidUsername(username))
            {
                ShowWarning("The username must be 4 to 30 characters long and use only letters, numbers, dots or underscores.");
                txtUsername.Focus();
                return;
            }

            if (!Validator.IsValidPassword(password))
            {
                ShowWarning("The password must have at least 6 characters.");
                txtPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                ShowWarning("The passwords do not match.");
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                if (AccountService.UsernameExists(username))
                {
                    ShowWarning("That username is already taken. Please choose another one.");
                    txtUsername.Focus();
                    return;
                }

                AccountService.CreateAccount(username, password);

                MessageBox.Show("Account created successfully. You can now log in.", "Create Account",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Create Account",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Create Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
