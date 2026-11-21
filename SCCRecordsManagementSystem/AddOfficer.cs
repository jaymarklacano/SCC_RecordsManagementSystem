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
    // Used for adding a new officer AND for editing an officer.
    // new AddOfficer()          -> add mode
    // new AddOfficer(officerId) -> edit mode
    public partial class AddOfficer : Form
    {
        // 0 = add a new officer. Any other number = the officer that is being edited.
        private int officerIdToEdit = 0;
        private int accountIdToEdit = 0;

        public AddOfficer()
        {
            InitializeComponent();
        }

        public AddOfficer(int officerId)
        {
            InitializeComponent();
            officerIdToEdit = officerId;
        }

        private void AddOfficer_Load(object sender, EventArgs e)
        {
            Database.loadDepartmentComboBox(cmbDepartment, "Select Course");

            if (officerIdToEdit == 0)
            {
                lblTitle.Text = "Add Officer";
                this.Text = "SCC Records Management System - Add Officer";
                btnSaveOfficer.Text = "SAVE OFFICER";
                btnClearOfficer.Text = "CLEAR FORM";
                lblPasswordNote.Text = "The password must have at least 6 characters.";
                clearForm();
            }
            else
            {
                lblTitle.Text = "Edit Officer";
                this.Text = "SCC Records Management System - Edit Officer";
                btnSaveOfficer.Text = "UPDATE OFFICER";
                btnClearOfficer.Text = "RESET FORM";
                lblPassword.Text = "New Password";
                lblConfirmPassword.Text = "Confirm New Password";
                lblPasswordNote.Text = "Leave the password boxes empty to keep the current password.";

                bool found = loadOfficerToEdit();

                if (found == false)
                {
                    MessageBox.Show("This officer no longer exists. The list will be refreshed.", "Officer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        // ------------------------------------------------------------------
        // Filling and clearing the form
        // ------------------------------------------------------------------

        private void clearForm()
        {
            txtOfficerNumber.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();

            if (cmbDepartment.Items.Count > 0)
            {
                cmbDepartment.SelectedIndex = 0;
            }

            cmbYearLevel.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
            txtEmail.Clear();
            txtContact.Clear();
            cmbPosition.SelectedIndex = -1;
            cmbPosition.Text = "";
            txtSchoolYear.Text = getCurrentSchoolYear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            chkShowPassword.Checked = false;

            txtOfficerNumber.Focus();
        }

        // The school year starts in June. Example: November 2026 -> "2026-2027", March 2027 -> "2026-2027".
        private string getCurrentSchoolYear()
        {
            int startYear = DateTime.Today.Year;

            if (DateTime.Today.Month < 6)
            {
                startYear = startYear - 1;
            }

            return startYear + "-" + (startYear + 1);
        }

        // Shows the saved information of the officer. Returns false when the officer is not found.
        private bool loadOfficerToEdit()
        {
            OfficerInfo officer = Database.searchAndGetOfficerInfo(officerIdToEdit);

            if (officer == null)
            {
                return false;
            }

            accountIdToEdit = officer.AccountId;

            txtOfficerNumber.Text = officer.OfficerNumber;
            txtFirstName.Text = officer.FirstName;
            txtMiddleName.Text = officer.MiddleName;
            txtLastName.Text = officer.LastName;
            cmbDepartment.SelectedValue = officer.DepartmentId;
            cmbYearLevel.SelectedItem = officer.YearLevel;
            cmbGender.SelectedItem = officer.Gender;
            txtEmail.Text = officer.Email;
            txtContact.Text = officer.ContactNumber;
            cmbPosition.Text = officer.Position;
            txtSchoolYear.Text = officer.SchoolYear;
            txtUsername.Text = officer.Username;
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            chkShowPassword.Checked = false;

            return true;
        }

        private void btnClearOfficer_Click(object sender, EventArgs e)
        {
            if (officerIdToEdit == 0)
            {
                clearForm();
            }
            else
            {
                // In edit mode the saved information is shown again.
                loadOfficerToEdit();
            }
        }

        // Officers are students too, so their information can be copied from Student Records.
        private void btnFindStudent_Click(object sender, EventArgs e)
        {
            string studentNumber = txtOfficerNumber.Text.Trim();

            if (studentNumber == "")
            {
                MessageBox.Show("Please type the student ID number first, then click Find Student.", "ID Number Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOfficerNumber.Focus();
                return;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@number", studentNumber);

            object studentIdValue = Database.GetValue("SELECT student_id FROM students WHERE student_number = @number", parameters);

            if (studentIdValue == null || studentIdValue == DBNull.Value)
            {
                MessageBox.Show("No student was found with the ID number \"" + studentNumber + "\".\n\nYou can still type the officer information by hand.",
                    "Student Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StudentInfo student = Database.searchAndGetStudentInfo(Convert.ToInt32(studentIdValue));

            if (student == null)
            {
                return;
            }

            txtFirstName.Text = student.FirstName;
            txtMiddleName.Text = student.MiddleName;
            txtLastName.Text = student.LastName;
            cmbDepartment.SelectedValue = student.DepartmentId;
            cmbYearLevel.SelectedItem = student.YearLevel;
            cmbGender.SelectedItem = student.Gender;
            txtEmail.Text = student.Email;
            txtContact.Text = student.ContactNumber;

            MessageBox.Show("The information of " + student.GetFullName() + " was filled in.\n\nPlease complete the position, school year and login account.",
                "Student Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

            cmbPosition.Focus();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtConfirmPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtConfirmPassword.UseSystemPasswordChar = true;
            }
        }

        // ------------------------------------------------------------------
        // Validation
        // ------------------------------------------------------------------

        private void showValidationMessage(string message, Control controlToFocus)
        {
            MessageBox.Show(message, "Invalid Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            controlToFocus.Focus();
        }

        private bool validateOfficerInfo()
        {
            string officerNumber = txtOfficerNumber.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string contactNumber = txtContact.Text.Trim();
            string position = cmbPosition.Text.Trim();
            string schoolYear = txtSchoolYear.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // \p{L} means "any letter" (so names like "Peña" are accepted).
            string namePattern = @"^[\p{L} .'\-]+$";

            if (officerNumber == "")
            {
                showValidationMessage("Please enter the ID number of the officer.", txtOfficerNumber);
                return false;
            }

            if (Regex.IsMatch(officerNumber, @"^[A-Za-z0-9\-]{3,20}$") == false)
            {
                showValidationMessage("The ID number may only contain letters, numbers and dashes (example: 2026-001).", txtOfficerNumber);
                return false;
            }

            if (firstName == "")
            {
                showValidationMessage("Please enter the first name.", txtFirstName);
                return false;
            }

            if (Regex.IsMatch(firstName, namePattern) == false)
            {
                showValidationMessage("The first name may only contain letters, spaces, dots, dashes and apostrophes.", txtFirstName);
                return false;
            }

            if (middleName != "" && Regex.IsMatch(middleName, namePattern) == false)
            {
                showValidationMessage("The middle name may only contain letters, spaces, dots, dashes and apostrophes.", txtMiddleName);
                return false;
            }

            if (lastName == "")
            {
                showValidationMessage("Please enter the last name.", txtLastName);
                return false;
            }

            if (Regex.IsMatch(lastName, namePattern) == false)
            {
                showValidationMessage("The last name may only contain letters, spaces, dots, dashes and apostrophes.", txtLastName);
                return false;
            }

            if (Database.getSelectedId(cmbDepartment) == 0)
            {
                showValidationMessage("Please select the course.", cmbDepartment);
                return false;
            }

            if (cmbYearLevel.SelectedIndex < 0)
            {
                showValidationMessage("Please select the year level.", cmbYearLevel);
                return false;
            }

            if (cmbGender.SelectedIndex < 0)
            {
                showValidationMessage("Please select the gender.", cmbGender);
                return false;
            }

            if (email != "" && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") == false)
            {
                showValidationMessage("Please enter a valid email address (example: juan@gmail.com).", txtEmail);
                return false;
            }

            if (contactNumber != "" && Regex.IsMatch(contactNumber, @"^(09\d{9}|\+639\d{9})$") == false)
            {
                showValidationMessage("Please enter a valid contact number (example: 09123456789 or +639123456789).", txtContact);
                return false;
            }

            if (position == "")
            {
                showValidationMessage("Please select or type the position of the officer.", cmbPosition);
                return false;
            }

            if (schoolYear == "")
            {
                showValidationMessage("Please enter the school year (example: " + getCurrentSchoolYear() + ").", txtSchoolYear);
                return false;
            }

            // The school year must look like 2026-2027 (the second year is the first year + 1).
            if (Regex.IsMatch(schoolYear, @"^\d{4}-\d{4}$") == false)
            {
                showValidationMessage("The school year must look like " + getCurrentSchoolYear() + ".", txtSchoolYear);
                return false;
            }

            int firstYear = Convert.ToInt32(schoolYear.Substring(0, 4));
            int secondYear = Convert.ToInt32(schoolYear.Substring(5, 4));

            if (secondYear != firstYear + 1)
            {
                showValidationMessage("The second year must be one year after the first year (example: " + getCurrentSchoolYear() + ").", txtSchoolYear);
                return false;
            }

            if (username == "")
            {
                showValidationMessage("Please enter the username that the officer will use to log in.", txtUsername);
                return false;
            }

            if (Regex.IsMatch(username, @"^[A-Za-z0-9_.]{4,50}$") == false)
            {
                showValidationMessage("The username must have at least 4 characters and may only contain letters, numbers, dots and underscores (no spaces).", txtUsername);
                return false;
            }

            // In edit mode an empty password means "keep the current password".
            bool passwordIsNeeded = false;
            if (officerIdToEdit == 0 || password != "" || confirmPassword != "")
            {
                passwordIsNeeded = true;
            }

            if (passwordIsNeeded == true)
            {
                if (password == "")
                {
                    showValidationMessage("Please enter the password.", txtPassword);
                    return false;
                }

                if (password.Length < 6)
                {
                    showValidationMessage("The password must have at least 6 characters.", txtPassword);
                    return false;
                }

                if (password.Contains(" "))
                {
                    showValidationMessage("The password must not contain spaces.", txtPassword);
                    return false;
                }

                if (password != confirmPassword)
                {
                    showValidationMessage("The passwords do not match. Please type the same password again.", txtConfirmPassword);
                    return false;
                }
            }

            return true;
        }

        // Checks the database for another officer with the same ID number, email or username.
        // Returns false when the officer must NOT be saved.
        private bool checkDuplicateOfficer()
        {
            Dictionary<string, object> numberParameters = new Dictionary<string, object>();
            numberParameters.Add("@number", txtOfficerNumber.Text.Trim());
            numberParameters.Add("@id", officerIdToEdit);

            int sameNumberCount = Database.GetCount("SELECT COUNT(*) FROM officers WHERE officer_number = @number AND officer_id <> @id", numberParameters);

            if (sameNumberCount > 0)
            {
                showValidationMessage("The ID number \"" + txtOfficerNumber.Text.Trim() + "\" is already used by another officer.", txtOfficerNumber);
                return false;
            }

            if (txtEmail.Text.Trim() != "")
            {
                Dictionary<string, object> emailParameters = new Dictionary<string, object>();
                emailParameters.Add("@email", txtEmail.Text.Trim());
                emailParameters.Add("@id", officerIdToEdit);

                int sameEmailCount = Database.GetCount("SELECT COUNT(*) FROM officers WHERE email = @email AND officer_id <> @id", emailParameters);

                if (sameEmailCount > 0)
                {
                    showValidationMessage("The email \"" + txtEmail.Text.Trim() + "\" is already used by another officer.", txtEmail);
                    return false;
                }
            }

            Dictionary<string, object> usernameParameters = new Dictionary<string, object>();
            usernameParameters.Add("@username", txtUsername.Text.Trim());
            usernameParameters.Add("@account", accountIdToEdit);

            int sameUsernameCount = Database.GetCount("SELECT COUNT(*) FROM accounts WHERE username = @username AND account_id <> @account", usernameParameters);

            if (sameUsernameCount > 0)
            {
                showValidationMessage("The username \"" + txtUsername.Text.Trim() + "\" is already taken. Please choose another username.", txtUsername);
                return false;
            }

            return true;
        }

        // ------------------------------------------------------------------
        // Saving
        // ------------------------------------------------------------------

        private void btnSaveOfficer_Click(object sender, EventArgs e)
        {
            if (validateOfficerInfo() == false)
            {
                return;
            }

            if (checkDuplicateOfficer() == false)
            {
                return;
            }

            string question = "Save this officer?";
            if (officerIdToEdit > 0)
            {
                question = "Save the changes to this officer?";
            }

            DialogResult answer = MessageBox.Show(question + "\n\nName: " + txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() +
                "\nPosition: " + cmbPosition.Text.Trim() + "\nUsername: " + txtUsername.Text.Trim(),
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            bool saved = saveOfficer();

            if (saved == true)
            {
                if (officerIdToEdit == 0)
                {
                    MessageBox.Show("The officer was added successfully!\n\nThe officer can now log in using the username \"" + txtUsername.Text.Trim() + "\".",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The officer was updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // Saves the login account and the officer information together.
        // A transaction is used so that both are saved, or nothing is saved when there is an error.
        private bool saveOfficer()
        {
            bool saved = false;

            object middleName = DBNull.Value;
            if (txtMiddleName.Text.Trim() != "")
            {
                middleName = txtMiddleName.Text.Trim();
            }

            object email = DBNull.Value;
            if (txtEmail.Text.Trim() != "")
            {
                email = txtEmail.Text.Trim();
            }

            object contactNumber = DBNull.Value;
            if (txtContact.Text.Trim() != "")
            {
                contactNumber = txtContact.Text.Trim();
            }

            MySqlConnection conn = Database.GetConnection();
            MySqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();

                int accountId = accountIdToEdit;

                if (officerIdToEdit == 0)
                {
                    // 1. Create the login account. The password is saved as a BCrypt hash.
                    MySqlCommand accountCmd = new MySqlCommand(
                        "INSERT INTO accounts (username, password, role) VALUES (@username, @password, 'Officer')", conn, transaction);
                    accountCmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    accountCmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(txtPassword.Text));
                    accountCmd.ExecuteNonQuery();

                    accountId = Convert.ToInt32(accountCmd.LastInsertedId);
                    accountCmd.Dispose();
                }
                else
                {
                    // 1. Update the login account. The password is only changed when a new one was typed.
                    MySqlCommand accountCmd;

                    if (txtPassword.Text != "")
                    {
                        accountCmd = new MySqlCommand("UPDATE accounts SET username = @username, password = @password WHERE account_id = @account", conn, transaction);
                        accountCmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(txtPassword.Text));
                    }
                    else
                    {
                        accountCmd = new MySqlCommand("UPDATE accounts SET username = @username WHERE account_id = @account", conn, transaction);
                    }

                    accountCmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    accountCmd.Parameters.AddWithValue("@account", accountId);
                    accountCmd.ExecuteNonQuery();
                    accountCmd.Dispose();
                }

                // 2. Save the officer information.
                string officerQuery;

                if (officerIdToEdit == 0)
                {
                    officerQuery = "INSERT INTO officers (account_id, officer_number, first_name, middle_name, last_name, department_id, year_level, gender, email, contact_number, position, school_year) " +
                                   "VALUES (@account, @number, @first, @middle, @last, @departmentId, @year, @gender, @email, @contact, @position, @schoolYear)";
                }
                else
                {
                    officerQuery = "UPDATE officers SET officer_number = @number, first_name = @first, middle_name = @middle, last_name = @last, " +
                                   "department_id = @departmentId, year_level = @year, gender = @gender, email = @email, contact_number = @contact, " +
                                   "position = @position, school_year = @schoolYear " +
                                   "WHERE officer_id = @officer";
                }

                MySqlCommand officerCmd = new MySqlCommand(officerQuery, conn, transaction);
                officerCmd.Parameters.AddWithValue("@account", accountId);
                officerCmd.Parameters.AddWithValue("@officer", officerIdToEdit);
                officerCmd.Parameters.AddWithValue("@number", txtOfficerNumber.Text.Trim());
                officerCmd.Parameters.AddWithValue("@first", txtFirstName.Text.Trim());
                officerCmd.Parameters.AddWithValue("@middle", middleName);
                officerCmd.Parameters.AddWithValue("@last", txtLastName.Text.Trim());
                officerCmd.Parameters.AddWithValue("@departmentId", Database.getSelectedId(cmbDepartment));
                officerCmd.Parameters.AddWithValue("@year", cmbYearLevel.Text);
                officerCmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                officerCmd.Parameters.AddWithValue("@email", email);
                officerCmd.Parameters.AddWithValue("@contact", contactNumber);
                officerCmd.Parameters.AddWithValue("@position", cmbPosition.Text.Trim());
                officerCmd.Parameters.AddWithValue("@schoolYear", txtSchoolYear.Text.Trim());
                officerCmd.ExecuteNonQuery();
                officerCmd.Dispose();

                transaction.Commit();
                saved = true;
            }
            catch (Exception ex)
            {
                // Undo everything that was saved before the error.
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception)
                    {
                        // The connection is already closed, so there is nothing to undo.
                    }
                }

                Database.showError("The officer was not saved.", ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return saved;
        }

        private void btnCancelOfficer_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
