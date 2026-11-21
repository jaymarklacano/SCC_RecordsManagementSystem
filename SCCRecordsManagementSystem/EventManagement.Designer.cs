namespace SCCRecordsManagementSystem
{
    partial class EventManagement
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
            this.lblDetailsSummary = new System.Windows.Forms.Label();
            this.dtgEventDetails = new System.Windows.Forms.DataGridView();
            this.rbStudents = new System.Windows.Forms.RadioButton();
            this.rbSessions = new System.Windows.Forms.RadioButton();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.lblEventHint = new System.Windows.Forms.Label();
            this.lblTotalEvents = new System.Windows.Forms.Label();
            this.lblNoEvent = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtgEventManagement = new System.Windows.Forms.DataGridView();
            this.colEventId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventVenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventDates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventPeriods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventParticipants = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventLogOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventStudents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEventStartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEditEvent = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDeleteEvent = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnAddEvent = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEventLogout = new System.Windows.Forms.Button();
            this.btnEventDashboard = new System.Windows.Forms.Button();
            this.btnEventRules = new System.Windows.Forms.Button();
            this.btnEventObligation = new System.Windows.Forms.Button();
            this.btnEventReports = new System.Windows.Forms.Button();
            this.btnEventAttendance = new System.Windows.Forms.Button();
            this.btnEventMan = new System.Windows.Forms.Button();
            this.btnEventFace = new System.Windows.Forms.Button();
            this.btnEventOfficer = new System.Windows.Forms.Button();
            this.btnEventStudentRec = new System.Windows.Forms.Button();
            this.btnEventDept = new System.Windows.Forms.Button();
            this.txtSearchEvent = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEventManagement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEventDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.lblTitle);
            this.panel5.Location = new System.Drawing.Point(321, -2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(984, 52);
            this.panel5.TabIndex = 27;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(78, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(170, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Event Management";
            // 
            // dtgEventManagement
            // 
            this.dtgEventManagement.AutoGenerateColumns = false;
            this.dtgEventManagement.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dtgEventManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgEventManagement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colEventId,
            this.colEventName,
            this.colEventVenue,
            this.colEventDates,
            this.colEventPeriods,
            this.colEventParticipants,
            this.colEventLogOut,
            this.colEventStudents,
            this.colEventStatus,
            this.colEventStartDate,
            this.colEditEvent,
            this.colDeleteEvent});
            this.dtgEventManagement.Location = new System.Drawing.Point(339, 104);
            this.dtgEventManagement.Name = "dtgEventManagement";
            this.dtgEventManagement.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgEventManagement_CellDoubleClick);
            this.dtgEventManagement.SelectionChanged += new System.EventHandler(this.dtgEventManagement_SelectionChanged);
            this.dtgEventManagement.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgEventManagement_CellContentClick);
            this.dtgEventManagement.ReadOnly = true;
            this.dtgEventManagement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgEventManagement.RowHeadersVisible = false;
            this.dtgEventManagement.MultiSelect = false;
            this.dtgEventManagement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgEventManagement.AllowUserToResizeRows = false;
            this.dtgEventManagement.AllowUserToDeleteRows = false;
            this.dtgEventManagement.AllowUserToAddRows = false;
            this.dtgEventManagement.RowHeadersWidth = 51;
            this.dtgEventManagement.RowTemplate.Height = 24;
            this.dtgEventManagement.Size = new System.Drawing.Size(954, 270);
            this.dtgEventManagement.TabIndex = 28;
            //
            // colEventId
            //
            this.colEventId.DataPropertyName = "event_id";
            this.colEventId.HeaderText = "ID";
            this.colEventId.Name = "colEventId";
            this.colEventId.ReadOnly = true;
            this.colEventId.Visible = false;
            //
            // colEventName
            //
            this.colEventName.DataPropertyName = "event_name";
            this.colEventName.FillWeight = 170F;
            this.colEventName.HeaderText = "Event Name";
            this.colEventName.Name = "colEventName";
            this.colEventName.ReadOnly = true;
            //
            // colEventVenue
            //
            this.colEventVenue.DataPropertyName = "venue";
            this.colEventVenue.HeaderText = "Venue";
            this.colEventVenue.Name = "colEventVenue";
            this.colEventVenue.ReadOnly = true;
            //
            // colEventDates
            //
            this.colEventDates.DataPropertyName = "event_dates";
            this.colEventDates.FillWeight = 130F;
            this.colEventDates.HeaderText = "Date";
            this.colEventDates.Name = "colEventDates";
            this.colEventDates.ReadOnly = true;
            //
            // colEventPeriods
            //
            this.colEventPeriods.DataPropertyName = "periods";
            this.colEventPeriods.FillWeight = 140F;
            this.colEventPeriods.HeaderText = "Sessions";
            this.colEventPeriods.Name = "colEventPeriods";
            this.colEventPeriods.ReadOnly = true;
            //
            // colEventParticipants
            //
            this.colEventParticipants.DataPropertyName = "participants";
            this.colEventParticipants.HeaderText = "For";
            this.colEventParticipants.Name = "colEventParticipants";
            this.colEventParticipants.ReadOnly = true;
            //
            // colEventLogOut
            //
            this.colEventLogOut.DataPropertyName = "log_out";
            this.colEventLogOut.FillWeight = 60F;
            this.colEventLogOut.HeaderText = "Log Out";
            this.colEventLogOut.Name = "colEventLogOut";
            this.colEventLogOut.ReadOnly = true;
            //
            // colEventStudents
            //
            this.colEventStudents.DataPropertyName = "students";
            this.colEventStudents.FillWeight = 65F;
            this.colEventStudents.HeaderText = "Students";
            this.colEventStudents.Name = "colEventStudents";
            this.colEventStudents.ReadOnly = true;
            //
            // colEventStatus
            //
            this.colEventStatus.DataPropertyName = "status";
            this.colEventStatus.FillWeight = 80F;
            this.colEventStatus.HeaderText = "Status";
            this.colEventStatus.Name = "colEventStatus";
            this.colEventStatus.ReadOnly = true;
            //
            // colEventStartDate
            //
            this.colEventStartDate.DataPropertyName = "start_date";
            this.colEventStartDate.HeaderText = "Start Date";
            this.colEventStartDate.Name = "colEventStartDate";
            this.colEventStartDate.ReadOnly = true;
            this.colEventStartDate.Visible = false;
            //
            // colEditEvent
            //
            this.colEditEvent.FillWeight = 55F;
            this.colEditEvent.HeaderText = "Action";
            this.colEditEvent.Name = "colEditEvent";
            this.colEditEvent.ReadOnly = true;
            this.colEditEvent.Text = "Edit";
            this.colEditEvent.UseColumnTextForButtonValue = true;
            //
            // colDeleteEvent
            //
            this.colDeleteEvent.FillWeight = 55F;
            this.colDeleteEvent.HeaderText = "";
            this.colDeleteEvent.Name = "colDeleteEvent";
            this.colDeleteEvent.ReadOnly = true;
            this.colDeleteEvent.Text = "Delete";
            this.colDeleteEvent.UseColumnTextForButtonValue = true;
            // 
            // btnAddEvent
            // 
            this.btnAddEvent.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEvent.Location = new System.Drawing.Point(1112, 58);
            this.btnAddEvent.Name = "btnAddEvent";
            this.btnAddEvent.Click += new System.EventHandler(this.btnAddEvent_Click);
            this.btnAddEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddEvent.Size = new System.Drawing.Size(181, 38);
            this.btnAddEvent.TabIndex = 29;
            this.btnAddEvent.Text = "+ Add Event";
            this.btnAddEvent.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.panel1.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._904439575_1911191946735306_1039497458727327980_n1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnEventLogout);
            this.panel1.Controls.Add(this.btnEventDashboard);
            this.panel1.Controls.Add(this.btnEventRules);
            this.panel1.Controls.Add(this.btnEventObligation);
            this.panel1.Controls.Add(this.btnEventReports);
            this.panel1.Controls.Add(this.btnEventAttendance);
            this.panel1.Controls.Add(this.btnEventMan);
            this.panel1.Controls.Add(this.btnEventFace);
            this.panel1.Controls.Add(this.btnEventOfficer);
            this.panel1.Controls.Add(this.btnEventStudentRec);
            this.panel1.Controls.Add(this.btnEventDept);
            this.panel1.Location = new System.Drawing.Point(-3, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 627);
            this.panel1.TabIndex = 30;
            // 
            // btnEventLogout
            // 
            this.btnEventLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnEventLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventLogout.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(75)))), ((int)(((byte)(80)))));
            this.btnEventLogout.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_log_out_24__2_;
            this.btnEventLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventLogout.Location = new System.Drawing.Point(22, 560);
            this.btnEventLogout.Name = "btnEventLogout";
            this.btnEventLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventLogout.Click += new System.EventHandler(this.btnEventLogout_Click);
            this.btnEventLogout.Size = new System.Drawing.Size(277, 38);
            this.btnEventLogout.TabIndex = 11;
            this.btnEventLogout.Text = "   LogOut";
            this.btnEventLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventLogout.UseVisualStyleBackColor = false;
            // 
            // btnEventDashboard
            // 
            this.btnEventDashboard.BackColor = System.Drawing.Color.Transparent;
            this.btnEventDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventDashboard.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventDashboard.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventDashboard.Image = global::SCCRecordsManagementSystem.Properties.Resources.house__1_;
            this.btnEventDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventDashboard.Location = new System.Drawing.Point(22, 123);
            this.btnEventDashboard.Name = "btnEventDashboard";
            this.btnEventDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventDashboard.Click += new System.EventHandler(this.btnEventDashboard_Click);
            this.btnEventDashboard.Size = new System.Drawing.Size(277, 36);
            this.btnEventDashboard.TabIndex = 1;
            this.btnEventDashboard.Text = "   Dashboard";
            this.btnEventDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventDashboard.UseVisualStyleBackColor = false;
            // 
            // btnEventRules
            // 
            this.btnEventRules.BackColor = System.Drawing.Color.Transparent;
            this.btnEventRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventRules.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventRules.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventRules.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_rules_book_24;
            this.btnEventRules.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventRules.Location = new System.Drawing.Point(22, 465);
            this.btnEventRules.Name = "btnEventRules";
            this.btnEventRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventRules.Click += new System.EventHandler(this.btnEventRules_Click);
            this.btnEventRules.Size = new System.Drawing.Size(277, 36);
            this.btnEventRules.TabIndex = 10;
            this.btnEventRules.Text = "   Rules and Permission";
            this.btnEventRules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventRules.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventRules.UseVisualStyleBackColor = false;
            // 
            // btnEventObligation
            // 
            this.btnEventObligation.BackColor = System.Drawing.Color.Transparent;
            this.btnEventObligation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventObligation.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventObligation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventObligation.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_user_24;
            this.btnEventObligation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventObligation.Location = new System.Drawing.Point(22, 427);
            this.btnEventObligation.Name = "btnEventObligation";
            this.btnEventObligation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventObligation.Click += new System.EventHandler(this.btnEventObligation_Click);
            this.btnEventObligation.Size = new System.Drawing.Size(277, 36);
            this.btnEventObligation.TabIndex = 9;
            this.btnEventObligation.Text = "Obligations";
            this.btnEventObligation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventObligation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventObligation.UseVisualStyleBackColor = false;
            // 
            // btnEventReports
            // 
            this.btnEventReports.BackColor = System.Drawing.Color.Transparent;
            this.btnEventReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventReports.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventReports.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventReports.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_analysis_24;
            this.btnEventReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventReports.Location = new System.Drawing.Point(22, 389);
            this.btnEventReports.Name = "btnEventReports";
            this.btnEventReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventReports.Click += new System.EventHandler(this.btnEventReports_Click);
            this.btnEventReports.Size = new System.Drawing.Size(277, 36);
            this.btnEventReports.TabIndex = 8;
            this.btnEventReports.Text = "   Reports and Analysis";
            this.btnEventReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventReports.UseVisualStyleBackColor = false;
            // 
            // btnEventAttendance
            // 
            this.btnEventAttendance.BackColor = System.Drawing.Color.Transparent;
            this.btnEventAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventAttendance.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventAttendance.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventAttendance.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_pass_fail_24;
            this.btnEventAttendance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventAttendance.Location = new System.Drawing.Point(22, 351);
            this.btnEventAttendance.Name = "btnEventAttendance";
            this.btnEventAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventAttendance.Click += new System.EventHandler(this.btnEventAttendance_Click);
            this.btnEventAttendance.Size = new System.Drawing.Size(277, 36);
            this.btnEventAttendance.TabIndex = 7;
            this.btnEventAttendance.Text = "   Attendance Monitoring";
            this.btnEventAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventAttendance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventAttendance.UseVisualStyleBackColor = false;
            // 
            // btnEventMan
            // 
            this.btnEventMan.BackColor = System.Drawing.Color.Transparent;
            this.btnEventMan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventMan.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventMan.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventMan.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_event_accepted_tentatively_24;
            this.btnEventMan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventMan.Location = new System.Drawing.Point(22, 313);
            this.btnEventMan.Name = "btnEventMan";
            this.btnEventMan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventMan.Click += new System.EventHandler(this.btnEventMan_Click);
            this.btnEventMan.Size = new System.Drawing.Size(277, 36);
            this.btnEventMan.TabIndex = 6;
            this.btnEventMan.Text = "   Event Management";
            this.btnEventMan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventMan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventMan.UseVisualStyleBackColor = false;
            // 
            // btnEventFace
            // 
            this.btnEventFace.BackColor = System.Drawing.Color.Transparent;
            this.btnEventFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventFace.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventFace.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventFace.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_facial_recognition_24;
            this.btnEventFace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventFace.Location = new System.Drawing.Point(22, 275);
            this.btnEventFace.Name = "btnEventFace";
            this.btnEventFace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventFace.Click += new System.EventHandler(this.btnEventFace_Click);
            this.btnEventFace.Size = new System.Drawing.Size(277, 36);
            this.btnEventFace.TabIndex = 5;
            this.btnEventFace.Text = "   Face Recognition";
            this.btnEventFace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventFace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventFace.UseVisualStyleBackColor = false;
            // 
            // btnEventOfficer
            // 
            this.btnEventOfficer.BackColor = System.Drawing.Color.Transparent;
            this.btnEventOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventOfficer.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventOfficer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventOfficer.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_management_24;
            this.btnEventOfficer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventOfficer.Location = new System.Drawing.Point(22, 237);
            this.btnEventOfficer.Name = "btnEventOfficer";
            this.btnEventOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventOfficer.Click += new System.EventHandler(this.btnEventOfficer_Click);
            this.btnEventOfficer.Size = new System.Drawing.Size(277, 36);
            this.btnEventOfficer.TabIndex = 4;
            this.btnEventOfficer.Text = "   Officer Records";
            this.btnEventOfficer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventOfficer.UseVisualStyleBackColor = false;
            // 
            // btnEventStudentRec
            // 
            this.btnEventStudentRec.BackColor = System.Drawing.Color.Transparent;
            this.btnEventStudentRec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventStudentRec.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventStudentRec.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventStudentRec.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_what_i_do_241;
            this.btnEventStudentRec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventStudentRec.Location = new System.Drawing.Point(22, 199);
            this.btnEventStudentRec.Name = "btnEventStudentRec";
            this.btnEventStudentRec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventStudentRec.Click += new System.EventHandler(this.btnEventStudentRec_Click);
            this.btnEventStudentRec.Size = new System.Drawing.Size(277, 36);
            this.btnEventStudentRec.TabIndex = 3;
            this.btnEventStudentRec.Text = "   Student Records";
            this.btnEventStudentRec.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventStudentRec.UseVisualStyleBackColor = false;
            // 
            // btnEventDept
            // 
            this.btnEventDept.BackColor = System.Drawing.Color.Transparent;
            this.btnEventDept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventDept.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventDept.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEventDept.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_building_24__1_;
            this.btnEventDept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventDept.Location = new System.Drawing.Point(22, 161);
            this.btnEventDept.Name = "btnEventDept";
            this.btnEventDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEventDept.Click += new System.EventHandler(this.btnEventDept_Click);
            this.btnEventDept.Size = new System.Drawing.Size(277, 36);
            this.btnEventDept.TabIndex = 2;
            this.btnEventDept.Text = "   Departments";
            this.btnEventDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventDept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEventDept.UseVisualStyleBackColor = false;
            // 
            // txtSearchEvent
            // 
            this.txtSearchEvent.Location = new System.Drawing.Point(425, 62);
            this.txtSearchEvent.Multiline = false;
            this.txtSearchEvent.Name = "txtSearchEvent";
            this.txtSearchEvent.TextChanged += new System.EventHandler(this.txtSearchEvent_TextChanged);
            this.txtSearchEvent.MaxLength = 60;
            this.txtSearchEvent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchEvent.Size = new System.Drawing.Size(280, 31);
            this.txtSearchEvent.TabIndex = 31;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(345, 66);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(78, 23);
            this.lblSearch.TabIndex = 32;
            this.lblSearch.Text = "Search:";
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnClearSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearch.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnClearSearch.Location = new System.Drawing.Point(712, 61);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(80, 33);
            this.btnClearSearch.TabIndex = 0;
            this.btnClearSearch.Text = "Clear";
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // lblStatusFilter
            // 
            this.lblStatusFilter.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusFilter.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusFilter.Location = new System.Drawing.Point(805, 67);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new System.Drawing.Size(62, 22);
            this.lblStatusFilter.TabIndex = 0;
            this.lblStatusFilter.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "All Events",
            "Upcoming",
            "In Progress",
            "Completed"});
            this.cmbStatus.Location = new System.Drawing.Point(868, 64);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(170, 26);
            this.cmbStatus.TabIndex = 0;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblNoEvent
            // 
            this.lblNoEvent.BackColor = System.Drawing.Color.White;
            this.lblNoEvent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoEvent.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoEvent.Location = new System.Drawing.Point(480, 210);
            this.lblNoEvent.Name = "lblNoEvent";
            this.lblNoEvent.Visible = false;
            this.lblNoEvent.Size = new System.Drawing.Size(680, 40);
            this.lblNoEvent.TabIndex = 0;
            this.lblNoEvent.Text = "No events found.";
            this.lblNoEvent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalEvents
            // 
            this.lblTotalEvents.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalEvents.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalEvents.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTotalEvents.Location = new System.Drawing.Point(339, 378);
            this.lblTotalEvents.Name = "lblTotalEvents";
            this.lblTotalEvents.Size = new System.Drawing.Size(600, 22);
            this.lblTotalEvents.TabIndex = 0;
            this.lblTotalEvents.Text = "Total: 0 event(s)";
            // 
            // lblEventHint
            // 
            this.lblEventHint.BackColor = System.Drawing.Color.Transparent;
            this.lblEventHint.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventHint.ForeColor = System.Drawing.Color.DimGray;
            this.lblEventHint.Location = new System.Drawing.Point(940, 378);
            this.lblEventHint.Name = "lblEventHint";
            this.lblEventHint.Size = new System.Drawing.Size(353, 22);
            this.lblEventHint.TabIndex = 0;
            this.lblEventHint.Text = "Double-click an event to edit it.";
            this.lblEventHint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblDetailsTitle.Location = new System.Drawing.Point(339, 406);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(560, 24);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "Select an event to see its sessions.";
            // 
            // rbSessions
            // 
            this.rbSessions.AutoSize = true;
            this.rbSessions.Checked = true;
            this.rbSessions.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSessions.Location = new System.Drawing.Point(930, 406);
            this.rbSessions.Name = "rbSessions";
            this.rbSessions.Size = new System.Drawing.Size(90, 24);
            this.rbSessions.TabIndex = 0;
            this.rbSessions.TabStop = true;
            this.rbSessions.Text = "Sessions";
            this.rbSessions.UseVisualStyleBackColor = true;
            this.rbSessions.CheckedChanged += new System.EventHandler(this.rbDetails_CheckedChanged);
            // 
            // rbStudents
            // 
            this.rbStudents.AutoSize = true;
            this.rbStudents.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbStudents.Location = new System.Drawing.Point(1040, 406);
            this.rbStudents.Name = "rbStudents";
            this.rbStudents.Size = new System.Drawing.Size(150, 24);
            this.rbStudents.TabIndex = 0;
            this.rbStudents.Text = "Students (Roster)";
            this.rbStudents.UseVisualStyleBackColor = true;
            this.rbStudents.CheckedChanged += new System.EventHandler(this.rbDetails_CheckedChanged);
            // 
            // dtgEventDetails
            // 
            this.dtgEventDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgEventDetails.Location = new System.Drawing.Point(339, 434);
            this.dtgEventDetails.Name = "dtgEventDetails";
            this.dtgEventDetails.ReadOnly = true;
            this.dtgEventDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgEventDetails.RowHeadersVisible = false;
            this.dtgEventDetails.MultiSelect = false;
            this.dtgEventDetails.BackgroundColor = System.Drawing.Color.White;
            this.dtgEventDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgEventDetails.AllowUserToResizeRows = false;
            this.dtgEventDetails.AllowUserToDeleteRows = false;
            this.dtgEventDetails.AllowUserToAddRows = false;
            this.dtgEventDetails.RowHeadersWidth = 51;
            this.dtgEventDetails.RowTemplate.Height = 24;
            this.dtgEventDetails.Size = new System.Drawing.Size(954, 152);
            this.dtgEventDetails.TabIndex = 40;
            // 
            // lblDetailsSummary
            // 
            this.lblDetailsSummary.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailsSummary.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailsSummary.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblDetailsSummary.Location = new System.Drawing.Point(339, 590);
            this.lblDetailsSummary.Name = "lblDetailsSummary";
            this.lblDetailsSummary.Size = new System.Drawing.Size(954, 22);
            this.lblDetailsSummary.TabIndex = 0;
            this.lblDetailsSummary.Text = "";
            // 
            // EventManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._891752403_1143943308365188_7410099486200290872_n;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.Controls.Add(this.lblDetailsSummary);
            this.Controls.Add(this.dtgEventDetails);
            this.Controls.Add(this.rbStudents);
            this.Controls.Add(this.rbSessions);
            this.Controls.Add(this.lblDetailsTitle);
            this.Controls.Add(this.lblEventHint);
            this.Controls.Add(this.lblTotalEvents);
            this.Controls.Add(this.lblNoEvent);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatusFilter);
            this.Controls.Add(this.btnClearSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearchEvent);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAddEvent);
            this.Controls.Add(this.dtgEventManagement);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EventManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCC Records Management System - Event Management";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EventManagement_FormClosed);
            this.Load += new System.EventHandler(this.EventManagement_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEventManagement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEventDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDetailsSummary;
        private System.Windows.Forms.DataGridView dtgEventDetails;
        private System.Windows.Forms.RadioButton rbStudents;
        private System.Windows.Forms.RadioButton rbSessions;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Label lblEventHint;
        private System.Windows.Forms.Label lblTotalEvents;
        private System.Windows.Forms.Label lblNoEvent;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dtgEventManagement;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventVenue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventDates;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventPeriods;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventParticipants;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventLogOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventStudents;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEventStartDate;
        private System.Windows.Forms.DataGridViewButtonColumn colEditEvent;
        private System.Windows.Forms.DataGridViewButtonColumn colDeleteEvent;
        private System.Windows.Forms.Button btnAddEvent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEventLogout;
        private System.Windows.Forms.Button btnEventDashboard;
        private System.Windows.Forms.Button btnEventRules;
        private System.Windows.Forms.Button btnEventObligation;
        private System.Windows.Forms.Button btnEventReports;
        private System.Windows.Forms.Button btnEventAttendance;
        private System.Windows.Forms.Button btnEventMan;
        private System.Windows.Forms.Button btnEventFace;
        private System.Windows.Forms.Button btnEventOfficer;
        private System.Windows.Forms.Button btnEventStudentRec;
        private System.Windows.Forms.Button btnEventDept;
        private System.Windows.Forms.TextBox txtSearchEvent;
        private System.Windows.Forms.Label lblSearch;
    }
}