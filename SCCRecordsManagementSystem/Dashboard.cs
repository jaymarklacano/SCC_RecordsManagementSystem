using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SCCRecordsManagementSystem.AccountInfo;

namespace SCCRecordsManagementSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome, " + Instantiate.currentUser.Username + " (" + Instantiate.currentUser.Role + ")!";

            // Only the Admin can see the log in history.
            if (Instantiate.currentUser.Role == "Admin")
            {
                btnLoginHistory.Visible = true;
            }
            else
            {
                btnLoginHistory.Visible = false;
            }

            showDateAndTime();
            timerClock.Start();

            loadDashboard();
        }

        // Runs every second to update the clock.
        private void timerClock_Tick(object sender, EventArgs e)
        {
            showDateAndTime();
        }

        private void showDateAndTime()
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy   hh:mm:ss tt");
        }

        private void loadDashboard()
        {
            try
            {
                if (loadTotals() == false) return;

                loadTodayAttendanceChart();
                loadRecentActivities();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                lblTotalAll.Text = lblTotalOfficer.Text = lblAllEvent.Text = "-";
                lblStudentDetails.Text = lblOfficerDetails.Text = lblEventDetails.Text = "";
                chtAttendanceOverview.Series["Series1"].Points.Clear();
                chtAttendanceOverview.Titles.Clear();
                dtgRecentEvent.DataSource = null;
                lblNoActivity.Visible = false;
                lblTodaySummary.Text = "Dashboard unavailable. Click Refresh to try again.";
                Database.showError("Could not refresh the dashboard.", ex);
            }
        }

        private bool loadTotals()
        {
            // One query that returns all the numbers that are shown in the 3 boxes.
            string query = "SELECT " +
                           "(SELECT COUNT(*) FROM students) AS total_students, " +
                           "(SELECT COUNT(*) FROM students WHERE status = 'Active') AS active_students, " +
                           "(SELECT COUNT(*) FROM students WHERE face_registered = 1) AS face_registered, " +
                           "(SELECT COUNT(*) FROM officers) AS total_officers, " +
                           "(SELECT COUNT(*) FROM accounts WHERE role = 'Admin') AS total_admins, " +
                           "(SELECT COUNT(*) FROM events) AS total_events, " +
                           "(SELECT COUNT(*) FROM events WHERE CURDATE() BETWEEN start_date AND end_date) AS ongoing_events, " +
                           "(SELECT COUNT(*) FROM events WHERE start_date > CURDATE()) AS upcoming_events";

            DataTable table = Database.GetData(query, null);

            if (table.Rows.Count == 0)
            {
                lblTotalAll.Text = "-";
                lblTotalOfficer.Text = "-";
                lblAllEvent.Text = "-";
                return false;
            }

            DataRow row = table.Rows[0];

            lblTotalAll.Text = row["total_students"].ToString();
            lblStudentDetails.Text = "Active: " + row["active_students"].ToString() + "  |  Face Registered: " + row["face_registered"].ToString();

            lblTotalOfficer.Text = row["total_officers"].ToString();
            lblOfficerDetails.Text = "Admin Accounts: " + row["total_admins"].ToString();

            lblAllEvent.Text = row["total_events"].ToString();
            lblEventDetails.Text = "Ongoing: " + row["ongoing_events"].ToString() + "  |  Upcoming: " + row["upcoming_events"].ToString();

            return true;
        }

        private void loadTodayAttendanceChart()
        {
            // "attendance_records" is a VIEW in the database.
            // It already computes the status (Present, Absent or Pending) of every student in every session.
            string query = "SELECT " +
                           "SUM(CASE WHEN status = 'Present' THEN 1 ELSE 0 END) AS present_count, " +
                           "SUM(CASE WHEN status = 'Absent' THEN 1 ELSE 0 END) AS absent_count, " +
                           "SUM(CASE WHEN status = 'Pending' THEN 1 ELSE 0 END) AS pending_count " +
                           "FROM attendance_records WHERE session_date = CURDATE()";

            DataTable table = Database.GetData(query, null);

            int present = 0;
            int absent = 0;
            int pending = 0;

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];

                // SUM() returns NULL when there are no rows, so we check for DBNull first.
                if (row["present_count"] != DBNull.Value)
                {
                    present = Convert.ToInt32(row["present_count"]);
                }

                if (row["absent_count"] != DBNull.Value)
                {
                    absent = Convert.ToInt32(row["absent_count"]);
                }

                if (row["pending_count"] != DBNull.Value)
                {
                    pending = Convert.ToInt32(row["pending_count"]);
                }
            }

            chtAttendanceOverview.Series["Series1"].Points.Clear();
            chtAttendanceOverview.Titles.Clear();

            if (present + absent + pending == 0)
            {
                chtAttendanceOverview.Titles.Add("No event sessions are scheduled today.");
                lblTodaySummary.Text = "Present: 0   Absent: 0   Pending: 0";
                return;
            }

            // A pie slice is only added when its value is more than zero.
            if (present > 0)
            {
                int index = chtAttendanceOverview.Series["Series1"].Points.AddXY("Present", present);
                chtAttendanceOverview.Series["Series1"].Points[index].Color = Color.SeaGreen;
            }

            if (absent > 0)
            {
                int index = chtAttendanceOverview.Series["Series1"].Points.AddXY("Absent", absent);
                chtAttendanceOverview.Series["Series1"].Points[index].Color = Color.IndianRed;
            }

            if (pending > 0)
            {
                int index = chtAttendanceOverview.Series["Series1"].Points.AddXY("Pending", pending);
                chtAttendanceOverview.Series["Series1"].Points[index].Color = Color.SteelBlue;
            }

            lblTodaySummary.Text = "Present: " + present + "   Absent: " + absent + "   Pending: " + pending;
        }

        private void loadRecentActivities()
        {
            // The last 15 face scans (log in or log out).
            string query = "SELECT " +
                           "DATE_FORMAT(IFNULL(a.time_out, a.time_in), '%b %d, %h:%i %p') AS `Time`, " +
                           "CONCAT(s.last_name, ', ', s.first_name) AS `Student`, " +
                           "e.event_name AS `Event`, " +
                           "es.session_name AS `Session`, " +
                           "CASE WHEN a.time_out IS NULL THEN 'Logged In' ELSE 'Logged Out' END AS `Activity` " +
                           "FROM event_attendance a " +
                           "JOIN students s ON s.student_id = a.student_id " +
                           "JOIN event_sessions es ON es.session_id = a.session_id " +
                           "JOIN events e ON e.event_id = es.event_id " +
                           "ORDER BY IFNULL(a.time_out, a.time_in) DESC " +
                           "LIMIT 15";

            DataTable table = Database.GetData(query, null);

            dtgRecentEvent.DataSource = table;

            if (table.Rows.Count == 0)
            {
                lblNoActivity.Visible = true;
                lblNoActivity.BringToFront();
            }
            else
            {
                lblNoActivity.Visible = false;
            }
        }

        private void btnRefreshDashboard_Click(object sender, EventArgs e)
        {
            loadDashboard();
        }

        private void btnLoginHistory_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Login History") == false)
            {
                return;
            }

            LoginHistory loginHistory = new LoginHistory();
            loginHistory.ShowDialog();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                timerClock.Stop();
                Database.saveLogoutTime();

                lnkCreateAccount login = new lnkCreateAccount();
                login.Show();
                this.Close();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // We are already in the Dashboard, so we only refresh the numbers.
            loadDashboard();
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Departments") == false)
            {
                return;
            }

            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnStudentRecords_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        private void btnOfficer_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Officer Records") == false)
            {
                return;
            }

            OfficerRecords officerrRecords = new OfficerRecords();
            officerrRecords.Show();
            this.Close();
        }

        private void btnFaceRecognition_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Face Recognition") == false)
            {
                return;
            }

            FaceRecognition records = new FaceRecognition();
            records.Show();
            this.Close();
        }

        private void btnEventManagement_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Event Management") == false)
            {
                return;
            }

            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnAttendanceMonitoring_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Attendance Monitoring") == false)
            {
                return;
            }

            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnReportAnalysis_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Reports and Analysis") == false)
            {
                return;
            }

            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        private void btnObligation_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Obligations") == false)
            {
                return;
            }

            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        private void btnRulesPermission_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Rules and Permission") == false)
            {
                return;
            }

            RulesPermission rulesPermission = new RulesPermission();
            rulesPermission.Show();
            this.Close();
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerClock.Stop();
            Database.exitIfNoFormIsOpen(this);
        }
    }
}
