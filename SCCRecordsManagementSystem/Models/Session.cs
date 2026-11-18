namespace SCCRecordsManagementSystem
{
    // Remembers who is logged in while the program is running.
    public static class Session
    {
        public static string Username = "";
        public static string Role = "";
        public static int AccountId;

        public static bool IsAdmin
        {
            get { return string.Equals(Role, "Admin", System.StringComparison.OrdinalIgnoreCase); }
        }
    }
}
