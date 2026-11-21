using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCRecordsManagementSystem
{
    // Holds the information of the person who is logged in (Admin or Officer).
    public class AccountInfo
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public int LoginId { get; set; }

        public class Instantiate
        {
            // The account that is currently logged in.
            // Any form can read it with:  Instantiate.currentUser.Username
            public static AccountInfo currentUser = new AccountInfo();
        }
    }
}
