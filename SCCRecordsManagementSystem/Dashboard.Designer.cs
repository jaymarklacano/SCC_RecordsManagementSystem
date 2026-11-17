namespace SCCRecordsManagementSystem
{
    partial class Dashboard
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.btnStartAttendance = new System.Windows.Forms.Button();
            this.pnlTotalCard = new System.Windows.Forms.Panel();
            this.pnlTotalCardCaption = new System.Windows.Forms.Label();
            this.lblTotalStudents = new System.Windows.Forms.Label();
            this.lblTotalNote = new System.Windows.Forms.Label();
            this.pnlPresentCard = new System.Windows.Forms.Panel();
            this.pnlPresentCardCaption = new System.Windows.Forms.Label();
            this.lblPresentToday = new System.Windows.Forms.Label();
            this.lblPresentNote = new System.Windows.Forms.Label();
            this.pnlAbsentCard = new System.Windows.Forms.Panel();
            this.pnlAbsentCardCaption = new System.Windows.Forms.Label();
            this.lblAbsentToday = new System.Windows.Forms.Label();
            this.lblAbsentNote = new System.Windows.Forms.Label();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.chartAttendance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblChartEmpty = new System.Windows.Forms.Label();
            this.lblTodayTitle = new System.Windows.Forms.Label();
            this.lblEmptyToday = new System.Windows.Forms.Label();
            this.dgvToday = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudentNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCourse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chartAttendance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToday)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlTotalCard.SuspendLayout();
            this.pnlPresentCard.SuspendLayout();
            this.pnlAbsentCard.SuspendLayout();
            this.pnlChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarMenu
            // 
            this.sidebarMenu.Location = new System.Drawing.Point(0, 0);
            this.sidebarMenu.Size = new System.Drawing.Size(320, 619);
            this.sidebarMenu.CurrentPage = SCCRecordsManagementSystem.SidebarPage.Dashboard;
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
            this.lblPageTitle.Text = "Dashboard";
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
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblWelcome.Location = new System.Drawing.Point(340, 62);
            this.lblWelcome.Text = "Welcome, admin!";
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.TabIndex = 2;
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.BackColor = System.Drawing.Color.Transparent;
            this.lblDateTime.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblDateTime.Location = new System.Drawing.Point(342, 102);
            this.lblDateTime.Text = "";
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.TabIndex = 3;
            // 
            // btnStartAttendance
            // 
            this.btnStartAttendance.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            this.btnStartAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartAttendance.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartAttendance.ForeColor = System.Drawing.Color.White;
            this.btnStartAttendance.Location = new System.Drawing.Point(1045, 66);
            this.btnStartAttendance.Size = new System.Drawing.Size(240, 40);
            this.btnStartAttendance.Text = "START FACE RECOGNITION";
            this.btnStartAttendance.UseVisualStyleBackColor = false;
            this.btnStartAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartAttendance.FlatAppearance.BorderSize = 0;
            this.btnStartAttendance.Name = "btnStartAttendance";
            this.btnStartAttendance.TabIndex = 4;
            this.btnStartAttendance.Click += new System.EventHandler(this.btnStartAttendance_Click);
            // 
            // pnlTotalCard
            // 
            this.pnlTotalCard.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.pnlTotalCard.Location = new System.Drawing.Point(340, 142);
            this.pnlTotalCard.Size = new System.Drawing.Size(295, 96);
            this.pnlTotalCard.Controls.Add(this.pnlTotalCardCaption);
            this.pnlTotalCard.Controls.Add(this.lblTotalStudents);
            this.pnlTotalCard.Controls.Add(this.lblTotalNote);
            this.pnlTotalCard.Name = "pnlTotalCard";
            this.pnlTotalCard.TabIndex = 5;
            // 
            // pnlTotalCardCaption
            // 
            this.pnlTotalCardCaption.AutoSize = true;
            this.pnlTotalCardCaption.BackColor = System.Drawing.Color.Transparent;
            this.pnlTotalCardCaption.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTotalCardCaption.ForeColor = System.Drawing.Color.Black;
            this.pnlTotalCardCaption.Location = new System.Drawing.Point(14, 8);
            this.pnlTotalCardCaption.Text = "Total Students";
            this.pnlTotalCardCaption.Name = "pnlTotalCardCaption";
            this.pnlTotalCardCaption.TabIndex = 0;
            // 
            // lblTotalStudents
            // 
            this.lblTotalStudents.AutoSize = true;
            this.lblTotalStudents.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalStudents.Font = new System.Drawing.Font("Cambria", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStudents.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalStudents.Location = new System.Drawing.Point(12, 28);
            this.lblTotalStudents.Text = "0";
            this.lblTotalStudents.Name = "lblTotalStudents";
            this.lblTotalStudents.TabIndex = 1;
            // 
            // lblTotalNote
            // 
            this.lblTotalNote.AutoSize = true;
            this.lblTotalNote.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalNote.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalNote.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblTotalNote.Location = new System.Drawing.Point(16, 72);
            this.lblTotalNote.Text = "";
            this.lblTotalNote.Name = "lblTotalNote";
            this.lblTotalNote.TabIndex = 2;
            // 
            // pnlPresentCard
            // 
            this.pnlPresentCard.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.pnlPresentCard.Location = new System.Drawing.Point(665, 142);
            this.pnlPresentCard.Size = new System.Drawing.Size(295, 96);
            this.pnlPresentCard.Controls.Add(this.pnlPresentCardCaption);
            this.pnlPresentCard.Controls.Add(this.lblPresentToday);
            this.pnlPresentCard.Controls.Add(this.lblPresentNote);
            this.pnlPresentCard.Name = "pnlPresentCard";
            this.pnlPresentCard.TabIndex = 6;
            // 
            // pnlPresentCardCaption
            // 
            this.pnlPresentCardCaption.AutoSize = true;
            this.pnlPresentCardCaption.BackColor = System.Drawing.Color.Transparent;
            this.pnlPresentCardCaption.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPresentCardCaption.ForeColor = System.Drawing.Color.Black;
            this.pnlPresentCardCaption.Location = new System.Drawing.Point(14, 8);
            this.pnlPresentCardCaption.Text = "Present Today";
            this.pnlPresentCardCaption.Name = "pnlPresentCardCaption";
            this.pnlPresentCardCaption.TabIndex = 0;
            // 
            // lblPresentToday
            // 
            this.lblPresentToday.AutoSize = true;
            this.lblPresentToday.BackColor = System.Drawing.Color.Transparent;
            this.lblPresentToday.Font = new System.Drawing.Font("Cambria", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentToday.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblPresentToday.Location = new System.Drawing.Point(12, 28);
            this.lblPresentToday.Text = "0";
            this.lblPresentToday.Name = "lblPresentToday";
            this.lblPresentToday.TabIndex = 1;
            // 
            // lblPresentNote
            // 
            this.lblPresentNote.AutoSize = true;
            this.lblPresentNote.BackColor = System.Drawing.Color.Transparent;
            this.lblPresentNote.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentNote.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblPresentNote.Location = new System.Drawing.Point(16, 72);
            this.lblPresentNote.Text = "";
            this.lblPresentNote.Name = "lblPresentNote";
            this.lblPresentNote.TabIndex = 2;
            // 
            // pnlAbsentCard
            // 
            this.pnlAbsentCard.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.pnlAbsentCard.Location = new System.Drawing.Point(990, 142);
            this.pnlAbsentCard.Size = new System.Drawing.Size(295, 96);
            this.pnlAbsentCard.Controls.Add(this.pnlAbsentCardCaption);
            this.pnlAbsentCard.Controls.Add(this.lblAbsentToday);
            this.pnlAbsentCard.Controls.Add(this.lblAbsentNote);
            this.pnlAbsentCard.Name = "pnlAbsentCard";
            this.pnlAbsentCard.TabIndex = 7;
            // 
            // pnlAbsentCardCaption
            // 
            this.pnlAbsentCardCaption.AutoSize = true;
            this.pnlAbsentCardCaption.BackColor = System.Drawing.Color.Transparent;
            this.pnlAbsentCardCaption.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAbsentCardCaption.ForeColor = System.Drawing.Color.Black;
            this.pnlAbsentCardCaption.Location = new System.Drawing.Point(14, 8);
            this.pnlAbsentCardCaption.Text = "Absent Today";
            this.pnlAbsentCardCaption.Name = "pnlAbsentCardCaption";
            this.pnlAbsentCardCaption.TabIndex = 0;
            // 
            // lblAbsentToday
            // 
            this.lblAbsentToday.AutoSize = true;
            this.lblAbsentToday.BackColor = System.Drawing.Color.Transparent;
            this.lblAbsentToday.Font = new System.Drawing.Font("Cambria", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsentToday.ForeColor = System.Drawing.Color.Firebrick;
            this.lblAbsentToday.Location = new System.Drawing.Point(12, 28);
            this.lblAbsentToday.Text = "0";
            this.lblAbsentToday.Name = "lblAbsentToday";
            this.lblAbsentToday.TabIndex = 1;
            // 
            // lblAbsentNote
            // 
            this.lblAbsentNote.AutoSize = true;
            this.lblAbsentNote.BackColor = System.Drawing.Color.Transparent;
            this.lblAbsentNote.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsentNote.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblAbsentNote.Location = new System.Drawing.Point(16, 72);
            this.lblAbsentNote.Text = "";
            this.lblAbsentNote.Name = "lblAbsentNote";
            this.lblAbsentNote.TabIndex = 2;
            // 
            // lblChartTitle
            // 
            this.lblChartTitle.AutoSize = true;
            this.lblChartTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblChartTitle.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChartTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblChartTitle.Location = new System.Drawing.Point(340, 252);
            this.lblChartTitle.Text = "Attendance Overview";
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.TabIndex = 8;
            // 
            // pnlChart
            // 
            this.pnlChart.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlChart.Location = new System.Drawing.Point(340, 276);
            this.pnlChart.Size = new System.Drawing.Size(330, 314);
            this.pnlChart.Controls.Add(this.chartAttendance);
            this.pnlChart.Controls.Add(this.lblChartEmpty);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.TabIndex = 9;
            // 
            // chartAttendance
            // 
            this.chartAttendance.BackColor = System.Drawing.Color.LightSteelBlue;
            this.chartAttendance.Location = new System.Drawing.Point(10, 10);
            this.chartAttendance.Size = new System.Drawing.Size(310, 294);
            this.chartAttendance.Name = "chartAttendance";
            this.chartAttendance.TabIndex = 0;
            // 
            // lblChartEmpty
            // 
            this.lblChartEmpty.AutoSize = false;
            this.lblChartEmpty.BackColor = System.Drawing.Color.Transparent;
            this.lblChartEmpty.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChartEmpty.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblChartEmpty.Location = new System.Drawing.Point(10, 10);
            this.lblChartEmpty.Text = "No students yet.";
            this.lblChartEmpty.Size = new System.Drawing.Size(310, 294);
            this.lblChartEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChartEmpty.Visible = false;
            this.lblChartEmpty.Name = "lblChartEmpty";
            this.lblChartEmpty.TabIndex = 1;
            // 
            // lblTodayTitle
            // 
            this.lblTodayTitle.AutoSize = true;
            this.lblTodayTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTodayTitle.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodayTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTodayTitle.Location = new System.Drawing.Point(690, 252);
            this.lblTodayTitle.Text = "Today's Attendance";
            this.lblTodayTitle.Name = "lblTodayTitle";
            this.lblTodayTitle.TabIndex = 10;
            // 
            // lblEmptyToday
            // 
            this.lblEmptyToday.AutoSize = false;
            this.lblEmptyToday.BackColor = System.Drawing.Color.White;
            this.lblEmptyToday.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmptyToday.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblEmptyToday.Location = new System.Drawing.Point(691, 311);
            this.lblEmptyToday.Text = "No attendance has been recorded today.";
            this.lblEmptyToday.Size = new System.Drawing.Size(593, 245);
            this.lblEmptyToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmptyToday.Visible = false;
            this.lblEmptyToday.Name = "lblEmptyToday";
            this.lblEmptyToday.TabIndex = 11;
            // 
            // dgvToday
            // 
            System.Windows.Forms.DataGridViewCellStyle dgvTodayHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvTodayCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvTodayAltStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dgvTodayHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvTodayHeaderStyle.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            dgvTodayHeaderStyle.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvTodayHeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvTodayHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            dgvTodayHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvTodayCellStyle.BackColor = System.Drawing.Color.White;
            dgvTodayCellStyle.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvTodayCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvTodayCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            dgvTodayCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgvTodayAltStyle.BackColor = System.Drawing.Color.FromArgb(238, 244, 249);
            dgvTodayAltStyle.SelectionBackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            dgvTodayAltStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvToday.ColumnHeadersDefaultCellStyle = dgvTodayHeaderStyle;
            this.dgvToday.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvToday.DefaultCellStyle = dgvTodayCellStyle;
            this.dgvToday.AlternatingRowsDefaultCellStyle = dgvTodayAltStyle;
            this.dgvToday.RowTemplate.Height = 28;
            this.dgvToday.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colStudentNumber,
            this.colName,
            this.colCourse,
            this.colYear});
            this.dgvToday.Location = new System.Drawing.Point(690, 276);
            this.dgvToday.Size = new System.Drawing.Size(595, 284);
            this.dgvToday.AllowUserToAddRows = false;
            this.dgvToday.AllowUserToDeleteRows = false;
            this.dgvToday.AllowUserToResizeRows = false;
            this.dgvToday.AutoGenerateColumns = false;
            this.dgvToday.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvToday.BackgroundColor = System.Drawing.Color.White;
            this.dgvToday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvToday.ColumnHeadersHeight = 34;
            this.dgvToday.EnableHeadersVisualStyles = false;
            this.dgvToday.MultiSelect = false;
            this.dgvToday.ReadOnly = true;
            this.dgvToday.RowHeadersVisible = false;
            this.dgvToday.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvToday.Name = "dgvToday";
            this.dgvToday.TabIndex = 12;
            // 
            // colTime
            // 
            this.colTime.DataPropertyName = "time_text";
            this.colTime.FillWeight = 18F;
            this.colTime.HeaderText = "Time In";
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colStudentNumber
            // 
            this.colStudentNumber.DataPropertyName = "student_number";
            this.colStudentNumber.FillWeight = 22F;
            this.colStudentNumber.HeaderText = "Student ID";
            this.colStudentNumber.Name = "colStudentNumber";
            this.colStudentNumber.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "full_name";
            this.colName.FillWeight = 38F;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colCourse
            // 
            this.colCourse.DataPropertyName = "department_code";
            this.colCourse.FillWeight = 14F;
            this.colCourse.HeaderText = "Course";
            this.colCourse.Name = "colCourse";
            this.colCourse.ReadOnly = true;
            // 
            // colYear
            // 
            this.colYear.DataPropertyName = "year_level";
            this.colYear.FillWeight = 16F;
            this.colYear.HeaderText = "Year";
            this.colYear.Name = "colYear";
            this.colYear.ReadOnly = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatus.Location = new System.Drawing.Point(690, 568);
            this.lblStatus.Text = "";
            this.lblStatus.Size = new System.Drawing.Size(595, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 13;
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Enabled = true;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 15000;
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // Dashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Controls.Add(this.sidebarMenu);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.btnStartAttendance);
            this.Controls.Add(this.pnlTotalCard);
            this.Controls.Add(this.pnlPresentCard);
            this.Controls.Add(this.pnlAbsentCard);
            this.Controls.Add(this.lblChartTitle);
            this.Controls.Add(this.pnlChart);
            this.Controls.Add(this.lblTodayTitle);
            this.Controls.Add(this.lblEmptyToday);
            this.Controls.Add(this.dgvToday);
            this.Controls.Add(this.lblStatus);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.Name = "Dashboard";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTotalCard.ResumeLayout(false);
            this.pnlTotalCard.PerformLayout();
            this.pnlPresentCard.ResumeLayout(false);
            this.pnlPresentCard.PerformLayout();
            this.pnlAbsentCard.ResumeLayout(false);
            this.pnlAbsentCard.PerformLayout();
            this.pnlChart.ResumeLayout(false);
            this.pnlChart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttendance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToday)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SidebarMenu sidebarMenu;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private WindowButtons windowButtons;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Button btnStartAttendance;
        private System.Windows.Forms.Panel pnlTotalCard;
        private System.Windows.Forms.Label pnlTotalCardCaption;
        private System.Windows.Forms.Label lblTotalStudents;
        private System.Windows.Forms.Label lblTotalNote;
        private System.Windows.Forms.Panel pnlPresentCard;
        private System.Windows.Forms.Label pnlPresentCardCaption;
        private System.Windows.Forms.Label lblPresentToday;
        private System.Windows.Forms.Label lblPresentNote;
        private System.Windows.Forms.Panel pnlAbsentCard;
        private System.Windows.Forms.Label pnlAbsentCardCaption;
        private System.Windows.Forms.Label lblAbsentToday;
        private System.Windows.Forms.Label lblAbsentNote;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.Panel pnlChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAttendance;
        private System.Windows.Forms.Label lblChartEmpty;
        private System.Windows.Forms.Label lblTodayTitle;
        private System.Windows.Forms.Label lblEmptyToday;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCourse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridView dgvToday;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Timer timerRefresh;
    }
}
