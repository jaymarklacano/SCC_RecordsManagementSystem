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
    public partial class RulesPermission : Form
    {
        private int ruleId;
        public RulesPermission()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu(); Program.Grid(dataGridView1); Program.Grid(dataGridView2);
            textBox2.ReadOnly = true; textBox3.MaxLength = 255;
            checkedListBox1.Items.Clear(); checkedListBox1.Items.Add("Active", true);
            label8.Text = "Officer access (Admin always has full access):"; label8.AutoSize = true;
            checkedListBox2.Height = 88; checkedListBox2.CheckOnClick = true;
            Program.AddButton(panel3, "Save Permissions", 250, 137, 180, SavePermissions);
            Program.AddButton(this, "Delete Rule", 965, 66, 150, DeleteRule);
            Program.AddButton(this, "New Admin", 845, 66, 110, delegate {
                try { Database.RequireAdmin(); using (CreateAccount form = new CreateAccount()) form.ShowDialog(this); }
                catch (Exception ex) { Program.Error(ex); }
            });
            Program.AddButton(this, "Manage Accounts", 830, 420, 160, ShowAccounts);
            button1.Click += delegate { ResetRule(); };
            button2.Click += SaveRule;
            textBox1.TextChanged += delegate { LoadRules(); };
            dataGridView1.SelectionChanged += delegate {
                if (dataGridView1.CurrentRow == null) { ResetRule(); return; }
                DataRow row = Program.SelectedRow(dataGridView1);
                ruleId = Convert.ToInt32(row["rule_id"]); textBox2.Text = ruleId.ToString(); textBox3.Text = row["description"].ToString();
                checkedListBox1.SetItemChecked(0, Convert.ToBoolean(row["active"]));
            };
            Shown += delegate {
                try
                {
                    Database.RequireAdmin(); LoadRules(); LoadPermissions();
                }
                catch (Exception ex) { Program.Error(ex); }
            };
        }

        private void WireMenu()
        {
            button12.Click += delegate { Program.Logout(this); };
            button13.Click += delegate { Program.OpenPage(this, "Dashboard", () => new Dashboard()); };
            button14.Enabled = button20.Enabled = Database.IsAdmin;
            button14.Click += delegate { LoadRules(); };
            button15.Click += delegate { Program.OpenPage(this, "Obligations", () => new Obligations()); };
            button16.Click += delegate { Program.OpenPage(this, "Reports and Analysis", () => new ReportsAndAnalysis()); };
            button17.Click += delegate { Program.OpenPage(this, "Attendance Monitoring", () => new AttendanceMonitoring()); };
            button18.Click += delegate { Program.OpenPage(this, "Event Management", () => new Event_Management()); };
            button19.Click += delegate { Program.OpenPage(this, "Face Recognition", () => new FaceRecognition()); };
            button20.Click += delegate { Program.OpenPage(this, "Officer Records", () => new Officerr_Records()); };
            button21.Click += delegate { Program.OpenPage(this, "Student Records", () => new StudentRecords()); };
            button22.Click += delegate { Program.OpenPage(this, "Departments", () => new Departments()); };
        }

        private void LoadRules()
        {
            try
            {
                Database.RequireAdmin();
                dataGridView1.DataSource = Database.GetData("SELECT * FROM rules WHERE description LIKE @q ORDER BY rule_id",
                    new Dictionary<string, object> { { "@q", "%" + textBox1.Text.Trim() + "%" } });
                if (dataGridView1.Rows.Count == 0) ResetRule();
            }
            catch (Exception ex) { dataGridView1.DataSource = null; ResetRule(); Program.Error(ex); }
        }

        private void ResetRule()
        {
            ruleId = 0; textBox2.Text = "(new)"; textBox3.Clear(); checkedListBox1.SetItemChecked(0, true);
        }

        private void SaveRule(object sender, EventArgs e)
        {
            try
            {
                Database.RequireAdmin(); Database.CheckText(textBox3.Text, "Description", 255);
                Database.Execute(ruleId == 0 ? "INSERT INTO rules(description,active) VALUES(@text,@active)"
                    : "UPDATE rules SET description=@text,active=@active WHERE rule_id=@id",
                    new Dictionary<string, object> { { "@id", ruleId }, { "@text", textBox3.Text.Trim() }, { "@active", checkedListBox1.GetItemChecked(0) } });
                LoadRules(); MessageBox.Show("Rule saved. Rules are informational; absence points remain 10 per session.");
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void DeleteRule(object sender, EventArgs e)
        {
            try
            {
                Database.RequireAdmin();
                DataRow row = Program.SelectedRow(dataGridView1);
                if (MessageBox.Show("Delete this rule?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                Database.Execute("DELETE FROM rules WHERE rule_id=@id", new Dictionary<string, object> { { "@id", row["rule_id"] } });
                LoadRules(); ResetRule();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void LoadPermissions()
        {
            DataTable rows = Database.GetData("SELECT module,allowed FROM role_permissions ORDER BY module");
            checkedListBox2.Items.Clear();
            foreach (DataRow row in rows.Rows) checkedListBox2.Items.Add(row["module"].ToString(), Convert.ToBoolean(row["allowed"]));
            dataGridView2.DataSource = rows;
        }

        private void SavePermissions(object sender, EventArgs e)
        {
            try
            {
                Database.RequireAdmin();
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        for (int i = 0; i < checkedListBox2.Items.Count; i++)
                            using (var command = Database.Command(connection, "UPDATE role_permissions SET allowed=@allowed WHERE module=@module",
                                new Dictionary<string, object> { { "@allowed", checkedListBox2.GetItemChecked(i) }, { "@module", checkedListBox2.Items[i].ToString() } }, transaction))
                                command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
                LoadPermissions(); MessageBox.Show("Officer permissions saved.");
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void ShowAccounts(object sender, EventArgs e)
        {
            try
            {
                Database.RequireAdmin();
                using (Form form = new Form { Text = "User Accounts", ClientSize = new Size(760, 430), StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false })
                using (DataGridView grid = new DataGridView { Location = new Point(10, 10), Size = new Size(740, 340) })
                {
                    Program.Grid(grid); form.Controls.Add(grid);
                    Action load = delegate
                    {
                        grid.DataSource = Database.GetData(@"SELECT a.account_id,a.username,a.role,a.created_at,
                            (SELECT MAX(h.logged_in) FROM login_history h WHERE h.account_id=a.account_id) AS last_login
                            FROM accounts a ORDER BY a.role,a.username");
                        grid.Columns["account_id"].Visible = false;
                    };
                    load();
                    Program.AddButton(form, "New Admin", 10, 365, 120, delegate {
                        using (CreateAccount create = new CreateAccount()) create.ShowDialog(form);
                        load();
                    });
                    Program.AddButton(form, "Reset Password", 140, 365, 150, delegate {
                        try
                        {
                            DataRow row = Program.SelectedRow(grid);
                            using (ForgotPassword reset = new ForgotPassword(row["username"].ToString())) reset.ShowDialog(form);
                        }
                        catch (Exception ex) { Program.Error(ex); }
                    });
                    Program.AddButton(form, "Delete Admin", 300, 365, 130, delegate {
                        try
                        {
                            DataRow row = Program.SelectedRow(grid);
                            if (MessageBox.Show("Delete the admin account " + row["username"] + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                            DeleteAdmin(Convert.ToInt32(row["account_id"])); load();
                        }
                        catch (Exception ex) { Program.Error(ex); }
                    });
                    Program.AddLabel(form, "Officer logins are added, edited and deleted in Officer Records.", 440, 365, 310, 45);
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        public static void DeleteAdmin(int accountId)
        {
            Database.RequireAdmin();
            if (accountId == Database.AccountId) throw new InvalidOperationException("You cannot delete the account you are signed in with.");
            if (Database.Execute("DELETE FROM accounts WHERE account_id=@id AND role='Admin'",
                new Dictionary<string, object> { { "@id", accountId } }) == 0)
                throw new InvalidOperationException("Only other administrator accounts can be deleted here. Delete officers in Officer Records.");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetRule();
        }
    }
}
