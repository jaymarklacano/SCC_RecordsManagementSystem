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

            object savedHash = Database.GetValue("SELECT password FROM accounts WHERE username = @username", parameters);
            if (savedHash == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, savedHash.ToString());
        }

        public static bool UsernameExists(string username)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@username", username.Trim());
            return Database.GetCount("SELECT COUNT(*) FROM accounts WHERE username = @username", parameters) > 0;
        }

        public static void CreateAccount(string username, string password)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@username", username.Trim());
            parameters.Add("@password", BCrypt.Net.BCrypt.HashPassword(password));
            Database.RunCommand("INSERT INTO accounts (username, password) VALUES (@username, @password)", parameters);
        }

        // Returns false when no account has this username.
        public static bool ResetPassword(string username, string newPassword)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@username", username.Trim());
            parameters.Add("@password", BCrypt.Net.BCrypt.HashPassword(newPassword));
            return Database.RunCommand("UPDATE accounts SET password = @password WHERE username = @username", parameters) > 0;
        }
    }
}
