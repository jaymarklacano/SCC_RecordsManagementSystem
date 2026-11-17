namespace SCCRecordsManagementSystem
{
    partial class StudentRecords
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
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbFace = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.pnlPhoto = new System.Windows.Forms.Panel();
            this.picPhoto = new System.Windows.Forms.PictureBox();
            this.lblPhotoName = new System.Windows.Forms.Label();
            this.lblPhotoInfo = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.lblNumberCaption = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblNameCaption = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCourseCaption = new System.Windows.Forms.Label();
            this.lblCourse = new System.Windows.Forms.Label();
            this.lblYearCaption = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblGenderCaption = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblEmailCaption = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblContactCaption = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblStatusCaption = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.colStudentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCourse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlPhoto.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarMenu
            // 
            this.sidebarMenu.Location = new System.Drawing.Point(0, 0);
            this.sidebarMenu.Size = new System.Drawing.Size(320, 619);
            this.sidebarMenu.CurrentPage = SCCRecordsManagementSystem.SidebarPage.Students;
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
            this.lblPageTitle.Text = "Student Records";
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
            this.txtSearch.TextChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.Items.AddRange(new object[] {
            });
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(670, 66);
            this.cmbDepartment.Size = new System.Drawing.Size(165, 25);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.TabIndex = 4;
            this.cmbDepartment.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // cmbYear
            // 
            this.cmbYear.Items.AddRange(new object[] {
            "All Years",
            "1st Year",
            "2nd Year",
            "3rd Year",
            "4th Year"});
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(845, 66);
            this.cmbYear.Size = new System.Drawing.Size(115, 25);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.TabIndex = 5;
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // cmbFace
            // 
            this.cmbFace.Items.AddRange(new object[] {
            "All Face Status",
            "Registered",
            "Not Registered"});
            this.cmbFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFace.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFace.FormattingEnabled = true;
            this.cmbFace.Location = new System.Drawing.Point(970, 66);
            this.cmbFace.Size = new System.Drawing.Size(160, 25);
            this.cmbFace.Name = "cmbFace";
            this.cmbFace.TabIndex = 6;
            this.cmbFace.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(1140, 63);
            this.btnClear.Size = new System.Drawing.Size(145, 30);
            this.btnClear.Text = "CLEAR FILTERS";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.Name = "btnClear";
            this.btnClear.TabIndex = 7;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pnlPhoto
            // 
            this.pnlPhoto.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlPhoto.Location = new System.Drawing.Point(340, 106);
            this.pnlPhoto.Size = new System.Drawing.Size(250, 494);
            this.pnlPhoto.Controls.Add(this.picPhoto);
            this.pnlPhoto.Controls.Add(this.lblPhotoName);
            this.pnlPhoto.Controls.Add(this.lblPhotoInfo);
            this.pnlPhoto.Controls.Add(this.btnAdd);
            this.pnlPhoto.Controls.Add(this.btnEdit);
            this.pnlPhoto.Controls.Add(this.btnDelete);
            this.pnlPhoto.Name = "pnlPhoto";
            this.pnlPhoto.TabIndex = 8;
            // 
            // picPhoto
            // 
            this.picPhoto.BackColor = System.Drawing.Color.White;
            this.picPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPhoto.Location = new System.Drawing.Point(25, 16);
            this.picPhoto.Size = new System.Drawing.Size(200, 200);
            this.picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPhoto.Name = "picPhoto";
            this.picPhoto.TabIndex = 0;
            // 
            // lblPhotoName
            // 
            this.lblPhotoName.AutoSize = false;
            this.lblPhotoName.BackColor = System.Drawing.Color.Transparent;
            this.lblPhotoName.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhotoName.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblPhotoName.Location = new System.Drawing.Point(10, 224);
            this.lblPhotoName.Text = "Select a student";
            this.lblPhotoName.Size = new System.Drawing.Size(230, 44);
            this.lblPhotoName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPhotoName.Name = "lblPhotoName";
            this.lblPhotoName.TabIndex = 1;
            // 
            // lblPhotoInfo
            // 
            this.lblPhotoInfo.AutoSize = false;
            this.lblPhotoInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPhotoInfo.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhotoInfo.ForeColor = System.Drawing.Color.Black;
            this.lblPhotoInfo.Location = new System.Drawing.Point(10, 270);
            this.lblPhotoInfo.Text = "";
            this.lblPhotoInfo.Size = new System.Drawing.Size(230, 22);
            this.lblPhotoInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPhotoInfo.Name = "lblPhotoInfo";
            this.lblPhotoInfo.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(25, 312);
            this.btnAdd.Size = new System.Drawing.Size(200, 40);
            this.btnAdd.Text = "ADD STUDENT";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Location = new System.Drawing.Point(25, 362);
            this.btnEdit.Size = new System.Drawing.Size(200, 40);
            this.btnEdit.Text = "EDIT STUDENT";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(139, 0, 0);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(25, 412);
            this.btnDelete.Size = new System.Drawing.Size(200, 40);
            this.btnDelete.Text = "DELETE STUDENT";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.pnlDetails.Location = new System.Drawing.Point(610, 106);
            this.pnlDetails.Size = new System.Drawing.Size(675, 162);
            this.pnlDetails.Controls.Add(this.lblDetailsTitle);
            this.pnlDetails.Controls.Add(this.lblNumberCaption);
            this.pnlDetails.Controls.Add(this.lblNumber);
            this.pnlDetails.Controls.Add(this.lblNameCaption);
            this.pnlDetails.Controls.Add(this.lblName);
            this.pnlDetails.Controls.Add(this.lblCourseCaption);
            this.pnlDetails.Controls.Add(this.lblCourse);
            this.pnlDetails.Controls.Add(this.lblYearCaption);
            this.pnlDetails.Controls.Add(this.lblYear);
            this.pnlDetails.Controls.Add(this.lblGenderCaption);
            this.pnlDetails.Controls.Add(this.lblGender);
            this.pnlDetails.Controls.Add(this.lblEmailCaption);
            this.pnlDetails.Controls.Add(this.lblEmail);
            this.pnlDetails.Controls.Add(this.lblContactCaption);
            this.pnlDetails.Controls.Add(this.lblContact);
            this.pnlDetails.Controls.Add(this.lblStatusCaption);
            this.pnlDetails.Controls.Add(this.lblStatus);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.TabIndex = 9;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetailsTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblDetailsTitle.Location = new System.Drawing.Point(14, 8);
            this.lblDetailsTitle.Text = "Student Details";
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.TabIndex = 0;
            // 
            // lblNumberCaption
            // 
            this.lblNumberCaption.AutoSize = true;
            this.lblNumberCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNumberCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberCaption.ForeColor = System.Drawing.Color.Black;
            this.lblNumberCaption.Location = new System.Drawing.Point(14, 38);
            this.lblNumberCaption.Text = "Student ID:";
            this.lblNumberCaption.Name = "lblNumberCaption";
            this.lblNumberCaption.TabIndex = 1;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = false;
            this.lblNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblNumber.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.ForeColor = System.Drawing.Color.Black;
            this.lblNumber.Location = new System.Drawing.Point(110, 38);
            this.lblNumber.Text = "-";
            this.lblNumber.Size = new System.Drawing.Size(220, 22);
            this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNumber.AutoEllipsis = true;
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.TabIndex = 2;
            // 
            // lblNameCaption
            // 
            this.lblNameCaption.AutoSize = true;
            this.lblNameCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNameCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameCaption.ForeColor = System.Drawing.Color.Black;
            this.lblNameCaption.Location = new System.Drawing.Point(14, 68);
            this.lblNameCaption.Text = "Full Name:";
            this.lblNameCaption.Name = "lblNameCaption";
            this.lblNameCaption.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = false;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(110, 68);
            this.lblName.Text = "-";
            this.lblName.Size = new System.Drawing.Size(220, 22);
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblName.AutoEllipsis = true;
            this.lblName.Name = "lblName";
            this.lblName.TabIndex = 4;
            // 
            // lblCourseCaption
            // 
            this.lblCourseCaption.AutoSize = true;
            this.lblCourseCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblCourseCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourseCaption.ForeColor = System.Drawing.Color.Black;
            this.lblCourseCaption.Location = new System.Drawing.Point(14, 98);
            this.lblCourseCaption.Text = "Course:";
            this.lblCourseCaption.Name = "lblCourseCaption";
            this.lblCourseCaption.TabIndex = 5;
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = false;
            this.lblCourse.BackColor = System.Drawing.Color.Transparent;
            this.lblCourse.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourse.ForeColor = System.Drawing.Color.Black;
            this.lblCourse.Location = new System.Drawing.Point(110, 98);
            this.lblCourse.Text = "-";
            this.lblCourse.Size = new System.Drawing.Size(220, 22);
            this.lblCourse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCourse.AutoEllipsis = true;
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.TabIndex = 6;
            // 
            // lblYearCaption
            // 
            this.lblYearCaption.AutoSize = true;
            this.lblYearCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblYearCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearCaption.ForeColor = System.Drawing.Color.Black;
            this.lblYearCaption.Location = new System.Drawing.Point(14, 128);
            this.lblYearCaption.Text = "Year Level:";
            this.lblYearCaption.Name = "lblYearCaption";
            this.lblYearCaption.TabIndex = 7;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = false;
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.ForeColor = System.Drawing.Color.Black;
            this.lblYear.Location = new System.Drawing.Point(110, 128);
            this.lblYear.Text = "-";
            this.lblYear.Size = new System.Drawing.Size(220, 22);
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblYear.AutoEllipsis = true;
            this.lblYear.Name = "lblYear";
            this.lblYear.TabIndex = 8;
            // 
            // lblGenderCaption
            // 
            this.lblGenderCaption.AutoSize = true;
            this.lblGenderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblGenderCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblGenderCaption.Location = new System.Drawing.Point(345, 38);
            this.lblGenderCaption.Text = "Gender:";
            this.lblGenderCaption.Name = "lblGenderCaption";
            this.lblGenderCaption.TabIndex = 9;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = false;
            this.lblGender.BackColor = System.Drawing.Color.Transparent;
            this.lblGender.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGender.ForeColor = System.Drawing.Color.Black;
            this.lblGender.Location = new System.Drawing.Point(440, 38);
            this.lblGender.Text = "-";
            this.lblGender.Size = new System.Drawing.Size(228, 22);
            this.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblGender.AutoEllipsis = true;
            this.lblGender.Name = "lblGender";
            this.lblGender.TabIndex = 10;
            // 
            // lblEmailCaption
            // 
            this.lblEmailCaption.AutoSize = true;
            this.lblEmailCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblEmailCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailCaption.ForeColor = System.Drawing.Color.Black;
            this.lblEmailCaption.Location = new System.Drawing.Point(345, 68);
            this.lblEmailCaption.Text = "Email:";
            this.lblEmailCaption.Name = "lblEmailCaption";
            this.lblEmailCaption.TabIndex = 11;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = false;
            this.lblEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblEmail.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.Black;
            this.lblEmail.Location = new System.Drawing.Point(440, 68);
            this.lblEmail.Text = "-";
            this.lblEmail.Size = new System.Drawing.Size(228, 22);
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEmail.AutoEllipsis = true;
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 12;
            // 
            // lblContactCaption
            // 
            this.lblContactCaption.AutoSize = true;
            this.lblContactCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblContactCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactCaption.ForeColor = System.Drawing.Color.Black;
            this.lblContactCaption.Location = new System.Drawing.Point(345, 98);
            this.lblContactCaption.Text = "Contact No.:";
            this.lblContactCaption.Name = "lblContactCaption";
            this.lblContactCaption.TabIndex = 13;
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = false;
            this.lblContact.BackColor = System.Drawing.Color.Transparent;
            this.lblContact.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContact.ForeColor = System.Drawing.Color.Black;
            this.lblContact.Location = new System.Drawing.Point(440, 98);
            this.lblContact.Text = "-";
            this.lblContact.Size = new System.Drawing.Size(228, 22);
            this.lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblContact.AutoEllipsis = true;
            this.lblContact.Name = "lblContact";
            this.lblContact.TabIndex = 14;
            // 
            // lblStatusCaption
            // 
            this.lblStatusCaption.AutoSize = true;
            this.lblStatusCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusCaption.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusCaption.ForeColor = System.Drawing.Color.Black;
            this.lblStatusCaption.Location = new System.Drawing.Point(345, 128);
            this.lblStatusCaption.Text = "Status:";
            this.lblStatusCaption.Name = "lblStatusCaption";
            this.lblStatusCaption.TabIndex = 15;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(440, 128);
            this.lblStatus.Text = "-";
            this.lblStatus.Size = new System.Drawing.Size(228, 22);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.TabIndex = 16;
            // 
            // lblEmpty
            // 
            this.lblEmpty.AutoSize = false;
            this.lblEmpty.BackColor = System.Drawing.Color.White;
            this.lblEmpty.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpty.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblEmpty.Location = new System.Drawing.Point(611, 315);
            this.lblEmpty.Text = "No students found.";
            this.lblEmpty.Size = new System.Drawing.Size(673, 245);
            this.lblEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmpty.Visible = false;
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.TabIndex = 10;
            // 
            // dgvStudents
            // 
            System.Windows.Forms.DataGridViewCellStyle dgvStudentsHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvStudentsCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvStudentsAltStyle = new System.Windows.Forms.DataGridViewCellStyle();
            dgvStudentsHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvStudentsHeaderStyle.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            dgvStudentsHeaderStyle.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvStudentsHeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvStudentsHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            dgvStudentsHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvStudentsCellStyle.BackColor = System.Drawing.Color.White;
            dgvStudentsCellStyle.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvStudentsCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvStudentsCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            dgvStudentsCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgvStudentsAltStyle.BackColor = System.Drawing.Color.FromArgb(238, 244, 249);
            dgvStudentsAltStyle.SelectionBackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            dgvStudentsAltStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvStudents.ColumnHeadersDefaultCellStyle = dgvStudentsHeaderStyle;
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvStudents.DefaultCellStyle = dgvStudentsCellStyle;
            this.dgvStudents.AlternatingRowsDefaultCellStyle = dgvStudentsAltStyle;
            this.dgvStudents.RowTemplate.Height = 28;
            this.dgvStudents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStudentId,
            this.colNumber,
            this.colName,
            this.colCourse,
            this.colYear,
            this.colFace,
            this.colStatus});
            this.dgvStudents.Location = new System.Drawing.Point(610, 280);
            this.dgvStudents.Size = new System.Drawing.Size(675, 296);
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.AllowUserToResizeRows = false;
            this.dgvStudents.AutoGenerateColumns = false;
            this.dgvStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudents.BackgroundColor = System.Drawing.Color.White;
            this.dgvStudents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvStudents.ColumnHeadersHeight = 34;
            this.dgvStudents.EnableHeadersVisualStyles = false;
            this.dgvStudents.MultiSelect = false;
            this.dgvStudents.ReadOnly = true;
            this.dgvStudents.RowHeadersVisible = false;
            this.dgvStudents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.TabIndex = 11;
            this.dgvStudents.SelectionChanged += new System.EventHandler(this.dgvStudents_SelectionChanged);
            this.dgvStudents.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudents_CellDoubleClick);
            // 
            // colStudentId
            // 
            this.colStudentId.DataPropertyName = "student_id";
            this.colStudentId.FillWeight = 5F;
            this.colStudentId.HeaderText = "ID";
            this.colStudentId.Name = "colStudentId";
            this.colStudentId.ReadOnly = true;
            this.colStudentId.Visible = false;
            // 
            // colNumber
            // 
            this.colNumber.DataPropertyName = "student_number";
            this.colNumber.FillWeight = 17F;
            this.colNumber.HeaderText = "Student ID";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "full_name";
            this.colName.FillWeight = 34F;
            this.colName.HeaderText = "Full Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colCourse
            // 
            this.colCourse.DataPropertyName = "department_code";
            this.colCourse.FillWeight = 12F;
            this.colCourse.HeaderText = "Course";
            this.colCourse.Name = "colCourse";
            this.colCourse.ReadOnly = true;
            // 
            // colYear
            // 
            this.colYear.DataPropertyName = "year_level";
            this.colYear.FillWeight = 14F;
            this.colYear.HeaderText = "Year Level";
            this.colYear.Name = "colYear";
            this.colYear.ReadOnly = true;
            // 
            // colFace
            // 
            this.colFace.DataPropertyName = "face_status";
            this.colFace.FillWeight = 18F;
            this.colFace.HeaderText = "Face";
            this.colFace.Name = "colFace";
            this.colFace.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "status";
            this.colStatus.FillWeight = 12F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = false;
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.DimGray;
            this.lblCount.Location = new System.Drawing.Point(610, 580);
            this.lblCount.Text = "";
            this.lblCount.Size = new System.Drawing.Size(675, 20);
            this.lblCount.Name = "lblCount";
            this.lblCount.TabIndex = 12;
            // 
            // StudentRecords
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StudentRecords";
            this.Controls.Add(this.sidebarMenu);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbDepartment);
            this.Controls.Add(this.cmbYear);
            this.Controls.Add(this.cmbFace);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.pnlPhoto);
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.lblEmpty);
            this.Controls.Add(this.dgvStudents);
            this.Controls.Add(this.lblCount);
            this.Load += new System.EventHandler(this.StudentRecords_Load);
            this.Name = "StudentRecords";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlPhoto.ResumeLayout(false);
            this.pnlPhoto.PerformLayout();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
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
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.ComboBox cmbFace;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel pnlPhoto;
        private System.Windows.Forms.PictureBox picPhoto;
        private System.Windows.Forms.Label lblPhotoName;
        private System.Windows.Forms.Label lblPhotoInfo;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Label lblNumberCaption;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblNameCaption;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCourseCaption;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.Label lblYearCaption;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblGenderCaption;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblEmailCaption;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblContactCaption;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCourse;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Label lblCount;
    }
}
