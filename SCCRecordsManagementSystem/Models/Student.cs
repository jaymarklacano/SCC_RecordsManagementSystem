namespace SCCRecordsManagementSystem
{
    // Holds the information of one student (one row of the "students" table).
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string YearLevel { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Status { get; set; }
        public bool FaceRegistered { get; set; }

        // Example: "Juan M. Dela Cruz"
        public string FullName
        {
            get
            {
                string middle = string.IsNullOrWhiteSpace(MiddleName) ? "" : " " + MiddleName.Trim();
                return FirstName + middle + " " + LastName;
            }
        }
    }
}
