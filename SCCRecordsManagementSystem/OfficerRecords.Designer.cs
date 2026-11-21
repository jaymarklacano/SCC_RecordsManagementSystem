namespace SCCRecordsManagementSystem
{
    partial class OfficerRecords
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
            this.lblListInfo = new System.Windows.Forms.Label();
            this.lblTotalOfficersTitle = new System.Windows.Forms.Label();
            this.lblTotalOfficers = new System.Windows.Forms.Label();
            this.pnlTotalOfficers = new System.Windows.Forms.Panel();
            this.btnShowAllOfficers = new System.Windows.Forms.Button();
            this.cmbSchoolYearOfficer = new System.Windows.Forms.ComboBox();
            this.lblSchoolYear = new System.Windows.Forms.Label();
            this.cmbPositionOfficer = new System.Windows.Forms.ComboBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.cmbDepartmentOfficer = new System.Windows.Forms.ComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.pnlOfficerFilter = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtgOfficer = new System.Windows.Forms.DataGridView();
            this.btnAddOfficer = new System.Windows.Forms.Button();
            this.txtSearchOfficer = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOffLogout = new System.Windows.Forms.Button();
            this.btnOffDash = new System.Windows.Forms.Button();
            this.btnOffRules = new System.Windows.Forms.Button();
            this.btnOffObligation = new System.Windows.Forms.Button();
            this.btnOffReport = new System.Windows.Forms.Button();
            this.btnOffAttendance = new System.Windows.Forms.Button();
            this.btnOffEvent = new System.Windows.Forms.Button();
            this.btnOffFace = new System.Windows.Forms.Button();
            this.btnOffOfficer = new System.Windows.Forms.Button();
            this.btnOffStudent = new System.Windows.Forms.Button();
            this.btnOffDept = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnDeleteOfficer = new System.Windows.Forms.Button();
            this.btnEditOfficer = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOfficer)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Location = new System.Drawing.Point(315, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(992, 49);
            this.pnlHeader.TabIndex = 26;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(78, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(138, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Officer Records";
            // 
            // dtgOfficer
            // 
            this.dtgOfficer.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dtgOfficer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgOfficer.Location = new System.Drawing.Point(325, 187);
            this.dtgOfficer.Name = "dtgOfficer";
            this.dtgOfficer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgOfficer_CellDoubleClick);
            this.dtgOfficer.ReadOnly = true;
            this.dtgOfficer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgOfficer.RowHeadersVisible = false;
            this.dtgOfficer.MultiSelect = false;
            this.dtgOfficer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgOfficer.AllowUserToResizeRows = false;
            this.dtgOfficer.AllowUserToDeleteRows = false;
            this.dtgOfficer.AllowUserToAddRows = false;
            this.dtgOfficer.RowHeadersWidth = 51;
            this.dtgOfficer.RowTemplate.Height = 24;
            this.dtgOfficer.Size = new System.Drawing.Size(968, 395);
            this.dtgOfficer.TabIndex = 28;
            // 
            // btnAddOfficer
            // 
            this.btnAddOfficer.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddOfficer.Location = new System.Drawing.Point(1164, 140);
            this.btnAddOfficer.Name = "btnAddOfficer";
            this.btnAddOfficer.Click += new System.EventHandler(this.btnAddOfficer_Click);
            this.btnAddOfficer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnAddOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddOfficer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnAddOfficer.Size = new System.Drawing.Size(129, 40);
            this.btnAddOfficer.TabIndex = 29;
            this.btnAddOfficer.Text = "Add Officer";
            this.btnAddOfficer.UseVisualStyleBackColor = false;
            // 
            // txtSearchOfficer
            // 
            this.txtSearchOfficer.Location = new System.Drawing.Point(398, 149);
            this.txtSearchOfficer.Multiline = false;
            this.txtSearchOfficer.Name = "txtSearchOfficer";
            this.txtSearchOfficer.TextChanged += new System.EventHandler(this.filter_Changed);
            this.txtSearchOfficer.MaxLength = 60;
            this.txtSearchOfficer.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchOfficer.Size = new System.Drawing.Size(480, 27);
            this.txtSearchOfficer.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.panel1.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._904439575_1911191946735306_1039497458727327980_n1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.btnOffLogout);
            this.panel1.Controls.Add(this.btnOffDash);
            this.panel1.Controls.Add(this.btnOffRules);
            this.panel1.Controls.Add(this.btnOffObligation);
            this.panel1.Controls.Add(this.btnOffReport);
            this.panel1.Controls.Add(this.btnOffAttendance);
            this.panel1.Controls.Add(this.btnOffEvent);
            this.panel1.Controls.Add(this.btnOffFace);
            this.panel1.Controls.Add(this.btnOffOfficer);
            this.panel1.Controls.Add(this.btnOffStudent);
            this.panel1.Controls.Add(this.btnOffDept);
            this.panel1.Location = new System.Drawing.Point(-4, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 627);
            this.panel1.TabIndex = 32;
            // 
            // btnOffLogout
            // 
            this.btnOffLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnOffLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffLogout.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(75)))), ((int)(((byte)(80)))));
            this.btnOffLogout.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_log_out_24__2_;
            this.btnOffLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffLogout.Location = new System.Drawing.Point(22, 560);
            this.btnOffLogout.Name = "btnOffLogout";
            this.btnOffLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffLogout.Click += new System.EventHandler(this.btnOffLogout_Click);
            this.btnOffLogout.Size = new System.Drawing.Size(277, 38);
            this.btnOffLogout.TabIndex = 11;
            this.btnOffLogout.Text = "   LogOut";
            this.btnOffLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffLogout.UseVisualStyleBackColor = false;
            // 
            // btnOffDash
            // 
            this.btnOffDash.BackColor = System.Drawing.Color.Transparent;
            this.btnOffDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffDash.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffDash.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffDash.Image = global::SCCRecordsManagementSystem.Properties.Resources.house__1_;
            this.btnOffDash.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffDash.Location = new System.Drawing.Point(22, 123);
            this.btnOffDash.Name = "btnOffDash";
            this.btnOffDash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffDash.Click += new System.EventHandler(this.btnOffDash_Click);
            this.btnOffDash.Size = new System.Drawing.Size(277, 36);
            this.btnOffDash.TabIndex = 1;
            this.btnOffDash.Text = "   Dashboard";
            this.btnOffDash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffDash.UseVisualStyleBackColor = false;
            // 
            // btnOffRules
            // 
            this.btnOffRules.BackColor = System.Drawing.Color.Transparent;
            this.btnOffRules.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffRules.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffRules.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffRules.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_rules_book_24;
            this.btnOffRules.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffRules.Location = new System.Drawing.Point(22, 465);
            this.btnOffRules.Name = "btnOffRules";
            this.btnOffRules.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffRules.Click += new System.EventHandler(this.btnOffRules_Click);
            this.btnOffRules.Size = new System.Drawing.Size(277, 36);
            this.btnOffRules.TabIndex = 10;
            this.btnOffRules.Text = "   Rules and Permission";
            this.btnOffRules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffRules.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffRules.UseVisualStyleBackColor = false;
            // 
            // btnOffObligation
            // 
            this.btnOffObligation.BackColor = System.Drawing.Color.Transparent;
            this.btnOffObligation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffObligation.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffObligation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffObligation.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_user_24;
            this.btnOffObligation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffObligation.Location = new System.Drawing.Point(22, 427);
            this.btnOffObligation.Name = "btnOffObligation";
            this.btnOffObligation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffObligation.Click += new System.EventHandler(this.btnOffObligation_Click);
            this.btnOffObligation.Size = new System.Drawing.Size(277, 36);
            this.btnOffObligation.TabIndex = 9;
            this.btnOffObligation.Text = "   Obligations";
            this.btnOffObligation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffObligation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffObligation.UseVisualStyleBackColor = false;
            // 
            // btnOffReport
            // 
            this.btnOffReport.BackColor = System.Drawing.Color.Transparent;
            this.btnOffReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffReport.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffReport.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffReport.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_analysis_24;
            this.btnOffReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffReport.Location = new System.Drawing.Point(22, 389);
            this.btnOffReport.Name = "btnOffReport";
            this.btnOffReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffReport.Click += new System.EventHandler(this.btnOffReport_Click);
            this.btnOffReport.Size = new System.Drawing.Size(277, 36);
            this.btnOffReport.TabIndex = 8;
            this.btnOffReport.Text = "   Reports and Analysis";
            this.btnOffReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffReport.UseVisualStyleBackColor = false;
            // 
            // btnOffAttendance
            // 
            this.btnOffAttendance.BackColor = System.Drawing.Color.Transparent;
            this.btnOffAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffAttendance.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffAttendance.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffAttendance.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_pass_fail_24;
            this.btnOffAttendance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffAttendance.Location = new System.Drawing.Point(22, 351);
            this.btnOffAttendance.Name = "btnOffAttendance";
            this.btnOffAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffAttendance.Click += new System.EventHandler(this.btnOffAttendance_Click);
            this.btnOffAttendance.Size = new System.Drawing.Size(277, 36);
            this.btnOffAttendance.TabIndex = 7;
            this.btnOffAttendance.Text = "   Attendance Monitoring";
            this.btnOffAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffAttendance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffAttendance.UseVisualStyleBackColor = false;
            // 
            // btnOffEvent
            // 
            this.btnOffEvent.BackColor = System.Drawing.Color.Transparent;
            this.btnOffEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffEvent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffEvent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffEvent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_event_accepted_tentatively_24;
            this.btnOffEvent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffEvent.Location = new System.Drawing.Point(22, 313);
            this.btnOffEvent.Name = "btnOffEvent";
            this.btnOffEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffEvent.Click += new System.EventHandler(this.btnOffEvent_Click);
            this.btnOffEvent.Size = new System.Drawing.Size(277, 36);
            this.btnOffEvent.TabIndex = 6;
            this.btnOffEvent.Text = "   Event Management";
            this.btnOffEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffEvent.UseVisualStyleBackColor = false;
            // 
            // btnOffFace
            // 
            this.btnOffFace.BackColor = System.Drawing.Color.Transparent;
            this.btnOffFace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffFace.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffFace.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffFace.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_facial_recognition_24;
            this.btnOffFace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffFace.Location = new System.Drawing.Point(22, 275);
            this.btnOffFace.Name = "btnOffFace";
            this.btnOffFace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffFace.Click += new System.EventHandler(this.btnOffFace_Click);
            this.btnOffFace.Size = new System.Drawing.Size(277, 36);
            this.btnOffFace.TabIndex = 5;
            this.btnOffFace.Text = "   Face Recognition";
            this.btnOffFace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffFace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffFace.UseVisualStyleBackColor = false;
            // 
            // btnOffOfficer
            // 
            this.btnOffOfficer.BackColor = System.Drawing.Color.Transparent;
            this.btnOffOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffOfficer.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffOfficer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffOfficer.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_management_24;
            this.btnOffOfficer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffOfficer.Location = new System.Drawing.Point(22, 237);
            this.btnOffOfficer.Name = "btnOffOfficer";
            this.btnOffOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffOfficer.Click += new System.EventHandler(this.btnOffOfficer_Click);
            this.btnOffOfficer.Size = new System.Drawing.Size(277, 36);
            this.btnOffOfficer.TabIndex = 4;
            this.btnOffOfficer.Text = "   Officer Records";
            this.btnOffOfficer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffOfficer.UseVisualStyleBackColor = false;
            // 
            // btnOffStudent
            // 
            this.btnOffStudent.BackColor = System.Drawing.Color.Transparent;
            this.btnOffStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffStudent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffStudent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffStudent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_what_i_do_241;
            this.btnOffStudent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffStudent.Location = new System.Drawing.Point(22, 199);
            this.btnOffStudent.Name = "btnOffStudent";
            this.btnOffStudent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffStudent.Click += new System.EventHandler(this.btnOffStudent_Click);
            this.btnOffStudent.Size = new System.Drawing.Size(277, 36);
            this.btnOffStudent.TabIndex = 3;
            this.btnOffStudent.Text = "   Student Records";
            this.btnOffStudent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffStudent.UseVisualStyleBackColor = false;
            // 
            // btnOffDept
            // 
            this.btnOffDept.BackColor = System.Drawing.Color.Transparent;
            this.btnOffDept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffDept.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffDept.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOffDept.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_building_24__1_;
            this.btnOffDept.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffDept.Location = new System.Drawing.Point(22, 161);
            this.btnOffDept.Name = "btnOffDept";
            this.btnOffDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOffDept.Click += new System.EventHandler(this.btnOffDept_Click);
            this.btnOffDept.Size = new System.Drawing.Size(277, 36);
            this.btnOffDept.TabIndex = 2;
            this.btnOffDept.Text = "   Departments";
            this.btnOffDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOffDept.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOffDept.UseVisualStyleBackColor = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.Black;
            this.lblSearch.Location = new System.Drawing.Point(325, 152);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(67, 20);
            this.lblSearch.TabIndex = 40;
            this.lblSearch.Text = "Search :";
            // 
            // btnDeleteOfficer
            // 
            this.btnDeleteOfficer.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteOfficer.Location = new System.Drawing.Point(1029, 140);
            this.btnDeleteOfficer.Name = "btnDeleteOfficer";
            this.btnDeleteOfficer.Click += new System.EventHandler(this.btnDeleteOfficer_Click);
            this.btnDeleteOfficer.ForeColor = System.Drawing.Color.Firebrick;
            this.btnDeleteOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteOfficer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnDeleteOfficer.Size = new System.Drawing.Size(129, 40);
            this.btnDeleteOfficer.TabIndex = 41;
            this.btnDeleteOfficer.Text = "Delete";
            this.btnDeleteOfficer.UseVisualStyleBackColor = false;
            // 
            // btnEditOfficer
            // 
            this.btnEditOfficer.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditOfficer.Location = new System.Drawing.Point(894, 140);
            this.btnEditOfficer.Name = "btnEditOfficer";
            this.btnEditOfficer.Click += new System.EventHandler(this.btnEditOfficer_Click);
            this.btnEditOfficer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnEditOfficer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditOfficer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditOfficer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnEditOfficer.Size = new System.Drawing.Size(129, 40);
            this.btnEditOfficer.TabIndex = 42;
            this.btnEditOfficer.Text = "Edit";
            this.btnEditOfficer.UseVisualStyleBackColor = false;
            // 
            // pnlOfficerFilter
            // 
            this.pnlOfficerFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.pnlOfficerFilter.Controls.Add(this.pnlTotalOfficers);
            this.pnlOfficerFilter.Controls.Add(this.btnShowAllOfficers);
            this.pnlOfficerFilter.Controls.Add(this.cmbSchoolYearOfficer);
            this.pnlOfficerFilter.Controls.Add(this.lblSchoolYear);
            this.pnlOfficerFilter.Controls.Add(this.cmbPositionOfficer);
            this.pnlOfficerFilter.Controls.Add(this.lblPosition);
            this.pnlOfficerFilter.Controls.Add(this.cmbDepartmentOfficer);
            this.pnlOfficerFilter.Controls.Add(this.lblDepartment);
            this.pnlOfficerFilter.Location = new System.Drawing.Point(325, 57);
            this.pnlOfficerFilter.Name = "pnlOfficerFilter";
            this.pnlOfficerFilter.Size = new System.Drawing.Size(968, 76);
            this.pnlOfficerFilter.TabIndex = 1;
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.BackColor = System.Drawing.Color.Transparent;
            this.lblDepartment.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.Location = new System.Drawing.Point(15, 8);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(95, 17);
            this.lblDepartment.TabIndex = 0;
            this.lblDepartment.Text = "Course :";
            // 
            // cmbDepartmentOfficer
            // 
            this.cmbDepartmentOfficer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartmentOfficer.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDepartmentOfficer.FormattingEnabled = true;
            this.cmbDepartmentOfficer.Location = new System.Drawing.Point(15, 32);
            this.cmbDepartmentOfficer.Name = "cmbDepartmentOfficer";
            this.cmbDepartmentOfficer.Size = new System.Drawing.Size(200, 24);
            this.cmbDepartmentOfficer.TabIndex = 0;
            this.cmbDepartmentOfficer.SelectedIndexChanged += new System.EventHandler(this.filter_Changed);
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.BackColor = System.Drawing.Color.Transparent;
            this.lblPosition.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.Location = new System.Drawing.Point(230, 8);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(70, 17);
            this.lblPosition.TabIndex = 0;
            this.lblPosition.Text = "Position :";
            // 
            // cmbPositionOfficer
            // 
            this.cmbPositionOfficer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPositionOfficer.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPositionOfficer.FormattingEnabled = true;
            this.cmbPositionOfficer.Location = new System.Drawing.Point(230, 32);
            this.cmbPositionOfficer.Name = "cmbPositionOfficer";
            this.cmbPositionOfficer.Size = new System.Drawing.Size(200, 24);
            this.cmbPositionOfficer.TabIndex = 0;
            this.cmbPositionOfficer.SelectedIndexChanged += new System.EventHandler(this.filter_Changed);
            // 
            // lblSchoolYear
            // 
            this.lblSchoolYear.AutoSize = true;
            this.lblSchoolYear.BackColor = System.Drawing.Color.Transparent;
            this.lblSchoolYear.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchoolYear.Location = new System.Drawing.Point(445, 8);
            this.lblSchoolYear.Name = "lblSchoolYear";
            this.lblSchoolYear.Size = new System.Drawing.Size(95, 17);
            this.lblSchoolYear.TabIndex = 0;
            this.lblSchoolYear.Text = "School Year :";
            // 
            // cmbSchoolYearOfficer
            // 
            this.cmbSchoolYearOfficer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSchoolYearOfficer.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSchoolYearOfficer.FormattingEnabled = true;
            this.cmbSchoolYearOfficer.Location = new System.Drawing.Point(445, 32);
            this.cmbSchoolYearOfficer.Name = "cmbSchoolYearOfficer";
            this.cmbSchoolYearOfficer.Size = new System.Drawing.Size(170, 24);
            this.cmbSchoolYearOfficer.TabIndex = 0;
            this.cmbSchoolYearOfficer.SelectedIndexChanged += new System.EventHandler(this.filter_Changed);
            // 
            // btnShowAllOfficers
            // 
            this.btnShowAllOfficers.BackColor = System.Drawing.Color.White;
            this.btnShowAllOfficers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowAllOfficers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAllOfficers.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAllOfficers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnShowAllOfficers.Location = new System.Drawing.Point(630, 20);
            this.btnShowAllOfficers.Name = "btnShowAllOfficers";
            this.btnShowAllOfficers.Size = new System.Drawing.Size(150, 40);
            this.btnShowAllOfficers.TabIndex = 0;
            this.btnShowAllOfficers.Text = "Show All";
            this.btnShowAllOfficers.UseVisualStyleBackColor = false;
            this.btnShowAllOfficers.Click += new System.EventHandler(this.btnShowAllOfficers_Click);
            // 
            // pnlTotalOfficers
            // 
            this.pnlTotalOfficers.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlTotalOfficers.Controls.Add(this.lblTotalOfficersTitle);
            this.pnlTotalOfficers.Controls.Add(this.lblTotalOfficers);
            this.pnlTotalOfficers.Location = new System.Drawing.Point(795, 6);
            this.pnlTotalOfficers.Name = "pnlTotalOfficers";
            this.pnlTotalOfficers.Size = new System.Drawing.Size(160, 64);
            this.pnlTotalOfficers.TabIndex = 0;
            // 
            // lblTotalOfficers
            // 
            this.lblTotalOfficers.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalOfficers.Font = new System.Drawing.Font("Cambria", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOfficers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblTotalOfficers.Location = new System.Drawing.Point(0, 26);
            this.lblTotalOfficers.Name = "lblTotalOfficers";
            this.lblTotalOfficers.Size = new System.Drawing.Size(160, 34);
            this.lblTotalOfficers.TabIndex = 0;
            this.lblTotalOfficers.Text = "0";
            this.lblTotalOfficers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalOfficersTitle
            // 
            this.lblTotalOfficersTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalOfficersTitle.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOfficersTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTotalOfficersTitle.Name = "lblTotalOfficersTitle";
            this.lblTotalOfficersTitle.Size = new System.Drawing.Size(160, 20);
            this.lblTotalOfficersTitle.TabIndex = 0;
            this.lblTotalOfficersTitle.Text = "Total Officers";
            this.lblTotalOfficersTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblListInfo
            // 
            this.lblListInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblListInfo.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblListInfo.Location = new System.Drawing.Point(325, 588);
            this.lblListInfo.Name = "lblListInfo";
            this.lblListInfo.Size = new System.Drawing.Size(968, 22);
            this.lblListInfo.TabIndex = 0;
            this.lblListInfo.Text = "";
            // 
            // lblNoRecord
            // 
            this.lblNoRecord.BackColor = System.Drawing.Color.White;
            this.lblNoRecord.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoRecord.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoRecord.Location = new System.Drawing.Point(560, 350);
            this.lblNoRecord.Name = "lblNoRecord";
            this.lblNoRecord.Size = new System.Drawing.Size(500, 40);
            this.lblNoRecord.TabIndex = 0;
            this.lblNoRecord.Text = "No officers found.";
            this.lblNoRecord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoRecord.Visible = false;
            // 
            // OfficerRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.Controls.Add(this.lblNoRecord);
            this.Controls.Add(this.lblListInfo);
            this.Controls.Add(this.pnlOfficerFilter);
            this.Controls.Add(this.btnEditOfficer);
            this.Controls.Add(this.btnDeleteOfficer);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtSearchOfficer);
            this.Controls.Add(this.btnAddOfficer);
            this.Controls.Add(this.dtgOfficer);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OfficerRecords";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SCC Records Management System - Officer Records";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OfficerRecords_FormClosed);
            this.Load += new System.EventHandler(this.OfficerRecords_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOfficer)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblNoRecord;
        private System.Windows.Forms.Label lblListInfo;
        private System.Windows.Forms.Label lblTotalOfficersTitle;
        private System.Windows.Forms.Label lblTotalOfficers;
        private System.Windows.Forms.Panel pnlTotalOfficers;
        private System.Windows.Forms.Button btnShowAllOfficers;
        private System.Windows.Forms.ComboBox cmbSchoolYearOfficer;
        private System.Windows.Forms.Label lblSchoolYear;
        private System.Windows.Forms.ComboBox cmbPositionOfficer;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.ComboBox cmbDepartmentOfficer;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Panel pnlOfficerFilter;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dtgOfficer;
        private System.Windows.Forms.Button btnAddOfficer;
        private System.Windows.Forms.TextBox txtSearchOfficer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOffLogout;
        private System.Windows.Forms.Button btnOffDash;
        private System.Windows.Forms.Button btnOffRules;
        private System.Windows.Forms.Button btnOffObligation;
        private System.Windows.Forms.Button btnOffReport;
        private System.Windows.Forms.Button btnOffAttendance;
        private System.Windows.Forms.Button btnOffEvent;
        private System.Windows.Forms.Button btnOffFace;
        private System.Windows.Forms.Button btnOffOfficer;
        private System.Windows.Forms.Button btnOffStudent;
        private System.Windows.Forms.Button btnOffDept;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnDeleteOfficer;
        private System.Windows.Forms.Button btnEditOfficer;
    }
}