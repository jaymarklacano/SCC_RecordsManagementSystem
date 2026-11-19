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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttendanceMonitoring));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.sidebarMenu = new SCCRecordsManagementSystem.SidebarMenu();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.windowButtons = new SCCRecordsManagementSystem.WindowButtons();
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
            this.lblCount = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).BeginInit();
            this.SuspendLayout();
            // 
            // sidebarMenu
            // 
            this.sidebarMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.sidebarMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sidebarMenu.BackgroundImage")));
            this.sidebarMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sidebarMenu.CurrentPage = SCCRecordsManagementSystem.SidebarPage.Attendance;
            this.sidebarMenu.Location = new System.Drawing.Point(0, 0);
            this.sidebarMenu.Name = "sidebarMenu";
            this.sidebarMenu.Size = new System.Drawing.Size(320, 619);
            this.sidebarMenu.TabIndex = 0;
            this.sidebarMenu.Load += new System.EventHandler(this.sidebarMenu_Load);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.pnlHeader.Controls.Add(this.lblPageTitle);
            this.pnlHeader.Controls.Add(this.windowButtons);
            this.pnlHeader.Location = new System.Drawing.Point(320, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(985, 50);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPageTitle.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblPageTitle.Location = new System.Drawing.Point(24, 13);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(224, 27);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Attendance Records";
            // 
            // windowButtons
            // 
            this.windowButtons.BackColor = System.Drawing.Color.Transparent;
            this.windowButtons.Location = new System.Drawing.Point(891, 8);
            this.windowButtons.Name = "windowButtons";
            this.windowButtons.Size = new System.Drawing.Size(90, 34);
            this.windowButtons.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.Black;
            this.lblDate.Location = new System.Drawing.Point(340, 70);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(50, 20);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date:";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "MMM d, yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(385, 66);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ShowCheckBox = true;
            this.dtpDate.Size = new System.Drawing.Size(170, 28);
            this.dtpDate.TabIndex = 3;
            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.BackColor = System.Drawing.Color.Transparent;
            this.lblStudent.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudent.ForeColor = System.Drawing.Color.Black;
            this.lblStudent.Location = new System.Drawing.Point(572, 70);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(74, 20);
            this.lblStudent.TabIndex = 4;
            this.lblStudent.Text = "Student:";
            // 
            // cmbStudent
            // 
            this.cmbStudent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStudent.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStudent.FormattingEnabled = true;
            this.cmbStudent.Location = new System.Drawing.Point(640, 66);
            this.cmbStudent.Name = "cmbStudent";
            this.cmbStudent.Size = new System.Drawing.Size(310, 28);
            this.cmbStudent.TabIndex = 5;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.Black;
            this.lblSearch.Location = new System.Drawing.Point(965, 70);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(67, 20);
            this.lblSearch.TabIndex = 6;
            this.lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(1030, 66);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(255, 28);
            this.txtSearch.TabIndex = 7;
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.BackColor = System.Drawing.Color.Transparent;
            this.lblCourse.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourse.ForeColor = System.Drawing.Color.Black;
            this.lblCourse.Location = new System.Drawing.Point(340, 108);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(69, 20);
            this.lblCourse.TabIndex = 8;
            this.lblCourse.Text = "Course:";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(400, 104);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(165, 28);
            this.cmbDepartment.TabIndex = 9;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.ForeColor = System.Drawing.Color.Black;
            this.lblYear.Location = new System.Drawing.Point(580, 108);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(49, 20);
            this.lblYear.TabIndex = 10;
            this.lblYear.Text = "Year:";
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Items.AddRange(new object[] {
            "All Years",
            "1st Year",
            "2nd Year",
            "3rd Year",
            "4th Year"});
            this.cmbYear.Location = new System.Drawing.Point(625, 104);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(105, 28);
            this.cmbYear.TabIndex = 11;
            // 
            // chkPresent
            // 
            this.chkPresent.AutoSize = true;
            this.chkPresent.Checked = true;
            this.chkPresent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPresent.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPresent.Location = new System.Drawing.Point(745, 107);
            this.chkPresent.Name = "chkPresent";
            this.chkPresent.Size = new System.Drawing.Size(91, 24);
            this.chkPresent.TabIndex = 12;
            this.chkPresent.Text = "Present";
            this.chkPresent.UseVisualStyleBackColor = true;
            // 
            // chkAbsent
            // 
            this.chkAbsent.AutoSize = true;
            this.chkAbsent.Checked = true;
            this.chkAbsent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAbsent.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAbsent.Location = new System.Drawing.Point(830, 107);
            this.chkAbsent.Name = "chkAbsent";
            this.chkAbsent.Size = new System.Drawing.Size(85, 24);
            this.chkAbsent.TabIndex = 13;
            this.chkAbsent.Text = "Absent";
            this.chkAbsent.UseVisualStyleBackColor = true;
            // 
            // btnToday
            // 
            this.btnToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.btnToday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToday.FlatAppearance.BorderSize = 0;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.ForeColor = System.Drawing.Color.White;
            this.btnToday.Location = new System.Drawing.Point(930, 102);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(100, 30);
            this.btnToday.TabIndex = 14;
            this.btnToday.Text = "TODAY";
            this.btnToday.UseVisualStyleBackColor = false;
            // 
            // btnAllDates
            // 
            this.btnAllDates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnAllDates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllDates.FlatAppearance.BorderSize = 0;
            this.btnAllDates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllDates.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllDates.ForeColor = System.Drawing.Color.Black;
            this.btnAllDates.Location = new System.Drawing.Point(1040, 102);
            this.btnAllDates.Name = "btnAllDates";
            this.btnAllDates.Size = new System.Drawing.Size(120, 30);
            this.btnAllDates.TabIndex = 15;
            this.btnAllDates.Text = "ALL DATES";
            this.btnAllDates.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(1170, 102);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(115, 30);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "RESET";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.pnlSummary.Controls.Add(this.lblSummaryDate);
            this.pnlSummary.Controls.Add(this.lblSummaryTotal);
            this.pnlSummary.Controls.Add(this.lblSummaryPresent);
            this.pnlSummary.Controls.Add(this.lblSummaryAbsent);
            this.pnlSummary.Location = new System.Drawing.Point(340, 142);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(945, 44);
            this.pnlSummary.TabIndex = 17;
            // 
            // lblSummaryDate
            // 
            this.lblSummaryDate.AutoSize = true;
            this.lblSummaryDate.BackColor = System.Drawing.Color.Transparent;
            this.lblSummaryDate.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryDate.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSummaryDate.Location = new System.Drawing.Point(14, 13);
            this.lblSummaryDate.Name = "lblSummaryDate";
            this.lblSummaryDate.Size = new System.Drawing.Size(0, 21);
            this.lblSummaryDate.TabIndex = 0;
            // 
            // lblSummaryTotal
            // 
            this.lblSummaryTotal.AutoSize = true;
            this.lblSummaryTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblSummaryTotal.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryTotal.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSummaryTotal.Location = new System.Drawing.Point(330, 13);
            this.lblSummaryTotal.Name = "lblSummaryTotal";
            this.lblSummaryTotal.Size = new System.Drawing.Size(0, 21);
            this.lblSummaryTotal.TabIndex = 1;
            // 
            // lblSummaryPresent
            // 
            this.lblSummaryPresent.AutoSize = true;
            this.lblSummaryPresent.BackColor = System.Drawing.Color.Transparent;
            this.lblSummaryPresent.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryPresent.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblSummaryPresent.Location = new System.Drawing.Point(540, 13);
            this.lblSummaryPresent.Name = "lblSummaryPresent";
            this.lblSummaryPresent.Size = new System.Drawing.Size(0, 21);
            this.lblSummaryPresent.TabIndex = 2;
            // 
            // lblSummaryAbsent
            // 
            this.lblSummaryAbsent.AutoSize = true;
            this.lblSummaryAbsent.BackColor = System.Drawing.Color.Transparent;
            this.lblSummaryAbsent.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummaryAbsent.ForeColor = System.Drawing.Color.Firebrick;
            this.lblSummaryAbsent.Location = new System.Drawing.Point(720, 13);
            this.lblSummaryAbsent.Name = "lblSummaryAbsent";
            this.lblSummaryAbsent.Size = new System.Drawing.Size(0, 21);
            this.lblSummaryAbsent.TabIndex = 3;
            // 
            // lblEmpty
            // 
            this.lblEmpty.BackColor = System.Drawing.Color.White;
            this.lblEmpty.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblEmpty.Location = new System.Drawing.Point(341, 231);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(943, 344);
            this.lblEmpty.TabIndex = 18;
            this.lblEmpty.Text = "No attendance records found.";
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            // 
            // dgvAttendance
            // 
            this.dgvAttendance.AllowUserToAddRows = false;
            this.dgvAttendance.AllowUserToDeleteRows = false;
            this.dgvAttendance.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAttendance.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAttendance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAttendance.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dgvAttendance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAttendance.ColumnHeadersHeight = 34;
            this.dgvAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAttendance.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAttendance.EnableHeadersVisualStyles = false;
            this.dgvAttendance.Location = new System.Drawing.Point(340, 196);
            this.dgvAttendance.MultiSelect = false;
            this.dgvAttendance.Name = "dgvAttendance";
            this.dgvAttendance.ReadOnly = true;
            this.dgvAttendance.RowHeadersVisible = false;
            this.dgvAttendance.RowHeadersWidth = 51;
            this.dgvAttendance.RowTemplate.Height = 28;
            this.dgvAttendance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttendance.Size = new System.Drawing.Size(945, 380);
            this.dgvAttendance.TabIndex = 19;
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.DimGray;
            this.lblCount.Location = new System.Drawing.Point(340, 580);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(945, 20);
            this.lblCount.TabIndex = 20;
            // 
            // AttendanceMonitoring
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1305, 619);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AttendanceMonitoring";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AttendanceMonitoring";
            this.Load += new System.EventHandler(this.AttendanceMonitoring_Load);
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
