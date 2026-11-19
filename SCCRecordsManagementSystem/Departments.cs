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
        private Label summary;
        public Departments()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu();
            Program.Grid(dataGridView1); dataGridView1.Width = 934;
            textBox1.SetBounds(350, 78, 210, 26);
            button2.SetBounds(566, 72, 90, 36); button1.SetBounds(663, 72, 180, 36);
            Program.AddButton(this, "Edit", 855, 72, 100, delegate { EditDepartment(false); });
            Program.AddButton(this, "Delete", 965, 72, 100, DeleteDepartment);
            dataGridView1.Height = 450;
            summary = Program.AddLabel(this, "", 359, 570, 925, 36);
            dataGridView1.CellDoubleClick += delegate { EditDepartment(false); };
            button1.Click += delegate { EditDepartment(true); };
            button2.Click += delegate { LoadDepartments(); };
            textBox1.TextChanged += delegate { LoadDepartments(); };
            Shown += delegate { LoadDepartments(); };
        }

        private void LoadDepartments()
        {
            try
            {
                Database.Require("Departments");
                dataGridView1.DataSource = Database.GetData(
                    @"SELECT d.*,(SELECT COUNT(*) FROM students s WHERE s.department_id=d.department_id) AS students,
                    (SELECT COUNT(*) FROM officers o WHERE o.department_id=d.department_id) AS officers
                    FROM departments d WHERE department_name LIKE @q OR department_code LIKE @q OR department_head LIKE @q ORDER BY department_code",
                    new Dictionary<string, object> { { "@q", "%" + textBox1.Text.Trim() + "%" } });
                dataGridView1.Columns["department_id"].Visible = false;
                summary.Text = dataGridView1.Rows.Count == 0 ? "No departments match. Add a department or clear the search." :
                    dataGridView1.Rows.Count + " department(s). Double-click to edit. Referenced departments cannot be deleted.";
            }
            catch (Exception ex) { dataGridView1.DataSource = null; summary.Text = Database.ErrorMessage(ex); }
        }

        private void EditDepartment(bool add)
        {
            try
            {
                Database.Require("Departments");
                int id = add ? 0 : Convert.ToInt32(Program.SelectedRow(dataGridView1)["department_id"]);
                using (AddDepartment form = new AddDepartment(id))
                    if (form.ShowDialog(this) == DialogResult.OK) LoadDepartments();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void DeleteDepartment(object sender, EventArgs e)
        {
            try
            {
                Database.Require("Departments");
                DataRow row = Program.SelectedRow(dataGridView1);
                if (MessageBox.Show("Delete " + row["department_name"] + "?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                Database.Execute("DELETE FROM departments WHERE department_id=@id",
                    new Dictionary<string, object> { { "@id", row["department_id"] } });
                LoadDepartments();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void WireMenu()
        {
            button12.Click += delegate { Program.Logout(this); };
            button13.Click += delegate { Program.OpenPage(this, "Dashboard", () => new Dashboard()); };
            button14.Enabled = button20.Enabled = Database.IsAdmin;
            button14.Click += delegate { Program.OpenPage(this, "Rules and Permission", () => new RulesPermission()); };
            button15.Click += delegate { Program.OpenPage(this, "Obligations", () => new Obligations()); };
            button16.Click += delegate { Program.OpenPage(this, "Reports and Analysis", () => new ReportsAndAnalysis()); };
            button17.Click += delegate { Program.OpenPage(this, "Attendance Monitoring", () => new AttendanceMonitoring()); };
            button18.Click += delegate { Program.OpenPage(this, "Event Management", () => new Event_Management()); };
            button19.Click += delegate { Program.OpenPage(this, "Face Recognition", () => new FaceRecognition()); };
            button20.Click += delegate { Program.OpenPage(this, "Officer Records", () => new Officerr_Records()); };
            button21.Click += delegate { Program.OpenPage(this, "Student Records", () => new StudentRecords()); };
            button22.Click += delegate { LoadDepartments(); };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
