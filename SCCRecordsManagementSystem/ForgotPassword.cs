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
        private readonly TextBox username = new TextBox { Dock = DockStyle.Fill, MaxLength = 50 };
        private readonly TextBox password = new TextBox { Dock = DockStyle.Fill, UseSystemPasswordChar = true, MaxLength = 72 };
        private readonly TextBox confirm = new TextBox { Dock = DockStyle.Fill, UseSystemPasswordChar = true, MaxLength = 72 };
        public ForgotPassword() : this(null) { }
        public ForgotPassword(string account)
        {
            InitializeComponent();
            Program.PrepareWindow(this);
            panel1.Controls.Add(username); panel2.Controls.Add(password); panel3.Controls.Add(confirm);
            button2.Enabled = Database.IsAdmin;
            AcceptButton = button2;
            if (!Database.IsAdmin)
                Program.AddLabel(this, "Contact the administrator to reset your password.", 120, 410, 450, 35);
            else username.Text = account ?? Database.Username;
            button2.Click += ResetPassword;
        }

        private void ResetPassword(object sender, EventArgs e)
        {
            try
            {
                Database.RequireAdmin();
                Database.CheckText(username.Text, "Username", 50);
                if (password.Text.Length < 6 || password.Text != confirm.Text)
                    throw new ArgumentException("Enter matching passwords with at least 6 characters.");
                if (Database.Execute("UPDATE accounts SET password=@password WHERE username=@name",
                    new Dictionary<string, object> { { "@password", BCrypt.Net.BCrypt.HashPassword(password.Text) },
                        { "@name", username.Text.Trim() } }) == 0)
                    throw new InvalidOperationException("No account has that username.");
                MessageBox.Show("Password changed.");
                Close();
            }
            catch (Exception ex) { Program.Error(ex); }
        }
    }
}
