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

            // 1. Check if any fields are empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Check if the new passwords match each other
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();

                    // 3. Check if the username exists in the accounts table
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

                    // Security Best Practice: Show a generic success message even if the username 
                    // isn't found, so attackers can't guess valid usernames in your system.
                    if (accountId == -1)
                    {
                        MessageBox.Show("If that username exists, the password has been reset successfully.", "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 4. Hash the new password securely using BCrypt
                    string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                    // 5. Update the password in the database for this account
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
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
