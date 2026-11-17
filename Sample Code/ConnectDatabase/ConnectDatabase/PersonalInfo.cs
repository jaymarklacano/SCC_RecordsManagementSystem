using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectDatabase
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        public class RecordChecker
        {
            public int MaxID { get; set; }
            public int MaxEducID { get; set; } // Make sure this exists
            public int MaxJobID { get; set; } // Make sure this exists
        }

        public class Instantiate
        {
            public static PersonalInfo personalInfo = new PersonalInfo();
            public static RecordChecker recordChecker = new RecordChecker();
        }

        public class EducAttainment
        {
            public int EducId { get; set; }
            public int PersonalId { get; set; }
            public string School { get; set; }
            public string Degree { get; set; }
            public string YearGraduated { get; set; }
            public DateTime Date { get; set; }
        }

        public class JobInfo
        {
            public int JobId { get; set; }
            public int PersonalId { get; set; }
            public string Employee { get; set; }
            public string Address { get; set; }
            public string Position { get; set; }
            public DateTime DateHired { get; set; }
            public DateTime DateEnd { get; set; }
            public string Status { get; set; }
        }
    }
}
