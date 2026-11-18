using System;
using System.Collections.Generic;
using System.Data;

namespace SCCRecordsManagementSystem
{
    public static class ReportService
    {
        public static DataTable GetAttendanceReport(int eventId, int departmentId, DateTime fromDate, DateTime toDate)
        {
            if (fromDate.Date > toDate.Date)
            {
                throw new ArgumentException("The start date must be on or before the end date.");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@event", eventId);
            parameters.Add("@department", departmentId);
            parameters.Add("@from", fromDate.Date);
            parameters.Add("@to", toDate.Date);
            return Database.GetTable(
                @"SELECT e.event_name, es.session_date, es.session_name, d.department_code,
                         COUNT(s.student_id) AS eligible_students,
                         SUM(IF(ea.student_id IS NULL, 0, 1)) AS present_count,
                         SUM(IF(ea.student_id IS NULL, 1, 0)) AS absent_count,
                         ROUND(100 * SUM(IF(ea.student_id IS NULL, 0, 1)) / NULLIF(COUNT(s.student_id), 0), 1) AS attendance_percent,
                         SUM(IF(ea.student_id IS NULL, 10, 0)) AS obligation_points
                  FROM event_sessions es
                  INNER JOIN attendance_events e ON e.event_id = es.event_id
                  INNER JOIN departments d ON (e.department_id IS NULL OR e.department_id = d.department_id)
                  INNER JOIN students s ON s.department_id = d.department_id AND s.status = 'Active'
                    AND DATE(s.created_at) <= es.session_date
                  LEFT JOIN event_attendance ea ON ea.session_id = es.session_id AND ea.student_id = s.student_id
                  WHERE es.enabled = 1 AND TIMESTAMP(es.session_date, es.end_time) <= NOW()
                    AND es.session_date BETWEEN @from AND @to
                    AND (@event = 0 OR e.event_id = @event)
                    AND (@department = 0 OR d.department_id = @department)
                  GROUP BY e.event_id, e.event_name, es.session_id, es.session_date, es.session_name, d.department_id, d.department_code
                  ORDER BY es.session_date DESC, e.event_name, d.department_code, es.start_time", parameters);
        }

        public static DataTable GetMostAbsentStudents(int eventId, DateTime fromDate, DateTime toDate)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@event", eventId);
            parameters.Add("@from", fromDate.Date);
            parameters.Add("@to", toDate.Date);
            return Database.GetTable(
                @"SELECT s.student_number,
                         CONCAT(s.last_name, ', ', s.first_name, IF(s.middle_name IS NULL, '', CONCAT(' ', s.middle_name))) AS full_name,
                         d.department_code, s.year_level, COUNT(*) AS absent_sessions, COUNT(*) * 10 AS obligation_points
                  FROM event_sessions es
                  INNER JOIN attendance_events e ON e.event_id = es.event_id
                  INNER JOIN students s ON s.status = 'Active' AND DATE(s.created_at) <= es.session_date
                    AND (e.department_id IS NULL OR e.department_id = s.department_id)
                  INNER JOIN departments d ON d.department_id = s.department_id
                  LEFT JOIN event_attendance ea ON ea.session_id = es.session_id AND ea.student_id = s.student_id
                  WHERE es.enabled = 1 AND TIMESTAMP(es.session_date, es.end_time) <= NOW()
                    AND es.session_date BETWEEN @from AND @to AND ea.student_id IS NULL
                    AND (@event = 0 OR e.event_id = @event)
                  GROUP BY s.student_id, s.student_number, s.first_name, s.middle_name, s.last_name, d.department_code, s.year_level
                  ORDER BY absent_sessions DESC, full_name LIMIT 25", parameters);
        }

        public static int GetOutstandingPoints()
        {
            DataTable balances = ObligationService.GetBalances(0, 0, "", "", false);
            int total = 0;
            foreach (DataRow row in balances.Rows)
            {
                total += Convert.ToInt32(row["balance"]);
            }
            return total;
        }
    }
}
