using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SCCRecordsManagementSystem
{
    // Database operations for login accounts.
    public static class AccountService
    {
        // Returns true when the username exists and the password matches the saved BCrypt hash.
        public static bool Login(string username, string password)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@username", username.Trim());

            DataTable accounts = Database.GetTable(
                "SELECT account_id, password, role FROM accounts WHERE username = @username", parameters);
            if (accounts.Rows.Count == 0)
            {
                return false;
            }

            DataRow account = accounts.Rows[0];
            if (!BCrypt.Net.BCrypt.Verify(password, account["password"].ToString()))
            {
                return false;
            }

            Session.AccountId = Convert.ToInt32(account["account_id"]);
            Session.Role = account["role"].ToString();
            return true;
        }

        public static bool UsernameExists(string username)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@username", username.Trim());
            return Database.GetCount("SELECT COUNT(*) FROM accounts WHERE username = @username", parameters) > 0;
        }

        public static void CreateAccount(string username, string password)
        {
            if (!Session.IsAdmin)
            {
                throw new UnauthorizedAccessException("Only an administrator can create accounts.");
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@username", username.Trim());
            parameters.Add("@password", BCrypt.Net.BCrypt.HashPassword(password));
            parameters.Add("@role", "Admin");
            Database.RunCommand("INSERT INTO accounts (username, password, role) VALUES (@username, @password, @role)", parameters);
        }

    }
}
