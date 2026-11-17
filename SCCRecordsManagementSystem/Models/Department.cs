namespace SCCRecordsManagementSystem
{
    // Holds the information of one department/course (one row of the "departments" table).
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentHead { get; set; }
        public string ContactEmail { get; set; }
        public string Description { get; set; }
        public string AcademicLevel { get; set; }
    }
}
