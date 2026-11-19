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
        private readonly System.Windows.Forms.Timer scanTimer = new System.Windows.Forms.Timer { Interval = 120 };
        private VideoCapture camera;
        private CascadeClassifier detector;
        private LBPHFaceRecognizer recognizer;
        private Mat frame;
        private Label message;
        private int candidate, consecutive, lastStudent, missedFrames;
        private DateTime lastFace = DateTime.MinValue;
        private DateTime nextAttempt = DateTime.MinValue;
        internal const double MatchThreshold = 70;
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
                Stop(); ResetRecognition();
                Program.Choices(comboBox1, Database.GetData(@"SELECT es.session_id,CONCAT(e.event_name,' - ',DATE_FORMAT(es.starts_at,'%b %e'),' ',es.session_name) AS name
                    FROM event_sessions es JOIN attendance_events e ON e.event_id=es.event_id
                    WHERE NOW()>=es.starts_at AND NOW()<es.ends_at ORDER BY es.starts_at"), "session_id", "name");
                if (comboBox1.Items.Count == 0) throw new InvalidOperationException("No active session. Create an event or wait for its start, then click Restart / Reload.");
                detector = Registration.CreateDetector();
                recognizer = LBPHFaceRecognizer.Create();
                DataTable data = Database.GetData(@"SELECT s.student_id,f.image_data FROM students s JOIN face_images f ON f.student_id=s.student_id
                    WHERE s.status='Active' AND s.face_registered=1 ORDER BY s.student_id,f.image_number");
                if (data.Rows.Count == 0) throw new InvalidOperationException("No registered faces. Add students with five face pictures first.");
                foreach (var group in data.AsEnumerable().GroupBy(r => Convert.ToInt32(r["student_id"])))
                    if (group.Count() != 5) throw new InvalidOperationException("Student " + group.Key + " has incomplete face data. Re-register that student.");
                List<Mat> training = new List<Mat>(); List<int> ids = new List<int>();
                try
                {
                    foreach (DataRow row in data.Rows)
                    {
                        using (Mat image = Cv2.ImDecode((byte[])row["image_data"], ImreadModes.Color))
                        {
                            if (image.Empty()) throw new InvalidOperationException("A registered picture is damaged. Re-register student " + row["student_id"] + ".");
                            training.Add(Registration.PrepareFace(image)); ids.Add(Convert.ToInt32(row["student_id"]));
                        }
                    }
                    recognizer.Train(training, ids);
                }
                finally { foreach (Mat image in training) image.Dispose(); }
                camera = Registration.OpenCamera(); frame = new Mat(); missedFrames = 0;
                scanTimer.Start(); label2.Text = "Camera ready. One person at a time.";
            }
            catch (Exception ex) { Stop(); label2.Text = Database.ErrorMessage(ex); message.Text = label2.Text; }
        }

        private void ResetRecognition()
        {
            candidate = consecutive = lastStudent = 0;
            nextAttempt = DateTime.MinValue;
            message.Text = "Waiting for a student.";
            message.ForeColor = Color.MidnightBlue;
            label5.Text = "Name:"; label6.Text = "Student ID:"; label7.Text = "Department:"; label8.Text = "Year:"; label9.Text = "Status:";
            Registration.ReplaceImage(pictureBox1, null);
        }

        private void Scan(object sender, EventArgs e)
        {
            try
            {
                if (!camera.Read(frame) || frame.Empty())
                {
                    if (++missedFrames >= 10) throw new InvalidOperationException("Webcam disconnected. Reconnect it and click Restart.");
                    return;
                }
                missedFrames = 0;
                Registration.ReplaceImage(preview, Registration.ToBitmap(frame));
                Rect[] faces = Registration.Detect(detector, frame);
                if (faces.Length != 1)
                {
                    candidate = consecutive = 0;
                    label2.Text = faces.Length == 0 ? "No face detected. Look at the camera." : "Multiple faces detected. One person only.";
                    if (DateTime.Now - lastFace > TimeSpan.FromSeconds(3)) ResetRecognition();
                    return;
                }
                lastFace = DateTime.Now;
                using (Mat crop = new Mat(frame, faces[0]))
                {
                    try { Registration.CheckFace(crop); }
                    catch (InvalidOperationException ex) { label2.Text = ex.Message; candidate = consecutive = 0; return; }
                    using (Mat prepared = Registration.PrepareFace(crop))
                    {
                        int id; double distance; recognizer.Predict(prepared, out id, out distance);
                        if (id <= 0 || distance > MatchThreshold)
                        {
                            ResetRecognition();
                            label2.Text = "Unknown face. This student is not registered or the face is not clear.";
                            message.Text = "Unknown face. No attendance recorded.";
                            return;
                        }
                        if (id == candidate) consecutive++; else { candidate = id; consecutive = 1; }
                        if (id == lastStudent)
                        {
                            label2.Text = "Attendance checked. Step away for the next student, or change action to Log Out.";
                            return;
                        }
                        label2.Text = "Recognizing... " + Math.Min(consecutive, 3) + "/3";
                        if (consecutive < 3 || DateTime.Now < nextAttempt) return;
                        DataTable found = Database.GetData(@"SELECT s.*,d.department_code FROM students s JOIN departments d ON d.department_id=s.department_id
                            WHERE student_id=@id", new Dictionary<string, object> { { "@id", id } });
                        if (found.Rows.Count == 0)
                        {
                            ResetRecognition(); message.Text = "This student was deleted. Click Restart / Reload to refresh the face list."; return;
                        }
                        DataRow student = found.Rows[0];
                        label5.Text = "Name: " + student["first_name"] + " " + student["last_name"];
                        label6.Text = "ID: " + student["student_number"]; label7.Text = "Department: " + student["department_code"]; label8.Text = "Year: " + student["year_level"];
                        Registration.ReplaceImage(pictureBox1, Registration.ToBitmap(crop));
                        try
                        {
                            string result = RecordAttendance(id, Program.SelectedId(comboBox1), action.Text);
                            message.Text = "Student Recognized\n" + result; message.ForeColor = Color.DarkGreen;
                            label9.Text = "Status: Present"; lastStudent = id; label2.Text = "Student recognized. Ready for the next student.";
                        }
                        catch (Exception ex)
                        {
                            message.Text = Database.ErrorMessage(ex); message.ForeColor = Color.Firebrick;
                            label9.Text = "Status: Not recorded"; nextAttempt = DateTime.Now.AddSeconds(5);
                        }
                    }
                }
            }
            catch (Exception ex) { Stop(); label2.Text = Database.ErrorMessage(ex); message.Text = label2.Text; }
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
