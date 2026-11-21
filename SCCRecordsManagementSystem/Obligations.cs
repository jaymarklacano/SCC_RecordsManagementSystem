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
    public partial class Obligations : Form
    {
        // true while the filters are being filled (so the change events do nothing).
        private bool isLoading = false;

        // Information of the student selected in the list.
        private int selectedStudentId = 0;
        private string selectedStudentName = "";
        private string selectedStudentNumber = "";
        private int selectedBalance = 0;

        public Obligations()
        {
            InitializeComponent();
        }

        private void Obligations_Load(object sender, EventArgs e)
        {
            isLoading = true;

            Database.loadDepartmentComboBox(cmbDepartmentObli, "All Departments");
            cmbYearLevelObli.SelectedIndex = 0;
            cmbBalanceFilter.SelectedIndex = 0;
            txtSearchObli.Text = "";
            txtReason.Text = "";
            nudPoints.Value = 10;

            isLoading = false;

            LoadObligations();
            LoadSummaryCards();
        }

        // ------------------------------------------------------------------
        // Loading the list
        // ------------------------------------------------------------------

        // Shows every student with their absences and obligation balance.
        // Every absence = 10 points. Adjustments are the points added, deducted or cleared by the Admin/Officer.
        private void LoadObligations()
        {
            int departmentId = Database.getSelectedId(cmbDepartmentObli);

            string yearLevel = "";
            if (cmbYearLevelObli.SelectedIndex > 0)
            {
                yearLevel = cmbYearLevelObli.Text;
            }

            // Extra condition for the "Show" filter.
            string balanceCondition = "";
            if (cmbBalanceFilter.SelectedIndex == 1)
            {
                balanceCondition = "AND balance > 0 ";
            }
            else if (cmbBalanceFilter.SelectedIndex == 2)
            {
                balanceCondition = "AND balance = 0 ";
            }
            else if (cmbBalanceFilter.SelectedIndex == 3)
            {
                balanceCondition = "AND absent_count > 0 ";
            }
            else if (cmbBalanceFilter.SelectedIndex == 4)
            {
                balanceCondition = "AND absent_count = 0 ";
            }

            string query = "SELECT student_id, student_number AS 'ID Number', full_name AS 'Student Name', " +
                           "department_code AS 'Department', year_level AS 'Year Level', status AS 'Status', " +
                           "absent_count AS 'Absences', absence_points AS 'Absence Points', " +
                           "adjustment_points AS 'Adjustments', balance AS 'Balance' " +
                           "FROM obligation_summary " +
                           "WHERE (@department = 0 OR department_id = @department) " +
                           "AND (@year = '' OR year_level = @year) " +
                           "AND (student_number LIKE @search OR full_name LIKE @search) " +
                           balanceCondition +
                           "ORDER BY balance DESC, full_name";

            MySqlConnection conn = Database.GetConnection();
            DataTable obligationTable = new DataTable();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@department", departmentId);
                cmd.Parameters.AddWithValue("@year", yearLevel);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearchObli.Text.Trim() + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(obligationTable);

                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Database.showError("The obligation list could not be loaded.", ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            // Remember the selected student so it can be selected again.
            int studentToSelect = selectedStudentId;

            isLoading = true;
            dtgObligation.DataSource = obligationTable;
            isLoading = false;

            if (dtgObligation.Columns.Contains("student_id"))
            {
                dtgObligation.Columns["student_id"].Visible = false;
            }

            colorObligationRows();

            // Count the students and the total balance that is shown.
            int totalBalance = 0;
            for (int i = 0; i < obligationTable.Rows.Count; i++)
            {
                totalBalance = totalBalance + Convert.ToInt32(obligationTable.Rows[i]["Balance"]);
            }

            lblListInfo.Text = "Showing " + obligationTable.Rows.Count + " student(s)   |   Balance shown: " + totalBalance + " point(s)";

            if (obligationTable.Rows.Count == 0)
            {
                lblNoRecord.Text = "No students found. Try changing the filters or the search text.";
                lblNoRecord.Visible = true;
                lblNoRecord.BringToFront();
            }
            else
            {
                lblNoRecord.Visible = false;
            }

            selectStudentInList(studentToSelect);
        }

        // Red row = has balance, green row = absences that are already cleared.
        private void colorObligationRows()
        {
            for (int i = 0; i < dtgObligation.Rows.Count; i++)
            {
                DataGridViewRow row = dtgObligation.Rows[i];
                int balance = Convert.ToInt32(row.Cells["Balance"].Value);
                int absences = Convert.ToInt32(row.Cells["Absences"].Value);

                if (balance > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.Cells["Balance"].Style.ForeColor = Color.Firebrick;
                }
                else if (absences > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Honeydew;
                    row.Cells["Balance"].Style.ForeColor = Color.DarkGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }

                row.Cells["Balance"].Style.Font = new Font(dtgObligation.Font, FontStyle.Bold);
            }
        }

        // Selects the row of the student again after the list is refreshed.
        private void selectStudentInList(int studentId)
        {
            dtgObligation.ClearSelection();

            for (int i = 0; i < dtgObligation.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtgObligation.Rows[i].Cells["student_id"].Value) == studentId)
                {
                    dtgObligation.Rows[i].Selected = true;
                    dtgObligation.CurrentCell = dtgObligation.Rows[i].Cells["ID Number"];
                    readSelectedStudent();
                    return;
                }
            }

            // The student is no longer in the list.
            clearSelectedStudent();
        }

        // Fills the summary cards. The cards follow the Department and Year Level filters.
        private void LoadSummaryCards()
        {
            int departmentId = Database.getSelectedId(cmbDepartmentObli);

            string yearLevel = "";
            if (cmbYearLevelObli.SelectedIndex > 0)
            {
                yearLevel = cmbYearLevelObli.Text;
            }

            MySqlConnection conn = Database.GetConnection();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "SELECT COUNT(*) AS total_students, " +
                    "COALESCE(SUM(absent_count > 0), 0) AS with_absence, " +
                    "COALESCE(SUM(absent_count = 0), 0) AS without_absence, " +
                    "COALESCE(SUM(balance), 0) AS unpaid_points " +
                    "FROM obligation_summary " +
                    "WHERE (@department = 0 OR department_id = @department) " +
                    "AND (@year = '' OR year_level = @year)", conn);
                cmd.Parameters.AddWithValue("@department", departmentId);
                cmd.Parameters.AddWithValue("@year", yearLevel);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblTotalStudents.Text = reader["total_students"].ToString();
                    lblWithAbsence.Text = reader["with_absence"].ToString();
                    lblWithoutAbsence.Text = reader["without_absence"].ToString();
                    lblUnpaidPoints.Text = reader["unpaid_points"].ToString();
                }

                reader.Close();
                cmd.Dispose();

                MySqlCommand eventCmd = new MySqlCommand("SELECT COUNT(*) FROM events", conn);
                lblTotalEvent.Text = eventCmd.ExecuteScalar().ToString();
                eventCmd.Dispose();
            }
            catch (Exception ex)
            {
                Database.showError("The obligation summary could not be loaded.", ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        // ------------------------------------------------------------------
        // Filters and selection
        // ------------------------------------------------------------------

        // Used by the department, year level, show and search filters.
        private void filter_Changed(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            LoadObligations();
            LoadSummaryCards();
        }

        private void btnShowAllRecord_Click(object sender, EventArgs e)
        {
            isLoading = true;

            if (cmbDepartmentObli.Items.Count > 0)
            {
                cmbDepartmentObli.SelectedIndex = 0;
            }

            cmbYearLevelObli.SelectedIndex = 0;
            cmbBalanceFilter.SelectedIndex = 0;
            txtSearchObli.Text = "";

            isLoading = false;

            LoadObligations();
            LoadSummaryCards();
        }

        private void dtgObligation_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            readSelectedStudent();
        }

        // Reads the selected row and shows the student above the action buttons.
        private void readSelectedStudent()
        {
            if (dtgObligation.CurrentRow == null || dtgObligation.SelectedRows.Count == 0)
            {
                clearSelectedStudent();
                return;
            }

            DataGridViewRow row = dtgObligation.CurrentRow;

            selectedStudentId = Convert.ToInt32(row.Cells["student_id"].Value);
            selectedStudentNumber = row.Cells["ID Number"].Value.ToString();
            selectedStudentName = row.Cells["Student Name"].Value.ToString();
            selectedBalance = Convert.ToInt32(row.Cells["Balance"].Value);

            lblSelectedStudent.Text = "Selected : " + selectedStudentName + " (" + selectedStudentNumber + ")  -  Balance: " + selectedBalance + " pts";

            if (selectedBalance > 0)
            {
                lblSelectedStudent.ForeColor = Color.Firebrick;
            }
            else
            {
                lblSelectedStudent.ForeColor = Color.DarkGreen;
            }
        }

        private void clearSelectedStudent()
        {
            selectedStudentId = 0;
            selectedStudentNumber = "";
            selectedStudentName = "";
            selectedBalance = 0;

            lblSelectedStudent.Text = "Select a student from the list.";
            lblSelectedStudent.ForeColor = Color.FromArgb(0, 0, 64);
        }

        // Checks that a student is selected before adding, deducting or clearing points.
        private bool isStudentSelected()
        {
            if (selectedStudentId == 0)
            {
                MessageBox.Show("Please select a student from the list first.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ------------------------------------------------------------------
        // Add, deduct and clear points
        // ------------------------------------------------------------------

        // Saves one adjustment. Positive points = added, negative points = deducted.
        private bool saveAdjustment(int points, string reason, string successMessage)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", selectedStudentId);
            parameters.Add("@points", points);
            parameters.Add("@reason", reason);

            // The account that made the change (empty when nobody is logged in).
            if (AccountInfo.Instantiate.currentUser.AccountId > 0)
            {
                parameters.Add("@account", AccountInfo.Instantiate.currentUser.AccountId);
            }
            else
            {
                parameters.Add("@account", DBNull.Value);
            }

            parameters.Add("@command", successMessage);

            bool saved = Database.saveUpdateDeleteData(
                "INSERT INTO obligation_adjustments (student_id, points, reason, account_id) " +
                "VALUES (@student, @points, @reason, @account)", parameters);

            if (saved == true)
            {
                txtReason.Text = "";
                nudPoints.Value = 10;
                LoadObligations();
                LoadSummaryCards();
            }

            return saved;
        }

        private void btnAddObligation_Click(object sender, EventArgs e)
        {
            if (isStudentSelected() == false)
            {
                return;
            }

            string reason = txtReason.Text.Trim();

            if (reason == "")
            {
                MessageBox.Show("Please enter the reason for adding points (example: Late submission of requirements).", "Reason Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReason.Focus();
                return;
            }

            int points = Convert.ToInt32(nudPoints.Value);

            DialogResult answer = MessageBox.Show("Add " + points + " point(s) to " + selectedStudentName + " (" + selectedStudentNumber + ")?\n\nReason: " + reason,
                "Add Points", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                saveAdjustment(points, reason, points + " point(s) were added to " + selectedStudentName + ".");
            }
        }

        private void btnDeductPoints_Click(object sender, EventArgs e)
        {
            if (isStudentSelected() == false)
            {
                return;
            }

            if (selectedBalance <= 0)
            {
                MessageBox.Show(selectedStudentName + " has no obligation balance to deduct.", "Nothing to Deduct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int points = Convert.ToInt32(nudPoints.Value);

            if (points > selectedBalance)
            {
                MessageBox.Show("You can only deduct up to " + selectedBalance + " point(s) for this student.", "Too Many Points", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPoints.Focus();
                return;
            }

            string reason = txtReason.Text.Trim();

            if (reason == "")
            {
                MessageBox.Show("Please enter the reason for deducting points (example: Paid at the SSC office).", "Reason Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReason.Focus();
                return;
            }

            DialogResult answer = MessageBox.Show("Deduct " + points + " point(s) from " + selectedStudentName + " (" + selectedStudentNumber + ")?\n\n" +
                "New balance: " + (selectedBalance - points) + " point(s)\nReason: " + reason,
                "Deduct Points", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                // Deducted points are saved as a negative number.
                saveAdjustment(-points, reason, points + " point(s) were deducted from " + selectedStudentName + ".");
            }
        }

        private void btnClearObligation_Click(object sender, EventArgs e)
        {
            if (isStudentSelected() == false)
            {
                return;
            }

            if (selectedBalance <= 0)
            {
                MessageBox.Show(selectedStudentName + " has no obligation balance. It is already 0.", "Already Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string reason = txtReason.Text.Trim();

            if (reason == "")
            {
                reason = "Obligation cleared";
            }

            DialogResult answer = MessageBox.Show("Clear the obligation of " + selectedStudentName + " (" + selectedStudentNumber + ")?\n\n" +
                "The balance of " + selectedBalance + " point(s) will become 0.\nReason: " + reason,
                "Clear Obligation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                // Deducting the whole balance makes it 0.
                saveAdjustment(-selectedBalance, reason, "The obligation of " + selectedStudentName + " is now cleared (0 points).");
            }
        }

        // ------------------------------------------------------------------
        // Details
        // ------------------------------------------------------------------

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (isStudentSelected() == false)
            {
                return;
            }

            openObligationDetails();
        }

        private void dtgObligation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // -1 is the header row.
            if (e.RowIndex < 0)
            {
                return;
            }

            readSelectedStudent();
            openObligationDetails();
        }

        // Shows the absences and the adjustment history of the selected student.
        private void openObligationDetails()
        {
            if (selectedStudentId == 0)
            {
                return;
            }

            ObligationDetails details = new ObligationDetails(selectedStudentId);
            details.ShowDialog();
            details.Dispose();

            // The Admin may have deleted an adjustment, so refresh the list.
            LoadObligations();
            LoadSummaryCards();
        }

        // ------------------------------------------------------------------
        // Closing and menu
        // ------------------------------------------------------------------

        private void Obligations_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.exitIfNoFormIsOpen(this);
        }

        private void btnObliLogout_Click(object sender, EventArgs e)
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

        private void btnObliDash_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnObliDept_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Departments") == false)
            {
                return;
            }

            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnObliStudent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        private void btnObliOfficer_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Officer Records") == false)
            {
                return;
            }

            OfficerRecords officerRecords = new OfficerRecords();
            officerRecords.Show();
            this.Close();
        }

        private void btnObliFace_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Face Recognition") == false)
            {
                return;
            }

            FaceRecognition faceRecognition = new FaceRecognition();
            faceRecognition.Show();
            this.Close();
        }

        private void btnObliEvent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Event Management") == false)
            {
                return;
            }

            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnObliAttendance_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Attendance Monitoring") == false)
            {
                return;
            }

            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnObliReport_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Reports and Analysis") == false)
            {
                return;
            }

            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        // This is the current page, so it only refreshes the list.
        private void btnObliObligation_Click(object sender, EventArgs e)
        {
            LoadObligations();
            LoadSummaryCards();
        }

        private void btnObliRules_Click(object sender, EventArgs e)
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
