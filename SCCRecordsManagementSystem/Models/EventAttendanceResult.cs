using System;

namespace SCCRecordsManagementSystem
{
    public class EventAttendanceResult
    {
        public bool AlreadyRecorded { get; set; }
        public bool AlreadyLoggedOut { get; set; }
        public bool RequiresLogout { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string TimeInText { get { return AttendanceService.FormatTime(TimeIn); } }
        public string TimeOutText { get { return TimeOut.HasValue ? AttendanceService.FormatTime(TimeOut.Value) : "-"; } }
    }
}
