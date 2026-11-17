using System;
using System.Data;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Shows the departments/courses. Departments can be added, edited and deleted here.
    public partial class Departments : Form
    {
        public Departments()
        {
            InitializeComponent();
            NavigationHelper.ExitWhenClosed(this);
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (AddDepartment form = new AddDepartment())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDepartments();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditSelectedDepartment();
        }

        private void dgvDepartments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedDepartment();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int departmentId = GetSelectedDepartmentId();
            if (departmentId == 0)
            {
                MessageBox.Show("Please select a department from the list first.", "Delete Department",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string code = dgvDepartments.CurrentRow.Cells["colCode"].Value.ToString();

            try
            {
                // A department that still has students cannot be deleted.
                int studentCount = DepartmentService.CountStudents(departmentId);
                if (studentCount > 0)
                {
                    MessageBox.Show("Department " + code + " still has " + studentCount + " student(s), so it cannot be deleted.\n\n" +
                        "Move or delete those students first.", "Delete Department",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult answer = MessageBox.Show("Are you sure you want to delete the department " + code + "?",
                    "Delete Department", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer != DialogResult.Yes)
                {
                    return;
                }

                DepartmentService.Delete(departmentId);
                MessageBox.Show("Department " + code + " was deleted.", "Delete Department",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDepartments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Delete Department",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditSelectedDepartment()
        {
            int departmentId = GetSelectedDepartmentId();
            if (departmentId == 0)
            {
                MessageBox.Show("Please select a department from the list first.", "Edit Department",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (AddDepartment form = new AddDepartment(departmentId))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDepartments();
                }
            }
        }

        // Returns the id of the selected row, or 0 when nothing is selected.
        private int GetSelectedDepartmentId()
        {
            if (dgvDepartments.CurrentRow == null || !dgvDepartments.CurrentRow.Selected)
            {
                return 0;
            }

            return Convert.ToInt32(dgvDepartments.CurrentRow.Cells["colDepartmentId"].Value);
        }

        private void LoadDepartments()
        {
            try
            {
                DataTable table = DepartmentService.Search(txtSearch.Text);
                dgvDepartments.DataSource = table;
                dgvDepartments.ClearSelection();

                bool hasRows = table.Rows.Count > 0;
                lblEmpty.Visible = !hasRows;
                lblEmpty.Text = (txtSearch.Text.Trim().Length > 0)
                    ? "No department matches your search."
                    : "There are no departments yet. Click ADD DEPARTMENT to create one.";
                lblCount.Text = "Showing " + table.Rows.Count + " department(s)";
                btnEdit.Enabled = hasRows;
                btnDelete.Enabled = hasRows;
            }
            catch (Exception ex)
            {
                // Show the problem inside the page, because this method also runs while typing in the search box.
                dgvDepartments.DataSource = null;
                lblEmpty.Text = Database.GetFriendlyMessage(ex);
                lblEmpty.Visible = true;
                lblCount.Text = "";
            }
        }
    }
}
