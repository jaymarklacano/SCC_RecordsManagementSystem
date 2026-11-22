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
using System.Windows.Forms.DataVisualization.Charting;

namespace SCCRecordsManagementSystem
{
    public partial class ReportsAndAnalysis : Form
    {
        // true while the filters are being filled (so the change events do nothing).
        private bool isLoading = true;

        // The filters used by every attendance query of this page.
        // Only FINISHED sessions are counted, so every student is already Present or Absent.
        private string attendanceFilter = "ends_at <= NOW() " +
                                          "AND (@event = 0 OR event_id = @event) " +
                                          "AND session_date >= @from AND session_date <= @to " +
                                          "AND (@department = 0 OR department_id = @department) " +
                                          "AND (@year = '' OR year_level = @year) " +
                                          "AND (student_number LIKE @search OR full_name LIKE @search)";

        public ReportsAndAnalysis()
        {
            InitializeComponent();
        }

        private void ReportsAndAnalysis_Load(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                Database.loadEventComboBox(cmbEventSelect, "All Events");
                Database.loadDepartmentComboBox(cmbDepartment, "All Departments");
                cmbYearLevel.SelectedIndex = 0;
                cmbReportType.SelectedIndex = 0;
                setDefaultDates();
                showEventDetails();
            }
            catch (MySqlException ex)
            {
                clearReport("Filters unavailable. Reopen Reports and Analysis to retry.");
                Database.showError("Could not load the report filters.", ex);
                return;
            }
            finally
            {
                isLoading = false;
            }

            GenerateReport();
        }

        // ------------------------------------------------------------------
        // Filters
        // ------------------------------------------------------------------

        // From = the first day that has a session, To = today.
        // So the first report shows all the attendance recorded so far.
        private void setDefaultDates()
        {
            object firstDate = Database.GetValue("SELECT MIN(session_date) FROM event_sessions", null);

            if (firstDate == null || firstDate == DBNull.Value)
            {
                dtpFromDate.Value = DateTime.Today.AddMonths(-1);
            }
            else
            {
                DateTime firstSessionDate = Convert.ToDateTime(firstDate);

                if (firstSessionDate > DateTime.Today)
                {
                    dtpFromDate.Value = DateTime.Today;
                }
                else
                {
                    dtpFromDate.Value = firstSessionDate;
                }
            }

            dtpToDate.Value = DateTime.Today;
        }

        // Shows the venue, schedule and organizer of the selected event.
        // When one event is selected, the date range becomes the dates of that event.
        private void showEventDetails()
        {
            int eventId = Database.getSelectedId(cmbEventSelect);

            if (eventId == 0)
            {
                lblVenue.Text = "All events";
                lblSchedule.Text = "Use the From and To dates above";
                lblOrganizer.Text = "-";
                return;
            }

            EventInfo eventInfo = Database.searchAndGetEventInfo(eventId);

            if (eventInfo == null)
            {
                lblVenue.Text = "-";
                lblSchedule.Text = "The event was not found.";
                lblOrganizer.Text = "-";
                return;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", eventId);
            int sessionCount = Database.GetCount("SELECT COUNT(*) FROM event_sessions WHERE event_id = @id", parameters);

            int numberOfDays = (eventInfo.EndDate.Date - eventInfo.StartDate.Date).Days + 1;

            lblVenue.Text = eventInfo.Venue;

            if (eventInfo.StartDate.Date == eventInfo.EndDate.Date)
            {
                lblSchedule.Text = eventInfo.StartDate.ToString("MMM dd, yyyy") + " (" + sessionCount + " session(s))";
            }
            else
            {
                lblSchedule.Text = eventInfo.StartDate.ToString("MMM dd") + " - " + eventInfo.EndDate.ToString("MMM dd, yyyy") +
                                   " (" + numberOfDays + " days, " + sessionCount + " sessions)";
            }

            if (eventInfo.Organizer.Trim() == "")
            {
                lblOrganizer.Text = "-";
            }
            else
            {
                lblOrganizer.Text = eventInfo.Organizer;
            }

            dtpFromDate.Value = eventInfo.StartDate.Date;
            dtpToDate.Value = eventInfo.EndDate.Date;
        }

        private void cmbEventSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            isLoading = true;
            try
            {
                showEventDetails();
            }
            catch (MySqlException ex)
            {
                clearReport("Could not load the selected event. Select it again to retry.");
                Database.showError("Could not load the event schedule.", ex);
                return;
            }
            finally
            {
                isLoading = false;
            }

            GenerateReport();
        }

        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateReport();
        }

        // Used by the dates, department, year level, search, Present and Absent filters.
        private void filter_Changed(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                if (cmbEventSelect.Items.Count > 0)
                {
                    cmbEventSelect.SelectedIndex = 0;
                }

                if (cmbDepartment.Items.Count > 0)
                {
                    cmbDepartment.SelectedIndex = 0;
                }

                cmbYearLevel.SelectedIndex = 0;
                cmbReportType.SelectedIndex = 0;
                txtSearchStudent.Text = "";
                chkPresent.Checked = true;
                chkAbsent.Checked = true;
                setDefaultDates();
                showEventDetails();
            }
            catch (MySqlException ex)
            {
                clearReport("Could not reset the filters. Try Clear Filters again.");
                Database.showError("Could not load the default report dates.", ex);
                return;
            }
            finally
            {
                isLoading = false;
            }

            GenerateReport();
        }

        // ------------------------------------------------------------------
        // Report
        // ------------------------------------------------------------------

        public void GenerateReport()
        {
            if (isLoading == true)
            {
                return;
            }

            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                clearReport("The From date must be the same as or before the To date.");
                return;
            }

            MySqlConnection conn = null;

            try
            {
                conn = Database.GetConnection();
                this.Cursor = Cursors.WaitCursor;
                conn.Open();

                // 1. Summary numbers
                string summaryQuery = "SELECT COUNT(DISTINCT session_id) AS total_sessions, " +
                                      "COUNT(DISTINCT student_id) AS total_students, " +
                                      "IFNULL(SUM(status = 'Present'), 0) AS present_count, " +
                                      "IFNULL(SUM(status = 'Absent'), 0) AS absent_count, " +
                                      "IFNULL(SUM(logout_status = 'Missing Log Out'), 0) AS missing_logout " +
                                      "FROM attendance_records WHERE " + attendanceFilter;

                DataTable summaryTable = fillTable(conn, summaryQuery);
                showSummary(summaryTable);

                // 2. Chart: present and absent per department
                string chartQuery = "SELECT department_code, " +
                                    "SUM(status = 'Present') AS present_count, " +
                                    "SUM(status = 'Absent') AS absent_count " +
                                    "FROM attendance_records WHERE " + attendanceFilter + " " +
                                    "GROUP BY department_code ORDER BY department_code";

                DataTable chartTable = fillTable(conn, chartQuery);
                showChart(chartTable);

                // 3. The table of the selected report type
                string reportQuery = buildReportQuery();

                if (reportQuery == "")
                {
                    dtgRecord.DataSource = null;
                    lblReportInfo.Text = cmbReportType.Text;
                    showNoRecord("Please check Present or Absent to show the list.");
                }
                else
                {
                    DataTable reportTable = fillTable(conn, reportQuery);
                    showReport(reportTable);
                }
            }
            catch (Exception ex)
            {
                clearReport("The report could not be generated.");
                Database.showError("Could not generate the report.", ex);
            }
            finally
            {
                if (conn != null) conn.Dispose();
                this.Cursor = Cursors.Default;
            }
        }

        // Runs a SELECT query with the values of the filters and returns the result.
        private DataTable fillTable(MySqlConnection conn, string query)
        {
            string yearLevel = "";

            if (cmbYearLevel.SelectedIndex > 0)
            {
                yearLevel = cmbYearLevel.SelectedItem.ToString();
            }

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@event", Database.getSelectedId(cmbEventSelect));
            cmd.Parameters.AddWithValue("@from", dtpFromDate.Value.Date);
            cmd.Parameters.AddWithValue("@to", dtpToDate.Value.Date);
            cmd.Parameters.AddWithValue("@department", Database.getSelectedId(cmbDepartment));
            cmd.Parameters.AddWithValue("@year", yearLevel);
            cmd.Parameters.AddWithValue("@search", "%" + txtSearchStudent.Text.Trim() + "%");

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            adapter.Dispose();
            cmd.Dispose();

            return table;
        }

        // Returns the SELECT query of the selected report type.
        // Returns "" when the Detailed Attendance List has no status checked.
        private string buildReportQuery()
        {
            string query = "";
            int reportType = cmbReportType.SelectedIndex;

            if (reportType == 0)
            {
                // Attendance per Session
                query = "SELECT session_id, event_name, DATE_FORMAT(session_date, '%b %d, %Y') AS session_day, session_name, " +
                        "CONCAT(DATE_FORMAT(MIN(starts_at), '%h:%i %p'), ' - ', DATE_FORMAT(MIN(ends_at), '%h:%i %p')) AS session_time, " +
                        "COUNT(*) AS total_students, " +
                        "SUM(status = 'Present') AS present_count, " +
                        "SUM(status = 'Absent') AS absent_count, " +
                        "ROUND(SUM(status = 'Present') * 100 / COUNT(*), 1) AS attendance_rate, " +
                        "SUM(logout_status = 'Missing Log Out') AS missing_logout " +
                        "FROM attendance_records WHERE " + attendanceFilter + " " +
                        "GROUP BY session_id, event_name, session_date, session_name " +
                        "ORDER BY MIN(starts_at) DESC";
            }
            else if (reportType == 1)
            {
                // Attendance per Department
                query = "SELECT department_code, " +
                        "COUNT(DISTINCT student_id) AS total_students, " +
                        "COUNT(DISTINCT session_id) AS total_sessions, " +
                        "SUM(status = 'Present') AS present_count, " +
                        "SUM(status = 'Absent') AS absent_count, " +
                        "ROUND(SUM(status = 'Present') * 100 / COUNT(*), 1) AS attendance_rate, " +
                        "SUM(logout_status = 'Missing Log Out') AS missing_logout " +
                        "FROM attendance_records WHERE " + attendanceFilter + " " +
                        "GROUP BY department_code ORDER BY department_code";
            }
            else if (reportType == 2 || reportType == 3)
            {
                // Student Attendance Summary / Most Absent Students
                query = "SELECT student_id, student_number, full_name, department_code, year_level, " +
                        "COUNT(*) AS total_sessions, " +
                        "SUM(status = 'Present') AS present_count, " +
                        "SUM(status = 'Absent') AS absent_count, " +
                        "ROUND(SUM(status = 'Present') * 100 / COUNT(*), 1) AS attendance_rate, " +
                        "SUM(logout_status = 'Missing Log Out') AS missing_logout, " +
                        "SUM(status = 'Absent') * 10 AS absence_points " +
                        "FROM attendance_records WHERE " + attendanceFilter + " " +
                        "GROUP BY student_id, student_number, full_name, department_code, year_level ";

                if (reportType == 2)
                {
                    query = query + "ORDER BY full_name";
                }
                else
                {
                    query = query + "HAVING SUM(status = 'Absent') > 0 ORDER BY absent_count DESC, full_name";
                }
            }
            else if (reportType == 4)
            {
                // Missing Log Out: present students who did not log out before the session ended
                query = "SELECT student_number, full_name, department_code, year_level, event_name, " +
                        "DATE_FORMAT(session_date, '%b %d, %Y') AS session_day, session_name, " +
                        "DATE_FORMAT(time_in, '%h:%i %p') AS time_in_text, " +
                        "DATE_FORMAT(ends_at, '%h:%i %p') AS session_end, logout_status " +
                        "FROM attendance_records WHERE " + attendanceFilter + " " +
                        "AND logout_status = 'Missing Log Out' " +
                        "ORDER BY starts_at DESC, full_name";
            }
            else if (reportType == 5)
            {
                // Detailed Attendance List (Present and/or Absent)
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

                if (statusList == "")
                {
                    return "";
                }

                query = "SELECT student_number, full_name, department_code, year_level, event_name, " +
                        "DATE_FORMAT(session_date, '%b %d, %Y') AS session_day, session_name, status, " +
                        "IFNULL(DATE_FORMAT(time_in, '%h:%i %p'), '-') AS time_in_text, " +
                        "IFNULL(DATE_FORMAT(time_out, '%h:%i %p'), '-') AS time_out_text, " +
                        "logout_status, IFNULL(recorded_by, '-') AS recorded_by " +
                        "FROM attendance_records WHERE " + attendanceFilter + " " +
                        "AND status IN (" + statusList + ") " +
                        "ORDER BY starts_at DESC, full_name";
            }
            else
            {
                // Obligation Balances: all events (the event and date filters are not used here)
                query = "SELECT student_id, student_number, full_name, department_code, year_level, " +
                        "absent_count, absence_points, adjustment_points, balance, " +
                        "IF(balance > 0, 'With Balance', 'Cleared') AS obligation_status " +
                        "FROM obligation_summary " +
                        "WHERE (absent_count > 0 OR adjustment_points <> 0) " +
                        "AND (@department = 0 OR department_id = @department) " +
                        "AND (@year = '' OR year_level = @year) " +
                        "AND (student_number LIKE @search OR full_name LIKE @search) " +
                        "ORDER BY balance DESC, full_name";
            }

            return query;
        }

        private void showSummary(DataTable summaryTable)
        {
            int totalSessions = 0;
            int totalStudents = 0;
            int presentCount = 0;
            int absentCount = 0;
            int missingLogout = 0;

            if (summaryTable.Rows.Count > 0)
            {
                DataRow row = summaryTable.Rows[0];
                totalSessions = Convert.ToInt32(row["total_sessions"]);
                totalStudents = Convert.ToInt32(row["total_students"]);
                presentCount = Convert.ToInt32(row["present_count"]);
                absentCount = Convert.ToInt32(row["absent_count"]);
                missingLogout = Convert.ToInt32(row["missing_logout"]);
            }

            // Attendance rate = present / (present + absent) x 100
            string rateText = "0%";

            if (presentCount + absentCount > 0)
            {
                double rate = presentCount * 100.0 / (presentCount + absentCount);
                rateText = rate.ToString("0.0") + "%";
            }

            lblSummary.ForeColor = Color.MidnightBlue;
            lblSummary.Text = "Sessions: " + totalSessions +
                              "    Students: " + totalStudents +
                              "    Present: " + presentCount +
                              "    Absent: " + absentCount +
                              "    Rate: " + rateText +
                              "    Missing Log Out: " + missingLogout;
        }

        private void showChart(DataTable chartTable)
        {
            Series presentSeries = chtAttendanceDepartment.Series["Present"];
            Series absentSeries = chtAttendanceDepartment.Series["Absent"];

            presentSeries.Points.Clear();
            absentSeries.Points.Clear();
            chtAttendanceDepartment.Titles.Clear();

            // The Present and Absent check boxes show or hide the bars.
            presentSeries.Enabled = chkPresent.Checked;
            absentSeries.Enabled = chkAbsent.Checked;

            for (int i = 0; i < chartTable.Rows.Count; i++)
            {
                DataRow row = chartTable.Rows[i];
                string departmentCode = row["department_code"].ToString();
                int presentCount = Convert.ToInt32(row["present_count"]);
                int absentCount = Convert.ToInt32(row["absent_count"]);

                presentSeries.Points.AddXY(departmentCode, presentCount);
                absentSeries.Points.AddXY(departmentCode, absentCount);
            }

            if (chartTable.Rows.Count == 0)
            {
                chtAttendanceDepartment.Titles.Add("No finished sessions for the selected filters.");
            }
            else if (chkPresent.Checked == false && chkAbsent.Checked == false)
            {
                chtAttendanceDepartment.Titles.Add("Check Present or Absent to show the bars.");
            }
        }

        private void showReport(DataTable reportTable)
        {
            dtgRecord.DataSource = reportTable;

            // Hide the id columns
            hideColumn("session_id");
            hideColumn("student_id");

            // Friendly column names
            setHeader("event_name", "Event");
            setHeader("session_day", "Date");
            setHeader("session_name", "Session");
            setHeader("session_time", "Time");
            setHeader("session_end", "Session End");
            setHeader("department_code", "Department");
            setHeader("student_number", "Student ID");
            setHeader("full_name", "Full Name");
            setHeader("year_level", "Year");
            setHeader("total_students", "Students");
            setHeader("total_sessions", "Sessions");
            setHeader("present_count", "Present");
            setHeader("absent_count", "Absent");
            setHeader("attendance_rate", "Rate (%)");
            setHeader("missing_logout", "Missing Log Out");
            setHeader("absence_points", "Absence Points");
            setHeader("adjustment_points", "Adjustments");
            setHeader("balance", "Balance");
            setHeader("obligation_status", "Obligation");
            setHeader("status", "Status");
            setHeader("time_in_text", "Time In");
            setHeader("time_out_text", "Time Out");
            setHeader("logout_status", "Log Out");
            setHeader("recorded_by", "Recorded By");

            if (dtgRecord.Columns.Contains("full_name") == true)
            {
                dtgRecord.Columns["full_name"].FillWeight = 150;
            }

            if (dtgRecord.Columns.Contains("event_name") == true)
            {
                dtgRecord.Columns["event_name"].FillWeight = 140;
            }

            colorReportRows();

            // Report title and number of rows
            if (cmbReportType.SelectedIndex == 6)
            {
                lblReportInfo.Text = cmbReportType.Text + ": " + reportTable.Rows.Count + " student(s)   (all events - the date range is not used)";
            }
            else
            {
                lblReportInfo.Text = cmbReportType.Text + ": " + reportTable.Rows.Count + " row(s)   (" +
                                     dtpFromDate.Value.ToString("MMM dd, yyyy") + " to " + dtpToDate.Value.ToString("MMM dd, yyyy") + ")";
            }

            if (reportTable.Rows.Count == 0)
            {
                showNoRecord("No records found for the selected filters.");
            }
            else
            {
                lblNoRecord.Visible = false;
            }
        }

        // Colors the important numbers so problems are easy to see.
        private void colorReportRows()
        {
            for (int i = 0; i < dtgRecord.Rows.Count; i++)
            {
                DataGridViewRow row = dtgRecord.Rows[i];

                if (dtgRecord.Columns.Contains("attendance_rate") == true)
                {
                    double rate = Convert.ToDouble(row.Cells["attendance_rate"].Value);

                    if (rate < 75)
                    {
                        row.Cells["attendance_rate"].Style.ForeColor = Color.Firebrick;
                    }
                    else if (rate >= 90)
                    {
                        row.Cells["attendance_rate"].Style.ForeColor = Color.DarkGreen;
                    }
                }

                if (dtgRecord.Columns.Contains("absent_count") == true)
                {
                    if (Convert.ToInt32(row.Cells["absent_count"].Value) > 0)
                    {
                        row.Cells["absent_count"].Style.ForeColor = Color.Firebrick;
                    }
                }

                if (dtgRecord.Columns.Contains("status") == true)
                {
                    if (row.Cells["status"].Value.ToString() == "Present")
                    {
                        row.Cells["status"].Style.ForeColor = Color.DarkGreen;
                    }
                    else
                    {
                        row.Cells["status"].Style.ForeColor = Color.Firebrick;
                    }
                }

                if (dtgRecord.Columns.Contains("logout_status") == true)
                {
                    if (row.Cells["logout_status"].Value.ToString() == "Missing Log Out")
                    {
                        row.Cells["logout_status"].Style.ForeColor = Color.DarkOrange;
                    }
                }

                if (dtgRecord.Columns.Contains("balance") == true)
                {
                    if (Convert.ToInt32(row.Cells["balance"].Value) > 0)
                    {
                        row.Cells["balance"].Style.ForeColor = Color.Firebrick;
                        row.Cells["obligation_status"].Style.ForeColor = Color.Firebrick;
                    }
                    else
                    {
                        row.Cells["balance"].Style.ForeColor = Color.DarkGreen;
                        row.Cells["obligation_status"].Style.ForeColor = Color.DarkGreen;
                    }
                }
            }
        }

        private void setHeader(string columnName, string headerText)
        {
            if (dtgRecord.Columns.Contains(columnName) == true)
            {
                dtgRecord.Columns[columnName].HeaderText = headerText;
            }
        }

        private void hideColumn(string columnName)
        {
            if (dtgRecord.Columns.Contains(columnName) == true)
            {
                dtgRecord.Columns[columnName].Visible = false;
            }
        }

        private void showNoRecord(string message)
        {
            lblNoRecord.Text = message;
            lblNoRecord.Visible = true;
            lblNoRecord.BringToFront();
        }

        // Empties the summary, the chart and the table and shows a message.
        private void clearReport(string message)
        {
            lblSummary.ForeColor = Color.Firebrick;
            lblSummary.Text = message;
            lblReportInfo.Text = "";

            chtAttendanceDepartment.Series["Present"].Points.Clear();
            chtAttendanceDepartment.Series["Absent"].Points.Clear();
            chtAttendanceDepartment.Titles.Clear();

            dtgRecord.DataSource = null;
            showNoRecord(message);
        }

        // ------------------------------------------------------------------
        // Export
        // ------------------------------------------------------------------

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (dtgRecord.DataSource == null || dtgRecord.Rows.Count == 0)
            {
                MessageBox.Show("There is no report to export. Please generate a report first.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string fileName;
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Export Report";
                saveDialog.Filter = "CSV file (*.csv)|*.csv";
                saveDialog.FileName = cmbReportType.Text.Replace(" ", "_") + "_" + DateTime.Now.ToString("yyyy-MM-dd_HHmm") + ".csv";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                fileName = saveDialog.FileName;
            }

            try
            {
                writeCsv(fileName);
                MessageBox.Show("The report (" + dtgRecord.Rows.Count + " row(s)) was exported to:\n" + fileName, "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                // 1. Report title, filters and summary
                writer.WriteLine(csvValue("SCC Records Management System - " + cmbReportType.Text));
                writer.WriteLine(csvValue("Event: " + cmbEventSelect.Text + "   Department: " + cmbDepartment.Text + "   Year Level: " + cmbYearLevel.Text));

                if (cmbReportType.SelectedIndex != 6)
                {
                    writer.WriteLine(csvValue("Date Range: " + dtpFromDate.Value.ToString("MMM dd, yyyy") + " to " + dtpToDate.Value.ToString("MMM dd, yyyy")));
                    writer.WriteLine(csvValue(lblSummary.Text));
                }

                writer.WriteLine(csvValue("Generated: " + DateTime.Now.ToString("MMM dd, yyyy hh:mm tt")));
                writer.WriteLine("");

                // 2. The header line (only the visible columns)
                string line = "";

                for (int c = 0; c < dtgRecord.Columns.Count; c++)
                {
                    if (dtgRecord.Columns[c].Visible == true)
                    {
                        if (line != "")
                        {
                            line = line + ",";
                        }

                        line = line + csvValue(dtgRecord.Columns[c].HeaderText);
                    }
                }

                writer.WriteLine(line);

                // 3. One line for every row
                for (int r = 0; r < dtgRecord.Rows.Count; r++)
                {
                    line = "";

                    for (int c = 0; c < dtgRecord.Columns.Count; c++)
                    {
                        if (dtgRecord.Columns[c].Visible == true)
                        {
                            if (line != "")
                            {
                                line = line + ",";
                            }

                            object value = dtgRecord.Rows[r].Cells[c].Value;

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
        // Navigation (side menu)
        // ------------------------------------------------------------------

        private void ReportsAndAnalysis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.exitIfNoFormIsOpen(this);
        }

        private void btnRepLogout_Click(object sender, EventArgs e)
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

        private void btnRepDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnRepDepartment_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Departments") == false)
            {
                return;
            }

            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnRepStudent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        private void btnRepOfficer_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Officer Records") == false)
            {
                return;
            }

            OfficerRecords officerRecords = new OfficerRecords();
            officerRecords.Show();
            this.Close();
        }

        private void btnRepFace_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Face Recognition") == false)
            {
                return;
            }

            FaceRecognition faceRecognition = new FaceRecognition();
            faceRecognition.Show();
            this.Close();
        }

        private void btnRepEvent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Event Management") == false)
            {
                return;
            }

            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnRepAttendance_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Attendance Monitoring") == false)
            {
                return;
            }

            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnRepReports_Click(object sender, EventArgs e)
        {
            // We are already in Reports and Analysis, so we only generate the report again.
            GenerateReport();
        }

        private void btnRepObligation_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Obligations") == false)
            {
                return;
            }

            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        private void btnRepRules_Click(object sender, EventArgs e)
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
