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
            this.sidebarMenu = new SidebarMenu();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.windowButtons = new WindowButtons();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.dgvDepartments = new System.Windows.Forms.DataGridView();
            this.colDepartmentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStudents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarMenu
            // 
            this.sidebarMenu.Location = new System.Drawing.Point(0, 0);
            this.sidebarMenu.Size = new System.Drawing.Size(320, 619);
            this.sidebarMenu.CurrentPage = SCCRecordsManagementSystem.SidebarPage.Departments;
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
            this.lblPageTitle.Text = "Departments";
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
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.Black;
            this.lblSearch.Location = new System.Drawing.Point(340, 70);
            this.lblSearch.Text = "Search:";
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(400, 66);
            this.txtSearch.Size = new System.Drawing.Size(260, 24);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(860, 62);
            this.btnAdd.Size = new System.Drawing.Size(200, 34);
            this.btnAdd.Text = "ADD DEPARTMENT";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Location = new System.Drawing.Point(1070, 62);
            this.btnEdit.Size = new System.Drawing.Size(100, 34);
            this.btnEdit.Text = "EDIT";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(139, 0, 0);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(1180, 62);
            this.btnDelete.Size = new System.Drawing.Size(105, 34);
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblEmpty
            // 
            this.lblEmpty.AutoSize = false;
            this.lblEmpty.BackColor = System.Drawing.Color.White;
            this.lblEmpty.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblEmpty.Location = new System.Drawing.Point(341, 145);
            this.lblEmpty.Text = "No departments found.";
            this.lblEmpty.Size = new System.Drawing.Size(943, 380);
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.TabIndex = 7;
            // 
            // dgvDepartments
            // 
            System.Windows.Forms.DataGridViewCellStyle dgvDepartmentsHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvDepartmentsCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvDepartmentsAltStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dgvDepartmentsHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvDepartmentsHeaderStyle.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            dgvDepartmentsHeaderStyle.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDepartmentsHeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvDepartmentsHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            dgvDepartmentsHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvDepartmentsCellStyle.BackColor = System.Drawing.Color.White;
            dgvDepartmentsCellStyle.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDepartmentsCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvDepartmentsCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            dgvDepartmentsCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgvDepartmentsAltStyle.BackColor = System.Drawing.Color.FromArgb(238, 244, 249);
            dgvDepartmentsAltStyle.SelectionBackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            dgvDepartmentsAltStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDepartments.ColumnHeadersDefaultCellStyle = dgvDepartmentsHeaderStyle;
            this.dgvDepartments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDepartments.DefaultCellStyle = dgvDepartmentsCellStyle;
            this.dgvDepartments.AlternatingRowsDefaultCellStyle = dgvDepartmentsAltStyle;
            this.dgvDepartments.RowTemplate.Height = 28;
            this.dgvDepartments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDepartmentId,
            this.colCode,
            this.colName,
            this.colHead,
            this.colLevel,
            this.colEmail,
            this.colStudents});
            this.dgvDepartments.Location = new System.Drawing.Point(340, 110);
            this.dgvDepartments.Size = new System.Drawing.Size(945, 458);
            this.dgvDepartments.AllowUserToAddRows = false;
            this.dgvDepartments.AllowUserToDeleteRows = false;
            this.dgvDepartments.AllowUserToResizeRows = false;
            this.dgvDepartments.AutoGenerateColumns = false;
            this.dgvDepartments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepartments.BackgroundColor = System.Drawing.Color.White;
            this.dgvDepartments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvDepartments.ColumnHeadersHeight = 34;
            this.dgvDepartments.EnableHeadersVisualStyles = false;
            this.dgvDepartments.MultiSelect = false;
            this.dgvDepartments.ReadOnly = true;
            this.dgvDepartments.RowHeadersVisible = false;
            this.dgvDepartments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartments.Name = "dgvDepartments";
            this.dgvDepartments.TabIndex = 8;
            this.dgvDepartments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartments_CellDoubleClick);
            // 
            // colDepartmentId
            // 
            this.colDepartmentId.DataPropertyName = "department_id";
            this.colDepartmentId.FillWeight = 5F;
            this.colDepartmentId.HeaderText = "ID";
            this.colDepartmentId.Name = "colDepartmentId";
            this.colDepartmentId.ReadOnly = true;
            this.colDepartmentId.Visible = false;
            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "department_code";
            this.colCode.FillWeight = 11F;
            this.colCode.HeaderText = "Code";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "department_name";
            this.colName.FillWeight = 33F;
            this.colName.HeaderText = "Department Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colHead
            // 
            this.colHead.DataPropertyName = "department_head";
            this.colHead.FillWeight = 20F;
            this.colHead.HeaderText = "Department Head / Dean";
            this.colHead.Name = "colHead";
            this.colHead.ReadOnly = true;
            // 
            // colLevel
            // 
            this.colLevel.DataPropertyName = "academic_level";
            this.colLevel.FillWeight = 14F;
            this.colLevel.HeaderText = "Academic Level";
            this.colLevel.Name = "colLevel";
            this.colLevel.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "contact_email";
            this.colEmail.FillWeight = 20F;
            this.colEmail.HeaderText = "Contact Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // colStudents
            // 
            this.colStudents.DataPropertyName = "student_count";
            this.colStudents.FillWeight = 10F;
            this.colStudents.HeaderText = "Students";
            this.colStudents.Name = "colStudents";
            this.colStudents.ReadOnly = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = false;
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.DimGray;
            this.lblCount.Location = new System.Drawing.Point(340, 576);
            this.lblCount.Text = "";
            this.lblCount.Size = new System.Drawing.Size(945, 20);
            this.lblCount.Name = "lblCount";
            this.lblCount.TabIndex = 9;
            // 
            // Departments
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Departments";
            this.Controls.Add(this.sidebarMenu);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblEmpty);
            this.Controls.Add(this.dgvDepartments);
            this.Controls.Add(this.lblCount);
            this.Load += new System.EventHandler(this.Departments_Load);
            this.Name = "Departments";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SidebarMenu sidebarMenu;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private WindowButtons windowButtons;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartmentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudents;
        private System.Windows.Forms.DataGridView dgvDepartments;
        private System.Windows.Forms.Label lblCount;
    }
}
