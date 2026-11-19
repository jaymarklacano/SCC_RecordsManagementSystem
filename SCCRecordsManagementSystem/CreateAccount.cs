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
using BCrypt.Net;

namespace SCCRecordsManagementSystem
{
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
            Program.PrepareWindow(this);
            txtCreateUsername.MaxLength = 50;
            txtCreatePassword.MaxLength = txtConfrmPassword.MaxLength = 72;
            AcceptButton = btnCreateAccount;
            txtCreatePassword.UseSystemPasswordChar = txtConfrmPassword.UseSystemPasswordChar = true;
        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {

        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (!Database.IsAdmin)
            {
                MessageBox.Show("Only the administrator may create accounts. Use Officer Records for officer accounts.");
                return;
            }
            string username = txtCreateUsername.Text.Trim();
            string password = txtCreatePassword.Text;
            string confirmPassword = txtConfrmPassword.Text;

            // 1. Basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Check if passwords match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (password.Length < 6 || password.Length > 72)
            {
                MessageBox.Show("Use a password of 6-72 characters.");
                return;
            }

            // 3. Hash the password securely using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // 4. Save to database
            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO accounts (username, password) VALUES (@username, @password)";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields after success
                    txtCreateUsername.Clear();
                    txtCreatePassword.Clear();
                    txtConfrmPassword.Clear();
                }
                catch (MySqlException ex)
                {
                    // Error code 1062 handles duplicate entry (username already exists)
                    if (ex.Number == 1062)
                    {
                        MessageBox.Show("That username is already taken. Please choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Program.Error(ex);
                    }
                }
            }
        }
    }
}
