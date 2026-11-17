using System;
using System.Data;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Shows the attendance records. The user can filter by date, student, course, year level,
    // status (Present / Absent) and search by name or student ID.
    public partial class AttendanceMonitoring : Form
    {
        // True while the form changes its own controls, so the filters do not run too early.
        private bool isLoading = true;

        public AttendanceMonitoring()
        {
            InitializeComponent();
            NavigationHelper.ExitWhenClosed(this);
        }

        private void AttendanceMonitoring_Load(object sender, EventArgs e)
        {
            try
            {
                // Student filter: the first item means "all students"
                DataTable students = StudentService.GetStudentChoices();
                DataRow allStudents = students.NewRow();
                allStudents["student_id"] = 0;
                allStudents["display_name"] = "All Students";
                students.Rows.InsertAt(allStudents, 0);

                cmbStudent.DataSource = students;
                cmbStudent.ValueMember = "student_id";
                cmbStudent.DisplayMember = "display_name";

                // Course filter: the first item means "all courses"
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
            dtpDate.Value = DateTime.Today;
            dtpDate.Checked = true;
            isLoading = false;

            LoadRecords();
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                LoadRecords();
            }
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            isLoading = true;
            dtpDate.Value = DateTime.Today;
            dtpDate.Checked = true;
            isLoading = false;

            LoadRecords();
        }

        private void btnAllDates_Click(object sender, EventArgs e)
        {
            // Without a date the grid shows the whole attendance history
            dtpDate.Checked = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            isLoading = true;
            dtpDate.Value = DateTime.Today;
            dtpDate.Checked = true;
            txtSearch.Clear();
            if (cmbStudent.Items.Count > 0)
            {
                cmbStudent.SelectedIndex = 0;
            }
            if (cmbDepartment.Items.Count > 0)
            {
                cmbDepartment.SelectedIndex = 0;
            }
            cmbYear.SelectedIndex = 0;
            chkPresent.Checked = true;
            chkAbsent.Checked = true;
            isLoading = false;

            LoadRecords();
        }

        // Reads the filters, gets the records from the database and shows them.
        private void LoadRecords()
        {
            // "Absent" only makes sense for one chosen date
            bool hasDate = dtpDate.Checked;
            chkAbsent.Enabled = hasDate;

            try
            {
                DateTime? date = null;
                if (hasDate)
                {
                    date = dtpDate.Value.Date;
                }

                int studentId = (cmbStudent.SelectedValue == null) ? 0 : Convert.ToInt32(cmbStudent.SelectedValue);
                int departmentId = (cmbDepartment.SelectedValue == null) ? 0 : Convert.ToInt32(cmbDepartment.SelectedValue);
                string yearLevel = (cmbYear.SelectedIndex > 0) ? cmbYear.Text : "";

                DataTable table = AttendanceService.GetRecords(date, studentId, txtSearch.Text, departmentId, yearLevel,
                    chkPresent.Checked, chkAbsent.Checked && hasDate);

                dgvAttendance.DataSource = table;
                dgvAttendance.ClearSelection();
                ColorStatusColumn();

                ShowEmptyMessage(table.Rows.Count, hasDate);
                lblCount.Text = "Showing " + table.Rows.Count + " record(s)";
                ShowSummary(date, table);
            }
            catch (Exception ex)
            {
                // Show the problem inside the page, because this method also runs while typing in the search box.
                dgvAttendance.DataSource = null;
                lblEmpty.Text = Database.GetFriendlyMessage(ex);
                lblEmpty.Visible = true;
                lblCount.Text = "";
            }
        }

        // The summary bar above the grid
        private void ShowSummary(DateTime? date, DataTable table)
        {
            int totalStudents = StudentService.CountAll();

            if (date.HasValue)
            {
                int total, present, absent;
                AttendanceService.GetSummary(date.Value, out total, out present, out absent);

                lblSummaryDate.Text = "Date: " + date.Value.ToString("MMM d, yyyy");
                lblSummaryTotal.Text = "Total Students: " + total;
                lblSummaryPresent.Text = "Present: " + present;
                lblSummaryAbsent.Text = "Absent: " + absent;
            }
            else
            {
                int presentRecords = 0;
                foreach (DataRow row in table.Rows)
                {
                    if (row["status"].ToString() == "Present")
                    {
                        presentRecords++;
                    }
                }

                lblSummaryDate.Text = "Date: All dates";
                lblSummaryTotal.Text = "Total Students: " + totalStudents;
                lblSummaryPresent.Text = "Present records: " + presentRecords;
                lblSummaryAbsent.Text = "Absent: choose a date";
            }
        }

        private void ShowEmptyMessage(int rowCount, bool hasDate)
        {
            lblEmpty.Visible = (rowCount == 0);
            if (rowCount > 0)
            {
                return;
            }

            if (!chkPresent.Checked && !(chkAbsent.Checked && hasDate))
            {
                lblEmpty.Text = "Tick Present or Absent to see records.";
            }
            else if (hasDate && dtpDate.Value.Date == DateTime.Today && IsNotFiltered())
            {
                lblEmpty.Text = "No attendance has been recorded today yet.";
            }
            else
            {
                lblEmpty.Text = "No attendance records match your filters.";
            }
        }

        private bool IsNotFiltered()
        {
            return txtSearch.Text.Trim().Length == 0 && cmbStudent.SelectedIndex <= 0 &&
                   cmbDepartment.SelectedIndex <= 0 && cmbYear.SelectedIndex <= 0;
        }

        // Green text for Present, red text for Absent
        private void ColorStatusColumn()
        {
            foreach (DataGridViewRow row in dgvAttendance.Rows)
            {
                bool isPresent = (row.Cells["colStatus"].Value.ToString() == "Present");
                row.Cells["colStatus"].Style.ForeColor = isPresent ? System.Drawing.Color.DarkGreen : System.Drawing.Color.Firebrick;
            }
        }
    }
}
