namespace SCCRecordsManagementSystem
{
    partial class AttendanceMonitoring
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
            this.lblSelectedRecord = new System.Windows.Forms.Label();
            this.lblNoRecord = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.lblManualHelp = new System.Windows.Forms.Label();
            this.btnRemoveAttendance = new System.Windows.Forms.Button();
            this.btnMarkLogout = new System.Windows.Forms.Button();
            this.btnMarkPresent = new System.Windows.Forms.Button();
            this.lblManualTitle = new System.Windows.Forms.Label();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.cmbLogoutStatus = new System.Windows.Forms.ComboBox();
            this.lblLogoutFilter = new System.Windows.Forms.Label();
            this.chkPending = new System.Windows.Forms.CheckBox();
            this.txtSearchAttendance = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbSession = new System.Windows.Forms.ComboBox();
            this.lblSession = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSelectEvent = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblYearLevel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAttendLogout = new System.Windows.Forms.Button();
            this.btnAttendDash = new System.Windows.Forms.Button();
            this.btnAttendRules = new System.Windows.Forms.Button();
            this.btnAttendObligation = new System.Windows.Forms.Button();
            this.btnAttendReports = new System.Windows.Forms.Button();
            this.btnAttendMonitoring = new System.Windows.Forms.Button();
            this.btnAttendEvent = new System.Windows.Forms.Button();
            this.btnAttendFace = new System.Windows.Forms.Button();
            this.btnAttendOfficer = new System.Windows.Forms.Button();
            this.btnAttendStudent = new System.Windows.Forms.Button();
            this.btnAttendDept = new System.Windows.Forms.Button();
            this.dtgAttendanceMon = new System.Windows.Forms.DataGridView();
            this.chkPresent = new System.Windows.Forms.CheckBox();
            this.chkAbsent = new System.Windows.Forms.CheckBox();
            this.cmbDepartmentAttend = new System.Windows.Forms.ComboBox();
            this.cmbYearLevelAttend = new System.Windows.Forms.ComboBox();
            this.cmbSelectEvent = new System.Windows.Forms.ComboBox();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAttendanceMon)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.lblTitle);
            this.panel5.Location = new System.Drawing.Point(319, -1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(986, 51);
            this.panel5.TabIndex = 28;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(78, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(204, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Attendance Monitoring";
            // 
            // lblSelectEvent
            // 
            this.lblSelectEvent.AutoSize = true;
            this.lblSelectEvent.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectEvent.Location = new System.Drawing.Point(340, 64);
            this.lblSelectEvent.Name = "lblSelectEvent";
            this.lblSelectEvent.Size = new System.Drawing.Size(101, 17);
            this.lblSelectEvent.TabIndex = 29;
            this.lblSelectEvent.Text = "Event :";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(340, 140);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(57, 17);
            this.lblStatus.TabIndex = 30;
            this.lblStatus.Text = "Status :";
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.Location = new System.Drawing.Point(340, 100);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(101, 17);
            this.lblDepartment.TabIndex = 31;
            this.lblDepartment.Text = "Department : ";
            // 
            // lblYearLevel
            // 
            this.lblYearLevel.AutoSize = true;
            this.lblYearLevel.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearLevel.Location = new System.Drawing.Point(585, 100);
            this.lblYearLevel.Name = "lblYearLevel";
            this.lblYearLevel.Size = new System.Drawing.Size(86, 17);
            this.lblYearLevel.TabIndex = 32;
            this.lblYearLevel.Text = "Year Level :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.panel1.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._904439575_1911191946735306_1039497458727327980_n1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnAttendLogout);
            this.panel1.Controls.Add(this.btnAttendDash);
            this.panel1.Controls.Add(this.btnAttendRules);
            this.panel1.Controls.Add(this.btnAttendObligation);
            this.panel1.Controls.Add(this.btnAttendReports);
            this.panel1.Controls.Add(this.btnAttendMonitoring);
            this.panel1.Controls.Add(this.btnAttendEvent);
            this.panel1.Controls.Add(this.btnAttendFace);
            this.panel1.Controls.Add(this.btnAttendOfficer);
            this.panel1.Controls.Add(this.btnAttendStudent);
            this.panel1.Controls.Add(this.btnAttendDept);
            this.panel1.Location = new System.Drawing.Point(-3, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 627);
            this.panel1.TabIndex = 33;
            // 
            // btnAttendLogout
            // 
            this.btnAttendLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendLogout.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(75)))), ((int)(((byte)(80)))));
            this.btnAttendLogout.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_log_out_24__2_;
            this.btnAttendLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendLogout.Location = new System.Drawing.Point(22, 560);
            this.btnAttendLogout.Name = "btnAttendLogout";
            this.btnAttendLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendLogout.Click += new System.EventHandler(this.btnAttendLogout_Click);
            this.btnAttendLogout.Size = new System.Drawing.Size(277, 38);
            this.btnAttendLogout.TabIndex = 11;
            this.btnAttendLogout.Text = "   LogOut";
            this.btnAttendLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendLogout.UseVisualStyleBackColor = false;
            // 
            // btnAttendDash
            // 
            this.btnAttendDash.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendDash.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendDash.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendDash.Image = global::SCCRecordsManagementSystem.Properties.Resources.house__1_;
            this.btnAttendDash.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendDash.Location = new System.Drawing.Point(22, 123);
            this.btnAttendDash.Name = "btnAttendDash";
            this.btnAttendDash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendDash.Click += new System.EventHandler(this.btnAttendDash_Click);
            this.btnAttendDash.Size = new System.Drawing.Size(277, 36);
            this.btnAttendDash.TabIndex = 1;
            this.btnAttendDash.Text = "   Dashboard";
            this.btnAttendDash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendDash.UseVisualStyleBackColor = false;
            // 
            // btnAttendRules
            // 
            this.btnAttendRules.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendRules.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendRules.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendRules.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_rules_book_24;
            this.btnAttendRules.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendRules.Location = new System.Drawing.Point(22, 465);
            this.btnAttendRules.Name = "btnAttendRules";
            this.btnAttendRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendRules.Click += new System.EventHandler(this.btnAttendRules_Click);
            this.btnAttendRules.Size = new System.Drawing.Size(277, 36);
            this.btnAttendRules.TabIndex = 10;
            this.btnAttendRules.Text = "   Rules and Permission";
            this.btnAttendRules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendRules.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendRules.UseVisualStyleBackColor = false;
            // 
            // btnAttendObligation
            // 
            this.btnAttendObligation.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendObligation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendObligation.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendObligation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendObligation.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_user_24;
            this.btnAttendObligation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendObligation.Location = new System.Drawing.Point(22, 427);
            this.btnAttendObligation.Name = "btnAttendObligation";
            this.btnAttendObligation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendObligation.Click += new System.EventHandler(this.btnAttendObligation_Click);
            this.btnAttendObligation.Size = new System.Drawing.Size(277, 36);
            this.btnAttendObligation.TabIndex = 9;
            this.btnAttendObligation.Text = "Obligations";
            this.btnAttendObligation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendObligation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendObligation.UseVisualStyleBackColor = false;
            // 
            // btnAttendReports
            // 
            this.btnAttendReports.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendReports.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendReports.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendReports.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_analysis_24;
            this.btnAttendReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendReports.Location = new System.Drawing.Point(22, 389);
            this.btnAttendReports.Name = "btnAttendReports";
            this.btnAttendReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendReports.Click += new System.EventHandler(this.btnAttendReports_Click);
            this.btnAttendReports.Size = new System.Drawing.Size(277, 36);
            this.btnAttendReports.TabIndex = 8;
            this.btnAttendReports.Text = "   Reports and Analysis";
            this.btnAttendReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendReports.UseVisualStyleBackColor = false;
            // 
            // btnAttendMonitoring
            // 
            this.btnAttendMonitoring.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendMonitoring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendMonitoring.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendMonitoring.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendMonitoring.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_pass_fail_24;
            this.btnAttendMonitoring.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendMonitoring.Location = new System.Drawing.Point(22, 351);
            this.btnAttendMonitoring.Name = "btnAttendMonitoring";
            this.btnAttendMonitoring.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendMonitoring.Click += new System.EventHandler(this.btnAttendMonitoring_Click);
            this.btnAttendMonitoring.Size = new System.Drawing.Size(277, 36);
            this.btnAttendMonitoring.TabIndex = 7;
            this.btnAttendMonitoring.Text = "   Attendance Monitoring";
            this.btnAttendMonitoring.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendMonitoring.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendMonitoring.UseVisualStyleBackColor = false;
            // 
            // btnAttendEvent
            // 
            this.btnAttendEvent.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendEvent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendEvent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendEvent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_event_accepted_tentatively_24;
            this.btnAttendEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendEvent.Location = new System.Drawing.Point(22, 313);
            this.btnAttendEvent.Name = "btnAttendEvent";
            this.btnAttendEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendEvent.Click += new System.EventHandler(this.btnAttendEvent_Click);
            this.btnAttendEvent.Size = new System.Drawing.Size(277, 36);
            this.btnAttendEvent.TabIndex = 6;
            this.btnAttendEvent.Text = "   Event Management";
            this.btnAttendEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendEvent.UseVisualStyleBackColor = false;
            // 
            // btnAttendFace
            // 
            this.btnAttendFace.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendFace.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendFace.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendFace.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_facial_recognition_24;
            this.btnAttendFace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendFace.Location = new System.Drawing.Point(22, 275);
            this.btnAttendFace.Name = "btnAttendFace";
            this.btnAttendFace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendFace.Click += new System.EventHandler(this.btnAttendFace_Click);
            this.btnAttendFace.Size = new System.Drawing.Size(277, 36);
            this.btnAttendFace.TabIndex = 5;
            this.btnAttendFace.Text = "   Face Recognition";
            this.btnAttendFace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendFace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendFace.UseVisualStyleBackColor = false;
            // 
            // btnAttendOfficer
            // 
            this.btnAttendOfficer.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendOfficer.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendOfficer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendOfficer.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_management_24;
            this.btnAttendOfficer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendOfficer.Location = new System.Drawing.Point(22, 237);
            this.btnAttendOfficer.Name = "btnAttendOfficer";
            this.btnAttendOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendOfficer.Click += new System.EventHandler(this.btnAttendOfficer_Click);
            this.btnAttendOfficer.Size = new System.Drawing.Size(277, 36);
            this.btnAttendOfficer.TabIndex = 4;
            this.btnAttendOfficer.Text = "   Officer Records";
            this.btnAttendOfficer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendOfficer.UseVisualStyleBackColor = false;
            // 
            // btnAttendStudent
            // 
            this.btnAttendStudent.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendStudent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendStudent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendStudent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_what_i_do_241;
            this.btnAttendStudent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendStudent.Location = new System.Drawing.Point(22, 199);
            this.btnAttendStudent.Name = "btnAttendStudent";
            this.btnAttendStudent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendStudent.Click += new System.EventHandler(this.btnAttendStudent_Click);
            this.btnAttendStudent.Size = new System.Drawing.Size(277, 36);
            this.btnAttendStudent.TabIndex = 3;
            this.btnAttendStudent.Text = "   Student Records";
            this.btnAttendStudent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendStudent.UseVisualStyleBackColor = false;
            // 
            // btnAttendDept
            // 
            this.btnAttendDept.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendDept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendDept.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendDept.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendDept.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_building_24__1_;
            this.btnAttendDept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendDept.Location = new System.Drawing.Point(22, 161);
            this.btnAttendDept.Name = "btnAttendDept";
            this.btnAttendDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendDept.Click += new System.EventHandler(this.btnAttendDept_Click);
            this.btnAttendDept.Size = new System.Drawing.Size(277, 36);
            this.btnAttendDept.TabIndex = 2;
            this.btnAttendDept.Text = "   Departments";
            this.btnAttendDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendDept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendDept.UseVisualStyleBackColor = false;
            // 
            // dtgAttendanceMon
            // 
            this.dtgAttendanceMon.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dtgAttendanceMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAttendanceMon.Location = new System.Drawing.Point(459, 160);
            this.dtgAttendanceMon.Name = "dtgAttendanceMon";
            this.dtgAttendanceMon.SelectionChanged += new System.EventHandler(this.dtgAttendanceMon_SelectionChanged);
            this.dtgAttendanceMon.ReadOnly = true;
            this.dtgAttendanceMon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAttendanceMon.RowHeadersVisible = false;
            this.dtgAttendanceMon.MultiSelect = false;
            this.dtgAttendanceMon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgAttendanceMon.AllowUserToResizeRows = false;
            this.dtgAttendanceMon.AllowUserToDeleteRows = false;
            this.dtgAttendanceMon.AllowUserToAddRows = false;
            this.dtgAttendanceMon.RowHeadersWidth = 51;
            this.dtgAttendanceMon.RowTemplate.Height = 24;
            this.dtgAttendanceMon.Size = new System.Drawing.Size(834, 420);
            this.dtgAttendanceMon.TabIndex = 37;
            // 
            // chkPresent
            // 
            this.chkPresent.AutoSize = true;
            this.chkPresent.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPresent.Location = new System.Drawing.Point(343, 162);
            this.chkPresent.Name = "chkPresent";
            this.chkPresent.CheckedChanged += new System.EventHandler(this.filter_Changed);
            this.chkPresent.ForeColor = System.Drawing.Color.DarkGreen;
            this.chkPresent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPresent.Checked = true;
            this.chkPresent.Size = new System.Drawing.Size(83, 21);
            this.chkPresent.TabIndex = 38;
            this.chkPresent.Text = "Present";
            this.chkPresent.UseVisualStyleBackColor = true;
            // 
            // chkAbsent
            // 
            this.chkAbsent.AutoSize = true;
            this.chkAbsent.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAbsent.Location = new System.Drawing.Point(343, 186);
            this.chkAbsent.Name = "chkAbsent";
            this.chkAbsent.CheckedChanged += new System.EventHandler(this.filter_Changed);
            this.chkAbsent.ForeColor = System.Drawing.Color.Firebrick;
            this.chkAbsent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAbsent.Checked = true;
            this.chkAbsent.Size = new System.Drawing.Size(78, 21);
            this.chkAbsent.TabIndex = 39;
            this.chkAbsent.Text = "Absent";
            this.chkAbsent.UseVisualStyleBackColor = true;
            // 
            // cmbDepartmentAttend
            // 
            this.cmbDepartmentAttend.FormattingEnabled = true;
            this.cmbDepartmentAttend.Location = new System.Drawing.Point(432, 96);
            this.cmbDepartmentAttend.Name = "cmbDepartmentAttend";
            this.cmbDepartmentAttend.SelectedIndexChanged += new System.EventHandler(this.filter_Changed);
            this.cmbDepartmentAttend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartmentAttend.Size = new System.Drawing.Size(140, 24);
            this.cmbDepartmentAttend.TabIndex = 2;
            // 
            // cmbYearLevelAttend
            // 
            this.cmbYearLevelAttend.FormattingEnabled = true;
            this.cmbYearLevelAttend.Location = new System.Drawing.Point(668, 96);
            this.cmbYearLevelAttend.Items.AddRange(new object[] {
            "All Year Levels",
            "1st Year",
            "2nd Year",
            "3rd Year",
            "4th Year"});
            this.cmbYearLevelAttend.Name = "cmbYearLevelAttend";
            this.cmbYearLevelAttend.SelectedIndexChanged += new System.EventHandler(this.filter_Changed);
            this.cmbYearLevelAttend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearLevelAttend.Size = new System.Drawing.Size(120, 24);
            this.cmbYearLevelAttend.TabIndex = 3;
            // 
            // cmbSelectEvent
            // 
            this.cmbSelectEvent.FormattingEnabled = true;
            this.cmbSelectEvent.Location = new System.Drawing.Point(395, 60);
            this.cmbSelectEvent.Name = "cmbSelectEvent";
            this.cmbSelectEvent.SelectedIndexChanged += new System.EventHandler(this.cmbSelectEvent_SelectedIndexChanged);
            this.cmbSelectEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectEvent.Size = new System.Drawing.Size(320, 24);
            this.cmbSelectEvent.TabIndex = 0;
            // 
            // lblSession
            // 
            this.lblSession.AutoSize = true;
            this.lblSession.BackColor = System.Drawing.Color.Transparent;
            this.lblSession.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSession.Location = new System.Drawing.Point(728, 64);
            this.lblSession.Name = "lblSession";
            this.lblSession.Size = new System.Drawing.Size(70, 17);
            this.lblSession.TabIndex = 0;
            this.lblSession.Text = "Session :";
            // 
            // cmbSession
            // 
            this.cmbSession.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSession.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSession.FormattingEnabled = true;
            this.cmbSession.Location = new System.Drawing.Point(795, 60);
            this.cmbSession.Name = "cmbSession";
            this.cmbSession.Size = new System.Drawing.Size(290, 24);
            this.cmbSession.TabIndex = 1;
            this.cmbSession.SelectedIndexChanged += new System.EventHandler(this.cmbSession_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnRefresh.Location = new System.Drawing.Point(1095, 58);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(95, 28);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnExportCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCsv.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCsv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnExportCsv.Location = new System.Drawing.Point(1198, 58);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(95, 28);
            this.btnExportCsv.TabIndex = 0;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = false;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(800, 100);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(45, 17);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Date :";
            // 
            // dtpDate
            // 
            this.dtpDate.Checked = false;
            this.dtpDate.CustomFormat = "MMM dd, yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(845, 96);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ShowCheckBox = true;
            this.dtpDate.Size = new System.Drawing.Size(150, 25);
            this.dtpDate.TabIndex = 4;
            this.dtpDate.ValueChanged += new System.EventHandler(this.filter_Changed);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(1005, 100);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(60, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search :";
            // 
            // txtSearchAttendance
            // 
            this.txtSearchAttendance.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchAttendance.Location = new System.Drawing.Point(1065, 96);
            this.txtSearchAttendance.Name = "txtSearchAttendance";
            this.txtSearchAttendance.Size = new System.Drawing.Size(228, 25);
            this.txtSearchAttendance.TabIndex = 5;
            this.txtSearchAttendance.MaxLength = 60;
            this.txtSearchAttendance.TextChanged += new System.EventHandler(this.filter_Changed);
            // 
            // chkPending
            // 
            this.chkPending.BackColor = System.Drawing.Color.Transparent;
            this.chkPending.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPending.Checked = true;
            this.chkPending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPending.ForeColor = System.Drawing.Color.DimGray;
            this.chkPending.Location = new System.Drawing.Point(343, 210);
            this.chkPending.Name = "chkPending";
            this.chkPending.AutoSize = true;
            this.chkPending.Size = new System.Drawing.Size(100, 21);
            this.chkPending.TabIndex = 0;
            this.chkPending.Text = "Pending";
            this.chkPending.UseVisualStyleBackColor = false;
            this.chkPending.CheckedChanged += new System.EventHandler(this.filter_Changed);
            // 
            // lblLogoutFilter
            // 
            this.lblLogoutFilter.AutoSize = true;
            this.lblLogoutFilter.BackColor = System.Drawing.Color.Transparent;
            this.lblLogoutFilter.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogoutFilter.Location = new System.Drawing.Point(340, 240);
            this.lblLogoutFilter.Name = "lblLogoutFilter";
            this.lblLogoutFilter.Size = new System.Drawing.Size(100, 17);
            this.lblLogoutFilter.TabIndex = 0;
            this.lblLogoutFilter.Text = "Log Out :";
            // 
            // cmbLogoutStatus
            // 
            this.cmbLogoutStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogoutStatus.Font = new System.Drawing.Font("Cambria", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLogoutStatus.FormattingEnabled = true;
            this.cmbLogoutStatus.Items.AddRange(new object[] {
            "All",
            "Logged Out",
            "Waiting",
            "Missing Log Out",
            "Not Required"});
            this.cmbLogoutStatus.Location = new System.Drawing.Point(340, 260);
            this.cmbLogoutStatus.Name = "cmbLogoutStatus";
            this.cmbLogoutStatus.Size = new System.Drawing.Size(110, 24);
            this.cmbLogoutStatus.TabIndex = 0;
            this.cmbLogoutStatus.SelectedIndexChanged += new System.EventHandler(this.filter_Changed);
            // 
            // btnToday
            // 
            this.btnToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnToday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnToday.Location = new System.Drawing.Point(340, 298);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(110, 30);
            this.btnToday.TabIndex = 0;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = false;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnClearFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilters.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearFilters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnClearFilters.Location = new System.Drawing.Point(340, 333);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(110, 30);
            this.btnClearFilters.TabIndex = 0;
            this.btnClearFilters.Text = "Clear Filters";
            this.btnClearFilters.UseVisualStyleBackColor = false;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // lblManualTitle
            // 
            this.lblManualTitle.AutoSize = true;
            this.lblManualTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblManualTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManualTitle.Location = new System.Drawing.Point(340, 380);
            this.lblManualTitle.Name = "lblManualTitle";
            this.lblManualTitle.Size = new System.Drawing.Size(110, 17);
            this.lblManualTitle.TabIndex = 0;
            this.lblManualTitle.Text = "Manual Entry :";
            // 
            // btnMarkPresent
            // 
            this.btnMarkPresent.BackColor = System.Drawing.Color.Honeydew;
            this.btnMarkPresent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarkPresent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkPresent.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarkPresent.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnMarkPresent.Location = new System.Drawing.Point(340, 402);
            this.btnMarkPresent.Name = "btnMarkPresent";
            this.btnMarkPresent.Size = new System.Drawing.Size(110, 32);
            this.btnMarkPresent.TabIndex = 0;
            this.btnMarkPresent.Text = "Mark Present";
            this.btnMarkPresent.UseVisualStyleBackColor = false;
            this.btnMarkPresent.Click += new System.EventHandler(this.btnMarkPresent_Click);
            // 
            // btnMarkLogout
            // 
            this.btnMarkLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnMarkLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarkLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarkLogout.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarkLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnMarkLogout.Location = new System.Drawing.Point(340, 439);
            this.btnMarkLogout.Name = "btnMarkLogout";
            this.btnMarkLogout.Size = new System.Drawing.Size(110, 32);
            this.btnMarkLogout.TabIndex = 0;
            this.btnMarkLogout.Text = "Mark Log Out";
            this.btnMarkLogout.UseVisualStyleBackColor = false;
            this.btnMarkLogout.Click += new System.EventHandler(this.btnMarkLogout_Click);
            // 
            // btnRemoveAttendance
            // 
            this.btnRemoveAttendance.BackColor = System.Drawing.Color.MistyRose;
            this.btnRemoveAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveAttendance.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAttendance.ForeColor = System.Drawing.Color.Firebrick;
            this.btnRemoveAttendance.Location = new System.Drawing.Point(340, 476);
            this.btnRemoveAttendance.Name = "btnRemoveAttendance";
            this.btnRemoveAttendance.Size = new System.Drawing.Size(110, 32);
            this.btnRemoveAttendance.TabIndex = 0;
            this.btnRemoveAttendance.Text = "Remove";
            this.btnRemoveAttendance.UseVisualStyleBackColor = false;
            this.btnRemoveAttendance.Click += new System.EventHandler(this.btnRemoveAttendance_Click);
            // 
            // lblManualHelp
            // 
            this.lblManualHelp.BackColor = System.Drawing.Color.Transparent;
            this.lblManualHelp.Font = new System.Drawing.Font("Cambria", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManualHelp.ForeColor = System.Drawing.Color.DimGray;
            this.lblManualHelp.Location = new System.Drawing.Point(338, 512);
            this.lblManualHelp.Name = "lblManualHelp";
            this.lblManualHelp.Size = new System.Drawing.Size(116, 95);
            this.lblManualHelp.TabIndex = 0;
            this.lblManualHelp.Text = "Use only when the camera cannot recognize the student. Select a record first.";
            // 
            // lblSummary
            // 
            this.lblSummary.BackColor = System.Drawing.Color.Transparent;
            this.lblSummary.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSummary.Location = new System.Drawing.Point(459, 134);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(834, 22);
            this.lblSummary.TabIndex = 0;
            this.lblSummary.Text = "";
            // 
            // lblNoRecord
            // 
            this.lblNoRecord.BackColor = System.Drawing.Color.White;
            this.lblNoRecord.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoRecord.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoRecord.Location = new System.Drawing.Point(560, 330);
            this.lblNoRecord.Name = "lblNoRecord";
            this.lblNoRecord.Visible = false;
            this.lblNoRecord.Size = new System.Drawing.Size(630, 40);
            this.lblNoRecord.TabIndex = 0;
            this.lblNoRecord.Text = "No attendance records found.";
            this.lblNoRecord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSelectedRecord
            // 
            this.lblSelectedRecord.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedRecord.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedRecord.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblSelectedRecord.Location = new System.Drawing.Point(459, 584);
            this.lblSelectedRecord.Name = "lblSelectedRecord";
            this.lblSelectedRecord.Size = new System.Drawing.Size(834, 22);
            this.lblSelectedRecord.TabIndex = 0;
            this.lblSelectedRecord.Text = "No record selected.";
            // 
            // AttendanceMonitoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.Controls.Add(this.lblSelectedRecord);
            this.Controls.Add(this.lblNoRecord);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblManualHelp);
            this.Controls.Add(this.btnRemoveAttendance);
            this.Controls.Add(this.btnMarkLogout);
            this.Controls.Add(this.btnMarkPresent);
            this.Controls.Add(this.lblManualTitle);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.cmbLogoutStatus);
            this.Controls.Add(this.lblLogoutFilter);
            this.Controls.Add(this.chkPending);
            this.Controls.Add(this.txtSearchAttendance);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.btnExportCsv);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cmbSession);
            this.Controls.Add(this.lblSession);
            this.Controls.Add(this.cmbSelectEvent);
            this.Controls.Add(this.cmbYearLevelAttend);
            this.Controls.Add(this.cmbDepartmentAttend);
            this.Controls.Add(this.chkAbsent);
            this.Controls.Add(this.chkPresent);
            this.Controls.Add(this.dtgAttendanceMon);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblYearLevel);
            this.Controls.Add(this.lblDepartment);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblSelectEvent);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AttendanceMonitoring";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCC Records Management System - Attendance Monitoring";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AttendanceMonitoring_FormClosed);
            this.Load += new System.EventHandler(this.AttendanceMonitoring_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAttendanceMon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSelectedRecord;
        private System.Windows.Forms.Label lblNoRecord;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Label lblManualHelp;
        private System.Windows.Forms.Button btnRemoveAttendance;
        private System.Windows.Forms.Button btnMarkLogout;
        private System.Windows.Forms.Button btnMarkPresent;
        private System.Windows.Forms.Label lblManualTitle;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.ComboBox cmbLogoutStatus;
        private System.Windows.Forms.Label lblLogoutFilter;
        private System.Windows.Forms.CheckBox chkPending;
        private System.Windows.Forms.TextBox txtSearchAttendance;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbSession;
        private System.Windows.Forms.Label lblSession;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSelectEvent;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label lblYearLevel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAttendLogout;
        private System.Windows.Forms.Button btnAttendDash;
        private System.Windows.Forms.Button btnAttendRules;
        private System.Windows.Forms.Button btnAttendObligation;
        private System.Windows.Forms.Button btnAttendReports;
        private System.Windows.Forms.Button btnAttendMonitoring;
        private System.Windows.Forms.Button btnAttendEvent;
        private System.Windows.Forms.Button btnAttendFace;
        private System.Windows.Forms.Button btnAttendOfficer;
        private System.Windows.Forms.Button btnAttendStudent;
        private System.Windows.Forms.Button btnAttendDept;
        private System.Windows.Forms.DataGridView dtgAttendanceMon;
        private System.Windows.Forms.CheckBox chkPresent;
        private System.Windows.Forms.CheckBox chkAbsent;
        private System.Windows.Forms.ComboBox cmbDepartmentAttend;
        private System.Windows.Forms.ComboBox cmbYearLevelAttend;
        private System.Windows.Forms.ComboBox cmbSelectEvent;
    }
}