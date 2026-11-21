namespace SCCRecordsManagementSystem
{
    partial class AddEvent
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
            this.btnCancelEvent = new System.Windows.Forms.Button();
            this.lblScheduleInfo = new System.Windows.Forms.Label();
            this.chkEvening = new System.Windows.Forms.CheckBox();
            this.chkAfternoon = new System.Windows.Forms.CheckBox();
            this.chkMorning = new System.Windows.Forms.CheckBox();
            this.lblSessionsTitle = new System.Windows.Forms.Label();
            this.lblExpectedStudents = new System.Windows.Forms.Label();
            this.cmbEventYear = new System.Windows.Forms.ComboBox();
            this.lblForYear = new System.Windows.Forms.Label();
            this.cmbEventDepartment = new System.Windows.Forms.ComboBox();
            this.lblForDepartment = new System.Windows.Forms.Label();
            this.lblNumberOfDays = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.txtOrganizer = new System.Windows.Forms.TextBox();
            this.lblOrganizer = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlEventInfo = new System.Windows.Forms.Panel();
            this.dtpMorningEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpMorningStart = new System.Windows.Forms.DateTimePicker();
            this.pnlSectionHeader = new System.Windows.Forms.Panel();
            this.lblSectionTitle = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtEventName = new System.Windows.Forms.TextBox();
            this.lblMorningEnd = new System.Windows.Forms.Label();
            this.pnlVenue = new System.Windows.Forms.Panel();
            this.txtVenue = new System.Windows.Forms.TextBox();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblVenue = new System.Windows.Forms.Label();
            this.lblMorningStart = new System.Windows.Forms.Label();
            this.lblEventName = new System.Windows.Forms.Label();
            this.btnResetEvent = new System.Windows.Forms.Button();
            this.btnSaveEvent = new System.Windows.Forms.Button();
            this.dtpAfternoonEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpAfternoonStart = new System.Windows.Forms.DateTimePicker();
            this.lblAfternoonEnd = new System.Windows.Forms.Label();
            this.lblAfternoonStart = new System.Windows.Forms.Label();
            this.dtpEveningEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpEveningStart = new System.Windows.Forms.DateTimePicker();
            this.lblEveningEnd = new System.Windows.Forms.Label();
            this.lblEveningStart = new System.Windows.Forms.Label();
            this.chkLogoutNeeded = new System.Windows.Forms.CheckBox();
            this.pnlHeader.SuspendLayout();
            this.pnlEventInfo.SuspendLayout();
            this.pnlSectionHeader.SuspendLayout();
            this.pnlVenue.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Location = new System.Drawing.Point(-1, -1);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(844, 44);
            this.pnlHeader.TabIndex = 29;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(23, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(95, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add Event";
            // 
            // pnlEventInfo
            // 
            this.pnlEventInfo.Controls.Add(this.lblScheduleInfo);
            this.pnlEventInfo.Controls.Add(this.chkEvening);
            this.pnlEventInfo.Controls.Add(this.chkAfternoon);
            this.pnlEventInfo.Controls.Add(this.chkMorning);
            this.pnlEventInfo.Controls.Add(this.lblSessionsTitle);
            this.pnlEventInfo.Controls.Add(this.lblExpectedStudents);
            this.pnlEventInfo.Controls.Add(this.cmbEventYear);
            this.pnlEventInfo.Controls.Add(this.lblForYear);
            this.pnlEventInfo.Controls.Add(this.cmbEventDepartment);
            this.pnlEventInfo.Controls.Add(this.lblForDepartment);
            this.pnlEventInfo.Controls.Add(this.lblNumberOfDays);
            this.pnlEventInfo.Controls.Add(this.dtpEndDate);
            this.pnlEventInfo.Controls.Add(this.lblEndDate);
            this.pnlEventInfo.Controls.Add(this.txtOrganizer);
            this.pnlEventInfo.Controls.Add(this.lblOrganizer);
            this.pnlEventInfo.Controls.Add(this.chkLogoutNeeded);
            this.pnlEventInfo.Controls.Add(this.dtpEveningEnd);
            this.pnlEventInfo.Controls.Add(this.dtpEveningStart);
            this.pnlEventInfo.Controls.Add(this.lblEveningEnd);
            this.pnlEventInfo.Controls.Add(this.lblEveningStart);
            this.pnlEventInfo.Controls.Add(this.dtpAfternoonEnd);
            this.pnlEventInfo.Controls.Add(this.dtpAfternoonStart);
            this.pnlEventInfo.Controls.Add(this.lblAfternoonEnd);
            this.pnlEventInfo.Controls.Add(this.lblAfternoonStart);
            this.pnlEventInfo.Controls.Add(this.dtpMorningEnd);
            this.pnlEventInfo.Controls.Add(this.dtpMorningStart);
            this.pnlEventInfo.Controls.Add(this.pnlSectionHeader);
            this.pnlEventInfo.Controls.Add(this.dtpStartDate);
            this.pnlEventInfo.Controls.Add(this.txtEventName);
            this.pnlEventInfo.Controls.Add(this.lblMorningEnd);
            this.pnlEventInfo.Controls.Add(this.pnlVenue);
            this.pnlEventInfo.Controls.Add(this.lblStartDate);
            this.pnlEventInfo.Controls.Add(this.lblVenue);
            this.pnlEventInfo.Controls.Add(this.lblMorningStart);
            this.pnlEventInfo.Controls.Add(this.lblEventName);
            this.pnlEventInfo.Location = new System.Drawing.Point(12, 56);
            this.pnlEventInfo.Name = "pnlEventInfo";
            this.pnlEventInfo.Size = new System.Drawing.Size(755, 494);
            this.pnlEventInfo.TabIndex = 0;
            // 
            // dtpMorningEnd
            // 
            this.dtpMorningEnd.Location = new System.Drawing.Point(488, 306);
            this.dtpMorningEnd.Name = "dtpMorningEnd";
            this.dtpMorningEnd.ValueChanged += new System.EventHandler(this.scheduleValue_Changed);
            this.dtpMorningEnd.ShowUpDown = true;
            this.dtpMorningEnd.CustomFormat = "hh:mm tt";
            this.dtpMorningEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMorningEnd.Size = new System.Drawing.Size(150, 23);
            this.dtpMorningEnd.TabIndex = 9;
            // 
            // dtpMorningStart
            // 
            this.dtpMorningStart.Location = new System.Drawing.Point(232, 306);
            this.dtpMorningStart.Name = "dtpMorningStart";
            this.dtpMorningStart.ValueChanged += new System.EventHandler(this.scheduleValue_Changed);
            this.dtpMorningStart.ShowUpDown = true;
            this.dtpMorningStart.CustomFormat = "hh:mm tt";
            this.dtpMorningStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMorningStart.Size = new System.Drawing.Size(150, 23);
            this.dtpMorningStart.TabIndex = 8;
            // 
            // pnlSectionHeader
            // 
            this.pnlSectionHeader.BackColor = System.Drawing.Color.Azure;
            this.pnlSectionHeader.Controls.Add(this.lblSectionTitle);
            this.pnlSectionHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlSectionHeader.Name = "pnlSectionHeader";
            this.pnlSectionHeader.Size = new System.Drawing.Size(755, 44);
            this.pnlSectionHeader.TabIndex = 30;
            // 
            // lblSectionTitle
            // 
            this.lblSectionTitle.AutoSize = true;
            this.lblSectionTitle.Location = new System.Drawing.Point(16, 11);
            this.lblSectionTitle.Name = "lblSectionTitle";
            this.lblSectionTitle.Size = new System.Drawing.Size(300, 15);
            this.lblSectionTitle.TabIndex = 31;
            this.lblSectionTitle.Text = "Event Information   (fields with * are required)";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(30, 180);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            this.dtpStartDate.CustomFormat = "ddd, MMM dd, yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Size = new System.Drawing.Size(215, 23);
            this.dtpStartDate.TabIndex = 3;
            // 
            // txtEventName
            // 
            this.txtEventName.Location = new System.Drawing.Point(30, 72);
            this.txtEventName.Name = "txtEventName";
            this.txtEventName.MaxLength = 120;
            this.txtEventName.Size = new System.Drawing.Size(330, 23);
            this.txtEventName.TabIndex = 0;
            // 
            // lblMorningEnd
            // 
            this.lblMorningEnd.AutoSize = true;
            this.lblMorningEnd.Location = new System.Drawing.Point(410, 310);
            this.lblMorningEnd.Name = "lblMorningEnd";
            this.lblMorningEnd.Size = new System.Drawing.Size(75, 15);
            this.lblMorningEnd.TabIndex = 41;
            this.lblMorningEnd.Text = "End Time :";
            // 
            // pnlVenue
            // 
            this.pnlVenue.BackColor = System.Drawing.SystemColors.Window;
            this.pnlVenue.Controls.Add(this.txtVenue);
            this.pnlVenue.Location = new System.Drawing.Point(30, 122);
            this.pnlVenue.Name = "pnlVenue";
            this.pnlVenue.Size = new System.Drawing.Size(693, 30);
            this.pnlVenue.TabIndex = 2;
            // 
            // txtVenue
            // 
            this.txtVenue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVenue.Location = new System.Drawing.Point(4, 7);
            this.txtVenue.Name = "txtVenue";
            this.txtVenue.MaxLength = 120;
            this.txtVenue.Size = new System.Drawing.Size(684, 16);
            this.txtVenue.TabIndex = 0;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(27, 162);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(42, 15);
            this.lblStartDate.TabIndex = 35;
            this.lblStartDate.Text = "Start Date : *";
            // 
            // lblVenue
            // 
            this.lblVenue.AutoSize = true;
            this.lblVenue.Location = new System.Drawing.Point(27, 104);
            this.lblVenue.Name = "lblVenue";
            this.lblVenue.Size = new System.Drawing.Size(51, 15);
            this.lblVenue.TabIndex = 34;
            this.lblVenue.Text = "Venue : *";
            // 
            // lblMorningStart
            // 
            this.lblMorningStart.AutoSize = true;
            this.lblMorningStart.Location = new System.Drawing.Point(150, 310);
            this.lblMorningStart.Name = "lblMorningStart";
            this.lblMorningStart.Size = new System.Drawing.Size(62, 15);
            this.lblMorningStart.TabIndex = 33;
            this.lblMorningStart.Text = "Start Time :";
            // 
            // lblEventName
            // 
            this.lblEventName.AutoSize = true;
            this.lblEventName.Location = new System.Drawing.Point(27, 54);
            this.lblEventName.Name = "lblEventName";
            this.lblEventName.Size = new System.Drawing.Size(87, 15);
            this.lblEventName.TabIndex = 32;
            this.lblEventName.Text = "Event Name : *";
            // 
            // btnResetEvent
            // 
            this.btnResetEvent.Location = new System.Drawing.Point(537, 560);
            this.btnResetEvent.Name = "btnResetEvent";
            this.btnResetEvent.Click += new System.EventHandler(this.btnResetEvent_Click);
            this.btnResetEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetEvent.Size = new System.Drawing.Size(112, 37);
            this.btnResetEvent.TabIndex = 2;
            this.btnResetEvent.Text = "Reset";
            this.btnResetEvent.UseVisualStyleBackColor = true;
            // 
            // btnSaveEvent
            // 
            this.btnSaveEvent.Location = new System.Drawing.Point(655, 560);
            this.btnSaveEvent.Name = "btnSaveEvent";
            this.btnSaveEvent.Click += new System.EventHandler(this.btnSaveEvent_Click);
            this.btnSaveEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveEvent.Size = new System.Drawing.Size(112, 37);
            this.btnSaveEvent.TabIndex = 1;
            this.btnSaveEvent.Text = "Save Event";
            this.btnSaveEvent.UseVisualStyleBackColor = true;
            // 
            // dtpAfternoonEnd
            // 
            this.dtpAfternoonEnd.Location = new System.Drawing.Point(488, 340);
            this.dtpAfternoonEnd.Name = "dtpAfternoonEnd";
            this.dtpAfternoonEnd.ValueChanged += new System.EventHandler(this.scheduleValue_Changed);
            this.dtpAfternoonEnd.ShowUpDown = true;
            this.dtpAfternoonEnd.CustomFormat = "hh:mm tt";
            this.dtpAfternoonEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAfternoonEnd.Size = new System.Drawing.Size(150, 23);
            this.dtpAfternoonEnd.TabIndex = 12;
            // 
            // dtpAfternoonStart
            // 
            this.dtpAfternoonStart.Location = new System.Drawing.Point(232, 340);
            this.dtpAfternoonStart.Name = "dtpAfternoonStart";
            this.dtpAfternoonStart.ValueChanged += new System.EventHandler(this.scheduleValue_Changed);
            this.dtpAfternoonStart.ShowUpDown = true;
            this.dtpAfternoonStart.CustomFormat = "hh:mm tt";
            this.dtpAfternoonStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAfternoonStart.Size = new System.Drawing.Size(150, 23);
            this.dtpAfternoonStart.TabIndex = 11;
            // 
            // lblAfternoonEnd
            // 
            this.lblAfternoonEnd.AutoSize = true;
            this.lblAfternoonEnd.Location = new System.Drawing.Point(410, 344);
            this.lblAfternoonEnd.Name = "lblAfternoonEnd";
            this.lblAfternoonEnd.Size = new System.Drawing.Size(75, 15);
            this.lblAfternoonEnd.TabIndex = 47;
            this.lblAfternoonEnd.Text = "End Time :";
            // 
            // lblAfternoonStart
            // 
            this.lblAfternoonStart.AutoSize = true;
            this.lblAfternoonStart.Location = new System.Drawing.Point(150, 344);
            this.lblAfternoonStart.Name = "lblAfternoonStart";
            this.lblAfternoonStart.Size = new System.Drawing.Size(71, 15);
            this.lblAfternoonStart.TabIndex = 46;
            this.lblAfternoonStart.Text = "Start Time :";
            // 
            // dtpEveningEnd
            // 
            this.dtpEveningEnd.Location = new System.Drawing.Point(488, 374);
            this.dtpEveningEnd.Name = "dtpEveningEnd";
            this.dtpEveningEnd.ValueChanged += new System.EventHandler(this.scheduleValue_Changed);
            this.dtpEveningEnd.ShowUpDown = true;
            this.dtpEveningEnd.CustomFormat = "hh:mm tt";
            this.dtpEveningEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEveningEnd.Size = new System.Drawing.Size(150, 23);
            this.dtpEveningEnd.TabIndex = 15;
            // 
            // dtpEveningStart
            // 
            this.dtpEveningStart.Location = new System.Drawing.Point(232, 374);
            this.dtpEveningStart.Name = "dtpEveningStart";
            this.dtpEveningStart.ValueChanged += new System.EventHandler(this.scheduleValue_Changed);
            this.dtpEveningStart.ShowUpDown = true;
            this.dtpEveningStart.CustomFormat = "hh:mm tt";
            this.dtpEveningStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEveningStart.Size = new System.Drawing.Size(150, 23);
            this.dtpEveningStart.TabIndex = 14;
            // 
            // lblEveningEnd
            // 
            this.lblEveningEnd.AutoSize = true;
            this.lblEveningEnd.Location = new System.Drawing.Point(410, 378);
            this.lblEveningEnd.Name = "lblEveningEnd";
            this.lblEveningEnd.Size = new System.Drawing.Size(75, 15);
            this.lblEveningEnd.TabIndex = 51;
            this.lblEveningEnd.Text = "End Time :";
            // 
            // lblEveningStart
            // 
            this.lblEveningStart.AutoSize = true;
            this.lblEveningStart.Location = new System.Drawing.Point(150, 378);
            this.lblEveningStart.Name = "lblEveningStart";
            this.lblEveningStart.Size = new System.Drawing.Size(60, 15);
            this.lblEveningStart.TabIndex = 50;
            this.lblEveningStart.Text = "Start Time :";
            // 
            // chkLogoutNeeded
            // 
            this.chkLogoutNeeded.AutoSize = true;
            this.chkLogoutNeeded.Location = new System.Drawing.Point(30, 410);
            this.chkLogoutNeeded.Name = "chkLogoutNeeded";
            this.chkLogoutNeeded.Size = new System.Drawing.Size(450, 19);
            this.chkLogoutNeeded.TabIndex = 16;
            this.chkLogoutNeeded.Text = "Log out Needed  (students must also scan their face when leaving)";
            this.chkLogoutNeeded.UseVisualStyleBackColor = true;
            // 
            // lblOrganizer
            // 
            this.lblOrganizer.AutoSize = true;
            this.lblOrganizer.BackColor = System.Drawing.Color.Transparent;
            this.lblOrganizer.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrganizer.Location = new System.Drawing.Point(390, 54);
            this.lblOrganizer.Name = "lblOrganizer";
            this.lblOrganizer.Size = new System.Drawing.Size(200, 15);
            this.lblOrganizer.TabIndex = 0;
            this.lblOrganizer.Text = "Organizer : (optional)";
            // 
            // txtOrganizer
            // 
            this.txtOrganizer.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrganizer.Location = new System.Drawing.Point(393, 72);
            this.txtOrganizer.Name = "txtOrganizer";
            this.txtOrganizer.Size = new System.Drawing.Size(330, 23);
            this.txtOrganizer.TabIndex = 1;
            this.txtOrganizer.MaxLength = 120;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.BackColor = System.Drawing.Color.Transparent;
            this.lblEndDate.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(265, 162);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(150, 15);
            this.lblEndDate.TabIndex = 0;
            this.lblEndDate.Text = "End Date : *";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "ddd, MMM dd, yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(268, 180);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(215, 23);
            this.dtpEndDate.TabIndex = 4;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.scheduleValue_Changed);
            // 
            // lblNumberOfDays
            // 
            this.lblNumberOfDays.BackColor = System.Drawing.Color.Transparent;
            this.lblNumberOfDays.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfDays.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblNumberOfDays.Location = new System.Drawing.Point(500, 183);
            this.lblNumberOfDays.Name = "lblNumberOfDays";
            this.lblNumberOfDays.Size = new System.Drawing.Size(223, 18);
            this.lblNumberOfDays.TabIndex = 0;
            this.lblNumberOfDays.Text = "1 day";
            // 
            // lblForDepartment
            // 
            this.lblForDepartment.AutoSize = true;
            this.lblForDepartment.BackColor = System.Drawing.Color.Transparent;
            this.lblForDepartment.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForDepartment.Location = new System.Drawing.Point(27, 212);
            this.lblForDepartment.Name = "lblForDepartment";
            this.lblForDepartment.Size = new System.Drawing.Size(150, 15);
            this.lblForDepartment.TabIndex = 0;
            this.lblForDepartment.Text = "For Department :";
            // 
            // cmbEventDepartment
            // 
            this.cmbEventDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventDepartment.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEventDepartment.FormattingEnabled = true;
            this.cmbEventDepartment.Location = new System.Drawing.Point(30, 230);
            this.cmbEventDepartment.Name = "cmbEventDepartment";
            this.cmbEventDepartment.Size = new System.Drawing.Size(330, 23);
            this.cmbEventDepartment.TabIndex = 5;
            this.cmbEventDepartment.SelectedIndexChanged += new System.EventHandler(this.cmbParticipants_SelectedIndexChanged);
            // 
            // lblForYear
            // 
            this.lblForYear.AutoSize = true;
            this.lblForYear.BackColor = System.Drawing.Color.Transparent;
            this.lblForYear.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForYear.Location = new System.Drawing.Point(390, 212);
            this.lblForYear.Name = "lblForYear";
            this.lblForYear.Size = new System.Drawing.Size(150, 15);
            this.lblForYear.TabIndex = 0;
            this.lblForYear.Text = "For Year Level :";
            // 
            // cmbEventYear
            // 
            this.cmbEventYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventYear.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEventYear.FormattingEnabled = true;
            this.cmbEventYear.Items.AddRange(new object[] {
            "All Year Levels",
            "1st Year",
            "2nd Year",
            "3rd Year",
            "4th Year"});
            this.cmbEventYear.Location = new System.Drawing.Point(393, 230);
            this.cmbEventYear.Name = "cmbEventYear";
            this.cmbEventYear.Size = new System.Drawing.Size(330, 23);
            this.cmbEventYear.TabIndex = 6;
            this.cmbEventYear.SelectedIndexChanged += new System.EventHandler(this.cmbParticipants_SelectedIndexChanged);
            // 
            // lblExpectedStudents
            // 
            this.lblExpectedStudents.BackColor = System.Drawing.Color.Transparent;
            this.lblExpectedStudents.Font = new System.Drawing.Font("Cambria", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpectedStudents.ForeColor = System.Drawing.Color.DimGray;
            this.lblExpectedStudents.Location = new System.Drawing.Point(30, 256);
            this.lblExpectedStudents.Name = "lblExpectedStudents";
            this.lblExpectedStudents.Size = new System.Drawing.Size(693, 18);
            this.lblExpectedStudents.TabIndex = 0;
            this.lblExpectedStudents.Text = "Expected students: 0";
            // 
            // lblSessionsTitle
            // 
            this.lblSessionsTitle.AutoSize = true;
            this.lblSessionsTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSessionsTitle.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionsTitle.Location = new System.Drawing.Point(27, 282);
            this.lblSessionsTitle.Name = "lblSessionsTitle";
            this.lblSessionsTitle.Size = new System.Drawing.Size(600, 15);
            this.lblSessionsTitle.TabIndex = 0;
            this.lblSessionsTitle.Text = "Attendance Sessions (check the sessions that happen EVERY day of the event) :";
            // 
            // chkMorning
            // 
            this.chkMorning.BackColor = System.Drawing.Color.Transparent;
            this.chkMorning.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMorning.Location = new System.Drawing.Point(30, 306);
            this.chkMorning.Name = "chkMorning";
            this.chkMorning.Size = new System.Drawing.Size(105, 22);
            this.chkMorning.TabIndex = 7;
            this.chkMorning.Text = "Morning";
            this.chkMorning.UseVisualStyleBackColor = false;
            this.chkMorning.CheckedChanged += new System.EventHandler(this.chkSession_CheckedChanged);
            // 
            // chkAfternoon
            // 
            this.chkAfternoon.BackColor = System.Drawing.Color.Transparent;
            this.chkAfternoon.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAfternoon.Location = new System.Drawing.Point(30, 340);
            this.chkAfternoon.Name = "chkAfternoon";
            this.chkAfternoon.Size = new System.Drawing.Size(105, 22);
            this.chkAfternoon.TabIndex = 10;
            this.chkAfternoon.Text = "Afternoon";
            this.chkAfternoon.UseVisualStyleBackColor = false;
            this.chkAfternoon.CheckedChanged += new System.EventHandler(this.chkSession_CheckedChanged);
            // 
            // chkEvening
            // 
            this.chkEvening.BackColor = System.Drawing.Color.Transparent;
            this.chkEvening.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEvening.Location = new System.Drawing.Point(30, 374);
            this.chkEvening.Name = "chkEvening";
            this.chkEvening.Size = new System.Drawing.Size(105, 22);
            this.chkEvening.TabIndex = 13;
            this.chkEvening.Text = "Evening";
            this.chkEvening.UseVisualStyleBackColor = false;
            this.chkEvening.CheckedChanged += new System.EventHandler(this.chkSession_CheckedChanged);
            // 
            // lblScheduleInfo
            // 
            this.lblScheduleInfo.BackColor = System.Drawing.Color.AliceBlue;
            this.lblScheduleInfo.Font = new System.Drawing.Font("Cambria", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScheduleInfo.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblScheduleInfo.Location = new System.Drawing.Point(30, 436);
            this.lblScheduleInfo.Name = "lblScheduleInfo";
            this.lblScheduleInfo.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.lblScheduleInfo.Size = new System.Drawing.Size(693, 48);
            this.lblScheduleInfo.TabIndex = 0;
            this.lblScheduleInfo.Text = "";
            // 
            // btnCancelEvent
            // 
            this.btnCancelEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelEvent.Location = new System.Drawing.Point(419, 560);
            this.btnCancelEvent.Name = "btnCancelEvent";
            this.btnCancelEvent.Size = new System.Drawing.Size(112, 37);
            this.btnCancelEvent.TabIndex = 3;
            this.btnCancelEvent.Text = "Cancel";
            this.btnCancelEvent.UseVisualStyleBackColor = true;
            this.btnCancelEvent.Click += new System.EventHandler(this.btnCancelEvent_Click);
            // 
            // AddEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 612);
            this.Controls.Add(this.btnCancelEvent);
            this.Controls.Add(this.btnSaveEvent);
            this.Controls.Add(this.btnResetEvent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlEventInfo);
            this.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "AddEvent";
            this.CancelButton = this.btnCancelEvent;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SCC Records Management System - Add Event";
            this.Load += new System.EventHandler(this.AddEvent_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlEventInfo.ResumeLayout(false);
            this.pnlEventInfo.PerformLayout();
            this.pnlSectionHeader.ResumeLayout(false);
            this.pnlSectionHeader.PerformLayout();
            this.pnlVenue.ResumeLayout(false);
            this.pnlVenue.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancelEvent;
        private System.Windows.Forms.Label lblScheduleInfo;
        private System.Windows.Forms.CheckBox chkEvening;
        private System.Windows.Forms.CheckBox chkAfternoon;
        private System.Windows.Forms.CheckBox chkMorning;
        private System.Windows.Forms.Label lblSessionsTitle;
        private System.Windows.Forms.Label lblExpectedStudents;
        private System.Windows.Forms.ComboBox cmbEventYear;
        private System.Windows.Forms.Label lblForYear;
        private System.Windows.Forms.ComboBox cmbEventDepartment;
        private System.Windows.Forms.Label lblForDepartment;
        private System.Windows.Forms.Label lblNumberOfDays;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.TextBox txtOrganizer;
        private System.Windows.Forms.Label lblOrganizer;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlEventInfo;
        private System.Windows.Forms.Label lblMorningStart;
        private System.Windows.Forms.Label lblEventName;
        private System.Windows.Forms.Label lblSectionTitle;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Panel pnlSectionHeader;
        private System.Windows.Forms.Label lblVenue;
        private System.Windows.Forms.Panel pnlVenue;
        private System.Windows.Forms.TextBox txtVenue;
        private System.Windows.Forms.Label lblMorningEnd;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.TextBox txtEventName;
        private System.Windows.Forms.Button btnResetEvent;
        private System.Windows.Forms.Button btnSaveEvent;
        private System.Windows.Forms.DateTimePicker dtpMorningEnd;
        private System.Windows.Forms.DateTimePicker dtpMorningStart;
        private System.Windows.Forms.DateTimePicker dtpAfternoonEnd;
        private System.Windows.Forms.DateTimePicker dtpAfternoonStart;
        private System.Windows.Forms.Label lblAfternoonEnd;
        private System.Windows.Forms.Label lblAfternoonStart;
        private System.Windows.Forms.DateTimePicker dtpEveningEnd;
        private System.Windows.Forms.DateTimePicker dtpEveningStart;
        private System.Windows.Forms.Label lblEveningEnd;
        private System.Windows.Forms.Label lblEveningStart;
        private System.Windows.Forms.CheckBox chkLogoutNeeded;
    }
}