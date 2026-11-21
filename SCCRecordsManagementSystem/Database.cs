using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SCCRecordsManagementSystem.AccountInfo;

namespace SCCRecordsManagementSystem
{
    public static class Database
    {
        // Update the Server, Uid, and Pwd if your local MySQL settings are different
        private static string connectionString = "Server=localhost;Database=scc_management;Uid=root;Pwd=;";

        // Returns a NEW connection that is NOT opened yet.
        // Always call conn.Open() before using it.
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // Changes a technical error into a message that a normal user can understand.
        public static string friendlyMessage(Exception ex)
        {
            MySqlException mysqlError = ex as MySqlException;

            if (mysqlError == null && ex.InnerException != null)
            {
                mysqlError = ex.InnerException as MySqlException;
            }

            if (mysqlError != null)
            {
                if (mysqlError.Number == 1042 || mysqlError.Message.Contains("Unable to connect"))
                {
                    return "Cannot connect to the database server.\nPlease open XAMPP and START the MySQL module, then try again.";
                }
                else if (mysqlError.Number == 1045)
                {
                    return "The database username or password is wrong.\nCheck the connection string inside Database.cs.";
                }
                else if (mysqlError.Number == 1049)
                {
                    return "The database \"scc_management\" was not found.\nPlease import the SQL file in phpMyAdmin first.";
                }
                else if (mysqlError.Number == 1146)
                {
                    return "A table is missing in the database.\nPlease import the latest SQL file in phpMyAdmin.";
                }
                else if (mysqlError.Number == 1062)
                {
                    return "This record already exists (duplicate value).";
                }
                else if (mysqlError.Number == 1451)
                {
                    return "This record cannot be deleted or changed because other records are still using it.";
                }
                else if (mysqlError.Number == 1452)
                {
                    return "The selected item does not exist anymore. Please refresh and try again.";
                }
                else if (mysqlError.Number == 1406)
                {
                    return "One of the values is too long.";
                }
                else if (mysqlError.Number == 3819 || mysqlError.Number == 4025)
                {
                    return "One of the values is not valid (it failed a database rule).";
                }

                return "Database error: " + mysqlError.Message;
            }

            return "Something went wrong: " + ex.Message;
        }

        // Shows a simple error message box.
        // Example:  Database.showError("Could not load the students.", ex);
        public static void showError(string whatHappened, Exception ex)
        {
            MessageBox.Show(whatHappened + "\n\n" + friendlyMessage(ex),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Copies the values of the dictionary into the command.
        // The special key "@command" is only used for the success message and is skipped.
        private static void addParameters(MySqlCommand cmd, Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return;
            }

            foreach (KeyValuePair<string, object> param in parameters)
            {
                if (param.Key == "@command")
                {
                    continue;
                }

                object value = param.Value;

                if (value == null)
                {
                    value = DBNull.Value;
                }

                cmd.Parameters.AddWithValue(param.Key, value);
            }
        }

        // Runs an INSERT, UPDATE or DELETE query.
        // Returns true when at least one row was changed.
        // Put "@command" = "insert" / "update" / "delete" (or your own message)
        // inside the dictionary to show a success message.
        public static bool saveUpdateDeleteData(string query, Dictionary<string, object> parameters)
        {
            bool success = false;
            MySqlConnection conn = GetConnection();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                addParameters(cmd, parameters);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    success = true;

                    if (parameters != null && parameters.ContainsKey("@command"))
                    {
                        object value = parameters["@command"];
                        displayInsertUpdateDelete(value);
                    }
                }
                else
                {
                    MessageBox.Show("No records were affected. The record may have been deleted already.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                showError("The changes were not saved.", ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return success;
        }

        public static void displayInsertUpdateDelete(object value)
        {
            string command = value as string;

            if (command == null)
            {
                return;
            }

            string message;

            // StringComparison.OrdinalIgnoreCase
            // accepts "insert", "INSERT", "Insert", etc.
            if (command.Equals("insert", StringComparison.OrdinalIgnoreCase))
            {
                message = "Record saved successfully!";
            }
            else if (command.Equals("update", StringComparison.OrdinalIgnoreCase))
            {
                message = "Record updated successfully!";
            }
            else if (command.Equals("delete", StringComparison.OrdinalIgnoreCase))
            {
                message = "Record deleted successfully!";
            }
            else
            {
                // Any other text is shown as it is.
                message = command;
            }

            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Runs a SELECT query and returns the rows as a DataTable.
        // When there is an error, an empty table is returned and a message is shown.
        public static DataTable GetData(string query, Dictionary<string, object> parameters)
        {
            DataTable table = new DataTable();

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        addParameters(cmd, parameters);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(table);
                        }
                    }
                }
                catch (Exception ex)
                {
                    showError("Could not load the records.", ex);
                }
            }

            return table;
        }

        // Runs a SELECT query that returns only ONE value (example: SELECT COUNT(*) ...).
        // Returns null when nothing was found or when there was an error.
        public static object GetValue(string query, Dictionary<string, object> parameters)
        {
            object value = null;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        addParameters(cmd, parameters);
                        value = cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    showError("Could not read from the database.", ex);
                }
            }

            if (value == DBNull.Value)
            {
                value = null;
            }

            return value;
        }

        // Same as GetValue but always returns a number (0 when nothing was found).
        public static int GetCount(string query, Dictionary<string, object> parameters)
        {
            object value = GetValue(query, parameters);

            if (value == null)
            {
                return 0;
            }

            return Convert.ToInt32(value);
        }

        // Fills a ComboBox with the departments.
        // firstItemText: for example "All Departments". Use "" if you do not want a first item.
        // The value of the first item is 0.
        public static void loadDepartmentComboBox(ComboBox comboBox, string firstItemText)
        {
            DataTable table = GetData("SELECT department_id, department_code FROM departments ORDER BY department_code", null);

            if (table.Columns.Count == 0)
            {
                comboBox.DataSource = null;
                return;
            }

            if (firstItemText != "")
            {
                DataRow firstRow = table.NewRow();
                firstRow["department_id"] = 0;
                firstRow["department_code"] = firstItemText;
                table.Rows.InsertAt(firstRow, 0);
            }

            // DisplayMember and ValueMember must be set BEFORE the DataSource.
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.DisplayMember = "department_code";
            comboBox.ValueMember = "department_id";
            comboBox.DataSource = table;

            if (table.Rows.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        // Fills a ComboBox with the events.
        // The value of the first item ("All Events" for example) is 0.
        public static void loadEventComboBox(ComboBox comboBox, string firstItemText)
        {
            DataTable table = GetData(
                "SELECT event_id, CONCAT(event_name, ' (', DATE_FORMAT(start_date, '%b %d, %Y'), ')') AS event_display " +
                "FROM events ORDER BY start_date DESC, event_name", null);

            if (table.Columns.Count == 0)
            {
                comboBox.DataSource = null;
                return;
            }

            if (firstItemText != "")
            {
                DataRow firstRow = table.NewRow();
                firstRow["event_id"] = 0;
                firstRow["event_display"] = firstItemText;
                table.Rows.InsertAt(firstRow, 0);
            }

            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.DisplayMember = "event_display";
            comboBox.ValueMember = "event_id";
            comboBox.DataSource = table;

            if (table.Rows.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        // Returns the selected id of a ComboBox that was filled by a DataTable.
        // Returns 0 when nothing is selected.
        public static int getSelectedId(ComboBox comboBox)
        {
            if (comboBox.SelectedValue == null)
            {
                return 0;
            }

            int id = 0;
            int.TryParse(comboBox.SelectedValue.ToString(), out id);
            return id;
        }

        // Checks if the logged in user may open a menu.
        // The Admin may open everything. Officers follow the "role_permissions" table.
        public static bool isAllowed(string module)
        {
            string role = Instantiate.currentUser.Role;

            if (role == "Admin")
            {
                return true;
            }

            // These menus are always for the Admin only.
            if (module == "Rules and Permission" || module == "Login History")
            {
                MessageBox.Show("Only the Admin can open " + module + ".",
                    "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@role", role);
            parameters.Add("@module", module);

            object allowed = GetValue("SELECT allowed FROM role_permissions WHERE role = @role AND module = @module", parameters);

            if (allowed != null && Convert.ToInt32(allowed) == 1)
            {
                return true;
            }

            MessageBox.Show("Your account is not allowed to open " + module + ".\nPlease ask the Admin to give you permission.",
                "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // Saves the log out time of the current user in the login history.
        public static void saveLogoutTime()
        {
            if (Instantiate.currentUser.LoginId > 0)
            {
                using (MySqlConnection conn = GetConnection())
                {
                    try
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand("UPDATE login_history SET logged_out = NOW() WHERE login_id = @id", conn))
                        {
                            cmd.Parameters.AddWithValue("@id", Instantiate.currentUser.LoginId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {
                        // Not important enough to stop the user from logging out,
                        // so the error is ignored on purpose.
                    }
                }
            }

            Instantiate.currentUser = new AccountInfo();
        }

        // Called by the FormClosed event of every page.
        // When the user clicks X on the last visible window, the whole program stops.
        // (When we move to another page, the new page is shown first, so it stays open.)
        public static void exitIfNoFormIsOpen(Form closingForm)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form != closingForm && form.Visible == true)
                {
                    return;
                }
            }

            saveLogoutTime();
            Application.Exit();
        }

        // Returns one student (or null when the student was not found).
        public static StudentInfo searchAndGetStudentInfo(int studentId)
        {
            StudentInfo studentInfo = null;

            string query = "SELECT s.*, d.department_code, d.department_name, " +
                           "(SELECT COUNT(*) FROM face_images f WHERE f.student_id = s.student_id) AS face_count " +
                           "FROM students s JOIN departments d ON d.department_id = s.department_id " +
                           "WHERE s.student_id = @id";

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", studentId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentInfo = new StudentInfo();
                                studentInfo.StudentId = Convert.ToInt32(reader["student_id"]);
                                studentInfo.StudentNumber = reader["student_number"].ToString();
                                studentInfo.FirstName = reader["first_name"].ToString();
                                studentInfo.MiddleName = reader["middle_name"].ToString();
                                studentInfo.LastName = reader["last_name"].ToString();
                                studentInfo.DepartmentId = Convert.ToInt32(reader["department_id"]);
                                studentInfo.DepartmentCode = reader["department_code"].ToString();
                                studentInfo.DepartmentName = reader["department_name"].ToString();
                                studentInfo.YearLevel = reader["year_level"].ToString();
                                studentInfo.Gender = reader["gender"].ToString();
                                studentInfo.Email = reader["email"].ToString();
                                studentInfo.ContactNumber = reader["contact_number"].ToString();
                                studentInfo.Status = reader["status"].ToString();
                                studentInfo.FaceRegistered = Convert.ToInt32(reader["face_registered"]) == 1;
                                studentInfo.FaceImageCount = Convert.ToInt32(reader["face_count"]);
                                studentInfo.CreatedAt = Convert.ToDateTime(reader["created_at"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    showError("Could not load the student.", ex);
                }
            }

            return studentInfo;
        }

        // Returns one officer (or null when the officer was not found).
        public static OfficerInfo searchAndGetOfficerInfo(int officerId)
        {
            OfficerInfo officerInfo = null;

            string query = "SELECT o.*, a.username, d.department_code " +
                           "FROM officers o " +
                           "JOIN accounts a ON a.account_id = o.account_id " +
                           "JOIN departments d ON d.department_id = o.department_id " +
                           "WHERE o.officer_id = @id";

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", officerId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                officerInfo = new OfficerInfo();
                                officerInfo.OfficerId = Convert.ToInt32(reader["officer_id"]);
                                officerInfo.AccountId = Convert.ToInt32(reader["account_id"]);
                                officerInfo.Username = reader["username"].ToString();
                                officerInfo.OfficerNumber = reader["officer_number"].ToString();
                                officerInfo.FirstName = reader["first_name"].ToString();
                                officerInfo.MiddleName = reader["middle_name"].ToString();
                                officerInfo.LastName = reader["last_name"].ToString();
                                officerInfo.DepartmentId = Convert.ToInt32(reader["department_id"]);
                                officerInfo.DepartmentCode = reader["department_code"].ToString();
                                officerInfo.YearLevel = reader["year_level"].ToString();
                                officerInfo.Gender = reader["gender"].ToString();
                                officerInfo.Email = reader["email"].ToString();
                                officerInfo.ContactNumber = reader["contact_number"].ToString();
                                officerInfo.Position = reader["position"].ToString();
                                officerInfo.SchoolYear = reader["school_year"].ToString();
                                officerInfo.CreatedAt = Convert.ToDateTime(reader["created_at"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    showError("Could not load the officer.", ex);
                }
            }

            return officerInfo;
        }

        // Returns one event (or null when the event was not found).
        public static EventInfo searchAndGetEventInfo(int eventId)
        {
            EventInfo eventInfo = null;

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM events WHERE event_id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", eventId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                eventInfo = new EventInfo();
                                eventInfo.EventId = Convert.ToInt32(reader["event_id"]);
                                eventInfo.EventName = reader["event_name"].ToString();
                                eventInfo.Venue = reader["venue"].ToString();
                                eventInfo.Organizer = reader["organizer"].ToString();
                                eventInfo.StartDate = Convert.ToDateTime(reader["start_date"]);
                                eventInfo.EndDate = Convert.ToDateTime(reader["end_date"]);

                                if (reader["department_id"] == DBNull.Value)
                                {
                                    eventInfo.DepartmentId = 0;
                                }
                                else
                                {
                                    eventInfo.DepartmentId = Convert.ToInt32(reader["department_id"]);
                                }

                                eventInfo.YearLevel = reader["year_level"].ToString();
                                eventInfo.RequiresLogout = Convert.ToInt32(reader["requires_logout"]) == 1;

                                eventInfo.MorningEnabled = Convert.ToInt32(reader["morning_enabled"]) == 1;
                                eventInfo.MorningStart = (TimeSpan)reader["morning_start"];
                                eventInfo.MorningEnd = (TimeSpan)reader["morning_end"];

                                eventInfo.AfternoonEnabled = Convert.ToInt32(reader["afternoon_enabled"]) == 1;
                                eventInfo.AfternoonStart = (TimeSpan)reader["afternoon_start"];
                                eventInfo.AfternoonEnd = (TimeSpan)reader["afternoon_end"];

                                eventInfo.EveningEnabled = Convert.ToInt32(reader["evening_enabled"]) == 1;
                                eventInfo.EveningStart = (TimeSpan)reader["evening_start"];
                                eventInfo.EveningEnd = (TimeSpan)reader["evening_end"];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    showError("Could not load the event.", ex);
                }
            }

            return eventInfo;
        }
    }
}
