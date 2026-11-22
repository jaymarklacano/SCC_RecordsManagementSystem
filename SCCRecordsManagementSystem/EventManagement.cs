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
    public partial class EventManagement : Form
    {
        // The event that is selected in the table (0 = none).
        private int selectedEventId = 0;
        private string selectedEventName = "";

        // true while the table is being filled (so SelectionChanged does nothing).
        private bool isLoading = false;

        public EventManagement()
        {
            InitializeComponent();
        }

        private void EventManagement_Load(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = 0;
            LoadEvents();
        }

        // ------------------------------------------------------------------
        // Events table
        // ------------------------------------------------------------------

        public void LoadEvents()
        {
            isLoading = true;
            try
            {
                // Remember the selected event so it is selected again after refreshing.
                int previousEventId = selectedEventId;
                string search = txtSearchEvent.Text.Trim();
                string status = cmbStatus.Text;

                if (status == "")
                {
                    status = "All Events";
                }

                // The inner SELECT computes the status of every event from its first and last session.
                string query = "SELECT * FROM (" +
                               "SELECT e.event_id, e.event_name, e.venue, " +
                               "IF(e.start_date = e.end_date, DATE_FORMAT(e.start_date, '%b %d, %Y'), " +
                               "CONCAT(DATE_FORMAT(e.start_date, '%b %d'), ' - ', DATE_FORMAT(e.end_date, '%b %d, %Y'))) AS event_dates, " +
                               "CONCAT_WS(', ', IF(e.morning_enabled = 1, 'Morning', NULL), IF(e.afternoon_enabled = 1, 'Afternoon', NULL), " +
                               "IF(e.evening_enabled = 1, 'Evening', NULL)) AS periods, " +
                               "CONCAT(IFNULL(d.department_code, 'All Departments'), ' - ', IFNULL(e.year_level, 'All Years')) AS participants, " +
                               "IF(e.requires_logout = 1, 'Yes', 'No') AS log_out, " +
                               "(SELECT COUNT(*) FROM event_students r WHERE r.event_id = e.event_id) AS students, " +
                               "CASE WHEN NOW() < (SELECT MIN(es.starts_at) FROM event_sessions es WHERE es.event_id = e.event_id) THEN 'Upcoming' " +
                               "WHEN NOW() >= (SELECT MAX(es.ends_at) FROM event_sessions es WHERE es.event_id = e.event_id) THEN 'Completed' " +
                               "ELSE 'In Progress' END AS status, " +
                               "e.start_date " +
                               "FROM events e " +
                               "LEFT JOIN departments d ON d.department_id = e.department_id " +
                               "WHERE e.event_name LIKE @search OR e.venue LIKE @search OR e.organizer LIKE @search" +
                               ") x " +
                               "WHERE (@status = 'All Events' OR x.status = @status) " +
                               "ORDER BY x.start_date DESC, x.event_name";

                MySqlConnection conn = null;

                try
                {
                    conn = Database.GetConnection();
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                    cmd.Parameters.AddWithValue("@status", status);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable eventTable = new DataTable();
                    adapter.Fill(eventTable);

                    dtgEventManagement.DataSource = eventTable;

                    adapter.Dispose();
                    cmd.Dispose();

                    // Color the status: green = in progress, blue = upcoming, gray = completed.
                    for (int i = 0; i < dtgEventManagement.Rows.Count; i++)
                    {
                        DataGridViewCell statusCell = dtgEventManagement.Rows[i].Cells["colEventStatus"];

                        if (statusCell.Value.ToString() == "In Progress")
                        {
                            statusCell.Style.ForeColor = Color.DarkGreen;
                            statusCell.Style.Font = new Font("Cambria", 9F, FontStyle.Bold);
                        }
                        else if (statusCell.Value.ToString() == "Upcoming")
                        {
                            statusCell.Style.ForeColor = Color.RoyalBlue;
                        }
                        else
                        {
                            statusCell.Style.ForeColor = Color.DimGray;
                        }
                    }

                    lblTotalEvents.Text = "Total: " + eventTable.Rows.Count + " event(s)";

                    // Empty-state message
                    if (eventTable.Rows.Count == 0)
                    {
                        if (search == "" && status == "All Events")
                        {
                            lblNoEvent.Text = "No events yet. Click \"+ Add Event\" to create one.";
                        }
                        else
                        {
                            lblNoEvent.Text = "No event matches your search or filter.";
                        }

                        lblNoEvent.Visible = true;
                        lblNoEvent.BringToFront();
                    }
                    else
                    {
                        lblNoEvent.Visible = false;
                    }

                    // Select the same event as before
                    dtgEventManagement.ClearSelection();

                    for (int i = 0; i < dtgEventManagement.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtgEventManagement.Rows[i].Cells["colEventId"].Value) == previousEventId)
                        {
                            dtgEventManagement.Rows[i].Selected = true;
                            dtgEventManagement.CurrentCell = dtgEventManagement.Rows[i].Cells["colEventName"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    dtgEventManagement.DataSource = null;
                    showSelectedEvent();
                    lblNoEvent.Visible = false;
                    lblTotalEvents.Text = "Event list unavailable.";
                    Database.showError("Could not load the events.", ex);
                    return;
                }
                finally
                {
                    if (conn != null) conn.Dispose();
                }
            }
            finally
            {
                isLoading = false;
            }
            showSelectedEvent();
        }

        private void txtSearchEvent_TextChanged(object sender, EventArgs e)
        {
            LoadEvents();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEvents();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchEvent.Text = "";
            cmbStatus.SelectedIndex = 0;
            txtSearchEvent.Focus();
        }

        private void dtgEventManagement_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            showSelectedEvent();
        }

        // Reads the selected row, then shows its sessions or students in the lower table.
        private void showSelectedEvent()
        {
            selectedEventId = 0;
            selectedEventName = "";

            if (dtgEventManagement.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dtgEventManagement.SelectedRows[0];
                selectedEventId = Convert.ToInt32(row.Cells["colEventId"].Value);
                selectedEventName = row.Cells["colEventName"].Value.ToString();
            }

            LoadEventDetails();
        }

        // ------------------------------------------------------------------
        // Lower table: sessions or students of the selected event
        // ------------------------------------------------------------------

        private void rbDetails_CheckedChanged(object sender, EventArgs e)
        {
            // This event runs for the radio button that was unchecked AND the one that was checked.
            // We only need to reload once.
            RadioButton radio = (RadioButton)sender;

            if (radio.Checked == true)
            {
                LoadEventDetails();
            }
        }

        private void LoadEventDetails()
        {
            if (selectedEventId == 0)
            {
                dtgEventDetails.DataSource = null;
                lblDetailsTitle.Text = "Select an event to see its sessions and students.";
                lblDetailsSummary.Text = "";
                return;
            }

            if (rbSessions.Checked == true)
            {
                LoadSessions();
            }
            else
            {
                LoadRoster();
            }
        }

        private void LoadSessions()
        {
            lblDetailsTitle.Text = "Sessions of: " + selectedEventName;

            // attendance_records is a VIEW that computes Present / Absent / Pending for every student and session.
            string query = "SELECT DATE_FORMAT(es.session_date, '%a, %b %d, %Y') AS session_day, es.session_name, " +
                           "CONCAT(DATE_FORMAT(es.starts_at, '%h:%i %p'), ' - ', DATE_FORMAT(es.ends_at, '%h:%i %p')) AS session_time, " +
                           "CASE WHEN NOW() < es.starts_at THEN 'Upcoming' WHEN NOW() < es.ends_at THEN 'Open' ELSE 'Closed' END AS session_status, " +
                           "COUNT(a.student_id) AS expected, " +
                           "COALESCE(SUM(a.status = 'Present'), 0) AS present, " +
                           "COALESCE(SUM(a.status = 'Absent'), 0) AS absent, " +
                           "COALESCE(SUM(a.logout_status = 'Logged Out'), 0) AS logged_out, " +
                           "COALESCE(SUM(a.logout_status = 'Missing Log Out'), 0) AS missing_logout " +
                           "FROM event_sessions es " +
                           "LEFT JOIN attendance_records a ON a.session_id = es.session_id " +
                           "WHERE es.event_id = @id " +
                           "GROUP BY es.session_id, es.session_date, es.session_name, es.starts_at, es.ends_at " +
                           "ORDER BY es.starts_at";

            MySqlConnection conn = null;

            try
            {
                conn = Database.GetConnection();
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", selectedEventId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable sessionTable = new DataTable();
                adapter.Fill(sessionTable);

                dtgEventDetails.DataSource = sessionTable;

                adapter.Dispose();
                cmd.Dispose();

                dtgEventDetails.Columns["session_day"].HeaderText = "Day";
                dtgEventDetails.Columns["session_day"].FillWeight = 140;
                dtgEventDetails.Columns["session_name"].HeaderText = "Session";
                dtgEventDetails.Columns["session_time"].HeaderText = "Time";
                dtgEventDetails.Columns["session_time"].FillWeight = 140;
                dtgEventDetails.Columns["session_status"].HeaderText = "Status";
                dtgEventDetails.Columns["expected"].HeaderText = "Expected";
                dtgEventDetails.Columns["present"].HeaderText = "Present";
                dtgEventDetails.Columns["absent"].HeaderText = "Absent";
                dtgEventDetails.Columns["logged_out"].HeaderText = "Logged Out";
                dtgEventDetails.Columns["missing_logout"].HeaderText = "Missing Log Out";

                // Count the totals with a simple loop
                int openSessions = 0;
                int totalPresent = 0;
                int totalAbsent = 0;

                for (int i = 0; i < sessionTable.Rows.Count; i++)
                {
                    if (sessionTable.Rows[i]["session_status"].ToString() == "Open")
                    {
                        openSessions = openSessions + 1;
                        dtgEventDetails.Rows[i].DefaultCellStyle.BackColor = Color.Honeydew;
                    }

                    totalPresent = totalPresent + Convert.ToInt32(sessionTable.Rows[i]["present"]);
                    totalAbsent = totalAbsent + Convert.ToInt32(sessionTable.Rows[i]["absent"]);
                }

                lblDetailsSummary.Text = sessionTable.Rows.Count + " session(s)   |   Open now: " + openSessions +
                                         "   |   Total Present: " + totalPresent + "   |   Total Absent: " + totalAbsent;
            }
            catch (Exception ex)
            {
                dtgEventDetails.DataSource = null;
                lblDetailsSummary.Text = "Could not load the sessions: " + Database.friendlyMessage(ex);
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }
        }

        private void LoadRoster()
        {
            lblDetailsTitle.Text = "Students of: " + selectedEventName;

            string query = "SELECT s.student_number, CONCAT(s.last_name, ', ', s.first_name) AS full_name, " +
                           "d.department_code, s.year_level, DATE_FORMAT(r.added_at, '%b %d, %Y %h:%i %p') AS added_on, " +
                           "(SELECT COUNT(*) FROM attendance_records a WHERE a.event_id = r.event_id AND a.student_id = r.student_id AND a.status = 'Present') AS present, " +
                           "(SELECT COUNT(*) FROM attendance_records a WHERE a.event_id = r.event_id AND a.student_id = r.student_id AND a.status = 'Absent') AS absent " +
                           "FROM event_students r " +
                           "JOIN students s ON s.student_id = r.student_id " +
                           "JOIN departments d ON d.department_id = s.department_id " +
                           "WHERE r.event_id = @id " +
                           "ORDER BY s.last_name, s.first_name";

            MySqlConnection conn = null;

            try
            {
                conn = Database.GetConnection();
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", selectedEventId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable rosterTable = new DataTable();
                adapter.Fill(rosterTable);

                dtgEventDetails.DataSource = rosterTable;

                adapter.Dispose();
                cmd.Dispose();

                dtgEventDetails.Columns["student_number"].HeaderText = "Student ID";
                dtgEventDetails.Columns["full_name"].HeaderText = "Full Name";
                dtgEventDetails.Columns["full_name"].FillWeight = 170;
                dtgEventDetails.Columns["department_code"].HeaderText = "Department";
                dtgEventDetails.Columns["year_level"].HeaderText = "Year Level";
                dtgEventDetails.Columns["added_on"].HeaderText = "Added On";
                dtgEventDetails.Columns["added_on"].FillWeight = 140;
                dtgEventDetails.Columns["present"].HeaderText = "Present";
                dtgEventDetails.Columns["absent"].HeaderText = "Absent";

                if (rosterTable.Rows.Count == 0)
                {
                    lblDetailsSummary.Text = "No students in this event. Only ACTIVE students with 5 registered faces of the selected department/year are included.";
                }
                else
                {
                    lblDetailsSummary.Text = rosterTable.Rows.Count + " student(s) must attend this event. " +
                                             "New students of the same department/year are added automatically until the event ends.";
                }
            }
            catch (Exception ex)
            {
                dtgEventDetails.DataSource = null;
                lblDetailsSummary.Text = "Could not load the students: " + Database.friendlyMessage(ex);
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }
        }

        // ------------------------------------------------------------------
        // Add, edit and delete
        // ------------------------------------------------------------------

        private void btnAddEvent_Click(object sender, EventArgs e)
        {
            AddEvent addEvent = new AddEvent();
            addEvent.ShowDialog();

            LoadEvents();
        }

        private void dtgEventManagement_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string columnName = dtgEventManagement.Columns[e.ColumnIndex].Name;

            // Double-clicking the Edit or Delete button is already handled by CellContentClick.
            if (columnName == "colEditEvent" || columnName == "colDeleteEvent")
            {
                return;
            }

            int eventId = Convert.ToInt32(dtgEventManagement.Rows[e.RowIndex].Cells["colEventId"].Value);

            AddEvent editEvent = new AddEvent(eventId);
            editEvent.ShowDialog();

            LoadEvents();
        }

        private void dtgEventManagement_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int eventId = Convert.ToInt32(dtgEventManagement.Rows[e.RowIndex].Cells["colEventId"].Value);
            string eventName = dtgEventManagement.Rows[e.RowIndex].Cells["colEventName"].Value.ToString();

            if (dtgEventManagement.Columns[e.ColumnIndex].Name == "colEditEvent")
            {
                AddEvent editEvent = new AddEvent(eventId);
                editEvent.ShowDialog();

                LoadEvents();
            }
            else if (dtgEventManagement.Columns[e.ColumnIndex].Name == "colDeleteEvent")
            {
                DeleteEvent(eventId, eventName);
            }
        }

        private void DeleteEvent(int eventId, string eventName)
        {
            // 1. Count what will be deleted together with the event
            int sessionCount = 0;
            int attendanceCount = 0;

            MySqlConnection conn = Database.GetConnection();

            try
            {
                conn.Open();

                MySqlCommand cmdCount = new MySqlCommand(
                    "SELECT (SELECT COUNT(*) FROM event_sessions WHERE event_id = @id) AS session_count, " +
                    "(SELECT COUNT(*) FROM event_attendance a JOIN event_sessions es ON es.session_id = a.session_id WHERE es.event_id = @id) AS attendance_count", conn);
                cmdCount.Parameters.AddWithValue("@id", eventId);

                MySqlDataReader reader = cmdCount.ExecuteReader();

                if (reader.Read())
                {
                    sessionCount = Convert.ToInt32(reader["session_count"]);
                    attendanceCount = Convert.ToInt32(reader["attendance_count"]);
                }

                reader.Close();
                cmdCount.Dispose();
            }
            catch (Exception ex)
            {
                Database.showError("Could not check the event.", ex);
                conn.Close();
                conn.Dispose();
                return;
            }

            conn.Close();
            conn.Dispose();

            // 2. Ask for confirmation
            string message = "Are you sure you want to delete the event \"" + eventName + "\"?\n\n" +
                             "This will also delete:\n" +
                             "- " + sessionCount + " session(s)\n" +
                             "- " + attendanceCount + " attendance record(s)\n" +
                             "- the list of students of this event\n\n";

            if (attendanceCount > 0)
            {
                message = message + "WARNING: The absences of this event will also be removed from the obligations.\n\n";
            }

            message = message + "This cannot be undone.";

            DialogResult answer = MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            // 3. Delete. The sessions, students and attendance are deleted automatically (ON DELETE CASCADE).
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", eventId);
            parameters.Add("@command", "Event deleted successfully.");

            bool deleted = Database.saveUpdateDeleteData("DELETE FROM events WHERE event_id = @id", parameters);

            if (deleted == true)
            {
                selectedEventId = 0;
            }

            LoadEvents();
        }

        // ------------------------------------------------------------------
        // Closing and menu
        // ------------------------------------------------------------------

        private void EventManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.exitIfNoFormIsOpen(this);
        }

        private void btnEventLogout_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                Database.saveLogoutTime();

                lnkCreateAccount login = new lnkCreateAccount();
                login.Show();
                this.Close();
            }
        }

        private void btnEventDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnEventDept_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Departments") == false)
            {
                return;
            }

            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnEventStudentRec_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        private void btnEventOfficer_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Officer Records") == false)
            {
                return;
            }

            OfficerRecords officerRecords = new OfficerRecords();
            officerRecords.Show();
            this.Close();
        }

        private void btnEventFace_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Face Recognition") == false)
            {
                return;
            }

            FaceRecognition faceRecognition = new FaceRecognition();
            faceRecognition.Show();
            this.Close();
        }

        private void btnEventMan_Click(object sender, EventArgs e)
        {
            // We are already in Event Management, so we only refresh the table.
            LoadEvents();
        }

        private void btnEventAttendance_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Attendance Monitoring") == false)
            {
                return;
            }

            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnEventReports_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Reports and Analysis") == false)
            {
                return;
            }

            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        private void btnEventObligation_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Obligations") == false)
            {
                return;
            }

            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        private void btnEventRules_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Rules and Permission") == false)
            {
                return;
            }

            RulesPermission rulesPermission = new RulesPermission();
            rulesPermission.Show();
            this.Close();
        }
    }
}
