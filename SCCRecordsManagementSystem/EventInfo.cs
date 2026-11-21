using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCRecordsManagementSystem
{
    // One row of the "events" table.
    public class EventInfo
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Venue { get; set; }
        public string Organizer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // 0 means the event is for ALL departments.
        public int DepartmentId { get; set; }

        // "" (empty) means the event is for ALL year levels.
        public string YearLevel { get; set; }

        public bool RequiresLogout { get; set; }

        public bool MorningEnabled { get; set; }
        public TimeSpan MorningStart { get; set; }
        public TimeSpan MorningEnd { get; set; }

        public bool AfternoonEnabled { get; set; }
        public TimeSpan AfternoonStart { get; set; }
        public TimeSpan AfternoonEnd { get; set; }

        public bool EveningEnabled { get; set; }
        public TimeSpan EveningStart { get; set; }
        public TimeSpan EveningEnd { get; set; }

        // One row of the "event_sessions" table.
        // Example: Day 1 Morning of "Foundation Day" from 8:00 AM to 12:00 PM.
        public class SessionInfo
        {
            public int SessionId { get; set; }
            public int EventId { get; set; }
            public string EventName { get; set; }
            public DateTime SessionDate { get; set; }
            public string SessionName { get; set; }
            public DateTime StartsAt { get; set; }
            public DateTime EndsAt { get; set; }
            public bool RequiresLogout { get; set; }
        }
    }
}
