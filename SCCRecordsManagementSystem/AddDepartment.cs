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
        private readonly int departmentId;
        public AddDepartment() : this(0) { }
        public AddDepartment(int id)
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            departmentId = id;
            Program.PrepareWindow(this);
            label1.Text = id == 0 ? "Add Department" : "Edit Department";
            label9.Text = "Academic Information";
            button2.Text = "Save Department";
            button2.Width = 140; button2.Left = 621;
            AcceptButton = button2;
            CancelButton = button1;
            comboBox1.Items.AddRange(new object[] { "Undergraduate", "Graduate", "Senior High School" });
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList; comboBox1.SelectedIndex = 0;
            textBox1.MaxLength = textBox4.MaxLength = textBox2.MaxLength = 100;
            textBox5.MaxLength = 20; textBox3.MaxLength = 255;
            button2.Click += SaveDepartment;
            button1.Text = "Cancel"; button1.Click += delegate { Close(); };
            if (id > 0)
            {
                DataRow row = Database.GetData("SELECT * FROM departments WHERE department_id=@id",
                    new Dictionary<string, object> { { "@id", id } }).Rows[0];
                textBox1.Text = row["department_name"].ToString(); textBox5.Text = row["department_code"].ToString();
                textBox4.Text = row["department_head"].ToString(); textBox2.Text = row["contact_email"].ToString();
                textBox3.Text = row["description"].ToString(); comboBox1.Text = row["academic_level"].ToString();
            }
        }

        private void SaveDepartment(object sender, EventArgs e)
        {
            try
            {
                Database.Require("Departments");
                Database.CheckText(textBox1.Text, "Department name", 100);
                Database.CheckText(textBox5.Text, "Department code", 20);
                Database.CheckContact(textBox2.Text, "");
                string sql = departmentId == 0
                    ? "INSERT INTO departments(department_name,department_code,department_head,contact_email,description,academic_level) VALUES(@name,@code,@head,@email,@description,@level)"
                    : "UPDATE departments SET department_name=@name,department_code=@code,department_head=@head,contact_email=@email,description=@description,academic_level=@level WHERE department_id=@id";
                Database.Execute(sql, new Dictionary<string, object> { { "@id", departmentId },
                    { "@name", textBox1.Text.Trim() }, { "@code", textBox5.Text.Trim() }, { "@head", Database.Optional(textBox4.Text) },
                    { "@email", Database.Optional(textBox2.Text) }, { "@description", Database.Optional(textBox3.Text) }, { "@level", comboBox1.Text } });
                MessageBox.Show("Department saved.");
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        private void AddDepartment_Load(object sender, EventArgs e)
        {

        }
    }
}
