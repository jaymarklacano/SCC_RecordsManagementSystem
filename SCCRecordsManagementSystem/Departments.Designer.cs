namespace SCCRecordsManagementSystem
{
    partial class Departments
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
            this.btnViewStudents = new System.Windows.Forms.Button();
            this.lblDetailEvents = new System.Windows.Forms.Label();
            this.lblDetailOfficers = new System.Windows.Forms.Label();
            this.lblDetailStudents = new System.Windows.Forms.Label();
            this.lblCountsTitle = new System.Windows.Forms.Label();
            this.lblDetailCreated = new System.Windows.Forms.Label();
            this.lblDetailDescription = new System.Windows.Forms.Label();
            this.lblDetailLevel = new System.Windows.Forms.Label();
            this.lblDetailEmail = new System.Windows.Forms.Label();
            this.lblDetailHead = new System.Windows.Forms.Label();
            this.lblDetailCode = new System.Windows.Forms.Label();
            this.lblDetailName = new System.Windows.Forms.Label();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.pnlDepartmentDetails = new System.Windows.Forms.Panel();
            this.lblTotalDepartments = new System.Windows.Forms.Label();
            this.lblNoDepartment = new System.Windows.Forms.Label();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtgDepartment = new System.Windows.Forms.DataGridView();
            this.colDepartmentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartmentCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartmentHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartmentEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartmentLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartmentStudents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEditDepartment = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDeleteDepartment = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnAddDepartment = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeptLogout = new System.Windows.Forms.Button();
            this.btnDeptDashboard = new System.Windows.Forms.Button();
            this.btnDeptRules = new System.Windows.Forms.Button();
            this.btnDeptObligation = new System.Windows.Forms.Button();
            this.btnDeptReport = new System.Windows.Forms.Button();
            this.btnDeptAttendance = new System.Windows.Forms.Button();
            this.btnDeptEvent = new System.Windows.Forms.Button();
            this.btnDeptFaceRecognition = new System.Windows.Forms.Button();
            this.btnDeptOfficer = new System.Windows.Forms.Button();
            this.btnDeptStudentRecords = new System.Windows.Forms.Button();
            this.btnDeptDepartments = new System.Windows.Forms.Button();
            this.txtSearchDepartment = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDepartment)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.lblTitle);
            this.panel5.Location = new System.Drawing.Point(324, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(983, 48);
            this.panel5.TabIndex = 5;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(111, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(121, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Departments";
            // 
            // dtgDepartment
            // 
            this.dtgDepartment.AutoGenerateColumns = false;
            this.dtgDepartment.BackgroundColor = System.Drawing.Color.White;
            this.dtgDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDepartment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDepartmentId,
            this.colDepartmentName,
            this.colDepartmentCode,
            this.colDepartmentHead,
            this.colDepartmentEmail,
            this.colDepartmentLevel,
            this.colDepartmentStudents,
            this.colEditDepartment,
            this.colDeleteDepartment});
            this.dtgDepartment.Location = new System.Drawing.Point(359, 114);
            this.dtgDepartment.Name = "dtgDepartment";
            this.dtgDepartment.SelectionChanged += new System.EventHandler(this.dtgDepartment_SelectionChanged);
            this.dtgDepartment.ReadOnly = true;
            this.dtgDepartment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgDepartment.RowHeadersVisible = false;
            this.dtgDepartment.MultiSelect = false;
            this.dtgDepartment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgDepartment.AllowUserToResizeRows = false;
            this.dtgDepartment.AllowUserToDeleteRows = false;
            this.dtgDepartment.AllowUserToAddRows = false;
            this.dtgDepartment.RowHeadersWidth = 51;
            this.dtgDepartment.RowTemplate.Height = 24;
            this.dtgDepartment.Size = new System.Drawing.Size(645, 437);
            this.dtgDepartment.TabIndex = 24;
            this.dtgDepartment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgDepartment_CellContentClick);
            //
            // colDepartmentId
            //
            this.colDepartmentId.DataPropertyName = "department_id";
            this.colDepartmentId.HeaderText = "ID";
            this.colDepartmentId.Name = "colDepartmentId";
            this.colDepartmentId.ReadOnly = true;
            this.colDepartmentId.Visible = false;
            //
            // colDepartmentName
            //
            this.colDepartmentName.DataPropertyName = "department_name";
            this.colDepartmentName.FillWeight = 200F;
            this.colDepartmentName.HeaderText = "Department Name";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.ReadOnly = true;
            //
            // colDepartmentCode
            //
            this.colDepartmentCode.DataPropertyName = "department_code";
            this.colDepartmentCode.HeaderText = "Code";
            this.colDepartmentCode.Name = "colDepartmentCode";
            this.colDepartmentCode.ReadOnly = true;
            //
            // colDepartmentHead
            //
            this.colDepartmentHead.DataPropertyName = "department_head";
            this.colDepartmentHead.HeaderText = "Head";
            this.colDepartmentHead.Name = "colDepartmentHead";
            this.colDepartmentHead.ReadOnly = true;
            //
            // colDepartmentEmail
            //
            this.colDepartmentEmail.DataPropertyName = "contact_email";
            this.colDepartmentEmail.HeaderText = "Email";
            this.colDepartmentEmail.Name = "colDepartmentEmail";
            this.colDepartmentEmail.ReadOnly = true;
            //
            // colDepartmentLevel
            //
            this.colDepartmentLevel.DataPropertyName = "academic_level";
            this.colDepartmentLevel.HeaderText = "Academic Level";
            this.colDepartmentLevel.Name = "colDepartmentLevel";
            this.colDepartmentLevel.ReadOnly = true;
            //
            // colDepartmentStudents
            //
            this.colDepartmentStudents.DataPropertyName = "student_count";
            this.colDepartmentStudents.FillWeight = 60F;
            this.colDepartmentStudents.HeaderText = "Students";
            this.colDepartmentStudents.Name = "colDepartmentStudents";
            this.colDepartmentStudents.ReadOnly = true;
            //
            // colEditDepartment
            //
            this.colEditDepartment.FillWeight = 55F;
            this.colEditDepartment.HeaderText = "Action";
            this.colEditDepartment.Name = "colEditDepartment";
            this.colEditDepartment.ReadOnly = true;
            this.colEditDepartment.Text = "Edit";
            this.colEditDepartment.UseColumnTextForButtonValue = true;
            //
            // colDeleteDepartment
            //
            this.colDeleteDepartment.FillWeight = 55F;
            this.colDeleteDepartment.HeaderText = "";
            this.colDeleteDepartment.Name = "colDeleteDepartment";
            this.colDeleteDepartment.ReadOnly = true;
            this.colDeleteDepartment.Text = "Delete";
            this.colDeleteDepartment.UseColumnTextForButtonValue = true;
            // 
            // btnAddDepartment
            // 
            this.btnAddDepartment.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDepartment.Location = new System.Drawing.Point(786, 66);
            this.btnAddDepartment.Name = "btnAddDepartment";
            this.btnAddDepartment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDepartment.Size = new System.Drawing.Size(218, 36);
            this.btnAddDepartment.TabIndex = 25;
            this.btnAddDepartment.Text = "Add Department";
            this.btnAddDepartment.UseVisualStyleBackColor = true;
            this.btnAddDepartment.Click += new System.EventHandler(this.btnAddDepartment_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.panel1.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._904439575_1911191946735306_1039497458727327980_n1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnDeptLogout);
            this.panel1.Controls.Add(this.btnDeptDashboard);
            this.panel1.Controls.Add(this.btnDeptRules);
            this.panel1.Controls.Add(this.btnDeptObligation);
            this.panel1.Controls.Add(this.btnDeptReport);
            this.panel1.Controls.Add(this.btnDeptAttendance);
            this.panel1.Controls.Add(this.btnDeptEvent);
            this.panel1.Controls.Add(this.btnDeptFaceRecognition);
            this.panel1.Controls.Add(this.btnDeptOfficer);
            this.panel1.Controls.Add(this.btnDeptStudentRecords);
            this.panel1.Controls.Add(this.btnDeptDepartments);
            this.panel1.Location = new System.Drawing.Point(-4, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 633);
            this.panel1.TabIndex = 26;
            // 
            // btnDeptLogout
            // 
            this.btnDeptLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptLogout.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(75)))), ((int)(((byte)(80)))));
            this.btnDeptLogout.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_log_out_24__2_;
            this.btnDeptLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptLogout.Location = new System.Drawing.Point(22, 560);
            this.btnDeptLogout.Name = "btnDeptLogout";
            this.btnDeptLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptLogout.Click += new System.EventHandler(this.btnDeptLogout_Click);
            this.btnDeptLogout.Size = new System.Drawing.Size(277, 38);
            this.btnDeptLogout.TabIndex = 11;
            this.btnDeptLogout.Text = "   LogOut";
            this.btnDeptLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptLogout.UseVisualStyleBackColor = false;
            // 
            // btnDeptDashboard
            // 
            this.btnDeptDashboard.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptDashboard.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptDashboard.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptDashboard.Image = global::SCCRecordsManagementSystem.Properties.Resources.house__1_;
            this.btnDeptDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptDashboard.Location = new System.Drawing.Point(22, 123);
            this.btnDeptDashboard.Name = "btnDeptDashboard";
            this.btnDeptDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptDashboard.Click += new System.EventHandler(this.btnDeptDashboard_Click);
            this.btnDeptDashboard.Size = new System.Drawing.Size(277, 36);
            this.btnDeptDashboard.TabIndex = 1;
            this.btnDeptDashboard.Text = "   Dashboard";
            this.btnDeptDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptDashboard.UseVisualStyleBackColor = false;
            // 
            // btnDeptRules
            // 
            this.btnDeptRules.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptRules.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptRules.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptRules.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_rules_book_24;
            this.btnDeptRules.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptRules.Location = new System.Drawing.Point(22, 465);
            this.btnDeptRules.Name = "btnDeptRules";
            this.btnDeptRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptRules.Click += new System.EventHandler(this.btnDeptRules_Click);
            this.btnDeptRules.Size = new System.Drawing.Size(277, 36);
            this.btnDeptRules.TabIndex = 10;
            this.btnDeptRules.Text = "   Rules and Permission";
            this.btnDeptRules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptRules.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptRules.UseVisualStyleBackColor = false;
            // 
            // btnDeptObligation
            // 
            this.btnDeptObligation.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptObligation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptObligation.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptObligation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptObligation.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_user_24;
            this.btnDeptObligation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptObligation.Location = new System.Drawing.Point(22, 427);
            this.btnDeptObligation.Name = "btnDeptObligation";
            this.btnDeptObligation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptObligation.Click += new System.EventHandler(this.btnDeptObligation_Click);
            this.btnDeptObligation.Size = new System.Drawing.Size(277, 36);
            this.btnDeptObligation.TabIndex = 9;
            this.btnDeptObligation.Text = "Obligations";
            this.btnDeptObligation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptObligation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptObligation.UseVisualStyleBackColor = false;
            // 
            // btnDeptReport
            // 
            this.btnDeptReport.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptReport.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptReport.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptReport.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_analysis_24;
            this.btnDeptReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptReport.Location = new System.Drawing.Point(22, 389);
            this.btnDeptReport.Name = "btnDeptReport";
            this.btnDeptReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptReport.Click += new System.EventHandler(this.btnDeptReport_Click);
            this.btnDeptReport.Size = new System.Drawing.Size(277, 36);
            this.btnDeptReport.TabIndex = 8;
            this.btnDeptReport.Text = "   Reports and Analysis";
            this.btnDeptReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptReport.UseVisualStyleBackColor = false;
            // 
            // btnDeptAttendance
            // 
            this.btnDeptAttendance.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptAttendance.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptAttendance.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptAttendance.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_pass_fail_24;
            this.btnDeptAttendance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptAttendance.Location = new System.Drawing.Point(22, 351);
            this.btnDeptAttendance.Name = "btnDeptAttendance";
            this.btnDeptAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptAttendance.Click += new System.EventHandler(this.btnDeptAttendance_Click);
            this.btnDeptAttendance.Size = new System.Drawing.Size(277, 36);
            this.btnDeptAttendance.TabIndex = 7;
            this.btnDeptAttendance.Text = "   Attendance Monitoring";
            this.btnDeptAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptAttendance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptAttendance.UseVisualStyleBackColor = false;
            // 
            // btnDeptEvent
            // 
            this.btnDeptEvent.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptEvent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptEvent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptEvent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_event_accepted_tentatively_24;
            this.btnDeptEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptEvent.Location = new System.Drawing.Point(22, 313);
            this.btnDeptEvent.Name = "btnDeptEvent";
            this.btnDeptEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptEvent.Click += new System.EventHandler(this.btnDeptEvent_Click);
            this.btnDeptEvent.Size = new System.Drawing.Size(277, 36);
            this.btnDeptEvent.TabIndex = 6;
            this.btnDeptEvent.Text = "   Event Management";
            this.btnDeptEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptEvent.UseVisualStyleBackColor = false;
            // 
            // btnDeptFaceRecognition
            // 
            this.btnDeptFaceRecognition.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptFaceRecognition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptFaceRecognition.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptFaceRecognition.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptFaceRecognition.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_facial_recognition_24;
            this.btnDeptFaceRecognition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptFaceRecognition.Location = new System.Drawing.Point(22, 275);
            this.btnDeptFaceRecognition.Name = "btnDeptFaceRecognition";
            this.btnDeptFaceRecognition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptFaceRecognition.Click += new System.EventHandler(this.btnDeptFaceRecognition_Click);
            this.btnDeptFaceRecognition.Size = new System.Drawing.Size(277, 36);
            this.btnDeptFaceRecognition.TabIndex = 5;
            this.btnDeptFaceRecognition.Text = "   Face Recognition";
            this.btnDeptFaceRecognition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptFaceRecognition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptFaceRecognition.UseVisualStyleBackColor = false;
            // 
            // btnDeptOfficer
            // 
            this.btnDeptOfficer.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptOfficer.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptOfficer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptOfficer.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_management_24;
            this.btnDeptOfficer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptOfficer.Location = new System.Drawing.Point(22, 237);
            this.btnDeptOfficer.Name = "btnDeptOfficer";
            this.btnDeptOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptOfficer.Click += new System.EventHandler(this.btnDeptOfficer_Click);
            this.btnDeptOfficer.Size = new System.Drawing.Size(277, 36);
            this.btnDeptOfficer.TabIndex = 4;
            this.btnDeptOfficer.Text = "   Officer Records";
            this.btnDeptOfficer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptOfficer.UseVisualStyleBackColor = false;
            // 
            // btnDeptStudentRecords
            // 
            this.btnDeptStudentRecords.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptStudentRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptStudentRecords.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptStudentRecords.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptStudentRecords.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_what_i_do_241;
            this.btnDeptStudentRecords.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptStudentRecords.Location = new System.Drawing.Point(22, 199);
            this.btnDeptStudentRecords.Name = "btnDeptStudentRecords";
            this.btnDeptStudentRecords.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptStudentRecords.Click += new System.EventHandler(this.btnDeptStudentRecords_Click);
            this.btnDeptStudentRecords.Size = new System.Drawing.Size(277, 36);
            this.btnDeptStudentRecords.TabIndex = 3;
            this.btnDeptStudentRecords.Text = "   Student Records";
            this.btnDeptStudentRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptStudentRecords.UseVisualStyleBackColor = false;
            // 
            // btnDeptDepartments
            // 
            this.btnDeptDepartments.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptDepartments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptDepartments.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptDepartments.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptDepartments.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_building_24__1_;
            this.btnDeptDepartments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptDepartments.Location = new System.Drawing.Point(22, 161);
            this.btnDeptDepartments.Name = "btnDeptDepartments";
            this.btnDeptDepartments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeptDepartments.Click += new System.EventHandler(this.btnDeptDepartments_Click);
            this.btnDeptDepartments.Size = new System.Drawing.Size(277, 36);
            this.btnDeptDepartments.TabIndex = 2;
            this.btnDeptDepartments.Text = "   Departments";
            this.btnDeptDepartments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptDepartments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptDepartments.UseVisualStyleBackColor = false;
            // 
            // txtSearchDepartment
            // 
            this.txtSearchDepartment.Location = new System.Drawing.Point(439, 72);
            this.txtSearchDepartment.Name = "txtSearchDepartment";
            this.txtSearchDepartment.TextChanged += new System.EventHandler(this.txtSearchDepartment_TextChanged);
            this.txtSearchDepartment.MaxLength = 100;
            this.txtSearchDepartment.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchDepartment.Size = new System.Drawing.Size(235, 32);
            this.txtSearchDepartment.TabIndex = 27;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(355, 79);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(78, 23);
            this.lblSearch.TabIndex = 28;
            this.lblSearch.Text = "Search:";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnClearSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearch.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnClearSearch.Location = new System.Drawing.Point(682, 68);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(92, 36);
            this.btnClearSearch.TabIndex = 0;
            this.btnClearSearch.Text = "Clear";
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // lblNoDepartment
            // 
            this.lblNoDepartment.BackColor = System.Drawing.Color.White;
            this.lblNoDepartment.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoDepartment.ForeColor = System.Drawing.Color.Gray;
            this.lblNoDepartment.Location = new System.Drawing.Point(459, 300);
            this.lblNoDepartment.Name = "lblNoDepartment";
            this.lblNoDepartment.Size = new System.Drawing.Size(445, 40);
            this.lblNoDepartment.TabIndex = 0;
            this.lblNoDepartment.Text = "No departments found.";
            this.lblNoDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoDepartment.Visible = false;
            // 
            // lblTotalDepartments
            // 
            this.lblTotalDepartments.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalDepartments.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDepartments.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalDepartments.Location = new System.Drawing.Point(359, 558);
            this.lblTotalDepartments.Name = "lblTotalDepartments";
            this.lblTotalDepartments.Size = new System.Drawing.Size(645, 24);
            this.lblTotalDepartments.TabIndex = 0;
            this.lblTotalDepartments.Text = "Total: 0 department(s)";
            // 
            // pnlDepartmentDetails
            // 
            this.pnlDepartmentDetails.BackColor = System.Drawing.Color.White;
            this.pnlDepartmentDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDepartmentDetails.Controls.Add(this.btnViewStudents);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailEvents);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailOfficers);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailStudents);
            this.pnlDepartmentDetails.Controls.Add(this.lblCountsTitle);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailCreated);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailDescription);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailLevel);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailEmail);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailHead);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailCode);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailName);
            this.pnlDepartmentDetails.Controls.Add(this.lblDetailsTitle);
            this.pnlDepartmentDetails.Location = new System.Drawing.Point(1020, 66);
            this.pnlDepartmentDetails.Name = "pnlDepartmentDetails";
            this.pnlDepartmentDetails.Size = new System.Drawing.Size(265, 485);
            this.pnlDepartmentDetails.TabIndex = 0;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.lblDetailsTitle.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.White;
            this.lblDetailsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(263, 36);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "Department Details";
            this.lblDetailsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDetailName
            // 
            this.lblDetailName.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailName.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailName.Location = new System.Drawing.Point(10, 46);
            this.lblDetailName.Name = "lblDetailName";
            this.lblDetailName.Size = new System.Drawing.Size(243, 44);
            this.lblDetailName.TabIndex = 0;
            this.lblDetailName.Text = "Name: -";
            // 
            // lblDetailCode
            // 
            this.lblDetailCode.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailCode.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailCode.Location = new System.Drawing.Point(10, 96);
            this.lblDetailCode.Name = "lblDetailCode";
            this.lblDetailCode.Size = new System.Drawing.Size(243, 22);
            this.lblDetailCode.TabIndex = 0;
            this.lblDetailCode.Text = "Code: -";
            // 
            // lblDetailHead
            // 
            this.lblDetailHead.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailHead.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailHead.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailHead.Location = new System.Drawing.Point(10, 124);
            this.lblDetailHead.Name = "lblDetailHead";
            this.lblDetailHead.Size = new System.Drawing.Size(243, 44);
            this.lblDetailHead.TabIndex = 0;
            this.lblDetailHead.Text = "Head / Dean: -";
            // 
            // lblDetailEmail
            // 
            this.lblDetailEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailEmail.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailEmail.Location = new System.Drawing.Point(10, 174);
            this.lblDetailEmail.Name = "lblDetailEmail";
            this.lblDetailEmail.Size = new System.Drawing.Size(243, 44);
            this.lblDetailEmail.TabIndex = 0;
            this.lblDetailEmail.Text = "Email: -";
            // 
            // lblDetailLevel
            // 
            this.lblDetailLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailLevel.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailLevel.Location = new System.Drawing.Point(10, 224);
            this.lblDetailLevel.Name = "lblDetailLevel";
            this.lblDetailLevel.Size = new System.Drawing.Size(243, 22);
            this.lblDetailLevel.TabIndex = 0;
            this.lblDetailLevel.Text = "Academic Level: -";
            // 
            // lblDetailDescription
            // 
            this.lblDetailDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailDescription.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailDescription.Location = new System.Drawing.Point(10, 252);
            this.lblDetailDescription.Name = "lblDetailDescription";
            this.lblDetailDescription.Size = new System.Drawing.Size(243, 44);
            this.lblDetailDescription.TabIndex = 0;
            this.lblDetailDescription.Text = "Description: -";
            // 
            // lblDetailCreated
            // 
            this.lblDetailCreated.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailCreated.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailCreated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailCreated.Location = new System.Drawing.Point(10, 302);
            this.lblDetailCreated.Name = "lblDetailCreated";
            this.lblDetailCreated.Size = new System.Drawing.Size(243, 22);
            this.lblDetailCreated.TabIndex = 0;
            this.lblDetailCreated.Text = "Date Added: -";
            // 
            // lblCountsTitle
            // 
            this.lblCountsTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCountsTitle.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountsTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblCountsTitle.Location = new System.Drawing.Point(10, 336);
            this.lblCountsTitle.Name = "lblCountsTitle";
            this.lblCountsTitle.Size = new System.Drawing.Size(243, 22);
            this.lblCountsTitle.TabIndex = 0;
            this.lblCountsTitle.Text = "Members of this Department";
            // 
            // lblDetailStudents
            // 
            this.lblDetailStudents.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailStudents.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailStudents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailStudents.Location = new System.Drawing.Point(10, 362);
            this.lblDetailStudents.Name = "lblDetailStudents";
            this.lblDetailStudents.Size = new System.Drawing.Size(243, 22);
            this.lblDetailStudents.TabIndex = 0;
            this.lblDetailStudents.Text = "Students: 0";
            // 
            // lblDetailOfficers
            // 
            this.lblDetailOfficers.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailOfficers.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailOfficers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailOfficers.Location = new System.Drawing.Point(10, 386);
            this.lblDetailOfficers.Name = "lblDetailOfficers";
            this.lblDetailOfficers.Size = new System.Drawing.Size(243, 22);
            this.lblDetailOfficers.TabIndex = 0;
            this.lblDetailOfficers.Text = "Officers: 0";
            // 
            // lblDetailEvents
            // 
            this.lblDetailEvents.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailEvents.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailEvents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblDetailEvents.Location = new System.Drawing.Point(10, 410);
            this.lblDetailEvents.Name = "lblDetailEvents";
            this.lblDetailEvents.Size = new System.Drawing.Size(243, 22);
            this.lblDetailEvents.TabIndex = 0;
            this.lblDetailEvents.Text = "Events: 0";
            // 
            // btnViewStudents
            // 
            this.btnViewStudents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnViewStudents.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewStudents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewStudents.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewStudents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnViewStudents.Location = new System.Drawing.Point(10, 442);
            this.btnViewStudents.Name = "btnViewStudents";
            this.btnViewStudents.Size = new System.Drawing.Size(243, 34);
            this.btnViewStudents.TabIndex = 0;
            this.btnViewStudents.Text = "View Students";
            this.btnViewStudents.UseVisualStyleBackColor = false;
            this.btnViewStudents.Click += new System.EventHandler(this.btnViewStudents_Click);
            // 
            // Departments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._903375047_2169860083631994_8617979005398290656_n;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.Controls.Add(this.pnlDepartmentDetails);
            this.Controls.Add(this.lblTotalDepartments);
            this.Controls.Add(this.lblNoDepartment);
            this.Controls.Add(this.btnClearSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearchDepartment);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAddDepartment);
            this.Controls.Add(this.dtgDepartment);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Departments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCC Records Management System - Departments";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Departments_FormClosed);
            this.Load += new System.EventHandler(this.Departments_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDepartment)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnViewStudents;
        private System.Windows.Forms.Label lblDetailEvents;
        private System.Windows.Forms.Label lblDetailOfficers;
        private System.Windows.Forms.Label lblDetailStudents;
        private System.Windows.Forms.Label lblCountsTitle;
        private System.Windows.Forms.Label lblDetailCreated;
        private System.Windows.Forms.Label lblDetailDescription;
        private System.Windows.Forms.Label lblDetailLevel;
        private System.Windows.Forms.Label lblDetailEmail;
        private System.Windows.Forms.Label lblDetailHead;
        private System.Windows.Forms.Label lblDetailCode;
        private System.Windows.Forms.Label lblDetailName;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Panel pnlDepartmentDetails;
        private System.Windows.Forms.Label lblTotalDepartments;
        private System.Windows.Forms.Label lblNoDepartment;
        private System.Windows.Forms.Button btnClearSearch;

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dtgDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartmentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartmentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartmentCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartmentHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartmentEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartmentLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartmentStudents;
        private System.Windows.Forms.DataGridViewButtonColumn colEditDepartment;
        private System.Windows.Forms.DataGridViewButtonColumn colDeleteDepartment;
        private System.Windows.Forms.Button btnAddDepartment;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeptLogout;
        private System.Windows.Forms.Button btnDeptDashboard;
        private System.Windows.Forms.Button btnDeptRules;
        private System.Windows.Forms.Button btnDeptObligation;
        private System.Windows.Forms.Button btnDeptReport;
        private System.Windows.Forms.Button btnDeptAttendance;
        private System.Windows.Forms.Button btnDeptEvent;
        private System.Windows.Forms.Button btnDeptFaceRecognition;
        private System.Windows.Forms.Button btnDeptOfficer;
        private System.Windows.Forms.Button btnDeptStudentRecords;
        private System.Windows.Forms.Button btnDeptDepartments;
        private System.Windows.Forms.TextBox txtSearchDepartment;
        private System.Windows.Forms.Label lblSearch;
    }
}