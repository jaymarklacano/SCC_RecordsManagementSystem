using MySql.Data.MySqlClient;
using OpenCvSharp;
using OpenCvSharp.Face;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public partial class FaceRecognition : Form
    {
        // Webcam, face detector and face recognizer objects from OpenCvSharp.
        private VideoCapture camera = null;
        private CascadeClassifier faceDetector = null;
        private LBPHFaceRecognizer faceRecognizer = null;
        private Mat cameraFrame = null;
        private int failedFrameCount = 0;

        // A face is accepted as a student when the LBPH "distance" is 70 or lower.
        // A smaller distance means the faces are more alike (0 = exactly the same picture).
        private const double matchThreshold = 70;

        // The same student must be matched in 3 camera pictures in a row before attendance is recorded.
        private const int matchesNeeded = 3;

        // After 5 camera pictures in a row without a match, the face is called "unknown".
        private const int missesNeeded = 5;

        // Recognition progress
        private int candidateStudentId = 0;
        private int sameMatchCount = 0;
        private int unknownCount = 0;
        private bool unknownMessageShown = false;

        // The student whose card is shown now. He/she will not be recorded again and again.
        private int lastShownStudentId = 0;

        // The last time a face was seen (the card is cleared 3 seconds after the student leaves).
        private DateTime lastFaceSeenTime = DateTime.MinValue;

        // When saving fails (example: database is off), we wait 5 seconds before trying again.
        private DateTime nextAttemptTime = DateTime.MinValue;

        // true while the session ComboBox is being filled (so its event does nothing).
        private bool isLoadingSessions = false;

        public FaceRecognition()
        {
            InitializeComponent();
        }

        private void FaceRecognition_Load(object sender, EventArgs e)
        {
            if (cmbInOut.Items.Count > 0)
            {
                cmbInOut.SelectedIndex = 0;
            }

            clearStudentCard();
            lblFaceDataInfo.Text = "";
            lblSessionCount.Text = "";
        }

        // The Shown event runs after the form is visible, so the user sees the form while the camera starts.
        private void FaceRecognition_Shown(object sender, EventArgs e)
        {
            startRecognition();
        }

        // ------------------------------------------------------------------
        // Start / stop
        // ------------------------------------------------------------------

        // Loads the open sessions and the registered faces, then starts the webcam.
        private void startRecognition()
        {
            stopCamera();
            resetRecognition();

            setCameraStatus("Loading the open sessions and the registered faces, please wait...", Color.MidnightBlue);
            lblCameraStatus.Refresh();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 1. Sessions that are open right now
                bool hasSession = loadOpenSessions();

                if (hasSession == false)
                {
                    return;
                }

                // 2. This line checks that the OpenCV files (OpenCvSharpExtern.dll) were copied next to the program.
                Cv2.GetVersionString();

                // 3. Face detector file
                string detectorPath = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");

                if (File.Exists(detectorPath) == false)
                {
                    setCameraStatus("The face detector file is missing. Rebuild the project.", Color.Firebrick);
                    showMessage("The file \"haarcascade_frontalface_default.xml\" was not found.\nRebuild the project in Visual Studio.", Color.Firebrick);
                    return;
                }

                faceDetector = new CascadeClassifier(detectorPath);

                if (faceDetector.Empty() == true)
                {
                    stopCamera();
                    setCameraStatus("The face detector file is damaged.", Color.Firebrick);
                    return;
                }

                // 4. Teach the recognizer the faces of the registered students
                bool hasFaces = trainRecognizer();

                if (hasFaces == false)
                {
                    stopCamera();
                    return;
                }

                // 5. Open the webcam
                setCameraStatus("Starting the webcam, please wait...", Color.MidnightBlue);
                lblCameraStatus.Refresh();

                camera = openWebcam();

                if (camera == null)
                {
                    stopCamera();
                    setCameraStatus("Webcam unavailable. Check the camera, then click RESTART / RELOAD.", Color.Firebrick);
                    showMessage("The webcam could not be opened.\n" +
                        "Connect a webcam, allow camera access in Windows Settings > Privacy > Camera,\n" +
                        "and close other programs that use the camera.", Color.Firebrick);
                    return;
                }

                cameraFrame = new Mat();
                failedFrameCount = 0;

                // The timer reads a picture from the webcam 10 times per second.
                timerScan.Start();
                setCameraStatus("Camera is on. Waiting for a student... (one person at a time)", Color.DarkGreen);
                showMessage("Waiting for a student.", Color.MidnightBlue);
            }
            catch (DllNotFoundException)
            {
                stopCamera();
                setCameraStatus("OpenCV files are missing. See the README (NuGet packages).", Color.Firebrick);
                showMessage("Restore the NuGet packages (OpenCvSharp4.runtime.win) and rebuild the project.", Color.Firebrick);
            }
            catch (TypeInitializationException)
            {
                stopCamera();
                setCameraStatus("OpenCV files are missing. See the README (NuGet packages).", Color.Firebrick);
                showMessage("Restore the NuGet packages (OpenCvSharp4.runtime.win) and rebuild the project.", Color.Firebrick);
            }
            catch (Exception ex)
            {
                stopCamera();
                setCameraStatus("Face recognition could not start.", Color.Firebrick);
                showMessage(Database.friendlyMessage(ex), Color.Firebrick);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // Fills the session ComboBox with the sessions that are open NOW.
        // Returns false when there is no open session.
        private bool loadOpenSessions()
        {
            // Remember the selected session so it stays selected after RESTART / RELOAD.
            int previousSessionId = Database.getSelectedId(cmbSession);

            string query = "SELECT es.session_id, " +
                           "CONCAT(e.event_name, ' - ', DATE_FORMAT(es.session_date, '%b %e'), ' ', es.session_name, " +
                           "' (', DATE_FORMAT(es.starts_at, '%l:%i %p'), ' - ', DATE_FORMAT(es.ends_at, '%l:%i %p'), ')') AS session_display " +
                           "FROM event_sessions es " +
                           "JOIN events e ON e.event_id = es.event_id " +
                           "WHERE NOW() >= es.starts_at AND NOW() < es.ends_at " +
                           "ORDER BY es.starts_at, e.event_name";

            DataTable sessionTable = new DataTable();

            MySqlConnection conn = Database.GetConnection();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(sessionTable);

                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                setCameraStatus("Cannot load the sessions.", Color.Firebrick);
                showMessage(Database.friendlyMessage(ex), Color.Firebrick);
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            isLoadingSessions = true;
            cmbSession.DisplayMember = "session_display";
            cmbSession.ValueMember = "session_id";
            cmbSession.DataSource = sessionTable;

            // Select the same session as before (when it is still open).
            for (int i = 0; i < sessionTable.Rows.Count; i++)
            {
                if (Convert.ToInt32(sessionTable.Rows[i]["session_id"]) == previousSessionId)
                {
                    cmbSession.SelectedIndex = i;
                }
            }

            isLoadingSessions = false;

            if (sessionTable.Rows.Count == 0)
            {
                lblSessionCount.Text = "";
                setCameraStatus("No open session right now. Attendance cannot be recorded.", Color.Firebrick);
                showMessage("NO OPEN SESSION\nCreate an event in Event Management (or wait until its session starts),\nthen click RESTART / RELOAD.", Color.Firebrick);
                return false;
            }

            updateSessionCount();
            return true;
        }

        // Loads the 5 face pictures of every registered student and trains the LBPH recognizer.
        // Returns false when there are no registered faces.
        private bool trainRecognizer()
        {
            // Only ACTIVE students that have exactly 5 face pictures are used.
            string query = "SELECT s.student_id, f.image_data " +
                           "FROM students s " +
                           "JOIN face_images f ON f.student_id = s.student_id " +
                           "WHERE s.status = 'Active' AND s.face_registered = 1 " +
                           "AND (SELECT COUNT(*) FROM face_images x WHERE x.student_id = s.student_id) = 5 " +
                           "ORDER BY s.student_id, f.image_number";

            DataTable faceTable = new DataTable();

            MySqlConnection conn = Database.GetConnection();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(faceTable);

                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                setCameraStatus("Cannot load the registered faces.", Color.Firebrick);
                showMessage(Database.friendlyMessage(ex), Color.Firebrick);
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            List<Mat> trainingFaces = new List<Mat>();
            List<int> trainingLabels = new List<int>();
            List<int> studentIds = new List<int>();
            int damagedPictures = 0;

            try
            {
                for (int i = 0; i < faceTable.Rows.Count; i++)
                {
                    int studentId = Convert.ToInt32(faceTable.Rows[i]["student_id"]);
                    byte[] imageBytes = (byte[])faceTable.Rows[i]["image_data"];

                    Mat facePicture = Cv2.ImDecode(imageBytes, ImreadModes.Color);

                    // A damaged picture is skipped so that one bad record does not stop the attendance of everybody.
                    if (facePicture.Empty() == true)
                    {
                        facePicture.Dispose();
                        damagedPictures = damagedPictures + 1;
                        continue;
                    }

                    trainingFaces.Add(prepareFaceForRecognition(facePicture));
                    facePicture.Dispose();

                    // The "label" of each picture is the student_id, so the recognizer answers with the student_id.
                    trainingLabels.Add(studentId);

                    if (studentIds.Contains(studentId) == false)
                    {
                        studentIds.Add(studentId);
                    }
                }

                if (trainingFaces.Count == 0)
                {
                    setCameraStatus("No registered faces yet.", Color.Firebrick);
                    showMessage("NO REGISTERED FACES\nAdd students with 5 face pictures in Student Records first,\nthen click RESTART / RELOAD.", Color.Firebrick);
                    lblFaceDataInfo.Text = "0 students with registered faces.";
                    lblFaceDataInfo.ForeColor = Color.Firebrick;
                    return false;
                }

                faceRecognizer = LBPHFaceRecognizer.Create();
                faceRecognizer.Train(trainingFaces, trainingLabels);
            }
            finally
            {
                for (int i = 0; i < trainingFaces.Count; i++)
                {
                    trainingFaces[i].Dispose();
                }
            }

            lblFaceDataInfo.Text = studentIds.Count + " student(s) with registered faces (" + trainingLabels.Count + " face pictures loaded).";
            lblFaceDataInfo.ForeColor = Color.DimGray;

            if (damagedPictures > 0)
            {
                lblFaceDataInfo.Text = lblFaceDataInfo.Text + " " + damagedPictures + " damaged picture(s) skipped - re-register those students.";
                lblFaceDataInfo.ForeColor = Color.Firebrick;
            }

            return true;
        }

        // Tries the built-in webcam first (0), then USB webcams (1 and 2).
        // Returns null when no webcam works.
        private VideoCapture openWebcam()
        {
            for (int cameraIndex = 0; cameraIndex < 3; cameraIndex++)
            {
                VideoCapture capture = new VideoCapture(cameraIndex, VideoCaptureAPIs.DSHOW);

                if (capture.IsOpened() == true)
                {
                    capture.Set(VideoCaptureProperties.FrameWidth, 640);
                    capture.Set(VideoCaptureProperties.FrameHeight, 480);

                    // Some webcams need a moment before the first real picture arrives.
                    using (Mat testFrame = new Mat())
                    {
                        for (int attempt = 0; attempt < 30; attempt++)
                        {
                            if (capture.Read(testFrame) == true && testFrame.Empty() == false)
                            {
                                return capture;
                            }

                            System.Threading.Thread.Sleep(50);
                        }
                    }
                }

                capture.Release();
                capture.Dispose();
            }

            return null;
        }

        private void stopCamera()
        {
            timerScan.Stop();

            if (camera != null)
            {
                camera.Release();
                camera.Dispose();
                camera = null;
            }

            if (faceDetector != null)
            {
                faceDetector.Dispose();
                faceDetector = null;
            }

            if (faceRecognizer != null)
            {
                faceRecognizer.Dispose();
                faceRecognizer = null;
            }

            if (cameraFrame != null)
            {
                cameraFrame.Dispose();
                cameraFrame = null;
            }

            showImage(picCameraPreview, null);
        }

        // ------------------------------------------------------------------
        // Scanning (runs 10 times per second)
        // ------------------------------------------------------------------

        private void timerScan_Tick(object sender, EventArgs e)
        {
            if (camera == null || cameraFrame == null)
            {
                return;
            }

            try
            {
                bool frameWasRead = camera.Read(cameraFrame);

                if (frameWasRead == false || cameraFrame.Empty() == true)
                {
                    failedFrameCount = failedFrameCount + 1;

                    // 10 bad pictures in a row = the webcam was unplugged.
                    if (failedFrameCount >= 10)
                    {
                        stopCamera();
                        setCameraStatus("Webcam disconnected. Reconnect it and click RESTART / RELOAD.", Color.Firebrick);
                        showMessage("WEBCAM DISCONNECTED\nReconnect the webcam, then click RESTART / RELOAD.", Color.Firebrick);
                    }

                    return;
                }

                failedFrameCount = 0;

                Rect[] faces = detectFaces(cameraFrame);
                Scalar boxColor = recognizeFace(faces);
                showCameraFrame(cameraFrame, faces, boxColor);
            }
            catch (Exception ex)
            {
                stopCamera();
                setCameraStatus("Recognition error. Click RESTART / RELOAD.", Color.Firebrick);
                showMessage("Recognition failed: " + ex.Message + "\nClick RESTART / RELOAD to try again.", Color.Firebrick);
            }
        }

        // Decides what to do with the faces of the current camera picture.
        // Returns the color of the box around the face:
        //   Gray = no face, Orange = problem, Gold = checking, Red = unknown, Green = recognized
        private Scalar recognizeFace(Rect[] faces)
        {
            if (faces.Length > 0)
            {
                lastFaceSeenTime = DateTime.Now;
            }

            string problem = checkFaceQuality(cameraFrame, faces);

            if (problem != "")
            {
                candidateStudentId = 0;
                sameMatchCount = 0;
                unknownCount = 0;

                if (faces.Length == 0)
                {
                    setCameraStatus(problem, Color.DimGray);

                    // The card stays on the screen until nobody is in front of the camera for 3 seconds.
                    TimeSpan timeWithoutFace = DateTime.Now - lastFaceSeenTime;

                    if ((lastShownStudentId != 0 || unknownMessageShown == true) && timeWithoutFace.TotalSeconds > 3)
                    {
                        resetRecognition();
                    }

                    return Scalar.Gray;
                }

                setCameraStatus(problem, Color.Firebrick);
                return Scalar.Orange;
            }

            // Ask the recognizer who this is
            int studentId;
            double distance;

            using (Mat faceCrop = new Mat(cameraFrame, faces[0]))
            {
                using (Mat preparedFace = prepareFaceForRecognition(faceCrop))
                {
                    faceRecognizer.Predict(preparedFace, out studentId, out distance);
                }
            }

            if (studentId <= 0 || distance > matchThreshold)
            {
                // One unclear picture is normal, so we wait for 5 in a row before saying "unknown".
                candidateStudentId = 0;
                sameMatchCount = 0;
                unknownCount = unknownCount + 1;

                if (unknownCount < missesNeeded)
                {
                    setCameraStatus("Recognizing your face...", Color.MidnightBlue);
                    return Scalar.Gold;
                }

                if (unknownMessageShown == false)
                {
                    resetRecognition();
                    unknownMessageShown = true;
                    unknownCount = missesNeeded;
                    lblCardTitle.Text = "Unknown Face";
                    lblCardTitle.ForeColor = Color.Firebrick;
                    showMessage("UNKNOWN FACE\nThis face is not registered. No attendance was recorded.", Color.Firebrick);
                }

                setCameraStatus("Unknown face. This person is not registered or the face is not clear.", Color.Firebrick);
                return Scalar.Red;
            }

            unknownCount = 0;

            if (studentId == candidateStudentId)
            {
                sameMatchCount = sameMatchCount + 1;
            }
            else
            {
                candidateStudentId = studentId;
                sameMatchCount = 1;
            }

            if (sameMatchCount < matchesNeeded)
            {
                setCameraStatus("Recognizing your face... " + sameMatchCount + "/" + matchesNeeded, Color.MidnightBlue);
                return Scalar.Gold;
            }

            // This student's card is already shown, so we do NOT record the attendance again.
            if (studentId == lastShownStudentId)
            {
                setCameraStatus("Done. Please step away for the next student.", Color.DarkGreen);
                return Scalar.LimeGreen;
            }

            // The last attempt failed (example: database is off). Wait a little before trying again.
            if (DateTime.Now < nextAttemptTime)
            {
                return Scalar.Gold;
            }

            showRecognizedStudent(studentId, faces[0]);
            return Scalar.LimeGreen;
        }

        // ------------------------------------------------------------------
        // Student card and attendance
        // ------------------------------------------------------------------

        // Shows the information of the recognized student, then records the attendance.
        private void showRecognizedStudent(int studentId, Rect face)
        {
            string query = "SELECT s.student_number, s.first_name, s.middle_name, s.last_name, s.year_level, d.department_code, " +
                           "(SELECT f.image_data FROM face_images f WHERE f.student_id = s.student_id ORDER BY f.image_number LIMIT 1) AS photo " +
                           "FROM students s " +
                           "JOIN departments d ON d.department_id = s.department_id " +
                           "WHERE s.student_id = @id";

            DataTable studentTable = new DataTable();

            MySqlConnection conn = Database.GetConnection();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", studentId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(studentTable);

                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                // Try again after 5 seconds.
                nextAttemptTime = DateTime.Now.AddSeconds(5);
                setCameraStatus("Database problem. Trying again in 5 seconds...", Color.Firebrick);
                showMessage(Database.friendlyMessage(ex), Color.Firebrick);
                return;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            if (studentTable.Rows.Count == 0)
            {
                resetRecognition();
                nextAttemptTime = DateTime.Now.AddSeconds(5);
                showMessage("This student was deleted.\nClick RESTART / RELOAD to refresh the list of faces.", Color.Firebrick);
                return;
            }

            DataRow student = studentTable.Rows[0];

            string fullName = student["first_name"].ToString();

            if (student["middle_name"].ToString() != "")
            {
                fullName = fullName + " " + student["middle_name"].ToString();
            }

            fullName = fullName + " " + student["last_name"].ToString();

            unknownMessageShown = false;
            lblCardTitle.Text = "Student Recognized";
            lblCardTitle.ForeColor = Color.DarkGreen;
            lblFullName.Text = fullName;
            lblIDNumber.Text = student["student_number"].ToString();
            lblDepartment.Text = student["department_code"].ToString();
            lblYearLevel.Text = student["year_level"].ToString();

            // Show the registered photo, so the person in charge can compare it with the real face.
            Bitmap photo = null;

            if (student["photo"] != DBNull.Value)
            {
                photo = bytesToBitmap((byte[])student["photo"]);
            }

            if (photo == null)
            {
                using (Mat faceCrop = new Mat(cameraFrame, face))
                {
                    Cv2.Flip(faceCrop, faceCrop, FlipMode.Y);
                    photo = matToBitmap(faceCrop);
                }
            }

            showImage(picStudentFace, photo);

            // The card stays for this student until he/she leaves (no repeated recording).
            lastShownStudentId = studentId;
            recordAttendance(studentId);
        }

        // Saves the Log In or Log Out of the student for the selected session.
        // Everything is done inside one transaction so two computers can not save the same attendance twice.
        private void recordAttendance(int studentId)
        {
            int sessionId = Database.getSelectedId(cmbSession);
            string action = cmbInOut.Text;

            if (sessionId == 0)
            {
                showAttendanceResult("Not recorded", "Please select a session first.", Color.Firebrick);
                return;
            }

            MySqlConnection conn = Database.GetConnection();
            MySqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();

                // 1. Is the session still open?
                int eventId = 0;
                bool requiresLogout = false;
                bool sessionIsOpen = false;
                DateTime sessionEnds = DateTime.MinValue;
                DateTime serverTime = DateTime.Now;

                MySqlCommand cmdSession = new MySqlCommand(
                    "SELECT es.event_id, e.requires_logout, es.ends_at, NOW() AS server_time, " +
                    "(NOW() >= es.starts_at AND NOW() < es.ends_at) AS is_open " +
                    "FROM event_sessions es JOIN events e ON e.event_id = es.event_id " +
                    "WHERE es.session_id = @session FOR UPDATE", conn, transaction);
                cmdSession.Parameters.AddWithValue("@session", sessionId);

                MySqlDataReader reader = cmdSession.ExecuteReader();

                if (reader.Read())
                {
                    eventId = Convert.ToInt32(reader["event_id"]);
                    requiresLogout = Convert.ToInt32(reader["requires_logout"]) == 1;
                    sessionIsOpen = Convert.ToInt32(reader["is_open"]) == 1;
                    sessionEnds = Convert.ToDateTime(reader["ends_at"]);
                    serverTime = Convert.ToDateTime(reader["server_time"]);
                }

                reader.Close();
                cmdSession.Dispose();

                if (eventId == 0 || sessionIsOpen == false)
                {
                    transaction.Rollback();
                    showAttendanceResult("Not recorded", "This session is already closed.\nClick RESTART / RELOAD to load the open sessions.", Color.Firebrick);
                    return;
                }

                // 2. Check the student, the five saved pictures, and the event roster
                // again immediately before attendance is saved.
                bool studentIsActive = false;
                bool studentIsFaceRegistered = false;
                int savedFaceCount = 0;
                int rosterCount = 0;

                MySqlCommand cmdStudent = new MySqlCommand(
                    "SELECT s.status, s.face_registered, " +
                    "(SELECT COUNT(*) FROM face_images f WHERE f.student_id = s.student_id) AS face_count, " +
                    "(SELECT COUNT(*) FROM event_students r WHERE r.event_id = @event AND r.student_id = s.student_id) AS roster_count " +
                    "FROM students s WHERE s.student_id = @student FOR UPDATE", conn, transaction);
                cmdStudent.Parameters.AddWithValue("@event", eventId);
                cmdStudent.Parameters.AddWithValue("@student", studentId);

                using (MySqlDataReader studentReader = cmdStudent.ExecuteReader())
                {
                    if (studentReader.Read() == true)
                    {
                        studentIsActive = studentReader["status"].ToString() == "Active";
                        studentIsFaceRegistered = Convert.ToInt32(studentReader["face_registered"]) == 1;
                        savedFaceCount = Convert.ToInt32(studentReader["face_count"]);
                        rosterCount = Convert.ToInt32(studentReader["roster_count"]);
                    }
                }

                cmdStudent.Dispose();

                if (studentIsActive == false)
                {
                    transaction.Rollback();
                    showAttendanceResult("Not recorded", "This student is inactive or no longer exists. Attendance was not recorded.", Color.Firebrick);
                    return;
                }

                if (studentIsFaceRegistered == false || savedFaceCount != 5)
                {
                    transaction.Rollback();
                    showAttendanceResult("Not recorded", "This student's face registration is incomplete. The student must be active, marked as face registered, and have exactly 5 saved face pictures. Please re-register the student's face.", Color.Firebrick);
                    return;
                }

                if (rosterCount == 0)
                {
                    transaction.Rollback();
                    showAttendanceResult("Not recorded", "This student is not included in this event (check the event's department and year level).", Color.Firebrick);
                    return;
                }

                // 3. Does the student already have attendance for this session?
                bool hasLogIn = false;
                DateTime timeIn = DateTime.MinValue;
                object timeOut = DBNull.Value;

                MySqlCommand cmdCheck = new MySqlCommand(
                    "SELECT time_in, time_out FROM event_attendance " +
                    "WHERE session_id = @session AND student_id = @student FOR UPDATE", conn, transaction);
                cmdCheck.Parameters.AddWithValue("@session", sessionId);
                cmdCheck.Parameters.AddWithValue("@student", studentId);

                MySqlDataReader checkReader = cmdCheck.ExecuteReader();

                if (checkReader.Read())
                {
                    hasLogIn = true;
                    timeIn = Convert.ToDateTime(checkReader["time_in"]);
                    timeOut = checkReader["time_out"];
                }

                checkReader.Close();
                cmdCheck.Dispose();

                if (action == "Log In")
                {
                    if (hasLogIn == true)
                    {
                        // Duplicate attendance is NOT saved.
                        transaction.Rollback();
                        showAttendanceResult("Present", "Attendance already recorded at " + timeIn.ToString("h:mm tt") + ".", Color.DarkOrange);
                        lblTime.Text = timeIn.ToString("h:mm tt");
                        return;
                    }

                    MySqlCommand cmdInsert = new MySqlCommand(
                        "INSERT INTO event_attendance (session_id, student_id, time_in, recorded_by) " +
                        "VALUES (@session, @student, @time, 'Face Recognition')", conn, transaction);
                    cmdInsert.Parameters.AddWithValue("@session", sessionId);
                    cmdInsert.Parameters.AddWithValue("@student", studentId);
                    cmdInsert.Parameters.AddWithValue("@time", serverTime);
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Dispose();

                    transaction.Commit();

                    string message = "Attendance Successfully Recorded\nLog In at " + serverTime.ToString("h:mm tt") + ".";

                    if (requiresLogout == true)
                    {
                        message = message + "\nPlease come back to LOG OUT before " + sessionEnds.ToString("h:mm tt") + ".";
                    }

                    lblTime.Text = serverTime.ToString("h:mm tt");
                    showAttendanceResult("Present", message, Color.DarkGreen);
                }
                else
                {
                    if (hasLogIn == false)
                    {
                        transaction.Rollback();
                        showAttendanceResult("Not recorded", "No Log In found for this session.\nPlease LOG IN first before logging out.", Color.Firebrick);
                        return;
                    }

                    if (timeOut != DBNull.Value)
                    {
                        transaction.Rollback();
                        DateTime savedTimeOut = Convert.ToDateTime(timeOut);
                        lblTime.Text = timeIn.ToString("h:mm tt") + " - " + savedTimeOut.ToString("h:mm tt");
                        showAttendanceResult("Logged Out", "Already logged out at " + savedTimeOut.ToString("h:mm tt") + ".", Color.DarkOrange);
                        return;
                    }

                    MySqlCommand cmdUpdate = new MySqlCommand(
                        "UPDATE event_attendance SET time_out = @time " +
                        "WHERE session_id = @session AND student_id = @student", conn, transaction);
                    cmdUpdate.Parameters.AddWithValue("@time", serverTime);
                    cmdUpdate.Parameters.AddWithValue("@session", sessionId);
                    cmdUpdate.Parameters.AddWithValue("@student", studentId);
                    cmdUpdate.ExecuteNonQuery();
                    cmdUpdate.Dispose();

                    transaction.Commit();

                    lblTime.Text = timeIn.ToString("h:mm tt") + " - " + serverTime.ToString("h:mm tt");
                    showAttendanceResult("Logged Out", "Log Out Successfully Recorded\nat " + serverTime.ToString("h:mm tt") + ".", Color.DarkGreen);
                }

                updateSessionCount();
                setCameraStatus("Student recognized. Ready for the next student.", Color.DarkGreen);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception)
                    {
                        // The connection is already broken, nothing more to undo.
                    }
                }

                // Show the card again after 5 seconds and try again.
                lastShownStudentId = 0;
                nextAttemptTime = DateTime.Now.AddSeconds(5);
                lblAttendanceStatus.Text = "Not recorded";
                lblAttendanceStatus.ForeColor = Color.Firebrick;
                showMessage("Attendance was NOT recorded.\n" + Database.friendlyMessage(ex), Color.Firebrick);
                setCameraStatus("Attendance was not recorded. Trying again in 5 seconds...", Color.Firebrick);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private void showAttendanceResult(string status, string message, Color color)
        {
            lblAttendanceStatus.Text = status;
            lblAttendanceStatus.ForeColor = color;
            showMessage(message, color);
        }

        // Shows "Present: 10 of 45" for the selected session.
        private void updateSessionCount()
        {
            int sessionId = Database.getSelectedId(cmbSession);

            if (sessionId == 0)
            {
                lblSessionCount.Text = "";
                return;
            }

            MySqlConnection conn = Database.GetConnection();

            try
            {
                conn.Open();

                MySqlCommand cmdPresent = new MySqlCommand("SELECT COUNT(*) FROM event_attendance WHERE session_id = @session", conn);
                cmdPresent.Parameters.AddWithValue("@session", sessionId);
                int presentCount = Convert.ToInt32(cmdPresent.ExecuteScalar());
                cmdPresent.Dispose();

                MySqlCommand cmdExpected = new MySqlCommand(
                    "SELECT COUNT(*) FROM event_students r " +
                    "JOIN event_sessions es ON es.event_id = r.event_id " +
                    "WHERE es.session_id = @session", conn);
                cmdExpected.Parameters.AddWithValue("@session", sessionId);
                int expectedCount = Convert.ToInt32(cmdExpected.ExecuteScalar());
                cmdExpected.Dispose();

                lblSessionCount.Text = "Present: " + presentCount + " of " + expectedCount;
            }
            catch (Exception)
            {
                // Only a small counter, so the error is ignored on purpose.
                lblSessionCount.Text = "";
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        // Clears the student card so the next student can be recognized.
        private void resetRecognition()
        {
            candidateStudentId = 0;
            sameMatchCount = 0;
            unknownCount = 0;
            unknownMessageShown = false;
            lastShownStudentId = 0;
            nextAttemptTime = DateTime.MinValue;
            clearStudentCard();
        }

        private void clearStudentCard()
        {
            lblCardTitle.Text = "Waiting for a student...";
            lblCardTitle.ForeColor = Color.MidnightBlue;
            lblFullName.Text = "-";
            lblIDNumber.Text = "-";
            lblDepartment.Text = "-";
            lblYearLevel.Text = "-";
            lblTime.Text = "-";
            lblAttendanceStatus.Text = "-";
            lblAttendanceStatus.ForeColor = Color.Black;
            showMessage("Waiting for a student.", Color.MidnightBlue);
            showImage(picStudentFace, null);
        }

        private void setCameraStatus(string message, Color color)
        {
            lblCameraStatus.Text = message;
            lblCameraStatus.ForeColor = color;
        }

        private void showMessage(string message, Color color)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = color;
        }

        // ------------------------------------------------------------------
        // Buttons and ComboBoxes
        // ------------------------------------------------------------------

        private void btnNextPerson_Click(object sender, EventArgs e)
        {
            resetRecognition();

            if (timerScan.Enabled == true)
            {
                setCameraStatus("Camera is on. Waiting for the next student...", Color.DarkGreen);
            }
        }

        private void btnRestartCamera_Click(object sender, EventArgs e)
        {
            startRecognition();
        }

        private void cmbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingSessions == true)
            {
                return;
            }

            resetRecognition();
            updateSessionCount();
        }

        private void cmbInOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetRecognition();

            if (cmbInOut.Text == "Log Out")
            {
                lblInOut.ForeColor = Color.Firebrick;
            }
            else
            {
                lblInOut.ForeColor = Color.Black;
            }
        }

        // ------------------------------------------------------------------
        // Face detection (the same code as in Registration.cs)
        // ------------------------------------------------------------------

        // Finds the faces in a picture. Returns a rectangle for every face.
        private Rect[] detectFaces(Mat picture)
        {
            Rect[] foundFaces;

            using (Mat gray = new Mat())
            {
                // The detector works with black and white pictures.
                Cv2.CvtColor(picture, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.EqualizeHist(gray, gray);
                foundFaces = faceDetector.DetectMultiScale(gray, 1.1, 5, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(60, 60));
            }

            // Faces smaller than 60% of the biggest face are ignored (they are usually not real faces).
            int biggestWidth = 0;

            for (int i = 0; i < foundFaces.Length; i++)
            {
                if (foundFaces[i].Width > biggestWidth)
                {
                    biggestWidth = foundFaces[i].Width;
                }
            }

            List<Rect> realFaces = new List<Rect>();

            for (int i = 0; i < foundFaces.Length; i++)
            {
                if (foundFaces[i].Width >= biggestWidth * 0.6)
                {
                    realFaces.Add(foundFaces[i]);
                }
            }

            return realFaces.ToArray();
        }

        // Returns "" when the picture has exactly ONE clear face.
        // Otherwise returns a message that tells the student what to fix.
        private string checkFaceQuality(Mat picture, Rect[] faces)
        {
            if (faces.Length == 0)
            {
                return "No face detected. Waiting for a student...";
            }

            if (faces.Length > 1)
            {
                return "Multiple faces detected. One student at a time, please.";
            }

            Rect face = faces[0];

            if (face.Width < 90)
            {
                return "Face is too small. Please move closer to the camera.";
            }

            if (face.X < 3 || face.Y < 3 || face.Right > picture.Width - 3 || face.Bottom > picture.Height - 3)
            {
                return "Face is too close to the edge. Please move to the center.";
            }

            using (Mat faceCrop = new Mat(picture, face))
            using (Mat gray = new Mat())
            using (Mat edges = new Mat())
            {
                Cv2.CvtColor(faceCrop, gray, ColorConversionCodes.BGR2GRAY);

                // Brightness: 0 = black, 255 = white
                double brightness = Cv2.Mean(gray).Val0;

                if (brightness < 60)
                {
                    return "The picture is too dark. Please turn on more light.";
                }

                if (brightness > 200)
                {
                    return "The picture is too bright. Avoid strong light on the face.";
                }

                // Sharpness = how much detail the face has. A blurry face has very little detail.
                Cv2.Resize(gray, gray, new OpenCvSharp.Size(100, 100));
                Cv2.Laplacian(gray, edges, MatType.CV_64F);

                Scalar mean;
                Scalar standardDeviation;
                Cv2.MeanStdDev(edges, out mean, out standardDeviation);

                double sharpness = standardDeviation.Val0 * standardDeviation.Val0;

                if (sharpness < 15)
                {
                    return "The picture is blurry. Please hold still.";
                }
            }

            return "";
        }

        // The same preparation as Registration.cs: gray, 100 x 100 and equalized light.
        private Mat prepareFaceForRecognition(Mat colorFace)
        {
            Mat grayFace = new Mat();
            Cv2.CvtColor(colorFace, grayFace, ColorConversionCodes.BGR2GRAY);
            Cv2.Resize(grayFace, grayFace, new OpenCvSharp.Size(100, 100));
            Cv2.EqualizeHist(grayFace, grayFace);
            return grayFace;
        }

        // Shows the webcam picture like a mirror, with a colored box around every face.
        private void showCameraFrame(Mat picture, Rect[] faces, Scalar boxColor)
        {
            using (Mat display = picture.Clone())
            {
                for (int i = 0; i < faces.Length; i++)
                {
                    Cv2.Rectangle(display, faces[i], boxColor, 3);
                }

                Cv2.Flip(display, display, FlipMode.Y);
                showImage(picCameraPreview, matToBitmap(display));
            }
        }

        // ------------------------------------------------------------------
        // Picture helpers
        // ------------------------------------------------------------------

        // Shows a picture in a PictureBox and frees the memory of the old picture.
        private void showImage(PictureBox pictureBox, Image newImage)
        {
            Image oldImage = pictureBox.Image;
            pictureBox.Image = newImage;

            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }

        // Changes an OpenCV picture (Mat) into a normal .NET picture (Bitmap).
        private Bitmap matToBitmap(Mat picture)
        {
            byte[] bmpBytes = picture.ImEncode(".bmp");
            return bytesToBitmap(bmpBytes);
        }

        // Changes picture bytes (PNG/BMP) into a Bitmap. Returns null when the bytes are not a picture.
        private Bitmap bytesToBitmap(byte[] imageBytes)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    using (Bitmap temporaryBitmap = new Bitmap(stream))
                    {
                        // A copy is made so the stream can be closed.
                        return new Bitmap(temporaryBitmap);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        // ------------------------------------------------------------------
        // Closing and menu
        // ------------------------------------------------------------------

        private void FaceRecognition_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopCamera();
            showImage(picStudentFace, null);
            Database.exitIfNoFormIsOpen(this);
        }

        private void btnFaceLogout_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                Database.saveLogoutTime();

                lnkCreateAccount login = new lnkCreateAccount();
                login.Show();
                this.Close();
            }
        }

        private void btnFaceDash_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnFaceDept_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Departments") == false)
            {
                return;
            }

            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnFaceStudent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        private void btnFaceOfficer_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Officer Records") == false)
            {
                return;
            }

            OfficerRecords officerRecords = new OfficerRecords();
            officerRecords.Show();
            this.Close();
        }

        private void btnFace_Click(object sender, EventArgs e)
        {
            // We are already in Face Recognition, so we only reload the sessions and faces.
            startRecognition();
        }

        private void btnFaceEvent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Event Management") == false)
            {
                return;
            }

            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnFaceAttendance_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Attendance Monitoring") == false)
            {
                return;
            }

            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnFaceReports_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Reports and Analysis") == false)
            {
                return;
            }

            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        private void btnFaceObli_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Obligations") == false)
            {
                return;
            }

            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        private void btnFaceRules_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Rules and Permission") == false)
            {
                return;
            }

            RulesPermission rulesPermission = new RulesPermission();
            rulesPermission.Show();
            this.Close();
        }
    }
}
