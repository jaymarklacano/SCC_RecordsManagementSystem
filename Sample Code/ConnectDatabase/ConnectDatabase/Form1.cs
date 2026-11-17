using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ConnectDatabase.PersonalInfo;

namespace ConnectDatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadIDs();
            Point sharedLocation = new Point(12, 45);
            Size sharedSize = new Size(720, 480);

            // Apply shared dimensions to all three group boxes
            groupBox1.Location = sharedLocation; // Replace groupBox1 with your Information GroupBox name if different
            groupBox1.Size = sharedSize;

            groupBox2.Location = sharedLocation; // Replace groupBox2 with your Education Info GroupBox name if different
            groupBox2.Size = sharedSize;

            groupBox3.Location = sharedLocation; // Replace groupBox3 with your Job Info GroupBox name if different
            groupBox3.Size = sharedSize;

            // Show only Step 1 (Information) on startup
            ShowStep1();

            txtFirstName.TextChanged += (s, args) => UpdateEmployeeName();
            txtMiddleName.TextChanged += (s, args) => UpdateEmployeeName();
            txtLastName.TextChanged += (s, args) => UpdateEmployeeName();
        }

        private void UpdateEmployeeName()
        {
            string firstName = txtFirstName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string lastName = txtLastName.Text.Trim();

            // Combine them with spaces (handling optional middle name cleanly)
            if (string.IsNullOrEmpty(middleName))
            {
                txtEmployee.Text = $"{firstName} {lastName}".Trim();
            }
            else
            {
                txtEmployee.Text = $"{firstName} {middleName} {lastName}".Trim();
            }
        }

        private void LoadIDs()
        {
            // Load IDs for all sections
            ConnectDB.getMaxID();
            ConnectDB.getMaxEducID();
            ConnectDB.getMaxJobID();

            lblIDNumber.Text = Instantiate.recordChecker.MaxID.ToString();
            lblEducIDNumber.Text = Instantiate.recordChecker.MaxEducID.ToString(); // Replace with your actual Education ID label/textbox name
            lblJobIDNumber.Text = Instantiate.recordChecker.MaxJobID.ToString();     // Replace with your actual Job ID label/textbox name
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int personalId = Convert.ToInt32(lblIDNumber.Text);

                // 1. Insert into personal_info
                string queryPersonal = @"INSERT INTO personal_info(ID, FirstName, MiddleName, LastName, Birthdate) 
                                         VALUES (@id, @firstname, @middlename, @lastname, @birthdate)";
                Dictionary<string, object> paramPersonal = new Dictionary<string, object>(){
                    { "@id", personalId },
                    { "@firstname", txtFirstName.Text },
                    { "@middlename", txtMiddleName.Text },
                    { "@lastname", txtLastName.Text },
                    { "@birthdate", dtpBirthDate.Value.Date }
                };
                ConnectDB.saveUpdateDeleteData(queryPersonal, paramPersonal);

                // 2. Insert into educ_attianment
                string queryEduc = @"INSERT INTO educ_attianment(educ_id, personal_id, school, degree, year_graduated, date) 
                                     VALUES (@educ_id, @personal_id, @school, @degree, @year_graduated, @date)";
                Dictionary<string, object> paramEduc = new Dictionary<string, object>(){
                    { "@educ_id", Convert.ToInt32(lblEducIDNumber.Text) },
                    { "@personal_id", personalId },
                    { "@school", txtSchool.Text },
                    { "@degree", txtDegree.Text },
                    { "@year_graduated", dtpYearGraduated.Value.Date },
                    { "@date", dtpDate.Value.Date }
                };
                ConnectDB.saveUpdateDeleteData(queryEduc, paramEduc);

                // 3. Insert into job_info
                string queryJob = @"INSERT INTO job_info(job_id, personal_id, employee, address, position, date_hired, date_end, status) 
                                    VALUES (@job_id, @personal_id, @employee, @address, @position, @date_hired, @date_end, @status)";

                // Check if the end date picker is disabled (meaning "Current Job" is checked)
                object endDateValue;
                if (dtpEndDate.Enabled)
                {
                    endDateValue = dtpEndDate.Value.Date; // Save the selected date
                }
                else
                {
                    endDateValue = DBNull.Value;          // Save NULL in the database if ongoing
                }
                Dictionary<string, object> paramJob = new Dictionary<string, object>(){
                    { "@job_id", Convert.ToInt32(lblJobIDNumber.Text) },
                    { "@personal_id", personalId },
                    { "@employee", txtEmployee.Text },     // Update control names to match your designer
                    { "@address", txtAddress.Text },
                    { "@position", txtPosition.Text },
                    { "@date_hired", dtpHiredDate.Enabled ? (object)dtpHiredDate.Value.Date : DBNull.Value },
                    { "@date_end", endDateValue},
                    { "@status", txtStatus.Text }
                };
                ConnectDB.saveUpdateDeleteData(queryJob, paramJob);

                MessageBox.Show("All records saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset / Get next IDs and clear fields
                LoadIDs();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormViewRecords rec = new FormViewRecords();
            rec.Show();
            this.Hide();
        }

        private void ClearFields()
        {
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtSchool.Clear();
            txtDegree.Clear();
            // Clear job fields as well...
        }

        private void btnNext1_Click(object sender, EventArgs e)
        {
            // Optional: Validate that required fields are filled before proceeding
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please fill in First Name and Last Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Personal info locked in. Proceed to Education Info.", "Step 1 Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowStep2();
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSchool.Text) || string.IsNullOrWhiteSpace(txtDegree.Text))
            {
                MessageBox.Show("Please fill in School and Degree.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Education info locked in. Proceed to Job Info.", "Step 2 Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowStep3();
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Information fm= new Information();
            fm.Show();
            this.Hide();
        }



        // Helper methods to switch visible group boxes in the exact same location
        private void ShowStep1()
        {
            groupBox1.Visible = true;  // Information
            groupBox2.Visible = false; // Education
            groupBox3.Visible = false; // Job
        }

        private void ShowStep2()
        {
            groupBox1.Visible = false; // Information
            groupBox2.Visible = true;  // Education
            groupBox3.Visible = false; // Job
        }

        private void ShowStep3()
        {
            groupBox1.Visible = false; // Information
            groupBox2.Visible = false; // Education
            groupBox3.Visible = true;  // Job
        }

        // Position validation: If position is N/A, disable everything. Otherwise respect checkbox.
        private void txtPosition_TextChanged(object sender, EventArgs e)
        {
            string position = txtPosition.Text.Trim();

            if (position.Equals("N/A", StringComparison.OrdinalIgnoreCase))
            {
                dtpHiredDate.Enabled = false;
                dtpEndDate.Enabled = false;
                chkCurrentJob.Enabled = false;
            }
            else
            {
                dtpHiredDate.Enabled = true;
                chkCurrentJob.Enabled = true;
                // End date is enabled only if the "Current Job" checkbox is NOT checked
                dtpEndDate.Enabled = !chkCurrentJob.Checked;
            }


        }

        private void chkCurrentJob_CheckedChanged(object sender, EventArgs e)
        {
            // If the checkbox is checked, disable the end date picker. Otherwise, enable it.
            dtpEndDate.Enabled = !chkCurrentJob.Checked;
        }
    }
}
