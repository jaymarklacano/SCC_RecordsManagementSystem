using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCRecordsManagementSystem
{
    // One row of the "students" table.
    public class StudentInfo
    {
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string YearLevel { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Status { get; set; }
        public bool FaceRegistered { get; set; }
        public int FaceImageCount { get; set; }
        public DateTime CreatedAt { get; set; }

        // Example: "Juan Santos Dela Cruz"
        public string GetFullName()
        {
            string fullName = FirstName;

            if (MiddleName != null && MiddleName != "")
            {
                fullName = fullName + " " + MiddleName;
            }

            fullName = fullName + " " + LastName;
            return fullName;
        }
    }
}
