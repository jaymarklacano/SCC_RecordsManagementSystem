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
    public partial class AddDepartment : Form
    {
        private int? departmentIdToEdit = null;
        public AddDepartment()
        {
            InitializeComponent();
        }

        public AddDepartment(int departmentId)
        {
            InitializeComponent();
            departmentIdToEdit = departmentId;
            LoadDepartmentData(departmentId);
            btnAddDept.Text = "Update Department";
        }

        private void LoadDepartmentData(int id)
        {
            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT department_name, department_code, department_head, contact_email, description, academic_level FROM departments WHERE department_id = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtDeptName.Text = reader["department_name"].ToString();
                                txtDeptCode.Text = reader["department_code"].ToString();
                                txtDeptHead.Text = reader["department_head"].ToString();
                                txtEmail.Text = reader["contact_email"] != DBNull.Value ? reader["contact_email"].ToString() : "";
                                txtDescription.Text = reader["description"] != DBNull.Value ? reader["description"].ToString() : "";
                                cmbAcademicLevel.SelectedItem = reader["academic_level"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading department details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnAddDept_Click(object sender, EventArgs e)
        {
            string deptName = txtDeptName.Text.Trim();
            string deptCode = txtDeptCode.Text.Trim();
            string deptHead = txtDeptHead.Text.Trim();
            string contactEmail = txtEmail.Text.Trim();
            string description = txtDescription.Text.Trim();
            string academicLevel = cmbAcademicLevel.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(deptName) || string.IsNullOrEmpty(deptCode) ||
                string.IsNullOrEmpty(deptHead) || string.IsNullOrEmpty(academicLevel))
            {
                MessageBox.Show("Please fill in all required fields and select an academic level.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query;

                    if (departmentIdToEdit == null)
                    {
                        query = @"INSERT INTO departments (department_name, department_code, department_head, contact_email, description, academic_level) 
                                  VALUES (@name, @code, @head, @email, @desc, @level)";
                    }
                    else
                    {
                        query = @"UPDATE departments 
                                  SET department_name = @name, department_code = @code, department_head = @head, 
                                      contact_email = @email, description = @desc, academic_level = @level 
                                  WHERE department_id = @id";
                    }

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", deptName);
                        cmd.Parameters.AddWithValue("@code", deptCode);
                        cmd.Parameters.AddWithValue("@head", deptHead);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(contactEmail) ? (object)DBNull.Value : contactEmail);
                        cmd.Parameters.AddWithValue("@desc", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                        cmd.Parameters.AddWithValue("@level", academicLevel);

                        if (departmentIdToEdit != null)
                        {
                            cmd.Parameters.AddWithValue("@id", departmentIdToEdit.Value);
                        }

                        cmd.ExecuteNonQuery();
                    }

                    string successMessage = departmentIdToEdit == null ? "Department added successfully!" : "Department updated successfully!";
                    MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062)
                    {
                        MessageBox.Show("A department with this department code already exists.", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (departmentIdToEdit == null)
            {
                txtDeptName.Clear();
                txtDeptCode.Clear();
                txtDeptHead.Clear();
                txtEmail.Clear();
                txtDescription.Clear();
                cmbAcademicLevel.SelectedIndex = -1;
            }
            else
            {
                LoadDepartmentData(departmentIdToEdit.Value);
            }
        }
    }
}
