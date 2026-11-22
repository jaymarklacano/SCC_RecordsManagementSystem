using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SCCRecordsManagementSystem.AccountInfo;

namespace SCCRecordsManagementSystem
{
    // This is the LOG IN form (the first form that opens).
    public partial class lnkCreateAccount : Form
    {
        public lnkCreateAccount()
        {
            InitializeComponent();
        }

        private void lnkCreateAccount_Shown(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void lnkCreateNewAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateAccount account = new CreateAccount();
            account.ShowDialog();
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPassword forgotpassword = new ForgotPassword();
            forgotpassword.ShowDialog();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // When checked, the password is shown as normal text.
            if (chkShowPassword.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string plainPassword = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(plainPassword))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    int accountId = 0;
                    string storedHash = null;
                    string role = "";
                    string savedUsername = "";

                    // 1. Find the account
                    string query = "SELECT account_id, username, password, role FROM accounts WHERE username = @username";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                accountId = Convert.ToInt32(reader["account_id"]);
                                savedUsername = reader["username"].ToString();
                                storedHash = reader["password"].ToString();
                                role = reader["role"].ToString();
                            }
                        }
                    }

                    // 2. Check if user exists and verify the password hash
                    bool passwordIsCorrect = false;

                    if (storedHash != null)
                    {
                        try
                        {
                            passwordIsCorrect = BCrypt.Net.BCrypt.Verify(plainPassword, storedHash);
                        }
                        catch (Exception)
                        {
                            // The saved password is not a valid BCrypt hash.
                            passwordIsCorrect = false;
                        }
                    }

                    if (passwordIsCorrect == false)
                    {
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Clear();
                        txtPassword.Focus();
                        return;
                    }

                    // 3. Save the log in time in the login history
                    long loginId = 0;
                    string historyQuery = "INSERT INTO login_history (account_id, username, role, logged_in) VALUES (@accountId, @username, @role, NOW())";

                    using (var cmd = new MySqlCommand(historyQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        cmd.Parameters.AddWithValue("@username", savedUsername);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.ExecuteNonQuery();
                        loginId = cmd.LastInsertedId;
                    }

                    // 4. Remember who is logged in (used by all the other forms)
                    Instantiate.currentUser = new AccountInfo();
                    Instantiate.currentUser.AccountId = accountId;
                    Instantiate.currentUser.Username = savedUsername;
                    Instantiate.currentUser.Role = role;
                    Instantiate.currentUser.LoginId = (int)loginId;

                    MessageBox.Show("Login successful! Welcome back, " + savedUsername + " (" + role + ").", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtPassword.Clear();
                    chkShowPassword.Checked = false;

                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                Database.showError("Log in failed.", ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you want to close the program?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lnkCreateAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.exitIfNoFormIsOpen(this);
        }
    }
}
