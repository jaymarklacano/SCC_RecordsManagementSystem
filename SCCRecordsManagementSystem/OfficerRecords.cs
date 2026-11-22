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
    public partial class OfficerRecords : Form
    {
        // true while the filters are being filled (so the change events do nothing).
        private bool isLoading = true;

        public OfficerRecords()
        {
            InitializeComponent();
        }

        private void OfficerRecords_Load(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                Database.loadDepartmentComboBox(cmbDepartmentOfficer, "All Courses");
                loadPositionComboBox();
                loadSchoolYearComboBox();
                txtSearchOfficer.Text = "";
            }
            finally
            {
                isLoading = false;
            }

            // Only the Admin can add, edit or delete officers. Officers can only view the list.
            if (AccountInfo.Instantiate.currentUser.Role == "Admin")
            {
                btnAddOfficer.Enabled = true;
                btnEditOfficer.Enabled = true;
                btnDeleteOfficer.Enabled = true;
            }
            else
            {
                btnAddOfficer.Enabled = false;
                btnEditOfficer.Enabled = false;
                btnDeleteOfficer.Enabled = false;
            }

            LoadOfficers();
        }

        // ------------------------------------------------------------------
        // Filters
        // ------------------------------------------------------------------

        // Fills the Position filter with the positions that are saved in the database.
        private void loadPositionComboBox()
        {
            DataTable positionTable = Database.GetData("SELECT DISTINCT position FROM officers ORDER BY position", null);

            cmbPositionOfficer.Items.Clear();
            cmbPositionOfficer.Items.Add("All Positions");

            for (int i = 0; i < positionTable.Rows.Count; i++)
            {
                cmbPositionOfficer.Items.Add(positionTable.Rows[i]["position"].ToString());
            }

            cmbPositionOfficer.SelectedIndex = 0;
        }

        // Fills the School Year filter with the school years that are saved in the database.
        private void loadSchoolYearComboBox()
        {
            DataTable schoolYearTable = Database.GetData("SELECT DISTINCT school_year FROM officers ORDER BY school_year DESC", null);

            cmbSchoolYearOfficer.Items.Clear();
            cmbSchoolYearOfficer.Items.Add("All School Years");

            for (int i = 0; i < schoolYearTable.Rows.Count; i++)
            {
                cmbSchoolYearOfficer.Items.Add(schoolYearTable.Rows[i]["school_year"].ToString());
            }

            cmbSchoolYearOfficer.SelectedIndex = 0;
        }

        // Used by the course, position, school year and search filters.
        private void filter_Changed(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            LoadOfficers();
        }

        private void btnShowAllOfficers_Click(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                if (cmbDepartmentOfficer.Items.Count > 0)
                {
                    cmbDepartmentOfficer.SelectedIndex = 0;
                }

                // Fill the filters again because a new position or school year may have been added.
                loadPositionComboBox();
                loadSchoolYearComboBox();
                txtSearchOfficer.Text = "";
            }
            finally
            {
                isLoading = false;
            }

            LoadOfficers();
        }

        // ------------------------------------------------------------------
        // Loading the list
        // ------------------------------------------------------------------

        private void LoadOfficers()
        {
            int departmentId = Database.getSelectedId(cmbDepartmentOfficer);

            string position = "";
            if (cmbPositionOfficer.SelectedIndex > 0)
            {
                position = cmbPositionOfficer.Text;
            }

            string schoolYear = "";
            if (cmbSchoolYearOfficer.SelectedIndex > 0)
            {
                schoolYear = cmbSchoolYearOfficer.Text;
            }

            string query = "SELECT o.officer_id, o.account_id, o.officer_number AS 'ID Number', " +
                           "CONCAT(o.last_name, ', ', o.first_name, IF(o.middle_name IS NULL OR o.middle_name = '', '', CONCAT(' ', LEFT(o.middle_name, 1), '.'))) AS 'Officer Name', " +
                           "d.department_code AS 'Course', o.year_level AS 'Year Level', o.position AS 'Position', " +
                           "o.school_year AS 'School Year', a.username AS 'Username', " +
                           "COALESCE(o.contact_number, '-') AS 'Contact Number', " +
                           "COALESCE(DATE_FORMAT((SELECT MAX(lh.logged_in) FROM login_history lh WHERE lh.account_id = o.account_id), '%b %d, %Y %h:%i %p'), 'Never') AS 'Last Login' " +
                           "FROM officers o " +
                           "JOIN accounts a ON a.account_id = o.account_id " +
                           "JOIN departments d ON d.department_id = o.department_id " +
                           "WHERE (@department = 0 OR o.department_id = @department) " +
                           "AND (@position = '' OR o.position = @position) " +
                           "AND (@schoolYear = '' OR o.school_year = @schoolYear) " +
                           "AND (o.officer_number LIKE @search OR o.first_name LIKE @search OR o.last_name LIKE @search " +
                           "OR CONCAT(o.first_name, ' ', o.last_name) LIKE @search OR a.username LIKE @search OR o.position LIKE @search) " +
                           "ORDER BY o.school_year DESC, o.last_name, o.first_name";

            MySqlConnection conn = null;
            DataTable officerTable = new DataTable();
            int totalOfficers = 0;

            try
            {
                conn = Database.GetConnection();
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@department", departmentId);
                cmd.Parameters.AddWithValue("@position", position);
                cmd.Parameters.AddWithValue("@schoolYear", schoolYear);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearchOfficer.Text.Trim() + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(officerTable);

                adapter.Dispose();
                cmd.Dispose();

                MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) FROM officers", conn);
                totalOfficers = Convert.ToInt32(countCmd.ExecuteScalar());
                countCmd.Dispose();
            }
            catch (Exception ex)
            {
                dtgOfficer.DataSource = null;
                lblNoRecord.Visible = false;
                lblTotalOfficers.Text = "-";
                lblListInfo.Text = "Officer list unavailable.";
                Database.showError("The officer list could not be loaded.", ex);
                return;
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }

            dtgOfficer.DataSource = officerTable;

            if (dtgOfficer.Columns.Contains("officer_id"))
            {
                dtgOfficer.Columns["officer_id"].Visible = false;
                dtgOfficer.Columns["account_id"].Visible = false;
                dtgOfficer.Columns["Officer Name"].FillWeight = 160;
                dtgOfficer.Columns["Last Login"].FillWeight = 140;
            }

            dtgOfficer.ClearSelection();

            lblTotalOfficers.Text = totalOfficers.ToString();

            string listInfo = "Showing " + officerTable.Rows.Count + " of " + totalOfficers + " officer(s).";
            if (AccountInfo.Instantiate.currentUser.Role != "Admin")
            {
                listInfo = listInfo + "   View only: only the Admin can add, edit or delete officers.";
            }
            else
            {
                listInfo = listInfo + "   Double-click a row to edit the officer.";
            }
            lblListInfo.Text = listInfo;

            if (officerTable.Rows.Count == 0)
            {
                if (totalOfficers == 0)
                {
                    lblNoRecord.Text = "No officers yet. Click \"Add Officer\" to create one.";
                }
                else
                {
                    lblNoRecord.Text = "No officers found. Try changing the filters or the search text.";
                }

                lblNoRecord.Visible = true;
                lblNoRecord.BringToFront();
            }
            else
            {
                lblNoRecord.Visible = false;
            }
        }

        // Returns the officer_id of the selected row (0 when nothing is selected).
        private int getSelectedOfficerId()
        {
            if (dtgOfficer.SelectedRows.Count == 0)
            {
                return 0;
            }

            return Convert.ToInt32(dtgOfficer.SelectedRows[0].Cells["officer_id"].Value);
        }

        // ------------------------------------------------------------------
        // Add, edit and delete (Admin only)
        // ------------------------------------------------------------------

        private bool isAdmin()
        {
            if (AccountInfo.Instantiate.currentUser.Role != "Admin")
            {
                MessageBox.Show("Only the Admin can add, edit or delete officers.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnAddOfficer_Click(object sender, EventArgs e)
        {
            if (isAdmin() == false)
            {
                return;
            }

            AddOfficer addOfficer = new AddOfficer();
            DialogResult result = addOfficer.ShowDialog();
            addOfficer.Dispose();

            if (result == DialogResult.OK)
            {
                refreshFiltersAndList();
            }
        }

        private void btnEditOfficer_Click(object sender, EventArgs e)
        {
            if (isAdmin() == false)
            {
                return;
            }

            int officerId = getSelectedOfficerId();

            if (officerId == 0)
            {
                MessageBox.Show("Please select an officer from the list first.", "No Officer Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            openEditOfficer(officerId);
        }

        private void openEditOfficer(int officerId)
        {
            AddOfficer editOfficer = new AddOfficer(officerId);
            DialogResult result = editOfficer.ShowDialog();
            editOfficer.Dispose();

            if (result == DialogResult.OK)
            {
                refreshFiltersAndList();
            }
        }

        // Fills the filters again (a new position or school year may exist) but keeps the selected values.
        private void refreshFiltersAndList()
        {
            string selectedPosition = cmbPositionOfficer.Text;
            string selectedSchoolYear = cmbSchoolYearOfficer.Text;

            isLoading = true;
            try
            {
                loadPositionComboBox();
                loadSchoolYearComboBox();

                if (cmbPositionOfficer.Items.Contains(selectedPosition))
                {
                    cmbPositionOfficer.SelectedItem = selectedPosition;
                }

                if (cmbSchoolYearOfficer.Items.Contains(selectedSchoolYear))
                {
                    cmbSchoolYearOfficer.SelectedItem = selectedSchoolYear;
                }
            }
            finally
            {
                isLoading = false;
            }

            LoadOfficers();
        }

        private void btnDeleteOfficer_Click(object sender, EventArgs e)
        {
            if (isAdmin() == false)
            {
                return;
            }

            if (dtgOfficer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an officer from the list first.", "No Officer Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dtgOfficer.SelectedRows[0];
            int accountId = Convert.ToInt32(row.Cells["account_id"].Value);
            string officerName = row.Cells["Officer Name"].Value.ToString();
            string position = row.Cells["Position"].Value.ToString();
            string username = row.Cells["Username"].Value.ToString();

            if (accountId == AccountInfo.Instantiate.currentUser.AccountId)
            {
                MessageBox.Show("You cannot delete the account that is currently logged in.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult answer = MessageBox.Show("Are you sure you want to delete this officer?\n\n" +
                "Name: " + officerName + "\nPosition: " + position + "\nUsername: " + username +
                "\n\nThe login account of the officer will also be deleted, so they can no longer log in.",
                "Delete Officer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            // Deleting the account also deletes the officer row (ON DELETE CASCADE in the database).
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@account", accountId);
            parameters.Add("@command", "delete");

            bool deleted = Database.saveUpdateDeleteData("DELETE FROM accounts WHERE account_id = @account AND role = 'Officer'", parameters);

            if (deleted == true)
            {
                refreshFiltersAndList();
            }
        }

        // Admin: opens the edit window. Officer: shows the details of the officer.
        private void dtgOfficer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // -1 is the header row.
            if (e.RowIndex < 0)
            {
                return;
            }

            int officerId = Convert.ToInt32(dtgOfficer.Rows[e.RowIndex].Cells["officer_id"].Value);

            if (AccountInfo.Instantiate.currentUser.Role == "Admin")
            {
                openEditOfficer(officerId);
                return;
            }

            OfficerInfo officer = Database.searchAndGetOfficerInfo(officerId);

            if (officer == null)
            {
                MessageBox.Show("This officer no longer exists.", "Officer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadOfficers();
                return;
            }

            string email = officer.Email;
            if (email == "")
            {
                email = "-";
            }

            string contactNumber = officer.ContactNumber;
            if (contactNumber == "")
            {
                contactNumber = "-";
            }

            MessageBox.Show("ID Number: " + officer.OfficerNumber +
                "\nName: " + officer.GetFullName() +
                "\nCourse: " + officer.DepartmentCode +
                "\nYear Level: " + officer.YearLevel +
                "\nGender: " + officer.Gender +
                "\nPosition: " + officer.Position +
                "\nSchool Year: " + officer.SchoolYear +
                "\nEmail: " + email +
                "\nContact Number: " + contactNumber +
                "\nUsername: " + officer.Username,
                "Officer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ------------------------------------------------------------------
        // Closing and menu
        // ------------------------------------------------------------------

        private void OfficerRecords_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.exitIfNoFormIsOpen(this);
        }

        private void btnOffLogout_Click(object sender, EventArgs e)
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

        private void btnOffDash_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnOffDept_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Departments") == false)
            {
                return;
            }

            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnOffStudent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        // This is the current page, so it only refreshes the list.
        private void btnOffOfficer_Click(object sender, EventArgs e)
        {
            refreshFiltersAndList();
        }

        private void btnOffFace_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Face Recognition") == false)
            {
                return;
            }

            FaceRecognition faceRecognition = new FaceRecognition();
            faceRecognition.Show();
            this.Close();
        }

        private void btnOffEvent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Event Management") == false)
            {
                return;
            }

            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnOffAttendance_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Attendance Monitoring") == false)
            {
                return;
            }

            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnOffReport_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Reports and Analysis") == false)
            {
                return;
            }

            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        private void btnOffObligation_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Obligations") == false)
            {
                return;
            }

            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        private void btnOffRules_Click(object sender, EventArgs e)
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
