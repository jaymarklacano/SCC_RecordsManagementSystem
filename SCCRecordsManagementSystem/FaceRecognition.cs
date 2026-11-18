using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // The main attendance screen. The webcam looks for a face, the face is recognized,
    // and the attendance of the student is recorded automatically.
    public partial class FaceRecognition : Form
    {
        // A student must be recognized in this many camera pictures in a row before attendance is recorded.
        // This avoids recording the wrong student because of one bad picture.
        private const int FramesNeeded = 3;

        // After this many pictures in a row without a match, the face is called "unknown".
        private const int UnknownFramesNeeded = 5;

        // The student card stays on the screen until no face was seen for this many seconds.
        private const int ClearCardAfterSeconds = 3;

        private static readonly Color WaitingBack = Color.FromArgb(189, 216, 233);
        private static readonly Color SuccessBack = Color.FromArgb(34, 120, 70);
        private static readonly Color InfoBack = Color.FromArgb(73, 118, 159);
        private static readonly Color ErrorBack = Color.FromArgb(139, 0, 0);

        private CameraHelper camera = new CameraHelper();
        private FaceService faceService;

        private bool isBusy = false;                 // stops the camera timer from running twice at the same time
        private int candidateStudentId = 0;          // the student we think we see
        private int candidateFrames = 0;             // how many pictures in a row show that student
        private int unknownFrames = 0;               // how many pictures in a row did not match anybody
        private int shownStudentId = 0;              // the student whose card is on the screen (0 = none)
        private bool unknownShown = false;           // true while the "unknown face" message is shown
        private DateTime lastFaceSeen = DateTime.MinValue;
        private ComboBox cmbSession;
        private ComboBox cmbAction;
        private bool loadingSessionChoices;

        public FaceRecognition()
        {
            InitializeComponent();
            NavigationHelper.ExitWhenClosed(this);
            AddAttendanceSelection();
        }

        private void FaceRecognition_Load(object sender, EventArgs e)
        {
            ClearCard();
            bool sessionsLoaded = false;
            try
            {
                LoadActiveSessions();
                sessionsLoaded = true;
            }
            catch (Exception ex)
            {
                ShowCameraStatus(Database.GetFriendlyMessage(ex), Color.Firebrick);
            }
            if (sessionsLoaded)
            {
                ShowCameraStatus("Choose the active event session and Log In or Log Out.", Color.MidnightBlue);
            }
        }

        // The camera starts after the window is visible, so the window does not freeze while opening.
        private void FaceRecognition_Shown(object sender, EventArgs e)
        {
            StartRecognition();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            StartRecognition();
        }

        private void btnNextStudent_Click(object sender, EventArgs e)
        {
            ClearCard();
            ShowCameraStatus("Ready. Waiting for the next student...", Color.MidnightBlue);
        }

        // ---------- Starting and stopping ----------

        // (Re)loads the registered faces from the database and starts the webcam.
        private void StartRecognition()
        {
            StopCamera();
            ClearCard();
            btnRestart.Enabled = false;
            btnNextStudent.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                if (SelectedSessionId() <= 0)
                {
                    ShowCameraStatus("No event session is currently open. Create or wait for an active session.", Color.Firebrick);
                    ShowBanner("NO ACTIVE SESSION", ErrorBack, Color.White);
                    return;
                }

                ShowCameraStatus("Loading the registered faces, please wait...", Color.MidnightBlue);
                Application.DoEvents();   // lets the message above appear before the slow work starts

                if (faceService == null)
                {
                    faceService = new FaceService();
                }
                faceService.LoadRegisteredFaces();
                lblFaceStats.Text = faceService.TrainedStudentCount + " student(s) with registered faces (" +
                    faceService.TrainedImageCount + " face pictures loaded)";

                ShowCameraStatus("Starting the webcam, please wait...", Color.MidnightBlue);
                Application.DoEvents();

                string problem = camera.Start();
                if (problem != "")
                {
                    ShowCameraStatus(problem, Color.Firebrick);
                    ShowBanner("CAMERA PROBLEM", ErrorBack, Color.White);
                    return;
                }

                timerCamera.Start();
                if (!faceService.IsTrained)
                {
                    ShowCameraStatus("No student has a registered face yet. Please register a student first.", Color.Firebrick);
                }
                else
                {
                    ShowCameraStatus("Camera is on. Waiting for a student...", Color.MidnightBlue);
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                ShowCameraStatus(ex.Message, Color.Firebrick);
            }
            catch (System.IO.InvalidDataException ex)
            {
                ShowCameraStatus(ex.Message, Color.Firebrick);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                ShowCameraStatus(Database.GetFriendlyMessage(ex), Color.Firebrick);
                ShowBanner("DATABASE PROBLEM", ErrorBack, Color.White);
            }
            catch (Exception)
            {
                ShowCameraStatus("Face recognition could not be started. Please install the Microsoft Visual C++ " +
                    "Redistributable (x64) and check the troubleshooting section of README.md.", Color.Firebrick);
                ShowBanner("RECOGNITION PROBLEM", ErrorBack, Color.White);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnRestart.Enabled = true;
                btnNextStudent.Enabled = true;
            }
        }

        private void StopCamera()
        {
            timerCamera.Stop();
            camera.Stop();
            ShowCameraImage(null);
        }

        // Always turn the webcam off when the window closes.
        private void FaceRecognition_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerCamera.Stop();
            camera.Dispose();

            if (faceService != null)
            {
                faceService.Dispose();
                faceService = null;
            }
        }

        // ---------- The recognition loop ----------

        // Runs about 10 times per second while the webcam is on.
        // Only the status labels are used here (no message boxes), so the loop never gets stuck.
        private void timerCamera_Tick(object sender, EventArgs e)
        {
            if (isBusy)
            {
                return;
            }
            isBusy = true;

            try
            {
                Bitmap frame = camera.ReadFrame();
                if (frame == null)
                {
                    if (camera.IsDisconnected)
                    {
                        StopCamera();
                        ShowCameraStatus("The webcam was disconnected. Plug it in again and click RESTART CAMERA.", Color.Firebrick);
                        ShowBanner("WEBCAM DISCONNECTED", ErrorBack, Color.White);
                    }
                    return;
                }

                using (frame)
                {
                    Rectangle[] faces = faceService.DetectFaces(frame);
                    Color boxColor = Color.Gray;

                    if (faces.Length == 0)
                    {
                        ShowCameraStatus("No face detected. Please stand in front of the camera.", Color.DimGray);
                        ResetCandidate();
                        ClearCardIfNobodyIsHere();
                    }
                    else if (faces.Length > 1)
                    {
                        lastFaceSeen = DateTime.Now;
                        boxColor = Color.Orange;
                        ShowCameraStatus("Multiple faces detected. Only one student can stand in front of the camera.", Color.Firebrick);
                        ResetCandidate();
                    }
                    else
                    {
                        lastFaceSeen = DateTime.Now;
                        boxColor = HandleOneFace(frame, faces[0]);
                    }

                    ShowCameraImage(DrawFaceBoxes(frame, faces, boxColor));
                }
            }
            catch (Exception)
            {
                StopCamera();
                ShowCameraStatus("Face recognition stopped because of a problem. Click RESTART CAMERA to try again.", Color.Firebrick);
                ShowBanner("RECOGNITION PROBLEM", ErrorBack, Color.White);
            }
            finally
            {
                isBusy = false;
            }
        }

        // Checks the quality of the face, recognizes it and records attendance.
        // Returns the color of the rectangle drawn around the face.
        private Color HandleOneFace(Bitmap frame, Rectangle face)
        {
            string quality = faceService.CheckFaceQuality(frame, face);
            if (quality != "")
            {
                ShowCameraStatus(quality, Color.Firebrick);
                ResetCandidate();
                return Color.Orange;
            }

            if (!faceService.IsTrained)
            {
                ShowCameraStatus("No student has a registered face yet. Please register a student first.", Color.Firebrick);
                return Color.Orange;
            }

            FaceMatch match = faceService.Recognize(frame, face);

            if (!match.IsMatch)
            {
                // Not recognized. Wait a few pictures before calling it "unknown".
                candidateStudentId = 0;
                candidateFrames = 0;
                unknownFrames++;

                if (unknownFrames < UnknownFramesNeeded)
                {
                    ShowCameraStatus("Recognizing your face...", Color.MidnightBlue);
                    return Color.Gold;
                }

                ShowUnknownFace();
                return Color.Red;
            }

            // Recognized. The same student must be seen in several pictures in a row.
            unknownFrames = 0;
            if (match.StudentId == candidateStudentId)
            {
                candidateFrames++;
            }
            else
            {
                candidateStudentId = match.StudentId;
                candidateFrames = 1;
            }

            if (candidateFrames < FramesNeeded)
            {
                ShowCameraStatus("Recognizing your face...", Color.MidnightBlue);
                return Color.Gold;
            }

            // This student's card is already on the screen: do nothing (no repeated records).
            if (shownStudentId != match.StudentId)
            {
                ShowRecognizedStudent(match.StudentId);
            }
            return Color.LimeGreen;
        }

        // ---------- Recording attendance ----------

        private void ShowRecognizedStudent(int studentId)
        {
            // Whatever happens below, this student is "handled", so we do not try again while he or she stays here.
            shownStudentId = studentId;
            unknownShown = false;

            try
            {
                Student student = StudentService.GetById(studentId);
                if (student == null)
                {
                    ShowCameraStatus("This student was removed from the system. Click RESTART CAMERA to reload the faces.", Color.Firebrick);
                    ShowBanner("STUDENT NOT FOUND", ErrorBack, Color.White);
                    return;
                }

                ShowStudentInfo(student);
                byte[] photo = StudentService.GetPhoto(studentId);
                ShowStudentPhoto(photo);

                if (student.Status != "Active")
                {
                    lblStatus.Text = student.Status;
                    lblTime.Text = DateTime.Now.ToString("h:mm tt");
                    ShowBanner("STUDENT RECOGNIZED", InfoBack, Color.White);
                    ShowResultMessage("This student is " + student.Status + ".\nAttendance was not recorded.", Color.Firebrick);
                    ShowCameraStatus(student.FullName + " was recognized, but is not an active student.", Color.Firebrick);
                    return;
                }

                EventAttendanceResult result;
                string action = cmbAction.SelectedItem == null ? "Log In" : cmbAction.SelectedItem.ToString();
                try
                {
                    result = AttendanceService.RecordSessionAttendance(studentId, SelectedSessionId(), action);
                }
                catch (InvalidOperationException ex)
                {
                    lblStatus.Text = "Not recorded";
                    lblTime.Text = "-";
                    ShowBanner("ATTENDANCE NOT RECORDED", ErrorBack, Color.White);
                    ShowResultMessage(ex.Message, Color.Firebrick);
                    ShowCameraStatus(ex.Message, Color.Firebrick);
                    return;
                }

                lblStatus.Text = action == "Log In" ? "Present" : "Logged Out";
                lblTime.Text = action == "Log In" ? result.TimeInText : result.TimeOutText;
                if (result.AlreadyRecorded)
                {
                    ShowBanner("STUDENT RECOGNIZED", InfoBack, Color.White);
                    if (result.AlreadyLoggedOut)
                    {
                        ShowResultMessage("Already logged out at " + result.TimeOutText + ".", Color.MidnightBlue);
                        ShowCameraStatus(student.FullName + " already logged out for this session.", Color.MidnightBlue);
                    }
                    else if (action == "Log In")
                    {
                        string alreadyLoggedIn = "Already logged in at " + result.TimeInText + ".";
                        if (result.RequiresLogout && !result.TimeOut.HasValue)
                        {
                            alreadyLoggedIn += "\nThis event requires a log out when leaving.";
                        }
                        ShowResultMessage(alreadyLoggedIn, Color.MidnightBlue);
                        ShowCameraStatus(student.FullName + " was already logged in for this session.", Color.MidnightBlue);
                    }
                    else
                    {
                        ShowResultMessage("Already logged in at " + result.TimeInText + ".", Color.MidnightBlue);
                        ShowCameraStatus("This student is already logged in for the selected session.", Color.MidnightBlue);
                    }
                }
                else
                {
                    ShowBanner("STUDENT RECOGNIZED", SuccessBack, Color.White);
                    string successMessage = action + " successfully recorded.";
                    if (action == "Log In" && result.RequiresLogout)
                    {
                        successMessage += "\nThis event requires a log out when leaving.";
                    }
                    ShowResultMessage(successMessage, Color.DarkGreen);
                    ShowCameraStatus(action + " recorded for " + student.FullName + ".", Color.DarkGreen);
                }
            }
            catch (Exception ex)
            {
                ShowBanner("DATABASE PROBLEM", ErrorBack, Color.White);
                ShowResultMessage("Attendance could not be saved.\nPlease step away and try again.", Color.Firebrick);
                ShowCameraStatus(Database.GetFriendlyMessage(ex), Color.Firebrick);
            }
        }

        private void ShowUnknownFace()
        {
            if (unknownShown)
            {
                return;
            }

            unknownShown = true;
            shownStudentId = 0;
            ClearStudentInfo();
            ShowBanner("UNKNOWN FACE", ErrorBack, Color.White);
            ShowResultMessage("This face is not registered.\nPlease register the student first.", Color.Firebrick);
            ShowCameraStatus("Unknown face. This person is not registered in the system.", Color.Firebrick);
        }

        // ---------- Screen helpers ----------

        private void ResetCandidate()
        {
            candidateStudentId = 0;
            candidateFrames = 0;
            unknownFrames = 0;
        }

        // Removes the student card when nobody has been in front of the camera for a few seconds.
        private void ClearCardIfNobodyIsHere()
        {
            bool cardIsShown = (shownStudentId != 0 || unknownShown);
            if (cardIsShown && (DateTime.Now - lastFaceSeen).TotalSeconds >= ClearCardAfterSeconds)
            {
                ClearCard();
            }
        }

        private void ClearCard()
        {
            shownStudentId = 0;
            unknownShown = false;
            ResetCandidate();
            ClearStudentInfo();
            ShowBanner("WAITING FOR A STUDENT", WaitingBack, Color.MidnightBlue);
            ShowResultMessage("", Color.MidnightBlue);
        }

        private void ClearStudentInfo()
        {
            ShowStudentPhoto(null);
            lblName.Text = "-";
            lblStudentNumber.Text = "-";
            lblCourse.Text = "-";
            lblYear.Text = "-";
            lblStatus.Text = "-";
            lblTime.Text = "-";
        }

        private void ShowStudentInfo(Student student)
        {
            lblName.Text = student.FullName;
            lblStudentNumber.Text = student.StudentNumber;
            lblCourse.Text = student.DepartmentCode;
            lblYear.Text = student.YearLevel;
        }

        private void ShowStudentPhoto(byte[] photo)
        {
            Image oldImage = picStudent.Image;
            picStudent.Image = (photo == null) ? null : ImageHelper.BytesToBitmap(photo);
            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }

        private void ShowBanner(string text, Color backColor, Color foreColor)
        {
            lblBanner.Text = text;
            lblBanner.ForeColor = foreColor;
            pnlResultBanner.BackColor = backColor;
        }

        private void ShowResultMessage(string text, Color color)
        {
            lblResultMessage.Text = text;
            lblResultMessage.ForeColor = color;
        }

        private void ShowCameraStatus(string text, Color color)
        {
            lblCameraStatus.Text = text;
            lblCameraStatus.ForeColor = color;
        }

        // Draws a colored rectangle around every face on a copy of the picture.
        private static Bitmap DrawFaceBoxes(Bitmap frame, Rectangle[] faces, Color color)
        {
            Bitmap preview = new Bitmap(frame);
            using (Graphics graphics = Graphics.FromImage(preview))
            using (Pen pen = new Pen(color, 3))
            {
                foreach (Rectangle face in faces)
                {
                    graphics.DrawRectangle(pen, face);
                }
            }
            return preview;
        }

        private void ShowCameraImage(Bitmap picture)
        {
            Image oldImage = picCamera.Image;
            picCamera.Image = picture;
            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }

        private void AddAttendanceSelection()
        {
        lblCameraStatus.Location = new Point(12, 42);
        lblCameraStatus.Size = new Size(437, 32);

        Label sessionLabel = new Label
            {
            AutoSize = true,
            Font = new Font("Cambria", 9F, FontStyle.Bold),
            Location = new Point(10, 10),
            Text = "Session:"
        };
        cmbSession = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Cambria", 9F),
            Location = new Point(70, 7),
            Size = new Size(250, 25)
        };
        Label actionLabel = new Label
        {
            AutoSize = true,
            Font = new Font("Cambria", 9F, FontStyle.Bold),
            Location = new Point(325, 10),
            Text = "Action:"
        };
        cmbAction = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Cambria", 9F),
            Location = new Point(380, 7),
            Size = new Size(72, 25)
        };
        cmbAction.Items.AddRange(new object[] { "Log In", "Log Out" });
        cmbAction.SelectedIndex = 0;
        cmbAction.SelectedIndexChanged += AttendanceSelectionChanged;
        cmbSession.SelectedIndexChanged += AttendanceSelectionChanged;
        pnlCameraStatus.Controls.Add(sessionLabel);
        pnlCameraStatus.Controls.Add(cmbSession);
        pnlCameraStatus.Controls.Add(actionLabel);
        pnlCameraStatus.Controls.Add(cmbAction);
        }

        private void LoadActiveSessions()
        {
        loadingSessionChoices = true;
        try
        {
            DataTable sessions = EventService.GetActiveSessionChoices();
            DataRow empty = sessions.NewRow();
            empty["session_id"] = 0;
            empty["display_name"] = sessions.Rows.Count == 0 ? "No active event session" : "Select active session";
            sessions.Rows.InsertAt(empty, 0);
            cmbSession.DataSource = sessions;
            cmbSession.ValueMember = "session_id";
            cmbSession.DisplayMember = "display_name";
            cmbSession.SelectedIndex = 0;
        }
        finally
        {
            loadingSessionChoices = false;
        }
        }

        private int SelectedSessionId()
        {
        if (cmbSession == null || cmbSession.SelectedValue == null || cmbSession.SelectedValue is DataRowView)
        {
            return 0;
        }
        return Convert.ToInt32(cmbSession.SelectedValue);
        }

        private void AttendanceSelectionChanged(object sender, EventArgs e)
        {
        if (loadingSessionChoices || !IsHandleCreated)
        {
            return;
        }
        if (sender == cmbSession)
        {
            StartRecognition();
        }
        else
        {
            ClearCard();
            ShowCameraStatus("Action changed to " + cmbAction.Text + ".", Color.MidnightBlue);
        }
        }
    }
}
