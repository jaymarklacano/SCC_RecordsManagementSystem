namespace SCCRecordsManagementSystem
{
    partial class ReportsAndAnalysis
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
            this.lblNoRecord = new System.Windows.Forms.Label();
            this.lblReportInfo = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.txtSearchStudent = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cmbYearLevel = new System.Windows.Forms.ComboBox();
            this.lblYearLevel = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.lblReportType = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOrganizer = new System.Windows.Forms.Label();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.lblOrganizerTitle = new System.Windows.Forms.Label();
            this.lblScheduleTitle = new System.Windows.Forms.Label();
            this.lblVenue = new System.Windows.Forms.Label();
            this.lblVenueTitle = new System.Windows.Forms.Label();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.cmbEventSelect = new System.Windows.Forms.ComboBox();
            this.lblSelectEvent = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkAbsent = new System.Windows.Forms.CheckBox();
            this.chkPresent = new System.Windows.Forms.CheckBox();
            this.chtAttendanceDepartment = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblFilterTitle = new System.Windows.Forms.Label();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.dtgRecord = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRepLogout = new System.Windows.Forms.Button();
            this.btnRepDashboard = new System.Windows.Forms.Button();
            this.btnRepRules = new System.Windows.Forms.Button();
            this.btnRepObligation = new System.Windows.Forms.Button();
            this.btnRepReports = new System.Windows.Forms.Button();
            this.btnRepAttendance = new System.Windows.Forms.Button();
            this.btnRepEvent = new System.Windows.Forms.Button();
            this.btnRepFace = new System.Windows.Forms.Button();
            this.btnRepOfficer = new System.Windows.Forms.Button();
            this.btnRepStudent = new System.Windows.Forms.Button();
            this.btnRepDepartment = new System.Windows.Forms.Button();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtAttendanceDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecord)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.lblTitle);
            this.panel5.Location = new System.Drawing.Point(321, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(985, 48);
            this.panel5.TabIndex = 6;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(107, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(187, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reports and Analysis";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(300, 6);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Size = new System.Drawing.Size(79, 16);
            this.lblFromDate.TabIndex = 10;
            this.lblFromDate.Text = "From :";
            // 
            // lblReportType
            // 
            this.lblReportType.AutoSize = true;
            this.lblReportType.Location = new System.Drawing.Point(590, 6);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportType.Size = new System.Drawing.Size(89, 16);
            this.lblReportType.TabIndex = 11;
            this.lblReportType.Text = "Report Type :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnExportCsv);
            this.panel1.Controls.Add(this.btnClearFilters);
            this.panel1.Controls.Add(this.txtSearchStudent);
            this.panel1.Controls.Add(this.lblSearch);
            this.panel1.Controls.Add(this.cmbYearLevel);
            this.panel1.Controls.Add(this.lblYearLevel);
            this.panel1.Controls.Add(this.cmbDepartment);
            this.panel1.Controls.Add(this.lblDepartment);
            this.panel1.Controls.Add(this.dtpToDate);
            this.panel1.Controls.Add(this.lblToDate);
            this.panel1.Controls.Add(this.lblOrganizer);
            this.panel1.Controls.Add(this.lblSchedule);
            this.panel1.Controls.Add(this.lblOrganizerTitle);
            this.panel1.Controls.Add(this.lblScheduleTitle);
            this.panel1.Controls.Add(this.lblVenue);
            this.panel1.Controls.Add(this.lblVenueTitle);
            this.panel1.Controls.Add(this.btnGenerateReport);
            this.panel1.Controls.Add(this.dtpFromDate);
            this.panel1.Controls.Add(this.cmbReportType);
            this.panel1.Controls.Add(this.cmbEventSelect);
            this.panel1.Controls.Add(this.lblSelectEvent);
            this.panel1.Controls.Add(this.lblFromDate);
            this.panel1.Controls.Add(this.lblReportType);
            this.panel1.Location = new System.Drawing.Point(330, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 160);
            this.panel1.TabIndex = 12;
            // 
            // lblOrganizer
            // 
            this.lblOrganizer.AutoSize = false;
            this.lblOrganizer.Location = new System.Drawing.Point(735, 126);
            this.lblOrganizer.Name = "lblOrganizer";
            this.lblOrganizer.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrganizer.AutoEllipsis = true;
            this.lblOrganizer.Size = new System.Drawing.Size(215, 18);
            this.lblOrganizer.TabIndex = 22;
            this.lblOrganizer.Text = "-";
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = false;
            this.lblSchedule.Location = new System.Drawing.Point(380, 126);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchedule.AutoEllipsis = true;
            this.lblSchedule.Size = new System.Drawing.Size(260, 18);
            this.lblSchedule.TabIndex = 21;
            this.lblSchedule.Text = "-";
            // 
            // lblOrganizerTitle
            // 
            this.lblOrganizerTitle.AutoSize = true;
            this.lblOrganizerTitle.Location = new System.Drawing.Point(650, 126);
            this.lblOrganizerTitle.Name = "lblOrganizerTitle";
            this.lblOrganizerTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrganizerTitle.Size = new System.Drawing.Size(71, 16);
            this.lblOrganizerTitle.TabIndex = 20;
            this.lblOrganizerTitle.Text = "Organizer :";
            // 
            // lblScheduleTitle
            // 
            this.lblScheduleTitle.AutoSize = true;
            this.lblScheduleTitle.Location = new System.Drawing.Point(300, 126);
            this.lblScheduleTitle.Name = "lblScheduleTitle";
            this.lblScheduleTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScheduleTitle.Size = new System.Drawing.Size(70, 16);
            this.lblScheduleTitle.TabIndex = 19;
            this.lblScheduleTitle.Text = "Schedule :";
            // 
            // lblVenue
            // 
            this.lblVenue.AutoSize = false;
            this.lblVenue.Location = new System.Drawing.Point(72, 126);
            this.lblVenue.Name = "lblVenue";
            this.lblVenue.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVenue.AutoEllipsis = true;
            this.lblVenue.Size = new System.Drawing.Size(220, 18);
            this.lblVenue.TabIndex = 18;
            this.lblVenue.Text = "-";
            // 
            // lblVenueTitle
            // 
            this.lblVenueTitle.AutoSize = true;
            this.lblVenueTitle.Location = new System.Drawing.Point(15, 126);
            this.lblVenueTitle.Name = "lblVenueTitle";
            this.lblVenueTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVenueTitle.Size = new System.Drawing.Size(52, 16);
            this.lblVenueTitle.TabIndex = 17;
            this.lblVenueTitle.Text = "Venue :";
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(805, 8);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            this.btnGenerateReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnGenerateReport.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerateReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnGenerateReport.Size = new System.Drawing.Size(145, 42);
            this.btnGenerateReport.TabIndex = 16;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = false;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(300, 26);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.filter_Changed);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Size = new System.Drawing.Size(130, 22);
            this.dtpFromDate.TabIndex = 15;
            // 
            // cmbReportType
            // 
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Items.AddRange(new object[] {
            "Attendance per Session",
            "Attendance per Department",
            "Student Attendance Summary",
            "Most Absent Students",
            "Missing Log Out",
            "Detailed Attendance List",
            "Obligation Balances"});
            this.cmbReportType.Location = new System.Drawing.Point(590, 26);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.SelectedIndexChanged += new System.EventHandler(this.cmbReportType_SelectedIndexChanged);
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.Size = new System.Drawing.Size(200, 24);
            this.cmbReportType.TabIndex = 14;
            // 
            // cmbEventSelect
            // 
            this.cmbEventSelect.FormattingEnabled = true;
            this.cmbEventSelect.Location = new System.Drawing.Point(15, 26);
            this.cmbEventSelect.Name = "cmbEventSelect";
            this.cmbEventSelect.SelectedIndexChanged += new System.EventHandler(this.cmbEventSelect_SelectedIndexChanged);
            this.cmbEventSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventSelect.Size = new System.Drawing.Size(270, 24);
            this.cmbEventSelect.TabIndex = 13;
            // 
            // lblSelectEvent
            // 
            this.lblSelectEvent.AutoSize = true;
            this.lblSelectEvent.Location = new System.Drawing.Point(15, 6);
            this.lblSelectEvent.Name = "lblSelectEvent";
            this.lblSelectEvent.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectEvent.Size = new System.Drawing.Size(181, 16);
            this.lblSelectEvent.TabIndex = 12;
            this.lblSelectEvent.Text = "Event :";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.lblSummary);
            this.panel3.Controls.Add(this.chkAbsent);
            this.panel3.Controls.Add(this.chkPresent);
            this.panel3.Controls.Add(this.chtAttendanceDepartment);
            this.panel3.Location = new System.Drawing.Point(331, 288);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(962, 172);
            this.panel3.TabIndex = 28;
            // 
            // chkAbsent
            // 
            this.chkAbsent.AutoSize = true;
            this.chkAbsent.Location = new System.Drawing.Point(843, 13);
            this.chkAbsent.Name = "chkAbsent";
            this.chkAbsent.ForeColor = System.Drawing.Color.Firebrick;
            this.chkAbsent.CheckedChanged += new System.EventHandler(this.filter_Changed);
            this.chkAbsent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAbsent.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAbsent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAbsent.Checked = true;
            this.chkAbsent.Size = new System.Drawing.Size(71, 20);
            this.chkAbsent.TabIndex = 2;
            this.chkAbsent.Text = "Absent";
            this.chkAbsent.UseVisualStyleBackColor = true;
            // 
            // chkPresent
            // 
            this.chkPresent.AutoSize = true;
            this.chkPresent.Location = new System.Drawing.Point(749, 13);
            this.chkPresent.Name = "chkPresent";
            this.chkPresent.ForeColor = System.Drawing.Color.SeaGreen;
            this.chkPresent.CheckedChanged += new System.EventHandler(this.filter_Changed);
            this.chkPresent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkPresent.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPresent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPresent.Checked = true;
            this.chkPresent.Size = new System.Drawing.Size(75, 20);
            this.chkPresent.TabIndex = 1;
            this.chkPresent.Text = "Present";
            this.chkPresent.UseVisualStyleBackColor = true;
            // 
            // chtAttendanceDepartment
            // 
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.Name = "ChartArea1";
            this.chtAttendanceDepartment.ChartAreas.Add(chartArea1);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chtAttendanceDepartment.Legends.Add(legend1);
            this.chtAttendanceDepartment.Location = new System.Drawing.Point(9, 39);
            this.chtAttendanceDepartment.Name = "chtAttendanceDepartment";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Color = System.Drawing.Color.SeaGreen;
            series1.IsValueShownAsLabel = true;
            series1.Name = "Present";
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.Firebrick;
            series2.IsValueShownAsLabel = true;
            series2.Legend = "Legend1";
            series2.Name = "Absent";
            this.chtAttendanceDepartment.Series.Add(series1);
            this.chtAttendanceDepartment.Series.Add(series2);
            this.chtAttendanceDepartment.Size = new System.Drawing.Size(945, 128);
            this.chtAttendanceDepartment.TabIndex = 0;
            this.chtAttendanceDepartment.Text = "Attendance Per Department";
            // 
            // lblFilterTitle
            // 
            this.lblFilterTitle.AutoSize = true;
            this.lblFilterTitle.Location = new System.Drawing.Point(336, 69);
            this.lblFilterTitle.Name = "lblFilterTitle";
            this.lblFilterTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblFilterTitle.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterTitle.Size = new System.Drawing.Size(261, 16);
            this.lblFilterTitle.TabIndex = 23;
            this.lblFilterTitle.Text = "Event Search and Attendance Filter Criteria";
            // 
            // lblChartTitle
            // 
            this.lblChartTitle.AutoSize = true;
            this.lblChartTitle.Location = new System.Drawing.Point(337, 268);
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblChartTitle.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChartTitle.Size = new System.Drawing.Size(172, 16);
            this.lblChartTitle.TabIndex = 29;
            this.lblChartTitle.Text = "Attendance Per Department";
            // 
            // dtgRecord
            // 
            this.dtgRecord.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dtgRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRecord.Location = new System.Drawing.Point(330, 466);
            this.dtgRecord.Name = "dtgRecord";
            this.dtgRecord.ReadOnly = true;
            this.dtgRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgRecord.RowHeadersVisible = false;
            this.dtgRecord.MultiSelect = false;
            this.dtgRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgRecord.AllowUserToResizeRows = false;
            this.dtgRecord.AllowUserToDeleteRows = false;
            this.dtgRecord.AllowUserToAddRows = false;
            this.dtgRecord.RowHeadersWidth = 51;
            this.dtgRecord.RowTemplate.Height = 24;
            this.dtgRecord.Size = new System.Drawing.Size(962, 141);
            this.dtgRecord.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.panel2.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._904439575_1911191946735306_1039497458727327980_n1;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.btnRepLogout);
            this.panel2.Controls.Add(this.btnRepDashboard);
            this.panel2.Controls.Add(this.btnRepRules);
            this.panel2.Controls.Add(this.btnRepObligation);
            this.panel2.Controls.Add(this.btnRepReports);
            this.panel2.Controls.Add(this.btnRepAttendance);
            this.panel2.Controls.Add(this.btnRepEvent);
            this.panel2.Controls.Add(this.btnRepFace);
            this.panel2.Controls.Add(this.btnRepOfficer);
            this.panel2.Controls.Add(this.btnRepStudent);
            this.panel2.Controls.Add(this.btnRepDepartment);
            this.panel2.Location = new System.Drawing.Point(-6, -3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 624);
            this.panel2.TabIndex = 27;
            // 
            // btnRepLogout
            // 
            this.btnRepLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnRepLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepLogout.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(75)))), ((int)(((byte)(80)))));
            this.btnRepLogout.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_log_out_24__2_;
            this.btnRepLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepLogout.Location = new System.Drawing.Point(22, 560);
            this.btnRepLogout.Name = "btnRepLogout";
            this.btnRepLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepLogout.Click += new System.EventHandler(this.btnRepLogout_Click);
            this.btnRepLogout.Size = new System.Drawing.Size(277, 38);
            this.btnRepLogout.TabIndex = 11;
            this.btnRepLogout.Text = "   LogOut";
            this.btnRepLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepLogout.UseVisualStyleBackColor = false;
            // 
            // btnRepDashboard
            // 
            this.btnRepDashboard.BackColor = System.Drawing.Color.Transparent;
            this.btnRepDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepDashboard.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepDashboard.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepDashboard.Image = global::SCCRecordsManagementSystem.Properties.Resources.house__1_;
            this.btnRepDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepDashboard.Location = new System.Drawing.Point(22, 123);
            this.btnRepDashboard.Name = "btnRepDashboard";
            this.btnRepDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepDashboard.Click += new System.EventHandler(this.btnRepDashboard_Click);
            this.btnRepDashboard.Size = new System.Drawing.Size(277, 36);
            this.btnRepDashboard.TabIndex = 1;
            this.btnRepDashboard.Text = "   Dashboard";
            this.btnRepDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepDashboard.UseVisualStyleBackColor = false;
            // 
            // btnRepRules
            // 
            this.btnRepRules.BackColor = System.Drawing.Color.Transparent;
            this.btnRepRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepRules.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepRules.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepRules.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_rules_book_24;
            this.btnRepRules.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepRules.Location = new System.Drawing.Point(22, 465);
            this.btnRepRules.Name = "btnRepRules";
            this.btnRepRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepRules.Click += new System.EventHandler(this.btnRepRules_Click);
            this.btnRepRules.Size = new System.Drawing.Size(277, 36);
            this.btnRepRules.TabIndex = 10;
            this.btnRepRules.Text = "   Rules and Permission";
            this.btnRepRules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepRules.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepRules.UseVisualStyleBackColor = false;
            // 
            // btnRepObligation
            // 
            this.btnRepObligation.BackColor = System.Drawing.Color.Transparent;
            this.btnRepObligation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepObligation.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepObligation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepObligation.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_user_24;
            this.btnRepObligation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepObligation.Location = new System.Drawing.Point(22, 427);
            this.btnRepObligation.Name = "btnRepObligation";
            this.btnRepObligation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepObligation.Click += new System.EventHandler(this.btnRepObligation_Click);
            this.btnRepObligation.Size = new System.Drawing.Size(277, 36);
            this.btnRepObligation.TabIndex = 9;
            this.btnRepObligation.Text = "Obligations";
            this.btnRepObligation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepObligation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepObligation.UseVisualStyleBackColor = false;
            // 
            // btnRepReports
            // 
            this.btnRepReports.BackColor = System.Drawing.Color.Transparent;
            this.btnRepReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepReports.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepReports.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepReports.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_analysis_24;
            this.btnRepReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepReports.Location = new System.Drawing.Point(22, 389);
            this.btnRepReports.Name = "btnRepReports";
            this.btnRepReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepReports.Click += new System.EventHandler(this.btnRepReports_Click);
            this.btnRepReports.Size = new System.Drawing.Size(277, 36);
            this.btnRepReports.TabIndex = 8;
            this.btnRepReports.Text = "   Reports and Analysis";
            this.btnRepReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepReports.UseVisualStyleBackColor = false;
            // 
            // btnRepAttendance
            // 
            this.btnRepAttendance.BackColor = System.Drawing.Color.Transparent;
            this.btnRepAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepAttendance.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepAttendance.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepAttendance.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_pass_fail_24;
            this.btnRepAttendance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepAttendance.Location = new System.Drawing.Point(22, 351);
            this.btnRepAttendance.Name = "btnRepAttendance";
            this.btnRepAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepAttendance.Click += new System.EventHandler(this.btnRepAttendance_Click);
            this.btnRepAttendance.Size = new System.Drawing.Size(277, 36);
            this.btnRepAttendance.TabIndex = 7;
            this.btnRepAttendance.Text = "   Attendance Monitoring";
            this.btnRepAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepAttendance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepAttendance.UseVisualStyleBackColor = false;
            // 
            // btnRepEvent
            // 
            this.btnRepEvent.BackColor = System.Drawing.Color.Transparent;
            this.btnRepEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepEvent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepEvent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepEvent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_event_accepted_tentatively_24;
            this.btnRepEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepEvent.Location = new System.Drawing.Point(22, 313);
            this.btnRepEvent.Name = "btnRepEvent";
            this.btnRepEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepEvent.Click += new System.EventHandler(this.btnRepEvent_Click);
            this.btnRepEvent.Size = new System.Drawing.Size(277, 36);
            this.btnRepEvent.TabIndex = 6;
            this.btnRepEvent.Text = "   Event Management";
            this.btnRepEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepEvent.UseVisualStyleBackColor = false;
            // 
            // btnRepFace
            // 
            this.btnRepFace.BackColor = System.Drawing.Color.Transparent;
            this.btnRepFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepFace.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepFace.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepFace.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_facial_recognition_24;
            this.btnRepFace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepFace.Location = new System.Drawing.Point(22, 275);
            this.btnRepFace.Name = "btnRepFace";
            this.btnRepFace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepFace.Click += new System.EventHandler(this.btnRepFace_Click);
            this.btnRepFace.Size = new System.Drawing.Size(277, 36);
            this.btnRepFace.TabIndex = 5;
            this.btnRepFace.Text = "   Face Recognition";
            this.btnRepFace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepFace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepFace.UseVisualStyleBackColor = false;
            // 
            // btnRepOfficer
            // 
            this.btnRepOfficer.BackColor = System.Drawing.Color.Transparent;
            this.btnRepOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepOfficer.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepOfficer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepOfficer.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_management_24;
            this.btnRepOfficer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepOfficer.Location = new System.Drawing.Point(22, 237);
            this.btnRepOfficer.Name = "btnRepOfficer";
            this.btnRepOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepOfficer.Click += new System.EventHandler(this.btnRepOfficer_Click);
            this.btnRepOfficer.Size = new System.Drawing.Size(277, 36);
            this.btnRepOfficer.TabIndex = 4;
            this.btnRepOfficer.Text = "   Officer Records";
            this.btnRepOfficer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepOfficer.UseVisualStyleBackColor = false;
            // 
            // btnRepStudent
            // 
            this.btnRepStudent.BackColor = System.Drawing.Color.Transparent;
            this.btnRepStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepStudent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepStudent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepStudent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_what_i_do_241;
            this.btnRepStudent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepStudent.Location = new System.Drawing.Point(22, 199);
            this.btnRepStudent.Name = "btnRepStudent";
            this.btnRepStudent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepStudent.Click += new System.EventHandler(this.btnRepStudent_Click);
            this.btnRepStudent.Size = new System.Drawing.Size(277, 36);
            this.btnRepStudent.TabIndex = 3;
            this.btnRepStudent.Text = "   Student Records";
            this.btnRepStudent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepStudent.UseVisualStyleBackColor = false;
            // 
            // btnRepDepartment
            // 
            this.btnRepDepartment.BackColor = System.Drawing.Color.Transparent;
            this.btnRepDepartment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepDepartment.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepDepartment.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRepDepartment.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_building_24__1_;
            this.btnRepDepartment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepDepartment.Location = new System.Drawing.Point(22, 161);
            this.btnRepDepartment.Name = "btnRepDepartment";
            this.btnRepDepartment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepDepartment.Click += new System.EventHandler(this.btnRepDepartment_Click);
            this.btnRepDepartment.Size = new System.Drawing.Size(277, 36);
            this.btnRepDepartment.TabIndex = 2;
            this.btnRepDepartment.Text = "   Departments";
            this.btnRepDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepDepartment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRepDepartment.UseVisualStyleBackColor = false;
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.BackColor = System.Drawing.Color.Transparent;
            this.lblToDate.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(445, 6);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(40, 17);
            this.lblToDate.TabIndex = 0;
            this.lblToDate.Text = "To :";
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(445, 26);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(130, 22);
            this.dtpToDate.TabIndex = 0;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.filter_Changed);
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.BackColor = System.Drawing.Color.Transparent;
            this.lblDepartment.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.Location = new System.Drawing.Point(15, 60);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(100, 17);
            this.lblDepartment.TabIndex = 0;
            this.lblDepartment.Text = "Department :";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(15, 80);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(270, 24);
            this.cmbDepartment.TabIndex = 0;
            this.cmbDepartment.SelectedIndexChanged += new System.EventHandler(this.filter_Changed);
            // 
            // lblYearLevel
            // 
            this.lblYearLevel.AutoSize = true;
            this.lblYearLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblYearLevel.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearLevel.Location = new System.Drawing.Point(300, 60);
            this.lblYearLevel.Name = "lblYearLevel";
            this.lblYearLevel.Size = new System.Drawing.Size(90, 17);
            this.lblYearLevel.TabIndex = 0;
            this.lblYearLevel.Text = "Year Level :";
            // 
            // cmbYearLevel
            // 
            this.cmbYearLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearLevel.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYearLevel.FormattingEnabled = true;
            this.cmbYearLevel.Items.AddRange(new object[] {
            "All Year Levels",
            "1st Year",
            "2nd Year",
            "3rd Year",
            "4th Year"});
            this.cmbYearLevel.Location = new System.Drawing.Point(300, 80);
            this.cmbYearLevel.Name = "cmbYearLevel";
            this.cmbYearLevel.Size = new System.Drawing.Size(130, 24);
            this.cmbYearLevel.TabIndex = 0;
            this.cmbYearLevel.SelectedIndexChanged += new System.EventHandler(this.filter_Changed);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(445, 60);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(220, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search Student (ID or Name) :";
            // 
            // txtSearchStudent
            // 
            this.txtSearchStudent.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchStudent.Location = new System.Drawing.Point(445, 80);
            this.txtSearchStudent.Name = "txtSearchStudent";
            this.txtSearchStudent.Size = new System.Drawing.Size(230, 24);
            this.txtSearchStudent.TabIndex = 0;
            this.txtSearchStudent.MaxLength = 60;
            this.txtSearchStudent.TextChanged += new System.EventHandler(this.filter_Changed);
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnClearFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearFilters.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearFilters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnClearFilters.Location = new System.Drawing.Point(690, 78);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(100, 28);
            this.btnClearFilters.TabIndex = 0;
            this.btnClearFilters.Text = "Clear Filters";
            this.btnClearFilters.UseVisualStyleBackColor = false;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnExportCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCsv.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCsv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnExportCsv.Location = new System.Drawing.Point(805, 60);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(145, 42);
            this.btnExportCsv.TabIndex = 0;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = false;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // lblSummary
            // 
            this.lblSummary.BackColor = System.Drawing.Color.Transparent;
            this.lblSummary.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblSummary.Location = new System.Drawing.Point(9, 12);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(730, 20);
            this.lblSummary.TabIndex = 0;
            this.lblSummary.Text = "";
            // 
            // lblReportInfo
            // 
            this.lblReportInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblReportInfo.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportInfo.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblReportInfo.Location = new System.Drawing.Point(530, 268);
            this.lblReportInfo.Name = "lblReportInfo";
            this.lblReportInfo.Size = new System.Drawing.Size(762, 18);
            this.lblReportInfo.TabIndex = 0;
            this.lblReportInfo.Text = "";
            this.lblReportInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNoRecord
            // 
            this.lblNoRecord.BackColor = System.Drawing.Color.White;
            this.lblNoRecord.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoRecord.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoRecord.Location = new System.Drawing.Point(560, 515);
            this.lblNoRecord.Name = "lblNoRecord";
            this.lblNoRecord.Size = new System.Drawing.Size(500, 40);
            this.lblNoRecord.TabIndex = 0;
            this.lblNoRecord.Text = "No records found for the selected filters.";
            this.lblNoRecord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoRecord.Visible = false;
            // 
            // ReportsAndAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._891752403_1143943308365188_7410099486200290872_n;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.Controls.Add(this.lblNoRecord);
            this.Controls.Add(this.lblReportInfo);
            this.Controls.Add(this.dtgRecord);
            this.Controls.Add(this.lblChartTitle);
            this.Controls.Add(this.lblFilterTitle);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReportsAndAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCC Records Management System - Reports and Analysis";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReportsAndAnalysis_FormClosed);
            this.Load += new System.EventHandler(this.ReportsAndAnalysis_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtAttendanceDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecord)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblNoRecord;
        private System.Windows.Forms.Label lblReportInfo;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.TextBox txtSearchStudent;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cmbYearLevel;
        private System.Windows.Forms.Label lblYearLevel;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblToDate;

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.ComboBox cmbEventSelect;
        private System.Windows.Forms.Label lblSelectEvent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRepLogout;
        private System.Windows.Forms.Button btnRepDashboard;
        private System.Windows.Forms.Button btnRepRules;
        private System.Windows.Forms.Button btnRepObligation;
        private System.Windows.Forms.Button btnRepReports;
        private System.Windows.Forms.Button btnRepAttendance;
        private System.Windows.Forms.Button btnRepEvent;
        private System.Windows.Forms.Button btnRepFace;
        private System.Windows.Forms.Button btnRepOfficer;
        private System.Windows.Forms.Button btnRepStudent;
        private System.Windows.Forms.Button btnRepDepartment;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Label lblOrganizerTitle;
        private System.Windows.Forms.Label lblScheduleTitle;
        private System.Windows.Forms.Label lblVenue;
        private System.Windows.Forms.Label lblVenueTitle;
        private System.Windows.Forms.Label lblOrganizer;
        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblFilterTitle;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.CheckBox chkAbsent;
        private System.Windows.Forms.CheckBox chkPresent;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtAttendanceDepartment;
        private System.Windows.Forms.DataGridView dtgRecord;
    }
}