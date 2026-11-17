using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SCCRecordsManagementSystem
{
    // Simple database helper. Every other class uses these methods to talk to MySQL.
    public static class Database
    {
        // CHANGE THIS if your MySQL settings are different (server, user, password, database name).
        private const string connectionString =
            "Server=localhost;Port=3306;Database=scc_records_management;Uid=root;Pwd=;";

        // Creates AND OPENS a connection. Always use it inside a "using" block so it is closed again.
        public static MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        // For INSERT, UPDATE and DELETE. Returns how many rows were changed.
        public static int RunCommand(string query, Dictionary<string, object> parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                AddParameters(cmd, parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        // For SELECT statements that return many rows (used by DataGridViews and ComboBoxes).
        public static DataTable GetTable(string query, Dictionary<string, object> parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                AddParameters(cmd, parameters);

                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
                return table;
            }
        }

        // For SELECT statements that return one value, such as COUNT(*). Returns null if nothing is found.
        public static object GetValue(string query, Dictionary<string, object> parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                AddParameters(cmd, parameters);

                object result = cmd.ExecuteScalar();
                if (result == DBNull.Value)
                {
                    return null;
                }
                return result;
            }
        }

        // Shortcut for COUNT(*) queries.
        public static int GetCount(string query, Dictionary<string, object> parameters = null)
        {
            object result = GetValue(query, parameters);
            return result == null ? 0 : Convert.ToInt32(result);
        }

        // Empty text is saved as NULL in the database (needed for optional UNIQUE columns like email).
        public static object TextOrNull(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return DBNull.Value;
            }
            return text.Trim();
        }

        // True when MySQL says "duplicate entry" (error 1062 = UNIQUE key violated).
        public static bool IsDuplicateError(Exception ex)
        {
            MySqlException mysqlError = ex as MySqlException;
            return mysqlError != null && mysqlError.Number == 1062;
        }

        // Turns a technical exception into a short message a normal user can understand.
        public static string GetFriendlyMessage(Exception ex)
        {
            MySqlException mysqlError = ex as MySqlException;

            if (mysqlError != null)
            {
                switch (mysqlError.Number)
                {
                    case 0:      // could not connect
                    case 1042:   // can't get host
                    case 2003:
                        return "Cannot connect to the database.\n\n" +
                               "Please make sure MySQL is running (start MySQL in the XAMPP Control Panel) " +
                               "and that the connection settings in Database.cs are correct.";
                    case 1045:
                        return "The database username or password is wrong.\n\nPlease check the connection settings in Database.cs.";
                    case 1049:
                        return "The database 'scc_records_management' was not found.\n\n" +
                               "Please import scc_records_management.sql first (see README.md).";
                    case 1146:
                        return "A database table is missing.\n\nPlease import scc_records_management.sql again (see README.md).";
                    case 1062:
                        return "This record already exists in the database.";
                    case 1451:
                        return "This record is still used by other records, so it cannot be deleted.";
                    default:
                        return "A database error occurred. Please try again.";
                }
            }

            return "Something went wrong. Please try again.";
        }

        private static void AddParameters(MySqlCommand cmd, Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return;
            }

            foreach (KeyValuePair<string, object> param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
        }
    }
}
