using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SCCRecordsManagementSystem
{
    public static class OfficerService
    {
        public static DataTable Search(string text)
        {
            EnsureAdmin();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@text", "%" + (text ?? "").Trim() + "%");
            return Database.GetTable(
                @"SELECT o.officer_id, o.account_id, o.officer_number, o.first_name, o.middle_name,
                         o.last_name, o.department_id,
                         CONCAT(o.last_name, ', ', o.first_name, IF(o.middle_name IS NULL, '', CONCAT(' ', o.middle_name))) AS full_name,
                         d.department_code, o.year_level, o.gender, o.email, o.contact_number, o.position,
                         o.school_year, a.username
                  FROM officers o
                  INNER JOIN accounts a ON a.account_id = o.account_id
                  INNER JOIN departments d ON d.department_id = o.department_id
                  WHERE o.officer_number LIKE @text OR o.first_name LIKE @text OR o.middle_name LIKE @text
                     OR o.last_name LIKE @text OR o.position LIKE @text OR a.username LIKE @text
                  ORDER BY o.last_name, o.first_name", parameters);
        }

        public static void Create(string username, string password, string officerNumber, string firstName,
            string middleName, string lastName, int departmentId, string yearLevel, string gender,
            string email, string contactNumber, string position, string schoolYear)
        {
            EnsureAdmin();
            Validate(username, password, true, officerNumber, firstName, lastName, departmentId, yearLevel, gender, position, schoolYear);
            using (MySqlConnection connection = Database.GetConnection())
            using (MySqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int accountId;
                    using (MySqlCommand account = new MySqlCommand(
                        "INSERT INTO accounts (username, password, role) VALUES (@username, @password, 'Officer')",
                        connection, transaction))
                    {
                        account.Parameters.AddWithValue("@username", username.Trim());
                        account.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(password));
                        account.ExecuteNonQuery();
                        accountId = Convert.ToInt32(account.LastInsertedId);
                    }

                    using (MySqlCommand officer = new MySqlCommand(
                        @"INSERT INTO officers
                          (account_id, officer_number, first_name, middle_name, last_name, department_id,
                           year_level, gender, email, contact_number, position, school_year)
                          VALUES (@account, @number, @first, @middle, @last, @department,
                           @year, @gender, @email, @contact, @position, @schoolYear)",
                        connection, transaction))
                    {
                        AddOfficerParameters(officer, officerNumber, firstName, middleName, lastName, departmentId,
                            yearLevel, gender, email, contactNumber, position, schoolYear);
                        officer.Parameters.AddWithValue("@account", accountId);
                        officer.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void Update(int officerId, int accountId, string username, string newPassword,
            string officerNumber, string firstName, string middleName, string lastName, int departmentId,
            string yearLevel, string gender, string email, string contactNumber, string position, string schoolYear)
        {
            EnsureAdmin();
            Validate(username, newPassword, false, officerNumber, firstName, lastName, departmentId, yearLevel, gender, position, schoolYear);
            using (MySqlConnection connection = Database.GetConnection())
            using (MySqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (MySqlCommand account = new MySqlCommand(
                        "UPDATE accounts SET username = @username WHERE account_id = @account",
                        connection, transaction))
                    {
                        account.Parameters.AddWithValue("@username", username.Trim());
                        account.Parameters.AddWithValue("@account", accountId);
                        account.ExecuteNonQuery();
                    }
                    if (!string.IsNullOrWhiteSpace(newPassword))
                    {
                        using (MySqlCommand password = new MySqlCommand(
                            "UPDATE accounts SET password = @password WHERE account_id = @account",
                            connection, transaction))
                        {
                            password.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(newPassword));
                            password.Parameters.AddWithValue("@account", accountId);
                            password.ExecuteNonQuery();
                        }
                    }
                    using (MySqlCommand officer = new MySqlCommand(
                        @"UPDATE officers SET officer_number = @number, first_name = @first,
                          middle_name = @middle, last_name = @last, department_id = @department,
                          year_level = @year, gender = @gender, email = @email, contact_number = @contact,
                          position = @position, school_year = @schoolYear WHERE officer_id = @id",
                        connection, transaction))
                    {
                        AddOfficerParameters(officer, officerNumber, firstName, middleName, lastName, departmentId,
                            yearLevel, gender, email, contactNumber, position, schoolYear);
                        officer.Parameters.AddWithValue("@id", officerId);
                        officer.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void Delete(int officerId)
        {
            EnsureAdmin();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", officerId);
            Database.RunCommand(
                "DELETE a FROM accounts a INNER JOIN officers o ON o.account_id = a.account_id WHERE o.officer_id = @id",
                parameters);
        }

        private static void EnsureAdmin()
        {
            if (!Session.IsAdmin)
            {
                throw new UnauthorizedAccessException("Only an administrator can manage officer records.");
            }
        }

        private static void Validate(string username, string password, bool requirePassword, string number, string firstName,
            string lastName, int departmentId, string yearLevel, string gender, string position, string schoolYear)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(number) ||
                string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                departmentId <= 0 || string.IsNullOrWhiteSpace(yearLevel) ||
                string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(position) ||
                string.IsNullOrWhiteSpace(schoolYear))
            {
                throw new ArgumentException("Complete all required officer details.");
            }
            if (requirePassword && string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Enter a password for the new officer account.");
            }
            if (!string.IsNullOrWhiteSpace(password) && password.Length < 6)
            {
                throw new ArgumentException("Passwords must contain at least 6 characters.");
            }
        }

        private static void AddOfficerParameters(MySqlCommand command, string number, string firstName,
            string middleName, string lastName, int departmentId, string yearLevel, string gender,
            string email, string contactNumber, string position, string schoolYear)
        {
            command.Parameters.AddWithValue("@number", number.Trim());
            command.Parameters.AddWithValue("@first", firstName.Trim());
            command.Parameters.AddWithValue("@middle", Database.TextOrNull(middleName));
            command.Parameters.AddWithValue("@last", lastName.Trim());
            command.Parameters.AddWithValue("@department", departmentId);
            command.Parameters.AddWithValue("@year", yearLevel);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@email", Database.TextOrNull(email));
            command.Parameters.AddWithValue("@contact", Database.TextOrNull(contactNumber));
            command.Parameters.AddWithValue("@position", position.Trim());
            command.Parameters.AddWithValue("@schoolYear", schoolYear.Trim());
        }
    }
}
