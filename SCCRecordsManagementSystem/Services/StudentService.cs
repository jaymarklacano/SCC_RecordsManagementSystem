using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SCCRecordsManagementSystem
{
    // Database operations for students and their 5 face pictures.
    public static class StudentService
    {
        // A student must have exactly this many face pictures.
        public const int RequiredFaceImages = 5;

        // The choices shown in the combo boxes of the forms
        public static readonly string[] YearLevels = { "1st Year", "2nd Year", "3rd Year", "4th Year" };
        public static readonly string[] Genders = { "Male", "Female" };

        // Gets the students for the grid. Every filter is optional:
        // searchText "" = no search, departmentId 0 = all courses,
        // yearLevel "" = all years, faceFilter "All", "Registered" or "Not Registered".
        public static DataTable Search(string searchText, int departmentId, string yearLevel, string faceFilter)
        {
            string query = @"SELECT s.student_id, s.student_number,
                                    CONCAT(s.last_name, ', ', s.first_name, IF(s.middle_name IS NULL, '', CONCAT(' ', s.middle_name))) AS full_name,
                                    d.department_code, s.year_level, s.gender, s.email, s.contact_number, s.status,
                                    IF(s.face_registered = 1, 'Registered', 'Not Registered') AS face_status
                             FROM students s
                             INNER JOIN departments d ON d.department_id = s.department_id
                             WHERE (@text = '' OR s.student_number LIKE @like
                                    OR CONCAT_WS(' ', s.first_name, s.middle_name, s.last_name) LIKE @like
                                    OR CONCAT(s.last_name, ' ', s.first_name) LIKE @like
                                    OR s.email LIKE @like OR s.contact_number LIKE @like)
                               AND (@dept = 0 OR s.department_id = @dept)
                               AND (@year = '' OR s.year_level = @year)
                               AND (@face = 'All' OR (@face = 'Registered' AND s.face_registered = 1)
                                                  OR (@face = 'Not Registered' AND s.face_registered = 0))
                             ORDER BY s.last_name, s.first_name";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@text", searchText.Trim());
            parameters.Add("@like", "%" + searchText.Trim() + "%");
            parameters.Add("@dept", departmentId);
            parameters.Add("@year", yearLevel);
            parameters.Add("@face", faceFilter);
            return Database.GetTable(query, parameters);
        }

        public static int CountAll()
        {
            return Database.GetCount("SELECT COUNT(*) FROM students");
        }

        // Students for the ComboBoxes: student_id and a text such as "2026-001 - Dela Cruz, Juan".
        public static DataTable GetStudentChoices()
        {
            string query = @"SELECT student_id,
                                    CONCAT(student_number, ' - ', last_name, ', ', first_name) AS display_name
                             FROM students
                             ORDER BY last_name, first_name";
            return Database.GetTable(query);
        }

        public static Student GetById(int studentId)
        {
            string query = @"SELECT s.*, d.department_code
                             FROM students s
                             INNER JOIN departments d ON d.department_id = s.department_id
                             WHERE s.student_id = @id";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", studentId);

            DataTable table = Database.GetTable(query, parameters);
            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            return new Student
            {
                StudentId = studentId,
                StudentNumber = row["student_number"].ToString(),
                FirstName = row["first_name"].ToString(),
                MiddleName = row["middle_name"].ToString(),
                LastName = row["last_name"].ToString(),
                DepartmentId = Convert.ToInt32(row["department_id"]),
                DepartmentCode = row["department_code"].ToString(),
                YearLevel = row["year_level"].ToString(),
                Gender = row["gender"].ToString(),
                Email = row["email"].ToString(),
                ContactNumber = row["contact_number"].ToString(),
                Status = row["status"].ToString(),
                FaceRegistered = Convert.ToInt32(row["face_registered"]) == 1
            };
        }

        // ---------- Duplicate checks (excludeId = the student being edited, 0 when adding) ----------

        public static bool StudentNumberExists(string studentNumber, int excludeId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@number", studentNumber.Trim());
            parameters.Add("@id", excludeId);
            return Database.GetCount("SELECT COUNT(*) FROM students WHERE student_number = @number AND student_id <> @id", parameters) > 0;
        }

        public static bool EmailExists(string email, int excludeId)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@email", email.Trim());
            parameters.Add("@id", excludeId);
            return Database.GetCount("SELECT COUNT(*) FROM students WHERE email = @email AND student_id <> @id", parameters) > 0;
        }

        // Same first, middle and last name as another student (different people can share a name,
        // so the form only asks the user to confirm).
        public static bool FullNameExists(Student student, int excludeId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@first", student.FirstName.Trim());
            parameters.Add("@middle", (student.MiddleName ?? "").Trim());
            parameters.Add("@last", student.LastName.Trim());
            parameters.Add("@id", excludeId);

            string query = @"SELECT COUNT(*) FROM students
                             WHERE first_name = @first AND last_name = @last
                               AND IFNULL(middle_name, '') = @middle AND student_id <> @id";
            return Database.GetCount(query, parameters) > 0;
        }

        // ---------- Add / Update / Delete ----------

        // Saves a new student together with the 5 face pictures.
        // Everything is saved in ONE transaction: if anything fails, nothing is saved.
        public static int AddStudent(Student student, List<byte[]> faceImages)
        {
            CheckFaceImageCount(faceImages);

            using (MySqlConnection conn = Database.GetConnection())
            using (MySqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    string query = @"INSERT INTO students
                                       (student_number, first_name, middle_name, last_name, department_id,
                                        year_level, gender, email, contact_number, status, face_registered)
                                     VALUES (@number, @first, @middle, @last, @dept,
                                             @year, @gender, @email, @contact, @status, 1)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                    {
                        AddStudentParameters(cmd, student);
                        cmd.ExecuteNonQuery();
                        student.StudentId = Convert.ToInt32(cmd.LastInsertedId);
                    }

                    SaveFaceImages(conn, transaction, student.StudentId, faceImages);
                    transaction.Commit();
                    return student.StudentId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Updates a student. Pass newFaceImages = null to keep the old face pictures,
        // or a list of 5 pictures to replace them (face re-registration).
        public static void UpdateStudent(Student student, List<byte[]> newFaceImages)
        {
            if (newFaceImages != null)
            {
                CheckFaceImageCount(newFaceImages);
            }

            using (MySqlConnection conn = Database.GetConnection())
            using (MySqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    string query = @"UPDATE students
                                     SET student_number = @number, first_name = @first, middle_name = @middle,
                                         last_name = @last, department_id = @dept, year_level = @year,
                                         gender = @gender, email = @email, contact_number = @contact, status = @status
                                     WHERE student_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                    {
                        AddStudentParameters(cmd, student);
                        cmd.Parameters.AddWithValue("@id", student.StudentId);
                        cmd.ExecuteNonQuery();
                    }

                    if (newFaceImages != null)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("DELETE FROM face_images WHERE student_id = @id", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@id", student.StudentId);
                            cmd.ExecuteNonQuery();
                        }

                        SaveFaceImages(conn, transaction, student.StudentId, newFaceImages);

                        using (MySqlCommand cmd = new MySqlCommand("UPDATE students SET face_registered = 1 WHERE student_id = @id", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@id", student.StudentId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Deleting a student also deletes the face pictures and attendance records
        // (the database does this automatically with ON DELETE CASCADE).
        public static void DeleteStudent(int studentId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", studentId);
            Database.RunCommand("DELETE FROM students WHERE student_id = @id", parameters);
        }

        // ---------- Face pictures ----------

        // The 5 saved pictures of one student (JPEG bytes), ordered 1 to 5.
        public static List<byte[]> GetFaceImages(int studentId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", studentId);

            DataTable table = Database.GetTable(
                "SELECT image_data FROM face_images WHERE student_id = @id ORDER BY image_number", parameters);

            List<byte[]> images = new List<byte[]>();
            foreach (DataRow row in table.Rows)
            {
                images.Add((byte[])row["image_data"]);
            }
            return images;
        }

        // The first face picture is used as the student's photo. Returns null if there is none.
        public static byte[] GetPhoto(int studentId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", studentId);

            object data = Database.GetValue(
                "SELECT image_data FROM face_images WHERE student_id = @id ORDER BY image_number LIMIT 1", parameters);
            return data as byte[];
        }

        // All face pictures of all students, used to train the face recognizer.
        public static DataTable GetAllFaceImages()
        {
            return Database.GetTable("SELECT student_id, image_data FROM face_images ORDER BY student_id, image_number");
        }

        // ---------- Private helpers ----------

        private static void CheckFaceImageCount(List<byte[]> faceImages)
        {
            if (faceImages == null || faceImages.Count != RequiredFaceImages)
            {
                throw new InvalidOperationException("Exactly " + RequiredFaceImages + " face pictures are required.");
            }
        }

        private static void AddStudentParameters(MySqlCommand cmd, Student student)
        {
            cmd.Parameters.AddWithValue("@number", student.StudentNumber.Trim());
            cmd.Parameters.AddWithValue("@first", student.FirstName.Trim());
            cmd.Parameters.AddWithValue("@middle", Database.TextOrNull(student.MiddleName));
            cmd.Parameters.AddWithValue("@last", student.LastName.Trim());
            cmd.Parameters.AddWithValue("@dept", student.DepartmentId);
            cmd.Parameters.AddWithValue("@year", student.YearLevel);
            cmd.Parameters.AddWithValue("@gender", student.Gender);
            cmd.Parameters.AddWithValue("@email", Database.TextOrNull(student.Email));
            cmd.Parameters.AddWithValue("@contact", Database.TextOrNull(student.ContactNumber));
            cmd.Parameters.AddWithValue("@status", student.Status);
        }

        private static void SaveFaceImages(MySqlConnection conn, MySqlTransaction transaction, int studentId, List<byte[]> faceImages)
        {
            for (int i = 0; i < faceImages.Count; i++)
            {
                string query = "INSERT INTO face_images (student_id, image_number, image_data) VALUES (@student, @number, @data)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@student", studentId);
                    cmd.Parameters.AddWithValue("@number", i + 1);
                    cmd.Parameters.AddWithValue("@data", faceImages[i]);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
