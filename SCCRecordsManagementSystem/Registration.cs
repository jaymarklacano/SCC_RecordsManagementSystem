using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using OpenCvSharp;

namespace SCCRecordsManagementSystem
{
    public partial class Registration : Form
    {
        private readonly int studentId;
        private readonly List<byte[]> pictures = new List<byte[]>();
        private readonly PictureBox[] thumbnails = new PictureBox[5];
        private readonly CheckBox replaceFaces = new CheckBox { Text = "Re-register all 5 face pictures", AutoSize = true };
        private readonly ComboBox status = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly System.Windows.Forms.Timer cameraTimer = new System.Windows.Forms.Timer { Interval = 50 };
        // Short tips shown to the student for each of the 5 pictures.
        private static readonly string[] PoseTips = { "look straight at the camera", "turn your head slightly to the left",
            "turn your head slightly to the right", "tilt your head slightly up", "look at the camera normally or smile" };
        private Label captureStatus;
        private VideoCapture camera;
        private CascadeClassifier detector;
        private Mat frame;
        private int failedFrames;
        private DateTime holdMessageUntil = DateTime.MinValue;
        public Registration() : this(0) { }
        public Registration(int id)
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            studentId = id;
            Program.PrepareWindow(this);
            AcceptButton = button1; CancelButton = button3;
            textBox2.MaxLength = 20; textBox1.MaxLength = textBox3.MaxLength = textBox4.MaxLength = 50;
            textBox5.MaxLength = 100; textBox6.MaxLength = 20;
            Program.AddLabel(panel1, id == 0 ? "New Student - all fields marked * are required" : "Edit Student", 30, 20, 550);
            label9.Text = "ID Number *"; label1.Text = "First Name *"; label3.Text = "Last Name *";
            label4.Text = "Department *"; label5.Text = "Year *"; label6.Text = "Gender *";
            Program.Choices(comboBox1, Database.GetData("SELECT department_id,department_code FROM departments ORDER BY department_code"), "department_id", "department_code");
            comboBox2.DropDownStyle = comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Clear(); comboBox2.Items.AddRange(new object[] { "1st Year", "2nd Year", "3rd Year", "4th Year", "5th Year" }); comboBox2.SelectedIndex = 0;
            comboBox3.Items.Clear(); comboBox3.Items.AddRange(new object[] { "Male", "Female", "Other" }); comboBox3.SelectedIndex = 0;
            status.Items.AddRange(new object[] { "Active", "Inactive" }); status.SelectedIndex = 0;
            status.SetBounds(450, 432, 130, 25); panel1.Controls.Add(status);
            replaceFaces.Location = new System.Drawing.Point(173, 474); panel1.Controls.Add(replaceFaces);
            replaceFaces.Checked = id == 0; replaceFaces.Enabled = id > 0;
            pictureBox1.SetBounds(20, 10, 580, 320); pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; pictureBox1.BackColor = Color.Black;
            captureStatus = Program.AddLabel(panel2, "Click START CAMERA. Look straight, then turn slightly left/right/up and look straight again.", 20, 335, 580, 65);
            for (int i = 0; i < 5; i++)
            {
                thumbnails[i] = new PictureBox { Location = new System.Drawing.Point(20 + i * 115, 410),
                    Size = new System.Drawing.Size(100, 90), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
                panel2.Controls.Add(thumbnails[i]);
            }
            button5.SetBounds(664, 551, 185, 45); button5.Text = "START CAMERA";
            button4.SetBounds(857, 551, 185, 45);
            Button retake = Program.AddButton(this, "RETAKE ALL", 1050, 551, 190, delegate {
                if (pictures.Count > 0 && MessageBox.Show("Delete the captured face pictures and capture them again?", "Retake Faces",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                ClearPictures(); replaceFaces.Checked = true;
                if (camera == null) StartCamera(null, null);
            });
            retake.Height = button4.Height; retake.BackColor = button4.BackColor; retake.Font = button4.Font;
            button5.Click += StartCamera; button4.Click += CaptureFace;
            button1.Click += SaveClick;
            button2.Click += delegate { textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); ClearPictures(); };
            button3.Click += delegate { Close(); };
            cameraTimer.Tick += ReadCamera;
            // A new student needs the camera, so start it as soon as the window is visible.
            Shown += delegate { if (studentId == 0) StartCamera(null, null); };
            replaceFaces.Click += delegate
            {
                try
                {
                    if (replaceFaces.Checked) { if (camera == null) StartCamera(null, null); }
                    else ShowSavedPictures();   // keep the saved pictures, so show them again
                }
                catch (Exception ex) { SetStatus(Database.ErrorMessage(ex), Color.Firebrick); }
            };
            FormClosed += delegate { StopCamera(); ClearPictures(); ReplaceImage(pictureBox1, null); cameraTimer.Dispose(); };
            if (id > 0) LoadStudent();
        }

        private void LoadStudent()
        {
            DataRow row = Database.GetData("SELECT * FROM students WHERE student_id=@id", new Dictionary<string, object> { { "@id", studentId } }).Rows[0];
            textBox2.Text = row["student_number"].ToString(); textBox1.Text = row["first_name"].ToString();
            textBox3.Text = row["middle_name"].ToString(); textBox4.Text = row["last_name"].ToString();
            textBox5.Text = row["email"].ToString(); textBox6.Text = row["contact_number"].ToString();
            comboBox1.SelectedValue = row["department_id"]; comboBox2.Text = row["year_level"].ToString();
            comboBox3.Text = row["gender"].ToString(); status.Text = row["status"].ToString();
            ShowSavedPictures();
        }

        // Shows the 5 pictures that are already saved in the database for this student.
        private void ShowSavedPictures()
        {
            DataTable saved = Database.GetData("SELECT image_data FROM face_images WHERE student_id=@id ORDER BY image_number",
                new Dictionary<string, object> { { "@id", studentId } });
            ClearPictures();
            replaceFaces.Checked = saved.Rows.Count != 5;
            try
            {
                for (int i = 0; i < saved.Rows.Count && i < 5; i++)
                    using (MemoryStream stream = new MemoryStream((byte[])saved.Rows[i]["image_data"]))
                    using (Bitmap image = new Bitmap(stream))
                        ReplaceImage(thumbnails[i], new Bitmap(image));
                SetStatus(saved.Rows.Count + " saved pictures. Check re-register to replace them with 5 new captures.", Color.MidnightBlue);
            }
            catch (ArgumentException ex) { DamagedPictures(ex); }
            catch (OutOfMemoryException ex) { DamagedPictures(ex); }
        }

        private void DamagedPictures(Exception error)
        {
            System.Diagnostics.Trace.WriteLine(error);
            ClearPictures(); replaceFaces.Checked = true; replaceFaces.Enabled = false;
            SetStatus("A saved picture is damaged. Click START CAMERA and capture five new pictures to repair this student's registration.", Color.Firebrick);
        }

        private void StartCamera(object sender, EventArgs e)
        {
            try
            {
                StopCamera();
                SetStatus("Starting the webcam, please wait...", Color.MidnightBlue);
                captureStatus.Refresh(); Cursor = Cursors.WaitCursor;
                detector = CreateDetector(); camera = OpenCamera(); frame = new Mat();
                failedFrames = 0; holdMessageUntil = DateTime.MinValue; cameraTimer.Start();
            }
            catch (Exception ex) { StopCamera(); SetStatus(Database.ErrorMessage(ex), Color.Firebrick); }
            finally { Cursor = Cursors.Default; }
        }

        // Runs about 20 times per second: shows the camera and tells the student if the face is good.
        private void ReadCamera(object sender, EventArgs e)
        {
            try
            {
                if (!camera.Read(frame) || frame.Empty())
                {
                    if (++failedFrames >= 10) throw new InvalidOperationException("Webcam disconnected. Reconnect it and press Start Camera.");
                    return;
                }
                failedFrames = 0;
                Rect[] faces = Detect(detector, frame);
                string problem = CheckFace(frame, faces);
                // Green box = good face, orange box = something must be fixed.
                ShowFrame(pictureBox1, frame, faces, problem == "" ? Scalar.LimeGreen : Scalar.Orange);
                if (DateTime.Now < holdMessageUntil) return;   // keep the last capture message visible for a moment
                if (problem != "") SetStatus(problem, Color.Firebrick);
                else if (pictures.Count < 5) SetStatus("Good! Please " + PoseTips[pictures.Count] + ", then click CAPTURE.", Color.DarkGreen);
                else SetStatus("All 5 face pictures are captured. Click SAVE REGISTRATION.", Color.DarkGreen);
            }
            catch (Exception ex) { StopCamera(); SetStatus(Database.ErrorMessage(ex), Color.Firebrick); }
        }

        private void CaptureFace(object sender, EventArgs e)
        {
            try
            {
                if (!cameraTimer.Enabled || failedFrames > 0 || frame == null || frame.Empty())
                    throw new InvalidOperationException("The webcam is not running. Click START CAMERA first.");
                if (pictures.Count == 5) throw new InvalidOperationException("All 5 pictures are captured. Save or choose Retake All.");
                Rect[] faces = Detect(detector, frame);
                string problem = CheckFace(frame, faces);
                if (problem != "") throw new InvalidOperationException(problem + " The picture was not saved.");
                // Replacing saved pictures: remove the old thumbnails so old and new pictures are never mixed.
                if (pictures.Count == 0) foreach (PictureBox thumbnail in thumbnails) ReplaceImage(thumbnail, null);
                using (Mat crop = new Mat(frame, faces[0]))
                using (Mat saved = new Mat())
                {
                    Cv2.Resize(crop, saved, new OpenCvSharp.Size(160, 160));
                    byte[] bytes = saved.ImEncode(".png");
                    if (pictures.Any(p => p.SequenceEqual(bytes))) throw new InvalidOperationException("This is the same frame. Move slightly and capture a new picture.");
                    pictures.Add(bytes); ReplaceImage(thumbnails[pictures.Count - 1], ToBitmap(saved));
                }
                replaceFaces.Checked = true;
                HoldStatus("Face " + pictures.Count + " of 5 captured. " + (pictures.Count == 5 ? "You can save the student now." : "Next: " + PoseTips[pictures.Count] + "."), Color.DarkGreen);
            }
            catch (Exception ex) { HoldStatus(Database.ErrorMessage(ex), Color.Firebrick); }
        }

        private void SetStatus(string text, Color color)
        {
            captureStatus.Text = text; captureStatus.ForeColor = color;
        }

        // Shows a message that the live camera tips will not replace for 2 seconds.
        private void HoldStatus(string text, Color color)
        {
            SetStatus(text, color); holdMessageUntil = DateTime.Now.AddSeconds(2);
        }

        private void ClearPictures()
        {
            pictures.Clear();
            foreach (PictureBox thumbnail in thumbnails) if (thumbnail != null) ReplaceImage(thumbnail, null);
            holdMessageUntil = DateTime.MinValue;
            if (captureStatus != null) SetStatus("Face 0 of 5 captured. Look straight at the camera and capture face 1.", Color.MidnightBlue);
        }

        private void StopCamera()
        {
            cameraTimer.Stop();
            if (camera != null) { camera.Dispose(); camera = null; }
            if (detector != null) { detector.Dispose(); detector = null; }
            if (frame != null) { frame.Dispose(); frame = null; }
            ReplaceImage(pictureBox1, null);
        }

        private void SaveClick(object sender, EventArgs e)
        {
            try
            {
                SaveStudent(studentId, new Dictionary<string, object> {
                    { "@number", textBox2.Text.Trim() }, { "@first", textBox1.Text.Trim() }, { "@middle", Database.Optional(textBox3.Text) },
                    { "@last", textBox4.Text.Trim() }, { "@department", Program.SelectedId(comboBox1) }, { "@year", comboBox2.Text },
                    { "@gender", comboBox3.Text }, { "@email", Database.Optional(textBox5.Text) },
                    { "@phone", Database.Optional(textBox6.Text) }, { "@status", status.Text } }, replaceFaces.Checked ? pictures : null);
                MessageBox.Show("Student and five face pictures saved successfully.");
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { Program.Error(ex); }
        }

        public static int SaveStudent(int id, Dictionary<string, object> values, List<byte[]> newPictures)
        {
            Database.Require("Student Records");
            Database.CheckText(Convert.ToString(values["@number"]), "Student number", 20);
            Database.CheckText(Convert.ToString(values["@first"]), "First name", 50);
            Database.CheckText(Convert.ToString(values["@last"]), "Last name", 50);
            Database.CheckText(Convert.ToString(values["@middle"]), "Middle name", 50, false);
            Database.CheckContact(Convert.ToString(values["@email"]), Convert.ToString(values["@phone"]));
            if (Convert.ToInt32(values["@department"]) <= 0 || string.IsNullOrEmpty(Convert.ToString(values["@year"])) ||
                string.IsNullOrEmpty(Convert.ToString(values["@gender"]))) throw new ArgumentException("Choose a department, year and gender.");
            if ((id == 0 && newPictures == null) || (newPictures != null && newPictures.Count != 5))
                throw new ArgumentException("Registration requires exactly 5 valid face pictures.");
            if (newPictures != null)
                foreach (byte[] bytes in newPictures)
                {
                    if (bytes == null || bytes.Length == 0) throw new ArgumentException("A face picture is empty. Retake all five.");
                    try
                    {
                        using (MemoryStream stream = new MemoryStream(bytes))
                        using (Bitmap image = new Bitmap(stream))
                            if (image.Width != 160 || image.Height != 160)
                                throw new ArgumentException("Invalid face picture size.");
                    }
                    catch (ArgumentException) { throw new ArgumentException("A face picture is damaged. Retake all five."); }
                }
            Dictionary<string, object> p = new Dictionary<string, object>(values) { { "@id", id } };
            using (MySqlConnection connection = Database.GetConnection())
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    if (id > 0)
                    {
                        using (MySqlCommand cmd = Database.Command(connection, "SELECT student_id FROM students WHERE student_id=@id FOR UPDATE", p, transaction))
                            if (cmd.ExecuteScalar() == null) throw new InvalidOperationException("The student no longer exists.");
                    }
                    string sql = id == 0
                        ? @"INSERT INTO students(student_number,first_name,middle_name,last_name,department_id,year_level,gender,email,contact_number,status,face_registered)
                            VALUES(@number,@first,@middle,@last,@department,@year,@gender,@email,@phone,@status,0)"
                        : @"UPDATE students SET student_number=@number,first_name=@first,middle_name=@middle,last_name=@last,department_id=@department,
                            year_level=@year,gender=@gender,email=@email,contact_number=@phone,status=@status WHERE student_id=@id";
                    using (MySqlCommand cmd = Database.Command(connection, sql, p, transaction))
                    {
                        cmd.ExecuteNonQuery(); if (id == 0) id = Convert.ToInt32(cmd.LastInsertedId);
                    }
                    p["@id"] = id;
                    if (newPictures != null)
                    {
                        using (MySqlCommand cmd = Database.Command(connection, "DELETE FROM face_images WHERE student_id=@id", p, transaction)) cmd.ExecuteNonQuery();
                        for (int i = 0; i < 5; i++)
                            using (MySqlCommand cmd = Database.Command(connection,
                                "INSERT INTO face_images(student_id,image_number,image_data) VALUES(@id,@index,@image)",
                                new Dictionary<string, object> { { "@id", id }, { "@index", i + 1 }, { "@image", newPictures[i] } }, transaction)) cmd.ExecuteNonQuery();
                    }
                    using (MySqlCommand cmd = Database.Command(connection, "SELECT COUNT(*) FROM face_images WHERE student_id=@id", p, transaction))
                        if (Convert.ToInt32(cmd.ExecuteScalar()) != 5) throw new InvalidOperationException("This student needs five face pictures. Re-register the face.");
                    using (MySqlCommand cmd = Database.Command(connection, "UPDATE students SET face_registered=1 WHERE student_id=@id", p, transaction)) cmd.ExecuteNonQuery();
                    // Keep event rosters current: upcoming events follow the student's latest details,
                    // and events that have not ended yet accept newly registered or re-activated students.
                    using (MySqlCommand cmd = Database.Command(connection, @"DELETE r FROM event_students r WHERE r.student_id=@id
                        AND NOT EXISTS (SELECT 1 FROM event_sessions es WHERE es.event_id=r.event_id AND es.starts_at<=NOW())", p, transaction)) cmd.ExecuteNonQuery();
                    using (MySqlCommand cmd = Database.Command(connection, @"INSERT INTO event_students(event_id,student_id,department_id,year_level)
                        SELECT e.event_id,s.student_id,s.department_id,s.year_level FROM attendance_events e JOIN students s ON s.student_id=@id
                        WHERE s.status='Active' AND (e.department_id IS NULL OR e.department_id=s.department_id)
                        AND EXISTS (SELECT 1 FROM event_sessions es WHERE es.event_id=e.event_id AND es.ends_at>NOW())
                        AND NOT EXISTS (SELECT 1 FROM event_students x WHERE x.event_id=e.event_id AND x.student_id=s.student_id)", p, transaction)) cmd.ExecuteNonQuery();
                    transaction.Commit(); return id;
                }
            }
        }

        // Both forms use the same preprocessing so saved and live faces are comparable.
        internal static CascadeClassifier CreateDetector()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_default.xml");
            if (!File.Exists(path)) throw new FileNotFoundException("Missing face detector file. Rebuild and copy the detector XML beside the application.");
            // Check native dependencies before allocating objects with native finalizers.
            Cv2.GetVersionString();
            CascadeClassifier classifier = new CascadeClassifier(path);
            if (classifier.Empty()) { classifier.Dispose(); throw new InvalidDataException("The face detector file is damaged."); }
            return classifier;
        }

        internal static VideoCapture OpenCamera()
        {
            // Try the built-in webcam first (0), then USB webcams (1 and 2).
            for (int index = 0; index < 3; index++)
            {
                VideoCapture capture = new VideoCapture(index, VideoCaptureAPIs.DSHOW);
                if (capture.IsOpened())
                {
                    capture.Set(VideoCaptureProperties.FrameWidth, 640); capture.Set(VideoCaptureProperties.FrameHeight, 480);
                    // Some webcams need a moment before the first real picture arrives.
                    using (Mat test = new Mat())
                        for (int attempt = 0; attempt < 30; attempt++)
                        {
                            if (capture.Read(test) && !test.Empty()) return capture;
                            System.Threading.Thread.Sleep(50);
                        }
                }
                capture.Dispose();
            }
            throw new InvalidOperationException("Webcam unavailable. Connect it, allow camera access in Windows Settings > Privacy > Camera, and close Zoom/Teams/Camera.");
        }

        internal static Rect[] Detect(CascadeClassifier classifier, Mat image)
        {
            Rect[] found;
            using (Mat gray = new Mat())
            {
                Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY); Cv2.EqualizeHist(gray, gray);
                found = classifier.DetectMultiScale(gray, 1.1, 5, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(60, 60));
            }
            // The detector sometimes finds tiny "faces" in the background or on clothes.
            // A real second person has a face of similar size, so faces under 60% of the biggest one are ignored.
            int biggest = found.Length == 0 ? 0 : found.Max(face => face.Width);
            return found.Where(face => face.Width >= biggest * 0.6).ToArray();
        }

        // Returns "" when the picture has exactly one clear face, otherwise a message telling the user what to fix.
        internal static string CheckFace(Mat frame, Rect[] faces)
        {
            if (faces.Length == 0) return "No face detected. Please look at the camera.";
            if (faces.Length > 1) return "Multiple faces detected. Only one person may be in front of the camera.";
            Rect face = faces[0];
            if (face.Width < 90) return "Your face is too small. Please move closer to the camera.";
            if (face.X < 3 || face.Y < 3 || face.Right > frame.Width - 3 || face.Bottom > frame.Height - 3)
                return "Your face is too close to the edge. Please move to the center of the camera.";
            using (Mat crop = new Mat(frame, face))
            using (Mat gray = new Mat())
            using (Mat edges = new Mat())
            {
                Cv2.CvtColor(crop, gray, ColorConversionCodes.BGR2GRAY);
                double light = Cv2.Mean(gray).Val0;
                if (light < 60) return "The picture is too dark. Please turn on more light.";
                if (light > 200) return "The picture is too bright. Avoid strong light behind or on your face.";
                // Sharpness = how much detail the face has. A blurry face has very little.
                Cv2.Resize(gray, gray, new OpenCvSharp.Size(100, 100));
                Cv2.Laplacian(gray, edges, MatType.CV_64F);
                Scalar mean, deviation; Cv2.MeanStdDev(edges, out mean, out deviation);
                if (deviation.Val0 * deviation.Val0 < 15) return "The picture is blurry. Please hold still and look at the camera.";
            }
            return "";
        }

        // Shows the camera picture like a mirror, with a colored box around every detected face.
        internal static void ShowFrame(PictureBox box, Mat frame, Rect[] faces, Scalar color)
        {
            using (Mat display = frame.Clone())
            {
                foreach (Rect face in faces) Cv2.Rectangle(display, face, color, 3);
                Cv2.Flip(display, display, FlipMode.Y);
                ReplaceImage(box, ToBitmap(display));
            }
        }

        internal static Mat PrepareFace(Mat color)
        {
            Mat gray = new Mat();
            Cv2.CvtColor(color, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Resize(gray, gray, new OpenCvSharp.Size(100, 100)); Cv2.EqualizeHist(gray, gray);
            return gray;
        }

        internal static Bitmap ToBitmap(Mat image)
        {
            if (image.Empty()) throw new InvalidDataException("A saved face picture is damaged. Re-register it.");
            using (MemoryStream stream = new MemoryStream(image.ImEncode(".bmp")))
            using (Bitmap bitmap = new Bitmap(stream)) return new Bitmap(bitmap);
        }

        internal static void ReplaceImage(PictureBox box, Image image)
        {
            Image old = box.Image; box.Image = image; if (old != null) old.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
