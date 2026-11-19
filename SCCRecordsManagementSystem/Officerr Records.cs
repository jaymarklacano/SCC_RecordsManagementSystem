using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SCCRecordsManagementSystem
{
    public partial class Officerr_Records : Form
    {
        private readonly ComboBox department = new ComboBox();
        private readonly ComboBox year = new ComboBox();
        private Label summary;
        public Officerr_Records()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu(); Program.Grid(dataGridView1);
            department.SetBounds(350, 85, 220, 25); Controls.Add(department);
            year.SetBounds(590, 85, 140, 25); Controls.Add(year);
            year.DropDownStyle = ComboBoxStyle.DropDownList;
            year.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year" }); year.SelectedIndex = 0;
            Program.AddButton(this, "Edit / Password", 910, 145, 145, delegate { EditOfficer(false); });
            Program.AddButton(this, "Delete", 1060, 145, 85, DeleteClick);
            Program.AddLabel(this, "Department", 350, 60, 220);
            Program.AddLabel(this, "Year", 590, 60, 140);
            summary = Program.AddLabel(this, "", 350, 115, 780, 25);
            dataGridView1.CellDoubleClick += delegate { EditOfficer(false); };
            button1.Click += delegate { EditOfficer(true); }; button2.Click += delegate { LoadOfficers(); };
            textBox1.TextChanged += delegate { LoadOfficers(); };
            department.SelectionChangeCommitted += delegate { LoadOfficers(); }; year.SelectedIndexChanged += delegate { LoadOfficers(); };
            Shown += delegate {
                try { Database.RequireAdmin(); Program.Choices(department, Database.GetData("SELECT department_id,department_code FROM departments"), "department_id", "department_code", "All Departments"); LoadOfficers(); }
                catch (Exception ex) { Program.Error(ex); }
            };
        }

        private void WireMenu()
        {
            button12.Click += delegate { Program.Logout(this); };
            button13.Click += delegate { Program.OpenPage(this, "Dashboard", () => new Dashboard()); };
            button14.Enabled = button20.Enabled = Database.IsAdmin;
            button14.Click += delegate { Program.OpenPage(this, "Rules and Permission", () => new RulesPermission()); };
            button15.Click += delegate { Program.OpenPage(this, "Obligations", () => new Obligations()); };
            button16.Click += delegate { Program.OpenPage(this, "Reports and Analysis", () => new ReportsAndAnalysis()); };
            button17.Click += delegate { Program.OpenPage(this, "Attendance Monitoring", () => new AttendanceMonitoring()); };
            button18.Click += delegate { Program.OpenPage(this, "Event Management", () => new Event_Management()); };
            button19.Click += delegate { Program.OpenPage(this, "Face Recognition", () => new FaceRecognition()); };
            button20.Click += delegate { LoadOfficers(); };
            button21.Click += delegate { Program.OpenPage(this, "Student Records", () => new StudentRecords()); };
            button22.Click += delegate { Program.OpenPage(this, "Departments", () => new Departments()); };
        }

        private void LoadOfficers()
        {
            try
            {
                Database.RequireAdmin();
                dataGridView1.DataSource = Database.GetData(@"SELECT o.*,a.username,d.department_code,
                    CONCAT_WS(' ',o.first_name,o.middle_name,o.last_name) AS full_name FROM officers o
                    JOIN accounts a ON a.account_id=o.account_id JOIN departments d ON d.department_id=o.department_id
                    WHERE (@department=0 OR o.department_id=@department) AND (@year='' OR o.year_level=@year)
                    AND (CONCAT_WS(' ',o.first_name,o.middle_name,o.last_name) LIKE @q OR o.officer_number LIKE @q OR a.username LIKE @q OR o.position LIKE @q)
                    ORDER BY o.last_name", new Dictionary<string, object> {
                    { "@department", Program.SelectedId(department) }, { "@year", year.SelectedIndex == 0 ? "" : year.Text }, { "@q", "%" + textBox1.Text.Trim() + "%" } });
                foreach (string name in new[] { "officer_id", "account_id", "department_id", "first_name", "middle_name", "last_name" }) dataGridView1.Columns[name].Visible = false;
                dataGridView1.Columns["full_name"].DisplayIndex = 0;
                dataGridView1.Columns["full_name"].HeaderText = "Officer Name";
                dataGridView1.Columns["officer_number"].DisplayIndex = 1;
                dataGridView1.Columns["username"].DisplayIndex = 2;
                summary.Text = dataGridView1.Rows.Count == 0 ? "No officers match these filters." : dataGridView1.Rows.Count + " officer(s). Double-click to edit details or reset a password.";
            }
            catch (Exception ex) { dataGridView1.DataSource = null; summary.Text = Database.ErrorMessage(ex); }
        }

        private void EditOfficer(bool add)
        {
            try
            {
                Database.RequireAdmin();
                using (OfficerEditor form = new OfficerEditor(add ? null : Program.SelectedRow(dataGridView1)))
                    if (form.ShowDialog(this) == DialogResult.OK) LoadOfficers();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            try
            {
                DataRow row = Program.SelectedRow(dataGridView1);
                if (MessageBox.Show("Delete " + row["full_name"] + " and their login?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                DeleteOfficer(Convert.ToInt32(row["officer_id"])); LoadOfficers();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        public static void DeleteOfficer(int id)
        {
            Database.RequireAdmin();
            Database.Execute("DELETE a FROM accounts a JOIN officers o ON o.account_id=a.account_id WHERE o.officer_id=@id AND a.role='Officer'",
                new Dictionary<string, object> { { "@id", id } });
        }

        public static void SaveOfficer(int id, Dictionary<string, object> values, string password)
        {
            Database.RequireAdmin();
            foreach (string key in new[] { "username", "officer_number", "first_name", "last_name", "position", "school_year", "year_level", "gender" })
                Database.CheckText(Convert.ToString(values["@" + key]), key.Replace('_', ' '), key == "officer_number" || key == "school_year" ? 20 : 50);
            Database.CheckText(Convert.ToString(values["@middle_name"]), "Middle name", 50, false);
            Database.CheckContact(Convert.ToString(values["@email"]), Convert.ToString(values["@contact_number"]));
            if (Convert.ToInt32(values["@department_id"]) <= 0) throw new ArgumentException("Choose a department.");
            if ((id == 0 || !string.IsNullOrEmpty(password)) && (password.Length < 6 || password.Length > 72))
                throw new ArgumentException("Use a password of 6-72 characters. Leave blank on edit to keep the current password.");
            Dictionary<string, object> p = new Dictionary<string, object>(values) { { "@id", id } };
            using (MySqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    int account;
                    if (id == 0)
                    {
                        p.Add("@password", BCrypt.Net.BCrypt.HashPassword(password));
                        using (MySqlCommand cmd = Database.Command(connection, "INSERT INTO accounts(username,password,role) VALUES(@username,@password,'Officer')", p, transaction))
                        { cmd.ExecuteNonQuery(); account = Convert.ToInt32(cmd.LastInsertedId); }
                    }
                    else
                    {
                        using (MySqlCommand cmd = Database.Command(connection, "SELECT account_id FROM officers WHERE officer_id=@id FOR UPDATE", p, transaction))
                        {
                            object found = cmd.ExecuteScalar(); if (found == null) throw new InvalidOperationException("Officer no longer exists.");
                            account = Convert.ToInt32(found);
                        }
                        p.Add("@account", account);
                        using (MySqlCommand cmd = Database.Command(connection, "UPDATE accounts SET username=@username WHERE account_id=@account AND role='Officer'", p, transaction)) cmd.ExecuteNonQuery();
                        if (!string.IsNullOrEmpty(password))
                        {
                            p.Add("@password", BCrypt.Net.BCrypt.HashPassword(password));
                            using (MySqlCommand cmd = Database.Command(connection, "UPDATE accounts SET password=@password WHERE account_id=@account AND role='Officer'", p, transaction)) cmd.ExecuteNonQuery();
                        }
                    }
                    p["@account"] = account;
                    string sql = id == 0
                        ? @"INSERT INTO officers(account_id,officer_number,first_name,middle_name,last_name,department_id,year_level,gender,email,contact_number,position,school_year)
                            VALUES(@account,@officer_number,@first_name,@middle_name,@last_name,@department_id,@year_level,@gender,@email,@contact_number,@position,@school_year)"
                        : @"UPDATE officers SET officer_number=@officer_number,first_name=@first_name,middle_name=@middle_name,last_name=@last_name,department_id=@department_id,
                            year_level=@year_level,gender=@gender,email=@email,contact_number=@contact_number,position=@position,school_year=@school_year WHERE officer_id=@id";
                    using (MySqlCommand cmd = Database.Command(connection, sql, p, transaction)) cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }
    }

    internal class OfficerEditor : Form
    {
        private readonly Dictionary<string, TextBox> inputs = new Dictionary<string, TextBox>();
        private readonly ComboBox department = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox year = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox gender = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly int officerId;
        public OfficerEditor(DataRow row)
        {
            officerId = row == null ? 0 : Convert.ToInt32(row["officer_id"]);
            Text = officerId == 0 ? "Add Officer and Login" : "Edit Officer / Reset Password";
            ClientSize = new Size(700, 500); StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false;
            string[] fields = { "officer_number", "first_name", "middle_name", "last_name", "email", "contact_number", "position", "school_year", "username", "password" };
            for (int i = 0; i < fields.Length; i++)
            {
                int x = 20 + (i % 2) * 340, y = 20 + (i / 2) * 65;
                string name = fields[i];
                string caption = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.Replace('_', ' '));
                if (name == "middle_name" || name == "email" || name == "contact_number") caption += " (optional)";
                if (name == "password" && row != null) caption += " (blank = keep current)";
                Program.AddLabel(this, caption, x, y, 300);
                TextBox input = new TextBox { Location = new Point(x, y + 24), Width = 310, MaxLength = 100,
                    Text = row != null && name != "password" ? row[name].ToString() : "" };
                inputs.Add(name, input); Controls.Add(input);
            }
            inputs["password"].UseSystemPasswordChar = true; inputs["password"].MaxLength = 72;
            department.SetBounds(20, 365, 260, 25); Controls.Add(department);
            year.SetBounds(290, 365, 170, 25); Controls.Add(year);
            gender.SetBounds(470, 365, 170, 25); Controls.Add(gender);
            Program.AddLabel(this, "Department", 20, 340, 260);
            Program.AddLabel(this, "Year", 290, 340, 170);
            Program.AddLabel(this, "Gender", 470, 340, 170);
            Program.Choices(department, Database.GetData("SELECT department_id,department_code FROM departments"), "department_id", "department_code");
            year.Items.AddRange(new object[] { "1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year" }); year.SelectedIndex = 0;
            gender.Items.AddRange(new object[] { "Male", "Female", "Other" }); gender.SelectedIndex = 0;
            if (row != null) { department.SelectedValue = row["department_id"]; year.Text = row["year_level"].ToString(); gender.Text = row["gender"].ToString(); }
            Program.AddLabel(this, "Password: 6-72 characters. Leave blank on edit to keep the existing password.", 20, 405, 650, 35);
            Program.AddButton(this, "Save", 440, 455, 100, Save);
            Program.AddButton(this, "Cancel", 550, 455, 100, delegate { Close(); });
        }
        private void Save(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                foreach (KeyValuePair<string, TextBox> input in inputs)
                    if (input.Key != "password") p.Add("@" + input.Key, Database.Optional(input.Value.Text));
                p.Add("@department_id", Program.SelectedId(department)); p.Add("@year_level", year.Text); p.Add("@gender", gender.Text);
                Officerr_Records.SaveOfficer(officerId, p, inputs["password"].Text);
                MessageBox.Show("Officer and login saved."); DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { Program.Error(ex); }
        }
    }
}
