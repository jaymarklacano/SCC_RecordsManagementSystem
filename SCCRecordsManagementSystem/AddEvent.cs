using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SCCRecordsManagementSystem.AccountInfo;

namespace SCCRecordsManagementSystem
{
    public partial class AddEvent : Form
    {
        // 0 = we are adding a NEW event. Any other number = the event we are editing.
        private int eventId = 0;

        // true when the event already started. Then only the name, venue and organizer can be changed.
        private bool scheduleLocked = false;

        // true while the form is being filled (so the change events do nothing).
        private bool isLoading = true;

        // An event may last at most 31 days.
        private const int MAXIMUM_DAYS = 31;

        // Use this to ADD a new event.
        public AddEvent()
        {
            InitializeComponent();
        }

        // Use this to EDIT an event.
        public AddEvent(int eventIdToEdit)
        {
            InitializeComponent();
            eventId = eventIdToEdit;
        }

        private void AddEvent_Load(object sender, EventArgs e)
        {
            btnSaveEvent.Enabled = false;
            isLoading = true;
            try
            {
                Database.loadDepartmentComboBox(cmbEventDepartment, "All Departments");
                cmbEventYear.SelectedIndex = 0;

                if (eventId == 0)
                {
                    lblTitle.Text = "Add Event";
                    this.Text = "SCC Records Management System - Add Event";
                    btnSaveEvent.Text = "Save Event";
                    setDefaultValues();
                }
                else
                {
                    lblTitle.Text = "Edit Event";
                    this.Text = "SCC Records Management System - Edit Event";
                    btnSaveEvent.Text = "Update Event";

                    bool found = loadEventToEdit();

                    if (found == false)
                    {
                        MessageBox.Show("This event was not found. It may have been deleted already.", "Event Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Database.showError("Could not load the event or its schedule. Close and reopen this window before saving.", ex);
                return;
            }
            finally
            {
                isLoading = false;
            }

            enableControls();
            updateExpectedStudents();
            updateScheduleInfo();
            btnSaveEvent.Enabled = true;
            txtEventName.Focus();
        }

        // ------------------------------------------------------------------
        // Filling the form
        // ------------------------------------------------------------------

        // The values of a new event.
        private void setDefaultValues()
        {
            txtEventName.Text = "";
            txtOrganizer.Text = "";
            txtVenue.Text = "";

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            if (cmbEventDepartment.Items.Count > 0)
            {
                cmbEventDepartment.SelectedIndex = 0;
            }

            cmbEventYear.SelectedIndex = 0;

            // Morning: 8:00 AM - 12:00 PM
            chkMorning.Checked = true;
            dtpMorningStart.Value = DateTime.Today.Add(new TimeSpan(8, 0, 0));
            dtpMorningEnd.Value = DateTime.Today.Add(new TimeSpan(12, 0, 0));

            // Afternoon: 1:00 PM - 5:00 PM
            chkAfternoon.Checked = true;
            dtpAfternoonStart.Value = DateTime.Today.Add(new TimeSpan(13, 0, 0));
            dtpAfternoonEnd.Value = DateTime.Today.Add(new TimeSpan(17, 0, 0));

            // Evening: 6:00 PM - 9:00 PM (not used by default)
            chkEvening.Checked = false;
            dtpEveningStart.Value = DateTime.Today.Add(new TimeSpan(18, 0, 0));
            dtpEveningEnd.Value = DateTime.Today.Add(new TimeSpan(21, 0, 0));

            chkLogoutNeeded.Checked = false;
        }

        // Puts the saved event in the form. Returns false when the event does not exist.
        private bool loadEventToEdit()
        {
            EventInfo eventInfo = Database.searchAndGetEventInfo(eventId);

            if (eventInfo == null)
            {
                return false;
            }

            txtEventName.Text = eventInfo.EventName;
            txtOrganizer.Text = eventInfo.Organizer;
            txtVenue.Text = eventInfo.Venue;

            dtpStartDate.Value = eventInfo.StartDate;
            dtpEndDate.Value = eventInfo.EndDate;

            // 0 = "All Departments"
            cmbEventDepartment.SelectedValue = eventInfo.DepartmentId;

            if (eventInfo.YearLevel == "")
            {
                cmbEventYear.SelectedIndex = 0;
            }
            else
            {
                cmbEventYear.SelectedItem = eventInfo.YearLevel;
            }

            chkMorning.Checked = eventInfo.MorningEnabled;
            dtpMorningStart.Value = DateTime.Today.Add(eventInfo.MorningStart);
            dtpMorningEnd.Value = DateTime.Today.Add(eventInfo.MorningEnd);

            chkAfternoon.Checked = eventInfo.AfternoonEnabled;
            dtpAfternoonStart.Value = DateTime.Today.Add(eventInfo.AfternoonStart);
            dtpAfternoonEnd.Value = DateTime.Today.Add(eventInfo.AfternoonEnd);

            chkEvening.Checked = eventInfo.EveningEnabled;
            dtpEveningStart.Value = DateTime.Today.Add(eventInfo.EveningStart);
            dtpEveningEnd.Value = DateTime.Today.Add(eventInfo.EveningEnd);

            chkLogoutNeeded.Checked = eventInfo.RequiresLogout;

            // When a session already started, students may already have attendance.
            // Changing the schedule now would delete it, so the schedule is locked.
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", eventId);

            int startedSessions = Database.GetCount("SELECT COUNT(*) FROM event_sessions WHERE event_id = @id AND starts_at <= NOW()", parameters);

            if (startedSessions > 0)
            {
                scheduleLocked = true;
            }
            else
            {
                scheduleLocked = false;
            }

            return true;
        }

        // Turns the controls on or off.
        private void enableControls()
        {
            bool canChangeSchedule = (scheduleLocked == false);

            dtpStartDate.Enabled = canChangeSchedule;
            dtpEndDate.Enabled = canChangeSchedule;
            cmbEventDepartment.Enabled = canChangeSchedule;
            cmbEventYear.Enabled = canChangeSchedule;
            chkMorning.Enabled = canChangeSchedule;
            chkAfternoon.Enabled = canChangeSchedule;
            chkEvening.Enabled = canChangeSchedule;
            chkLogoutNeeded.Enabled = canChangeSchedule;

            // The time pickers can only be used when their session is checked.
            dtpMorningStart.Enabled = canChangeSchedule && chkMorning.Checked;
            dtpMorningEnd.Enabled = canChangeSchedule && chkMorning.Checked;
            dtpAfternoonStart.Enabled = canChangeSchedule && chkAfternoon.Checked;
            dtpAfternoonEnd.Enabled = canChangeSchedule && chkAfternoon.Checked;
            dtpEveningStart.Enabled = canChangeSchedule && chkEvening.Checked;
            dtpEveningEnd.Enabled = canChangeSchedule && chkEvening.Checked;
        }

        // ------------------------------------------------------------------
        // Live information
        // ------------------------------------------------------------------

        private void chkSession_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            enableControls();
            updateScheduleInfo();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            // Helpful: move the end date together with the start date.
            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
            {
                dtpEndDate.Value = dtpStartDate.Value.Date;
            }

            updateScheduleInfo();
        }

        private void scheduleValue_Changed(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            updateScheduleInfo();
        }

        private void cmbParticipants_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading == true)
            {
                return;
            }

            updateExpectedStudents();
        }

        // Shows how many days and sessions the event will have.
        private void updateScheduleInfo()
        {
            int numberOfDays = (dtpEndDate.Value.Date - dtpStartDate.Value.Date).Days + 1;

            if (numberOfDays < 1)
            {
                lblNumberOfDays.Text = "End date is before the start date!";
                lblNumberOfDays.ForeColor = Color.Firebrick;
            }
            else if (numberOfDays > MAXIMUM_DAYS)
            {
                lblNumberOfDays.Text = numberOfDays + " days (maximum is " + MAXIMUM_DAYS + ")";
                lblNumberOfDays.ForeColor = Color.Firebrick;
            }
            else if (numberOfDays == 1)
            {
                lblNumberOfDays.Text = "1 day";
                lblNumberOfDays.ForeColor = Color.DarkSlateBlue;
            }
            else
            {
                lblNumberOfDays.Text = numberOfDays + " days";
                lblNumberOfDays.ForeColor = Color.DarkSlateBlue;
            }

            if (scheduleLocked == true)
            {
                lblScheduleInfo.Text = "This event has already started, so the dates, sessions, students and log out setting are LOCKED " +
                                       "(changing them would delete attendance). You can still change the name, venue and organizer.";
                lblScheduleInfo.ForeColor = Color.DarkOrange;
                return;
            }

            int sessionsPerDay = 0;

            if (chkMorning.Checked == true)
            {
                sessionsPerDay = sessionsPerDay + 1;
            }

            if (chkAfternoon.Checked == true)
            {
                sessionsPerDay = sessionsPerDay + 1;
            }

            if (chkEvening.Checked == true)
            {
                sessionsPerDay = sessionsPerDay + 1;
            }

            if (sessionsPerDay == 0)
            {
                lblScheduleInfo.Text = "Please check at least one session (Morning, Afternoon or Evening).";
                lblScheduleInfo.ForeColor = Color.Firebrick;
                return;
            }

            if (numberOfDays < 1 || numberOfDays > MAXIMUM_DAYS)
            {
                lblScheduleInfo.Text = "Please fix the dates of the event.";
                lblScheduleInfo.ForeColor = Color.Firebrick;
                return;
            }

            int totalSessions = numberOfDays * sessionsPerDay;

            string info = "Schedule: " + numberOfDays + " day(s) x " + sessionsPerDay + " session(s) per day = " +
                          totalSessions + " attendance session(s).\n" +
                          "Each session is a separate attendance. Every absence = 10 obligation points.";

            if (chkLogoutNeeded.Checked == true)
            {
                info = info + " Students must also LOG OUT.";
            }

            lblScheduleInfo.Text = info;
            lblScheduleInfo.ForeColor = Color.DarkSlateBlue;
        }

        // Shows how many students will be required to attend.
        private void updateExpectedStudents()
        {
            int studentCount = 0;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (scheduleLocked == true)
            {
                parameters.Add("@id", eventId);
                studentCount = Database.GetCount("SELECT COUNT(*) FROM event_students WHERE event_id = @id", parameters);
                lblExpectedStudents.Text = "Students of this event: " + studentCount;
                return;
            }

            parameters.Add("@department", Database.getSelectedId(cmbEventDepartment));
            parameters.Add("@year", getSelectedYearLevel());

            studentCount = Database.GetCount(
                "SELECT COUNT(*) FROM students " +
                "WHERE status = 'Active' AND face_registered = 1 " +
                "AND (@department = 0 OR department_id = @department) " +
                "AND (@year = '' OR year_level = @year)", parameters);

            lblExpectedStudents.Text = "Expected students: " + studentCount +
                                       " (active students with 5 registered faces). New matching students are added automatically until the event ends.";
        }

        // Returns "" when "All Year Levels" is selected.
        private string getSelectedYearLevel()
        {
            if (cmbEventYear.SelectedIndex <= 0)
            {
                return "";
            }

            return cmbEventYear.SelectedItem.ToString();
        }

        // Returns only the hour and minute of a time picker (no seconds).
        private TimeSpan getTime(DateTimePicker picker)
        {
            return new TimeSpan(picker.Value.Hour, picker.Value.Minute, 0);
        }

        // ------------------------------------------------------------------
        // Validation
        // ------------------------------------------------------------------

        private bool validateEvent()
        {
            string eventName = txtEventName.Text.Trim();
            string venue = txtVenue.Text.Trim();

            if (eventName == "")
            {
                MessageBox.Show("Please enter the event name.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEventName.Focus();
                return false;
            }

            if (eventName.Length < 3)
            {
                MessageBox.Show("The event name is too short (at least 3 characters).", "Invalid Event Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEventName.Focus();
                return false;
            }

            if (venue == "")
            {
                MessageBox.Show("Please enter the venue of the event.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVenue.Focus();
                return false;
            }

            // The same event name cannot be used twice on the same start date.
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@name", eventName);
            parameters.Add("@start", dtpStartDate.Value.Date);
            parameters.Add("@id", eventId);

            int sameEvent = Database.GetCount("SELECT COUNT(*) FROM events WHERE event_name = @name AND start_date = @start AND event_id <> @id", parameters);

            if (sameEvent > 0)
            {
                MessageBox.Show("An event named \"" + eventName + "\" that starts on " + dtpStartDate.Value.ToString("MMM dd, yyyy") + " already exists.",
                    "Duplicate Event", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEventName.Focus();
                return false;
            }

            // When the schedule is locked, the dates and sessions do not need to be checked.
            if (scheduleLocked == true)
            {
                return true;
            }

            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (endDate < startDate)
            {
                MessageBox.Show("The end date cannot be before the start date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEndDate.Focus();
                return false;
            }

            int numberOfDays = (endDate - startDate).Days + 1;

            if (numberOfDays > MAXIMUM_DAYS)
            {
                MessageBox.Show("An event can only last up to " + MAXIMUM_DAYS + " days.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEndDate.Focus();
                return false;
            }

            if (chkMorning.Checked == false && chkAfternoon.Checked == false && chkEvening.Checked == false)
            {
                MessageBox.Show("Please check at least one session (Morning, Afternoon or Evening).", "No Session", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkMorning.Focus();
                return false;
            }

            TimeSpan morningStart = getTime(dtpMorningStart);
            TimeSpan morningEnd = getTime(dtpMorningEnd);
            TimeSpan afternoonStart = getTime(dtpAfternoonStart);
            TimeSpan afternoonEnd = getTime(dtpAfternoonEnd);
            TimeSpan eveningStart = getTime(dtpEveningStart);
            TimeSpan eveningEnd = getTime(dtpEveningEnd);

            // Every session must end after it starts.
            if (chkMorning.Checked == true && morningEnd <= morningStart)
            {
                MessageBox.Show("The Morning end time must be later than its start time.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpMorningEnd.Focus();
                return false;
            }

            if (chkAfternoon.Checked == true && afternoonEnd <= afternoonStart)
            {
                MessageBox.Show("The Afternoon end time must be later than its start time.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpAfternoonEnd.Focus();
                return false;
            }

            if (chkEvening.Checked == true && eveningEnd <= eveningStart)
            {
                MessageBox.Show("The Evening end time must be later than its start time.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEveningEnd.Focus();
                return false;
            }

            // The sessions must be in order and must not overlap.
            if (chkMorning.Checked == true && chkAfternoon.Checked == true && afternoonStart < morningEnd)
            {
                MessageBox.Show("The Afternoon session must start after the Morning session ends.", "Overlapping Sessions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpAfternoonStart.Focus();
                return false;
            }

            if (chkAfternoon.Checked == true && chkEvening.Checked == true && eveningStart < afternoonEnd)
            {
                MessageBox.Show("The Evening session must start after the Afternoon session ends.", "Overlapping Sessions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEveningStart.Focus();
                return false;
            }

            if (chkMorning.Checked == true && chkAfternoon.Checked == false && chkEvening.Checked == true && eveningStart < morningEnd)
            {
                MessageBox.Show("The Evening session must start after the Morning session ends.", "Overlapping Sessions", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEveningStart.Focus();
                return false;
            }

            // A session that already ended cannot take attendance anymore.
            // We only need to check the first day, because the other days are later.
            DateTime now = DateTime.Now;

            if (chkMorning.Checked == true && startDate.Add(morningEnd) <= now)
            {
                MessageBox.Show("The Morning session of " + startDate.ToString("MMM dd, yyyy") + " has already ended.\nPlease change the start date or the time.",
                    "Session Already Ended", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpStartDate.Focus();
                return false;
            }

            if (chkAfternoon.Checked == true && startDate.Add(afternoonEnd) <= now)
            {
                MessageBox.Show("The Afternoon session of " + startDate.ToString("MMM dd, yyyy") + " has already ended.\nPlease change the start date or the time.",
                    "Session Already Ended", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpStartDate.Focus();
                return false;
            }

            if (chkEvening.Checked == true && startDate.Add(eveningEnd) <= now)
            {
                MessageBox.Show("The Evening session of " + startDate.ToString("MMM dd, yyyy") + " has already ended.\nPlease change the start date or the time.",
                    "Session Already Ended", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpStartDate.Focus();
                return false;
            }

            return true;
        }

        // ------------------------------------------------------------------
        // Saving
        // ------------------------------------------------------------------

        private void btnSaveEvent_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateEvent() == false)
                {
                    return;
                }
            }
            catch (MySqlException ex)
            {
                Database.showError("Could not verify the event schedule. No changes were saved.", ex);
                return;
            }

            string eventName = txtEventName.Text.Trim();
            string venue = txtVenue.Text.Trim();
            string organizer = txtOrganizer.Text.Trim();
            int departmentId = Database.getSelectedId(cmbEventDepartment);
            string yearLevel = getSelectedYearLevel();

            int sessionCount = 0;
            int studentCount = 0;
            int savedEventId = eventId;

            this.Cursor = Cursors.WaitCursor;
            btnSaveEvent.Enabled = false;

            MySqlConnection conn = null;
            MySqlTransaction transaction = null;

            try
            {
                conn = Database.GetConnection();
                conn.Open();

                // A transaction saves everything together: if one step fails, nothing is saved.
                transaction = conn.BeginTransaction();

                // Check again: the event could have started while this form was open.
                if (eventId != 0 && scheduleLocked == false)
                {
                    MySqlCommand cmdStarted = new MySqlCommand("SELECT COUNT(*) FROM event_sessions WHERE event_id = @id AND starts_at <= NOW()", conn, transaction);
                    cmdStarted.Parameters.AddWithValue("@id", eventId);
                    int startedSessions = Convert.ToInt32(cmdStarted.ExecuteScalar());
                    cmdStarted.Dispose();

                    if (startedSessions > 0)
                    {
                        transaction.Rollback();
                        MessageBox.Show("This event started while you were editing it, so its schedule can no longer be changed.\nPlease close this window and open the event again.",
                            "Event Already Started", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (scheduleLocked == true)
                {
                    // Only the name, venue and organizer are changed.
                    MySqlCommand cmdUpdateInfo = new MySqlCommand(
                        "UPDATE events SET event_name = @name, venue = @venue, organizer = @organizer WHERE event_id = @id", conn, transaction);
                    cmdUpdateInfo.Parameters.AddWithValue("@name", eventName);
                    cmdUpdateInfo.Parameters.AddWithValue("@venue", venue);
                    cmdUpdateInfo.Parameters.AddWithValue("@organizer", getNullIfEmpty(organizer));
                    cmdUpdateInfo.Parameters.AddWithValue("@id", eventId);
                    cmdUpdateInfo.ExecuteNonQuery();
                    cmdUpdateInfo.Dispose();

                    transaction.Commit();

                    MessageBox.Show("Event updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }

                string query;

                if (eventId == 0)
                {
                    query = "INSERT INTO events (event_name, venue, organizer, start_date, end_date, department_id, year_level, requires_logout, " +
                            "morning_enabled, morning_start, morning_end, afternoon_enabled, afternoon_start, afternoon_end, " +
                            "evening_enabled, evening_start, evening_end, created_by) " +
                            "VALUES (@name, @venue, @organizer, @start, @end, @department, @year, @logout, " +
                            "@morning, @morningStart, @morningEnd, @afternoon, @afternoonStart, @afternoonEnd, " +
                            "@evening, @eveningStart, @eveningEnd, @createdBy)";
                }
                else
                {
                    query = "UPDATE events SET event_name = @name, venue = @venue, organizer = @organizer, start_date = @start, end_date = @end, " +
                            "department_id = @department, year_level = @year, requires_logout = @logout, " +
                            "morning_enabled = @morning, morning_start = @morningStart, morning_end = @morningEnd, " +
                            "afternoon_enabled = @afternoon, afternoon_start = @afternoonStart, afternoon_end = @afternoonEnd, " +
                            "evening_enabled = @evening, evening_start = @eveningStart, evening_end = @eveningEnd " +
                            "WHERE event_id = @id";
                }

                MySqlCommand cmdEvent = new MySqlCommand(query, conn, transaction);
                cmdEvent.Parameters.AddWithValue("@name", eventName);
                cmdEvent.Parameters.AddWithValue("@venue", venue);
                cmdEvent.Parameters.AddWithValue("@organizer", getNullIfEmpty(organizer));
                cmdEvent.Parameters.AddWithValue("@start", dtpStartDate.Value.Date);
                cmdEvent.Parameters.AddWithValue("@end", dtpEndDate.Value.Date);

                if (departmentId == 0)
                {
                    cmdEvent.Parameters.AddWithValue("@department", DBNull.Value);
                }
                else
                {
                    cmdEvent.Parameters.AddWithValue("@department", departmentId);
                }

                cmdEvent.Parameters.AddWithValue("@year", getNullIfEmpty(yearLevel));
                cmdEvent.Parameters.AddWithValue("@logout", chkLogoutNeeded.Checked ? 1 : 0);
                cmdEvent.Parameters.AddWithValue("@morning", chkMorning.Checked ? 1 : 0);
                cmdEvent.Parameters.AddWithValue("@morningStart", getTime(dtpMorningStart));
                cmdEvent.Parameters.AddWithValue("@morningEnd", getTime(dtpMorningEnd));
                cmdEvent.Parameters.AddWithValue("@afternoon", chkAfternoon.Checked ? 1 : 0);
                cmdEvent.Parameters.AddWithValue("@afternoonStart", getTime(dtpAfternoonStart));
                cmdEvent.Parameters.AddWithValue("@afternoonEnd", getTime(dtpAfternoonEnd));
                cmdEvent.Parameters.AddWithValue("@evening", chkEvening.Checked ? 1 : 0);
                cmdEvent.Parameters.AddWithValue("@eveningStart", getTime(dtpEveningStart));
                cmdEvent.Parameters.AddWithValue("@eveningEnd", getTime(dtpEveningEnd));
                cmdEvent.Parameters.AddWithValue("@createdBy", Instantiate.currentUser.AccountId);
                cmdEvent.Parameters.AddWithValue("@id", eventId);
                cmdEvent.ExecuteNonQuery();

                if (eventId == 0)
                {
                    savedEventId = Convert.ToInt32(cmdEvent.LastInsertedId);
                }

                cmdEvent.Dispose();

                // When editing, remove the old sessions and students first. They are created again below.
                // (The event has not started yet, so there is no attendance to lose.)
                if (eventId != 0)
                {
                    MySqlCommand cmdDeleteSessions = new MySqlCommand("DELETE FROM event_sessions WHERE event_id = @id", conn, transaction);
                    cmdDeleteSessions.Parameters.AddWithValue("@id", eventId);
                    cmdDeleteSessions.ExecuteNonQuery();
                    cmdDeleteSessions.Dispose();

                    MySqlCommand cmdDeleteStudents = new MySqlCommand("DELETE FROM event_students WHERE event_id = @id", conn, transaction);
                    cmdDeleteStudents.Parameters.AddWithValue("@id", eventId);
                    cmdDeleteStudents.ExecuteNonQuery();
                    cmdDeleteStudents.Dispose();
                }

                // Create one session for every checked period of every day.
                DateTime day = dtpStartDate.Value.Date;

                while (day <= dtpEndDate.Value.Date)
                {
                    if (chkMorning.Checked == true)
                    {
                        insertSession(conn, transaction, savedEventId, day, "Morning", getTime(dtpMorningStart), getTime(dtpMorningEnd));
                        sessionCount = sessionCount + 1;
                    }

                    if (chkAfternoon.Checked == true)
                    {
                        insertSession(conn, transaction, savedEventId, day, "Afternoon", getTime(dtpAfternoonStart), getTime(dtpAfternoonEnd));
                        sessionCount = sessionCount + 1;
                    }

                    if (chkEvening.Checked == true)
                    {
                        insertSession(conn, transaction, savedEventId, day, "Evening", getTime(dtpEveningStart), getTime(dtpEveningEnd));
                        sessionCount = sessionCount + 1;
                    }

                    day = day.AddDays(1);
                }

                // Add the students who must attend: ACTIVE students with 5 registered faces
                // of the selected department and year level.
                MySqlCommand cmdStudents = new MySqlCommand(
                    "INSERT INTO event_students (event_id, student_id, added_at) " +
                    "SELECT @id, student_id, NOW() FROM students " +
                    "WHERE status = 'Active' AND face_registered = 1 " +
                    "AND (@department = 0 OR department_id = @department) " +
                    "AND (@year = '' OR year_level = @year)", conn, transaction);
                cmdStudents.Parameters.AddWithValue("@id", savedEventId);
                cmdStudents.Parameters.AddWithValue("@department", departmentId);
                cmdStudents.Parameters.AddWithValue("@year", yearLevel);
                studentCount = cmdStudents.ExecuteNonQuery();
                cmdStudents.Dispose();

                transaction.Commit();

                string message;

                if (eventId == 0)
                {
                    message = "Event saved successfully!";
                }
                else
                {
                    message = "Event updated successfully!";
                }

                message = message + "\n\n" + sessionCount + " attendance session(s) were created.\n" +
                          studentCount + " student(s) were added to the event.";

                if (studentCount == 0)
                {
                    message = message + "\n\nNote: No student matches yet. Students who register later are added automatically.";
                }

                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                }
                catch
                {
                    // The connection may already be closed. Nothing else to do.
                }

                Database.showError("The event was not saved.", ex);
            }
            finally
            {
                if (conn != null) conn.Dispose();

                this.Cursor = Cursors.Default;
                btnSaveEvent.Enabled = true;
            }
        }

        // Saves one session (example: Day 1 Morning from 8:00 AM to 12:00 PM).
        private void insertSession(MySqlConnection conn, MySqlTransaction transaction, int savedEventId, DateTime day, string sessionName, TimeSpan startTime, TimeSpan endTime)
        {
            MySqlCommand cmdSession = new MySqlCommand(
                "INSERT INTO event_sessions (event_id, session_date, session_name, starts_at, ends_at) " +
                "VALUES (@eventId, @date, @name, @startsAt, @endsAt)", conn, transaction);
            cmdSession.Parameters.AddWithValue("@eventId", savedEventId);
            cmdSession.Parameters.AddWithValue("@date", day.Date);
            cmdSession.Parameters.AddWithValue("@name", sessionName);
            cmdSession.Parameters.AddWithValue("@startsAt", day.Date.Add(startTime));
            cmdSession.Parameters.AddWithValue("@endsAt", day.Date.Add(endTime));
            cmdSession.ExecuteNonQuery();
            cmdSession.Dispose();
        }

        // Empty text is saved as NULL in the database.
        private object getNullIfEmpty(string text)
        {
            if (text == "")
            {
                return DBNull.Value;
            }

            return text;
        }

        // ------------------------------------------------------------------
        // Reset and cancel
        // ------------------------------------------------------------------

        private void btnResetEvent_Click(object sender, EventArgs e)
        {
            string question;

            if (eventId == 0)
            {
                question = "Clear everything you entered?";
            }
            else
            {
                question = "Undo your changes and show the saved values again?";
            }

            DialogResult answer = MessageBox.Show(question, "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            isLoading = true;
            btnSaveEvent.Enabled = false;
            try
            {
                if (eventId == 0)
                {
                    setDefaultValues();
                }
                else if (!loadEventToEdit())
                {
                    MessageBox.Show("This event could not be reloaded. Close this window and reopen it from Event Management.");
                    return;
                }
            }
            finally
            {
                isLoading = false;
            }

            enableControls();
            updateExpectedStudents();
            updateScheduleInfo();
            btnSaveEvent.Enabled = cmbEventDepartment.DataSource != null;
            txtEventName.Focus();
        }

        private void btnCancelEvent_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
