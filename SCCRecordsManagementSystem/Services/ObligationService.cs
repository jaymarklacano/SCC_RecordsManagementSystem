using System;
using System.Collections.Generic;
using System.Data;

namespace SCCRecordsManagementSystem
{
    public static class ObligationService
    {
        public const int PointsPerAbsence = 10;

        public static DataTable GetBalances(int eventId, int departmentId, string yearLevel, string searchText,
            bool onlyWithBalance)
        {
            string query = @"SELECT balance_rows.*
                FROM (
                    SELECT s.student_id, s.student_number,
                           CONCAT(s.last_name, ', ', s.first_name, IF(s.middle_name IS NULL, '', CONCAT(' ', s.middle_name))) AS full_name,
                           d.department_code, s.year_level,
                           IFNULL(absences.absent_count, 0) AS absent_count,
                           IFNULL(absences.absent_count, 0) * 10 AS absence_points,
                           IFNULL(deductions.deducted_points, 0) AS deducted_points,
                           GREATEST(IFNULL(absences.absent_count, 0) * 10 - IFNULL(deductions.deducted_points, 0), 0) AS balance
                    FROM students s
                    INNER JOIN departments d ON d.department_id = s.department_id
                    LEFT JOIN (
                        SELECT roster.student_id, COUNT(*) AS absent_count
                        FROM event_sessions es
                        INNER JOIN attendance_events e ON e.event_id = es.event_id
                        INNER JOIN students roster ON roster.status = 'Active'
                          AND DATE(roster.created_at) <= es.session_date
                          AND (e.department_id IS NULL OR e.department_id = roster.department_id)
                        LEFT JOIN event_attendance ea ON ea.session_id = es.session_id AND ea.student_id = roster.student_id
                        WHERE es.enabled = 1 AND (@event = 0 OR e.event_id = @event)
                          AND TIMESTAMP(es.session_date, es.end_time) <= NOW()
                          AND ea.student_id IS NULL
                        GROUP BY roster.student_id
                    ) absences ON absences.student_id = s.student_id
                    LEFT JOIN (
                        SELECT student_id, SUM(points_deducted) AS deducted_points
                        FROM obligation_adjustments
                        WHERE (@event = 0 OR event_id = @event)
                        GROUP BY student_id
                    ) deductions ON deductions.student_id = s.student_id
                    WHERE s.status = 'Active'
                ) balance_rows
                WHERE (@department = 0 OR balance_rows.student_id IN
                      (SELECT student_id FROM students WHERE department_id = @department))
                  AND (@year = '' OR balance_rows.year_level = @year)
                  AND (@search = '' OR balance_rows.student_number LIKE @like
                       OR balance_rows.full_name LIKE @like OR balance_rows.department_code LIKE @like)
                  AND (@withBalance = 0 OR balance_rows.balance > 0)
                ORDER BY balance_rows.balance DESC, balance_rows.full_name";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@event", eventId);
            parameters.Add("@department", departmentId);
            parameters.Add("@year", yearLevel ?? "");
            parameters.Add("@search", (searchText ?? "").Trim());
            parameters.Add("@like", "%" + (searchText ?? "").Trim() + "%");
            parameters.Add("@withBalance", onlyWithBalance ? 1 : 0);
            return Database.GetTable(query, parameters);
        }

        public static int GetBalance(int studentId, int eventId)
        {
            DataTable rows = GetBalances(eventId, 0, "", "", false);
            foreach (DataRow row in rows.Rows)
            {
                if (Convert.ToInt32(row["student_id"]) == studentId)
                {
                    return Convert.ToInt32(row["balance"]);
                }
            }
            return 0;
        }

        public static void DeductPoints(int studentId, int points, string reason, int eventId)
        {
            if (eventId <= 0)
            {
                throw new ArgumentException("Choose one event before adjusting obligation points.");
            }
            if (points <= 0 || points > GetBalance(studentId, eventId))
            {
                throw new ArgumentException("Enter points up to the student's current balance.");
            }
            AddAdjustment(studentId, eventId, points, reason);
        }

        public static void ClearBalance(int studentId, int eventId, string reason)
        {
            if (eventId <= 0)
            {
                throw new ArgumentException("Choose one event before adjusting obligation points.");
            }
            int currentBalance = GetBalance(studentId, eventId);
            if (currentBalance <= 0)
            {
                throw new InvalidOperationException("This student has no points to clear.");
            }
            AddAdjustment(studentId, eventId, currentBalance, reason);
        }

        public static DataTable GetHistory(int studentId, int eventId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", studentId);
            parameters.Add("@event", eventId);
            return Database.GetTable(
                @"SELECT obligation_adjustments.points_deducted, obligation_adjustments.reason,
                         obligation_adjustments.created_at, e.event_name,
                         IFNULL((SELECT username FROM accounts WHERE account_id = obligation_adjustments.account_id), 'Unknown') AS adjusted_by
                  FROM obligation_adjustments
                  INNER JOIN attendance_events e ON e.event_id = obligation_adjustments.event_id
                  WHERE student_id = @student AND (@event = 0 OR obligation_adjustments.event_id = @event)
                  ORDER BY obligation_adjustments.created_at DESC", parameters);
        }

        private static void AddAdjustment(int studentId, int eventId, int points, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException("Enter a reason for changing obligation points.");
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", studentId);
            parameters.Add("@event", eventId);
            parameters.Add("@points", points);
            parameters.Add("@reason", reason.Trim());
            parameters.Add("@account", Session.AccountId == 0 ? (object)DBNull.Value : Session.AccountId);
            Database.RunCommand(
                "INSERT INTO obligation_adjustments (student_id, event_id, points_deducted, reason, account_id) VALUES (@student, @event, @points, @reason, @account)",
                parameters);
        }
    }
}
