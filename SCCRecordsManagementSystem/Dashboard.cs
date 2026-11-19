using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SCCRecordsManagementSystem
{
    public partial class Dashboard : Form
    {
        private Label students, officers, events, summary, clock;
        private readonly DataGridView recent = new DataGridView { Dock = DockStyle.Fill };
        private readonly ComboBox sessions = new ComboBox();
        private readonly System.Windows.Forms.Timer refresh = new System.Windows.Forms.Timer { Interval = 15000 };
        private readonly System.Windows.Forms.Timer ticker = new System.Windows.Forms.Timer { Interval = 1000 };
        public Dashboard()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu();
            label5.Text = "Welcome, " + Database.Username + "!";
            students = Program.AddLabel(panel2, "0", 125, 78, 140, 40);
            officers = Program.AddLabel(panel3, "0", 145, 78, 140, 40);
            events = Program.AddLabel(panel4, "0", 145, 78, 130, 40);
            students.Font = officers.Font = events.Font = new Font("Cambria", 22, FontStyle.Bold);
            Program.Grid(recent); groupBox1.Controls.Add(recent);
            sessions.SetBounds(10, 4, 420, 26); panel6.Controls.Add(sessions); sessions.BringToFront();
            sessions.SelectionChangeCommitted += delegate { LoadDashboard(); };
            summary = Program.AddLabel(panel6, "", 10, 35, 420, 45);
            chart1.Top = 83; chart1.Height = 210;
            clock = Program.AddLabel(this, "", 870, 65, 390, 25);
            Program.AddButton(this, "Login History", 1090, 88, 160, delegate {
                try { Database.RequireAdmin(); using (LoginHistory form = new LoginHistory()) form.ShowDialog(this); }
                catch (Exception ex) { Program.Error(ex); }
            }).Enabled = Database.IsAdmin;
            ticker.Tick += delegate { clock.Text = DateTime.Now.ToString("dddd, MMM d yyyy  h:mm:ss tt"); };
            refresh.Tick += delegate { LoadDashboard(); };
            Shown += delegate { LoadDashboard(); refresh.Start(); ticker.Start(); };
            FormClosed += delegate { refresh.Dispose(); ticker.Dispose(); };
        }

        private void LoadDashboard()
        {
            try
            {
                students.Text = Database.Scalar("SELECT COUNT(*) FROM students").ToString();
                officers.Text = Database.Scalar("SELECT COUNT(*) FROM officers").ToString();
                events.Text = Database.Scalar("SELECT COUNT(*) FROM attendance_events").ToString();
                int selected = Program.SelectedId(sessions);
                DataTable session = Database.GetData(@"SELECT es.session_id,CONCAT(e.event_name,' - ',DATE_FORMAT(es.starts_at,'%b %e %Y'),' ',es.session_name) AS name FROM event_sessions es
                    JOIN attendance_events e ON e.event_id=es.event_id
                    ORDER BY (NOW()>=es.starts_at AND NOW()<es.ends_at) DESC,(es.starts_at<=NOW()) DESC,es.starts_at DESC");
                Program.Choices(sessions, session, "session_id", "name");
                if (session.AsEnumerable().Any(r => Convert.ToInt32(r["session_id"]) == selected)) sessions.SelectedValue = selected;
                chart1.Series.Clear();
                if (session.Rows.Count == 0) { summary.Text = "No event sessions. Create an event to begin."; recent.DataSource = null; return; }
                DataTable rows = Database.GetData("SELECT status,COUNT(*) AS total FROM attendance_records WHERE session_id=@id GROUP BY status",
                    new Dictionary<string, object> { { "@id", Program.SelectedId(sessions) } });
                Series series = chart1.Series.Add("Attendance"); series.ChartType = SeriesChartType.Pie; series.IsValueShownAsLabel = true;
                foreach (DataRow row in rows.Rows) series.Points.AddXY(row["status"], row["total"]);
                summary.Text = rows.Rows.Count == 0 ? "This session has no eligible roster students." :
                    string.Join(" | ", rows.AsEnumerable().Select(r => r["status"] + ": " + r["total"])) + "\nAbsences finalize after the session ends.";
                recent.DataSource = Database.GetData(@"SELECT event_name,session_name,full_name,time_in,time_out FROM attendance_records
                    WHERE time_in IS NOT NULL ORDER BY time_in DESC LIMIT 20");
                label7.Text = "Recent Attendance";
            }
            catch (Exception ex)
            {
                summary.Text = Database.ErrorMessage(ex); recent.DataSource = null; chart1.Series.Clear();
                students.Text = officers.Text = events.Text = "-";
            }
        }

        private void WireMenu()
        {
            button12.Click += delegate { Program.Logout(this); };
            button13.Click += delegate { LoadDashboard(); };
            button14.Enabled = button20.Enabled = Database.IsAdmin;
            button14.Click += delegate { Program.OpenPage(this, "Rules and Permission", () => new RulesPermission()); };
            button15.Click += delegate { Program.OpenPage(this, "Obligations", () => new Obligations()); };
            button16.Click += delegate { Program.OpenPage(this, "Reports and Analysis", () => new ReportsAndAnalysis()); };
            button17.Click += delegate { Program.OpenPage(this, "Attendance Monitoring", () => new AttendanceMonitoring()); };
            button18.Click += delegate { Program.OpenPage(this, "Event Management", () => new Event_Management()); };
            button19.Click += delegate { Program.OpenPage(this, "Face Recognition", () => new FaceRecognition()); };
            button20.Click += delegate { Program.OpenPage(this, "Officer Records", () => new Officerr_Records()); };
            button21.Click += delegate { Program.OpenPage(this, "Student Records", () => new StudentRecords()); };
            button22.Click += delegate { Program.OpenPage(this, "Departments", () => new Departments()); };
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
