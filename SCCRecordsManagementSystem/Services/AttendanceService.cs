using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace SCCRecordsManagementSystem
{
    // Attendance rules (kept simple):
    //  - Attendance is daily: one "Present" record per student per day.
    //  - A student without a record for a date is shown as "Absent" (it is calculated, not saved).
    public static class AttendanceService
    {
        // Saves today's attendance of a student. If the student was already recorded today,
        // nothing is saved and the result says AlreadyRecorded with the earlier time.
        public static AttendanceResult RecordAttendance(int studentId)
        {
            DateTime now = DateTime.Now;
            DateTime today = now.Date;

            AttendanceResult result = new AttendanceResult();
            result.AttendanceDate = today;

            TimeSpan? existingTime = GetTimeIn(studentId, today);
            if (existingTime.HasValue)
            {
                result.AlreadyRecorded = true;
                result.TimeIn = existingTime.Value;
                return result;
            }

            // Remove the fractions of a second
            TimeSpan timeNow = new TimeSpan(now.Hour, now.Minute, now.Second);

            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@student", studentId);
                parameters.Add("@date", today);
                parameters.Add("@time", timeNow);

                Database.RunCommand(
                    "INSERT INTO attendance (student_id, attendance_date, time_in, status) VALUES (@student, @date, @time, 'Present')",
                    parameters);

                result.TimeIn = timeNow;
            }
            catch (MySqlException ex)
            {
                // The database has a UNIQUE key (student + date). If something else saved the
                // record at the same moment, treat it as "already recorded".
                if (!Database.IsDuplicateError(ex))
                {
                    throw;
                }

                existingTime = GetTimeIn(studentId, today);
                result.AlreadyRecorded = true;
                result.TimeIn = existingTime.HasValue ? existingTime.Value : timeNow;
            }

            return result;
        }

        // Time of the attendance of a student on a date, or null if there is no record.
        public static TimeSpan? GetTimeIn(int studentId, DateTime date)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", studentId);
            parameters.Add("@date", date.Date);

            object value = Database.GetValue(
                "SELECT time_in FROM attendance WHERE student_id = @student AND attendance_date = @date", parameters);

            if (value == null)
            {
                return null;
            }
            return (TimeSpan)value;
        }

        // Number of attendance records of one student (shown when deleting a student).
        public static int CountForStudent(int studentId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", studentId);
            return Database.GetCount("SELECT COUNT(*) FROM attendance WHERE student_id = @student", parameters);
        }

        // Gets attendance rows for the grid.
        //  date = null          -> all dates (only "Present" rows exist, absences are calculated per date)
        //  studentId 0          -> all students,  departmentId 0 -> all courses,  yearLevel "" -> all years
        //  showPresent/showAbsent choose which statuses are returned.
        public static DataTable GetRecords(DateTime? date, int studentId, string searchText,
                                           int departmentId, string yearLevel, bool showPresent, bool showAbsent)
        {
            string fullName = "CONCAT(s.last_name, ', ', s.first_name, IF(s.middle_name IS NULL, '', CONCAT(' ', s.middle_name)))";

            // Filters shared by the Present part and the Absent part
            string filters = @" AND (@student = 0 OR s.student_id = @student)
                                AND (@dept = 0 OR s.department_id = @dept)
                                AND (@year = '' OR s.year_level = @year)
                                AND (@text = '' OR s.student_number LIKE @like
                                     OR CONCAT_WS(' ', s.first_name, s.middle_name, s.last_name) LIKE @like
                                     OR CONCAT(s.last_name, ' ', s.first_name) LIKE @like)";

            List<string> parts = new List<string>();

            if (showPresent)
            {
                parts.Add(@"SELECT a.attendance_date AS record_date, s.student_number, " + fullName + @" AS full_name,
                                   d.department_code, s.year_level, a.status, a.time_in
                            FROM attendance a
                            INNER JOIN students s ON s.student_id = a.student_id
                            INNER JOIN departments d ON d.department_id = s.department_id
                            WHERE (@hasDate = 0 OR a.attendance_date = @date)" + filters);
            }

            // Absent = active students without a record on that date (only possible for one chosen date,
            // and never for a future date).
            if (showAbsent && date.HasValue)
            {
                parts.Add(@"SELECT CAST(@date AS DATE) AS record_date, s.student_number, " + fullName + @" AS full_name,
                                   d.department_code, s.year_level, 'Absent' AS status, NULL AS time_in
                            FROM students s
                            INNER JOIN departments d ON d.department_id = s.department_id
                            WHERE s.status = 'Active'
                              AND DATE(s.created_at) <= @date
                              AND @date <= CURDATE()
                              AND NOT EXISTS (SELECT 1 FROM attendance a2
                                              WHERE a2.student_id = s.student_id AND a2.attendance_date = @date)" + filters);
            }

            if (parts.Count == 0)
            {
                return new DataTable();
            }

            // date_text and time_text are ready-to-show texts for the grid
            string query = "SELECT records.*, DATE_FORMAT(record_date, '%b %e, %Y') AS date_text, " +
                           "IFNULL(TIME_FORMAT(time_in, '%l:%i %p'), '-') AS time_text FROM (" +
                           string.Join(" UNION ALL ", parts) +
                           ") AS records ORDER BY record_date DESC, FIELD(status, 'Present', 'Absent'), time_in DESC, full_name";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@hasDate", date.HasValue ? 1 : 0);
            parameters.Add("@date", date.HasValue ? date.Value.Date : DateTime.Today);
            parameters.Add("@student", studentId);
            parameters.Add("@dept", departmentId);
            parameters.Add("@year", yearLevel);
            parameters.Add("@text", searchText.Trim());
            parameters.Add("@like", "%" + searchText.Trim() + "%");

            return Database.GetTable(query, parameters);
        }

        // Records of today, newest first (used by the Dashboard).
        public static DataTable GetTodayRecords()
        {
            return GetRecords(DateTime.Today, 0, "", 0, "", true, false);
        }

        // Total = active students, Present = students with a record on that date, Absent = the rest.
        public static void GetSummary(DateTime date, out int total, out int present, out int absent)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@date", date.Date);

            total = Database.GetCount(
                "SELECT COUNT(*) FROM students WHERE status = 'Active' AND DATE(created_at) <= @date", parameters);

            present = Database.GetCount(
                @"SELECT COUNT(*) FROM attendance a
                  INNER JOIN students s ON s.student_id = a.student_id
                  WHERE a.attendance_date = @date AND s.status = 'Active'", parameters);

            absent = date.Date > DateTime.Today ? 0 : Math.Max(total - present, 0);
        }

        // 7:53 AM style text for a time of the day.
        public static string FormatTime(TimeSpan time)
        {
            return DateTime.Today.Add(time).ToString("h:mm tt", CultureInfo.InvariantCulture);
        }
    }
}
