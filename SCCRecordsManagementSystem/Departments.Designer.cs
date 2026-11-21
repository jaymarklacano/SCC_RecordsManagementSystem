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
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgDepartment = new System.Windows.Forms.DataGridView();
            this.btnAddDepartment = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeptLogout = new System.Windows.Forms.Button();
            this.btnDeptDashboard = new System.Windows.Forms.Button();
            this.btnDeptRules = new System.Windows.Forms.Button();
            this.btnDeptObligation = new System.Windows.Forms.Button();
            this.btnDeptReport = new System.Windows.Forms.Button();
            this.btnDeptAttendance = new System.Windows.Forms.Button();
            this.btnDeptEvent = new System.Windows.Forms.Button();
            this.btnDeptFaceRicognation = new System.Windows.Forms.Button();
            this.btnDeptOfficer = new System.Windows.Forms.Button();
            this.btnDeptStudentRecords = new System.Windows.Forms.Button();
            this.btnDepartMent = new System.Windows.Forms.Button();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDepartment)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(324, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(983, 48);
            this.panel5.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.GhostWhite;
            this.label1.Location = new System.Drawing.Point(111, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Departments";
            // 
            // dtgDepartment
            // 
            this.dtgDepartment.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dtgDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDepartment.Location = new System.Drawing.Point(359, 114);
            this.dtgDepartment.Name = "dtgDepartment";
            this.dtgDepartment.RowHeadersWidth = 51;
            this.dtgDepartment.RowTemplate.Height = 24;
            this.dtgDepartment.Size = new System.Drawing.Size(645, 437);
            this.dtgDepartment.TabIndex = 24;
            this.dtgDepartment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgDepartment_CellContentClick);
            // 
            // btnAddDepartment
            // 
            this.btnAddDepartment.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDepartment.Location = new System.Drawing.Point(786, 66);
            this.btnAddDepartment.Name = "btnAddDepartment";
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
            this.panel1.Controls.Add(this.btnDeptFaceRicognation);
            this.panel1.Controls.Add(this.btnDeptOfficer);
            this.panel1.Controls.Add(this.btnDeptStudentRecords);
            this.panel1.Controls.Add(this.btnDepartMent);
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
            this.btnDeptEvent.Size = new System.Drawing.Size(277, 36);
            this.btnDeptEvent.TabIndex = 6;
            this.btnDeptEvent.Text = "   Event Management";
            this.btnDeptEvent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptEvent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptEvent.UseVisualStyleBackColor = false;
            // 
            // btnDeptFaceRicognation
            // 
            this.btnDeptFaceRicognation.BackColor = System.Drawing.Color.Transparent;
            this.btnDeptFaceRicognation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeptFaceRicognation.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeptFaceRicognation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDeptFaceRicognation.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_facial_recognition_24;
            this.btnDeptFaceRicognation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptFaceRicognation.Location = new System.Drawing.Point(22, 275);
            this.btnDeptFaceRicognation.Name = "btnDeptFaceRicognation";
            this.btnDeptFaceRicognation.Size = new System.Drawing.Size(277, 36);
            this.btnDeptFaceRicognation.TabIndex = 5;
            this.btnDeptFaceRicognation.Text = "   Face Recognition";
            this.btnDeptFaceRicognation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeptFaceRicognation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptFaceRicognation.UseVisualStyleBackColor = false;
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
            this.btnDeptStudentRecords.Size = new System.Drawing.Size(277, 36);
            this.btnDeptStudentRecords.TabIndex = 3;
            this.btnDeptStudentRecords.Text = "   Student Records";
            this.btnDeptStudentRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeptStudentRecords.UseVisualStyleBackColor = false;
            // 
            // btnDepartMent
            // 
            this.btnDepartMent.BackColor = System.Drawing.Color.Transparent;
            this.btnDepartMent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDepartMent.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDepartMent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDepartMent.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_building_24__1_;
            this.btnDepartMent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDepartMent.Location = new System.Drawing.Point(22, 161);
            this.btnDepartMent.Name = "btnDepartMent";
            this.btnDepartMent.Size = new System.Drawing.Size(277, 36);
            this.btnDepartMent.TabIndex = 2;
            this.btnDepartMent.Text = "   Departments";
            this.btnDepartMent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDepartMent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDepartMent.UseVisualStyleBackColor = false;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(439, 66);
            this.txtDepartment.Multiline = true;
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(235, 36);
            this.txtDepartment.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(355, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 23);
            this.label2.TabIndex = 28;
            this.label2.Text = "Search:";
            // 
            // Departments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._903375047_2169860083631994_8617979005398290656_n;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAddDepartment);
            this.Controls.Add(this.dtgDepartment);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Departments";
            this.Text = "Departments";
            this.Load += new System.EventHandler(this.Departments_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDepartment)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgDepartment;
        private System.Windows.Forms.Button btnAddDepartment;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeptLogout;
        private System.Windows.Forms.Button btnDeptDashboard;
        private System.Windows.Forms.Button btnDeptRules;
        private System.Windows.Forms.Button btnDeptObligation;
        private System.Windows.Forms.Button btnDeptReport;
        private System.Windows.Forms.Button btnDeptAttendance;
        private System.Windows.Forms.Button btnDeptEvent;
        private System.Windows.Forms.Button btnDeptFaceRicognation;
        private System.Windows.Forms.Button btnDeptOfficer;
        private System.Windows.Forms.Button btnDeptStudentRecords;
        private System.Windows.Forms.Button btnDepartMent;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.Label label2;
    }
}