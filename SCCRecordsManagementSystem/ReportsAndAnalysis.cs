using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace SCCRecordsManagementSystem
{
    public partial class ReportsAndAnalysis : Form
    {
        private readonly DateTimePicker toDate = new DateTimePicker { Format = DateTimePickerFormat.Short };
        private readonly ComboBox department = new ComboBox();
        private readonly TextBox search = new TextBox();
        public ReportsAndAnalysis()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu(); Program.Grid(dataGridView1);
            label11.AutoSize = false; label11.Size = new Size(780, 30);
            label12.AutoSize = false; label12.Size = new Size(950, 30);
            dateTimePicker1.Format = DateTimePickerFormat.Short; dateTimePicker1.Width = 110; dateTimePicker1.Value = DateTime.Today.AddDays(-30);
            toDate.SetBounds(440, 51, 120, 25); panel1.Controls.Add(toDate);
            label4.Text = "From / To:";
            comboBox2.Items.Clear(); comboBox2.Items.AddRange(new object[] { "Sessions", "Most absent", "Obligations", "Student attendance", "Missing logout" });
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList; comboBox2.SelectedIndex = 0; comboBox2.Width = 200;
            department.SetBounds(47, 88, 225, 26); panel1.Controls.Add(department);
            search.SetBounds(322, 88, 238, 25); panel1.Controls.Add(search);
            Program.AddLabel(panel1, "Student name / ID", 574, 90, 180);
            Program.AddButton(this, "Export CSV", 1130, 52, 150, Export);
            button1.Click += delegate { Generate(); }; checkBox1.Checked = checkBox2.Checked = true;
            checkBox1.CheckedChanged += delegate { Generate(); }; checkBox2.CheckedChanged += delegate { Generate(); };
            comboBox2.SelectionChangeCommitted += delegate { Generate(); };
            comboBox1.SelectionChangeCommitted += delegate { UseEventDates(); Generate(); };
            department.SelectionChangeCommitted += delegate { Generate(); };
            search.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Generate(); } };
            Shown += delegate {
                try
                {
                    Program.Choices(comboBox1, Database.GetData("SELECT event_id,event_name FROM attendance_events ORDER BY start_date DESC"), "event_id", "event_name", "All Events");
                    Program.Choices(department, Database.GetData("SELECT department_id,department_code FROM departments"), "department_id", "department_code", "All Departments");
                    Generate();
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
            button15.Click += delegate { Program.OpenPage(this, "Obligations", () => new Obligations()); };
            button16.Click += delegate { Generate(); };
            button17.Click += delegate { Program.OpenPage(this, "Attendance Monitoring", () => new AttendanceMonitoring()); };
            button18.Click += delegate { Program.OpenPage(this, "Event Management", () => new Event_Management()); };
            button19.Click += delegate { Program.OpenPage(this, "Face Recognition", () => new FaceRecognition()); };
            button20.Click += delegate { Program.OpenPage(this, "Officer Records", () => new Officerr_Records()); };
            button21.Click += delegate { Program.OpenPage(this, "Student Records", () => new StudentRecords()); };
            button22.Click += delegate { Program.OpenPage(this, "Departments", () => new Departments()); };
        }

        public static DataTable ReportRows(int eventId, int departmentId, DateTime first, DateTime last, string searchText)
        {
            Database.Require("Reports and Analysis");
            if (first.Date > last.Date) throw new ArgumentException("Start date must not follow end date.");
            return Database.GetData(@"SELECT * FROM attendance_records WHERE ends_at<=NOW()
                AND starts_at>=@first AND starts_at<@after AND (@event=0 OR event_id=@event)
                AND (@department=0 OR department_id=@department) AND (student_number LIKE @q OR full_name LIKE @q)",
                new Dictionary<string, object> { { "@first", first.Date }, { "@after", last.Date.AddDays(1) },
                    { "@event", eventId }, { "@department", departmentId }, { "@q", "%" + searchText.Trim() + "%" } });
        }

        // When one event is chosen, show its whole date range so its sessions are not hidden by the date filter.
        private void UseEventDates()
        {
            try
            {
                if (Program.SelectedId(comboBox1) == 0) return;
                DataTable dates = Database.GetData("SELECT start_date,end_date FROM attendance_events WHERE event_id=@id",
                    new Dictionary<string, object> { { "@id", Program.SelectedId(comboBox1) } });
                if (dates.Rows.Count == 0) return;
                dateTimePicker1.Value = Convert.ToDateTime(dates.Rows[0]["start_date"]);
                toDate.Value = Convert.ToDateTime(dates.Rows[0]["end_date"]);
            }
            catch (Exception ex) { label11.Text = Database.ErrorMessage(ex); }
        }

        private void Generate()
        {
            try
            {
                DataTable rows = ReportRows(Program.SelectedId(comboBox1), Program.SelectedId(department), dateTimePicker1.Value, toDate.Value, search.Text);
                int present = rows.Select("status='Present'").Length, absent = rows.Select("status='Absent'").Length;
                label3.Text = "Present:"; label6.Text = present.ToString(); label7.Text = "Absent:"; label9.Text = absent.ToString();
                label8.Text = "Rate:"; label10.Text = rows.Rows.Count == 0 ? "0%" : (100.0 * present / rows.Rows.Count).ToString("0.0") + "%";
                chart1.Series.Clear();
                Series yes = chart1.Series.Add("Present"), no = chart1.Series.Add("Absent");
                yes.ChartType = no.ChartType = SeriesChartType.Column;
                foreach (var group in rows.AsEnumerable().GroupBy(r => r["department_code"].ToString()))
                {
                    yes.Points.AddXY(group.Key, checkBox1.Checked ? group.Count(r => r["status"].ToString() == "Present") : 0);
                    no.Points.AddXY(group.Key, checkBox2.Checked ? group.Count(r => r["status"].ToString() == "Absent") : 0);
                }
                DataTable report = new DataTable();
                if (comboBox2.SelectedIndex == 2)
                {
                    report = Obligations.GetBalances(Program.SelectedId(comboBox1), Program.SelectedId(department), "", search.Text);
                    label12.Text = "Attendance chart: selected dates. Obligation balances below: all dates for selected event.";
                }
                else if (comboBox2.SelectedIndex >= 3)
                {
                    if (comboBox2.SelectedIndex == 4) rows.DefaultView.RowFilter = "logout_status='Missing'";
                    report = rows.DefaultView.ToTable();
                    label12.Text = "Completed-session details, including login / logout times and logout status.";
                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    report.Columns.Add("Student ID"); report.Columns.Add("Student"); report.Columns.Add("Absences", typeof(int)); report.Columns.Add("Points Earned", typeof(int));
                    foreach (var group in rows.AsEnumerable().Where(r => r["status"].ToString() == "Absent").GroupBy(r => Convert.ToInt32(r["student_id"])).OrderByDescending(g => g.Count()))
                        report.Rows.Add(group.First()["student_number"], group.First()["full_name"], group.Count(), group.Count() * 10);
                    label12.Text = "Students with the most absences (10 points earned per absence, before deductions)";
                }
                else
                {
                    report.Columns.Add("Event"); report.Columns.Add("Session"); report.Columns.Add("Start", typeof(DateTime));
                    report.Columns.Add("Department"); report.Columns.Add("Present", typeof(int)); report.Columns.Add("Absent", typeof(int)); report.Columns.Add("Rate %", typeof(double));
                    foreach (var group in rows.AsEnumerable().GroupBy(r => new { Session = Convert.ToInt32(r["session_id"]), Department = r["department_code"].ToString() }))
                    {
                        int count = group.Count(r => r["status"].ToString() == "Present");
                        DataRow firstRow = group.First();
                        report.Rows.Add(firstRow["event_name"], firstRow["session_name"], firstRow["starts_at"], group.Key.Department, count, group.Count() - count, Math.Round(100.0 * count / group.Count(), 1));
                    }
                    label12.Text = "Attendance per department";
                }
                dataGridView1.DataSource = report;
                foreach (string column in new[] { "event_id", "session_id", "student_id", "department_id" })
                    if (dataGridView1.Columns.Contains(column)) dataGridView1.Columns[column].Visible = false;
                label11.Text = report.Rows.Count == 0 ? "No matching records." : "Report generated: " + report.Rows.Count + " rows";
            }
            catch (Exception ex)
            {
                label11.Text = Database.ErrorMessage(ex); dataGridView1.DataSource = null;
                chart1.Series.Clear(); label6.Text = label9.Text = label10.Text = "-";
            }
        }

        private void Export(object sender, EventArgs e)
        {
            try
            {
                Database.Require("Reports and Analysis");
                Program.ExportGrid(this, dataGridView1, "scc-report.csv");
            }
            catch (Exception ex) { Program.Error(ex); }
        }

    }
}
