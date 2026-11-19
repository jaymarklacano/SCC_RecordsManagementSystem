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
    public partial class lnkCreateAccount : Form
    {
        public lnkCreateAccount()
        {
            InitializeComponent();
            Program.PrepareWindow(this);
            txtUsername.MaxLength = 50;
            txtPassword.UseSystemPasswordChar = true;
            AcceptButton = btnLogIn;
            linkLabel1.Text = "Accounts are created by the administrator";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Ask the administrator to create your account in Officer Records.");
            
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

            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT account_id,username,password,role FROM accounts WHERE username = @username";

                    DataTable accounts = new DataTable();

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) adapter.Fill(accounts);
                    }

                    // 1. Check if user exists and verify the password hash
                    if (accounts.Rows.Count == 1 && BCrypt.Net.BCrypt.Verify(plainPassword, accounts.Rows[0]["password"].ToString()))
                    {
                        DataRow row = accounts.Rows[0];
                        using (MySqlCommand log = new MySqlCommand(
                            "INSERT INTO login_history(account_id,username,logged_in) VALUES(@id,@name,NOW())", conn))
                        {
                            log.Parameters.AddWithValue("@id", row["account_id"]);
                            log.Parameters.AddWithValue("@name", row["username"]);
                            log.ExecuteNonQuery();
                            Database.LoginId = Convert.ToInt32(log.LastInsertedId);
                        }
                        Database.AccountId = Convert.ToInt32(row["account_id"]);
                        Database.Username = row["username"].ToString();
                        Database.Role = row["role"].ToString();
                        Dashboard dashboard = new Dashboard();
                        dashboard.Show();
                        this.Hide();
                        txtPassword.Clear();
                       
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Database.AccountId = Database.LoginId = 0; Database.Role = Database.Username = "";
                    Program.Error(ex);
                }
            }
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            using (ForgotPassword form = new ForgotPassword()) form.ShowDialog(this);
        }
    }
}
