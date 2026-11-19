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
    public partial class AttendanceMonitoring : Form
    {
        private readonly ComboBox sessions = new ComboBox();
        private readonly DateTimePicker date = new DateTimePicker { Format = DateTimePickerFormat.Short, ShowCheckBox = true, Checked = false };
        private readonly TextBox search = new TextBox();
        private readonly CheckBox pending = new CheckBox { Text = "Pending", Checked = true, AutoSize = true };
        private readonly ComboBox logout = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private Label summary;
        public AttendanceMonitoring()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu(); Program.Grid(dataGridView1);
            dataGridView1.SetBounds(449, 200, 844, 407);
            sessions.SetBounds(350, 64, 350, 26); Controls.Add(sessions);
            date.SetBounds(710, 64, 145, 26); Controls.Add(date);
            search.SetBounds(870, 64, 290, 26); Controls.Add(search);
            Program.AddButton(this, "Refresh", 1170, 62, 115, delegate { LoadRecords(); });
            Program.AddLabel(this, "Session", 350, 47, 300, 17);
            Program.AddLabel(this, "Date (check to filter)", 710, 47, 150, 17);
            Program.AddLabel(this, "Student name / ID", 870, 47, 280, 17);
            pending.Location = new Point(345, 260); Controls.Add(pending);
            logout.SetBounds(550, 138, 220, 25); Controls.Add(logout);
            logout.Items.AddRange(new object[] { "All logout states", "Complete", "Missing", "Optional", "Pending" }); logout.SelectedIndex = 0;
            Program.AddLabel(this, "Log out:", 450, 140, 90);
            summary = Program.AddLabel(this, "", 450, 172, 830, 26);
            Program.AddButton(this, "Today", 345, 305, 95, delegate { date.Value = DateTime.Today; date.Checked = true; LoadRecords(); });
            Program.AddButton(this, "Clear filters", 345, 345, 95, delegate {
                comboBox3.SelectedIndex = comboBox1.SelectedIndex = comboBox2.SelectedIndex = logout.SelectedIndex = 0;
                date.Checked = false; search.Clear(); checkBox1.Checked = checkBox2.Checked = pending.Checked = true;
                try { LoadSessions(); LoadRecords(); } catch (Exception ex) { Program.Error(ex); }
            });
            Program.AddButton(this, "Export CSV", 345, 385, 95, delegate {
                try { Database.Require("Attendance Monitoring"); Program.ExportGrid(this, dataGridView1, "scc-attendance.csv"); }
                catch (Exception ex) { Program.Error(ex); }
            });
            comboBox2.Items.Clear(); comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year" }); comboBox2.SelectedIndex = 0;
            checkBox1.Checked = checkBox2.Checked = true;
            Shown += delegate {
                try
                {
                    Program.Choices(comboBox3, Database.GetData("SELECT event_id,event_name FROM attendance_events ORDER BY start_date DESC"), "event_id", "event_name", "All Events");
                    Program.Choices(comboBox1, Database.GetData("SELECT department_id,department_code FROM departments ORDER BY department_code"), "department_id", "department_code", "All Departments");
                    LoadSessions(); LoadRecords();
                }
                catch (Exception ex) { Program.Error(ex); }
            };
            comboBox3.SelectionChangeCommitted += delegate { try { LoadSessions(); LoadRecords(); } catch (Exception ex) { Program.Error(ex); } };
            comboBox1.SelectionChangeCommitted += delegate { LoadRecords(); };
            sessions.SelectionChangeCommitted += delegate { LoadRecords(); };
            comboBox2.SelectedIndexChanged += delegate { LoadRecords(); };
            checkBox1.CheckedChanged += delegate { LoadRecords(); }; checkBox2.CheckedChanged += delegate { LoadRecords(); };
            pending.CheckedChanged += delegate { LoadRecords(); }; logout.SelectedIndexChanged += delegate { LoadRecords(); };
            date.ValueChanged += delegate { LoadRecords(); }; search.TextChanged += delegate { LoadRecords(); };
        }

        private void WireMenu()
        {
            button12.Click += delegate { Program.Logout(this); };
            button13.Click += delegate { Program.OpenPage(this, "Dashboard", () => new Dashboard()); };
            button14.Enabled = button20.Enabled = Database.IsAdmin;
            button14.Click += delegate { Program.OpenPage(this, "Rules and Permission", () => new RulesPermission()); };
            button15.Click += delegate { Program.OpenPage(this, "Obligations", () => new Obligations()); };
            button16.Click += delegate { Program.OpenPage(this, "Reports and Analysis", () => new ReportsAndAnalysis()); };
            button17.Click += delegate { LoadRecords(); };
            button18.Click += delegate { Program.OpenPage(this, "Event Management", () => new Event_Management()); };
            button19.Click += delegate { Program.OpenPage(this, "Face Recognition", () => new FaceRecognition()); };
            button20.Click += delegate { Program.OpenPage(this, "Officer Records", () => new Officerr_Records()); };
            button21.Click += delegate { Program.OpenPage(this, "Student Records", () => new StudentRecords()); };
            button22.Click += delegate { Program.OpenPage(this, "Departments", () => new Departments()); };
        }

        private void LoadSessions()
        {
            Program.Choices(sessions, Database.GetData(@"SELECT session_id,CONCAT(e.event_name,' - ',DATE_FORMAT(starts_at,'%b %e %Y'),' ',session_name) AS name
                FROM event_sessions es JOIN attendance_events e ON e.event_id=es.event_id
                WHERE @event=0 OR es.event_id=@event ORDER BY starts_at DESC",
                new Dictionary<string, object> { { "@event", Program.SelectedId(comboBox3) } }), "session_id", "name", "All Sessions");
        }

        public static DataTable GetRecords(int eventId, int sessionId, int department, string year, string searchText,
            DateTime? day, string status)
        {
            Database.Require("Attendance Monitoring");
            return Database.GetData(@"SELECT * FROM attendance_records WHERE (@event=0 OR event_id=@event)
                AND (@session=0 OR session_id=@session) AND (@department=0 OR department_id=@department)
                AND (@year='' OR year_level=@year) AND (student_number LIKE @q OR full_name LIKE @q)
                AND (@day IS NULL OR DATE(starts_at)=@day) AND (@status='All' OR status=@status)
                ORDER BY starts_at DESC,full_name", new Dictionary<string, object> {
                { "@event", eventId }, { "@session", sessionId }, { "@department", department }, { "@year", year },
                { "@q", "%" + searchText.Trim() + "%" }, { "@day", day.HasValue ? (object)day.Value.Date : DBNull.Value }, { "@status", status } });
        }

        private void LoadRecords()
        {
            try
            {
                DataTable rows = GetRecords(Program.SelectedId(comboBox3), Program.SelectedId(sessions),
                    Program.SelectedId(comboBox1), comboBox2.SelectedIndex == 0 ? "" : comboBox2.Text, search.Text,
                    date.Checked ? (DateTime?)date.Value.Date : null, "All");
                rows.DefaultView.RowFilter = StatusFilter(checkBox1.Checked, checkBox2.Checked, pending.Checked)
                    + (logout.SelectedIndex > 0 ? " AND logout_status='" + logout.Text + "'" : "");
                rows = rows.DefaultView.ToTable();
                dataGridView1.DataSource = rows;
                foreach (string column in new[] { "event_id", "session_id", "student_id", "department_id" }) dataGridView1.Columns[column].Visible = false;
                string[] order = { "student_number", "full_name", "status", "event_name", "session_name", "starts_at", "time_in",
                    "time_out", "logout_status", "department_code", "year_level", "ends_at" };
                for (int i = 0; i < order.Length; i++) dataGridView1.Columns[order[i]].DisplayIndex = i;
                dataGridView1.Columns["time_in"].DefaultCellStyle.Format = dataGridView1.Columns["time_out"].DefaultCellStyle.Format = "h:mm tt";
                summary.Text = rows.Rows.Count == 0 ? "No records match these filters." :
                    rows.Rows.Count + " rows | Present: " + rows.Select("status='Present'").Length + " | Absent: " + rows.Select("status='Absent'").Length
                    + " | Pending: " + rows.Select("status='Pending'").Length;
            }

            catch (Exception ex) { summary.Text = Database.ErrorMessage(ex); dataGridView1.DataSource = null; }
        }

        internal static string StatusFilter(bool present, bool absent, bool pending)
        {
            List<string> states = new List<string>();
            if (present) states.Add("'Present'");
            if (absent) states.Add("'Absent'");
            if (pending) states.Add("'Pending'");
            return states.Count == 0 ? "1=0" : "status IN (" + string.Join(",", states) + ")";
        }
    }
}
