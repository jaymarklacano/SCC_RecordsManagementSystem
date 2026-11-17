using System;

namespace SCCRecordsManagementSystem
{
    // The answer of AttendanceService.RecordAttendance().
    public class AttendanceResult
    {
        // True if the student already had attendance for today (nothing new was saved).
        public bool AlreadyRecorded { get; set; }

        public DateTime AttendanceDate { get; set; }

        public TimeSpan TimeIn { get; set; }

        // Example: "7:53 AM"
        public string TimeText
        {
            get { return AttendanceService.FormatTime(TimeIn); }
        }
    }
}
