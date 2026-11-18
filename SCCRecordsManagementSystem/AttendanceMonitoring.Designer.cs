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
            this.sidebarMenu = new SidebarMenu();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.windowButtons = new WindowButtons();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblStudent = new System.Windows.Forms.Label();
            this.cmbStudent = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblCourse = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.chkPresent = new System.Windows.Forms.CheckBox();
            this.chkAbsent = new System.Windows.Forms.CheckBox();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnAllDates = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblSummaryDate = new System.Windows.Forms.Label();
            this.lblSummaryTotal = new System.Windows.Forms.Label();
            this.lblSummaryPresent = new System.Windows.Forms.Label();
            this.lblSummaryAbsent = new System.Windows.Forms.Label();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.dgvAttendance = new System.Windows.Forms.DataGridView();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCourse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarMenu
            // 
            this.sidebarMenu.Location = new System.Drawing.Point(0, 0);
            this.sidebarMenu.Size = new System.Drawing.Size(320, 619);
            this.sidebarMenu.CurrentPage = SCCRecordsManagementSystem.SidebarPage.Attendance;
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
            this.lblPageTitle.Text = "Attendance Records";
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
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.Black;
            this.lblDate.Location = new System.Drawing.Point(340, 70);
            this.lblDate.Text = "Date:";
            this.lblDate.Name = "lblDate";
            this.lblDate.TabIndex = 2;
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.CustomFormat = "MMM d, yyyy";
            this.dtpDate.ShowCheckBox = true;
            this.dtpDate.Checked = true;
            this.dtpDate.Location = new System.Drawing.Point(385, 66);
            this.dtpDate.Size = new System.Drawing.Size(170, 24);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.TabIndex = 3;
            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.BackColor = System.Drawing.Color.Transparent;
            this.lblStudent.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudent.ForeColor = System.Drawing.Color.Black;
            this.lblStudent.Location = new System.Drawing.Point(572, 70);
            this.lblStudent.Text = "Student:";
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.TabIndex = 4;
            // 
            // cmbStudent
            // 
            this.cmbStudent.Items.AddRange(new object[] {
            });
            this.cmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudent.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStudent.FormattingEnabled = true;
            this.cmbStudent.Location = new System.Drawing.Point(640, 66);
            this.cmbStudent.Size = new System.Drawing.Size(310, 25);
            this.cmbStudent.Name = "cmbStudent";
            this.cmbStudent.TabIndex = 5;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.Black;
            this.lblSearch.Location = new System.Drawing.Point(965, 70);
            this.lblSearch.Text = "Search:";
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.TabIndex = 6;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(1030, 66);
            this.txtSearch.Size = new System.Drawing.Size(255, 24);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TabIndex = 7;
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.BackColor = System.Drawing.Color.Transparent;
            this.lblCourse.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourse.ForeColor = System.Drawing.Color.Black;
            this.lblCourse.Location = new System.Drawing.Point(340, 108);
            this.lblCourse.Text = "Course:";
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.TabIndex = 8;
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Items.AddRange(new object[] {
            });
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(400, 104);
            this.cmbDepartment.Size = new System.Drawing.Size(165, 25);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.TabIndex = 9;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.ForeColor = System.Drawing.Color.Black;
            this.lblYear.Location = new System.Drawing.Point(580, 108);
            this.lblYear.Text = "Year:";
            this.lblYear.Name = "lblYear";
            this.lblYear.TabIndex = 10;
            // 
            // cmbYear
            // 
            this.cmbYear.Items.AddRange(new object[] {
            "All Years",
            "1st Year",
            "2nd Year",
            "3rd Year",
            "4th Year"});
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(625, 104);
            this.cmbYear.Size = new System.Drawing.Size(105, 25);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.TabIndex = 11;
            // 
            // chkPresent
            // 
            this.chkPresent.AutoSize = true;
            this.chkPresent.Checked = true;
            this.chkPresent.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPresent.Location = new System.Drawing.Point(745, 107);
            this.chkPresent.Text = "Present";
            this.chkPresent.UseVisualStyleBackColor = true;
            this.chkPresent.Name = "chkPresent";
            this.chkPresent.TabIndex = 12;
            // 
            // chkAbsent
            // 
            this.chkAbsent.AutoSize = true;
            this.chkAbsent.Checked = true;
            this.chkAbsent.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAbsent.Location = new System.Drawing.Point(830, 107);
            this.chkAbsent.Text = "Absent";
            this.chkAbsent.UseVisualStyleBackColor = true;
            this.chkAbsent.Name = "chkAbsent";
            this.chkAbsent.TabIndex = 13;
            // 
            // btnToday
            // 
            this.btnToday.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.ForeColor = System.Drawing.Color.White;
            this.btnToday.Location = new System.Drawing.Point(930, 102);
            this.btnToday.Size = new System.Drawing.Size(100, 30);
            this.btnToday.Text = "TODAY";
            this.btnToday.UseVisualStyleBackColor = false;
            this.btnToday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToday.FlatAppearance.BorderSize = 0;
            this.btnToday.Name = "btnToday";
            this.btnToday.TabIndex = 14;
            // 
            // btnAllDates
            // 
            this.btnAllDates.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.btnAllDates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllDates.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllDates.ForeColor = System.Drawing.Color.Black;
            this.btnAllDates.Location = new System.Drawing.Point(1040, 102);
            this.btnAllDates.Size = new System.Drawing.Size(120, 30);
            this.btnAllDates.Text = "ALL DATES";
            this.btnAllDates.UseVisualStyleBackColor = false;
            this.btnAllDates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllDates.FlatAppearance.BorderSize = 0;
            this.btnAllDates.Name = "btnAllDates";
            this.btnAllDates.TabIndex = 15;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(1170, 102);
            this.btnClear.Size = new System.Drawing.Size(115, 30);
            this.btnClear.Text = "RESET";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.Name = "btnClear";
            this.btnClear.TabIndex = 16;
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.pnlSummary.Location = new System.Drawing.Point(340, 142);
            this.pnlSummary.Size = new System.Drawing.Size(945, 44);
            this.pnlSummary.Controls.Add(this.lblSummaryDate);
            this.pnlSummary.Controls.Add(this.lblSummaryTotal);
            this.pnlSummary.Controls.Add(this.lblSummaryPresent);
            this.pnlSummary.Controls.Add(this.lblSummaryAbsent);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.TabIndex = 17;
            // 
            // lblSummaryDate
            // 
            this.lblSummaryDate.AutoSize = true;
            this.lblSummaryDate.BackColor = System.Drawing.Color.Transparent;
            this.lblSummaryDate.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryDate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSummaryDate.Location = new System.Drawing.Point(14, 13);
            this.lblSummaryDate.Text = "";
            this.lblSummaryDate.Name = "lblSummaryDate";
            this.lblSummaryDate.TabIndex = 0;
            // 
            // lblSummaryTotal
            // 
            this.lblSummaryTotal.AutoSize = true;
            this.lblSummaryTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblSummaryTotal.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryTotal.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSummaryTotal.Location = new System.Drawing.Point(330, 13);
            this.lblSummaryTotal.Text = "";
            this.lblSummaryTotal.Name = "lblSummaryTotal";
            this.lblSummaryTotal.TabIndex = 1;
            // 
            // lblSummaryPresent
            // 
            this.lblSummaryPresent.AutoSize = true;
            this.lblSummaryPresent.BackColor = System.Drawing.Color.Transparent;
            this.lblSummaryPresent.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryPresent.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblSummaryPresent.Location = new System.Drawing.Point(540, 13);
            this.lblSummaryPresent.Text = "";
            this.lblSummaryPresent.Name = "lblSummaryPresent";
            this.lblSummaryPresent.TabIndex = 2;
            // 
            // lblSummaryAbsent
            // 
            this.lblSummaryAbsent.AutoSize = true;
            this.lblSummaryAbsent.BackColor = System.Drawing.Color.Transparent;
            this.lblSummaryAbsent.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryAbsent.ForeColor = System.Drawing.Color.Firebrick;
            this.lblSummaryAbsent.Location = new System.Drawing.Point(720, 13);
            this.lblSummaryAbsent.Text = "";
            this.lblSummaryAbsent.Name = "lblSummaryAbsent";
            this.lblSummaryAbsent.TabIndex = 3;
            // 
            // lblEmpty
            // 
            this.lblEmpty.AutoSize = false;
            this.lblEmpty.BackColor = System.Drawing.Color.White;
            this.lblEmpty.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblEmpty.Location = new System.Drawing.Point(341, 231);
            this.lblEmpty.Text = "No attendance records found.";
            this.lblEmpty.Size = new System.Drawing.Size(943, 344);
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.TabIndex = 18;
            // 
            // dgvAttendance
            // 
            System.Windows.Forms.DataGridViewCellStyle dgvAttendanceHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvAttendanceCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvAttendanceAltStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dgvAttendanceHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvAttendanceHeaderStyle.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            dgvAttendanceHeaderStyle.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvAttendanceHeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvAttendanceHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            dgvAttendanceHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvAttendanceCellStyle.BackColor = System.Drawing.Color.White;
            dgvAttendanceCellStyle.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvAttendanceCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvAttendanceCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            dgvAttendanceCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgvAttendanceAltStyle.BackColor = System.Drawing.Color.FromArgb(238, 244, 249);
            dgvAttendanceAltStyle.SelectionBackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            dgvAttendanceAltStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAttendance.ColumnHeadersDefaultCellStyle = dgvAttendanceHeaderStyle;
            this.dgvAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAttendance.DefaultCellStyle = dgvAttendanceCellStyle;
            this.dgvAttendance.AlternatingRowsDefaultCellStyle = dgvAttendanceAltStyle;
            this.dgvAttendance.RowTemplate.Height = 28;
            this.dgvAttendance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDate,
            this.colNumber,
            this.colName,
            this.colCourse,
            this.colYear,
            this.colTime,
            this.colStatus});
            this.dgvAttendance.Location = new System.Drawing.Point(340, 196);
            this.dgvAttendance.Size = new System.Drawing.Size(945, 380);
            this.dgvAttendance.AllowUserToAddRows = false;
            this.dgvAttendance.AllowUserToDeleteRows = false;
            this.dgvAttendance.AllowUserToResizeRows = false;
            this.dgvAttendance.AutoGenerateColumns = false;
            this.dgvAttendance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAttendance.BackgroundColor = System.Drawing.Color.White;
            this.dgvAttendance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvAttendance.ColumnHeadersHeight = 34;
            this.dgvAttendance.EnableHeadersVisualStyles = false;
            this.dgvAttendance.MultiSelect = false;
            this.dgvAttendance.ReadOnly = true;
            this.dgvAttendance.RowHeadersVisible = false;
            this.dgvAttendance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttendance.Name = "dgvAttendance";
            this.dgvAttendance.TabIndex = 19;
            // 
            // colDate
            // 
            this.colDate.DataPropertyName = "date_text";
            this.colDate.FillWeight = 15F;
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colNumber
            // 
            this.colNumber.DataPropertyName = "student_number";
            this.colNumber.FillWeight = 14F;
            this.colNumber.HeaderText = "Student ID";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "full_name";
            this.colName.FillWeight = 30F;
            this.colName.HeaderText = "Full Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colCourse
            // 
            this.colCourse.DataPropertyName = "department_code";
            this.colCourse.FillWeight = 10F;
            this.colCourse.HeaderText = "Course";
            this.colCourse.Name = "colCourse";
            this.colCourse.ReadOnly = true;
            // 
            // colYear
            // 
            this.colYear.DataPropertyName = "year_level";
            this.colYear.FillWeight = 11F;
            this.colYear.HeaderText = "Year Level";
            this.colYear.Name = "colYear";
            this.colYear.ReadOnly = true;
            // 
            // colTime
            // 
            this.colTime.DataPropertyName = "time_text";
            this.colTime.FillWeight = 10F;
            this.colTime.HeaderText = "Time In";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "status";
            this.colStatus.FillWeight = 10F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = false;
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.DimGray;
            this.lblCount.Location = new System.Drawing.Point(340, 580);
            this.lblCount.Text = "";
            this.lblCount.Size = new System.Drawing.Size(945, 20);
            this.lblCount.Name = "lblCount";
            this.lblCount.TabIndex = 20;
            // 
            // AttendanceMonitoring
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AttendanceMonitoring";
            this.Controls.Add(this.sidebarMenu);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblStudent);
            this.Controls.Add(this.cmbStudent);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblCourse);
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.chkPresent);
            this.Controls.Add(this.chkAbsent);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnAllDates);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.lblEmpty);
            this.Controls.Add(this.dgvAttendance);
            this.Controls.Add(this.lblCount);
            this.Load += new System.EventHandler(this.AttendanceMonitoring_Load);
            this.Name = "AttendanceMonitoring";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SidebarMenu sidebarMenu;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private WindowButtons windowButtons;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.ComboBox cmbStudent;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.CheckBox chkPresent;
        private System.Windows.Forms.CheckBox chkAbsent;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnAllDates;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblSummaryDate;
        private System.Windows.Forms.Label lblSummaryTotal;
        private System.Windows.Forms.Label lblSummaryPresent;
        private System.Windows.Forms.Label lblSummaryAbsent;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCourse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridView dgvAttendance;
        private System.Windows.Forms.Label lblCount;
    }
}
