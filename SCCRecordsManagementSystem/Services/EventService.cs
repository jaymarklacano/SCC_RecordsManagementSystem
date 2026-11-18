using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SCCRecordsManagementSystem
{
    public static class EventService
    {
        public static DataTable Search(string text)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@text", "%" + text.Trim() + "%");
            return Database.GetTable(
                @"SELECT e.event_id, e.event_name, e.venue, e.start_date, e.end_date, e.department_id,
                         IFNULL(d.department_code, 'All Departments') AS department_code,
                         e.requires_logout, e.morning_enabled, e.morning_start, e.morning_end,
                         e.afternoon_enabled, e.afternoon_start, e.afternoon_end,
                         (SELECT COUNT(*) FROM event_sessions es WHERE es.event_id = e.event_id AND es.enabled = 1) AS session_count
                  FROM attendance_events e
                  LEFT JOIN departments d ON d.department_id = e.department_id
                  WHERE e.event_name LIKE @text OR e.venue LIKE @text OR d.department_code LIKE @text
                  ORDER BY e.start_date DESC, e.event_name", parameters);
        }

        public static DataTable GetChoices()
        {
            return Database.GetTable(
                @"SELECT event_id, CONCAT(event_name, ' (', DATE_FORMAT(start_date, '%b %e'), ' - ',
                         DATE_FORMAT(end_date, '%b %e, %Y'), ')') AS display_name
                  FROM attendance_events ORDER BY start_date DESC, event_name");
        }

        public static int Create(string name, string venue, DateTime startDate, DateTime endDate, int departmentId,
            bool requiresLogout, bool morningEnabled, TimeSpan morningStart, TimeSpan morningEnd,
            bool afternoonEnabled, TimeSpan afternoonStart, TimeSpan afternoonEnd)
        {
            ValidateSchedule(name, startDate, endDate, morningEnabled, morningStart, morningEnd,
                afternoonEnabled, afternoonStart, afternoonEnd);

            using (MySqlConnection connection = Database.GetConnection())
            using (MySqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    string insertEvent = @"INSERT INTO attendance_events
                        (event_name, venue, start_date, end_date, department_id, requires_logout,
                         morning_enabled, morning_start, morning_end, afternoon_enabled, afternoon_start, afternoon_end, created_by)
                        VALUES (@name, @venue, @start, @end, @department, @logout,
                         @morning, @morningStart, @morningEnd, @afternoon, @afternoonStart, @afternoonEnd, @account)";
                    int eventId;
                    using (MySqlCommand command = new MySqlCommand(insertEvent, connection, transaction))
                    {
                        AddEventParameters(command, name, venue, startDate, endDate, departmentId, requiresLogout,
                            morningEnabled, morningStart, morningEnd, afternoonEnabled, afternoonStart, afternoonEnd);
                        command.Parameters.AddWithValue("@account", Session.AccountId == 0 ? (object)DBNull.Value : Session.AccountId);
                        command.ExecuteNonQuery();
                        eventId = Convert.ToInt32(command.LastInsertedId);
                    }

                    AddSessions(connection, transaction, eventId, startDate.Date, endDate.Date, morningEnabled,
                        morningStart, morningEnd, afternoonEnabled, afternoonStart, afternoonEnd);
                    transaction.Commit();
                    return eventId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void Update(int eventId, string name, string venue, DateTime startDate, DateTime endDate,
            int departmentId, bool requiresLogout, bool morningEnabled, TimeSpan morningStart, TimeSpan morningEnd,
            bool afternoonEnabled, TimeSpan afternoonStart, TimeSpan afternoonEnd)
        {
            ValidateSchedule(name, startDate, endDate, morningEnabled, morningStart, morningEnd,
                afternoonEnabled, afternoonStart, afternoonEnd);
            Dictionary<string, object> check = new Dictionary<string, object>();
            check.Add("@id", eventId);
            int historyCount = Database.GetCount(
                @"SELECT
                    (SELECT COUNT(*) FROM event_attendance ea INNER JOIN event_sessions es ON es.session_id = ea.session_id WHERE es.event_id = @id) +
                    (SELECT COUNT(*) FROM obligation_adjustments oa WHERE oa.event_id = @id) +
                    (SELECT COUNT(*) FROM event_sessions es WHERE es.event_id = @id
                       AND TIMESTAMP(es.session_date, es.end_time) <= NOW())",
                check);
            if (historyCount > 0)
            {
                DataTable currentRows = Database.GetTable("SELECT * FROM attendance_events WHERE event_id = @id", check);
                if (currentRows.Rows.Count == 0)
                {
                    throw new InvalidOperationException("This event no longer exists.");
                }

                if (!ScheduleIsUnchanged(currentRows.Rows[0], startDate, endDate, departmentId, requiresLogout,
                    morningEnabled, morningStart, morningEnd, afternoonEnabled, afternoonStart, afternoonEnd))
                {
                    throw new InvalidOperationException(
                        "This event has attendance or completed sessions. Only its name and venue can be changed.");
                }

                Dictionary<string, object> details = new Dictionary<string, object>();
                details.Add("@id", eventId);
                details.Add("@name", name.Trim());
                details.Add("@venue", Database.TextOrNull(venue));
                Database.RunCommand("UPDATE attendance_events SET event_name = @name, venue = @venue WHERE event_id = @id", details);
                return;
            }

            using (MySqlConnection connection = Database.GetConnection())
            using (MySqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (MySqlCommand delete = new MySqlCommand("DELETE FROM event_sessions WHERE event_id = @id", connection, transaction))
                    {
                        delete.Parameters.AddWithValue("@id", eventId);
                        delete.ExecuteNonQuery();
                    }

                    string update = @"UPDATE attendance_events SET event_name = @name, venue = @venue,
                        start_date = @start, end_date = @end, department_id = @department, requires_logout = @logout,
                        morning_enabled = @morning, morning_start = @morningStart, morning_end = @morningEnd,
                        afternoon_enabled = @afternoon, afternoon_start = @afternoonStart, afternoon_end = @afternoonEnd
                        WHERE event_id = @id";
                    using (MySqlCommand command = new MySqlCommand(update, connection, transaction))
                    {
                        AddEventParameters(command, name, venue, startDate, endDate, departmentId, requiresLogout,
                            morningEnabled, morningStart, morningEnd, afternoonEnabled, afternoonStart, afternoonEnd);
                        command.Parameters.AddWithValue("@id", eventId);
                        command.ExecuteNonQuery();
                    }

                    AddSessions(connection, transaction, eventId, startDate.Date, endDate.Date, morningEnabled,
                        morningStart, morningEnd, afternoonEnabled, afternoonStart, afternoonEnd);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void Delete(int eventId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", eventId);
            Database.RunCommand("DELETE FROM attendance_events WHERE event_id = @id", parameters);
        }

        public static DataTable GetSessions(int eventId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@event", eventId);
            return Database.GetTable(
                @"SELECT es.session_id, es.event_id, es.session_date, es.session_name, es.start_time, es.end_time,
                         CONCAT(event_name, ' - ', DATE_FORMAT(session_date, '%b %e, %Y'), ' - ', session_name) AS display_name
                  FROM event_sessions es INNER JOIN attendance_events e ON e.event_id = es.event_id
                  WHERE es.enabled = 1 AND (@event = 0 OR es.event_id = @event)
                  ORDER BY session_date DESC, start_time DESC", parameters);
        }

        public static DataTable GetActiveSessionChoices()
        {
            return Database.GetTable(
                @"SELECT es.session_id, es.event_id,
                         CONCAT(e.event_name, ' - ', DATE_FORMAT(es.session_date, '%b %e'), ' - ', es.session_name) AS display_name
                  FROM event_sessions es INNER JOIN attendance_events e ON e.event_id = es.event_id
                  WHERE es.enabled = 1 AND es.session_date = CURDATE()
                    AND CURTIME() BETWEEN es.start_time AND es.end_time
                    AND CURDATE() BETWEEN e.start_date AND e.end_date
                  ORDER BY es.start_time");
        }

        public static DataRow GetCurrentSession()
        {
            DataTable sessions = GetActiveSessionChoices();
            return sessions.Rows.Count == 0 ? null : sessions.Rows[0];
        }

        public static DataTable GetDepartmentChoices()
        {
            return DepartmentService.GetAll();
        }

        private static void AddEventParameters(MySqlCommand command, string name, string venue,
            DateTime startDate, DateTime endDate, int departmentId, bool requiresLogout,
            bool morningEnabled, TimeSpan morningStart, TimeSpan morningEnd,
            bool afternoonEnabled, TimeSpan afternoonStart, TimeSpan afternoonEnd)
        {
            command.Parameters.AddWithValue("@name", name.Trim());
            command.Parameters.AddWithValue("@venue", Database.TextOrNull(venue));
            command.Parameters.AddWithValue("@start", startDate.Date);
            command.Parameters.AddWithValue("@end", endDate.Date);
            command.Parameters.AddWithValue("@department", departmentId == 0 ? (object)DBNull.Value : departmentId);
            command.Parameters.AddWithValue("@logout", requiresLogout ? 1 : 0);
            command.Parameters.AddWithValue("@morning", morningEnabled ? 1 : 0);
            command.Parameters.AddWithValue("@morningStart", morningStart);
            command.Parameters.AddWithValue("@morningEnd", morningEnd);
            command.Parameters.AddWithValue("@afternoon", afternoonEnabled ? 1 : 0);
            command.Parameters.AddWithValue("@afternoonStart", afternoonStart);
            command.Parameters.AddWithValue("@afternoonEnd", afternoonEnd);
        }

        private static bool ScheduleIsUnchanged(DataRow current, DateTime startDate, DateTime endDate,
            int departmentId, bool requiresLogout, bool morningEnabled, TimeSpan morningStart,
            TimeSpan morningEnd, bool afternoonEnabled, TimeSpan afternoonStart, TimeSpan afternoonEnd)
        {
            int currentDepartment = current["department_id"] == DBNull.Value
                ? 0 : Convert.ToInt32(current["department_id"]);
            return Convert.ToDateTime(current["start_date"]).Date == startDate.Date &&
                   Convert.ToDateTime(current["end_date"]).Date == endDate.Date &&
                   currentDepartment == departmentId &&
                   Convert.ToInt32(current["requires_logout"]) == (requiresLogout ? 1 : 0) &&
                   Convert.ToInt32(current["morning_enabled"]) == (morningEnabled ? 1 : 0) &&
                   (TimeSpan)current["morning_start"] == morningStart &&
                   (TimeSpan)current["morning_end"] == morningEnd &&
                   Convert.ToInt32(current["afternoon_enabled"]) == (afternoonEnabled ? 1 : 0) &&
                   (TimeSpan)current["afternoon_start"] == afternoonStart &&
                   (TimeSpan)current["afternoon_end"] == afternoonEnd;
        }

        private static void AddSessions(MySqlConnection connection, MySqlTransaction transaction, int eventId,
            DateTime startDate, DateTime endDate, bool morningEnabled, TimeSpan morningStart, TimeSpan morningEnd,
            bool afternoonEnabled, TimeSpan afternoonStart, TimeSpan afternoonEnd)
        {
            const string query = @"INSERT INTO event_sessions
                (event_id, session_date, session_name, start_time, end_time, enabled)
                VALUES (@event, @date, @name, @start, @end, 1)";

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (morningEnabled)
                {
                    AddSession(connection, transaction, query, eventId, date, "Morning", morningStart, morningEnd);
                }
                if (afternoonEnabled)
                {
                    AddSession(connection, transaction, query, eventId, date, "Afternoon", afternoonStart, afternoonEnd);
                }
            }
        }

        private static void AddSession(MySqlConnection connection, MySqlTransaction transaction, string query,
            int eventId, DateTime date, string name, TimeSpan start, TimeSpan end)
        {
            using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@event", eventId);
                command.Parameters.AddWithValue("@date", date.Date);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@start", start);
                command.Parameters.AddWithValue("@end", end);
                command.ExecuteNonQuery();
            }
        }

        private static void ValidateSchedule(string name, DateTime startDate, DateTime endDate,
            bool morningEnabled, TimeSpan morningStart, TimeSpan morningEnd,
            bool afternoonEnabled, TimeSpan afternoonStart, TimeSpan afternoonEnd)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Enter an event name.");
            }
            if (startDate.Date > endDate.Date || (endDate.Date - startDate.Date).TotalDays > 365)
            {
                throw new ArgumentException("Choose a valid event date range of no more than one year.");
            }
            if (!morningEnabled && !afternoonEnabled)
            {
                throw new ArgumentException("Enable at least one attendance session.");
            }
            if ((morningEnabled && morningEnd <= morningStart) ||
                (afternoonEnabled && afternoonEnd <= afternoonStart))
            {
                throw new ArgumentException("Each enabled session must end after it starts.");
            }
        }
    }
}
