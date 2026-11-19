using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SCCRecordsManagementSystem
{
    public partial class AddEvent : Form
    {
        private readonly int eventId;
        private readonly DateTimePicker endDate = new DateTimePicker { Format = DateTimePickerFormat.Short };
        private readonly DateTimePicker afternoonStart = new DateTimePicker();
        private readonly DateTimePicker afternoonEnd = new DateTimePicker();
        private readonly CheckBox morning = new CheckBox { Text = "Morning", Checked = true, AutoSize = true };
        private readonly CheckBox afternoon = new CheckBox { Text = "Afternoon", Checked = true, AutoSize = true };
        private readonly CheckBox logout = new CheckBox { Text = "Require log out (missing log outs are flagged)", AutoSize = true };
        private readonly ComboBox department = new ComboBox();
        private Label scheduleInfo;
        private bool scheduleLocked;
        public AddEvent() : this(0) { }
        public AddEvent(int id)
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            eventId = id;
            ClientSize = new Size(790, 650); panel2.Height = 470;
            Program.PrepareWindow(this);
            button1.Top = button2.Top = 590;
            label1.Text = id == 0 ? "Add Event" : "Edit Event";
            label6.Text = "Start / End dates:";
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            endDate.SetBounds(424, 114, 299, 24); panel2.Controls.Add(endDate);
            label4.Text = "Session start"; label9.Text = "Session end";
            label4.Top = label9.Top = 150; dateTimePicker2.Top = dateTimePicker3.Top = 180;
            morning.SetBounds(30, 210, 150, 25); panel2.Controls.Add(morning);
            afternoon.SetBounds(30, 275, 150, 25); panel2.Controls.Add(afternoon);
            afternoonStart.SetBounds(30, 246, 315, 24); panel2.Controls.Add(afternoonStart);
            afternoonEnd.SetBounds(424, 246, 299, 24); panel2.Controls.Add(afternoonEnd);
            SetTime(dateTimePicker2, 8); SetTime(dateTimePicker3, 12);
            SetTime(afternoonStart, 13); SetTime(afternoonEnd, 17);
            label5.Top = 310; panel7.Top = 330;
            Program.AddLabel(panel2, "Department / roster:", 30, 377, 180);
            department.SetBounds(220, 375, 300, 26); panel2.Controls.Add(department);
            Program.Choices(department, Database.GetData("SELECT department_id,department_code FROM departments ORDER BY department_code"),
                "department_id", "department_code", "All Departments");
            logout.Location = new Point(30, 415); panel2.Controls.Add(logout);
            scheduleInfo = Program.AddLabel(this, "", 20, 542, 740, 40);
            textBox1.MaxLength = textBox3.MaxLength = 120;
            button2.Click += SaveClick;
            button1.Text = "Cancel"; button1.Click += delegate { Close(); };
            AcceptButton = button2; CancelButton = button1;
            dateTimePicker1.ValueChanged += delegate { UpdateScheduleInfo(); };
            endDate.ValueChanged += delegate { UpdateScheduleInfo(); };
            morning.CheckedChanged += delegate { UpdateScheduleInfo(); };
            afternoon.CheckedChanged += delegate { UpdateScheduleInfo(); };
            if (id > 0) LoadEvent();
            UpdateScheduleInfo();
        }

        private void UpdateScheduleInfo()
        {
            int days = (endDate.Value.Date - dateTimePicker1.Value.Date).Days + 1;
            int sessions = (morning.Checked ? 1 : 0) + (afternoon.Checked ? 1 : 0);
            dateTimePicker2.Enabled = dateTimePicker3.Enabled = !scheduleLocked && morning.Checked;
            afternoonStart.Enabled = afternoonEnd.Enabled = !scheduleLocked && afternoon.Checked;
            scheduleInfo.Text = scheduleLocked ? "This event has started. Only name and venue can change; the schedule is protected."
                : days <= 0 || sessions == 0 ? "Select a valid date range and at least one session."
                : days + " day(s), " + (days * sessions) + " attendance sessions. Students registered later are added to the roster automatically.";
        }

        private static void SetTime(DateTimePicker picker, int hour)
        {
            picker.Format = DateTimePickerFormat.Custom; picker.CustomFormat = "hh:mm tt";
            picker.ShowUpDown = true; picker.Value = DateTime.Today.AddHours(hour);
        }

        private void LoadEvent()
        {
            DataRow row = Database.GetData("SELECT * FROM attendance_events WHERE event_id=@id",
                new Dictionary<string, object> { { "@id", eventId } }).Rows[0];
            textBox1.Text = row["event_name"].ToString(); textBox3.Text = row["venue"].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(row["start_date"]); endDate.Value = Convert.ToDateTime(row["end_date"]);
            department.SelectedValue = row["department_id"] == DBNull.Value ? 0 : row["department_id"];
            morning.Checked = Convert.ToBoolean(row["morning_enabled"]); afternoon.Checked = Convert.ToBoolean(row["afternoon_enabled"]);
            logout.Checked = Convert.ToBoolean(row["requires_logout"]);
            dateTimePicker2.Value = DateTime.Today.Add((TimeSpan)row["morning_start"]);
            dateTimePicker3.Value = DateTime.Today.Add((TimeSpan)row["morning_end"]);
            afternoonStart.Value = DateTime.Today.Add((TimeSpan)row["afternoon_start"]);
            afternoonEnd.Value = DateTime.Today.Add((TimeSpan)row["afternoon_end"]);
            scheduleLocked = Convert.ToInt32(Database.Scalar("SELECT COUNT(*) FROM event_sessions WHERE event_id=@id AND starts_at<=NOW()",
                new Dictionary<string, object> { { "@id", eventId } })) > 0;
            foreach (Control control in new Control[] { dateTimePicker1, endDate, department, morning, afternoon, logout })
                control.Enabled = !scheduleLocked;
        }

        private void SaveClick(object sender, EventArgs e)
        {
            try
            {
                SaveEvent(eventId, textBox1.Text, textBox3.Text, dateTimePicker1.Value.Date, endDate.Value.Date,
                    Program.SelectedId(department), logout.Checked, morning.Checked, dateTimePicker2.Value.TimeOfDay,
                    dateTimePicker3.Value.TimeOfDay, afternoon.Checked, afternoonStart.Value.TimeOfDay, afternoonEnd.Value.TimeOfDay);
                MessageBox.Show("Event saved. Use View roster to check participants. New students are added automatically until the event ends.");
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        public static int SaveEvent(int id, string name, string venue, DateTime first, DateTime last,
            int departmentId, bool requireLogout, bool am, TimeSpan amStart, TimeSpan amEnd,
            bool pm, TimeSpan pmStart, TimeSpan pmEnd)
        {
            Database.Require("Event Management");
            Database.CheckText(name, "Event name", 120); Database.CheckText(venue, "Venue", 120, false);
            if (last < first || (last - first).TotalDays > 365) throw new ArgumentException("Choose a date range of up to one year.");
            if (!am && !pm) throw new ArgumentException("Enable at least one session.");
            amStart = new TimeSpan(amStart.Hours, amStart.Minutes, 0); amEnd = new TimeSpan(amEnd.Hours, amEnd.Minutes, 0);
            pmStart = new TimeSpan(pmStart.Hours, pmStart.Minutes, 0); pmEnd = new TimeSpan(pmEnd.Hours, pmEnd.Minutes, 0);
            if ((am && amEnd <= amStart) || (pm && pmEnd <= pmStart) || (am && pm && pmStart < amEnd))
                throw new ArgumentException("Session end must follow start; morning and afternoon cannot overlap.");
            Dictionary<string, object> p = new Dictionary<string, object> {
                { "@id", id }, { "@name", name.Trim() }, { "@venue", Database.Optional(venue) }, { "@first", first.Date },
                { "@last", last.Date }, { "@department", departmentId == 0 ? (object)DBNull.Value : departmentId },
                { "@logout", requireLogout }, { "@am", am }, { "@ams", amStart }, { "@ame", amEnd },
                { "@pm", pm }, { "@pms", pmStart }, { "@pme", pmEnd }, { "@account", Database.AccountId } };
            using (MySqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    if (id > 0)
                    {
                        DataTable existing = new DataTable();
                        using (MySqlCommand cmd = Database.Command(connection, "SELECT * FROM attendance_events WHERE event_id=@id FOR UPDATE", p, transaction))
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) adapter.Fill(existing);
                        if (existing.Rows.Count == 0) throw new InvalidOperationException("The event was deleted.");
                        bool locked;
                        using (MySqlCommand cmd = Database.Command(connection,
                            "SELECT COUNT(*) FROM event_sessions WHERE event_id=@id AND starts_at<=NOW()", p, transaction))
                            locked = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                        if (locked)
                        {
                            DataRow r = existing.Rows[0];
                            bool unchanged = Convert.ToDateTime(r["start_date"]) == first.Date && Convert.ToDateTime(r["end_date"]) == last.Date
                                && Convert.ToString(r["department_id"]) == Convert.ToString(p["@department"])
                                && Convert.ToBoolean(r["requires_logout"]) == requireLogout && Convert.ToBoolean(r["morning_enabled"]) == am
                                && Convert.ToBoolean(r["afternoon_enabled"]) == pm && (TimeSpan)r["morning_start"] == amStart
                                && (TimeSpan)r["morning_end"] == amEnd && (TimeSpan)r["afternoon_start"] == pmStart && (TimeSpan)r["afternoon_end"] == pmEnd;
                            if (!unchanged) throw new InvalidOperationException("This event has started. Only its name and venue can change.");
                            using (MySqlCommand cmd = Database.Command(connection,
                                "UPDATE attendance_events SET event_name=@name,venue=@venue WHERE event_id=@id", p, transaction)) cmd.ExecuteNonQuery();
                            transaction.Commit(); return id;
                        }
                    }
                    string sql = id == 0
                        ? @"INSERT INTO attendance_events(event_name,venue,start_date,end_date,department_id,requires_logout,morning_enabled,morning_start,morning_end,afternoon_enabled,afternoon_start,afternoon_end,created_by)
                            VALUES(@name,@venue,@first,@last,@department,@logout,@am,@ams,@ame,@pm,@pms,@pme,@account)"
                        : @"UPDATE attendance_events SET event_name=@name,venue=@venue,start_date=@first,end_date=@last,department_id=@department,requires_logout=@logout,
                            morning_enabled=@am,morning_start=@ams,morning_end=@ame,afternoon_enabled=@pm,afternoon_start=@pms,afternoon_end=@pme WHERE event_id=@id";
                    using (MySqlCommand cmd = Database.Command(connection, sql, p, transaction))
                    {
                        cmd.ExecuteNonQuery(); if (id == 0) id = Convert.ToInt32(cmd.LastInsertedId);
                    }
                    p["@id"] = id;
                    using (MySqlCommand cmd = Database.Command(connection, "DELETE FROM event_sessions WHERE event_id=@id", p, transaction)) cmd.ExecuteNonQuery();
                    using (MySqlCommand cmd = Database.Command(connection, "DELETE FROM event_students WHERE event_id=@id", p, transaction)) cmd.ExecuteNonQuery();
                    using (MySqlCommand cmd = Database.Command(connection, @"INSERT INTO event_students(event_id,student_id,department_id,year_level)
                        SELECT @id,s.student_id,s.department_id,s.year_level FROM students s WHERE s.status='Active' AND s.face_registered=1
                        AND (@department IS NULL OR s.department_id=@department)
                        AND (SELECT COUNT(*) FROM face_images f WHERE f.student_id=s.student_id)=5", p, transaction)) cmd.ExecuteNonQuery();
                    for (DateTime day = first.Date; day <= last.Date; day = day.AddDays(1))
                    {
                        if (am) InsertSession(connection, transaction, id, "Morning", day.Add(amStart), day.Add(amEnd));
                        if (pm) InsertSession(connection, transaction, id, "Afternoon", day.Add(pmStart), day.Add(pmEnd));
                    }
                    transaction.Commit(); return id;
                }
            }
        }

        private static void InsertSession(MySqlConnection connection, MySqlTransaction transaction, int id,
            string name, DateTime start, DateTime end)
        {
            using (MySqlCommand cmd = Database.Command(connection,
                "INSERT INTO event_sessions(event_id,session_name,starts_at,ends_at) VALUES(@id,@name,@start,@end)",
                new Dictionary<string, object> { { "@id", id }, { "@name", name }, { "@start", start }, { "@end", end } }, transaction))
                cmd.ExecuteNonQuery();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
