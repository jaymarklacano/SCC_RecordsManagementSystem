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

namespace SCCRecordsManagementSystem
{
    // Resets the password of any account (Admin or Officer).
    // An Admin must type his/her password to allow the reset.
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void btnForgotPass_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string newPassword = txtNewPass.Text;
            string confirmPassword = txtNewConfirmPass.Text;
            string adminPassword = txtAdminPassword.Text;

            // 1. Check if any fields are empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("The new password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPass.Focus();
                return;
            }

            // 2. Check if the new passwords match each other
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewConfirmPass.Clear();
                txtNewConfirmPass.Focus();
                return;
            }

            if (string.IsNullOrEmpty(adminPassword))
            {
                MessageBox.Show("Only an Admin can reset a password.\nPlease ask the Admin to type the Admin Password.", "Admin Password Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdminPassword.Focus();
                return;
            }

            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    // 3. Check the Admin Password
                    DataTable admins = new DataTable();

                    using (var cmd = new MySqlCommand("SELECT password FROM accounts WHERE role = 'Admin'", conn))
                    {
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(admins);
                        }
                    }

                    bool adminPasswordIsCorrect = false;

                    foreach (DataRow row in admins.Rows)
                    {
                        try
                        {
                            if (BCrypt.Net.BCrypt.Verify(adminPassword, row["password"].ToString()))
                            {
                                adminPasswordIsCorrect = true;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // Skip a damaged password hash
                        }
                    }

                    if (adminPasswordIsCorrect == false)
                    {
                        MessageBox.Show("The Admin Password is wrong.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAdminPassword.Clear();
                        txtAdminPassword.Focus();
                        return;
                    }

                    // 4. Check if the username exists in the accounts table
                    int accountId = -1;
                    string checkQuery = "SELECT account_id FROM accounts WHERE username = @username";

                    using (var cmd = new MySqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            accountId = Convert.ToInt32(result);
                        }
                    }

                    if (accountId == -1)
                    {
                        MessageBox.Show("The username \"" + username + "\" was not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUser.Focus();
                        return;
                    }

                    // 5. Hash the new password securely using BCrypt
                    string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                    // 6. Update the password in the database for this account
                    string updateQuery = "UPDATE accounts SET password = @password WHERE account_id = @accountId";

                    using (var cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@password", hashedNewPassword);
                        cmd.Parameters.AddWithValue("@accountId", accountId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Password updated successfully! You can now log in with your new password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Close the form after successful reset
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Database.showError("The password was not changed.", ex);
            }
        }
    }
}
