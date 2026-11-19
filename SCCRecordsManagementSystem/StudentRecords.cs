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
    public partial class StudentRecords : Form
    {
        private readonly ComboBox department = new ComboBox();
        private readonly ComboBox year = new ComboBox();
        private readonly ComboBox state = new ComboBox();
        private Label count;
        private Label attendance;
        public StudentRecords()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu(); Program.Grid(dataGridView1);
            department.SetBounds(336, 513, 255, 26); Controls.Add(department);
            year.SetBounds(336, 548, 123, 26); Controls.Add(year);
            state.SetBounds(468, 548, 123, 26); Controls.Add(state);
            department.BringToFront(); year.BringToFront(); state.BringToFront();
            year.DropDownStyle = state.DropDownStyle = ComboBoxStyle.DropDownList;
            year.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year" }); year.SelectedIndex = 0;
            state.Items.AddRange(new object[] { "All Status", "Active", "Inactive" }); state.SelectedIndex = 0;
            count = Program.AddLabel(this, "", 336, 582, 260);
            attendance = Program.AddLabel(panel3, "", 380, 20, 290, 250);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            button3.Click += delegate { EditStudent(true); }; button2.Click += delegate { EditStudent(false); };
            button1.Click += DeleteStudent; button4.Click += delegate { LoadStudents(); };
            textBox1.TextChanged += delegate { LoadStudents(); };
            department.SelectionChangeCommitted += delegate { LoadStudents(); };
            year.SelectedIndexChanged += delegate { LoadStudents(); };
            state.SelectedIndexChanged += delegate { LoadStudents(); };
            dataGridView1.SelectionChanged += ShowStudent;
            dataGridView1.CellDoubleClick += delegate { EditStudent(false); };
            Shown += delegate {
                try { Program.Choices(department, Database.GetData("SELECT department_id,department_code FROM departments ORDER BY department_code"), "department_id", "department_code", "All Departments"); LoadStudents(); }
                catch (Exception ex) { Program.Error(ex); }
            };
            FormClosed += delegate { Registration.ReplaceImage(pictureBox1, null); };
        }

        private void LoadStudents()
        {
            try
            {
                Database.Require("Student Records");
                DataTable rows = Database.GetData(@"SELECT s.*,d.department_code,CONCAT_WS(' ',s.first_name,s.middle_name,s.last_name) AS full_name
                    FROM students s JOIN departments d ON d.department_id=s.department_id
                    WHERE (@department=0 OR s.department_id=@department) AND (@year='' OR s.year_level=@year)
                    AND (@status='' OR s.status=@status) AND (s.student_number LIKE @q OR CONCAT_WS(' ',s.first_name,s.middle_name,s.last_name) LIKE @q)
                    ORDER BY s.last_name,s.first_name", new Dictionary<string, object> {
                        { "@department", Program.SelectedId(department) }, { "@year", year.SelectedIndex <= 0 ? "" : year.Text },
                        { "@status", state.SelectedIndex <= 0 ? "" : state.Text }, { "@q", "%" + textBox1.Text.Trim() + "%" } });
                dataGridView1.DataSource = rows;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    column.Visible = new[] { "student_number", "full_name", "department_code", "year_level", "status" }.Contains(column.Name);
                count.Text = rows.Rows.Count == 0 ? "No students found." : rows.Rows.Count + " student(s)";
            }
            catch (Exception ex)
            {
                dataGridView1.DataSource = null; ShowStudent(this, EventArgs.Empty);
                count.Text = Database.ErrorMessage(ex);
            }
        }

        private void ShowStudent(object sender, EventArgs e)
        {
            try
            {
                Registration.ReplaceImage(pictureBox1, null);
                if (dataGridView1.CurrentRow == null)
                {
                    label2.Text = "No selection"; label3.Text = "Name:"; label4.Text = "Gender:";
                    label5.Text = "Email:"; label6.Text = "Contact:"; label7.Text = "Status:"; label8.Text = "Face Registered:";
                    attendance.Text = "";
                    return;
                }
                DataRow r = Program.SelectedRow(dataGridView1);
                label2.Text = r["student_number"].ToString(); label2.Left = 20;
                label3.Text = "Name: " + r["full_name"]; label4.Text = "Gender: " + r["gender"];
                label5.Text = "Email: " + r["email"]; label6.Text = "Contact: " + r["contact_number"];
                label7.Text = "Status: " + r["status"];
                // Attendance totals only count finished sessions (Present or Absent).
                DataRow totals = Database.GetData(@"SELECT COALESCE(SUM(status='Present'),0) AS present,COALESCE(SUM(status='Absent'),0) AS absent,
                    (SELECT COALESCE(SUM(balance),0) FROM obligation_balances WHERE student_id=@id) AS balance,
                    (SELECT MAX(time_in) FROM event_attendance WHERE student_id=@id) AS last_scan
                    FROM attendance_records WHERE student_id=@id", new Dictionary<string, object> { { "@id", r["student_id"] } }).Rows[0];
                attendance.Text = "Department: " + r["department_code"] + " - " + r["year_level"] + "\n\nATTENDANCE SUMMARY\nPresent sessions: " + totals["present"] +
                    "\nAbsent sessions: " + totals["absent"] + "\nObligation balance: " + totals["balance"] + " points\nLast scan: " +
                    (totals["last_scan"] == DBNull.Value ? "none yet" : Convert.ToDateTime(totals["last_scan"]).ToString("MMM d, yyyy h:mm tt"));
                DataTable faces = Database.GetData("SELECT image_data FROM face_images WHERE student_id=@id ORDER BY image_number",
                    new Dictionary<string, object> { { "@id", r["student_id"] } });
                label8.Text = "Face Registered: " + faces.Rows.Count + " of 5";
                if (faces.Rows.Count > 0)
                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream((byte[])faces.Rows[0]["image_data"]))
                    using (Bitmap image = new Bitmap(stream)) Registration.ReplaceImage(pictureBox1, new Bitmap(image));
            }
            catch (Exception ex) { count.Text = Database.ErrorMessage(ex); }
        }

        private void EditStudent(bool add)
        {
            try
            {
                Database.Require("Student Records");
                using (Registration form = new Registration(add ? 0 : Convert.ToInt32(Program.SelectedRow(dataGridView1)["student_id"])))
                    if (form.ShowDialog(this) == DialogResult.OK) LoadStudents();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void DeleteStudent(object sender, EventArgs e)
        {
            try
            {
                Database.Require("Student Records");
                DataRow row = Program.SelectedRow(dataGridView1);
                if (MessageBox.Show("Delete " + row["full_name"] + " and their face images, event attendance and obligations?",
                    "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                Database.Execute("DELETE FROM students WHERE student_id=@id", new Dictionary<string, object> { { "@id", row["student_id"] } });
                LoadStudents();
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
            button21.Click += delegate { LoadStudents(); };
            button22.Click += delegate { Program.OpenPage(this, "Departments", () => new Departments()); };
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
