using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Student list with search and filters, a details panel and the Add / Edit / Delete buttons.
    public partial class StudentRecords : Form
    {
        // True while the form fills the ComboBoxes, so the filters do not run too early.
        private bool isLoading = true;

        public StudentRecords()
        {
            InitializeComponent();
            NavigationHelper.ExitWhenClosed(this);
        }

        private void StudentRecords_Load(object sender, EventArgs e)
        {
            try
            {
                // First item of the course filter means "no filter"
                DataTable departments = DepartmentService.GetAll();
                DataTable courseChoices = new DataTable();
                courseChoices.Columns.Add("department_id", typeof(int));
                courseChoices.Columns.Add("department_code", typeof(string));
                courseChoices.Rows.Add(0, "All Courses");
                foreach (DataRow row in departments.Rows)
                {
                    courseChoices.Rows.Add(row["department_id"], row["department_code"]);
                }

                cmbDepartment.DataSource = courseChoices;
                cmbDepartment.ValueMember = "department_id";
                cmbDepartment.DisplayMember = "department_code";
            }
            catch (Exception ex)
            {
                lblEmpty.Text = Database.GetFriendlyMessage(ex);
                lblEmpty.Visible = true;
            }

            cmbYear.SelectedIndex = 0;
            cmbFace.SelectedIndex = 0;
            isLoading = false;

            LoadStudents();
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                LoadStudents();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            isLoading = true;
            txtSearch.Clear();
            if (cmbDepartment.Items.Count > 0)
            {
                cmbDepartment.SelectedIndex = 0;
            }
            cmbYear.SelectedIndex = 0;
            cmbFace.SelectedIndex = 0;
            isLoading = false;

            LoadStudents();
        }

        // ---------- Add / Edit / Delete ----------

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenPage(this, new Registration());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedStudent();
        }

        private void dgvStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedStudent();
            }
        }

        private void EditSelectedStudent()
        {
            int studentId = GetSelectedStudentId();
            if (studentId == 0)
            {
                MessageBox.Show("Please select a student from the list first.", "Edit Student",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            NavigationHelper.OpenPage(this, new Registration(studentId));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int studentId = GetSelectedStudentId();
            if (studentId == 0)
            {
                MessageBox.Show("Please select a student from the list first.", "Delete Student",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string studentNumber = dgvStudents.CurrentRow.Cells["colNumber"].Value.ToString();
            string fullName = dgvStudents.CurrentRow.Cells["colName"].Value.ToString();

            try
            {
                int attendanceCount = AttendanceService.CountForStudent(studentId);

                DialogResult answer = MessageBox.Show(
                    "Are you sure you want to delete this student?\n\n" +
                    studentNumber + " - " + fullName + "\n\n" +
                    "The registered face pictures and " + attendanceCount + " attendance record(s) of this student will also be deleted. " +
                    "This cannot be undone.",
                    "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (answer != DialogResult.Yes)
                {
                    return;
                }

                StudentService.DeleteStudent(studentId);
                MessageBox.Show("The student was deleted.", "Delete Student",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Delete Student",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Showing the students ----------

        private void LoadStudents()
        {
            try
            {
                int departmentId = 0;
                if (cmbDepartment.SelectedValue != null)
                {
                    departmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
                }

                string yearLevel = (cmbYear.SelectedIndex > 0) ? cmbYear.Text : "";
                string faceFilter = (cmbFace.SelectedIndex > 0) ? cmbFace.Text : "All";

                DataTable table = StudentService.Search(txtSearch.Text, departmentId, yearLevel, faceFilter);
                dgvStudents.DataSource = table;
                dgvStudents.ClearSelection();

                bool hasRows = table.Rows.Count > 0;
                bool isFiltering = txtSearch.Text.Trim().Length > 0 || departmentId > 0 || yearLevel.Length > 0 || faceFilter != "All";
                lblEmpty.Visible = !hasRows;
                lblEmpty.Text = isFiltering
                    ? "No student matches your search or filters."
                    : "There are no students yet. Click ADD STUDENT to register one.";
                lblCount.Text = "Showing " + table.Rows.Count + " of " + StudentService.CountAll() + " student(s)";

                if (hasRows)
                {
                    dgvStudents.Rows[0].Selected = true;
                    dgvStudents.CurrentCell = dgvStudents.Rows[0].Cells["colNumber"];
                }
                ShowSelectedStudent();
            }
            catch (Exception ex)
            {
                // Show the problem inside the page, because this method also runs while typing in the search box.
                dgvStudents.DataSource = null;
                lblEmpty.Text = Database.GetFriendlyMessage(ex);
                lblEmpty.Visible = true;
                lblCount.Text = "";
                ShowSelectedStudent();
            }
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            ShowSelectedStudent();
        }

        // Returns the id of the selected row, or 0 when nothing is selected.
        private int GetSelectedStudentId()
        {
            if (dgvStudents.CurrentRow == null || !dgvStudents.CurrentRow.Selected)
            {
                return 0;
            }

            return Convert.ToInt32(dgvStudents.CurrentRow.Cells["colStudentId"].Value);
        }

        // Fills the photo and the details panel with the selected student.
        private void ShowSelectedStudent()
        {
            int studentId = GetSelectedStudentId();
            bool hasSelection = studentId != 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;

            if (!hasSelection)
            {
                ShowPhoto(null);
                lblPhotoName.Text = "Select a student";
                lblPhotoInfo.Text = "";
                SetDetails("-", "-", "-", "-", "-", "-", "-", "-");
                return;
            }

            try
            {
                Student student = StudentService.GetById(studentId);
                if (student == null)
                {
                    return;
                }

                SetDetails(student.StudentNumber, student.FullName, student.DepartmentCode, student.YearLevel,
                    student.Gender, TextOrDash(student.Email), TextOrDash(student.ContactNumber), student.Status);

                lblPhotoName.Text = student.FullName;
                lblPhotoInfo.Text = student.FaceRegistered ? "Face registered (5 pictures)" : "Face not registered";
                lblPhotoInfo.ForeColor = student.FaceRegistered ? Color.DarkGreen : Color.Firebrick;

                byte[] photo = StudentService.GetPhoto(studentId);
                ShowPhoto(photo);
            }
            catch (Exception ex)
            {
                lblPhotoInfo.Text = Database.GetFriendlyMessage(ex);
                lblPhotoInfo.ForeColor = Color.Firebrick;
            }
        }

        private void SetDetails(string number, string name, string course, string year,
            string gender, string email, string contact, string status)
        {
            lblNumber.Text = number;
            lblName.Text = name;
            lblCourse.Text = course;
            lblYear.Text = year;
            lblGender.Text = gender;
            lblEmail.Text = email;
            lblContact.Text = contact;
            lblStatus.Text = status;
        }

        private static string TextOrDash(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? "-" : text;
        }

        // Shows the photo (JPEG bytes) in the PictureBox. null clears it.
        private void ShowPhoto(byte[] photo)
        {
            Image oldImage = picPhoto.Image;
            picPhoto.Image = null;
            if (oldImage != null)
            {
                oldImage.Dispose();
            }

            if (photo != null)
            {
                picPhoto.Image = ImageHelper.BytesToBitmap(photo);
            }
        }
    }
}
