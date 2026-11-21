namespace SCCRecordsManagementSystem
{
    partial class LoginHistory
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblListInfo = new System.Windows.Forms.Label();
            this.lblNoRecord = new System.Windows.Forms.Label();
            this.lblSessionNote = new System.Windows.Forms.Label();
            this.btnLast30Days = new System.Windows.Forms.Button();
            this.btnLast7Days = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.lblQuick = new System.Windows.Forms.Label();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.cmbRoleFilter = new System.Windows.Forms.ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblDash = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.lblSearchUsername = new System.Windows.Forms.Label();
            this.dtgLoginRecords = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLoginRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Location = new System.Drawing.Point(-2, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(977, 64);
            this.pnlHeader.TabIndex = 30;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(53, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(170, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Login History";
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.lblSessionNote);
            this.pnlFilter.Controls.Add(this.btnLast30Days);
            this.pnlFilter.Controls.Add(this.btnLast7Days);
            this.pnlFilter.Controls.Add(this.btnToday);
            this.pnlFilter.Controls.Add(this.lblQuick);
            this.pnlFilter.Controls.Add(this.btnShowAll);
            this.pnlFilter.Controls.Add(this.cmbRoleFilter);
            this.pnlFilter.Controls.Add(this.lblRole);
            this.pnlFilter.Controls.Add(this.dtpEndDate);
            this.pnlFilter.Controls.Add(this.lblDash);
            this.pnlFilter.Controls.Add(this.dtpStartDate);
            this.pnlFilter.Controls.Add(this.txtUserName);
            this.pnlFilter.Controls.Add(this.lblDateRange);
            this.pnlFilter.Controls.Add(this.lblSearchUsername);
            this.pnlFilter.Location = new System.Drawing.Point(12, 70);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlFilter.Size = new System.Drawing.Size(946, 116);
            this.pnlFilter.TabIndex = 31;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(603, 34);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.filter_Changed);
            this.dtpEndDate.CustomFormat = "MMM dd, yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Size = new System.Drawing.Size(155, 26);
            this.dtpEndDate.TabIndex = 5;
            // 
            // lblDash
            // 
            this.lblDash.AutoSize = true;
            this.lblDash.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDash.Location = new System.Drawing.Point(582, 32);
            this.lblDash.Name = "lblDash";
            this.lblDash.Size = new System.Drawing.Size(22, 29);
            this.lblDash.TabIndex = 4;
            this.lblDash.Text = "-";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(423, 34);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.filter_Changed);
            this.dtpStartDate.CustomFormat = "MMM dd, yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Size = new System.Drawing.Size(155, 26);
            this.dtpStartDate.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(23, 34);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.TextChanged += new System.EventHandler(this.filter_Changed);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Size = new System.Drawing.Size(230, 26);
            this.txtUserName.TabIndex = 2;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(420, 12);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateRange.Size = new System.Drawing.Size(83, 16);
            this.lblDateRange.TabIndex = 1;
            this.lblDateRange.Text = "Date Range :";
            // 
            // lblSearchUsername
            // 
            this.lblSearchUsername.AutoSize = true;
            this.lblSearchUsername.Location = new System.Drawing.Point(20, 12);
            this.lblSearchUsername.Name = "lblSearchUsername";
            this.lblSearchUsername.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchUsername.Size = new System.Drawing.Size(119, 16);
            this.lblSearchUsername.TabIndex = 0;
            this.lblSearchUsername.Text = "Search Username / Name :";
            // 
            // dtgLoginRecords
            // 
            this.dtgLoginRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgLoginRecords.Location = new System.Drawing.Point(12, 192);
            this.dtgLoginRecords.Name = "dtgLoginRecords";
            this.dtgLoginRecords.ReadOnly = true;
            this.dtgLoginRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgLoginRecords.RowHeadersVisible = false;
            this.dtgLoginRecords.MultiSelect = false;
            this.dtgLoginRecords.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dtgLoginRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgLoginRecords.AllowUserToResizeRows = false;
            this.dtgLoginRecords.AllowUserToDeleteRows = false;
            this.dtgLoginRecords.AllowUserToAddRows = false;
            this.dtgLoginRecords.RowHeadersWidth = 51;
            this.dtgLoginRecords.RowTemplate.Height = 24;
            this.dtgLoginRecords.Size = new System.Drawing.Size(946, 258);
            this.dtgLoginRecords.TabIndex = 32;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(875, 465);
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnClose.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnClose.Size = new System.Drawing.Size(83, 42);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.BackColor = System.Drawing.Color.Transparent;
            this.lblRole.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.Location = new System.Drawing.Point(270, 12);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(50, 17);
            this.lblRole.TabIndex = 0;
            this.lblRole.Text = "Role :";
            // 
            // cmbRoleFilter
            // 
            this.cmbRoleFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoleFilter.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRoleFilter.FormattingEnabled = true;
            this.cmbRoleFilter.Items.AddRange(new object[] {
            "All Roles",
            "Admin",
            "Officer"});
            this.cmbRoleFilter.Location = new System.Drawing.Point(273, 34);
            this.cmbRoleFilter.Name = "cmbRoleFilter";
            this.cmbRoleFilter.Size = new System.Drawing.Size(130, 24);
            this.cmbRoleFilter.TabIndex = 0;
            this.cmbRoleFilter.SelectedIndexChanged += new System.EventHandler(this.filter_Changed);
            // 
            // btnShowAll
            // 
            this.btnShowAll.BackColor = System.Drawing.Color.White;
            this.btnShowAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAll.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnShowAll.Location = new System.Drawing.Point(785, 30);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(140, 32);
            this.btnShowAll.TabIndex = 0;
            this.btnShowAll.Text = "Show All / Reset";
            this.btnShowAll.UseVisualStyleBackColor = false;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // lblQuick
            // 
            this.lblQuick.AutoSize = true;
            this.lblQuick.BackColor = System.Drawing.Color.Transparent;
            this.lblQuick.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuick.Location = new System.Drawing.Point(20, 80);
            this.lblQuick.Name = "lblQuick";
            this.lblQuick.Size = new System.Drawing.Size(90, 17);
            this.lblQuick.TabIndex = 0;
            this.lblQuick.Text = "Quick Filter :";
            // 
            // btnToday
            // 
            this.btnToday.BackColor = System.Drawing.Color.White;
            this.btnToday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnToday.Location = new System.Drawing.Point(120, 74);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(110, 30);
            this.btnToday.TabIndex = 0;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = false;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnLast7Days
            // 
            this.btnLast7Days.BackColor = System.Drawing.Color.White;
            this.btnLast7Days.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast7Days.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast7Days.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast7Days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnLast7Days.Location = new System.Drawing.Point(240, 74);
            this.btnLast7Days.Name = "btnLast7Days";
            this.btnLast7Days.Size = new System.Drawing.Size(110, 30);
            this.btnLast7Days.TabIndex = 0;
            this.btnLast7Days.Text = "Last 7 Days";
            this.btnLast7Days.UseVisualStyleBackColor = false;
            this.btnLast7Days.Click += new System.EventHandler(this.btnLast7Days_Click);
            // 
            // btnLast30Days
            // 
            this.btnLast30Days.BackColor = System.Drawing.Color.White;
            this.btnLast30Days.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast30Days.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast30Days.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast30Days.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnLast30Days.Location = new System.Drawing.Point(360, 74);
            this.btnLast30Days.Name = "btnLast30Days";
            this.btnLast30Days.Size = new System.Drawing.Size(110, 30);
            this.btnLast30Days.TabIndex = 0;
            this.btnLast30Days.Text = "Last 30 Days";
            this.btnLast30Days.UseVisualStyleBackColor = false;
            this.btnLast30Days.Click += new System.EventHandler(this.btnLast30Days_Click);
            // 
            // lblSessionNote
            // 
            this.lblSessionNote.BackColor = System.Drawing.Color.Transparent;
            this.lblSessionNote.Font = new System.Drawing.Font("Cambria", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionNote.ForeColor = System.Drawing.Color.DimGray;
            this.lblSessionNote.Location = new System.Drawing.Point(490, 72);
            this.lblSessionNote.Name = "lblSessionNote";
            this.lblSessionNote.Size = new System.Drawing.Size(440, 36);
            this.lblSessionNote.TabIndex = 0;
            this.lblSessionNote.Text = "\"No log out recorded\" means the program was closed without clicking Log Out.";
            // 
            // lblNoRecord
            // 
            this.lblNoRecord.BackColor = System.Drawing.Color.White;
            this.lblNoRecord.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoRecord.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoRecord.Location = new System.Drawing.Point(260, 290);
            this.lblNoRecord.Name = "lblNoRecord";
            this.lblNoRecord.Size = new System.Drawing.Size(450, 40);
            this.lblNoRecord.TabIndex = 0;
            this.lblNoRecord.Text = "No login records found.";
            this.lblNoRecord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoRecord.Visible = false;
            // 
            // lblListInfo
            // 
            this.lblListInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblListInfo.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblListInfo.Location = new System.Drawing.Point(12, 460);
            this.lblListInfo.Name = "lblListInfo";
            this.lblListInfo.Size = new System.Drawing.Size(660, 48);
            this.lblListInfo.TabIndex = 0;
            this.lblListInfo.Text = "";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnRefresh.Location = new System.Drawing.Point(780, 465);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(85, 42);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // LoginHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 519);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblListInfo);
            this.Controls.Add(this.lblNoRecord);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtgLoginRecords);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginHistory";
            this.ShowInTaskbar = false;
            this.CancelButton = this.btnClose;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SCC Records Management System - Login History";
            this.Load += new System.EventHandler(this.LoginHistory_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLoginRecords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblListInfo;
        private System.Windows.Forms.Label lblNoRecord;
        private System.Windows.Forms.Label lblSessionNote;
        private System.Windows.Forms.Button btnLast30Days;
        private System.Windows.Forms.Button btnLast7Days;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Label lblQuick;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.ComboBox cmbRoleFilter;
        private System.Windows.Forms.Label lblRole;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.Label lblSearchUsername;
        private System.Windows.Forms.Label lblDash;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DataGridView dtgLoginRecords;
        private System.Windows.Forms.Button btnClose;
    }
}