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
    public partial class StudentRecords : Form
    {
        // The id of the student selected in the table (0 = nothing selected).
        private int selectedStudentId = 0;

        // true while the ComboBoxes or the table are being filled.
        private bool isLoading = true;

        // Used when another page (Departments) opens this page for one department only.
        private int departmentIdFromOtherPage = 0;

        public StudentRecords()
        {
            InitializeComponent();
        }

        public StudentRecords(int departmentId)
        {
            InitializeComponent();
            departmentIdFromOtherPage = departmentId;
        }

        private void StudentRecords_Load(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                Database.loadDepartmentComboBox(cmbDepartment, "All Departments");

                if (departmentIdFromOtherPage > 0)
                {
                    cmbDepartment.SelectedValue = departmentIdFromOtherPage;
                }

                cmbYear.SelectedIndex = 0;
                cmbFace.SelectedIndex = 0;
            }
            finally
            {
                isLoading = false;
            }

            LoadStudents();
            txtSearch.Focus();
        }

        public void LoadStudents()
        {
            isLoading = true;
            try
            {
                // Remember the selected student so we can select it again after refreshing.
                int studentIdToSelect = selectedStudentId;

                string search = txtSearch.Text.Trim();
                int departmentId = Database.getSelectedId(cmbDepartment);

                string query = "SELECT s.student_id, s.student_number, " +
                               "CONCAT(s.last_name, ', ', s.first_name, IF(s.middle_name IS NULL OR s.middle_name = '', '', CONCAT(' ', LEFT(s.middle_name, 1), '.'))) AS full_name, " +
                               "d.department_code, s.year_level, s.gender, s.status, " +
                               "CASE WHEN s.face_registered = 1 THEN 'Registered' ELSE 'Not Registered' END AS face_status " +
                               "FROM students s " +
                               "JOIN departments d ON d.department_id = s.department_id " +
                               "WHERE (s.student_number LIKE @search OR s.first_name LIKE @search OR s.last_name LIKE @search " +
                               "OR CONCAT(s.first_name, ' ', s.last_name) LIKE @search) ";

                // The filters are only added when something is selected.
                if (departmentId > 0)
                {
                    query = query + "AND s.department_id = @departmentId ";
                }

                if (cmbYear.SelectedIndex > 0)
                {
                    query = query + "AND s.year_level = @yearLevel ";
                }

                if (cmbFace.SelectedIndex == 1)
                {
                    query = query + "AND s.face_registered = 1 ";
                }
                else if (cmbFace.SelectedIndex == 2)
                {
                    query = query + "AND s.face_registered = 0 ";
                }

                query = query + "ORDER BY s.last_name, s.first_name";

                DataTable dt = new DataTable();

                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                        cmd.Parameters.AddWithValue("@departmentId", departmentId);

                        if (cmbYear.SelectedIndex > 0)
                        {
                            cmd.Parameters.AddWithValue("@yearLevel", cmbYear.SelectedItem.ToString());
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                dgvStudents.DataSource = dt;

                if (dgvStudents.Columns["student_id"] != null)
                {
                    dgvStudents.Columns["student_id"].Visible = false;
                    dgvStudents.Columns["student_number"].HeaderText = "Student ID";
                    dgvStudents.Columns["full_name"].HeaderText = "Full Name";
                    dgvStudents.Columns["full_name"].FillWeight = 180;
                    dgvStudents.Columns["department_code"].HeaderText = "Course";
                    dgvStudents.Columns["year_level"].HeaderText = "Year";
                    dgvStudents.Columns["gender"].HeaderText = "Gender";
                    dgvStudents.Columns["status"].HeaderText = "Status";
                    dgvStudents.Columns["face_status"].HeaderText = "Face";
                    dgvStudents.Columns["face_status"].FillWeight = 110;
                }

                lblTotalStudents.Text = "Total: " + dt.Rows.Count + " student(s)";

                // Empty-state message
                if (dt.Rows.Count == 0)
                {
                    lblEmpty.Text = "No students found.\nClick \"ADD STUDENT\" to register a student, or change the search and filters.";
                    lblEmpty.Visible = true;
                    lblEmpty.BringToFront();
                }
                else
                {
                    lblEmpty.Visible = false;
                }

                // Select the same student again (if he/she is still in the list).
                dgvStudents.ClearSelection();

                foreach (DataGridViewRow row in dgvStudents.Rows)
                {
                    if (Convert.ToInt32(row.Cells["student_id"].Value) == studentIdToSelect)
                    {
                        row.Selected = true;
                        dgvStudents.CurrentCell = row.Cells["student_number"];
                        break;
                    }
                }

                if (dgvStudents.SelectedRows.Count == 0 && dgvStudents.Rows.Count > 0)
                {
                    dgvStudents.Rows[0].Selected = true;
                    dgvStudents.CurrentCell = dgvStudents.Rows[0].Cells["student_number"];
                }
            }
            catch (Exception ex)
            {
                dgvStudents.DataSource = null;
                clearStudentDetails();
                lblEmpty.Visible = false;
                lblTotalStudents.Text = "Student list unavailable.";
                Database.showError("Could not load the students.", ex);
                return;
            }
            finally
            {
                isLoading = false;
            }
            showSelectedStudent();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            LoadStudents();
        }

        // One event handler for the 3 filter ComboBoxes (Department, Year and Face).
        private void filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            LoadStudents();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                txtSearch.Clear();

                if (cmbDepartment.Items.Count > 0)
                {
                    cmbDepartment.SelectedIndex = 0;
                }

                cmbYear.SelectedIndex = 0;
                cmbFace.SelectedIndex = 0;
            }
            finally
            {
                isLoading = false;
            }

            LoadStudents();
            txtSearch.Focus();
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            showSelectedStudent();
        }

        private void dgvStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Double clicking a student opens the edit page.
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e);
            }
        }

        private void clearStudentDetails()
        {
            selectedStudentId = 0;

            lblNumber.Text = "-";
            lblName.Text = "-";
            lblCourse.Text = "-";
            lblYear.Text = "-";
            lblGender.Text = "-";
            lblEmail.Text = "-";
            lblContact.Text = "-";
            lblStatus.Text = "-";
            lblStatus.ForeColor = Color.Black;

            lblPhotoName.Text = "Select a student";
            lblPhotoInfo.Text = "";
            lblAttendanceSummary.Text = "";

            showPhoto(null);

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void showSelectedStudent()
        {
            if (dgvStudents.CurrentRow == null || dgvStudents.SelectedRows.Count == 0)
            {
                clearStudentDetails();
                return;
            }

            int studentId = Convert.ToInt32(dgvStudents.CurrentRow.Cells["student_id"].Value);

            // Database.searchAndGetStudentInfo returns a StudentInfo object (like PersonalInfo in the sample code).
            StudentInfo student;
            try
            {
                student = Database.searchAndGetStudentInfo(studentId);
            }
            catch (MySqlException ex)
            {
                clearStudentDetails();
                Database.showError("Could not load the selected student.", ex);
                return;
            }

            if (student == null)
            {
                clearStudentDetails();
                return;
            }

            selectedStudentId = student.StudentId;

            lblNumber.Text = student.StudentNumber;
            lblName.Text = student.GetFullName();
            lblCourse.Text = student.DepartmentCode;
            lblYear.Text = student.YearLevel;
            lblGender.Text = student.Gender;

            if (student.Email == "")
            {
                lblEmail.Text = "(none)";
            }
            else
            {
                lblEmail.Text = student.Email;
            }

            if (student.ContactNumber == "")
            {
                lblContact.Text = "(none)";
            }
            else
            {
                lblContact.Text = student.ContactNumber;
            }

            lblStatus.Text = student.Status;

            if (student.Status == "Active")
            {
                lblStatus.ForeColor = Color.DarkGreen;
            }
            else
            {
                lblStatus.ForeColor = Color.DarkRed;
            }

            lblPhotoName.Text = student.GetFullName();

            if (student.FaceRegistered == true)
            {
                lblPhotoInfo.Text = "Face registered (" + student.FaceImageCount + " of 5 pictures)";
                lblPhotoInfo.ForeColor = Color.DarkGreen;
            }
            else
            {
                lblPhotoInfo.Text = "Face NOT registered (" + student.FaceImageCount + " of 5)";
                lblPhotoInfo.ForeColor = Color.DarkRed;
            }

            loadStudentPhoto(student.StudentId);
            loadAttendanceSummary(student.StudentId);

            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }

        // Shows the first registered face picture of the student.
        private void loadStudentPhoto(int studentId)
        {
            Image photo = null;

            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT image_data FROM face_images WHERE student_id = @id ORDER BY image_number LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", studentId);
                        object value = cmd.ExecuteScalar();

                        if (value != null && value != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])value;

                            // Image.FromStream needs the stream to stay open,
                            // so we make a copy (new Bitmap) and close the stream right away.
                            using (MemoryStream stream = new MemoryStream(imageBytes))
                            {
                                using (Image temporaryImage = Image.FromStream(stream))
                                {
                                    photo = new Bitmap(temporaryImage);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // When the picture cannot be loaded we simply show no picture.
                photo = null;
            }

            showPhoto(photo);
        }

        private void showPhoto(Image photo)
        {
            // Free the memory of the old picture before showing the new one.
            if (picPhoto.Image != null)
            {
                Image oldPhoto = picPhoto.Image;
                picPhoto.Image = null;
                oldPhoto.Dispose();
            }

            picPhoto.Image = photo;
        }

        private void loadAttendanceSummary(int studentId)
        {
            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // attendance_records and obligation_summary are VIEWS in the database.
                    string query = "SELECT " +
                                   "(SELECT COUNT(*) FROM attendance_records WHERE student_id = @id AND status = 'Present') AS present_count, " +
                                   "(SELECT COUNT(*) FROM attendance_records WHERE student_id = @id AND status = 'Absent') AS absent_count, " +
                                   "(SELECT balance FROM obligation_summary WHERE student_id = @id) AS balance";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", studentId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string balance = "0";

                                if (reader["balance"] != DBNull.Value)
                                {
                                    balance = reader["balance"].ToString();
                                }

                                lblAttendanceSummary.Text = "Present: " + reader["present_count"].ToString() +
                                                            "   Absent: " + reader["absent_count"].ToString() +
                                                            "\nObligation Balance: " + balance + " pts";
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                lblAttendanceSummary.Text = "Attendance summary is not available.";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Registration() without an id means "add a new student".
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedStudentId == 0)
            {
                MessageBox.Show("Please select a student to edit.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Registration(id) means "edit this student".
            Registration registration = new Registration(selectedStudentId);
            registration.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedStudentId == 0)
            {
                MessageBox.Show("Please select a student to delete.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string studentName = lblName.Text;
            string studentNumber = lblNumber.Text;

            DialogResult answer = MessageBox.Show("Are you sure you want to delete this student?\n\n" +
                                                  "Student ID: " + studentNumber + "\n" +
                                                  "Name: " + studentName + "\n\n" +
                                                  "Their face pictures, attendance records and obligation records will also be deleted.\n" +
                                                  "This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // The face_images, event_students, event_attendance and obligation_adjustments rows
                    // are deleted automatically by the database (ON DELETE CASCADE).
                    string query = "DELETE FROM students WHERE student_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedStudentId);
                        int rowsDeleted = cmd.ExecuteNonQuery();

                        if (rowsDeleted > 0)
                        {
                            MessageBox.Show("Student deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The student was not found. It may have been deleted already.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Database.showError("The student was not deleted.", ex);
            }

            selectedStudentId = 0;
            LoadStudents();
        }

        private void btnStudentLogout_Click(object sender, EventArgs e)
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

        private void btnStudentDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnStudentDepartment_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Departments") == false)
            {
                return;
            }

            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnStudentRec_Click(object sender, EventArgs e)
        {
            // We are already in Student Records, so we only refresh the table.
            LoadStudents();
        }

        private void btnStudentOfficer_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Officer Records") == false)
            {
                return;
            }

            OfficerRecords officerRecords = new OfficerRecords();
            officerRecords.Show();
            this.Close();
        }

        private void btnStudentFace_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Face Recognition") == false)
            {
                return;
            }

            FaceRecognition faceRecognition = new FaceRecognition();
            faceRecognition.Show();
            this.Close();
        }

        private void btnStudentEvent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Event Management") == false)
            {
                return;
            }

            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnStudentAttendance_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Attendance Monitoring") == false)
            {
                return;
            }

            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnStudentReports_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Reports and Analysis") == false)
            {
                return;
            }

            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        private void btnStudentObligation_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Obligations") == false)
            {
                return;
            }

            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        private void btnStudentRules_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Rules and Permission") == false)
            {
                return;
            }

            RulesPermission rulesPermission = new RulesPermission();
            rulesPermission.Show();
            this.Close();
        }

        private void StudentRecords_FormClosed(object sender, FormClosedEventArgs e)
        {
            showPhoto(null);
            Database.exitIfNoFormIsOpen(this);
        }
    }
}
