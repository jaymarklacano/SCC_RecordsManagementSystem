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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public partial class Registration : Form
    {
        // 0 = we are adding a new student. Any other number = the id of the student we are editing.
        private int studentIdToEdit = 0;

        // true when the student being edited already has 5 saved face pictures.
        private bool hasSavedFaces = false;

        // true after the student was saved (so we do not ask "discard changes?" when closing).
        private bool isSaved = false;

        // Webcam and face detector objects from OpenCvSharp.
        private VideoCapture camera = null;
        private CascadeClassifier faceDetector = null;
        private Mat cameraFrame = null;
        private int failedFrameCount = 0;
        private bool hasFreshCameraFrame = false;

        // A capture message stays on the screen until this time (so the live tips do not hide it right away).
        private DateTime keepMessageUntil = DateTime.MinValue;

        // The new face pictures (PNG bytes, 160 x 160) captured with the webcam.
        private List<byte[]> capturedFaces = new List<byte[]>();

        // The 5 small picture boxes and the 5 hint labels (filled in the Load event).
        private PictureBox[] facePictureBoxes = new PictureBox[5];
        private Label[] hintLabels = new Label[5];

        // What the student should do for picture 1, 2, 3, 4 and 5.
        private string[] poseTips = new string[]
        {
            "look straight at the camera",
            "turn your head slightly to the left",
            "turn your head slightly to the right",
            "tilt your head slightly up",
            "look normal or smile"
        };

        // Add a new student
        public Registration()
        {
            InitializeComponent();
        }

        // Edit an existing student
        public Registration(int studentId)
        {
            InitializeComponent();
            studentIdToEdit = studentId;
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            try
            {
                loadRegistrationForm();
            }
            catch (MySqlException ex)
            {
                setStatus("Registration unavailable. Return to Student Records and reopen this form.", Color.Firebrick);
                Database.showError("Could not load registration details. Saving is disabled.", ex);
            }
        }

        private void loadRegistrationForm()
        {
            facePictureBoxes[0] = picFace1;
            facePictureBoxes[1] = picFace2;
            facePictureBoxes[2] = picFace3;
            facePictureBoxes[3] = picFace4;
            facePictureBoxes[4] = picFace5;

            hintLabels[0] = lblHint1;
            hintLabels[1] = lblHint2;
            hintLabels[2] = lblHint3;
            hintLabels[3] = lblHint4;
            hintLabels[4] = lblHint5;

            Database.loadDepartmentComboBox(cmbDepartment, "-- Select Course --");

            if (studentIdToEdit == 0)
            {
                prepareAddMode();
            }
            else
            {
                bool studentFound = loadStudentToEdit();

                if (studentFound == false)
                {
                    MessageBox.Show("The student was not found. It may have been deleted.", "Student Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isSaved = true;
                    this.Close();
                    return;
                }
            }

            updateProgress();
            updateCaptureButtons();
            btnSave.Enabled = true;
            txtNumber.Focus();
        }

        private void prepareAddMode()
        {
            lblFormTitle.Text = "Register New Student";
            btnSave.Text = "SAVE STUDENT";

            txtNumber.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtContact.Clear();

            if (cmbDepartment.Items.Count > 0)
            {
                cmbDepartment.SelectedIndex = 0;
            }

            cmbYear.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
            cmbStatus.SelectedIndex = 0;

            // A new student ALWAYS needs 5 face pictures, so the user cannot uncheck this.
            chkReRegister.Text = "Register face images (5 pictures are required)";
            chkReRegister.Checked = true;
            chkReRegister.Enabled = false;

            lblNote.Text = "Face registration is required. Start the camera and capture 5 face pictures before saving.";
            lblNote.ForeColor = Color.DimGray;

            hasSavedFaces = false;
            capturedFaces.Clear();
            clearFacePictureBoxes();
            setStatus("Click START CAMERA to begin the face registration.", Color.MidnightBlue);
        }

        // Fills the form with the student that will be edited. Returns false when the student was not found.
        private bool loadStudentToEdit()
        {
            StudentInfo student = Database.searchAndGetStudentInfo(studentIdToEdit);

            if (student == null)
            {
                return false;
            }

            lblFormTitle.Text = "Edit Student Information";
            btnSave.Text = "UPDATE STUDENT";

            txtNumber.Text = student.StudentNumber;
            txtFirstName.Text = student.FirstName;
            txtMiddleName.Text = student.MiddleName;
            txtLastName.Text = student.LastName;
            txtEmail.Text = student.Email;
            txtContact.Text = student.ContactNumber;

            cmbDepartment.SelectedValue = student.DepartmentId;
            cmbYear.SelectedItem = student.YearLevel;
            cmbGender.SelectedItem = student.Gender;
            cmbStatus.SelectedItem = student.Status;

            capturedFaces.Clear();
            int savedFaceCount = loadSavedFacePictures();

            if (student.FaceRegistered == true && savedFaceCount == 5)
            {
                hasSavedFaces = true;

                chkReRegister.Text = "Re-register face images (capture 5 new pictures)";
                chkReRegister.Checked = false;
                chkReRegister.Enabled = true;

                lblNote.Text = "The 5 saved face pictures will be kept. Check the box above if you want to capture 5 new pictures.";
                lblNote.ForeColor = Color.DimGray;
                setStatus("These are the saved face pictures of the student.", Color.MidnightBlue);
            }
            else
            {
                // The student does not have a complete face registration, so 5 new pictures are required.
                hasSavedFaces = false;
                clearFacePictureBoxes();

                chkReRegister.Text = "Register face images (5 pictures are required)";
                chkReRegister.Checked = true;
                chkReRegister.Enabled = false;

                lblNote.Text = "This student has no complete face registration. Please capture 5 face pictures.";
                lblNote.ForeColor = Color.Firebrick;
                setStatus("Click START CAMERA to begin the face registration.", Color.MidnightBlue);
            }

            return true;
        }

        // Shows the saved face pictures of the student. Returns how many pictures were found.
        private int loadSavedFacePictures()
        {
            clearFacePictureBoxes();
            int pictureCount = 0;

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string query = "SELECT image_number, image_data FROM face_images WHERE student_id = @id ORDER BY image_number";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", studentIdToEdit);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int imageNumber = Convert.ToInt32(reader["image_number"]);
                            byte[] imageBytes = (byte[])reader["image_data"];

                            if (imageNumber >= 1 && imageNumber <= 5)
                            {
                                Bitmap picture = bytesToBitmap(imageBytes);

                                if (picture != null)
                                {
                                    showImage(facePictureBoxes[imageNumber - 1], picture);
                                    pictureCount = pictureCount + 1;
                                }
                            }
                        }
                    }
                }
            }

            return pictureCount;
        }

        private void chkReRegister_CheckedChanged(object sender, EventArgs e)
        {
            // This only matters in edit mode (in add mode the CheckBox is always checked and disabled).
            if (chkReRegister.Enabled == false)
            {
                return;
            }

            if (chkReRegister.Checked == true)
            {
                capturedFaces.Clear();
                clearFacePictureBoxes();
                lblNote.Text = "The old face pictures will be REPLACED by 5 new pictures when you save.";
                lblNote.ForeColor = Color.Firebrick;
                setStatus("Click START CAMERA to capture 5 new face pictures.", Color.MidnightBlue);
            }
            else
            {
                stopCamera();
                capturedFaces.Clear();
                loadSavedFacePictures();
                lblNote.Text = "The 5 saved face pictures will be kept. Check the box above if you want to capture 5 new pictures.";
                lblNote.ForeColor = Color.DimGray;
                setStatus("These are the saved face pictures of the student.", Color.MidnightBlue);
            }

            updateProgress();
            updateCaptureButtons();
        }

        // ------------------------------------------------------------------
        // Buttons, progress and status messages
        // ------------------------------------------------------------------

        private void updateCaptureButtons()
        {
            bool cameraIsRunning = timerCamera.Enabled;

            btnStartCamera.Enabled = chkReRegister.Checked;

            if (cameraIsRunning == true)
            {
                btnStartCamera.Text = "STOP CAMERA";
            }
            else
            {
                btnStartCamera.Text = "START CAMERA";
            }

            if (chkReRegister.Checked == true && cameraIsRunning == true && capturedFaces.Count < 5)
            {
                btnCapture.Enabled = true;
            }
            else
            {
                btnCapture.Enabled = false;
            }

            if (chkReRegister.Checked == true && capturedFaces.Count > 0)
            {
                btnRetake.Enabled = true;
            }
            else
            {
                btnRetake.Enabled = false;
            }
        }

        private void updateProgress()
        {
            int faceCount = 0;

            if (chkReRegister.Checked == true)
            {
                faceCount = capturedFaces.Count;
                lblProgress.Text = "Face " + faceCount + " of 5 captured";
            }
            else if (hasSavedFaces == true)
            {
                faceCount = 5;
                lblProgress.Text = "5 of 5 saved face pictures";
            }
            else
            {
                lblProgress.Text = "Face 0 of 5 captured";
            }

            progressFaces.Value = faceCount;

            if (faceCount == 5)
            {
                lblProgress.ForeColor = Color.DarkGreen;
            }
            else
            {
                lblProgress.ForeColor = Color.Black;
            }

            // Green = done, Blue = the next picture to capture, Black = not yet.
            for (int i = 0; i < 5; i++)
            {
                if (i < faceCount)
                {
                    hintLabels[i].ForeColor = Color.DarkGreen;
                }
                else if (i == faceCount && chkReRegister.Checked == true)
                {
                    hintLabels[i].ForeColor = Color.Blue;
                }
                else
                {
                    hintLabels[i].ForeColor = Color.Black;
                }
            }
        }

        private void setStatus(string message, Color color)
        {
            lblCaptureStatus.Text = message;
            lblCaptureStatus.ForeColor = color;
        }

        // Shows a message that the live camera tips will not replace for 2 seconds.
        private void holdStatus(string message, Color color)
        {
            setStatus(message, color);
            keepMessageUntil = DateTime.Now.AddSeconds(2);
        }

        // ------------------------------------------------------------------
        // Webcam
        // ------------------------------------------------------------------

        private void btnStartCamera_Click(object sender, EventArgs e)
        {
            // The same button is used to start and to stop the camera.
            if (timerCamera.Enabled == true)
            {
                stopCamera();
                setStatus("Camera stopped. Click START CAMERA to continue.", Color.MidnightBlue);
                updateCaptureButtons();
                return;
            }

            if (capturedFaces.Count >= 5)
            {
                MessageBox.Show("All 5 face pictures are already captured.\nClick RETAKE ALL FACES if you want to capture them again.",
                    "Face Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            startCamera();
            updateCaptureButtons();
        }

        private void startCamera()
        {
            stopCamera();
            setStatus("Starting the webcam, please wait...", Color.MidnightBlue);
            lblCaptureStatus.Refresh();
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // This line checks that the OpenCV files (OpenCvSharpExtern.dll) were copied next to the program.
                Cv2.GetVersionString();

                // 1. Load the face detector file (it is copied to the bin folder when the project is built).
                string detectorPath = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");

                if (File.Exists(detectorPath) == false)
                {
                    setStatus("The face detector file is missing. Rebuild the project.", Color.Firebrick);
                    MessageBox.Show("The file \"haarcascade_frontalface_default.xml\" was not found in:\n" + Application.StartupPath +
                        "\n\nRebuild the project in Visual Studio so the file is copied again.", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                faceDetector = new CascadeClassifier(detectorPath);

                if (faceDetector.Empty() == true)
                {
                    stopCamera();
                    setStatus("The face detector file is damaged.", Color.Firebrick);
                    return;
                }

                // 2. Open the webcam
                camera = openWebcam();

                if (camera == null)
                {
                    stopCamera();
                    setStatus("Webcam unavailable. Check the camera and try again.", Color.Firebrick);
                    MessageBox.Show("The webcam could not be opened.\n\n" +
                        "- Make sure a webcam is connected.\n" +
                        "- Allow camera access: Windows Settings > Privacy > Camera.\n" +
                        "- Close other programs that use the camera (Zoom, Teams, Camera app).",
                        "Webcam Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cameraFrame = new Mat();
                failedFrameCount = 0;
                hasFreshCameraFrame = false;
                keepMessageUntil = DateTime.MinValue;

                // The timer reads a new picture from the webcam 20 times per second.
                timerCamera.Start();
                setStatus("Camera started. Please " + poseTips[capturedFaces.Count] + ".", Color.DarkGreen);
            }
            catch (DllNotFoundException)
            {
                stopCamera();
                setStatus("OpenCV files are missing. See the README (NuGet packages).", Color.Firebrick);
                MessageBox.Show("The OpenCV files were not found.\nRestore the NuGet packages (OpenCvSharp4.runtime.win) and rebuild the project.",
                    "Missing OpenCV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TypeInitializationException)
            {
                stopCamera();
                setStatus("OpenCV files are missing. See the README (NuGet packages).", Color.Firebrick);
                MessageBox.Show("The OpenCV files were not found.\nRestore the NuGet packages (OpenCvSharp4.runtime.win) and rebuild the project.",
                    "Missing OpenCV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                stopCamera();
                setStatus("Could not start the webcam.", Color.Firebrick);
                Database.showError("Could not start the webcam. Check camera access in Windows and close other camera apps.", ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // Tries the built-in webcam first (0), then USB webcams (1 and 2).
        // Returns null when no webcam works.
        private VideoCapture openWebcam()
        {
            for (int cameraIndex = 0; cameraIndex < 3; cameraIndex++)
            {
                VideoCapture capture = new VideoCapture(cameraIndex, VideoCaptureAPIs.DSHOW);
                bool ready = false;
                try
                {
                    if (!capture.IsOpened()) continue;
                    capture.Set(VideoCaptureProperties.FrameWidth, 640);
                    capture.Set(VideoCaptureProperties.FrameHeight, 480);

                    // Some webcams need a moment before the first real picture arrives.
                    using (Mat testFrame = new Mat())
                    {
                        for (int attempt = 0; attempt < 30; attempt++)
                        {
                            if (capture.Read(testFrame) == true && testFrame.Empty() == false)
                            {
                                ready = true;
                                return capture;
                            }

                            System.Threading.Thread.Sleep(50);
                        }
                    }
                }

                finally
                {
                    if (!ready) capture.Dispose();
                }
            }

            return null;
        }

        private void stopCamera()
        {
            timerCamera.Stop();
            hasFreshCameraFrame = false;

            if (camera != null)
            {
                camera.Dispose();
                camera = null;
            }

            if (faceDetector != null)
            {
                faceDetector.Dispose();
                faceDetector = null;
            }

            if (cameraFrame != null)
            {
                cameraFrame.Dispose();
                cameraFrame = null;
            }

            showImage(picCameraPreview, null);
        }

        // Runs 20 times per second while the camera is on.
        private void timerCamera_Tick(object sender, EventArgs e)
        {
            if (camera == null || cameraFrame == null)
            {
                hasFreshCameraFrame = false;
                return;
            }

            try
            {
                // A failed read can leave the previous picture in cameraFrame.
                // Do not allow the Capture button to save that old picture.
                hasFreshCameraFrame = false;
                bool frameWasRead = camera.Read(cameraFrame);

                if (frameWasRead == false || cameraFrame.Empty() == true)
                {
                    failedFrameCount = failedFrameCount + 1;

                    // 10 bad pictures in a row = the webcam was unplugged.
                    if (failedFrameCount >= 10)
                    {
                        stopCamera();
                        updateCaptureButtons();
                        setStatus("Webcam disconnected. Reconnect it and click START CAMERA.", Color.Firebrick);
                    }

                    return;
                }

                failedFrameCount = 0;
                hasFreshCameraFrame = true;

                Rect[] faces = detectFaces(cameraFrame);
                string problem = checkFaceQuality(cameraFrame, faces);

                // Green box = good face, orange box = something must be fixed.
                if (problem == "")
                {
                    showCameraFrame(cameraFrame, faces, Scalar.LimeGreen);
                }
                else
                {
                    showCameraFrame(cameraFrame, faces, Scalar.Orange);
                }

                // Keep the last capture message visible for a moment.
                if (DateTime.Now < keepMessageUntil)
                {
                    return;
                }

                if (problem != "")
                {
                    setStatus(problem, Color.Firebrick);
                }
                else if (capturedFaces.Count < 5)
                {
                    setStatus("Good! Please " + poseTips[capturedFaces.Count] + ", then click CAPTURE FACE.", Color.DarkGreen);
                }
            }
            catch (Exception ex)
            {
                stopCamera();
                updateCaptureButtons();
                setStatus("Camera stopped. Reconnect it and click START CAMERA. " + Database.friendlyMessage(ex), Color.Firebrick);
            }
        }

        // ------------------------------------------------------------------
        // Face detection
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

            // The detector sometimes finds tiny "faces" in the background or on clothes.
            // A real second person has a face of similar size, so faces smaller than 60% of the biggest face are ignored.
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
                return "No face detected. Please look at the camera.";
            }

            if (faces.Length > 1)
            {
                return "Multiple faces detected. Only one person may be in front of the camera.";
            }

            Rect face = faces[0];

            if (face.Width < 90)
            {
                return "Your face is too small. Please move closer to the camera.";
            }

            if (face.X < 3 || face.Y < 3 || face.Right > picture.Width - 3 || face.Bottom > picture.Height - 3)
            {
                return "Your face is too close to the edge. Please move to the center.";
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
                    return "The picture is too bright. Avoid strong light on your face.";
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
                    return "The picture is blurry. Please hold still and look at the camera.";
                }
            }

            return "";
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
        // Capture, retake and reset
        // ------------------------------------------------------------------

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (timerCamera.Enabled == false)
            {
                holdStatus("The webcam is not running. Click START CAMERA first.", Color.Firebrick);
                return;
            }

            if (hasFreshCameraFrame == false || failedFrameCount > 0)
            {
                holdStatus("No new webcam picture is available. Wait for the next clear camera frame, then try again.", Color.Firebrick);
                return;
            }

            if (capturedFaces.Count >= 5)
            {
                holdStatus("All 5 face pictures are already captured.", Color.DarkGreen);
                return;
            }

            try
            {
                if (cameraFrame == null || cameraFrame.Empty())
                {
                    holdStatus("No camera frame is available. Wait for a new picture or restart the webcam.", Color.Firebrick);
                    return;
                }

                Rect[] faces = detectFaces(cameraFrame);
                string problem = checkFaceQuality(cameraFrame, faces);
                if (problem != "")
                {
                    holdStatus(problem + " The picture was NOT saved.", Color.Firebrick);
                    return;
                }

                using (Mat faceCrop = new Mat(cameraFrame, faces[0]))
                using (Mat resizedFace = new Mat())
                {
                    // Every saved face picture is 160 x 160 pixels.
                    Cv2.Resize(faceCrop, resizedFace, new OpenCvSharp.Size(160, 160));
                    byte[] pngBytes = resizedFace.ImEncode(".png");

                    // Do not accept the exact same picture two times.
                    for (int i = 0; i < capturedFaces.Count; i++)
                    {
                        if (isSamePicture(capturedFaces[i], pngBytes) == true)
                        {
                            holdStatus("This is the same picture as before. Move your head slightly and capture again.", Color.Firebrick);
                            return;
                        }
                    }

                    showImage(facePictureBoxes[capturedFaces.Count], matToBitmap(resizedFace));
                    capturedFaces.Add(pngBytes);
                }
            }
            catch (Exception ex)
            {
                hasFreshCameraFrame = false;
                holdStatus("Picture not captured. Wait for a new frame or restart the camera. " + Database.friendlyMessage(ex), Color.Firebrick);
                return;
            }

            int faceCount = capturedFaces.Count;

            if (faceCount == 5)
            {
                // All pictures are done, so the webcam is no longer needed.
                stopCamera();
                setStatus("Face 5 of 5 captured. Face registration is complete - click " + btnSave.Text + ".", Color.DarkGreen);
            }
            else
            {
                holdStatus("Face " + faceCount + " of 5 captured. Next: " + poseTips[faceCount] + ".", Color.DarkGreen);
            }

            updateProgress();
            updateCaptureButtons();
        }

        private bool isSamePicture(byte[] firstPicture, byte[] secondPicture)
        {
            if (firstPicture.Length != secondPicture.Length)
            {
                return false;
            }

            for (int i = 0; i < firstPicture.Length; i++)
            {
                if (firstPicture[i] != secondPicture[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void btnRetake_Click(object sender, EventArgs e)
        {
            if (capturedFaces.Count == 0)
            {
                return;
            }

            DialogResult answer = MessageBox.Show("Delete the " + capturedFaces.Count + " captured picture(s) and start again?",
                "Retake All Faces", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            capturedFaces.Clear();
            clearFacePictureBoxes();
            keepMessageUntil = DateTime.MinValue;

            if (timerCamera.Enabled == true)
            {
                setStatus("Face 0 of 5 captured. Please " + poseTips[0] + ".", Color.MidnightBlue);
            }
            else
            {
                setStatus("Face 0 of 5 captured. Click START CAMERA to capture again.", Color.MidnightBlue);
            }

            updateProgress();
            updateCaptureButtons();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Reset the form? All typed information and captured pictures will be removed.",
                "Reset Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            stopCamera();

            if (studentIdToEdit == 0)
            {
                prepareAddMode();
            }
            else
            {
                // In edit mode we load the saved information again.
                btnSave.Enabled = false;
                if (!loadStudentToEdit())
                {
                    MessageBox.Show("This student could not be reloaded. Return to Student Records and reopen the student.");
                    return;
                }
                btnSave.Enabled = cmbDepartment.DataSource != null;
            }

            updateProgress();
            updateCaptureButtons();
            txtNumber.Focus();
        }

        // ------------------------------------------------------------------
        // Validation
        // ------------------------------------------------------------------

        private void showValidationMessage(string message, Control controlToFocus)
        {
            MessageBox.Show(message, "Invalid Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            controlToFocus.Focus();
        }

        private bool validateStudentInfo()
        {
            string studentNumber = txtNumber.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string contactNumber = txtContact.Text.Trim();

            // \p{L} means "any letter" (so names like "Peña" are accepted).
            string namePattern = @"^[\p{L} .'\-]+$";

            if (studentNumber == "")
            {
                showValidationMessage("Please enter the student ID number.", txtNumber);
                return false;
            }

            if (Regex.IsMatch(studentNumber, @"^[A-Za-z0-9\-]{3,20}$") == false)
            {
                showValidationMessage("The ID number may only contain letters, numbers and dashes (example: 2026-001).", txtNumber);
                return false;
            }

            if (firstName == "")
            {
                showValidationMessage("Please enter the first name.", txtFirstName);
                return false;
            }

            if (Regex.IsMatch(firstName, namePattern) == false)
            {
                showValidationMessage("The first name may only contain letters, spaces, dots, dashes and apostrophes.", txtFirstName);
                return false;
            }

            if (middleName != "" && Regex.IsMatch(middleName, namePattern) == false)
            {
                showValidationMessage("The middle name may only contain letters, spaces, dots, dashes and apostrophes.", txtMiddleName);
                return false;
            }

            if (lastName == "")
            {
                showValidationMessage("Please enter the last name.", txtLastName);
                return false;
            }

            if (Regex.IsMatch(lastName, namePattern) == false)
            {
                showValidationMessage("The last name may only contain letters, spaces, dots, dashes and apostrophes.", txtLastName);
                return false;
            }

            if (Database.getSelectedId(cmbDepartment) == 0)
            {
                showValidationMessage("Please select the course.", cmbDepartment);
                return false;
            }

            if (cmbYear.SelectedIndex < 0)
            {
                showValidationMessage("Please select the year level.", cmbYear);
                return false;
            }

            if (cmbGender.SelectedIndex < 0)
            {
                showValidationMessage("Please select the gender.", cmbGender);
                return false;
            }

            if (email != "" && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") == false)
            {
                showValidationMessage("Please enter a valid email address (example: juan@gmail.com).", txtEmail);
                return false;
            }

            if (contactNumber != "" && Regex.IsMatch(contactNumber, @"^(09\d{9}|\+639\d{9})$") == false)
            {
                showValidationMessage("Please enter a valid contact number (example: 09123456789 or +639123456789).", txtContact);
                return false;
            }

            if (cmbStatus.SelectedIndex < 0)
            {
                showValidationMessage("Please select the status.", cmbStatus);
                return false;
            }

            return true;
        }

        // Checks the database for another student with the same ID number, email or name.
        // Returns false when the student must NOT be saved.
        private bool checkDuplicateStudent()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@number", txtNumber.Text.Trim());
            parameters.Add("@id", studentIdToEdit);

            int sameNumberCount = Database.GetCount("SELECT COUNT(*) FROM students WHERE student_number = @number AND student_id <> @id", parameters);

            if (sameNumberCount > 0)
            {
                showValidationMessage("The ID number \"" + txtNumber.Text.Trim() + "\" is already used by another student.", txtNumber);
                return false;
            }

            if (txtEmail.Text.Trim() != "")
            {
                Dictionary<string, object> emailParameters = new Dictionary<string, object>();
                emailParameters.Add("@email", txtEmail.Text.Trim());
                emailParameters.Add("@id", studentIdToEdit);

                int sameEmailCount = Database.GetCount("SELECT COUNT(*) FROM students WHERE email = @email AND student_id <> @id", emailParameters);

                if (sameEmailCount > 0)
                {
                    showValidationMessage("The email \"" + txtEmail.Text.Trim() + "\" is already used by another student.", txtEmail);
                    return false;
                }
            }

            // The same full name is allowed (two students can have the same name), but we ask first.
            Dictionary<string, object> nameParameters = new Dictionary<string, object>();
            nameParameters.Add("@first", txtFirstName.Text.Trim());
            nameParameters.Add("@last", txtLastName.Text.Trim());
            nameParameters.Add("@middle", txtMiddleName.Text.Trim());
            nameParameters.Add("@id", studentIdToEdit);

            object sameNameNumber = Database.GetValue("SELECT student_number FROM students WHERE first_name = @first AND last_name = @last " +
                                                      "AND IFNULL(middle_name, '') = @middle AND student_id <> @id LIMIT 1", nameParameters);

            if (sameNameNumber != null)
            {
                DialogResult answer = MessageBox.Show("A student with the same name already exists (ID: " + sameNameNumber.ToString() + ").\n\n" +
                    "Are you sure this is a different student?", "Possible Duplicate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (answer != DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        // Compares the 5 new face pictures with the faces of the OTHER registered students.
        // Returns the name of a student whose face looks the same, or "" when no similar face was found.
        private string findSimilarRegisteredFace()
        {
            List<Mat> trainingFaces = new List<Mat>();
            List<int> trainingLabels = new List<int>();
            Dictionary<int, string> studentNames = new Dictionary<int, string>();

            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT f.student_id, f.image_data, s.student_number, CONCAT(s.first_name, ' ', s.last_name) AS full_name " +
                                   "FROM face_images f JOIN students s ON s.student_id = f.student_id " +
                                   "WHERE s.face_registered = 1 AND s.student_id <> @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", studentIdToEdit);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int studentId = Convert.ToInt32(reader["student_id"]);
                                byte[] imageBytes = (byte[])reader["image_data"];

                                using (Mat colorFace = Cv2.ImDecode(imageBytes, ImreadModes.Color))
                                {
                                    if (colorFace.Empty() == false)
                                    {
                                        trainingFaces.Add(prepareFaceForRecognition(colorFace));
                                        trainingLabels.Add(studentId);
                                    }
                                }

                                if (studentNames.ContainsKey(studentId) == false)
                                {
                                    studentNames.Add(studentId, reader["full_name"].ToString() + " (ID: " + reader["student_number"].ToString() + ")");
                                }
                            }
                        }
                    }
                }

                // There are no other registered students yet.
                if (trainingFaces.Count == 0)
                {
                    return "";
                }

                // How many of the 5 new pictures look like each registered student.
                Dictionary<int, int> matchCount = new Dictionary<int, int>();

                using (LBPHFaceRecognizer recognizer = LBPHFaceRecognizer.Create())
                {
                    recognizer.Train(trainingFaces, trainingLabels);

                    for (int i = 0; i < capturedFaces.Count; i++)
                    {
                        using (Mat colorFace = Cv2.ImDecode(capturedFaces[i], ImreadModes.Color))
                        using (Mat preparedFace = prepareFaceForRecognition(colorFace))
                        {
                            int predictedStudentId;
                            double distance;
                            recognizer.Predict(preparedFace, out predictedStudentId, out distance);

                            // A small distance means the faces are very similar.
                            if (distance <= 55)
                            {
                                if (matchCount.ContainsKey(predictedStudentId) == false)
                                {
                                    matchCount.Add(predictedStudentId, 0);
                                }

                                matchCount[predictedStudentId] = matchCount[predictedStudentId] + 1;
                            }
                        }
                    }
                }

                // At least 3 of the 5 pictures must match the same student.
                foreach (KeyValuePair<int, int> match in matchCount)
                {
                    if (match.Value >= 3)
                    {
                        return studentNames[match.Key];
                    }
                }
            }
            finally
            {
                for (int i = 0; i < trainingFaces.Count; i++)
                {
                    trainingFaces[i].Dispose();
                }
            }

            return "";
        }

        // The same preparation is used in FaceRecognition.cs: gray, 100 x 100 and equalized light.
        private Mat prepareFaceForRecognition(Mat colorFace)
        {
            Mat grayFace = new Mat();
            Cv2.CvtColor(colorFace, grayFace, ColorConversionCodes.BGR2GRAY);
            Cv2.Resize(grayFace, grayFace, new OpenCvSharp.Size(100, 100));
            Cv2.EqualizeHist(grayFace, grayFace);
            return grayFace;
        }

        // ------------------------------------------------------------------
        // Save
        // ------------------------------------------------------------------

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validateStudentInfo() == false)
            {
                return;
            }

            bool saveNewFaces = chkReRegister.Checked;

            // A student is only fully registered with EXACTLY 5 face pictures.
            if (saveNewFaces == true && capturedFaces.Count != 5)
            {
                MessageBox.Show("Face registration is not complete.\n\n" +
                    "Captured: " + capturedFaces.Count + " of 5 face pictures.\n" +
                    "Please capture all 5 face pictures before saving.", "Face Registration Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (checkDuplicateStudent() == false)
                {
                    return;
                }
            }
            catch (MySqlException ex)
            {
                Database.showError("Could not check for a duplicate student. No changes were saved.", ex);
                return;
            }

            if (saveNewFaces == true)
            {
                this.Cursor = Cursors.WaitCursor;
                setStatus("Checking if this face is already registered...", Color.MidnightBlue);
                lblCaptureStatus.Refresh();

                string similarStudent;
                try
                {
                    similarStudent = findSimilarRegisteredFace();
                }
                catch (Exception ex) when (ex is MySqlException || ex is OpenCVException ||
                    ex is DllNotFoundException || ex is TypeInitializationException)
                {
                    setStatus("The face check failed. Your captured pictures are still here.", Color.Firebrick);
                    Database.showError("Could not compare the registered faces. The student was not saved.", ex);
                    return;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                if (similarStudent != "")
                {
                    DialogResult answer = MessageBox.Show("This face looks like a student who is already registered:\n\n" + similarStudent + "\n\n" +
                        "Saving the same person two times can cause wrong attendance.\nDo you still want to save?",
                        "Possible Duplicate Face", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (answer != DialogResult.Yes)
                    {
                        setStatus("Saving was cancelled.", Color.Firebrick);
                        return;
                    }
                }
            }

            stopCamera();
            updateCaptureButtons();

            int savedStudentId = saveStudentToDatabase(saveNewFaces);

            if (savedStudentId == 0)
            {
                setStatus("The student was NOT saved.", Color.Firebrick);
                return;
            }

            isSaved = true;

            if (studentIdToEdit == 0)
            {
                MessageBox.Show("Student registered successfully!\n\n" +
                    "Name: " + txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() + "\n" +
                    "Student ID: " + txtNumber.Text.Trim() + "\n" +
                    "Face pictures: 5 of 5", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Student information updated successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Going back to Student Records happens in the FormClosed event.
            this.Close();
        }

        // Saves the student, the face pictures and the event rosters in ONE transaction:
        // when one step fails, nothing is saved. Returns the student id (0 = not saved).
        private int saveStudentToDatabase(bool saveNewFaces)
        {
            int studentId = studentIdToEdit;

            object middleName = DBNull.Value;
            object email = DBNull.Value;
            object contactNumber = DBNull.Value;

            if (txtMiddleName.Text.Trim() != "")
            {
                middleName = txtMiddleName.Text.Trim();
            }

            if (txtEmail.Text.Trim() != "")
            {
                email = txtEmail.Text.Trim();
            }

            if (txtContact.Text.Trim() != "")
            {
                contactNumber = txtContact.Text.Trim();
            }

            using (MySqlConnection conn = Database.GetConnection())
            {
                MySqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // 1. Save the student information
                    string studentQuery;

                    if (studentId == 0)
                    {
                        studentQuery = "INSERT INTO students (student_number, first_name, middle_name, last_name, department_id, year_level, gender, email, contact_number, status, face_registered) " +
                                       "VALUES (@number, @first, @middle, @last, @departmentId, @year, @gender, @email, @contact, @status, 0)";
                    }
                    else
                    {
                        studentQuery = "UPDATE students SET student_number = @number, first_name = @first, middle_name = @middle, last_name = @last, " +
                                       "department_id = @departmentId, year_level = @year, gender = @gender, email = @email, contact_number = @contact, status = @status " +
                                       "WHERE student_id = @id";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(studentQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@number", txtNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@first", txtFirstName.Text.Trim());
                        cmd.Parameters.AddWithValue("@middle", middleName);
                        cmd.Parameters.AddWithValue("@last", txtLastName.Text.Trim());
                        cmd.Parameters.AddWithValue("@departmentId", Database.getSelectedId(cmbDepartment));
                        cmd.Parameters.AddWithValue("@year", cmbYear.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@gender", cmbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@contact", contactNumber);
                        cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@id", studentId);

                        int rowsSaved = cmd.ExecuteNonQuery();

                        if (rowsSaved == 0)
                        {
                            transaction.Rollback();
                            MessageBox.Show("The student was not found. It may have been deleted by another user.", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return 0;
                        }

                        if (studentId == 0)
                        {
                            studentId = Convert.ToInt32(cmd.LastInsertedId);
                        }
                    }

                    // 2. Save the 5 new face pictures (the old pictures are deleted first)
                    if (saveNewFaces == true)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("DELETE FROM face_images WHERE student_id = @id", conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@id", studentId);
                            cmd.ExecuteNonQuery();
                        }

                        for (int i = 0; i < capturedFaces.Count; i++)
                        {
                            using (MySqlCommand cmd = new MySqlCommand("INSERT INTO face_images (student_id, image_number, image_data) VALUES (@id, @imageNumber, @imageData)", conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@id", studentId);
                                cmd.Parameters.AddWithValue("@imageNumber", i + 1);
                                cmd.Parameters.Add("@imageData", MySqlDbType.MediumBlob).Value = capturedFaces[i];
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // 3. Make sure the student really has 5 face pictures
                    int savedFaceCount = 0;

                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM face_images WHERE student_id = @id", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", studentId);
                        savedFaceCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (savedFaceCount != 5)
                    {
                        transaction.Rollback();
                        MessageBox.Show("The student needs exactly 5 face pictures. Please re-register the face.", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 0;
                    }

                    using (MySqlCommand cmd = new MySqlCommand("UPDATE students SET face_registered = 1 WHERE student_id = @id", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", studentId);
                        cmd.ExecuteNonQuery();
                    }

                    // 4. Update the event rosters (the list of students expected in each event).
                    //    Events that have not started yet are rebuilt, so a new course or year level is followed.
                    string removeQuery = "DELETE r FROM event_students r WHERE r.student_id = @id " +
                                         "AND NOT EXISTS (SELECT 1 FROM event_sessions es WHERE es.event_id = r.event_id AND es.starts_at <= NOW())";

                    using (MySqlCommand cmd = new MySqlCommand(removeQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", studentId);
                        cmd.ExecuteNonQuery();
                    }

                    //    Active students are added to every event that has not ended yet and matches the course and year level.
                    string addQuery = "INSERT INTO event_students (event_id, student_id, added_at) " +
                                      "SELECT e.event_id, s.student_id, NOW() FROM events e JOIN students s ON s.student_id = @id " +
                                      "WHERE s.status = 'Active' " +
                                      "AND (e.department_id IS NULL OR e.department_id = s.department_id) " +
                                      "AND (e.year_level IS NULL OR e.year_level = s.year_level) " +
                                      "AND EXISTS (SELECT 1 FROM event_sessions es WHERE es.event_id = e.event_id AND es.ends_at > NOW()) " +
                                      "AND NOT EXISTS (SELECT 1 FROM event_students x WHERE x.event_id = e.event_id AND x.student_id = s.student_id)";

                    using (MySqlCommand cmd = new MySqlCommand(addQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@id", studentId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return studentId;
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
                            // The connection is already broken, so there is nothing to roll back.
                        }
                    }

                    Database.showError("The student was not saved.", ex);
                    return 0;
                }
            }
        }

        // ------------------------------------------------------------------
        // Cancel and closing
        // ------------------------------------------------------------------

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Going back to Student Records happens in the FormClosed event.
            this.Close();
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaved == false && capturedFaces.Count > 0)
            {
                DialogResult answer = MessageBox.Show("The student is not saved yet. Close and discard the captured face pictures?",
                    "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Always turn off the webcam so other programs can use it.
            stopCamera();
            clearFacePictureBoxes();

            // Go back to the Student Records page.
            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
        }

        // ------------------------------------------------------------------
        // Picture helpers
        // ------------------------------------------------------------------

        private void clearFacePictureBoxes()
        {
            for (int i = 0; i < facePictureBoxes.Length; i++)
            {
                if (facePictureBoxes[i] != null)
                {
                    showImage(facePictureBoxes[i], null);
                }
            }
        }

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
    }
}
