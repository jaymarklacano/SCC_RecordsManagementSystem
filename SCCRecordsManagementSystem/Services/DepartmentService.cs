using System.Collections.Generic;
using System.Data;

namespace SCCRecordsManagementSystem
{
    // Database operations for departments/courses.
    public static class DepartmentService
    {
        public static DataTable GetAll()
        {
            string query = @"SELECT d.department_id, d.department_code, d.department_name, d.department_head,
                                    d.contact_email, d.academic_level, d.description,
                                    (SELECT COUNT(*) FROM students s WHERE s.department_id = d.department_id) AS student_count
                             FROM departments d
                             ORDER BY d.department_code";
            return Database.GetTable(query);
        }

        public static DataTable Search(string text)
        {
            string query = @"SELECT d.department_id, d.department_code, d.department_name, d.department_head,
                                    d.contact_email, d.academic_level, d.description,
                                    (SELECT COUNT(*) FROM students s WHERE s.department_id = d.department_id) AS student_count
                             FROM departments d
                             WHERE d.department_code LIKE @text OR d.department_name LIKE @text
                                OR d.department_head LIKE @text OR d.academic_level LIKE @text
                             ORDER BY d.department_code";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@text", "%" + text.Trim() + "%");
            return Database.GetTable(query, parameters);
        }

        public static Department GetById(int departmentId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", departmentId);

            DataTable table = Database.GetTable("SELECT * FROM departments WHERE department_id = @id", parameters);
            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            return new Department
            {
                DepartmentId = departmentId,
                DepartmentCode = row["department_code"].ToString(),
                DepartmentName = row["department_name"].ToString(),
                DepartmentHead = row["department_head"].ToString(),
                ContactEmail = row["contact_email"].ToString(),
                Description = row["description"].ToString(),
                AcademicLevel = row["academic_level"].ToString()
            };
        }

        // True if another department already uses this code or name.
        // excludeId is the department being edited (use 0 when adding a new one).
        public static bool CodeOrNameExists(string code, string name, int excludeId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@code", code.Trim());
            parameters.Add("@name", name.Trim());
            parameters.Add("@id", excludeId);

            string query = @"SELECT COUNT(*) FROM departments
                             WHERE (department_code = @code OR department_name = @name) AND department_id <> @id";
            return Database.GetCount(query, parameters) > 0;
        }

        public static void Add(Department department)
        {
            string query = @"INSERT INTO departments
                               (department_code, department_name, department_head, contact_email, description, academic_level)
                             VALUES (@code, @name, @head, @email, @description, @level)";
            Database.RunCommand(query, ToParameters(department));
        }

        public static void Update(Department department)
        {
            string query = @"UPDATE departments
                             SET department_code = @code, department_name = @name, department_head = @head,
                                 contact_email = @email, description = @description, academic_level = @level
                             WHERE department_id = @id";

            Dictionary<string, object> parameters = ToParameters(department);
            parameters.Add("@id", department.DepartmentId);
            Database.RunCommand(query, parameters);
        }

        public static int CountStudents(int departmentId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", departmentId);
            return Database.GetCount("SELECT COUNT(*) FROM students WHERE department_id = @id", parameters);
        }

        public static void Delete(int departmentId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", departmentId);
            Database.RunCommand("DELETE FROM departments WHERE department_id = @id", parameters);
        }

        private static Dictionary<string, object> ToParameters(Department department)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@code", department.DepartmentCode.Trim());
            parameters.Add("@name", department.DepartmentName.Trim());
            parameters.Add("@head", Database.TextOrNull(department.DepartmentHead));
            parameters.Add("@email", Database.TextOrNull(department.ContactEmail));
            parameters.Add("@description", Database.TextOrNull(department.Description));
            parameters.Add("@level", Database.TextOrNull(department.AcademicLevel));
            return parameters;
        }
    }
}
