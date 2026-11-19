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
    public partial class Event_Management : Form
    {
        private readonly ComboBox state = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private Label summary;
        public Event_Management()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu(); Program.Grid(dataGridView1);
            dataGridView1.SetBounds(339, 195, 954, 412);
            state.SetBounds(440, 73, 220, 25); Controls.Add(state);
            state.Items.AddRange(new object[] { "All Events", "Upcoming", "In Progress", "Completed" }); state.SelectedIndex = 0;
            Program.AddLabel(this, "Status:", 340, 75, 90);
            summary = Program.AddLabel(this, "", 340, 165, 940, 28);
            state.SelectedIndexChanged += delegate { LoadEvents(); };
            dataGridView1.CellDoubleClick += delegate { EditEvent(false); };
            button12.Click += delegate { EditEvent(true); };
            button2.Click += delegate { LoadEvents(); };
            textBox1.TextChanged += delegate { LoadEvents(); };
            Program.AddButton(this, "Edit", 800, 121, 100, delegate { EditEvent(false); });
            Program.AddButton(this, "Delete", 910, 121, 100, DeleteEvent);
            Program.AddButton(this, "View roster", 1030, 72, 150, ViewRoster);
            Program.AddButton(this, "Sessions", 800, 72, 150, ViewSessions);
            Shown += delegate { LoadEvents(); };
        }

        private void LoadEvents()
        {
            try
            {
                Database.Require("Event Management");
                dataGridView1.DataSource = Database.GetData(@"SELECT e.event_id,e.event_name,e.venue,e.start_date,e.end_date,
                    e.requires_logout,
                    CASE WHEN NOW()<(SELECT MIN(starts_at) FROM event_sessions WHERE event_id=e.event_id) THEN 'Upcoming'
                         WHEN NOW()>=(SELECT MAX(ends_at) FROM event_sessions WHERE event_id=e.event_id) THEN 'Completed'
                         ELSE 'In Progress' END AS status,
                    (SELECT COUNT(*) FROM event_sessions es WHERE es.event_id=e.event_id) AS sessions,
                    (SELECT COUNT(*) FROM event_students r WHERE r.event_id=e.event_id) AS students
                    FROM attendance_events e WHERE e.event_name LIKE @q OR e.venue LIKE @q ORDER BY e.start_date DESC",
                    new Dictionary<string, object> { { "@q", "%" + textBox1.Text.Trim() + "%" } });
                dataGridView1.Columns["event_id"].Visible = false;
                DataTable rows = (DataTable)dataGridView1.DataSource;
                rows.DefaultView.RowFilter = state.SelectedIndex > 0 ? "status='" + state.Text + "'" : "";
                summary.Text = rows.DefaultView.Count == 0 ? "No events match these filters." :
                    rows.DefaultView.Count + " event(s). Select a row to inspect sessions, roster or edit.";
            }
            catch (Exception ex) { dataGridView1.DataSource = null; summary.Text = Database.ErrorMessage(ex); }
        }

        private void ViewSessions(object sender, EventArgs e)
        {
            try
            {
                Database.Require("Event Management");
                DataRow row = Program.SelectedRow(dataGridView1);
                using (Form form = new Form { Text = "Sessions - " + row["event_name"], Size = new Size(1100, 500), StartPosition = FormStartPosition.CenterParent })
                using (DataGridView grid = new DataGridView { Dock = DockStyle.Fill })
                {
                    Program.Grid(grid); form.Controls.Add(grid);
                    grid.DataSource = Database.GetData(@"SELECT es.session_name,es.starts_at,es.ends_at,
                        CASE WHEN NOW()<es.starts_at THEN 'Upcoming' WHEN NOW()<es.ends_at THEN 'Open' ELSE 'Closed' END AS status,
                        COUNT(a.student_id) AS expected,
                        COALESCE(SUM(a.status='Present'),0) AS present,COALESCE(SUM(a.status='Absent'),0) AS absent,
                        COALESCE(SUM(a.status='Pending'),0) AS pending,COALESCE(SUM(a.logout_status='Missing'),0) AS missing_logout
                        FROM event_sessions es LEFT JOIN attendance_records a ON a.session_id=es.session_id
                        WHERE es.event_id=@id GROUP BY es.session_id,es.session_name,es.starts_at,es.ends_at ORDER BY es.starts_at",
                        new Dictionary<string, object> { { "@id", row["event_id"] } });
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void EditEvent(bool add)
        {
            try
            {
                Database.Require("Event Management");
                using (AddEvent editor = new AddEvent(add ? 0 : Convert.ToInt32(Program.SelectedRow(dataGridView1)["event_id"])))
                    if (editor.ShowDialog(this) == DialogResult.OK) LoadEvents();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                Database.Require("Event Management");
                DataRow row = Program.SelectedRow(dataGridView1);
                if (MessageBox.Show("Delete " + row["event_name"] + ", all sessions, attendance and obligation adjustments?",
                    "Delete Event", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                Database.Execute("DELETE FROM attendance_events WHERE event_id=@id", new Dictionary<string, object> { { "@id", row["event_id"] } });
                LoadEvents();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void ViewRoster(object sender, EventArgs e)
        {
            try
            {
                Database.Require("Event Management");
                DataRow row = Program.SelectedRow(dataGridView1);
                using (Form form = new Form { Text = "Event roster - " + row["event_name"], Size = new Size(850, 500), StartPosition = FormStartPosition.CenterParent })
                using (DataGridView grid = new DataGridView { Dock = DockStyle.Fill })
                {
                    Program.Grid(grid); form.Controls.Add(grid);
                    grid.DataSource = Database.GetData(@"SELECT s.student_number,CONCAT_WS(' ',s.first_name,s.middle_name,s.last_name) AS name,
                        d.department_code,r.year_level,s.status AS current_student_status FROM event_students r JOIN students s ON s.student_id=r.student_id
                        JOIN departments d ON d.department_id=r.department_id WHERE r.event_id=@id ORDER BY name",
                        new Dictionary<string, object> { { "@id", row["event_id"] } });
                    if (grid.Rows.Count == 0) form.Text += " (empty - register active students with 5 face pictures)";
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void WireMenu()
        {
            button1.Click += delegate { Program.Logout(this); };
            button13.Click += delegate { Program.OpenPage(this, "Dashboard", () => new Dashboard()); };
            button14.Enabled = button20.Enabled = Database.IsAdmin;
            button14.Click += delegate { Program.OpenPage(this, "Rules and Permission", () => new RulesPermission()); };
            button15.Click += delegate { Program.OpenPage(this, "Obligations", () => new Obligations()); };
            button16.Click += delegate { Program.OpenPage(this, "Reports and Analysis", () => new ReportsAndAnalysis()); };
            button17.Click += delegate { Program.OpenPage(this, "Attendance Monitoring", () => new AttendanceMonitoring()); };
            button18.Click += delegate { LoadEvents(); };
            button19.Click += delegate { Program.OpenPage(this, "Face Recognition", () => new FaceRecognition()); };
            button20.Click += delegate { Program.OpenPage(this, "Officer Records", () => new Officerr_Records()); };
            button21.Click += delegate { Program.OpenPage(this, "Student Records", () => new StudentRecords()); };
            button22.Click += delegate { Program.OpenPage(this, "Departments", () => new Departments()); };
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
