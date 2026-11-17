using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    }
}
