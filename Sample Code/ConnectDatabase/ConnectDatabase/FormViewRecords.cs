using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectDatabase
{
    public partial class FormViewRecords : Form
    {
        public FormViewRecords()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormViewRecords_Load(object sender, EventArgs e)
        {
            ConnectDB.displayData(dataGridView);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "Select * from personal_info where ID = @id";

            Dictionary<string, object> parameters =new Dictionary<string, object>(){
                { "@id", txtIDNum.Text }
            };

            PersonalInfo personalInfo =
                ConnectDB.searchAndGetPersonalInfo(query, parameters);

            if (personalInfo != null)
            {
                // Display data in the controls
                lblIDNumber.Text = personalInfo.Id.ToString();
                txtFname.Text = personalInfo.FirstName;
                txtMname.Text = personalInfo.MiddleName;
                txtLname.Text = personalInfo.LastName;
                dtpBdate.Value = personalInfo.Birthdate;
            }
            else
            {
                MessageBox.Show("No records found.","Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "Update personal_info SET firstname=@firstname, middlename=@middlename, lastname=@lastname, birthdate=@birthdate where ID =@id";

            Dictionary<string, object> parameters =new Dictionary<string, object>()
            {
                { "@id", Convert.ToInt32(lblIDNumber.Text) },
                { "@firstname", txtFname.Text },
                { "@middlename", txtMname.Text },
                { "@lastname", txtLname.Text },
                { "@birthdate", dtpBdate.Value.Date },
                { "@command", "UPDATE" }
            };

            ConnectDB.saveUpdateDeleteData(query, parameters);
            ConnectDB.displayData(dataGridView);

            MessageBox.Show(
                "Record updated successfully!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?","Alert",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string query =
                    "Delete from personal_info where ID = @id";

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "@id", lblIDNumber.Text },
                    { "@command", "delete" }
                };

                ConnectDB.saveUpdateDeleteData(query, parameters);

                MessageBox.Show(
                    "Record deleted successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ConnectDB.displayData(dataGridView);

                // Clear fields
                lblIDNumber.Text = "";
                txtFname.Clear();
                txtMname.Clear();
                txtLname.Clear();
            }
        }

            private void addRecordToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Form1 f=new Form1();
                f.Show();
                this.Hide();
            }
    }
}
