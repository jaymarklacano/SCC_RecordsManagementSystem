using System;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Login screen. This is the main form of the program: it stays hidden while the other pages are open.
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            lnkCreateAccount.Visible = false;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (Validator.IsEmpty(username) || password.Length == 0)
            {
                MessageBox.Show("Please enter both your username and password.", "Log In",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (AccountService.Login(username, password))
                {
                    Session.Username = username;
                    ClearFields();
                    Hide();
                    new Dashboard().Show();
                }
                else
                {
                    MessageBox.Show("The username or password is incorrect.", "Log In Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Log In",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (CreateAccount createAccountForm = new CreateAccount())
            {
                createAccountForm.ShowDialog(this);
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            using (ForgotPassword forgotPasswordForm = new ForgotPassword())
            {
                forgotPasswordForm.ShowDialog(this);
            }
        }

        // Called after logging out, so the next person starts with empty boxes.
        public void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }
    }
}
