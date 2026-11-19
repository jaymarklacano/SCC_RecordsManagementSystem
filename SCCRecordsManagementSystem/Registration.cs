using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Adds a new student or edits an existing one.
    // The student must have exactly 5 valid face pictures captured from the webcam.
    public partial class Registration : Form
    {
        private const int RequiredFaces = StudentService.RequiredFaceImages;

        // Short tips shown to the student for each of the 5 pictures.
        private static readonly string[] PoseTips =
        {
            "look straight at the camera",
            "turn your head slightly to the left",
            "turn your head slightly to the right",
            "tilt your head slightly up",
            "look at the camera normally or smile"
        };

        private static readonly Color InfoColor = Color.MidnightBlue;
        private static readonly Color GoodColor = Color.DarkGreen;
        private static readonly Color BadColor = Color.Firebrick;

        // 0 = add a new student, any other number = the student being edited
        private int studentId;

        private CameraHelper camera = new CameraHelper();
        private FaceService faceService;

        // The JPEG pictures captured in this window (a student needs exactly 5)
        private List<byte[]> capturedImages = new List<byte[]>();

        // The pictures already saved in the database (edit mode only, shown as "Saved")
        private List<byte[]> savedImages = new List<byte[]>();

        private PictureBox[] faceBoxes;
        private Label[] hintLabels;
        private string[] hintTexts;

        // The newest webcam picture without the green rectangle (this is the one we capture)
        private Bitmap lastFrame;

        private bool isLoading = true;      // true while the form fills its own boxes
        private bool isDirty = false;       // true when the user typed or captured something
        private bool isSaved = false;       // true after a successful save (no "unsaved changes" question)
        private bool isBusy = false;        // stops the camera timer from running twice at the same time
        private bool needsFaces = true;     // true when 5 new pictures must be captured
        private DateTime messageHoldUntil = DateTime.MinValue;

        public Registration() : this(0)
        {
        }

        public Registration(int studentId)
        {
            InitializeComponent();
            NavigationHelper.ExitWhenClosed(this);
            this.studentId = studentId;

            faceBoxes = new PictureBox[] { picFace1, picFace2, picFace3, picFace4, picFace5 };
            hintLabels = new Label[] { lblHint1, lblHint2, lblHint3, lblHint4, lblHint5 };
            hintTexts = new string[hintLabels.Length];
            for (int i = 0; i < hintLabels.Length; i++)
            {
                hintTexts[i] = hintLabels[i].Text;
            }
        }

        // ---------- Opening the form ----------

        private void Registration_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCourses();
                cmbYear.Items.Clear();
                cmbYear.Items.AddRange(StudentService.YearLevels);
                cmbGender.Items.Clear();
                cmbGender.Items.AddRange(StudentService.Genders);

                if (studentId == 0)
                {
                    ClearForm();
                }
                else
                {
                    lblPageTitle.Text = "Edit Student";
                    btnSave.Text = "UPDATE STUDENT";
                    FillFormFromDatabase();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Student Registration",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            isLoading = false;
            UpdateFaceControls();
        }

        // The webcam starts after the window is visible, so the window does not freeze while opening.
        private void Registration_Shown(object sender, EventArgs e)
        {
            if (needsFaces)
            {
                StartCamera();
            }
        }

        private void LoadCourses()
        {
            DataTable departments = DepartmentService.GetAll();
            DataTable choices = new DataTable();
            choices.Columns.Add("department_id", typeof(int));
            choices.Columns.Add("display_name", typeof(string));
            foreach (DataRow row in departments.Rows)
            {
                choices.Rows.Add(row["department_id"], row["department_code"] + " - " + row["department_name"]);
            }

            cmbDepartment.DataSource = choices;
            cmbDepartment.ValueMember = "department_id";
            cmbDepartment.DisplayMember = "display_name";
            cmbDepartment.DropDownWidth = 460;
        }

        // Empty form for a new student
        private void ClearForm()
        {
            bool oldLoading = isLoading;
            isLoading = true;

            txtNumber.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
            cmbStatus.SelectedIndex = 0;

            needsFaces = true;
            lblNote.Text = "Face registration is required: capture 5 face pictures before saving.";

            isLoading = oldLoading;
            isDirty = false;
        }

        // Fills the boxes with the student that is being edited
        private void FillFormFromDatabase()
        {
            bool oldLoading = isLoading;
            isLoading = true;

            Student student = StudentService.GetById(studentId);
            if (student == null)
            {
                throw new InvalidOperationException("This student no longer exists.");
            }

            txtNumber.Text = student.StudentNumber;
            txtFirstName.Text = student.FirstName;
            txtMiddleName.Text = student.MiddleName;
            txtLastName.Text = student.LastName;
            txtEmail.Text = student.Email;
            txtContact.Text = student.ContactNumber;
            cmbDepartment.SelectedValue = student.DepartmentId;
            cmbYear.SelectedIndex = cmbYear.FindStringExact(student.YearLevel);
            cmbGender.SelectedIndex = cmbGender.FindStringExact(student.Gender);
            cmbStatus.SelectedIndex = cmbStatus.FindStringExact(student.Status);

            savedImages = StudentService.GetFaceImages(studentId);
            chkReRegister.Visible = true;

            if (student.FaceRegistered && savedImages.Count == RequiredFaces)
            {
                // The saved pictures are kept unless the user ticks "Re-register"
                needsFaces = false;
                chkReRegister.Checked = false;
                lblNote.Text = "The saved face pictures are kept. Tick the box above to capture new ones.";
            }
            else
            {
                // Face data is missing, so new pictures are required
                needsFaces = true;
                chkReRegister.Checked = true;
                chkReRegister.Enabled = false;
                lblNote.Text = "This student has no complete face registration. Capture 5 face pictures.";
            }

            isLoading = oldLoading;
            isDirty = false;
        }

        // ---------- Typing in the form ----------

        private void Field_Changed(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                isDirty = true;
            }
        }

        private void chkReRegister_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoading)
            {
                return;
            }

            needsFaces = chkReRegister.Checked;
            capturedImages.Clear();
            messageHoldUntil = DateTime.MinValue;
            UpdateFaceControls();

            if (needsFaces)
            {
                StartCamera();
            }
            else
            {
                StopCamera();
            }
        }

        // ---------- Webcam ----------

        private void btnStartCamera_Click(object sender, EventArgs e)
        {
            StartCamera();
        }

        private void StartCamera()
        {
            if (camera.IsRunning)
            {
                return;
            }

            SetStatus("Starting the webcam, please wait...", InfoColor);
            btnStartCamera.Enabled = false;
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();   // lets the message above appear before the webcam starts

            try
            {
                if (faceService == null)
                {
                    faceService = new FaceService();
                }

                string problem = camera.Start();
                if (problem != "")
                {
                    SetStatus(problem, BadColor);
                }
                else
                {
                    timerCamera.Start();
                    SetStatus("Webcam is on. Look at the camera.", InfoColor);
                }
            }
            catch (Exception ex)
            {
                SetStatus(GetFaceErrorMessage(ex), BadColor);
            }
            finally
            {
                Cursor = Cursors.Default;
                UpdateFaceControls();
            }
        }

        private void StopCamera()
        {
            timerCamera.Stop();
            camera.Stop();

            if (lastFrame != null)
            {
                lastFrame.Dispose();
                lastFrame = null;
            }
            ShowPreview(null);
            UpdateFaceControls();
        }

        // Runs about 20 times per second while the webcam is on.
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
                        SetStatus("The webcam was disconnected. Plug it in again and click START CAMERA.", BadColor);
                    }
                    return;
                }

                if (lastFrame != null)
                {
                    lastFrame.Dispose();
                }
                lastFrame = frame;

                // Find the face, check it, then draw a rectangle on the preview
                Rectangle[] faces = faceService.DetectFaces(frame);
                string message;
                Color color;
                Color boxColor = Color.Orange;

                if (faces.Length == 0)
                {
                    message = "No face detected. Please look at the camera.";
                    color = BadColor;
                }
                else if (faces.Length > 1)
                {
                    message = "Multiple faces detected. Only one person can register at a time.";
                    color = BadColor;
                }
                else
                {
                    string quality = faceService.CheckFaceQuality(frame, faces[0]);
                    if (quality != "")
                    {
                        message = quality;
                        color = BadColor;
                    }
                    else
                    {
                        boxColor = Color.LimeGreen;
                        color = GoodColor;
                        message = (capturedImages.Count < RequiredFaces)
                            ? "Good! Please " + PoseTips[capturedImages.Count] + ", then click CAPTURE FACE."
                            : "All 5 face pictures are captured. You can save the student now.";
                    }
                }

                if (DateTime.Now >= messageHoldUntil)
                {
                    SetStatus(message, color);
                }

                Bitmap preview = new Bitmap(frame);
                using (Graphics graphics = Graphics.FromImage(preview))
                using (Pen pen = new Pen(boxColor, 3))
                {
                    foreach (Rectangle face in faces)
                    {
                        graphics.DrawRectangle(pen, face);
                    }
                }
                ShowPreview(preview);
            }
            catch (Exception)
            {
                StopCamera();
                SetStatus("Face detection stopped because of a problem. Click START CAMERA to try again.", BadColor);
            }
            finally
            {
                isBusy = false;
            }
        }

        // ---------- Capturing the 5 pictures ----------

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (!camera.IsRunning || lastFrame == null)
            {
                SetStatus("The webcam is not running. Click START CAMERA first.", BadColor);
                return;
            }

            if (capturedImages.Count >= RequiredFaces)
            {
                SetStatus("All 5 face pictures are already captured. Click RETAKE ALL FACES to start again.", GoodColor);
                return;
            }

            try
            {
                using (Bitmap frame = new Bitmap(lastFrame))
                {
                    Rectangle[] faces = faceService.DetectFaces(frame);

                    // Exactly one clear face is required
                    if (faces.Length == 0)
                    {
                        HoldMessage("No face detected. The picture was not saved.", BadColor);
                        return;
                    }
                    if (faces.Length > 1)
                    {
                        HoldMessage("Multiple faces detected. The picture was not saved.", BadColor);
                        return;
                    }

                    string quality = faceService.CheckFaceQuality(frame, faces[0]);
                    if (quality != "")
                    {
                        HoldMessage(quality + " The picture was not saved.", BadColor);
                        return;
                    }

                    capturedImages.Add(faceService.CropFaceToJpeg(frame, faces[0]));
                }

                isDirty = true;
                UpdateFaceControls();

                if (capturedImages.Count < RequiredFaces)
                {
                    HoldMessage("Face " + capturedImages.Count + " of " + RequiredFaces + " captured. Next: " +
                        PoseTips[capturedImages.Count] + ".", GoodColor);
                }
                else
                {
                    HoldMessage("Face 5 of 5 captured. You can save the student now.", GoodColor);
                }
            }
            catch (Exception ex)
            {
                SetStatus(GetFaceErrorMessage(ex), BadColor);
            }
        }

        private void btnRetake_Click(object sender, EventArgs e)
        {
            if (capturedImages.Count > 0)
            {
                DialogResult answer = MessageBox.Show("Delete the captured face pictures and capture them again?",
                    "Retake Faces", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer != DialogResult.Yes)
                {
                    return;
                }
            }

            capturedImages.Clear();
            messageHoldUntil = DateTime.MinValue;
            UpdateFaceControls();
            SetStatus("The face pictures were cleared. Look straight at the camera and capture face 1.", InfoColor);
        }

        // ---------- Saving ----------

        private void btnSave_Click(object sender, EventArgs e)
        {
            Student student = ReadAndCheckForm();
            if (student == null)
            {
                return;
            }

            // The face registration is required
            if (needsFaces && capturedImages.Count != RequiredFaces)
            {
                MessageBox.Show("Face registration is not complete.\n\n" + capturedImages.Count + " of " + RequiredFaces +
                    " face pictures were captured. Please capture all 5 face pictures before saving.",
                    "Face Registration Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Duplicate checks
                if (StudentService.StudentNumberExists(student.StudentNumber, studentId))
                {
                    ShowWarning("Another student already has the ID number " + student.StudentNumber + ".");
                    txtNumber.Focus();
                    return;
                }

                if (StudentService.EmailExists(student.Email, studentId))
                {
                    ShowWarning("Another student already uses this email address.");
                    txtEmail.Focus();
                    return;
                }

                if (StudentService.FullNameExists(student, studentId))
                {
                    DialogResult answer = MessageBox.Show(
                        "A student with the same name already exists. Do you want to save this student anyway?",
                        "Same Name Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer != DialogResult.Yes)
                    {
                        return;
                    }
                }

                Cursor = Cursors.WaitCursor;
                if (studentId == 0)
                {
                    StudentService.AddStudent(student, capturedImages);
                }
                else
                {
                    StudentService.UpdateStudent(student, needsFaces ? capturedImages : null);
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Save Student",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            isSaved = true;
            MessageBox.Show("Student " + student.FullName + " was saved successfully.", "Save Student",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            NavigationHelper.OpenPage(this, new StudentRecords());
        }

        // Reads the boxes, checks every field and returns the student.
        // Returns null (after showing a message) when something is wrong.
        private Student ReadAndCheckForm()
        {
            string number = txtNumber.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string contact = txtContact.Text.Trim();

            if (Validator.IsEmpty(number) || Validator.IsEmpty(firstName) || Validator.IsEmpty(lastName) ||
                cmbDepartment.SelectedIndex < 0 || cmbYear.SelectedIndex < 0 || cmbGender.SelectedIndex < 0 ||
                cmbStatus.SelectedIndex < 0)
            {
                ShowWarning("Please fill in all fields marked with *.");
                return null;
            }

            if (!Validator.IsValidStudentNumber(number))
            {
                ShowWarning("The ID number may only contain letters, numbers and hyphens (3 to 20 characters), for example 2026-001.");
                txtNumber.Focus();
                return null;
            }

            if (!Validator.IsValidName(firstName) || !Validator.IsValidName(lastName) ||
                (middleName.Length > 0 && !Validator.IsValidName(middleName)))
            {
                ShowWarning("Names may only contain letters, spaces, dots, hyphens and apostrophes.");
                return null;
            }

            if (email.Length > 0 && !Validator.IsValidEmail(email))
            {
                ShowWarning("Please enter a valid email address, for example juan@example.com.");
                txtEmail.Focus();
                return null;
            }

            if (contact.Length > 0 && !Validator.IsValidPhone(contact))
            {
                ShowWarning("Please enter a valid contact number (7 to 15 digits), for example 09123456789.");
                txtContact.Focus();
                return null;
            }

            Student student = new Student();
            student.StudentId = studentId;
            student.StudentNumber = number;
            student.FirstName = firstName;
            student.MiddleName = middleName;
            student.LastName = lastName;
            student.DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
            student.YearLevel = cmbYear.Text;
            student.Gender = cmbGender.Text;
            student.Email = email;
            student.ContactNumber = contact;
            student.Status = cmbStatus.Text;
            return student;
        }

        // ---------- Reset / Cancel / Closing ----------

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (HasUnsavedChanges())
            {
                DialogResult answer = MessageBox.Show("Clear everything you entered and captured?",
                    "Reset Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer != DialogResult.Yes)
                {
                    return;
                }
            }

            capturedImages.Clear();
            messageHoldUntil = DateTime.MinValue;

            try
            {
                if (studentId == 0)
                {
                    ClearForm();
                }
                else
                {
                    FillFormFromDatabase();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.GetFriendlyMessage(ex), "Reset Form",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateFaceControls();
            if (needsFaces && !camera.IsRunning)
            {
                StartCamera();
            }
            else if (!needsFaces)
            {
                StopCamera();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // The "unsaved changes" question is asked in Registration_FormClosing
            NavigationHelper.OpenPage(this, new StudentRecords());
        }

        private bool HasUnsavedChanges()
        {
            return isDirty || capturedImages.Count > 0;
        }

        private void Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSaved || !HasUnsavedChanges() || e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            DialogResult answer = MessageBox.Show("You have unsaved changes. Leave this page without saving?",
                "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (answer != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        // Always turn the webcam off when the window closes.
        private void Registration_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerCamera.Stop();
            camera.Dispose();

            if (faceService != null)
            {
                faceService.Dispose();
                faceService = null;
            }
            if (lastFrame != null)
            {
                lastFrame.Dispose();
                lastFrame = null;
            }
        }

        // ---------- Updating the screen ----------

        // Updates the 5 thumbnails, the progress text, the progress bar and the buttons.
        private void UpdateFaceControls()
        {
            for (int i = 0; i < faceBoxes.Length; i++)
            {
                if (faceBoxes[i].Image != null)
                {
                    faceBoxes[i].Image.Dispose();
                    faceBoxes[i].Image = null;
                }

                if (needsFaces && i < capturedImages.Count)
                {
                    faceBoxes[i].Image = ImageHelper.BytesToBitmap(capturedImages[i]);
                    hintLabels[i].Text = hintTexts[i] + "\nCaptured";
                    hintLabels[i].ForeColor = GoodColor;
                }
                else if (!needsFaces && i < savedImages.Count)
                {
                    faceBoxes[i].Image = ImageHelper.BytesToBitmap(savedImages[i]);
                    hintLabels[i].Text = hintTexts[i] + "\nSaved";
                    hintLabels[i].ForeColor = GoodColor;
                }
                else
                {
                    hintLabels[i].Text = hintTexts[i];
                    hintLabels[i].ForeColor = Color.DimGray;
                }
            }

            if (needsFaces)
            {
                lblProgress.Text = "Face " + capturedImages.Count + " of " + RequiredFaces + " captured";
                progressFaces.Value = capturedImages.Count;
            }
            else
            {
                lblProgress.Text = "Face " + savedImages.Count + " of " + RequiredFaces + " saved";
                progressFaces.Value = Math.Min(savedImages.Count, RequiredFaces);
                SetStatus("The saved face pictures will be kept. Tick \"Re-register face images\" to change them.", InfoColor);
            }

            btnStartCamera.Enabled = needsFaces && !camera.IsRunning;
            btnCapture.Enabled = needsFaces && camera.IsRunning && capturedImages.Count < RequiredFaces;
            btnRetake.Enabled = needsFaces && capturedImages.Count > 0;
        }

        private void ShowPreview(Bitmap preview)
        {
            Image oldImage = picPreview.Image;
            picPreview.Image = preview;
            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }

        private void SetStatus(string message, Color color)
        {
            lblInstruction.Text = message;
            lblInstruction.ForeColor = color;
        }

        // Shows a message that the camera timer will not replace for 2 seconds.
        private void HoldMessage(string message, Color color)
        {
            SetStatus(message, color);
            messageHoldUntil = DateTime.Now.AddSeconds(2);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Student Registration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Turns a face-library error into a message a normal user can understand.
        private static string GetFaceErrorMessage(Exception ex)
        {
            if (ex is System.IO.FileNotFoundException || ex is System.IO.InvalidDataException)
            {
                return ex.Message;
            }

            return "Face detection could not be started. Please install the Microsoft Visual C++ Redistributable (x64) " +
                   "and check the troubleshooting section of README.md.";
        }

        private void progressFaces_Click(object sender, EventArgs e)
        {

        }
    }
}
