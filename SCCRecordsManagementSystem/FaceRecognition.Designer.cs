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
            this.timerScan = new System.Windows.Forms.Timer(this.components);
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblAttendanceStatus = new System.Windows.Forms.Label();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.lblSessionCount = new System.Windows.Forms.Label();
            this.lblFaceDataInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFaceLogout = new System.Windows.Forms.Button();
            this.btnFaceDash = new System.Windows.Forms.Button();
            this.btnFaceRules = new System.Windows.Forms.Button();
            this.btnFaceObli = new System.Windows.Forms.Button();
            this.btnFaceReports = new System.Windows.Forms.Button();
            this.btnFaceAttendance = new System.Windows.Forms.Button();
            this.btnFaceEvent = new System.Windows.Forms.Button();
            this.btnFace = new System.Windows.Forms.Button();
            this.btnFaceOfficer = new System.Windows.Forms.Button();
            this.btnFaceStudent = new System.Windows.Forms.Button();
            this.btnFaceDept = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlCameraStatus = new System.Windows.Forms.Panel();
            this.lblCameraStatus = new System.Windows.Forms.Label();
            this.pnlSession = new System.Windows.Forms.Panel();
            this.cmbSession = new System.Windows.Forms.ComboBox();
            this.lblSession = new System.Windows.Forms.Label();
            this.pnlCamera = new System.Windows.Forms.Panel();
            this.picCameraPreview = new System.Windows.Forms.PictureBox();
            this.pnlStudentCard = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblYearLevel = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblIDNumber = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblTimeTitle = new System.Windows.Forms.Label();
            this.lblYearTitle = new System.Windows.Forms.Label();
            this.lblDepartmentTitle = new System.Windows.Forms.Label();
            this.lblIdTitle = new System.Windows.Forms.Label();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.picStudentFace = new System.Windows.Forms.PictureBox();
            this.btnNextPerson = new System.Windows.Forms.Button();
            this.btnRestartCamera = new System.Windows.Forms.Button();
            this.cmbInOut = new System.Windows.Forms.ComboBox();
            this.lblInOut = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlCameraStatus.SuspendLayout();
            this.pnlSession.SuspendLayout();
            this.pnlCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCameraPreview)).BeginInit();
            this.pnlStudentCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStudentFace)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.panel1.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._904439575_1911191946735306_1039497458727327980_n1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnFaceLogout);
            this.panel1.Controls.Add(this.btnFaceDash);
            this.panel1.Controls.Add(this.btnFaceRules);
            this.panel1.Controls.Add(this.btnFaceObli);
            this.panel1.Controls.Add(this.btnFaceReports);
            this.panel1.Controls.Add(this.btnFaceAttendance);
            this.panel1.Controls.Add(this.btnFaceEvent);
            this.panel1.Controls.Add(this.btnFace);
            this.panel1.Controls.Add(this.btnFaceOfficer);
            this.panel1.Controls.Add(this.btnFaceStudent);
            this.panel1.Controls.Add(this.btnFaceDept);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 633);
            this.panel1.TabIndex = 27;
            // 
            // btnFaceLogout
            // 
            this.btnFaceLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceLogout.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(75)))), ((int)(((byte)(80)))));
            this.btnFaceLogout.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_log_out_24__2_;
            this.btnFaceLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceLogout.Location = new System.Drawing.Point(22, 560);
            this.btnFaceLogout.Name = "btnFaceLogout";
            this.btnFaceLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceLogout.Click += new System.EventHandler(this.btnFaceLogout_Click);
            this.btnFaceLogout.Size = new System.Drawing.Size(277, 38);
            this.btnFaceLogout.TabIndex = 11;
            this.btnFaceLogout.Text = "   LogOut";
            this.btnFaceLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceLogout.UseVisualStyleBackColor = false;
            // 
            // btnFaceDash
            // 
            this.btnFaceDash.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceDash.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceDash.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceDash.Image = global::SCCRecordsManagementSystem.Properties.Resources.house__1_;
            this.btnFaceDash.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceDash.Location = new System.Drawing.Point(22, 123);
            this.btnFaceDash.Name = "btnFaceDash";
            this.btnFaceDash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceDash.Click += new System.EventHandler(this.btnFaceDash_Click);
            this.btnFaceDash.Size = new System.Drawing.Size(277, 36);
            this.btnFaceDash.TabIndex = 1;
            this.btnFaceDash.Text = "   Dashboard";
            this.btnFaceDash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceDash.UseVisualStyleBackColor = false;
            // 
            // btnFaceRules
            // 
            this.btnFaceRules.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceRules.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceRules.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceRules.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_rules_book_24;
            this.btnFaceRules.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceRules.Location = new System.Drawing.Point(22, 465);
            this.btnFaceRules.Name = "btnFaceRules";
            this.btnFaceRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceRules.Click += new System.EventHandler(this.btnFaceRules_Click);
            this.btnFaceRules.Size = new System.Drawing.Size(277, 36);
            this.btnFaceRules.TabIndex = 10;
            this.btnFaceRules.Text = "   Rules and Permission";
            this.btnFaceRules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceRules.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceRules.UseVisualStyleBackColor = false;
            // 
            // btnFaceObli
            // 
            this.btnFaceObli.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceObli.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceObli.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceObli.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceObli.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_user_24;
            this.btnFaceObli.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceObli.Location = new System.Drawing.Point(22, 427);
            this.btnFaceObli.Name = "btnFaceObli";
            this.btnFaceObli.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceObli.Click += new System.EventHandler(this.btnFaceObli_Click);
            this.btnFaceObli.Size = new System.Drawing.Size(277, 36);
            this.btnFaceObli.TabIndex = 9;
            this.btnFaceObli.Text = "Obligations";
            this.btnFaceObli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceObli.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceObli.UseVisualStyleBackColor = false;
            // 
            // btnFaceReports
            // 
            this.btnFaceReports.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceReports.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceReports.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceReports.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_analysis_24;
            this.btnFaceReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceReports.Location = new System.Drawing.Point(22, 389);
            this.btnFaceReports.Name = "btnFaceReports";
            this.btnFaceReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceReports.Click += new System.EventHandler(this.btnFaceReports_Click);
            this.btnFaceReports.Size = new System.Drawing.Size(277, 36);
            this.btnFaceReports.TabIndex = 8;
            this.btnFaceReports.Text = "   Reports and Analysis";
            this.btnFaceReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceReports.UseVisualStyleBackColor = false;
            // 
            // btnFaceAttendance
            // 
            this.btnFaceAttendance.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceAttendance.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceAttendance.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceAttendance.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_pass_fail_24;
            this.btnFaceAttendance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceAttendance.Location = new System.Drawing.Point(22, 351);
            this.btnFaceAttendance.Name = "btnFaceAttendance";
            this.btnFaceAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceAttendance.Click += new System.EventHandler(this.btnFaceAttendance_Click);
            this.btnFaceAttendance.Size = new System.Drawing.Size(277, 36);
            this.btnFaceAttendance.TabIndex = 7;
            this.btnFaceAttendance.Text = "   Attendance Monitoring";
            this.btnFaceAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceAttendance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceAttendance.UseVisualStyleBackColor = false;
            // 
            // btnFaceEvent
            // 
            this.btnFaceEvent.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceEvent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceEvent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceEvent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_event_accepted_tentatively_24;
            this.btnFaceEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceEvent.Location = new System.Drawing.Point(22, 313);
            this.btnFaceEvent.Name = "btnFaceEvent";
            this.btnFaceEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceEvent.Click += new System.EventHandler(this.btnFaceEvent_Click);
            this.btnFaceEvent.Size = new System.Drawing.Size(277, 36);
            this.btnFaceEvent.TabIndex = 6;
            this.btnFaceEvent.Text = "   Event Management";
            this.btnFaceEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceEvent.UseVisualStyleBackColor = false;
            // 
            // btnFace
            // 
            this.btnFace.BackColor = System.Drawing.Color.Transparent;
            this.btnFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFace.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFace.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFace.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_facial_recognition_24;
            this.btnFace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFace.Location = new System.Drawing.Point(22, 275);
            this.btnFace.Name = "btnFace";
            this.btnFace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFace.Click += new System.EventHandler(this.btnFace_Click);
            this.btnFace.Size = new System.Drawing.Size(277, 36);
            this.btnFace.TabIndex = 5;
            this.btnFace.Text = "   Face Recognition";
            this.btnFace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFace.UseVisualStyleBackColor = false;
            // 
            // btnFaceOfficer
            // 
            this.btnFaceOfficer.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceOfficer.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceOfficer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceOfficer.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_management_24;
            this.btnFaceOfficer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceOfficer.Location = new System.Drawing.Point(22, 237);
            this.btnFaceOfficer.Name = "btnFaceOfficer";
            this.btnFaceOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceOfficer.Click += new System.EventHandler(this.btnFaceOfficer_Click);
            this.btnFaceOfficer.Size = new System.Drawing.Size(277, 36);
            this.btnFaceOfficer.TabIndex = 4;
            this.btnFaceOfficer.Text = "   Officer Records";
            this.btnFaceOfficer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceOfficer.UseVisualStyleBackColor = false;
            // 
            // btnFaceStudent
            // 
            this.btnFaceStudent.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceStudent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceStudent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceStudent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_what_i_do_241;
            this.btnFaceStudent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceStudent.Location = new System.Drawing.Point(22, 199);
            this.btnFaceStudent.Name = "btnFaceStudent";
            this.btnFaceStudent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceStudent.Click += new System.EventHandler(this.btnFaceStudent_Click);
            this.btnFaceStudent.Size = new System.Drawing.Size(277, 36);
            this.btnFaceStudent.TabIndex = 3;
            this.btnFaceStudent.Text = "   Student Records";
            this.btnFaceStudent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceStudent.UseVisualStyleBackColor = false;
            // 
            // btnFaceDept
            // 
            this.btnFaceDept.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceDept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceDept.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceDept.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceDept.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_building_24__1_;
            this.btnFaceDept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceDept.Location = new System.Drawing.Point(22, 161);
            this.btnFaceDept.Name = "btnFaceDept";
            this.btnFaceDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceDept.Click += new System.EventHandler(this.btnFaceDept_Click);
            this.btnFaceDept.Size = new System.Drawing.Size(277, 36);
            this.btnFaceDept.TabIndex = 2;
            this.btnFaceDept.Text = "   Departments";
            this.btnFaceDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceDept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceDept.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.lblTitle);
            this.panel5.Location = new System.Drawing.Point(326, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(979, 48);
            this.panel5.TabIndex = 28;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(68, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(151, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Face Recognition";
            // 
            // pnlCameraStatus
            // 
            this.pnlCameraStatus.Controls.Add(this.lblFaceDataInfo);
            this.pnlCameraStatus.Controls.Add(this.lblCameraStatus);
            this.pnlCameraStatus.Location = new System.Drawing.Point(348, 62);
            this.pnlCameraStatus.Name = "pnlCameraStatus";
            this.pnlCameraStatus.Size = new System.Drawing.Size(475, 82);
            this.pnlCameraStatus.TabIndex = 29;
            // 
            // lblCameraStatus
            // 
            this.lblCameraStatus.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCameraStatus.Location = new System.Drawing.Point(10, 4);
            this.lblCameraStatus.Name = "lblCameraStatus";
            this.lblCameraStatus.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblCameraStatus.Size = new System.Drawing.Size(455, 46);
            this.lblCameraStatus.TabIndex = 0;
            this.lblCameraStatus.Text = "Camera is off.";
            // 
            // pnlSession
            // 
            this.pnlSession.Controls.Add(this.lblSessionCount);
            this.pnlSession.Controls.Add(this.lblInOut);
            this.pnlSession.Controls.Add(this.cmbInOut);
            this.pnlSession.Controls.Add(this.cmbSession);
            this.pnlSession.Controls.Add(this.lblSession);
            this.pnlSession.Location = new System.Drawing.Point(817, 62);
            this.pnlSession.Name = "pnlSession";
            this.pnlSession.Size = new System.Drawing.Size(476, 82);
            this.pnlSession.TabIndex = 30;
            // 
            // cmbSession
            // 
            this.cmbSession.FormattingEnabled = true;
            this.cmbSession.Location = new System.Drawing.Point(80, 12);
            this.cmbSession.Name = "cmbSession";
            this.cmbSession.SelectedIndexChanged += new System.EventHandler(this.cmbSession_SelectedIndexChanged);
            this.cmbSession.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSession.Size = new System.Drawing.Size(385, 24);
            this.cmbSession.TabIndex = 2;
            // 
            // lblSession
            // 
            this.lblSession.AutoSize = true;
            this.lblSession.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSession.Location = new System.Drawing.Point(10, 15);
            this.lblSession.Name = "lblSession";
            this.lblSession.Size = new System.Drawing.Size(54, 17);
            this.lblSession.TabIndex = 1;
            this.lblSession.Text = "Session:";
            // 
            // pnlCamera
            // 
            this.pnlCamera.Controls.Add(this.picCameraPreview);
            this.pnlCamera.Location = new System.Drawing.Point(350, 150);
            this.pnlCamera.Name = "pnlCamera";
            this.pnlCamera.Size = new System.Drawing.Size(461, 365);
            this.pnlCamera.TabIndex = 31;
            // 
            // picCameraPreview
            // 
            this.picCameraPreview.Location = new System.Drawing.Point(8, 8);
            this.picCameraPreview.Name = "picCameraPreview";
            this.picCameraPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCameraPreview.BackColor = System.Drawing.Color.Black;
            this.picCameraPreview.Size = new System.Drawing.Size(445, 349);
            this.picCameraPreview.TabIndex = 1;
            this.picCameraPreview.TabStop = false;
            // 
            // pnlStudentCard
            // 
            this.pnlStudentCard.Controls.Add(this.lblAttendanceStatus);
            this.pnlStudentCard.Controls.Add(this.lblStatusTitle);
            this.pnlStudentCard.Controls.Add(this.lblCardTitle);
            this.pnlStudentCard.Controls.Add(this.lblMessage);
            this.pnlStudentCard.Controls.Add(this.lblTime);
            this.pnlStudentCard.Controls.Add(this.lblYearLevel);
            this.pnlStudentCard.Controls.Add(this.lblDepartment);
            this.pnlStudentCard.Controls.Add(this.lblIDNumber);
            this.pnlStudentCard.Controls.Add(this.lblFullName);
            this.pnlStudentCard.Controls.Add(this.lblTimeTitle);
            this.pnlStudentCard.Controls.Add(this.lblYearTitle);
            this.pnlStudentCard.Controls.Add(this.lblDepartmentTitle);
            this.pnlStudentCard.Controls.Add(this.lblIdTitle);
            this.pnlStudentCard.Controls.Add(this.lblNameTitle);
            this.pnlStudentCard.Controls.Add(this.picStudentFace);
            this.pnlStudentCard.Location = new System.Drawing.Point(817, 150);
            this.pnlStudentCard.Name = "pnlStudentCard";
            this.pnlStudentCard.Size = new System.Drawing.Size(476, 365);
            this.pnlStudentCard.TabIndex = 32;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(15, 288);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Size = new System.Drawing.Size(446, 70);
            this.lblMessage.TabIndex = 11;
            this.lblMessage.Text = "";
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(320, 190);
            this.lblTime.Name = "lblTime";
            this.lblTime.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Size = new System.Drawing.Size(145, 22);
            this.lblTime.TabIndex = 10;
            this.lblTime.Text = "-";
            // 
            // lblYearLevel
            // 
            this.lblYearLevel.Location = new System.Drawing.Point(320, 160);
            this.lblYearLevel.Name = "lblYearLevel";
            this.lblYearLevel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearLevel.Size = new System.Drawing.Size(145, 22);
            this.lblYearLevel.TabIndex = 9;
            this.lblYearLevel.Text = "-";
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(320, 130);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.Size = new System.Drawing.Size(145, 22);
            this.lblDepartment.TabIndex = 8;
            this.lblDepartment.Text = "-";
            // 
            // lblIDNumber
            // 
            this.lblIDNumber.Location = new System.Drawing.Point(320, 100);
            this.lblIDNumber.Name = "lblIDNumber";
            this.lblIDNumber.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIDNumber.Size = new System.Drawing.Size(145, 22);
            this.lblIDNumber.TabIndex = 7;
            this.lblIDNumber.Text = "-";
            // 
            // lblFullName
            // 
            this.lblFullName.Location = new System.Drawing.Point(320, 50);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.Size = new System.Drawing.Size(145, 46);
            this.lblFullName.TabIndex = 6;
            this.lblFullName.Text = "-";
            // 
            // lblTimeTitle
            // 
            this.lblTimeTitle.AutoSize = true;
            this.lblTimeTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeTitle.Location = new System.Drawing.Point(225, 190);
            this.lblTimeTitle.Name = "lblTimeTitle";
            this.lblTimeTitle.Size = new System.Drawing.Size(48, 17);
            this.lblTimeTitle.TabIndex = 5;
            this.lblTimeTitle.Text = "Time:";
            // 
            // lblYearTitle
            // 
            this.lblYearTitle.AutoSize = true;
            this.lblYearTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearTitle.Location = new System.Drawing.Point(225, 160);
            this.lblYearTitle.Name = "lblYearTitle";
            this.lblYearTitle.Size = new System.Drawing.Size(83, 17);
            this.lblYearTitle.TabIndex = 4;
            this.lblYearTitle.Text = "Year Level:";
            // 
            // lblDepartmentTitle
            // 
            this.lblDepartmentTitle.AutoSize = true;
            this.lblDepartmentTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentTitle.Location = new System.Drawing.Point(225, 130);
            this.lblDepartmentTitle.Name = "lblDepartmentTitle";
            this.lblDepartmentTitle.Size = new System.Drawing.Size(95, 17);
            this.lblDepartmentTitle.TabIndex = 3;
            this.lblDepartmentTitle.Text = "Department:";
            // 
            // lblIdTitle
            // 
            this.lblIdTitle.AutoSize = true;
            this.lblIdTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdTitle.Location = new System.Drawing.Point(225, 100);
            this.lblIdTitle.Name = "lblIdTitle";
            this.lblIdTitle.Size = new System.Drawing.Size(87, 17);
            this.lblIdTitle.TabIndex = 2;
            this.lblIdTitle.Text = "ID Number:";
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTitle.Location = new System.Drawing.Point(225, 50);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(81, 17);
            this.lblNameTitle.TabIndex = 1;
            this.lblNameTitle.Text = "Full Name:";
            // 
            // picStudentFace
            // 
            this.picStudentFace.Location = new System.Drawing.Point(15, 40);
            this.picStudentFace.Name = "picStudentFace";
            this.picStudentFace.BackColor = System.Drawing.Color.White;
            this.picStudentFace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picStudentFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStudentFace.Size = new System.Drawing.Size(200, 240);
            this.picStudentFace.TabIndex = 0;
            this.picStudentFace.TabStop = false;
            // 
            // btnNextPerson
            // 
            this.btnNextPerson.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextPerson.Location = new System.Drawing.Point(450, 532);
            this.btnNextPerson.Name = "btnNextPerson";
            this.btnNextPerson.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextPerson.Click += new System.EventHandler(this.btnNextPerson_Click);
            this.btnNextPerson.Size = new System.Drawing.Size(253, 45);
            this.btnNextPerson.TabIndex = 33;
            this.btnNextPerson.Text = "NEXT PERSON";
            this.btnNextPerson.UseVisualStyleBackColor = true;
            // 
            // btnRestartCamera
            // 
            this.btnRestartCamera.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestartCamera.Location = new System.Drawing.Point(936, 532);
            this.btnRestartCamera.Name = "btnRestartCamera";
            this.btnRestartCamera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestartCamera.Click += new System.EventHandler(this.btnRestartCamera_Click);
            this.btnRestartCamera.Size = new System.Drawing.Size(253, 45);
            this.btnRestartCamera.TabIndex = 34;
            this.btnRestartCamera.Text = "RESTART / RELOAD";
            this.btnRestartCamera.UseVisualStyleBackColor = true;
            // 
            // cmbInOut
            // 
            this.cmbInOut.FormattingEnabled = true;
            this.cmbInOut.Location = new System.Drawing.Point(80, 46);
            this.cmbInOut.Name = "cmbInOut";
            this.cmbInOut.SelectedIndexChanged += new System.EventHandler(this.cmbInOut_SelectedIndexChanged);
            this.cmbInOut.Items.AddRange(new object[] {
            "Log In",
            "Log Out"});
            this.cmbInOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInOut.Size = new System.Drawing.Size(120, 24);
            this.cmbInOut.TabIndex = 1;
            // 
            // lblInOut
            // 
            this.lblInOut.AutoSize = true;
            this.lblInOut.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInOut.Location = new System.Drawing.Point(10, 49);
            this.lblInOut.Name = "lblInOut";
            this.lblInOut.Size = new System.Drawing.Size(58, 17);
            this.lblInOut.TabIndex = 3;
            this.lblInOut.Text = "Action:";
            // 
            // lblFaceDataInfo
            // 
            this.lblFaceDataInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblFaceDataInfo.Font = new System.Drawing.Font("Cambria", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaceDataInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblFaceDataInfo.Location = new System.Drawing.Point(10, 52);
            this.lblFaceDataInfo.Name = "lblFaceDataInfo";
            this.lblFaceDataInfo.Size = new System.Drawing.Size(455, 24);
            this.lblFaceDataInfo.TabIndex = 0;
            this.lblFaceDataInfo.Text = "";
            // 
            // lblSessionCount
            // 
            this.lblSessionCount.BackColor = System.Drawing.Color.Transparent;
            this.lblSessionCount.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionCount.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblSessionCount.Location = new System.Drawing.Point(210, 48);
            this.lblSessionCount.Name = "lblSessionCount";
            this.lblSessionCount.Size = new System.Drawing.Size(255, 22);
            this.lblSessionCount.TabIndex = 0;
            this.lblSessionCount.Text = "";
            this.lblSessionCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCardTitle
            // 
            this.lblCardTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCardTitle.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblCardTitle.Location = new System.Drawing.Point(15, 8);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(446, 26);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "Waiting for a student...";
            this.lblCardTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusTitle.Location = new System.Drawing.Point(225, 220);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(90, 20);
            this.lblStatusTitle.TabIndex = 0;
            this.lblStatusTitle.Text = "Status:";
            // 
            // lblAttendanceStatus
            // 
            this.lblAttendanceStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblAttendanceStatus.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttendanceStatus.Location = new System.Drawing.Point(320, 220);
            this.lblAttendanceStatus.Name = "lblAttendanceStatus";
            this.lblAttendanceStatus.Size = new System.Drawing.Size(145, 22);
            this.lblAttendanceStatus.TabIndex = 0;
            this.lblAttendanceStatus.Text = "-";
            // 
            // lblHelp
            // 
            this.lblHelp.BackColor = System.Drawing.Color.Transparent;
            this.lblHelp.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelp.ForeColor = System.Drawing.Color.DimGray;
            this.lblHelp.Location = new System.Drawing.Point(350, 585);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(943, 24);
            this.lblHelp.TabIndex = 0;
            this.lblHelp.Text = "Tip: one student at a time. Look straight at the camera until the box turns GREEN. Press NEXT PERSON to clear the card.";
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerScan
            // 
            this.timerScan.Interval = 100;
            this.timerScan.Tick += new System.EventHandler(this.timerScan_Tick);
            // 
            // FaceRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._891752403_1143943308365188_7410099486200290872_n2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.btnRestartCamera);
            this.Controls.Add(this.btnNextPerson);
            this.Controls.Add(this.pnlStudentCard);
            this.Controls.Add(this.pnlCamera);
            this.Controls.Add(this.pnlSession);
            this.Controls.Add(this.pnlCameraStatus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FaceRecognition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCC Records Management System - Face Recognition Attendance";
            this.Shown += new System.EventHandler(this.FaceRecognition_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FaceRecognition_FormClosed);
            this.Load += new System.EventHandler(this.FaceRecognition_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlCameraStatus.ResumeLayout(false);
            this.pnlCameraStatus.PerformLayout();
            this.pnlSession.ResumeLayout(false);
            this.pnlSession.PerformLayout();
            this.pnlCamera.ResumeLayout(false);
            this.pnlCamera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCameraPreview)).EndInit();
            this.pnlStudentCard.ResumeLayout(false);
            this.pnlStudentCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStudentFace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerScan;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblAttendanceStatus;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label lblSessionCount;
        private System.Windows.Forms.Label lblFaceDataInfo;

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFaceLogout;
        private System.Windows.Forms.Button btnFaceDash;
        private System.Windows.Forms.Button btnFaceRules;
        private System.Windows.Forms.Button btnFaceObli;
        private System.Windows.Forms.Button btnFaceReports;
        private System.Windows.Forms.Button btnFaceAttendance;
        private System.Windows.Forms.Button btnFaceEvent;
        private System.Windows.Forms.Button btnFace;
        private System.Windows.Forms.Button btnFaceOfficer;
        private System.Windows.Forms.Button btnFaceStudent;
        private System.Windows.Forms.Button btnFaceDept;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlCameraStatus;
        private System.Windows.Forms.Label lblCameraStatus;
        private System.Windows.Forms.Panel pnlSession;
        private System.Windows.Forms.Label lblSession;
        private System.Windows.Forms.ComboBox cmbSession;
        private System.Windows.Forms.Panel pnlCamera;
        private System.Windows.Forms.Panel pnlStudentCard;
        private System.Windows.Forms.Label lblTimeTitle;
        private System.Windows.Forms.Label lblYearTitle;
        private System.Windows.Forms.Label lblDepartmentTitle;
        private System.Windows.Forms.Label lblIdTitle;
        private System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.PictureBox picStudentFace;
        private System.Windows.Forms.Button btnNextPerson;
        private System.Windows.Forms.Button btnRestartCamera;
        private System.Windows.Forms.PictureBox picCameraPreview;
        private System.Windows.Forms.Label lblYearLevel;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label lblIDNumber;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblInOut;
        private System.Windows.Forms.ComboBox cmbInOut;
    }
}