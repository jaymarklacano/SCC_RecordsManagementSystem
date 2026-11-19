using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Face;
using MySql.Data.MySqlClient;

namespace SCCRecordsManagementSystem
{
    public partial class FaceRecognition : Form
    {
        private readonly PictureBox preview = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Black };
        private readonly ComboBox action = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly System.Windows.Forms.Timer scanTimer = new System.Windows.Forms.Timer { Interval = 100 };
        private VideoCapture camera;
        private CascadeClassifier detector;
        private LBPHFaceRecognizer recognizer;
        private Mat frame;
        private Label message, faceInfo;
        private int candidate, consecutive, unknownFrames, lastStudent, missedFrames;
        private bool unknownShown;
        private DateTime lastFace = DateTime.MinValue;
        private DateTime nextAttempt = DateTime.MinValue;
        // A face is accepted as a student when the LBPH distance is at most this number (lower = stricter).
        internal const double MatchThreshold = 70;
        // The same student must be matched in this many pictures in a row before attendance is recorded.
        private const int FramesNeeded = 3;
        // After this many pictures in a row without a match, the face is called "unknown".
        private const int UnknownFramesNeeded = 5;
        public FaceRecognition()
        {
            InitializeComponent();
            Program.PrepareWindow(this, true); WireMenu();
            panel4.Controls.Add(preview);
            label2.AutoSize = false; label2.SetBounds(10, 4, 455, 35); label2.Font = new Font("Cambria", 10);
            action.SetBounds(100, 45, 130, 25); action.Items.AddRange(new object[] { "Log In", "Log Out" }); action.SelectedIndex = 0;
            panel2.Controls.Add(action); Program.AddLabel(panel2, "Action:", 12, 46, 80);
            label3.Text = "Session:"; label3.Left = 10; comboBox1.Left = 80; comboBox1.Width = 385;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            faceInfo = Program.AddLabel(panel3, "", 10, 56, 455, 22); faceInfo.Font = new Font("Cambria", 9);
            comboBox1.SelectionChangeCommitted += delegate { ResetRecognition(); };
            action.SelectedIndexChanged += delegate { ResetRecognition(); };
            message = Program.AddLabel(panel6, "Waiting for a student.", 15, 292, 450, 70);
            foreach (Label label in new[] { label5, label6, label7, label8, label9 })
            {
                label.AutoSize = false; label.Width = 230; label.Height = 45; label.Font = new Font("Cambria", 10);
            }
            label5.Top = 20; label6.Top = 70; label7.Top = 120; label8.Top = 170; label9.Top = 220;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            button2.Text = "Restart / Reload"; button2.Click += Start;
            button1.Click += delegate { ResetRecognition(); };
            scanTimer.Tick += Scan;
            Shown += Start;
            FormClosed += delegate { Stop(); scanTimer.Dispose(); Registration.ReplaceImage(preview, null); Registration.ReplaceImage(pictureBox1, null); };
        }

        private void Start(object sender, EventArgs e)
        {
            try
            {
                Database.Require("Face Recognition");
                Stop(); ResetRecognition(); faceInfo.Text = "";
                Status("Loading active sessions and registered faces, please wait...", Color.MidnightBlue);
                label2.Refresh(); Cursor = Cursors.WaitCursor;
                Program.Choices(comboBox1, Database.GetData(@"SELECT es.session_id,CONCAT(e.event_name,' - ',DATE_FORMAT(es.starts_at,'%b %e'),' ',es.session_name) AS name
                    FROM event_sessions es JOIN attendance_events e ON e.event_id=es.event_id
                    WHERE NOW()>=es.starts_at AND NOW()<es.ends_at ORDER BY es.starts_at"), "session_id", "name");
                if (comboBox1.Items.Count == 0) throw new InvalidOperationException("No active session. Create an event or wait for its start, then click Restart / Reload.");
                detector = Registration.CreateDetector();
                // Only active students with all 5 pictures are loaded into the recognizer.
                DataTable data = Database.GetData(@"SELECT s.student_id,f.image_data FROM students s JOIN face_images f ON f.student_id=s.student_id
                    WHERE s.status='Active' AND s.face_registered=1 AND (SELECT COUNT(*) FROM face_images x WHERE x.student_id=s.student_id)=5
                    ORDER BY s.student_id,f.image_number");
                List<Mat> training = new List<Mat>(); List<int> ids = new List<int>(); int damaged = 0;
                try
                {
                    foreach (DataRow row in data.Rows)
                    {
                        using (Mat image = Cv2.ImDecode((byte[])row["image_data"], ImreadModes.Color))
                        {
                            // A damaged picture is skipped so one bad record does not stop attendance for everybody.
                            if (image.Empty()) { damaged++; continue; }
                            training.Add(Registration.PrepareFace(image)); ids.Add(Convert.ToInt32(row["student_id"]));
                        }
                    }
                    if (training.Count == 0) throw new InvalidOperationException("No registered faces. Add students with five face pictures first.");
                    // The label of each picture is the student_id, so the answer tells us who it is.
                    recognizer = LBPHFaceRecognizer.Create();
                    recognizer.Train(training, ids);
                }
                finally { foreach (Mat image in training) image.Dispose(); }
                faceInfo.Text = ids.Distinct().Count() + " student(s) with registered faces (" + training.Count + " face pictures loaded)" +
                    (damaged > 0 ? " - " + damaged + " damaged picture(s) skipped, re-register them." : ".");
                faceInfo.ForeColor = damaged > 0 ? Color.Firebrick : Color.MidnightBlue;
                Status("Starting the webcam, please wait...", Color.MidnightBlue); label2.Refresh();
                camera = Registration.OpenCamera(); frame = new Mat(); missedFrames = 0;
                scanTimer.Start(); Status("Camera is on. Waiting for a student... (one person at a time)", Color.MidnightBlue);
            }
            catch (Exception ex)
            {
                Stop(); Status(Database.ErrorMessage(ex), Color.Firebrick);
                message.Text = label2.Text; message.ForeColor = Color.Firebrick;
            }
            finally { Cursor = Cursors.Default; }
        }

        private void ResetRecognition()
        {
            candidate = consecutive = unknownFrames = lastStudent = 0;
            unknownShown = false;
            nextAttempt = DateTime.MinValue;
            message.Text = "Waiting for a student.";
            message.ForeColor = Color.MidnightBlue;
            label5.Text = "Name:"; label6.Text = "Student ID:"; label7.Text = "Department:"; label8.Text = "Year:"; label9.Text = "Status:";
            Registration.ReplaceImage(pictureBox1, null);
        }

        private void Status(string text, Color color)
        {
            label2.Text = text; label2.ForeColor = color;
        }

        // Runs about 10 times per second while the webcam is on.
        private void Scan(object sender, EventArgs e)
        {
            try
            {
                if (!camera.Read(frame) || frame.Empty())
                {
                    if (++missedFrames >= 10) throw new InvalidOperationException("Webcam disconnected. Reconnect it and click Restart / Reload.");
                    return;
                }
                missedFrames = 0;
                Rect[] faces = Registration.Detect(detector, frame);
                Scalar boxColor = Recognize(faces);
                Registration.ShowFrame(preview, frame, faces, boxColor);
            }
            catch (Exception ex)
            {
                Stop(); Status(Database.ErrorMessage(ex), Color.Firebrick);
                message.Text = label2.Text; message.ForeColor = Color.Firebrick;
            }
        }

        // Decides what to do with the faces in the current camera picture.
        // Returns the color of the box drawn around the face: gray/orange = problem, gold = checking,
        // red = unknown, green = recognized.
        private Scalar Recognize(Rect[] faces)
        {
            if (faces.Length > 0) lastFace = DateTime.Now;
            string problem = Registration.CheckFace(frame, faces);
            if (problem != "")
            {
                candidate = consecutive = unknownFrames = 0;
                Status(problem, faces.Length == 0 ? Color.DimGray : Color.Firebrick);
                // The student card stays until nobody has been in front of the camera for 3 seconds.
                if (faces.Length == 0 && (lastStudent != 0 || unknownShown) && DateTime.Now - lastFace > TimeSpan.FromSeconds(3)) ResetRecognition();
                return faces.Length == 0 ? Scalar.Gray : Scalar.Orange;
            }
            int id; double distance;
            using (Mat crop = new Mat(frame, faces[0]))
            using (Mat prepared = Registration.PrepareFace(crop))
                recognizer.Predict(prepared, out id, out distance);
            if (id <= 0 || distance > MatchThreshold)
            {
                // One unclear picture is normal, so wait for several in a row before calling the face unknown.
                candidate = consecutive = 0;
                if (++unknownFrames < UnknownFramesNeeded) { Status("Recognizing your face...", Color.MidnightBlue); return Scalar.Gold; }
                if (!unknownShown)
                {
                    ResetRecognition(); unknownShown = true; unknownFrames = UnknownFramesNeeded;
                    message.Text = "UNKNOWN FACE\nThis face is not registered. No attendance recorded."; message.ForeColor = Color.Firebrick;
                }
                Status("Unknown face. This person is not registered or the face is not clear.", Color.Firebrick);
                return Scalar.Red;
            }
            unknownFrames = 0;
            if (id == candidate) consecutive++; else { candidate = id; consecutive = 1; }
            if (consecutive < FramesNeeded) { Status("Recognizing your face... " + consecutive + "/" + FramesNeeded, Color.MidnightBlue); return Scalar.Gold; }
            // This student's card is already shown: do nothing, so attendance is not recorded again and again.
            if (id == lastStudent)
            {
                Status("Attendance checked. Step away for the next student, or change the action to Log Out.", Color.DarkGreen);
                return Scalar.LimeGreen;
            }
            if (DateTime.Now < nextAttempt) return Scalar.Gold;
            ShowStudent(id, faces[0]);
            return Scalar.LimeGreen;
        }

        // Shows the recognized student and records the attendance.
        private void ShowStudent(int id, Rect face)
        {
            DataTable found = Database.GetData(@"SELECT s.*,d.department_code FROM students s JOIN departments d ON d.department_id=s.department_id
                WHERE student_id=@id", new Dictionary<string, object> { { "@id", id } });
            if (found.Rows.Count == 0)
            {
                ResetRecognition(); message.Text = "This student was deleted. Click Restart / Reload to refresh the face list.";
                nextAttempt = DateTime.Now.AddSeconds(5); return;
            }
            DataRow student = found.Rows[0];
            unknownShown = false;
            label5.Text = "Name: " + student["first_name"] + " " + student["last_name"];
            label6.Text = "ID: " + student["student_number"]; label7.Text = "Department: " + student["department_code"]; label8.Text = "Year: " + student["year_level"];
            using (Mat crop = new Mat(frame, face)) Registration.ReplaceImage(pictureBox1, Registration.ToBitmap(crop));
            try
            {
                string result = RecordAttendance(id, Program.SelectedId(comboBox1), action.Text);
                message.Text = "Student Recognized\n" + result; message.ForeColor = Color.DarkGreen;
                label9.Text = "Status: Present"; lastStudent = id;
                Status("Student recognized. Ready for the next student.", Color.DarkGreen);
            }
            catch (Exception ex)
            {
                // Try again after 5 seconds (for example when the database was busy).
                message.Text = Database.ErrorMessage(ex); message.ForeColor = Color.Firebrick;
                label9.Text = "Status: Not recorded"; nextAttempt = DateTime.Now.AddSeconds(5);
                Status("Attendance was not recorded. Trying again in 5 seconds...", Color.Firebrick);
            }
        }

        public static string RecordAttendance(int studentId, int sessionId, string actionName)
        {
            Database.Require("Face Recognition");
            if (actionName != "Log In" && actionName != "Log Out") throw new ArgumentException("Choose Log In or Log Out.");
            Dictionary<string, object> p = new Dictionary<string, object> { { "@student", studentId }, { "@session", sessionId } };
            using (MySqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    DataTable sessions = new DataTable();
                    using (MySqlCommand cmd = Database.Command(connection, @"SELECT es.event_id,e.requires_logout,
                        (NOW()>=es.starts_at AND NOW()<es.ends_at) AS is_open FROM event_sessions es
                        JOIN attendance_events e ON e.event_id=es.event_id WHERE es.session_id=@session FOR UPDATE", p, transaction))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) adapter.Fill(sessions);
                    if (sessions.Rows.Count == 0 || !Convert.ToBoolean(sessions.Rows[0]["is_open"]))
                        throw new InvalidOperationException("This session is not open. Click Restart / Reload to select an active session.");
                    p.Add("@event", sessions.Rows[0]["event_id"]);
                    using (MySqlCommand cmd = Database.Command(connection, @"SELECT COUNT(*) FROM event_students r JOIN students s ON s.student_id=r.student_id
                        WHERE r.event_id=@event AND r.student_id=@student AND s.status='Active' AND s.face_registered=1
                        AND (SELECT COUNT(*) FROM face_images f WHERE f.student_id=s.student_id)=5", p, transaction))
                        if (Convert.ToInt32(cmd.ExecuteScalar()) != 1) throw new InvalidOperationException("Student is not active, not fully registered, or not on this event's roster.");
                    DataTable records = new DataTable();
                    using (MySqlCommand cmd = Database.Command(connection,
                        "SELECT time_in,time_out FROM event_attendance WHERE session_id=@session AND student_id=@student FOR UPDATE", p, transaction))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) adapter.Fill(records);
                    string result;
                    if (actionName == "Log In")
                    {
                        if (records.Rows.Count > 0) result = "Attendance already recorded at " + Convert.ToDateTime(records.Rows[0]["time_in"]).ToString("h:mm tt") + ".";
                        else
                        {
                            using (MySqlCommand cmd = Database.Command(connection,
                                "INSERT INTO event_attendance(session_id,student_id,time_in) VALUES(@session,@student,NOW())", p, transaction)) cmd.ExecuteNonQuery();
                            using (MySqlCommand cmd = Database.Command(connection, "SELECT NOW()", null, transaction))
                                result = "Log In successfully recorded at " + Convert.ToDateTime(cmd.ExecuteScalar()).ToString("h:mm tt") + ".";
                        }
                        if (Convert.ToBoolean(sessions.Rows[0]["requires_logout"])) result += "\nPlease scan Log Out before the session ends.";
                    }
                    else
                    {
                        if (records.Rows.Count == 0) throw new InvalidOperationException("Log in first before logging out.");
                        if (records.Rows[0]["time_out"] != DBNull.Value) result = "Already logged out at " + Convert.ToDateTime(records.Rows[0]["time_out"]).ToString("h:mm tt") + ".";
                        else
                        {
                            using (MySqlCommand cmd = Database.Command(connection,
                                "UPDATE event_attendance SET time_out=NOW() WHERE session_id=@session AND student_id=@student", p, transaction)) cmd.ExecuteNonQuery();
                            using (MySqlCommand cmd = Database.Command(connection, "SELECT NOW()", null, transaction))
                                result = "Log Out successfully recorded at " + Convert.ToDateTime(cmd.ExecuteScalar()).ToString("h:mm tt") + ".";
                        }
                    }
                    transaction.Commit(); return result;
                }
            }
        }

        private void WireMenu()
        {
            button12.Click += delegate { Program.Logout(this); };
            button13.Click += delegate { Program.OpenPage(this, "Dashboard", () => new Dashboard()); };
            button14.Enabled = button20.Enabled = Database.IsAdmin;
            button14.Click += delegate { Program.OpenPage(this, "Rules and Permission", () => new RulesPermission()); };
            button15.Click += delegate { Program.OpenPage(this, "Obligations", () => new Obligations()); };
            button16.Click += delegate { Program.OpenPage(this, "Reports and Analysis", () => new ReportsAndAnalysis()); };
            button17.Click += delegate { Program.OpenPage(this, "Attendance Monitoring", () => new AttendanceMonitoring()); };
            button18.Click += delegate { Program.OpenPage(this, "Event Management", () => new Event_Management()); };
            button19.Click += Start;
            button20.Click += delegate { Program.OpenPage(this, "Officer Records", () => new Officerr_Records()); };
            button21.Click += delegate { Program.OpenPage(this, "Student Records", () => new StudentRecords()); };
            button22.Click += delegate { Program.OpenPage(this, "Departments", () => new Departments()); };
        }

        private void Stop()
        {
            scanTimer.Stop();
            if (camera != null) { camera.Dispose(); camera = null; }
            if (frame != null) { frame.Dispose(); frame = null; }
            if (detector != null) { detector.Dispose(); detector = null; }
            if (recognizer != null) { recognizer.Dispose(); recognizer = null; }
            Registration.ReplaceImage(preview, null);
        }
    }
}
