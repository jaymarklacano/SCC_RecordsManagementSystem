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
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.lblNoActivity = new System.Windows.Forms.Label();
            this.lblTodaySummary = new System.Windows.Forms.Label();
            this.btnLoginHistory = new System.Windows.Forms.Button();
            this.btnRefreshDashboard = new System.Windows.Forms.Button();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblEventDetails = new System.Windows.Forms.Label();
            this.lblOfficerDetails = new System.Windows.Forms.Label();
            this.lblStudentDetails = new System.Windows.Forms.Label();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTotalAll = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTotalOfficer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAllEvent = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.chtAttendanceOverview = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgRecentEvent = new System.Windows.Forms.DataGridView();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.lblRecentTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnRulesPermission = new System.Windows.Forms.Button();
            this.btnObligation = new System.Windows.Forms.Button();
            this.btnReportAnalysis = new System.Windows.Forms.Button();
            this.btnAttendanceMonitoring = new System.Windows.Forms.Button();
            this.btnEventManagement = new System.Windows.Forms.Button();
            this.btnFaceRecognition = new System.Windows.Forms.Button();
            this.btnOfficer = new System.Windows.Forms.Button();
            this.btnStudentRecords = new System.Windows.Forms.Button();
            this.btnDepartment = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtAttendanceOverview)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecentEvent)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.panel2.Controls.Add(this.lblStudentDetails);
            this.panel2.Controls.Add(this.lblTotalAll);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(358, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(281, 136);
            this.panel2.TabIndex = 1;
            // 
            // lblTotalAll
            // 
            this.lblTotalAll.AutoSize = false;
            this.lblTotalAll.Location = new System.Drawing.Point(0, 52);
            this.lblTotalAll.Name = "lblTotalAll";
            this.lblTotalAll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalAll.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalAll.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAll.Size = new System.Drawing.Size(281, 46);
            this.lblTotalAll.TabIndex = 1;
            this.lblTotalAll.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = false;
            this.label2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 22);
            this.label2.Name = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Size = new System.Drawing.Size(281, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total Students";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.panel3.Controls.Add(this.lblOfficerDetails);
            this.panel3.Controls.Add(this.lblTotalOfficer);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(663, 121);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(291, 136);
            this.panel3.TabIndex = 2;
            // 
            // lblTotalOfficer
            // 
            this.lblTotalOfficer.AutoSize = false;
            this.lblTotalOfficer.Location = new System.Drawing.Point(0, 52);
            this.lblTotalOfficer.Name = "lblTotalOfficer";
            this.lblTotalOfficer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotalOfficer.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalOfficer.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOfficer.Size = new System.Drawing.Size(291, 46);
            this.lblTotalOfficer.TabIndex = 2;
            this.lblTotalOfficer.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = false;
            this.label3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 22);
            this.label3.Name = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Size = new System.Drawing.Size(291, 26);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total Officers";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.panel4.Controls.Add(this.lblEventDetails);
            this.panel4.Controls.Add(this.lblAllEvent);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(986, 121);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(284, 136);
            this.panel4.TabIndex = 3;
            // 
            // lblAllEvent
            // 
            this.lblAllEvent.AutoSize = false;
            this.lblAllEvent.Location = new System.Drawing.Point(0, 52);
            this.lblAllEvent.Name = "lblAllEvent";
            this.lblAllEvent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAllEvent.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblAllEvent.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllEvent.Size = new System.Drawing.Size(284, 46);
            this.lblAllEvent.TabIndex = 3;
            this.lblAllEvent.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = false;
            this.label4.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(0, 22);
            this.label4.Name = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Size = new System.Drawing.Size(284, 26);
            this.label4.TabIndex = 1;
            this.label4.Text = "Total Events";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.btnLoginHistory);
            this.panel5.Controls.Add(this.btnRefreshDashboard);
            this.panel5.Controls.Add(this.lblDateTime);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(320, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(989, 50);
            this.panel5.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.GhostWhite;
            this.label1.Location = new System.Drawing.Point(56, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dashboard";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblWelcome.Location = new System.Drawing.Point(352, 69);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(328, 36);
            this.lblWelcome.TabIndex = 5;
            this.lblWelcome.Text = "Welcome, Admin User!";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel6.Controls.Add(this.lblTodaySummary);
            this.panel6.Controls.Add(this.chtAttendanceOverview);
            this.panel6.Location = new System.Drawing.Point(358, 293);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(442, 305);
            this.panel6.TabIndex = 6;
            // 
            // chtAttendanceOverview
            // 
            chartArea8.BackColor = System.Drawing.Color.Transparent;
            chartArea8.Name = "ChartArea1";
            this.chtAttendanceOverview.ChartAreas.Add(chartArea8);
            legend8.BackColor = System.Drawing.Color.Transparent;
            legend8.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend8.Name = "Legend1";
            this.chtAttendanceOverview.Legends.Add(legend8);
            this.chtAttendanceOverview.BackColor = System.Drawing.Color.LightSteelBlue;
            this.chtAttendanceOverview.Location = new System.Drawing.Point(18, 12);
            this.chtAttendanceOverview.Name = "chtAttendanceOverview";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series8.IsValueShownAsLabel = true;
            series8.LabelForeColor = System.Drawing.Color.White;
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            this.chtAttendanceOverview.Series.Add(series8);
            this.chtAttendanceOverview.Size = new System.Drawing.Size(406, 258);
            this.chtAttendanceOverview.TabIndex = 0;
            this.chtAttendanceOverview.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.lblNoActivity);
            this.groupBox1.Controls.Add(this.dtgRecentEvent);
            this.groupBox1.Location = new System.Drawing.Point(817, 293);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 305);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // dtgRecentEvent
            // 
            this.dtgRecentEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRecentEvent.Location = new System.Drawing.Point(6, 21);
            this.dtgRecentEvent.Name = "dtgRecentEvent";
            this.dtgRecentEvent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgRecentEvent.RowHeadersVisible = false;
            this.dtgRecentEvent.ReadOnly = true;
            this.dtgRecentEvent.MultiSelect = false;
            this.dtgRecentEvent.BackgroundColor = System.Drawing.Color.White;
            this.dtgRecentEvent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgRecentEvent.AllowUserToResizeRows = false;
            this.dtgRecentEvent.AllowUserToDeleteRows = false;
            this.dtgRecentEvent.AllowUserToAddRows = false;
            this.dtgRecentEvent.RowHeadersWidth = 51;
            this.dtgRecentEvent.RowTemplate.Height = 24;
            this.dtgRecentEvent.Size = new System.Drawing.Size(441, 276);
            this.dtgRecentEvent.TabIndex = 0;
            // 
            // lblChartTitle
            // 
            this.lblChartTitle.AutoSize = true;
            this.lblChartTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblChartTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChartTitle.Location = new System.Drawing.Point(355, 273);
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.Size = new System.Drawing.Size(155, 17);
            this.lblChartTitle.TabIndex = 8;
            this.lblChartTitle.Text = "Today's Attendance Overview";
            // 
            // lblRecentTitle
            // 
            this.lblRecentTitle.AutoSize = true;
            this.lblRecentTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblRecentTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecentTitle.Location = new System.Drawing.Point(814, 273);
            this.lblRecentTitle.Name = "lblRecentTitle";
            this.lblRecentTitle.Size = new System.Drawing.Size(123, 17);
            this.lblRecentTitle.TabIndex = 9;
            this.lblRecentTitle.Text = "Recent Attendance Activities";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.panel1.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._904439575_1911191946735306_1039497458727327980_n1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnLogOut);
            this.panel1.Controls.Add(this.btnDashboard);
            this.panel1.Controls.Add(this.btnRulesPermission);
            this.panel1.Controls.Add(this.btnObligation);
            this.panel1.Controls.Add(this.btnReportAnalysis);
            this.panel1.Controls.Add(this.btnAttendanceMonitoring);
            this.panel1.Controls.Add(this.btnEventManagement);
            this.panel1.Controls.Add(this.btnFaceRecognition);
            this.panel1.Controls.Add(this.btnOfficer);
            this.panel1.Controls.Add(this.btnStudentRecords);
            this.panel1.Controls.Add(this.btnDepartment);
            this.panel1.Location = new System.Drawing.Point(-3, -5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 624);
            this.panel1.TabIndex = 25;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.Transparent;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.ForeColor = System.Drawing.Color.DarkRed;
            this.btnLogOut.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_log_out_24__2_;
            this.btnLogOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogOut.Location = new System.Drawing.Point(22, 560);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            this.btnLogOut.Size = new System.Drawing.Size(277, 38);
            this.btnLogOut.TabIndex = 11;
            this.btnLogOut.Text = "   LogOut";
            this.btnLogOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogOut.UseVisualStyleBackColor = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.Transparent;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDashboard.Image = global::SCCRecordsManagementSystem.Properties.Resources.house__1_;
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(22, 123);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            this.btnDashboard.Size = new System.Drawing.Size(277, 36);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "   Dashboard";
            this.btnDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDashboard.UseVisualStyleBackColor = false;
            // 
            // btnRulesPermission
            // 
            this.btnRulesPermission.BackColor = System.Drawing.Color.Transparent;
            this.btnRulesPermission.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRulesPermission.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRulesPermission.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRulesPermission.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_rules_book_24;
            this.btnRulesPermission.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRulesPermission.Location = new System.Drawing.Point(22, 465);
            this.btnRulesPermission.Name = "btnRulesPermission";
            this.btnRulesPermission.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRulesPermission.Size = new System.Drawing.Size(277, 36);
            this.btnRulesPermission.TabIndex = 10;
            this.btnRulesPermission.Text = "   Rules and Permission";
            this.btnRulesPermission.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRulesPermission.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRulesPermission.UseVisualStyleBackColor = false;
            this.btnRulesPermission.Click += new System.EventHandler(this.btnRulesPermission_Click);
            // 
            // btnObligation
            // 
            this.btnObligation.BackColor = System.Drawing.Color.Transparent;
            this.btnObligation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnObligation.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnObligation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnObligation.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_user_24;
            this.btnObligation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObligation.Location = new System.Drawing.Point(22, 427);
            this.btnObligation.Name = "btnObligation";
            this.btnObligation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnObligation.Size = new System.Drawing.Size(277, 36);
            this.btnObligation.TabIndex = 9;
            this.btnObligation.Text = "Obligations";
            this.btnObligation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnObligation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnObligation.UseVisualStyleBackColor = false;
            this.btnObligation.Click += new System.EventHandler(this.btnObligation_Click);
            // 
            // btnReportAnalysis
            // 
            this.btnReportAnalysis.BackColor = System.Drawing.Color.Transparent;
            this.btnReportAnalysis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportAnalysis.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportAnalysis.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReportAnalysis.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_analysis_24;
            this.btnReportAnalysis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportAnalysis.Location = new System.Drawing.Point(22, 389);
            this.btnReportAnalysis.Name = "btnReportAnalysis";
            this.btnReportAnalysis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportAnalysis.Size = new System.Drawing.Size(277, 36);
            this.btnReportAnalysis.TabIndex = 8;
            this.btnReportAnalysis.Text = "   Reports and Analysis";
            this.btnReportAnalysis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportAnalysis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReportAnalysis.UseVisualStyleBackColor = false;
            this.btnReportAnalysis.Click += new System.EventHandler(this.btnReportAnalysis_Click);
            // 
            // btnAttendanceMonitoring
            // 
            this.btnAttendanceMonitoring.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendanceMonitoring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendanceMonitoring.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendanceMonitoring.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendanceMonitoring.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_pass_fail_24;
            this.btnAttendanceMonitoring.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendanceMonitoring.Location = new System.Drawing.Point(22, 351);
            this.btnAttendanceMonitoring.Name = "btnAttendanceMonitoring";
            this.btnAttendanceMonitoring.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendanceMonitoring.Size = new System.Drawing.Size(277, 36);
            this.btnAttendanceMonitoring.TabIndex = 7;
            this.btnAttendanceMonitoring.Text = "   Attendance Monitoring";
            this.btnAttendanceMonitoring.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendanceMonitoring.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttendanceMonitoring.UseVisualStyleBackColor = false;
            this.btnAttendanceMonitoring.Click += new System.EventHandler(this.btnAttendanceMonitoring_Click);
            // 
            // btnEventManagement
            // 
            this.btnEventManagement.BackColor = System.Drawing.Color.Transparent;
            this.btnEventManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventManagement.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventManagement.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventManagement.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_event_accepted_tentatively_24;
            this.btnEventManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventManagement.Location = new System.Drawing.Point(22, 313);
            this.btnEventManagement.Name = "btnEventManagement";
            this.btnEventManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventManagement.Size = new System.Drawing.Size(277, 36);
            this.btnEventManagement.TabIndex = 6;
            this.btnEventManagement.Text = "   Event Management";
            this.btnEventManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventManagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventManagement.UseVisualStyleBackColor = false;
            this.btnEventManagement.Click += new System.EventHandler(this.btnEventManagement_Click);
            // 
            // btnFaceRecognition
            // 
            this.btnFaceRecognition.BackColor = System.Drawing.Color.Transparent;
            this.btnFaceRecognition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFaceRecognition.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceRecognition.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFaceRecognition.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_facial_recognition_24;
            this.btnFaceRecognition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceRecognition.Location = new System.Drawing.Point(22, 275);
            this.btnFaceRecognition.Name = "btnFaceRecognition";
            this.btnFaceRecognition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFaceRecognition.Size = new System.Drawing.Size(277, 36);
            this.btnFaceRecognition.TabIndex = 5;
            this.btnFaceRecognition.Text = "   Face Recognition";
            this.btnFaceRecognition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceRecognition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFaceRecognition.UseVisualStyleBackColor = false;
            this.btnFaceRecognition.Click += new System.EventHandler(this.btnFaceRecognition_Click);
            // 
            // btnOfficer
            // 
            this.btnOfficer.BackColor = System.Drawing.Color.Transparent;
            this.btnOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOfficer.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOfficer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOfficer.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_management_24;
            this.btnOfficer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOfficer.Location = new System.Drawing.Point(22, 237);
            this.btnOfficer.Name = "btnOfficer";
            this.btnOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOfficer.Size = new System.Drawing.Size(277, 36);
            this.btnOfficer.TabIndex = 4;
            this.btnOfficer.Text = "   Officer Records";
            this.btnOfficer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOfficer.UseVisualStyleBackColor = false;
            this.btnOfficer.Click += new System.EventHandler(this.btnOfficer_Click);
            // 
            // btnStudentRecords
            // 
            this.btnStudentRecords.BackColor = System.Drawing.Color.Transparent;
            this.btnStudentRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentRecords.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStudentRecords.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnStudentRecords.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_what_i_do_241;
            this.btnStudentRecords.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStudentRecords.Location = new System.Drawing.Point(22, 199);
            this.btnStudentRecords.Name = "btnStudentRecords";
            this.btnStudentRecords.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStudentRecords.Size = new System.Drawing.Size(277, 36);
            this.btnStudentRecords.TabIndex = 3;
            this.btnStudentRecords.Text = "   Student Records";
            this.btnStudentRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStudentRecords.UseVisualStyleBackColor = false;
            this.btnStudentRecords.Click += new System.EventHandler(this.btnStudentRecords_Click);
            // 
            // btnDepartment
            // 
            this.btnDepartment.BackColor = System.Drawing.Color.Transparent;
            this.btnDepartment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDepartment.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDepartment.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDepartment.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_building_24__1_;
            this.btnDepartment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDepartment.Location = new System.Drawing.Point(22, 161);
            this.btnDepartment.Name = "btnDepartment";
            this.btnDepartment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDepartment.Size = new System.Drawing.Size(277, 36);
            this.btnDepartment.TabIndex = 2;
            this.btnDepartment.Text = "   Departments";
            this.btnDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDepartment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDepartment.UseVisualStyleBackColor = false;
            this.btnDepartment.Click += new System.EventHandler(this.btnDepartment_Click);
            // 
            // lblStudentDetails
            // 
            this.lblStudentDetails.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblStudentDetails.Location = new System.Drawing.Point(0, 102);
            this.lblStudentDetails.Name = "lblStudentDetails";
            this.lblStudentDetails.Size = new System.Drawing.Size(281, 22);
            this.lblStudentDetails.TabIndex = 4;
            this.lblStudentDetails.Text = "Active: 0  |  Face Registered: 0";
            this.lblStudentDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOfficerDetails
            // 
            this.lblOfficerDetails.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOfficerDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblOfficerDetails.Location = new System.Drawing.Point(0, 102);
            this.lblOfficerDetails.Name = "lblOfficerDetails";
            this.lblOfficerDetails.Size = new System.Drawing.Size(291, 22);
            this.lblOfficerDetails.TabIndex = 4;
            this.lblOfficerDetails.Text = "Admin Accounts: 0";
            this.lblOfficerDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEventDetails
            // 
            this.lblEventDetails.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblEventDetails.Location = new System.Drawing.Point(0, 102);
            this.lblEventDetails.Name = "lblEventDetails";
            this.lblEventDetails.Size = new System.Drawing.Size(284, 22);
            this.lblEventDetails.TabIndex = 4;
            this.lblEventDetails.Text = "Ongoing: 0  |  Upcoming: 0";
            this.lblEventDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblDateTime.Location = new System.Drawing.Point(616, 14);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(360, 24);
            this.lblDateTime.TabIndex = 3;
            this.lblDateTime.Text = "Date and Time";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRefreshDashboard
            // 
            this.btnRefreshDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnRefreshDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshDashboard.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnRefreshDashboard.Location = new System.Drawing.Point(300, 10);
            this.btnRefreshDashboard.Name = "btnRefreshDashboard";
            this.btnRefreshDashboard.Size = new System.Drawing.Size(100, 30);
            this.btnRefreshDashboard.TabIndex = 1;
            this.btnRefreshDashboard.Text = "Refresh";
            this.btnRefreshDashboard.UseVisualStyleBackColor = false;
            this.btnRefreshDashboard.Click += new System.EventHandler(this.btnRefreshDashboard_Click);
            // 
            // btnLoginHistory
            // 
            this.btnLoginHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnLoginHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoginHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginHistory.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoginHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnLoginHistory.Location = new System.Drawing.Point(410, 10);
            this.btnLoginHistory.Name = "btnLoginHistory";
            this.btnLoginHistory.Size = new System.Drawing.Size(130, 30);
            this.btnLoginHistory.TabIndex = 1;
            this.btnLoginHistory.Text = "Login History";
            this.btnLoginHistory.UseVisualStyleBackColor = false;
            this.btnLoginHistory.Click += new System.EventHandler(this.btnLoginHistory_Click);
            // 
            // lblTodaySummary
            // 
            this.lblTodaySummary.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodaySummary.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTodaySummary.Location = new System.Drawing.Point(18, 274);
            this.lblTodaySummary.Name = "lblTodaySummary";
            this.lblTodaySummary.Size = new System.Drawing.Size(406, 24);
            this.lblTodaySummary.TabIndex = 1;
            this.lblTodaySummary.Text = "Present: 0   Absent: 0   Pending: 0";
            this.lblTodaySummary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoActivity
            // 
            this.lblNoActivity.BackColor = System.Drawing.Color.White;
            this.lblNoActivity.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoActivity.ForeColor = System.Drawing.Color.Gray;
            this.lblNoActivity.Location = new System.Drawing.Point(40, 130);
            this.lblNoActivity.Name = "lblNoActivity";
            this.lblNoActivity.Size = new System.Drawing.Size(373, 40);
            this.lblNoActivity.TabIndex = 1;
            this.lblNoActivity.Text = "No attendance activity yet.";
            this.lblNoActivity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoActivity.Visible = false;
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._891752403_1143943308365188_7410099486200290872_n1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblRecentTitle);
            this.Controls.Add(this.lblChartTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCC Records Management System - Dashboard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Dashboard_FormClosed);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtAttendanceOverview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecentEvent)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Label lblNoActivity;
        private System.Windows.Forms.Label lblTodaySummary;
        private System.Windows.Forms.Button btnLoginHistory;
        private System.Windows.Forms.Button btnRefreshDashboard;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblEventDetails;
        private System.Windows.Forms.Label lblOfficerDetails;
        private System.Windows.Forms.Label lblStudentDetails;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtAttendanceOverview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.Label lblRecentTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnRulesPermission;
        private System.Windows.Forms.Button btnObligation;
        private System.Windows.Forms.Button btnReportAnalysis;
        private System.Windows.Forms.Button btnAttendanceMonitoring;
        private System.Windows.Forms.Button btnEventManagement;
        private System.Windows.Forms.Button btnFaceRecognition;
        private System.Windows.Forms.Button btnOfficer;
        private System.Windows.Forms.Button btnStudentRecords;
        private System.Windows.Forms.Button btnDepartment;
        private System.Windows.Forms.Label lblTotalAll;
        private System.Windows.Forms.Label lblTotalOfficer;
        private System.Windows.Forms.Label lblAllEvent;
        private System.Windows.Forms.DataGridView dtgRecentEvent;
    }
}