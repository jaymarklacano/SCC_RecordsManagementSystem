using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public partial class AddDepartment : Form
    {
        // 0 means we are ADDING a new department.
        // Any other number is the id of the department that we are EDITING.
        private int departmentIdToEdit = 0;

        public AddDepartment()
        {
            InitializeComponent();
        }

        public AddDepartment(int departmentId)
        {
            InitializeComponent();
            departmentIdToEdit = departmentId;
        }

        private void AddDepartment_Load(object sender, EventArgs e)
        {
            if (departmentIdToEdit == 0)
            {
                this.Text = "Add Department";
                lblFormTitle.Text = "Add Department";
                btnAddDept.Text = "Save";
                cmbAcademicLevel.SelectedIndex = 0;
            }
            else
            {
                this.Text = "Edit Department";
                lblFormTitle.Text = "Edit Department";
                btnAddDept.Text = "Update";
                LoadDepartmentData(departmentIdToEdit);
            }

            txtDeptName.Focus();
        }

        private void LoadDepartmentData(int id)
        {
            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT department_name, department_code, department_head, contact_email, description, academic_level FROM departments WHERE department_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtDeptName.Text = reader["department_name"].ToString();
                                txtDeptCode.Text = reader["department_code"].ToString();
                                txtDeptHead.Text = reader["department_head"].ToString();

                                // ToString() of a NULL value (DBNull) is an empty string.
                                txtEmail.Text = reader["contact_email"].ToString();
                                txtDescription.Text = reader["description"].ToString();

                                string academicLevel = reader["academic_level"].ToString();

                                if (cmbAcademicLevel.Items.Contains(academicLevel) == false)
                                {
                                    cmbAcademicLevel.Items.Add(academicLevel);
                                }

                                cmbAcademicLevel.SelectedItem = academicLevel;
                            }
                            else
                            {
                                MessageBox.Show("This department was not found. It may have been deleted.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                btnAddDept.Enabled = false;
                Database.showError("Could not load the department details.", ex);
                this.Close();
            }
        }

        private bool isValidEmail(string email)
        {
            // Simple email pattern: something@something.something
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool validateInputs()
        {
            string deptName = txtDeptName.Text.Trim();
            string deptCode = txtDeptCode.Text.Trim();
            string deptHead = txtDeptHead.Text.Trim();
            string contactEmail = txtEmail.Text.Trim();

            if (deptName == "")
            {
                MessageBox.Show("Please enter the department name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDeptName.Focus();
                return false;
            }

            if (deptName.Length < 3)
            {
                MessageBox.Show("The department name is too short.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDeptName.Focus();
                return false;
            }

            if (deptCode == "")
            {
                MessageBox.Show("Please enter the department code (example: BSIT).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDeptCode.Focus();
                return false;
            }

            // Only letters, numbers and the dash (-) are allowed in the code.
            if (Regex.IsMatch(deptCode, @"^[A-Za-z0-9\-]{2,20}$") == false)
            {
                MessageBox.Show("The department code must be 2 to 20 letters or numbers only (example: BSIT).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDeptCode.Focus();
                return false;
            }

            if (deptHead == "")
            {
                MessageBox.Show("Please enter the department head / dean.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDeptHead.Focus();
                return false;
            }

            if (contactEmail != "" && isValidEmail(contactEmail) == false)
            {
                MessageBox.Show("Please enter a valid email address (example: dean@school.edu).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (cmbAcademicLevel.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the academic level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAcademicLevel.Focus();
                return false;
            }

            return true;
        }

        // Returns true when another department already uses the same name or code.
        private bool isDuplicateDepartment(string deptName, string deptCode)
        {
            bool isDuplicate = false;

            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // "department_id <> @id" skips the department that we are editing.
                    string query = "SELECT department_name, department_code FROM departments " +
                                   "WHERE (department_name = @name OR department_code = @code) AND department_id <> @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", deptName);
                        cmd.Parameters.AddWithValue("@code", deptCode);
                        cmd.Parameters.AddWithValue("@id", departmentIdToEdit);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isDuplicate = true;

                                if (reader["department_code"].ToString().ToUpper() == deptCode.ToUpper())
                                {
                                    MessageBox.Show("The department code \"" + deptCode + "\" is already used.", "Duplicate Department", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtDeptCode.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("The department name \"" + deptName + "\" already exists.", "Duplicate Department", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtDeptName.Focus();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Database.showError("Could not check for duplicate departments.", ex);
                isDuplicate = true;
            }

            return isDuplicate;
        }

        private void btnAddDept_Click(object sender, EventArgs e)
        {
            if (validateInputs() == false)
            {
                return;
            }

            string deptName = txtDeptName.Text.Trim();
            string deptCode = txtDeptCode.Text.Trim().ToUpper();
            string deptHead = txtDeptHead.Text.Trim();
            string contactEmail = txtEmail.Text.Trim();
            string description = txtDescription.Text.Trim();
            string academicLevel = cmbAcademicLevel.SelectedItem.ToString();

            if (isDuplicateDepartment(deptName, deptCode) == true)
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query;

                    if (departmentIdToEdit == 0)
                    {
                        query = "INSERT INTO departments (department_name, department_code, department_head, contact_email, description, academic_level) " +
                                "VALUES (@name, @code, @head, @email, @desc, @level)";
                    }
                    else
                    {
                        query = "UPDATE departments " +
                                "SET department_name = @name, department_code = @code, department_head = @head, " +
                                "contact_email = @email, description = @desc, academic_level = @level " +
                                "WHERE department_id = @id";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", deptName);
                        cmd.Parameters.AddWithValue("@code", deptCode);
                        cmd.Parameters.AddWithValue("@head", deptHead);

                        // Optional fields are saved as NULL when they are empty.
                        if (contactEmail == "")
                        {
                            cmd.Parameters.AddWithValue("@email", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@email", contactEmail);
                        }

                        if (description == "")
                        {
                            cmd.Parameters.AddWithValue("@desc", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@desc", description);
                        }

                        cmd.Parameters.AddWithValue("@level", academicLevel);

                        if (departmentIdToEdit != 0)
                        {
                            cmd.Parameters.AddWithValue("@id", departmentIdToEdit);
                        }

                        cmd.ExecuteNonQuery();
                    }

                    if (departmentIdToEdit == 0)
                    {
                        MessageBox.Show("Department added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Department updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Database.showError("The department was not saved.", ex);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (departmentIdToEdit == 0)
            {
                txtDeptName.Clear();
                txtDeptCode.Clear();
                txtDeptHead.Clear();
                txtEmail.Clear();
                txtDescription.Clear();
                cmbAcademicLevel.SelectedIndex = 0;
                txtDeptName.Focus();
            }
            else
            {
                // When editing, Reset brings back the saved values.
                LoadDepartmentData(departmentIdToEdit);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
