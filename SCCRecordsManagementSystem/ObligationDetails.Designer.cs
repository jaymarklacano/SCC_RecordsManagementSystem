namespace SCCRecordsManagementSystem
{
    partial class ObligationDetails
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
            this.lblNoAdjustment = new System.Windows.Forms.Label();
            this.lblNoAbsence = new System.Windows.Forms.Label();
            this.btnCloseDetails = new System.Windows.Forms.Button();
            this.lblAdminOnly = new System.Windows.Forms.Label();
            this.btnDeleteAdjustment = new System.Windows.Forms.Button();
            this.dtgAdjustments = new System.Windows.Forms.DataGridView();
            this.lblAdjustmentsTitle = new System.Windows.Forms.Label();
            this.dtgAbsences = new System.Windows.Forms.DataGridView();
            this.lblAbsencesTitle = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblAdjustmentPoints = new System.Windows.Forms.Label();
            this.lblAbsencePoints = new System.Windows.Forms.Label();
            this.lblAbsentCount = new System.Windows.Forms.Label();
            this.lblStudentInfo = new System.Windows.Forms.Label();
            this.lblStudentName = new System.Windows.Forms.Label();
            this.pnlStudentInfo = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAbsences)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdjustments)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(780, 44);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(20, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Obligation Details";
            // 
            // pnlStudentInfo
            // 
            this.pnlStudentInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.pnlStudentInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStudentInfo.Controls.Add(this.lblBalance);
            this.pnlStudentInfo.Controls.Add(this.lblAdjustmentPoints);
            this.pnlStudentInfo.Controls.Add(this.lblAbsencePoints);
            this.pnlStudentInfo.Controls.Add(this.lblAbsentCount);
            this.pnlStudentInfo.Controls.Add(this.lblStudentInfo);
            this.pnlStudentInfo.Controls.Add(this.lblStudentName);
            this.pnlStudentInfo.Location = new System.Drawing.Point(14, 54);
            this.pnlStudentInfo.Name = "pnlStudentInfo";
            this.pnlStudentInfo.Size = new System.Drawing.Size(752, 104);
            this.pnlStudentInfo.TabIndex = 1;
            // 
            // lblStudentName
            // 
            this.lblStudentName.BackColor = System.Drawing.Color.Transparent;
            this.lblStudentName.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblStudentName.Location = new System.Drawing.Point(14, 8);
            this.lblStudentName.Name = "lblStudentName";
            this.lblStudentName.Size = new System.Drawing.Size(720, 26);
            this.lblStudentName.TabIndex = 0;
            this.lblStudentName.Text = "Student Name";
            this.lblStudentName.AutoEllipsis = true;
            // 
            // lblStudentInfo
            // 
            this.lblStudentInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblStudentInfo.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentInfo.Location = new System.Drawing.Point(14, 38);
            this.lblStudentInfo.Name = "lblStudentInfo";
            this.lblStudentInfo.Size = new System.Drawing.Size(720, 20);
            this.lblStudentInfo.TabIndex = 0;
            this.lblStudentInfo.Text = "ID Number :   |   Department :   |   Year Level :   |   Status :";
            this.lblStudentInfo.AutoEllipsis = true;
            // 
            // lblAbsentCount
            // 
            this.lblAbsentCount.BackColor = System.Drawing.Color.Transparent;
            this.lblAbsentCount.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsentCount.ForeColor = System.Drawing.Color.Firebrick;
            this.lblAbsentCount.Location = new System.Drawing.Point(14, 68);
            this.lblAbsentCount.Name = "lblAbsentCount";
            this.lblAbsentCount.Size = new System.Drawing.Size(170, 24);
            this.lblAbsentCount.TabIndex = 0;
            this.lblAbsentCount.Text = "Absences : 0";
            // 
            // lblAbsencePoints
            // 
            this.lblAbsencePoints.BackColor = System.Drawing.Color.Transparent;
            this.lblAbsencePoints.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsencePoints.Location = new System.Drawing.Point(194, 68);
            this.lblAbsencePoints.Name = "lblAbsencePoints";
            this.lblAbsencePoints.Size = new System.Drawing.Size(180, 24);
            this.lblAbsencePoints.TabIndex = 0;
            this.lblAbsencePoints.Text = "Absence Points : 0";
            // 
            // lblAdjustmentPoints
            // 
            this.lblAdjustmentPoints.BackColor = System.Drawing.Color.Transparent;
            this.lblAdjustmentPoints.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustmentPoints.Location = new System.Drawing.Point(384, 68);
            this.lblAdjustmentPoints.Name = "lblAdjustmentPoints";
            this.lblAdjustmentPoints.Size = new System.Drawing.Size(180, 24);
            this.lblAdjustmentPoints.TabIndex = 0;
            this.lblAdjustmentPoints.Text = "Adjustments : 0";
            // 
            // lblBalance
            // 
            this.lblBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblBalance.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.Firebrick;
            this.lblBalance.Location = new System.Drawing.Point(574, 68);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(170, 24);
            this.lblBalance.TabIndex = 0;
            this.lblBalance.Text = "Balance : 0";
            // 
            // lblAbsencesTitle
            // 
            this.lblAbsencesTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAbsencesTitle.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbsencesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblAbsencesTitle.Location = new System.Drawing.Point(14, 168);
            this.lblAbsencesTitle.Name = "lblAbsencesTitle";
            this.lblAbsencesTitle.Size = new System.Drawing.Size(500, 20);
            this.lblAbsencesTitle.TabIndex = 0;
            this.lblAbsencesTitle.Text = "Absences (10 points for every absence)";
            // 
            // dtgAbsences
            // 
            this.dtgAbsences.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAbsences.Location = new System.Drawing.Point(14, 190);
            this.dtgAbsences.Name = "dtgAbsences";
            this.dtgAbsences.ReadOnly = true;
            this.dtgAbsences.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAbsences.RowHeadersVisible = false;
            this.dtgAbsences.MultiSelect = false;
            this.dtgAbsences.BackgroundColor = System.Drawing.Color.White;
            this.dtgAbsences.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgAbsences.AllowUserToResizeRows = false;
            this.dtgAbsences.AllowUserToDeleteRows = false;
            this.dtgAbsences.AllowUserToAddRows = false;
            this.dtgAbsences.RowHeadersWidth = 51;
            this.dtgAbsences.Size = new System.Drawing.Size(752, 150);
            this.dtgAbsences.TabIndex = 2;
            // 
            // lblAdjustmentsTitle
            // 
            this.lblAdjustmentsTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAdjustmentsTitle.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustmentsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.lblAdjustmentsTitle.Location = new System.Drawing.Point(14, 350);
            this.lblAdjustmentsTitle.Name = "lblAdjustmentsTitle";
            this.lblAdjustmentsTitle.Size = new System.Drawing.Size(500, 20);
            this.lblAdjustmentsTitle.TabIndex = 0;
            this.lblAdjustmentsTitle.Text = "Points Added / Deducted / Cleared";
            // 
            // dtgAdjustments
            // 
            this.dtgAdjustments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAdjustments.Location = new System.Drawing.Point(14, 372);
            this.dtgAdjustments.Name = "dtgAdjustments";
            this.dtgAdjustments.ReadOnly = true;
            this.dtgAdjustments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAdjustments.RowHeadersVisible = false;
            this.dtgAdjustments.MultiSelect = false;
            this.dtgAdjustments.BackgroundColor = System.Drawing.Color.White;
            this.dtgAdjustments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgAdjustments.AllowUserToResizeRows = false;
            this.dtgAdjustments.AllowUserToDeleteRows = false;
            this.dtgAdjustments.AllowUserToAddRows = false;
            this.dtgAdjustments.RowHeadersWidth = 51;
            this.dtgAdjustments.Size = new System.Drawing.Size(752, 150);
            this.dtgAdjustments.TabIndex = 3;
            // 
            // btnDeleteAdjustment
            // 
            this.btnDeleteAdjustment.BackColor = System.Drawing.Color.White;
            this.btnDeleteAdjustment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteAdjustment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAdjustment.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteAdjustment.ForeColor = System.Drawing.Color.Firebrick;
            this.btnDeleteAdjustment.Location = new System.Drawing.Point(14, 536);
            this.btnDeleteAdjustment.Name = "btnDeleteAdjustment";
            this.btnDeleteAdjustment.Size = new System.Drawing.Size(250, 44);
            this.btnDeleteAdjustment.TabIndex = 4;
            this.btnDeleteAdjustment.Text = "Delete Selected Adjustment";
            this.btnDeleteAdjustment.UseVisualStyleBackColor = false;
            this.btnDeleteAdjustment.Click += new System.EventHandler(this.btnDeleteAdjustment_Click);
            // 
            // lblAdminOnly
            // 
            this.lblAdminOnly.BackColor = System.Drawing.Color.Transparent;
            this.lblAdminOnly.Font = new System.Drawing.Font("Cambria", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminOnly.ForeColor = System.Drawing.Color.DimGray;
            this.lblAdminOnly.Location = new System.Drawing.Point(274, 550);
            this.lblAdminOnly.Name = "lblAdminOnly";
            this.lblAdminOnly.Size = new System.Drawing.Size(330, 20);
            this.lblAdminOnly.TabIndex = 0;
            this.lblAdminOnly.Text = "Only the Admin can delete an adjustment.";
            // 
            // btnCloseDetails
            // 
            this.btnCloseDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(216)))), ((int)(((byte)(233)))));
            this.btnCloseDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseDetails.Font = new System.Drawing.Font("Cambria", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.btnCloseDetails.Location = new System.Drawing.Point(626, 536);
            this.btnCloseDetails.Name = "btnCloseDetails";
            this.btnCloseDetails.Size = new System.Drawing.Size(140, 44);
            this.btnCloseDetails.TabIndex = 5;
            this.btnCloseDetails.Text = "Close";
            this.btnCloseDetails.UseVisualStyleBackColor = false;
            this.btnCloseDetails.Click += new System.EventHandler(this.btnCloseDetails_Click);
            // 
            // lblNoAbsence
            // 
            this.lblNoAbsence.BackColor = System.Drawing.Color.White;
            this.lblNoAbsence.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoAbsence.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoAbsence.Location = new System.Drawing.Point(190, 250);
            this.lblNoAbsence.Name = "lblNoAbsence";
            this.lblNoAbsence.Size = new System.Drawing.Size(400, 30);
            this.lblNoAbsence.TabIndex = 0;
            this.lblNoAbsence.Text = "This student has no absences.";
            this.lblNoAbsence.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoAbsence.Visible = false;
            // 
            // lblNoAdjustment
            // 
            this.lblNoAdjustment.BackColor = System.Drawing.Color.White;
            this.lblNoAdjustment.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoAdjustment.ForeColor = System.Drawing.Color.DimGray;
            this.lblNoAdjustment.Location = new System.Drawing.Point(190, 432);
            this.lblNoAdjustment.Name = "lblNoAdjustment";
            this.lblNoAdjustment.Size = new System.Drawing.Size(400, 30);
            this.lblNoAdjustment.TabIndex = 0;
            this.lblNoAdjustment.Text = "No points were added or deducted yet.";
            this.lblNoAdjustment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoAdjustment.Visible = false;
            // 
            // ObligationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 600);
            this.Controls.Add(this.lblNoAdjustment);
            this.Controls.Add(this.lblNoAbsence);
            this.Controls.Add(this.btnCloseDetails);
            this.Controls.Add(this.lblAdminOnly);
            this.Controls.Add(this.btnDeleteAdjustment);
            this.Controls.Add(this.dtgAdjustments);
            this.Controls.Add(this.lblAdjustmentsTitle);
            this.Controls.Add(this.dtgAbsences);
            this.Controls.Add(this.lblAbsencesTitle);
            this.Controls.Add(this.pnlStudentInfo);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObligationDetails";
            this.CancelButton = this.btnCloseDetails;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SCC Records Management System - Obligation Details";
            this.Load += new System.EventHandler(this.ObligationDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAbsences)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdjustments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblNoAdjustment;
        private System.Windows.Forms.Label lblNoAbsence;
        private System.Windows.Forms.Button btnCloseDetails;
        private System.Windows.Forms.Label lblAdminOnly;
        private System.Windows.Forms.Button btnDeleteAdjustment;
        private System.Windows.Forms.DataGridView dtgAdjustments;
        private System.Windows.Forms.Label lblAdjustmentsTitle;
        private System.Windows.Forms.DataGridView dtgAbsences;
        private System.Windows.Forms.Label lblAbsencesTitle;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblAdjustmentPoints;
        private System.Windows.Forms.Label lblAbsencePoints;
        private System.Windows.Forms.Label lblAbsentCount;
        private System.Windows.Forms.Label lblStudentInfo;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.Panel pnlStudentInfo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;

    }
}
