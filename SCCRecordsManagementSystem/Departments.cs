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
    public partial class Departments : Form
    {
        // The id of the department that is selected in the table (0 = nothing selected).
        private int selectedDepartmentId = 0;

        // true while the table is being filled, so SelectionChanged does not run many times.
        private bool isLoading = false;

        public Departments()
        {
            InitializeComponent();
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        public void LoadDepartments()
        {
            isLoading = true;
            string search = txtSearchDepartment.Text.Trim();

            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT d.department_id, d.department_name, d.department_code, d.department_head, d.contact_email, d.academic_level, " +
                                   "(SELECT COUNT(*) FROM students s WHERE s.department_id = d.department_id) AS student_count " +
                                   "FROM departments d " +
                                   "WHERE d.department_name LIKE @search OR d.department_code LIKE @search OR d.department_head LIKE @search " +
                                   "ORDER BY d.department_code";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // The % signs mean "anything before or after the search text".
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dtgDepartment.DataSource = dt;

                            lblTotalDepartments.Text = "Total: " + dt.Rows.Count + " department(s)";

                            // Empty-state message
                            if (dt.Rows.Count == 0)
                            {
                                if (search == "")
                                {
                                    lblNoDepartment.Text = "No departments yet. Click \"Add Department\" to add one.";
                                }
                                else
                                {
                                    lblNoDepartment.Text = "No department matches \"" + search + "\".";
                                }

                                lblNoDepartment.Visible = true;
                                lblNoDepartment.BringToFront();
                            }
                            else
                            {
                                lblNoDepartment.Visible = false;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Database.showError("Could not load the departments.", ex);
                }
            }

            isLoading = false;
            showSelectedDepartmentDetails();
        }

        private void txtSearchDepartment_TextChanged(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchDepartment.Clear();
            txtSearchDepartment.Focus();
        }

        private void dtgDepartment_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            showSelectedDepartmentDetails();
        }

        private void clearDepartmentDetails()
        {
            selectedDepartmentId = 0;
            lblDetailName.Text = "Name: -";
            lblDetailCode.Text = "Code: -";
            lblDetailHead.Text = "Head / Dean: -";
            lblDetailEmail.Text = "Email: -";
            lblDetailLevel.Text = "Academic Level: -";
            lblDetailDescription.Text = "Description: -";
            lblDetailCreated.Text = "Date Added: -";
            lblDetailStudents.Text = "Students: 0";
            lblDetailOfficers.Text = "Officers: 0";
            lblDetailEvents.Text = "Events: 0";
            btnViewStudents.Enabled = false;
        }

        private void showSelectedDepartmentDetails()
        {
            if (dtgDepartment.CurrentRow == null)
            {
                clearDepartmentDetails();
                return;
            }

            int departmentId = Convert.ToInt32(dtgDepartment.CurrentRow.Cells["colDepartmentId"].Value);

            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT d.*, " +
                                   "(SELECT COUNT(*) FROM students s WHERE s.department_id = d.department_id) AS student_count, " +
                                   "(SELECT COUNT(*) FROM officers o WHERE o.department_id = d.department_id) AS officer_count, " +
                                   "(SELECT COUNT(*) FROM events e WHERE e.department_id = d.department_id) AS event_count " +
                                   "FROM departments d WHERE d.department_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", departmentId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedDepartmentId = departmentId;

                                string email = reader["contact_email"].ToString();
                                string description = reader["description"].ToString();

                                if (email == "")
                                {
                                    email = "(none)";
                                }

                                if (description == "")
                                {
                                    description = "(none)";
                                }

                                lblDetailName.Text = "Name: " + reader["department_name"].ToString();
                                lblDetailCode.Text = "Code: " + reader["department_code"].ToString();
                                lblDetailHead.Text = "Head / Dean: " + reader["department_head"].ToString();
                                lblDetailEmail.Text = "Email: " + email;
                                lblDetailLevel.Text = "Academic Level: " + reader["academic_level"].ToString();
                                lblDetailDescription.Text = "Description: " + description;
                                lblDetailCreated.Text = "Date Added: " + Convert.ToDateTime(reader["created_at"]).ToString("MMM dd, yyyy");
                                lblDetailStudents.Text = "Students: " + reader["student_count"].ToString();
                                lblDetailOfficers.Text = "Officers: " + reader["officer_count"].ToString();
                                lblDetailEvents.Text = "Department Events: " + reader["event_count"].ToString();
                                btnViewStudents.Enabled = true;
                            }
                            else
                            {
                                clearDepartmentDetails();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    clearDepartmentDetails();
                    Database.showError("Could not load the department details.", ex);
                }
            }
        }

        private void dtgDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            int departmentId = Convert.ToInt32(dtgDepartment.Rows[e.RowIndex].Cells["colDepartmentId"].Value);
            string departmentCode = dtgDepartment.Rows[e.RowIndex].Cells["colDepartmentCode"].Value.ToString();

            if (dtgDepartment.Columns[e.ColumnIndex].Name == "colEditDepartment")
            {
                // Open AddDepartmentForm in edit mode
                AddDepartment editForm = new AddDepartment(departmentId);
                editForm.ShowDialog();
                LoadDepartments(); // Refresh grid after editing
            }
            else if (dtgDepartment.Columns[e.ColumnIndex].Name == "colDeleteDepartment")
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to delete the department \"" + departmentCode + "\"?\n\nThis cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    DeleteDepartment(departmentId, departmentCode);
                }
            }
        }

        private void DeleteDepartment(int departmentId, string departmentCode)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();

                    // 1. A department that still has students, officers or events cannot be deleted.
                    string countQuery = "SELECT " +
                                        "(SELECT COUNT(*) FROM students WHERE department_id = @id) AS student_count, " +
                                        "(SELECT COUNT(*) FROM officers WHERE department_id = @id) AS officer_count, " +
                                        "(SELECT COUNT(*) FROM events WHERE department_id = @id) AS event_count";

                    int studentCount = 0;
                    int officerCount = 0;
                    int eventCount = 0;

                    using (MySqlCommand cmd = new MySqlCommand(countQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", departmentId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentCount = Convert.ToInt32(reader["student_count"]);
                                officerCount = Convert.ToInt32(reader["officer_count"]);
                                eventCount = Convert.ToInt32(reader["event_count"]);
                            }
                        }
                    }

                    if (studentCount > 0 || officerCount > 0 || eventCount > 0)
                    {
                        MessageBox.Show("The department \"" + departmentCode + "\" cannot be deleted because it is still used by:\n\n" +
                                        studentCount + " student(s)\n" +
                                        officerCount + " officer(s)\n" +
                                        eventCount + " event(s)\n\n" +
                                        "Move or delete those records first.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 2. Delete the department
                    string query = "DELETE FROM departments WHERE department_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", departmentId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Department deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Database.showError("The department was not deleted.", ex);
                }
            }

            LoadDepartments();
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            AddDepartment addDepartment = new AddDepartment();
            addDepartment.ShowDialog();

            // Refresh grid right after the add form closes
            LoadDepartments();
        }

        private void btnViewStudents_Click(object sender, EventArgs e)
        {
            if (selectedDepartmentId == 0)
            {
                MessageBox.Show("Please select a department first.", "No Department Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            // Opens the Student Records page that shows only the students of this department.
            StudentRecords studentRecords = new StudentRecords(selectedDepartmentId);
            studentRecords.Show();
            this.Close();
        }

        private void btnDeptLogout_Click(object sender, EventArgs e)
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

        private void btnDeptDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnDeptDepartments_Click(object sender, EventArgs e)
        {
            // We are already in Departments, so we only refresh the table.
            LoadDepartments();
        }

        private void btnDeptStudentRecords_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        private void btnDeptOfficer_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Officer Records") == false)
            {
                return;
            }

            OfficerRecords officerRecords = new OfficerRecords();
            officerRecords.Show();
            this.Close();
        }

        private void btnDeptFaceRecognition_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Face Recognition") == false)
            {
                return;
            }

            FaceRecognition faceRecognition = new FaceRecognition();
            faceRecognition.Show();
            this.Close();
        }

        private void btnDeptEvent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Event Management") == false)
            {
                return;
            }

            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnDeptAttendance_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Attendance Monitoring") == false)
            {
                return;
            }

            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnDeptReport_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Reports and Analysis") == false)
            {
                return;
            }

            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        private void btnDeptObligation_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Obligations") == false)
            {
                return;
            }

            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        private void btnDeptRules_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Rules and Permission") == false)
            {
                return;
            }

            RulesPermission rulesPermission = new RulesPermission();
            rulesPermission.Show();
            this.Close();
        }

        private void Departments_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.exitIfNoFormIsOpen(this);
        }
    }
}
