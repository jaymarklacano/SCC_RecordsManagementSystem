namespace SCCRecordsManagementSystem
{
    partial class FaceRecognition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.sidebarMenu = new SidebarMenu();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.windowButtons = new WindowButtons();
            this.pnlCameraStatus = new System.Windows.Forms.Panel();
            this.lblCameraStatus = new System.Windows.Forms.Label();
            this.pnlResultBanner = new System.Windows.Forms.Panel();
            this.lblBanner = new System.Windows.Forms.Label();
            this.pnlCamera = new System.Windows.Forms.Panel();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.picStudent = new System.Windows.Forms.PictureBox();
            this.lblInfoTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblStudentNumberCaption = new System.Windows.Forms.Label();
            this.lblStudentNumber = new System.Windows.Forms.Label();
            this.lblCourseCaption = new System.Windows.Forms.Label();
            this.lblCourse = new System.Windows.Forms.Label();
            this.lblYearCaption = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblStatusCaption = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTimeCaption = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblResultMessage = new System.Windows.Forms.Label();
            this.lblFaceStats = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnNextStudent = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.timerCamera = new System.Windows.Forms.Timer(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlCameraStatus.SuspendLayout();
            this.pnlResultBanner.SuspendLayout();
            this.pnlCamera.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarMenu
            // 
            this.sidebarMenu.Location = new System.Drawing.Point(0, 0);
            this.sidebarMenu.Size = new System.Drawing.Size(320, 619);
            this.sidebarMenu.CurrentPage = SCCRecordsManagementSystem.SidebarPage.FaceRecognition;
            this.sidebarMenu.Name = "sidebarMenu";
            this.sidebarMenu.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            this.pnlHeader.Location = new System.Drawing.Point(320, 0);
            this.pnlHeader.Size = new System.Drawing.Size(985, 50);
            this.pnlHeader.Controls.Add(this.lblPageTitle);
            this.pnlHeader.Controls.Add(this.windowButtons);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.TabIndex = 1;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblPageTitle.Location = new System.Drawing.Point(24, 13);
            this.lblPageTitle.Text = "Face Recognition Attendance";
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.TabIndex = 0;
            // 
            // windowButtons
            // 
            this.windowButtons.Location = new System.Drawing.Point(891, 8);
            this.windowButtons.Size = new System.Drawing.Size(90, 34);
            this.windowButtons.Name = "windowButtons";
            this.windowButtons.TabIndex = 1;
            // 
            // pnlCameraStatus
            // 
            this.pnlCameraStatus.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlCameraStatus.Location = new System.Drawing.Point(348, 62);
            this.pnlCameraStatus.Size = new System.Drawing.Size(461, 82);
            this.pnlCameraStatus.Controls.Add(this.lblCameraStatus);
            this.pnlCameraStatus.Name = "pnlCameraStatus";
            this.pnlCameraStatus.TabIndex = 2;
            // 
            // lblCameraStatus
            // 
            this.lblCameraStatus.AutoSize = false;
            this.lblCameraStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblCameraStatus.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCameraStatus.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblCameraStatus.Location = new System.Drawing.Point(12, 8);
            this.lblCameraStatus.Text = "Starting...";
            this.lblCameraStatus.Size = new System.Drawing.Size(437, 66);
            this.lblCameraStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCameraStatus.Name = "lblCameraStatus";
            this.lblCameraStatus.TabIndex = 0;
            // 
            // pnlResultBanner
            // 
            this.pnlResultBanner.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.pnlResultBanner.Location = new System.Drawing.Point(817, 62);
            this.pnlResultBanner.Size = new System.Drawing.Size(468, 82);
            this.pnlResultBanner.Controls.Add(this.lblBanner);
            this.pnlResultBanner.Name = "pnlResultBanner";
            this.pnlResultBanner.TabIndex = 3;
            // 
            // lblBanner
            // 
            this.lblBanner.AutoSize = false;
            this.lblBanner.BackColor = System.Drawing.Color.Transparent;
            this.lblBanner.Font = new System.Drawing.Font("Cambria", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanner.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblBanner.Location = new System.Drawing.Point(0, 0);
            this.lblBanner.Text = "WAITING FOR A STUDENT";
            this.lblBanner.Size = new System.Drawing.Size(468, 82);
            this.lblBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBanner.Name = "lblBanner";
            this.lblBanner.TabIndex = 0;
            // 
            // pnlCamera
            // 
            this.pnlCamera.BackColor = System.Drawing.Color.Black;
            this.pnlCamera.Location = new System.Drawing.Point(348, 150);
            this.pnlCamera.Size = new System.Drawing.Size(461, 346);
            this.pnlCamera.Controls.Add(this.picCamera);
            this.pnlCamera.Name = "pnlCamera";
            this.pnlCamera.TabIndex = 4;
            // 
            // picCamera
            // 
            this.picCamera.BackColor = System.Drawing.Color.Black;
            this.picCamera.Location = new System.Drawing.Point(0, 0);
            this.picCamera.Size = new System.Drawing.Size(461, 346);
            this.picCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCamera.Name = "picCamera";
            this.picCamera.TabIndex = 0;
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlInfo.Location = new System.Drawing.Point(817, 150);
            this.pnlInfo.Size = new System.Drawing.Size(468, 346);
            this.pnlInfo.Controls.Add(this.picStudent);
            this.pnlInfo.Controls.Add(this.lblInfoTitle);
            this.pnlInfo.Controls.Add(this.lblName);
            this.pnlInfo.Controls.Add(this.lblStudentNumberCaption);
            this.pnlInfo.Controls.Add(this.lblStudentNumber);
            this.pnlInfo.Controls.Add(this.lblCourseCaption);
            this.pnlInfo.Controls.Add(this.lblCourse);
            this.pnlInfo.Controls.Add(this.lblYearCaption);
            this.pnlInfo.Controls.Add(this.lblYear);
            this.pnlInfo.Controls.Add(this.lblStatusCaption);
            this.pnlInfo.Controls.Add(this.lblStatus);
            this.pnlInfo.Controls.Add(this.lblTimeCaption);
            this.pnlInfo.Controls.Add(this.lblTime);
            this.pnlInfo.Controls.Add(this.lblResultMessage);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.TabIndex = 5;
            // 
            // picStudent
            // 
            this.picStudent.BackColor = System.Drawing.Color.White;
            this.picStudent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picStudent.Location = new System.Drawing.Point(20, 16);
            this.picStudent.Size = new System.Drawing.Size(170, 210);
            this.picStudent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStudent.Name = "picStudent";
            this.picStudent.TabIndex = 0;
            // 
            // lblInfoTitle
            // 
            this.lblInfoTitle.AutoSize = true;
            this.lblInfoTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoTitle.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblInfoTitle.Location = new System.Drawing.Point(205, 16);
            this.lblInfoTitle.Text = "Student Information";
            this.lblInfoTitle.Name = "lblInfoTitle";
            this.lblInfoTitle.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = false;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(205, 44);
            this.lblName.Text = "-";
            this.lblName.Size = new System.Drawing.Size(250, 48);
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblName.Name = "lblName";
            this.lblName.TabIndex = 2;
            // 
            // lblStudentNumberCaption
            // 
            this.lblStudentNumberCaption.AutoSize = true;
            this.lblStudentNumberCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblStudentNumberCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentNumberCaption.ForeColor = System.Drawing.Color.Black;
            this.lblStudentNumberCaption.Location = new System.Drawing.Point(205, 100);
            this.lblStudentNumberCaption.Text = "Student ID:";
            this.lblStudentNumberCaption.Name = "lblStudentNumberCaption";
            this.lblStudentNumberCaption.TabIndex = 3;
            // 
            // lblStudentNumber
            // 
            this.lblStudentNumber.AutoSize = false;
            this.lblStudentNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblStudentNumber.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentNumber.ForeColor = System.Drawing.Color.Black;
            this.lblStudentNumber.Location = new System.Drawing.Point(295, 100);
            this.lblStudentNumber.Text = "-";
            this.lblStudentNumber.Size = new System.Drawing.Size(160, 22);
            this.lblStudentNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStudentNumber.AutoEllipsis = true;
            this.lblStudentNumber.Name = "lblStudentNumber";
            this.lblStudentNumber.TabIndex = 4;
            // 
            // lblCourseCaption
            // 
            this.lblCourseCaption.AutoSize = true;
            this.lblCourseCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblCourseCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourseCaption.ForeColor = System.Drawing.Color.Black;
            this.lblCourseCaption.Location = new System.Drawing.Point(205, 128);
            this.lblCourseCaption.Text = "Course:";
            this.lblCourseCaption.Name = "lblCourseCaption";
            this.lblCourseCaption.TabIndex = 5;
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = false;
            this.lblCourse.BackColor = System.Drawing.Color.Transparent;
            this.lblCourse.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourse.ForeColor = System.Drawing.Color.Black;
            this.lblCourse.Location = new System.Drawing.Point(295, 128);
            this.lblCourse.Text = "-";
            this.lblCourse.Size = new System.Drawing.Size(160, 22);
            this.lblCourse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCourse.AutoEllipsis = true;
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.TabIndex = 6;
            // 
            // lblYearCaption
            // 
            this.lblYearCaption.AutoSize = true;
            this.lblYearCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblYearCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearCaption.ForeColor = System.Drawing.Color.Black;
            this.lblYearCaption.Location = new System.Drawing.Point(205, 156);
            this.lblYearCaption.Text = "Year Level:";
            this.lblYearCaption.Name = "lblYearCaption";
            this.lblYearCaption.TabIndex = 7;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = false;
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.ForeColor = System.Drawing.Color.Black;
            this.lblYear.Location = new System.Drawing.Point(295, 156);
            this.lblYear.Text = "-";
            this.lblYear.Size = new System.Drawing.Size(160, 22);
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblYear.AutoEllipsis = true;
            this.lblYear.Name = "lblYear";
            this.lblYear.TabIndex = 8;
            // 
            // lblStatusCaption
            // 
            this.lblStatusCaption.AutoSize = true;
            this.lblStatusCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusCaption.ForeColor = System.Drawing.Color.Black;
            this.lblStatusCaption.Location = new System.Drawing.Point(205, 184);
            this.lblStatusCaption.Text = "Status:";
            this.lblStatusCaption.Name = "lblStatusCaption";
            this.lblStatusCaption.TabIndex = 9;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(295, 184);
            this.lblStatus.Text = "-";
            this.lblStatus.Size = new System.Drawing.Size(160, 22);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 10;
            // 
            // lblTimeCaption
            // 
            this.lblTimeCaption.AutoSize = true;
            this.lblTimeCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeCaption.ForeColor = System.Drawing.Color.Black;
            this.lblTimeCaption.Location = new System.Drawing.Point(205, 212);
            this.lblTimeCaption.Text = "Time:";
            this.lblTimeCaption.Name = "lblTimeCaption";
            this.lblTimeCaption.TabIndex = 11;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = false;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.Black;
            this.lblTime.Location = new System.Drawing.Point(295, 212);
            this.lblTime.Text = "-";
            this.lblTime.Size = new System.Drawing.Size(160, 22);
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTime.AutoEllipsis = true;
            this.lblTime.Name = "lblTime";
            this.lblTime.TabIndex = 12;
            // 
            // lblResultMessage
            // 
            this.lblResultMessage.AutoSize = false;
            this.lblResultMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblResultMessage.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultMessage.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblResultMessage.Location = new System.Drawing.Point(20, 240);
            this.lblResultMessage.Text = "";
            this.lblResultMessage.Size = new System.Drawing.Size(428, 90);
            this.lblResultMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResultMessage.Name = "lblResultMessage";
            this.lblResultMessage.TabIndex = 13;
            // 
            // lblFaceStats
            // 
            this.lblFaceStats.AutoSize = false;
            this.lblFaceStats.BackColor = System.Drawing.Color.Transparent;
            this.lblFaceStats.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaceStats.ForeColor = System.Drawing.Color.DimGray;
            this.lblFaceStats.Location = new System.Drawing.Point(348, 502);
            this.lblFaceStats.Text = "";
            this.lblFaceStats.Size = new System.Drawing.Size(461, 22);
            this.lblFaceStats.Name = "lblFaceStats";
            this.lblFaceStats.TabIndex = 6;
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = false;
            this.lblHint.BackColor = System.Drawing.Color.Transparent;
            this.lblHint.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.ForeColor = System.Drawing.Color.DimGray;
            this.lblHint.Location = new System.Drawing.Point(817, 502);
            this.lblHint.Text = "Stand in front of the camera and look straight at it. Attendance is recorded automatically.";
            this.lblHint.Size = new System.Drawing.Size(468, 36);
            this.lblHint.Name = "lblHint";
            this.lblHint.TabIndex = 7;
            // 
            // btnNextStudent
            // 
            this.btnNextStudent.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            this.btnNextStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextStudent.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextStudent.ForeColor = System.Drawing.Color.White;
            this.btnNextStudent.Location = new System.Drawing.Point(450, 540);
            this.btnNextStudent.Size = new System.Drawing.Size(253, 45);
            this.btnNextStudent.Text = "NEXT STUDENT";
            this.btnNextStudent.UseVisualStyleBackColor = false;
            this.btnNextStudent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextStudent.FlatAppearance.BorderSize = 0;
            this.btnNextStudent.Name = "btnNextStudent";
            this.btnNextStudent.TabIndex = 8;
            this.btnNextStudent.Click += new System.EventHandler(this.btnNextStudent_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestart.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.ForeColor = System.Drawing.Color.Black;
            this.btnRestart.Location = new System.Drawing.Point(936, 540);
            this.btnRestart.Size = new System.Drawing.Size(253, 45);
            this.btnRestart.Text = "RESTART CAMERA";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.FlatAppearance.BorderSize = 0;
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.TabIndex = 9;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // timerCamera
            // 
            this.timerCamera.Interval = 100;
            this.timerCamera.Enabled = false;
            this.timerCamera.Tick += new System.EventHandler(this.timerCamera_Tick);
            // 
            // FaceRecognition
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FaceRecognition";
            this.Controls.Add(this.sidebarMenu);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlCameraStatus);
            this.Controls.Add(this.pnlResultBanner);
            this.Controls.Add(this.pnlCamera);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.lblFaceStats);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnNextStudent);
            this.Controls.Add(this.btnRestart);
            this.Load += new System.EventHandler(this.FaceRecognition_Load);
            this.Shown += new System.EventHandler(this.FaceRecognition_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FaceRecognition_FormClosed);
            this.Name = "FaceRecognition";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlCameraStatus.ResumeLayout(false);
            this.pnlCameraStatus.PerformLayout();
            this.pnlResultBanner.ResumeLayout(false);
            this.pnlResultBanner.PerformLayout();
            this.pnlCamera.ResumeLayout(false);
            this.pnlCamera.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SidebarMenu sidebarMenu;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private WindowButtons windowButtons;
        private System.Windows.Forms.Panel pnlCameraStatus;
        private System.Windows.Forms.Label lblCameraStatus;
        private System.Windows.Forms.Panel pnlResultBanner;
        private System.Windows.Forms.Label lblBanner;
        private System.Windows.Forms.Panel pnlCamera;
        private System.Windows.Forms.PictureBox picCamera;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.PictureBox picStudent;
        private System.Windows.Forms.Label lblInfoTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStudentNumberCaption;
        private System.Windows.Forms.Label lblStudentNumber;
        private System.Windows.Forms.Label lblCourseCaption;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.Label lblYearCaption;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblTimeCaption;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblResultMessage;
        private System.Windows.Forms.Label lblFaceStats;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnNextStudent;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Timer timerCamera;
    }
}
