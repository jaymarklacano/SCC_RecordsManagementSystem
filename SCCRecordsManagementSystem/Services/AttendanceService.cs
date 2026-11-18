using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace SCCRecordsManagementSystem
{
    public static class AttendanceService
    {
        public static EventAttendanceResult RecordSessionAttendance(int studentId, int sessionId, string action)
        {
            if (action != "Log In" && action != "Log Out")
            {
                throw new ArgumentException("Choose Log In or Log Out.");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", studentId);
            parameters.Add("@session", sessionId);
            DataTable rows = Database.GetTable(
                @"SELECT es.session_id, es.session_date, es.start_time, es.end_time, e.requires_logout,
                         s.status, s.department_id, e.department_id AS event_department
                  FROM event_sessions es
                  INNER JOIN attendance_events e ON e.event_id = es.event_id
                  INNER JOIN students s ON s.student_id = @student
                  WHERE es.session_id = @session AND es.enabled = 1", parameters);
            if (rows.Rows.Count == 0)
            {
                throw new InvalidOperationException("The selected attendance session is no longer available.");
            }

            DataRow session = rows.Rows[0];
            if (session["status"].ToString() != "Active")
            {
                throw new InvalidOperationException("This student is not active and cannot be marked present.");
            }
            if (session["event_department"] != DBNull.Value &&
                Convert.ToInt32(session["event_department"]) != Convert.ToInt32(session["department_id"]))
            {
                throw new InvalidOperationException("This student is not included in the selected event.");
            }

            DateTime now = DateTime.Now;
            DateTime sessionDate = Convert.ToDateTime(session["session_date"]).Date;
            TimeSpan start = (TimeSpan)session["start_time"];
            TimeSpan end = (TimeSpan)session["end_time"];
            if (now.Date != sessionDate || now.TimeOfDay < start || now.TimeOfDay > end)
            {
                throw new InvalidOperationException("Attendance can only be recorded during the scheduled session.");
            }

            Dictionary<string, object> existingParameters = new Dictionary<string, object>();
            existingParameters.Add("@student", studentId);
            existingParameters.Add("@session", sessionId);
            DataTable existing = Database.GetTable(
                "SELECT time_in, time_out FROM event_attendance WHERE student_id = @student AND session_id = @session",
                existingParameters);
            bool requiresLogout = Convert.ToInt32(session["requires_logout"]) == 1;
            if (action == "Log In")
            {
                if (existing.Rows.Count > 0)
                {
                    return new EventAttendanceResult
                    {
                        AlreadyRecorded = true,
                        TimeIn = Convert.ToDateTime(existing.Rows[0]["time_in"]),
                        TimeOut = existing.Rows[0]["time_out"] == DBNull.Value
                            ? (DateTime?)null : Convert.ToDateTime(existing.Rows[0]["time_out"]),
                        RequiresLogout = requiresLogout
                    };
                }

                Dictionary<string, object> insert = new Dictionary<string, object>();
                insert.Add("@student", studentId);
                insert.Add("@session", sessionId);
                insert.Add("@now", now);
                try
                {
                    Database.RunCommand(
                        "INSERT INTO event_attendance (session_id, student_id, time_in) VALUES (@session, @student, @now)",
                        insert);
                }
                catch (MySqlException ex)
                {
                    if (!Database.IsDuplicateError(ex))
                    {
                        throw;
                    }

                    existing = Database.GetTable(
                        "SELECT time_in, time_out FROM event_attendance WHERE student_id = @student AND session_id = @session",
                        existingParameters);
                    if (existing.Rows.Count == 0)
                    {
                        throw new InvalidOperationException("The attendance record could not be confirmed. Please try again.");
                    }
                    return new EventAttendanceResult
                    {
                        AlreadyRecorded = true,
                        TimeIn = Convert.ToDateTime(existing.Rows[0]["time_in"]),
                        TimeOut = existing.Rows[0]["time_out"] == DBNull.Value
                            ? (DateTime?)null : Convert.ToDateTime(existing.Rows[0]["time_out"]),
                        RequiresLogout = requiresLogout
                    };
                }

                return new EventAttendanceResult { TimeIn = now, RequiresLogout = requiresLogout };
            }

            if (existing.Rows.Count == 0)
            {
                throw new InvalidOperationException("This student must log in before logging out.");
            }
            if (existing.Rows[0]["time_out"] != DBNull.Value)
            {
                return new EventAttendanceResult
                {
                    AlreadyRecorded = true,
                    AlreadyLoggedOut = true,
                    TimeIn = Convert.ToDateTime(existing.Rows[0]["time_in"]),
                    TimeOut = Convert.ToDateTime(existing.Rows[0]["time_out"]),
                    RequiresLogout = requiresLogout
                };
            }

            Dictionary<string, object> update = new Dictionary<string, object>();
            update.Add("@student", studentId);
            update.Add("@session", sessionId);
            update.Add("@now", now);
            int updated = Database.RunCommand(
                "UPDATE event_attendance SET time_out = @now WHERE student_id = @student AND session_id = @session AND time_out IS NULL",
                update);
            if (updated == 0)
            {
                existing = Database.GetTable(
                    "SELECT time_in, time_out FROM event_attendance WHERE student_id = @student AND session_id = @session",
                    existingParameters);
                if (existing.Rows.Count == 0 || existing.Rows[0]["time_out"] == DBNull.Value)
                {
                    throw new InvalidOperationException("The log out could not be confirmed. Please try again.");
                }
                return new EventAttendanceResult
                {
                    AlreadyRecorded = true,
                    AlreadyLoggedOut = true,
                    TimeIn = Convert.ToDateTime(existing.Rows[0]["time_in"]),
                    TimeOut = Convert.ToDateTime(existing.Rows[0]["time_out"]),
                    RequiresLogout = requiresLogout
                };
            }
            return new EventAttendanceResult
            {
                TimeIn = Convert.ToDateTime(existing.Rows[0]["time_in"]),
                TimeOut = now,
                RequiresLogout = requiresLogout
            };
        }

        public static DataTable GetRecords(int eventId, int sessionId, DateTime? sessionDate,
            int departmentId, string yearLevel, string searchText, string status)
        {
            string query = @"SELECT es.session_id, e.event_name, es.session_date,
                    es.session_name, s.student_id, s.student_number,
                    CONCAT(s.last_name, ', ', s.first_name, IF(s.middle_name IS NULL, '', CONCAT(' ', s.middle_name))) AS full_name,
                    d.department_code, s.year_level,
                    IF(ea.student_id IS NULL, 'Absent', 'Present') AS status,
                    IF(ea.student_id IS NULL, '-', IF(e.requires_logout = 0, 'Optional',
                       IF(ea.time_out IS NOT NULL, 'Complete',
                          IF(TIMESTAMP(es.session_date, es.end_time) <= NOW(), 'Missing', 'Pending')))) AS logout_status,
                    IFNULL(DATE_FORMAT(ea.time_in, '%l:%i %p'), '-') AS time_in_text,
                    IFNULL(DATE_FORMAT(ea.time_out, '%l:%i %p'), '-') AS time_out_text
                FROM event_sessions es
                INNER JOIN attendance_events e ON e.event_id = es.event_id
                INNER JOIN students s ON DATE(s.created_at) <= es.session_date
                    AND (e.department_id IS NULL OR e.department_id = s.department_id)
                INNER JOIN departments d ON d.department_id = s.department_id
                LEFT JOIN event_attendance ea ON ea.session_id = es.session_id AND ea.student_id = s.student_id
                WHERE es.enabled = 1
                  AND (@event = 0 OR e.event_id = @event)
                  AND (@session = 0 OR es.session_id = @session)
                  AND (@dateEnabled = 0 OR es.session_date = @date)
                  AND (@department = 0 OR s.department_id = @department)
                  AND (@year = '' OR s.year_level = @year)
                  AND (@search = '' OR s.student_number LIKE @like
                       OR CONCAT_WS(' ', s.first_name, s.middle_name, s.last_name) LIKE @like
                       OR CONCAT(s.last_name, ' ', s.first_name) LIKE @like)
                  AND (@status = 'All' OR IF(ea.student_id IS NULL, 'Absent', 'Present') = @status)
                  AND (ea.student_id IS NOT NULL OR (s.status = 'Active' AND TIMESTAMP(es.session_date, es.end_time) <= NOW()))
                ORDER BY es.session_date DESC, es.start_time DESC, s.last_name, s.first_name";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@event", eventId);
            parameters.Add("@session", sessionId);
            parameters.Add("@dateEnabled", sessionDate.HasValue ? 1 : 0);
            parameters.Add("@date", sessionDate.HasValue ? sessionDate.Value.Date : DateTime.Today);
            parameters.Add("@department", departmentId);
            parameters.Add("@year", yearLevel ?? "");
            parameters.Add("@search", (searchText ?? "").Trim());
            parameters.Add("@like", "%" + (searchText ?? "").Trim() + "%");
            parameters.Add("@status", status ?? "All");
            return Database.GetTable(query, parameters);
        }

        public static DataTable GetSessionSummary(int sessionId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@session", sessionId);
            return Database.GetTable(
                @"SELECT COUNT(*) AS total,
                         IFNULL(SUM(IF(ea.student_id IS NULL, 0, 1)), 0) AS present,
                         IF(TIMESTAMP(es.session_date, es.end_time) <= NOW(),
                            IFNULL(SUM(IF(ea.student_id IS NULL, 1, 0)), 0), 0) AS absent
                  FROM event_sessions es
                  INNER JOIN attendance_events e ON e.event_id = es.event_id
                  INNER JOIN students s ON s.status = 'Active' AND DATE(s.created_at) <= es.session_date
                    AND (e.department_id IS NULL OR e.department_id = s.department_id)
                  LEFT JOIN event_attendance ea ON ea.session_id = es.session_id AND ea.student_id = s.student_id
                  WHERE es.session_id = @session AND es.enabled = 1",
                parameters);
        }

        public static DataTable GetSessionPresentRecords(int sessionId)
        {
            DataTable rows = GetRecords(0, sessionId, null, 0, "", "", "Present");
            if (!rows.Columns.Contains("time_text"))
            {
                rows.Columns.Add("time_text", typeof(string), "time_in_text");
            }
            return rows;
        }

        public static int CountForStudent(int studentId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", studentId);
            return Database.GetCount("SELECT COUNT(*) FROM event_attendance WHERE student_id = @student", parameters);
        }

        public static string FormatTime(DateTime time)
        {
            return time.ToString("h:mm tt", CultureInfo.InvariantCulture);
        }
    }
}
