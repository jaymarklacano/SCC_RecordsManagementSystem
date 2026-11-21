using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;

namespace SCCRecordsManagementSystem
{
    // Creates a new ADMIN account.
    // An existing Admin must type his/her password to allow it.
    // (Officer accounts are created in the Officer Records menu.)
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void CreateAccount_Load(object sender, EventArgs e)
        {
            txtCreateUsername.Focus();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string username = txtCreateUsername.Text.Trim();
            string password = txtCreatePassword.Text;
            string confirmPassword = txtConfrmPassword.Text;
            string adminPassword = txtAdminPassword.Text;

            // 1. Basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Username: 4 to 50 letters, numbers, dot or underscore only
            if (Regex.IsMatch(username, "^[A-Za-z0-9_.]{4,50}$") == false)
            {
                MessageBox.Show("Username must be 4 to 50 characters and can only use letters, numbers, dot (.) and underscore (_).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCreateUsername.Focus();
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCreatePassword.Focus();
                return;
            }

            // 2. Check if passwords match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfrmPassword.Clear();
                txtConfrmPassword.Focus();
                return;
            }

            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();

                    // 3. Only an Admin can create accounts. Check the admin password.
                    //    (If there is no Admin account at all, the first one can be created freely.)
                    DataTable admins = new DataTable();

                    using (var cmd = new MySqlCommand("SELECT password FROM accounts WHERE role = 'Admin'", conn))
                    {
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(admins);
                        }
                    }

                    if (admins.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(adminPassword))
                        {
                            MessageBox.Show("Only an Admin can create a new account.\nPlease ask the Admin to type the Admin Password.", "Admin Password Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtAdminPassword.Focus();
                            return;
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
                    }

                    // 4. Hash the password securely using BCrypt
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                    // 5. Save to database
                    string query = "INSERT INTO accounts (username, password, role) VALUES (@username, @password, 'Admin')";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Admin account \"" + username + "\" created successfully!\nYou can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields after success
                    txtCreateUsername.Clear();
                    txtCreatePassword.Clear();
                    txtConfrmPassword.Clear();
                    txtAdminPassword.Clear();
                    this.Close();
                }
                catch (MySqlException ex)
                {
                    // Error code 1062 handles duplicate entry (username already exists)
                    if (ex.Number == 1062)
                    {
                        MessageBox.Show("That username is already taken. Please choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCreateUsername.Focus();
                    }
                    else
                    {
                        Database.showError("The account was not created.", ex);
                    }
                }
                catch (Exception ex)
                {
                    Database.showError("The account was not created.", ex);
                }
            }
        }
    }
}
