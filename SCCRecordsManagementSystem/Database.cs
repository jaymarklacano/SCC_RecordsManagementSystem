using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCRecordsManagementSystem
{
    public static class Database
    {
        // Update the Server, Uid, and Pwd if your local MySQL settings are different
        private static string connectionString = "Server=localhost;Database=scc_records_management;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static int AccountId;
        public static string Username = "";
        public static string Role = "";
        public static int LoginId;
        public static bool IsAdmin { get { return AccountId > 0 && Role == "Admin"; } }

        public static MySqlCommand Command(MySqlConnection connection, string sql,
            Dictionary<string, object> parameters = null, MySqlTransaction transaction = null)
        {
            MySqlCommand command = new MySqlCommand(sql, connection, transaction);
            if (parameters != null)
                foreach (KeyValuePair<string, object> item in parameters)
                    command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);
            return command;
        }

        public static DataTable GetData(string sql, Dictionary<string, object> parameters = null)
        {
            using (MySqlConnection connection = GetConnection())
            using (MySqlCommand command = Command(connection, sql, parameters))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static int Execute(string sql, Dictionary<string, object> parameters = null)
        {
            using (MySqlConnection connection = GetConnection())
            using (MySqlCommand command = Command(connection, sql, parameters))
            {
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static object Scalar(string sql, Dictionary<string, object> parameters = null)
        {
            using (MySqlConnection connection = GetConnection())
            using (MySqlCommand command = Command(connection, sql, parameters))
            {
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        public static bool CanUse(string page)
        {
            if (AccountId == 0) return false;
            if (IsAdmin) return true;
            if (page == "Dashboard") return true;
            return Convert.ToInt32(Scalar(
                "SELECT COUNT(*) FROM role_permissions WHERE module=@module AND allowed=1",
                new Dictionary<string, object> { { "@module", page } })) == 1;
        }

        public static void Require(string page)
        {
            if (!CanUse(page)) throw new UnauthorizedAccessException("Your account cannot open or change " + page + ".");
        }

        public static void RequireAdmin()
        {
            if (!IsAdmin) throw new UnauthorizedAccessException("Only an administrator can do this.");
        }

        public static object Optional(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? (object)DBNull.Value : text.Trim();
        }

        public static void CheckText(string text, string name, int maximum, bool required = true)
        {
            if ((required && string.IsNullOrWhiteSpace(text)) || (text ?? "").Trim().Length > maximum)
                throw new ArgumentException(name + (required ? " is required and" : "") + " must be at most " + maximum + " characters.");
        }

        public static void CheckContact(string email, string phone)
        {
            CheckText(email, "Email", 100, false);
            CheckText(phone, "Contact number", 20, false);
            if (!string.IsNullOrWhiteSpace(email))
            {
                try
                {
                    if (new MailAddress(email.Trim()).Address != email.Trim()) throw new FormatException();
                }
                catch (FormatException) { throw new ArgumentException("Enter a valid email address."); }
            }
            if (!string.IsNullOrWhiteSpace(phone) &&
                (!System.Text.RegularExpressions.Regex.IsMatch(phone.Trim(), @"^\+?[0-9 ()-]{7,20}$") ||
                 phone.Count(char.IsDigit) < 7))
                throw new ArgumentException("Enter a valid contact number (7-20 digits, spaces, +, - or parentheses).");
        }

        public static string ErrorMessage(Exception error)
        {
            System.Diagnostics.Trace.WriteLine(error);
            if (error is ArgumentException || error is InvalidOperationException ||
                error is UnauthorizedAccessException || error is FileNotFoundException ||
                error is InvalidDataException) return error.Message;
            MySqlException mysql = error as MySqlException;
            if (mysql != null)
            {
                if (mysql.Number == 1062) return "This ID, username, email or name already exists. Please check the entered information.";
                if (mysql.Number == 1451) return "This record is still used by other records and cannot be deleted.";
                if (mysql.Number == 1406) return "One of the entered values is too long.";
                return "The database operation failed. Check that MySQL is running, the SQL file was imported, and the connection settings are correct.";
            }
            if (error is DllNotFoundException || error is BadImageFormatException || error is TypeInitializationException)
                return "The camera library could not load. Use 64-bit Windows, restore NuGet packages and install the Visual C++ x64 redistributable.";
            return "The operation could not be completed. Retry, check the selected record, and contact the administrator if it continues.";
        }
    }
}
