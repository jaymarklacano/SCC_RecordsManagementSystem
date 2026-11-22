using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public partial class AttendanceMonitoring : Form
    {
        // true while the filters are being filled (so the change events do nothing).
        private bool isLoading = true;

        // The selected record, so it can be selected again after refreshing.
        private int selectedSessionId = 0;
        private int selectedStudentId = 0;

        public AttendanceMonitoring()
        {
            InitializeComponent();
        }

        private void AttendanceMonitoring_Load(object sender, EventArgs e)
        {
            isLoading = true;

            try
            {
                Database.loadEventComboBox(cmbSelectEvent, "All Events");
                Database.loadDepartmentComboBox(cmbDepartmentAttend, "All Departments");

                if (cmbYearLevelAttend.Items.Count > 0)
                {
                    cmbYearLevelAttend.SelectedIndex = 0;
                }

                if (cmbLogoutStatus.Items.Count > 0)
                {
                    cmbLogoutStatus.SelectedIndex = 0;
                }

                dtpDate.Value = DateTime.Today;
                dtpDate.Checked = false;
                loadSessionComboBox();
            }
            catch (Exception ex)
            {
                Database.showError("Could not prepare the attendance filters.", ex);
                lblSummary.Text = "Filters unavailable. Click Refresh to try again.";
                return;
            }
            finally
            {
                isLoading = false;
            }

            LoadAttendance();
        }

        // ------------------------------------------------------------------
        // Filters
        // ------------------------------------------------------------------

        // Fills the Session ComboBox with the sessions of the selected event.
        private void loadSessionComboBox()
        {
            // Never leave sessions or actionable rows from the previously selected event on screen.
            cmbSession.DataSource = null;
            dtgAttendanceMon.DataSource = null;
            updateSelectedRecord();
            lblSummary.Text = "Sessions unavailable. Click Refresh to reload the filters.";
            int eventId = Database.getSelectedId(cmbSelectEvent);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@event", eventId);

            // When "All Events" is selected, the event name is shown in front of every session.
            DataTable sessionTable = Database.GetData(
                "SELECT es.session_id, CONCAT(IF(@event = 0, CONCAT(e.event_name, ' - '), ''), " +
                "DATE_FORMAT(es.session_date, '%b %d'), ' ', es.session_name, ' (', " +
                "DATE_FORMAT(es.starts_at, '%h:%i %p'), ' - ', DATE_FORMAT(es.ends_at, '%h:%i %p'), ')') AS session_display " +
                "FROM event_sessions es JOIN events e ON e.event_id = es.event_id " +
                "WHERE (@event = 0 OR es.event_id = @event) " +
                "ORDER BY es.starts_at DESC", parameters);

            if (sessionTable.Columns.Count == 0)
            {
                cmbSession.DataSource = null;
                return;
            }

            DataRow firstRow = sessionTable.NewRow();
            firstRow["session_id"] = 0;
            firstRow["session_display"] = "All Sessions";
            sessionTable.Rows.InsertAt(firstRow, 0);

            cmbSession.DisplayMember = "session_display";
            cmbSession.ValueMember = "session_id";
            cmbSession.DataSource = sessionTable;
            cmbSession.SelectedIndex = 0;
        }

        private void cmbSelectEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            isLoading = true;
            try
            {
                loadSessionComboBox();
            }
            catch (MySqlException ex)
            {
                Database.showError("Could not load this event's sessions. Click Refresh to try again.", ex);
                return;
            }
            finally
            {
                isLoading = false;
            }

            LoadAttendance();
        }

        private void cmbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            LoadAttendance();
        }

        // Used by the department, year level, date, search, status and log out filters.
        private void filter_Changed(object sender, EventArgs e)
        {
            LoadAttendance();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                dtpDate.Value = DateTime.Today;
                dtpDate.Checked = true;
            }
            finally
            {
                isLoading = false;
            }

            LoadAttendance();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                if (cmbSelectEvent.Items.Count > 0)
                {
                    cmbSelectEvent.SelectedIndex = 0;
                }

                if (cmbDepartmentAttend.Items.Count > 0)
                {
                    cmbDepartmentAttend.SelectedIndex = 0;
                }

                cmbYearLevelAttend.SelectedIndex = 0;
                cmbLogoutStatus.SelectedIndex = 0;
                dtpDate.Value = DateTime.Today;
                dtpDate.Checked = false;
                txtSearchAttendance.Text = "";
                chkPresent.Checked = true;
                chkAbsent.Checked = true;
                chkPending.Checked = true;
                loadSessionComboBox();
            }
            finally
            {
                isLoading = false;
            }

            LoadAttendance();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cmbSelectEvent.DataSource == null || cmbDepartmentAttend.DataSource == null || cmbSession.DataSource == null)
            {
                AttendanceMonitoring_Load(sender, e);
            }
            else
            {
                LoadAttendance();
            }
        }

        // Returns the checked statuses, for example: 'Present', 'Absent'
        private string buildStatusList()
        {
            string statusList = "";

            if (chkPresent.Checked == true)
            {
                statusList = "'Present'";
            }

            if (chkAbsent.Checked == true)
            {
                if (statusList != "")
                {
                    statusList = statusList + ", ";
                }

                statusList = statusList + "'Absent'";
            }

            if (chkPending.Checked == true)
            {
                if (statusList != "")
                {
                    statusList = statusList + ", ";
                }

                statusList = statusList + "'Pending'";
            }

            return statusList;
        }

        // ------------------------------------------------------------------
        // Attendance table
        // ------------------------------------------------------------------

        public void LoadAttendance()
        {
            if (isLoading || cmbSession.DataSource == null || cmbDepartmentAttend.DataSource == null)
            {
                return;
            }

            string statusList = buildStatusList();

            if (statusList == "")
            {
                dtgAttendanceMon.DataSource = null;
                lblSummary.Text = "Please check at least one status (Present, Absent or Pending).";
                lblNoRecord.Text = "No status is checked.";
                lblNoRecord.Visible = true;
                lblNoRecord.BringToFront();
                updateSelectedRecord();
                return;
            }

            string yearLevel = "";

            if (cmbYearLevelAttend.SelectedIndex > 0)
            {
                yearLevel = cmbYearLevelAttend.SelectedItem.ToString();
            }

            // attendance_records is a VIEW: one row for every student of an event for every session.
            // Present = has a time in, Absent = session ended without time in, Pending = session not finished yet.
            string query = "SELECT event_id, session_id, student_id, student_number, full_name, department_code, year_level, " +
                           "event_name, DATE_FORMAT(session_date, '%b %d, %Y') AS session_day, session_name, status, " +
                           "IFNULL(DATE_FORMAT(time_in, '%h:%i %p'), '-') AS time_in_text, " +
                           "IFNULL(DATE_FORMAT(time_out, '%h:%i %p'), '-') AS time_out_text, " +
                           "logout_status, IFNULL(recorded_by, '-') AS recorded_by, " +
                           "CASE WHEN NOW() < starts_at THEN 'Upcoming' WHEN NOW() < ends_at THEN 'Open' ELSE 'Closed' END AS session_state " +
                           "FROM attendance_records " +
                           "WHERE (@event = 0 OR event_id = @event) " +
                           "AND (@session = 0 OR session_id = @session) " +
                           "AND (@useDate = 0 OR session_date = @date) " +
                           "AND (@department = 0 OR department_id = @department) " +
                           "AND (@year = '' OR year_level = @year) " +
                           "AND (student_number LIKE @search OR full_name LIKE @search) " +
                           "AND status IN (" + statusList + ") " +
                           "AND (@logout = 'All' OR logout_status = @logout) " +
                           "ORDER BY starts_at DESC, full_name";

            MySqlConnection conn = null;

            try
            {
                conn = Database.GetConnection();
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@event", Database.getSelectedId(cmbSelectEvent));
                cmd.Parameters.AddWithValue("@session", Database.getSelectedId(cmbSession));

                if (dtpDate.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@useDate", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@useDate", 0);
                }

                cmd.Parameters.AddWithValue("@date", dtpDate.Value.Date);
                cmd.Parameters.AddWithValue("@department", Database.getSelectedId(cmbDepartmentAttend));
                cmd.Parameters.AddWithValue("@year", yearLevel);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearchAttendance.Text.Trim() + "%");
                cmd.Parameters.AddWithValue("@logout", cmbLogoutStatus.Text);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable attendanceTable = new DataTable();
                adapter.Fill(attendanceTable);

                dtgAttendanceMon.DataSource = attendanceTable;

                adapter.Dispose();
                cmd.Dispose();

                dtgAttendanceMon.Columns["event_id"].Visible = false;
                dtgAttendanceMon.Columns["session_id"].Visible = false;
                dtgAttendanceMon.Columns["student_id"].Visible = false;
                dtgAttendanceMon.Columns["session_state"].Visible = false;

                dtgAttendanceMon.Columns["student_number"].HeaderText = "Student ID";
                dtgAttendanceMon.Columns["full_name"].HeaderText = "Full Name";
                dtgAttendanceMon.Columns["full_name"].FillWeight = 150;
                dtgAttendanceMon.Columns["department_code"].HeaderText = "Department";
                dtgAttendanceMon.Columns["year_level"].HeaderText = "Year";
                dtgAttendanceMon.Columns["event_name"].HeaderText = "Event";
                dtgAttendanceMon.Columns["event_name"].FillWeight = 130;
                dtgAttendanceMon.Columns["session_day"].HeaderText = "Date";
                dtgAttendanceMon.Columns["session_name"].HeaderText = "Session";
                dtgAttendanceMon.Columns["status"].HeaderText = "Status";
                dtgAttendanceMon.Columns["time_in_text"].HeaderText = "Time In";
                dtgAttendanceMon.Columns["time_out_text"].HeaderText = "Time Out";
                dtgAttendanceMon.Columns["logout_status"].HeaderText = "Log Out";
                dtgAttendanceMon.Columns["logout_status"].FillWeight = 110;
                dtgAttendanceMon.Columns["recorded_by"].HeaderText = "Recorded By";
                dtgAttendanceMon.Columns["recorded_by"].FillWeight = 110;

                // Count the records and color the status cells.
                int presentCount = 0;
                int absentCount = 0;
                int pendingCount = 0;
                int missingLogoutCount = 0;

                for (int i = 0; i < dtgAttendanceMon.Rows.Count; i++)
                {
                    DataGridViewRow row = dtgAttendanceMon.Rows[i];
                    string status = row.Cells["status"].Value.ToString();
                    string logoutStatus = row.Cells["logout_status"].Value.ToString();

                    if (status == "Present")
                    {
                        presentCount = presentCount + 1;
                        row.Cells["status"].Style.ForeColor = Color.DarkGreen;
                    }
                    else if (status == "Absent")
                    {
                        absentCount = absentCount + 1;
                        row.Cells["status"].Style.ForeColor = Color.Firebrick;
                    }
                    else
                    {
                        pendingCount = pendingCount + 1;
                        row.Cells["status"].Style.ForeColor = Color.DimGray;
                    }

                    if (logoutStatus == "Missing Log Out")
                    {
                        missingLogoutCount = missingLogoutCount + 1;
                        row.Cells["logout_status"].Style.ForeColor = Color.DarkOrange;
                    }
                    else if (logoutStatus == "Logged Out")
                    {
                        row.Cells["logout_status"].Style.ForeColor = Color.DarkGreen;
                    }
                }

                // Attendance rate = present / (present + absent). Pending records are not counted yet.
                string rateText = "-";

                if (presentCount + absentCount > 0)
                {
                    double rate = (presentCount * 100.0) / (presentCount + absentCount);
                    rateText = rate.ToString("0.0") + "%";
                }

                lblSummary.Text = "Records: " + attendanceTable.Rows.Count +
                                  "   |   Present: " + presentCount +
                                  "   |   Absent: " + absentCount +
                                  "   |   Pending: " + pendingCount +
                                  "   |   Missing Log Out: " + missingLogoutCount +
                                  "   |   Attendance Rate: " + rateText;

                if (attendanceTable.Rows.Count == 0)
                {
                    lblNoRecord.Text = "No attendance records match the filters.";
                    lblNoRecord.Visible = true;
                    lblNoRecord.BringToFront();
                }
                else
                {
                    lblNoRecord.Visible = false;
                }

                // Select the same record as before
                dtgAttendanceMon.ClearSelection();

                for (int i = 0; i < dtgAttendanceMon.Rows.Count; i++)
                {
                    int sessionId = Convert.ToInt32(dtgAttendanceMon.Rows[i].Cells["session_id"].Value);
                    int studentId = Convert.ToInt32(dtgAttendanceMon.Rows[i].Cells["student_id"].Value);

                    if (sessionId == selectedSessionId && studentId == selectedStudentId)
                    {
                        dtgAttendanceMon.Rows[i].Selected = true;
                        dtgAttendanceMon.CurrentCell = dtgAttendanceMon.Rows[i].Cells["student_number"];
                    }
                }
            }
            catch (Exception ex)
            {
                dtgAttendanceMon.DataSource = null;
                lblNoRecord.Visible = false;
                lblSummary.Text = "Could not load the attendance: " + Database.friendlyMessage(ex);
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }

            updateSelectedRecord();
        }

        private void dtgAttendanceMon_SelectionChanged(object sender, EventArgs e)
        {
            updateSelectedRecord();
        }

        // Shows the selected record and turns the manual buttons on or off.
        private void updateSelectedRecord()
        {
            btnMarkPresent.Enabled = false;
            btnMarkLogout.Enabled = false;
            btnRemoveAttendance.Enabled = false;

            if (dtgAttendanceMon.DataSource == null || dtgAttendanceMon.SelectedRows.Count == 0)
            {
                lblSelectedRecord.Text = "No record selected.";
                return;
            }

            DataGridViewRow row = dtgAttendanceMon.SelectedRows[0];

            selectedSessionId = Convert.ToInt32(row.Cells["session_id"].Value);
            selectedStudentId = Convert.ToInt32(row.Cells["student_id"].Value);

            string status = row.Cells["status"].Value.ToString();
            string sessionState = row.Cells["session_state"].Value.ToString();
            string logoutStatus = row.Cells["logout_status"].Value.ToString();

            // Mark Present: only when the student is not present yet and the session already started.
            if (status != "Present" && sessionState != "Upcoming")
            {
                btnMarkPresent.Enabled = true;
            }

            // Mark Log Out: only when the student is present and still has no log out.
            if (status == "Present" && (logoutStatus == "Waiting" || logoutStatus == "Missing Log Out"))
            {
                btnMarkLogout.Enabled = true;
            }

            // Remove: only when there is an attendance to remove.
            if (status == "Present")
            {
                btnRemoveAttendance.Enabled = true;
            }

            lblSelectedRecord.Text = "Selected: " + row.Cells["full_name"].Value + " (" + row.Cells["student_number"].Value + ")   |   " +
                                     row.Cells["event_name"].Value + ", " + row.Cells["session_day"].Value + " " + row.Cells["session_name"].Value +
                                     " (" + sessionState + ")   |   " + status;
        }

        // ------------------------------------------------------------------
        // Manual entry (when the camera cannot recognize the student)
        // ------------------------------------------------------------------

        private void btnMarkPresent_Click(object sender, EventArgs e)
        {
            if (dtgAttendanceMon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record first.", "No Record Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dtgAttendanceMon.SelectedRows[0];
            string fullName = row.Cells["full_name"].Value.ToString();
            string sessionText = row.Cells["event_name"].Value + " - " + row.Cells["session_day"].Value + " " + row.Cells["session_name"].Value;
            string status = row.Cells["status"].Value.ToString();

            string message = "Mark " + fullName + " as PRESENT in:\n" + sessionText + "?\n\nThis will be saved as a MANUAL attendance.";

            if (status == "Absent")
            {
                message = message + "\n\nThe absence (10 obligation points) will be removed.";
            }

            DialogResult answer = MessageBox.Show(message, "Mark Present", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            // Open session: time in = now.
            // Closed session: time in = start of the session (and time out = end when log out is needed).
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@session", selectedSessionId);
            parameters.Add("@student", selectedStudentId);
            parameters.Add("@command", "Attendance recorded. " + fullName + " is now PRESENT (Manual).");

            Database.saveUpdateDeleteData(
                "INSERT INTO event_attendance (session_id, student_id, time_in, time_out, recorded_by) " +
                "SELECT es.session_id, @student, IF(NOW() < es.ends_at, NOW(), es.starts_at), " +
                "IF(NOW() >= es.ends_at AND e.requires_logout = 1, es.ends_at, NULL), 'Manual' " +
                "FROM event_sessions es JOIN events e ON e.event_id = es.event_id " +
                "WHERE es.session_id = @session AND es.starts_at <= NOW()", parameters);

            LoadAttendance();
        }

        private void btnMarkLogout_Click(object sender, EventArgs e)
        {
            if (dtgAttendanceMon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record first.", "No Record Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dtgAttendanceMon.SelectedRows[0];
            string fullName = row.Cells["full_name"].Value.ToString();

            DialogResult answer = MessageBox.Show("Record the LOG OUT of " + fullName + "?", "Mark Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            // Time out = now, but never later than the end of the session and never before the time in.
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@session", selectedSessionId);
            parameters.Add("@student", selectedStudentId);
            parameters.Add("@command", "Log out recorded for " + fullName + ".");

            Database.saveUpdateDeleteData(
                "UPDATE event_attendance a JOIN event_sessions es ON es.session_id = a.session_id " +
                "SET a.time_out = GREATEST(a.time_in, IF(NOW() < es.ends_at, NOW(), es.ends_at)) " +
                "WHERE a.session_id = @session AND a.student_id = @student AND a.time_out IS NULL", parameters);

            LoadAttendance();
        }

        private void btnRemoveAttendance_Click(object sender, EventArgs e)
        {
            if (dtgAttendanceMon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record first.", "No Record Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dtgAttendanceMon.SelectedRows[0];
            string fullName = row.Cells["full_name"].Value.ToString();
            string sessionState = row.Cells["session_state"].Value.ToString();

            string message = "Remove the attendance of " + fullName + " in this session?\n\n" +
                             "Use this only when the attendance was recorded by mistake.";

            if (sessionState == "Closed")
            {
                message = message + "\n\nWARNING: The session is already closed, so the student will become ABSENT (+10 obligation points).";
            }

            DialogResult answer = MessageBox.Show(message, "Remove Attendance", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@session", selectedSessionId);
            parameters.Add("@student", selectedStudentId);
            parameters.Add("@command", "The attendance of " + fullName + " was removed.");

            Database.saveUpdateDeleteData("DELETE FROM event_attendance WHERE session_id = @session AND student_id = @student", parameters);

            LoadAttendance();
        }

        // ------------------------------------------------------------------
        // Export to CSV (can be opened in Excel)
        // ------------------------------------------------------------------

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (dtgAttendanceMon.DataSource == null || dtgAttendanceMon.Rows.Count == 0)
            {
                MessageBox.Show("There are no records to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string fileName;
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Export Attendance";
                saveDialog.Filter = "CSV file (*.csv)|*.csv";
                saveDialog.FileName = "attendance_" + DateTime.Now.ToString("yyyy-MM-dd_HHmm") + ".csv";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                fileName = saveDialog.FileName;
            }

            try
            {
                writeCsv(fileName);
                MessageBox.Show(dtgAttendanceMon.Rows.Count + " record(s) were exported to:\n" + fileName, "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException)
            {
                MessageBox.Show("The file could not be saved. If it is open in Excel, close it and try again.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You are not allowed to save in that folder. Please choose another folder.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void writeCsv(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                // 1. The header line (only the visible columns)
                string line = "";

                for (int c = 0; c < dtgAttendanceMon.Columns.Count; c++)
                {
                    if (dtgAttendanceMon.Columns[c].Visible == true)
                    {
                        if (line != "")
                        {
                            line = line + ",";
                        }

                        line = line + csvValue(dtgAttendanceMon.Columns[c].HeaderText);
                    }
                }

                writer.WriteLine(line);

                // 2. One line for every record
                for (int r = 0; r < dtgAttendanceMon.Rows.Count; r++)
                {
                    line = "";

                    for (int c = 0; c < dtgAttendanceMon.Columns.Count; c++)
                    {
                        if (dtgAttendanceMon.Columns[c].Visible == true)
                        {
                            if (line != "")
                            {
                                line = line + ",";
                            }

                            object value = dtgAttendanceMon.Rows[r].Cells[c].Value;

                            if (value == null)
                            {
                                line = line + csvValue("");
                            }
                            else
                            {
                                line = line + csvValue(value.ToString());
                            }
                        }
                    }

                    writer.WriteLine(line);
                }
            }
        }

        // Puts a value inside quotes so commas in names do not break the CSV file.
        private string csvValue(string value)
        {
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        // ------------------------------------------------------------------
        // Closing and menu
        // ------------------------------------------------------------------

        private void AttendanceMonitoring_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.exitIfNoFormIsOpen(this);
        }

        private void btnAttendLogout_Click(object sender, EventArgs e)
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

        private void btnAttendDash_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnAttendDept_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Departments") == false)
            {
                return;
            }

            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnAttendStudent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        private void btnAttendOfficer_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Officer Records") == false)
            {
                return;
            }

            OfficerRecords officerRecords = new OfficerRecords();
            officerRecords.Show();
            this.Close();
        }

        private void btnAttendFace_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Face Recognition") == false)
            {
                return;
            }

            FaceRecognition faceRecognition = new FaceRecognition();
            faceRecognition.Show();
            this.Close();
        }

        private void btnAttendEvent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Event Management") == false)
            {
                return;
            }

            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnAttendMonitoring_Click(object sender, EventArgs e)
        {
            // We are already in Attendance Monitoring, so we only refresh the table.
            LoadAttendance();
        }

        private void btnAttendReports_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Reports and Analysis") == false)
            {
                return;
            }

            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        private void btnAttendObligation_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Obligations") == false)
            {
                return;
            }

            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        private void btnAttendRules_Click(object sender, EventArgs e)
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
