using System;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Small window used to add a new department or to edit an existing one.
    public partial class AddDepartment : Form
    {
        // 0 means "add a new department", any other number is the department being edited.
        private int departmentId;

        public AddDepartment() : this(0)
        {
        }

        public AddDepartment(int departmentId)
        {
            InitializeComponent();
            this.departmentId = departmentId;
        }

        private void AddDepartment_Load(object sender, EventArgs e)
        {
            if (departmentId == 0)
            {
                cmbLevel.SelectedIndex = 0;
                return;
            }

            lblTitle.Text = "Edit Department";
            btnSave.Text = "UPDATE";

            try
            {
                Department department = DepartmentService.GetById(departmentId);
                if (department == null)
                {
                    MessageBox.Show("This department no longer exists.", "Edit Department",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.Cancel;
                    return;
                }

                txtName.Text = department.DepartmentName;
                txtCode.Text = department.DepartmentCode;
                txtHead.Text = department.DepartmentHead;
                txtEmail.Text = department.ContactEmail;
                txtDescription.Text = department.Description;

                int index = cmbLevel.FindStringExact(department.AcademicLevel);
                cmbLevel.SelectedIndex = (index >= 0) ? index : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Edit Department",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string code = txtCode.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (Validator.IsEmpty(name) || Validator.IsEmpty(code))
            {
                ShowWarning("Please enter the department name and the department code.");
                return;
            }

            if (email.Length > 0 && !Validator.IsValidEmail(email))
            {
                ShowWarning("Please enter a valid email address, for example name@example.com.");
                txtEmail.Focus();
                return;
            }

            if (cmbLevel.SelectedIndex < 0)
            {
                ShowWarning("Please choose the academic level.");
                return;
            }

            Department department = new Department();
            department.DepartmentId = departmentId;
            department.DepartmentName = name;
            department.DepartmentCode = code;
            department.DepartmentHead = txtHead.Text.Trim();
            department.ContactEmail = email;
            department.Description = txtDescription.Text.Trim();
            department.AcademicLevel = cmbLevel.Text;

            try
            {
                if (DepartmentService.CodeOrNameExists(code, name, departmentId))
                {
                    ShowWarning("Another department already uses this code or name.");
                    return;
                }

                if (departmentId == 0)
                {
                    DepartmentService.Add(department);
                }
                else
                {
                    DepartmentService.Update(department);
                }

                MessageBox.Show("Department saved successfully.", "Department",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Department",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Clear the boxes (when editing, go back to the saved values)
            if (departmentId == 0)
            {
                txtName.Clear();
                txtCode.Clear();
                txtHead.Clear();
                txtEmail.Clear();
                txtDescription.Clear();
                cmbLevel.SelectedIndex = 0;
                txtName.Focus();
            }
            else
            {
                AddDepartment_Load(sender, e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Department", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
