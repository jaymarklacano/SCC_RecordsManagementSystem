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
            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT department_id, department_name, department_code, department_head, contact_email, academic_level FROM departments";

                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dtgDepartment.DataSource = dt;

                        if (dtgDepartment.Columns["department_id"] != null)
                        {
                            dtgDepartment.Columns["department_id"].Visible = false; // Hide ID column
                            dtgDepartment.Columns["department_name"].HeaderText = "Department Name";
                            dtgDepartment.Columns["department_code"].HeaderText = "Code";
                            dtgDepartment.Columns["department_head"].HeaderText = "Head";
                            dtgDepartment.Columns["contact_email"].HeaderText = "Email";
                            dtgDepartment.Columns["academic_level"].HeaderText = "Academic Level";
                        }
                    }

                    // Add Edit button column if not already added
                    if (!dtgDepartment.Columns.Contains("EditAction"))
                    {
                        DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();
                        editBtn.Name = "EditAction";
                        editBtn.HeaderText = "Action";
                        editBtn.Text = "Edit";
                        editBtn.UseColumnTextForButtonValue = true;
                        dtgDepartment.Columns.Add(editBtn);
                    }

                    // Add Delete button column if not already added
                    if (!dtgDepartment.Columns.Contains("DeleteAction"))
                    {
                        DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
                        deleteBtn.Name = "DeleteAction";
                        deleteBtn.HeaderText = "";
                        deleteBtn.Text = "Delete";
                        deleteBtn.UseColumnTextForButtonValue = true;
                        dtgDepartment.Columns.Add(deleteBtn);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading departments: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtgDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int departmentId = Convert.ToInt32(dtgDepartment.Rows[e.RowIndex].Cells["department_id"].Value);

            if (dtgDepartment.Columns[e.ColumnIndex].Name == "EditAction")
            {
                // Open AddDepartmentForm in edit mode
                AddDepartment editForm = new AddDepartment(departmentId);
                editForm.ShowDialog();
                LoadDepartments(); // Refresh grid after editing
            }
            else if (dtgDepartment.Columns[e.ColumnIndex].Name == "DeleteAction")
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to delete this department?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    DeleteDepartment(departmentId);
                }
            }
        }

        private void DeleteDepartment(int departmentId)
        {
            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM departments WHERE department_id = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", departmentId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Department deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDepartments();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting department: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            AddDepartment addDepartment = new AddDepartment();
            addDepartment.ShowDialog();

            // Refresh grid right after the add form closes
            LoadDepartments();
        }

        
    }
}
