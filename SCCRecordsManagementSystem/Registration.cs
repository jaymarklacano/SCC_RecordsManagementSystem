using MySql.Data.MySqlClient;
using OpenCvSharp;
using OpenCvSharp.Dnn;
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

        // Webcam, face detector (YuNet) and face recognizer (SFace) objects from OpenCvSharp.
        private VideoCapture camera = null;
        private FaceDetectorYN faceDetector = null;
        private OpenCvSharp.Size faceDetectorSize = new OpenCvSharp.Size(0, 0);
        private Net faceRecognizer = null;
        private Mat cameraFrame = null;
        private int failedFrameCount = 0;
        private bool hasFreshCameraFrame = false;

        // The 2 face model files. They are copied next to the program when the project is built.
        private const string detectorFileName = "face_detection_yunet_2023mar.onnx";
        private const string recognizerFileName = "face_recognition_sface_2021dec.onnx";

        // "Similarity" tells how much two faces look alike: 1.0 = exactly the same, 0 or lower = not alike at all.
        // Two faces are the SAME PERSON when the similarity is 0.50 or higher (the same number is used in FaceRecognition.cs).
        private const double sameFaceThreshold = 0.50;

        // Picture 2, 3, 4 and 5 must have at least this similarity with one of the earlier pictures,
        // so that the 5 pictures are surely of the same student.
        private const double samePersonThreshold = 0.40;

        // Face features of the saved pictures that were already calculated, by face_image_id. "static" = remembered
        // while the program is open, so the duplicate face check is faster next time. (Re-registered faces get a new face_image_id.)
        private static Dictionary<int, float[]> savedFeatures = new Dictionary<int, float[]>();

        // A capture message stays on the screen until this time (so the live tips do not hide it right away).
        private DateTime keepMessageUntil = DateTime.MinValue;

        // The new face pictures captured with the webcam (PNG bytes, 112 x 112, the face is lined up by the eyes).
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

                // 1. Load the face detector and face recognizer files (they are copied to the bin folder when the project is built).
                string modelProblem = loadFaceModels();

                if (modelProblem != "")
                {
                    stopCamera();
                    setStatus("The face model files are missing or damaged. Rebuild the project.", Color.Firebrick);
                    MessageBox.Show(modelProblem, "Face Model Files", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (BadImageFormatException)
            {
                stopCamera();
                setStatus("OpenCV needs 64-bit. See the README (Troubleshooting).", Color.Firebrick);
                MessageBox.Show("The OpenCV files are 64-bit, but the program is running as 32-bit.\n" +
                    "In Visual Studio: Project > Properties > Build > uncheck \"Prefer 32-bit\", then rebuild.",
                    "Wrong Platform", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // The face models stay loaded, because the SAVE button still needs them.
            // They are removed from memory when the form closes (see unloadFaceModels).

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

                List<FaceInfo> faces = detectFaces(cameraFrame);
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
        // Face detection and face recognition
        // (the same code is in FaceRecognition.cs)
        //
        // How it works:
        //   1. YuNet finds the face and 5 points on it (eyes, nose and mouth corners).
        //   2. The face is turned and resized to 112 x 112 so the eyes are always at the same place ("aligned").
        //   3. SFace changes the aligned face into 128 numbers (the "face feature").
        //   4. Two faces are compared by their features. The result is the "similarity".
        // ------------------------------------------------------------------

        // Loads the 2 face model files. Returns "" when everything is OK,
        // otherwise a message that explains the problem.
        private string loadFaceModels()
        {
            // Already loaded
            if (faceDetector != null && faceRecognizer != null)
            {
                return "";
            }

            string detectorPath = Path.Combine(Application.StartupPath, detectorFileName);
            string recognizerPath = Path.Combine(Application.StartupPath, recognizerFileName);

            if (File.Exists(detectorPath) == false || File.Exists(recognizerPath) == false)
            {
                return "The face model files were not found in:\n" + Application.StartupPath + "\n\n" +
                    "Needed files:\n- " + detectorFileName + "\n- " + recognizerFileName + "\n\n" +
                    "Rebuild the project in Visual Studio so the files are copied again.";
            }

            try
            {
                // The detector is created for a 640 x 480 picture. detectFaces() creates it again when the picture size is different.
                faceDetectorSize = new OpenCvSharp.Size(640, 480);
                faceDetector = FaceDetectorYN.Create(detectorPath, "", faceDetectorSize, 0.8f, 0.3f, 5000);
                faceRecognizer = CvDnn.ReadNetFromOnnx(recognizerPath);
            }
            catch (OpenCVException)
            {
                unloadFaceModels();
                return "The face model files are damaged.\nDownload the project again (or copy the .onnx files again) and rebuild it.";
            }

            if (faceRecognizer == null || faceRecognizer.Empty() == true)
            {
                unloadFaceModels();
                return "The face recognizer file \"" + recognizerFileName + "\" is damaged.\nDownload the project again and rebuild it.";
            }

            return "";
        }

        private void unloadFaceModels()
        {
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
        }

        // Finds the faces in a picture. Returns one FaceInfo (box + 5 points) for every face.
        private List<FaceInfo> detectFaces(Mat picture)
        {
            // YuNet must know the size of the picture. When the size changes, the detector is created again.
            if (faceDetectorSize != picture.Size())
            {
                faceDetector.Dispose();
                faceDetectorSize = picture.Size();
                faceDetector = FaceDetectorYN.Create(Path.Combine(Application.StartupPath, detectorFileName), "", faceDetectorSize, 0.8f, 0.3f, 5000);
            }

            List<FaceInfo> foundFaces = new List<FaceInfo>();

            using (Mat result = new Mat())
            {
                faceDetector.Detect(picture, result);

                // Every row of the result is one face with 15 numbers:
                // x, y, width, height, the 5 points (x and y of each) and the score.
                for (int row = 0; row < result.Rows; row++)
                {
                    FaceInfo face = new FaceInfo();
                    face.Box = new Rect((int)result.At<float>(row, 0), (int)result.At<float>(row, 1),
                                        (int)result.At<float>(row, 2), (int)result.At<float>(row, 3));

                    face.Landmarks = new Point2f[5];

                    for (int i = 0; i < 5; i++)
                    {
                        face.Landmarks[i] = new Point2f(result.At<float>(row, 4 + i * 2), result.At<float>(row, 5 + i * 2));
                    }

                    face.Score = result.At<float>(row, 14);
                    foundFaces.Add(face);
                }
            }

            // People far behind the student have small faces. A real second person in front of
            // the camera has a face of similar size, so faces smaller than 60% of the biggest face are ignored.
            int biggestWidth = 0;

            for (int i = 0; i < foundFaces.Count; i++)
            {
                if (foundFaces[i].Box.Width > biggestWidth)
                {
                    biggestWidth = foundFaces[i].Box.Width;
                }
            }

            List<FaceInfo> realFaces = new List<FaceInfo>();

            for (int i = 0; i < foundFaces.Count; i++)
            {
                if (foundFaces[i].Box.Width >= biggestWidth * 0.6)
                {
                    realFaces.Add(foundFaces[i]);
                }
            }

            return realFaces;
        }

        // Returns "" when the picture has exactly ONE clear face.
        // Otherwise returns a message that tells the student what to fix.
        private string checkFaceQuality(Mat picture, List<FaceInfo> faces)
        {
            if (faces.Count == 0)
            {
                return "No face detected. Please look at the camera.";
            }

            if (faces.Count > 1)
            {
                return "Multiple faces detected. Only one person may be in front of the camera.";
            }

            Rect face = faces[0].Box;

            if (face.Width < 90)
            {
                return "Your face is too small. Please move closer to the camera.";
            }

            if (face.X < 3 || face.Y < 3 || face.Right > picture.Width - 3 || face.Bottom > picture.Height - 3)
            {
                return "Your face is too close to the edge. Please move to the center.";
            }

            // The nose must be between the 2 eyes. If not, the head is turned too much to the side.
            Point2f rightEye = faces[0].Landmarks[0];
            Point2f leftEye = faces[0].Landmarks[1];
            Point2f nose = faces[0].Landmarks[2];

            if (nose.X <= Math.Min(rightEye.X, leftEye.X) || nose.X >= Math.Max(rightEye.X, leftEye.X))
            {
                return "Your head is turned too much. Please face the camera.";
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

        // Turns and resizes the face to 112 x 112 so that the eyes, nose and mouth are always at the same place.
        // Returns null when the face can not be lined up.
        private Mat alignFace(Mat picture, FaceInfo face)
        {
            // Where the 5 points must be in the 112 x 112 picture (the SFace model was trained like this).
            Point2f[] targetPoints = new Point2f[]
            {
                new Point2f(38.2946f, 51.6963f),   // right eye
                new Point2f(73.5318f, 51.5014f),   // left eye
                new Point2f(56.0252f, 71.7366f),   // nose tip
                new Point2f(41.5493f, 92.3655f),   // right mouth corner
                new Point2f(70.7299f, 92.2041f)    // left mouth corner
            };

            // "transform" tells how to move, turn and resize the picture so the 5 points land on the target points.
            using (Mat transform = Cv2.EstimateAffinePartial2D(InputArray.Create(face.Landmarks), InputArray.Create(targetPoints),
                                                               null, RobustEstimationAlgorithms.LMEDS))
            {
                if (transform == null || transform.Empty() == true)
                {
                    return null;
                }

                Mat alignedFace = new Mat();
                Cv2.WarpAffine(picture, alignedFace, transform, new OpenCvSharp.Size(112, 112));
                return alignedFace;
            }
        }

        // Changes an aligned 112 x 112 face into its 128-number "face feature" (with SFace).
        private float[] getFaceFeature(Mat alignedFace)
        {
            using (Mat blob = CvDnn.BlobFromImage(alignedFace, 1.0, new OpenCvSharp.Size(112, 112), new Scalar(0, 0, 0), true, false))
            {
                faceRecognizer.SetInput(blob);

                using (Mat output = faceRecognizer.Forward())
                {
                    float[] feature = new float[output.Cols];

                    for (int i = 0; i < feature.Length; i++)
                    {
                        feature[i] = output.At<float>(0, i);
                    }

                    // Make the length of the feature 1, so comparing two features gives a number from -1 to 1.
                    double length = 0;

                    for (int i = 0; i < feature.Length; i++)
                    {
                        length = length + feature[i] * feature[i];
                    }

                    length = Math.Sqrt(length);

                    for (int i = 0; i < feature.Length; i++)
                    {
                        feature[i] = (float)(feature[i] / length);
                    }

                    return feature;
                }
            }
        }

        // Compares two face features. 1.0 = exactly the same face, 0.50 or more = the same person.
        private double compareFaces(float[] firstFeature, float[] secondFeature)
        {
            double similarity = 0;

            for (int i = 0; i < firstFeature.Length; i++)
            {
                similarity = similarity + firstFeature[i] * secondFeature[i];
            }

            return similarity;
        }

        // Gets the face feature of a picture saved in the database. Returns null when the picture can not be used.
        private float[] getSavedFaceFeature(byte[] imageBytes)
        {
            using (Mat savedFace = Cv2.ImDecode(imageBytes, ImreadModes.Color))
            {
                if (savedFace.Empty() == true)
                {
                    return null;
                }

                using (Mat alignedFace = alignSavedFace(savedFace))
                {
                    if (alignedFace == null)
                    {
                        return null;
                    }

                    return getFaceFeature(alignedFace);
                }
            }
        }

        // New pictures are saved already aligned (112 x 112), so they are used as they are.
        // Pictures saved by the older version of this program (160 x 160) are aligned here first.
        // Returns null when no face is found in the picture.
        private Mat alignSavedFace(Mat savedFace)
        {
            if (savedFace.Width == 112 && savedFace.Height == 112)
            {
                return savedFace.Clone();
            }

            // The face fills the whole old picture, so a border is added around it first. YuNet finds the face better that way.
            int border = savedFace.Width * 3 / 10;

            using (Mat biggerPicture = new Mat())
            {
                Cv2.CopyMakeBorder(savedFace, biggerPicture, border, border, border, border, BorderTypes.Replicate);

                List<FaceInfo> faces = detectFaces(biggerPicture);

                if (faces.Count == 0)
                {
                    return null;
                }

                // Use the biggest face
                FaceInfo biggestFace = faces[0];

                for (int i = 1; i < faces.Count; i++)
                {
                    if (faces[i].Box.Width > biggestFace.Box.Width)
                    {
                        biggestFace = faces[i];
                    }
                }

                return alignFace(biggerPicture, biggestFace);
            }
        }

        // Shows the webcam picture like a mirror, with a colored box and the 5 face points on every face.
        private void showCameraFrame(Mat picture, List<FaceInfo> faces, Scalar boxColor)
        {
            using (Mat display = picture.Clone())
            {
                for (int i = 0; i < faces.Count; i++)
                {
                    Cv2.Rectangle(display, faces[i].Box, boxColor, 3);

                    for (int p = 0; p < faces[i].Landmarks.Length; p++)
                    {
                        OpenCvSharp.Point point = new OpenCvSharp.Point((int)faces[i].Landmarks[p].X, (int)faces[i].Landmarks[p].Y);
                        Cv2.Circle(display, point, 3, boxColor, -1);
                    }
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

                List<FaceInfo> faces = detectFaces(cameraFrame);
                string problem = checkFaceQuality(cameraFrame, faces);
                if (problem != "")
                {
                    holdStatus(problem + " The picture was NOT saved.", Color.Firebrick);
                    return;
                }

                // Every saved face picture is 112 x 112 pixels and lined up by the eyes (this is what SFace needs).
                using (Mat alignedFace = alignFace(cameraFrame, faces[0]))
                {
                    if (alignedFace == null)
                    {
                        holdStatus("The face could not be lined up. Look straight at the camera and capture again.", Color.Firebrick);
                        return;
                    }

                    // Picture 2 to 5 must look like at least one of the earlier pictures (the same student).
                    if (capturedFaces.Count > 0)
                    {
                        float[] newFeature = getFaceFeature(alignedFace);
                        double bestSimilarity = -1;

                        for (int i = 0; i < capturedFaces.Count; i++)
                        {
                            float[] earlierFeature = getSavedFaceFeature(capturedFaces[i]);

                            if (earlierFeature != null)
                            {
                                double similarity = compareFaces(newFeature, earlierFeature);

                                if (similarity > bestSimilarity)
                                {
                                    bestSimilarity = similarity;
                                }
                            }
                        }

                        if (bestSimilarity < samePersonThreshold)
                        {
                            holdStatus("This face does not look like the earlier pictures. Only the student being registered may " +
                                "be in front of the camera. (Click RETAKE ALL FACES if an earlier picture was wrong.)", Color.Firebrick);
                            return;
                        }
                    }

                    byte[] pngBytes = alignedFace.ImEncode(".png");

                    // Do not accept the exact same picture two times.
                    for (int i = 0; i < capturedFaces.Count; i++)
                    {
                        if (isSamePicture(capturedFaces[i], pngBytes) == true)
                        {
                            holdStatus("This is the same picture as before. Move your head slightly and capture again.", Color.Firebrick);
                            return;
                        }
                    }

                    showImage(facePictureBoxes[capturedFaces.Count], matToBitmap(alignedFace));
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
            // 1. The face features of the 5 new pictures
            List<float[]> newFeatures = new List<float[]>();

            for (int i = 0; i < capturedFaces.Count; i++)
            {
                float[] feature = getSavedFaceFeature(capturedFaces[i]);

                if (feature != null)
                {
                    newFeatures.Add(feature);
                }
            }

            if (newFeatures.Count == 0)
            {
                return "";
            }

            // 2. Compare them with every saved picture of the other students.
            // matchedPictures[studentId][i] = true when new picture number i looks like that student.
            Dictionary<int, bool[]> matchedPictures = new Dictionary<int, bool[]>();
            Dictionary<int, string> studentNames = new Dictionary<int, string>();
            int checkedPictures = 0;

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT f.student_id, f.face_image_id, f.image_data, s.student_number, CONCAT(s.first_name, ' ', s.last_name) AS full_name " +
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
                            int faceImageId = Convert.ToInt32(reader["face_image_id"]);
                            float[] savedFeature;

                            // A picture that was already read before is not read again (this makes saving faster).
                            if (savedFeatures.ContainsKey(faceImageId) == true)
                            {
                                savedFeature = savedFeatures[faceImageId];
                            }
                            else
                            {
                                byte[] imageBytes = (byte[])reader["image_data"];

                                try
                                {
                                    savedFeature = getSavedFaceFeature(imageBytes);
                                }
                                catch (OpenCVException)
                                {
                                    // A damaged picture can not be read. It is skipped below.
                                    savedFeature = null;
                                }

                                savedFeatures[faceImageId] = savedFeature;
                            }

                            checkedPictures = checkedPictures + 1;

                            // Show that the program is still working when there are many students.
                            if (checkedPictures % 25 == 0)
                            {
                                setStatus("Checking if this face is already registered... " + checkedPictures + " pictures checked.", Color.MidnightBlue);
                                lblCaptureStatus.Refresh();
                            }

                            if (savedFeature == null)
                            {
                                continue;
                            }

                            if (studentNames.ContainsKey(studentId) == false)
                            {
                                studentNames.Add(studentId, reader["full_name"].ToString() + " (ID: " + reader["student_number"].ToString() + ")");
                                matchedPictures.Add(studentId, new bool[newFeatures.Count]);
                            }

                            for (int i = 0; i < newFeatures.Count; i++)
                            {
                                if (compareFaces(newFeatures[i], savedFeature) >= sameFaceThreshold)
                                {
                                    matchedPictures[studentId][i] = true;
                                }
                            }
                        }
                    }
                }
            }

            // 3. At least 3 of the 5 new pictures must look like the same student.
            foreach (KeyValuePair<int, bool[]> match in matchedPictures)
            {
                int matchCount = 0;

                for (int i = 0; i < match.Value.Length; i++)
                {
                    if (match.Value[i] == true)
                    {
                        matchCount = matchCount + 1;
                    }
                }

                if (matchCount >= 3)
                {
                    return studentNames[match.Key];
                }
            }

            return "";
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
                // The face models are needed to compare the new face with the registered faces.
                string modelProblem;
                try
                {
                    modelProblem = loadFaceModels();
                }
                catch (Exception ex) when (ex is DllNotFoundException || ex is TypeInitializationException || ex is BadImageFormatException)
                {
                    modelProblem = "The OpenCV files were not found or are the wrong version (the program must run as 64-bit).\n" +
                        "Restore the NuGet packages (OpenCvSharp4.runtime.win) and rebuild the project.";
                }

                if (modelProblem != "")
                {
                    setStatus("The face check failed. Your captured pictures are still here.", Color.Firebrick);
                    MessageBox.Show(modelProblem, "Face Model Files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                setStatus("Checking if this face is already registered...", Color.MidnightBlue);
                lblCaptureStatus.Refresh();

                string similarStudent;
                try
                {
                    similarStudent = findSimilarRegisteredFace();
                }
                catch (Exception ex) when (ex is MySqlException || ex is OpenCVException ||
                    ex is DllNotFoundException || ex is TypeInitializationException || ex is BadImageFormatException)
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
                    MessageBox.Show("This face matches a student who is already registered:\n\n" + similarStudent + "\n\n" +
                        "The student was NOT saved. The same face cannot be registered to another student.\n" +
                        "Open the existing student record to update it, or retake the pictures if the wrong face was captured.",
                        "Face Already Registered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    holdStatus("The student was NOT saved. This face is already registered.", Color.Firebrick);
                    return;
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
            unloadFaceModels();
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
