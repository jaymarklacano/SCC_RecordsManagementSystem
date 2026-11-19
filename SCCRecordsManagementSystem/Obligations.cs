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
    public partial class Obligations : Form
    {
        private readonly ComboBox events = new ComboBox();
        private Label summary;
        public Obligations()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu(); Program.Grid(dataGridView1);
            events.SetBounds(100, 65, 350, 26); panel4.Controls.Add(events);
            Program.AddLabel(panel4, "Event:", 16, 68, 80);
            Program.AddButton(panel4, "Deduct / Clear", 16, 110, 150, AdjustClick);
            Program.AddButton(panel4, "History", 185, 110, 100, ShowHistory);
            Program.AddButton(panel4, "Export CSV", 300, 110, 130, delegate {
                try { Database.Require("Obligations"); Program.ExportGrid(this, dataGridView1, "scc-obligations.csv"); }
                catch (Exception ex) { Program.Error(ex); }
            });
            dataGridView1.CellDoubleClick += delegate { ShowHistory(this, EventArgs.Empty); };
            summary = Program.AddLabel(panel6, "", 20, 34, 900, 30);
            foreach (Panel card in new[] { panel7, panel8, panel9, panel10 })
            {
                card.Top = 76; card.Height = 70;
                card.BackColor = Color.LightSteelBlue;
            }
            foreach (Label total in new[] { label7, label8, label9, label10 })
            {
                total.AutoSize = false; total.SetBounds(5, 10, 210, 45);
                total.TextAlign = ContentAlignment.MiddleCenter;
            }
            comboBox2.Items.Clear(); comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year" }); comboBox2.SelectedIndex = 0;
            button2.Click += delegate { LoadBalances(); }; textBox1.TextChanged += delegate { LoadBalances(); };
            button1.Click += delegate { events.SelectedIndex = 0; comboBox1.SelectedIndex = comboBox2.SelectedIndex = 0; textBox1.Clear(); LoadBalances(); };
            events.SelectionChangeCommitted += delegate { LoadBalances(); }; comboBox1.SelectionChangeCommitted += delegate { LoadBalances(); };
            comboBox2.SelectedIndexChanged += delegate { LoadBalances(); };
            Shown += delegate {
                try
                {
                    Program.Choices(events, Database.GetData("SELECT event_id,event_name FROM attendance_events ORDER BY start_date DESC"), "event_id", "event_name", "All Events");
                    Program.Choices(comboBox1, Database.GetData("SELECT department_id,department_code FROM departments"), "department_id", "department_code", "All Departments");
                    LoadBalances();
                }
                catch (Exception ex) { Program.Error(ex); }
            };
        }

        private void WireMenu()
        {
            button12.Click += delegate { Program.Logout(this); };
            button13.Click += delegate { Program.OpenPage(this, "Dashboard", () => new Dashboard()); };
            button14.Enabled = button20.Enabled = Database.IsAdmin;
            button14.Click += delegate { Program.OpenPage(this, "Rules and Permission", () => new RulesPermission()); };
            button15.Click += delegate { LoadBalances(); };
            button16.Click += delegate { Program.OpenPage(this, "Reports and Analysis", () => new ReportsAndAnalysis()); };
            button17.Click += delegate { Program.OpenPage(this, "Attendance Monitoring", () => new AttendanceMonitoring()); };
            button18.Click += delegate { Program.OpenPage(this, "Event Management", () => new Event_Management()); };
            button19.Click += delegate { Program.OpenPage(this, "Face Recognition", () => new FaceRecognition()); };
            button20.Click += delegate { Program.OpenPage(this, "Officer Records", () => new Officerr_Records()); };
            button21.Click += delegate { Program.OpenPage(this, "Student Records", () => new StudentRecords()); };
            button22.Click += delegate { Program.OpenPage(this, "Departments", () => new Departments()); };
        }

        public static DataTable GetBalances(int eventId, int department, string year, string search)
        {
            return Database.GetData(@"SELECT student_id,student_number,full_name,department_code,year_level,
                SUM(absent_count) AS absences,SUM(absence_points) AS points,SUM(deducted_points) AS deducted,SUM(balance) AS balance
                FROM obligation_balances WHERE (@event=0 OR event_id=@event) AND (@department=0 OR department_id=@department)
                AND (@year='' OR year_level=@year) AND (student_number LIKE @q OR full_name LIKE @q)
                GROUP BY student_id,student_number,full_name,department_code,year_level ORDER BY balance DESC,full_name",
                new Dictionary<string, object> { { "@event", eventId }, { "@department", department }, { "@year", year }, { "@q", "%" + search.Trim() + "%" } });
        }

        private void LoadBalances()
        {
            try
            {
                Database.Require("Obligations");
                DataTable rows = GetBalances(Program.SelectedId(events), Program.SelectedId(comboBox1),
                    comboBox2.SelectedIndex == 0 ? "" : comboBox2.Text, textBox1.Text);
                dataGridView1.DataSource = rows; dataGridView1.Columns["student_id"].Visible = false;
                int students = rows.AsEnumerable().Select(r => Convert.ToInt32(r["student_id"])).Distinct().Count();
                int absent = rows.AsEnumerable().Where(r => Convert.ToInt32(r["absences"]) > 0).Select(r => Convert.ToInt32(r["student_id"])).Distinct().Count();
                label7.Text = students + " Students"; label8.Text = absent + " with absence";
                label9.Text = (students - absent) + " without absence";
                label10.Text = rows.AsEnumerable().Sum(r => Convert.ToInt32(r["balance"])) + " Points due";
                summary.Text = rows.Rows.Count == 0 ? "No students in the selected event roster." : "10 points per missed session. Select ONE event before adjusting points. Absence counts are not erased.";
            }
            catch (Exception ex)
            {
                dataGridView1.DataSource = null; summary.Text = Database.ErrorMessage(ex);
                label7.Text = label8.Text = label9.Text = label10.Text = "-";
            }
        }

        public static void AdjustPoints(int eventId, int studentId, int points, bool clear, string reason)
        {
            Database.Require("Obligations");
            if (eventId <= 0) throw new ArgumentException("Select one event first.");
            Database.CheckText(reason, "Reason", 255);
            Dictionary<string, object> p = new Dictionary<string, object> { { "@event", eventId }, { "@student", studentId },
                { "@reason", reason.Trim() }, { "@account", Database.AccountId } };
            using (MySqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    using (MySqlCommand cmd = Database.Command(connection,
                        "SELECT student_id FROM event_students WHERE event_id=@event AND student_id=@student FOR UPDATE", p, transaction))
                        if (cmd.ExecuteScalar() == null) throw new InvalidOperationException("Student is not on this event roster.");
                    int balance;
                    using (MySqlCommand cmd = Database.Command(connection,
                        "SELECT balance FROM obligation_balances WHERE event_id=@event AND student_id=@student", p, transaction))
                        balance = Convert.ToInt32(cmd.ExecuteScalar());
                    if (clear) points = balance;
                    if (points <= 0 || points > balance) throw new ArgumentException("Deduct between 1 and the current balance of " + balance + " points.");
                    p.Add("@points", points);
                    using (MySqlCommand cmd = Database.Command(connection,
                        "INSERT INTO obligation_adjustments(event_id,student_id,points_deducted,reason,account_id) VALUES(@event,@student,@points,@reason,@account)", p, transaction)) cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        private void AdjustClick(object sender, EventArgs e)
        {
            try
            {
                DataRow row = Program.SelectedRow(dataGridView1);
                int eventId = Program.SelectedId(events);
                if (eventId == 0) throw new InvalidOperationException("Select one event before adjusting points.");
                int balance = Convert.ToInt32(row["balance"]);
                if (balance == 0) throw new InvalidOperationException("This student has no outstanding points for the selected event.");
                using (Form dialog = new Form { Text = "Adjust " + row["full_name"] + " - balance: " + balance, ClientSize = new Size(480, 220),
                    StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog })
                using (NumericUpDown points = new NumericUpDown { Location = new Point(180, 20), Maximum = balance, Minimum = 1, Value = 1 })
                using (CheckBox clear = new CheckBox { Text = "Clear balance to zero", Location = new Point(20, 60), AutoSize = true })
                using (TextBox reason = new TextBox { Location = new Point(20, 120), Width = 430, MaxLength = 255 })
                {
                    Program.AddLabel(dialog, "Points to deduct:", 20, 20, 150); Program.AddLabel(dialog, "Reason (required):", 20, 95, 250);
                    dialog.Controls.Add(points); dialog.Controls.Add(clear); dialog.Controls.Add(reason);
                    clear.CheckedChanged += delegate { points.Enabled = !clear.Checked; };
                    Program.AddButton(dialog, "Cancel", 230, 170, 100, delegate { dialog.Close(); });
                    Program.AddButton(dialog, "Save", 350, 170, 100, delegate {
                        try {
                            Database.CheckText(reason.Text, "Reason", 255);
                            if (MessageBox.Show(clear.Checked ? "Clear this event's current balance? Absences remain recorded." : "Deduct " + points.Value + " points?",
                                "Confirm adjustment", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                            AdjustPoints(eventId, Convert.ToInt32(row["student_id"]), (int)points.Value, clear.Checked, reason.Text);
                            dialog.DialogResult = DialogResult.OK; dialog.Close();
                        }
                        catch (Exception ex) { Program.Error(ex); }
                    });
                    if (dialog.ShowDialog(this) == DialogResult.OK) LoadBalances();
                }
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void ShowHistory(object sender, EventArgs e)
        {
            try
            {
                Database.Require("Obligations");
                DataRow row = Program.SelectedRow(dataGridView1);
                using (Form form = new Form { Text = "Adjustment History - " + row["full_name"], Size = new Size(850, 450), StartPosition = FormStartPosition.CenterParent })
                using (DataGridView grid = new DataGridView { Dock = DockStyle.Fill })
                {
                    Program.Grid(grid); form.Controls.Add(grid);
                    grid.DataSource = Database.GetData(@"SELECT e.event_name,o.points_deducted,o.reason,o.created_at,COALESCE(a.username,'Deleted account') AS adjusted_by
                        FROM obligation_adjustments o JOIN attendance_events e ON e.event_id=o.event_id LEFT JOIN accounts a ON a.account_id=o.account_id
                        WHERE o.student_id=@student AND (@event=0 OR o.event_id=@event) ORDER BY o.adjustment_id DESC",
                        new Dictionary<string, object> { { "@student", row["student_id"] }, { "@event", Program.SelectedId(events) } });
                    if (grid.Rows.Count == 0) form.Text += " (no adjustments)";
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex) { Program.Error(ex); }
        }
    }
}
