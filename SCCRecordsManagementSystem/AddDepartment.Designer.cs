namespace SCCRecordsManagementSystem
{
    partial class AddDepartment
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlInformation = new System.Windows.Forms.Panel();
            this.pnlInformationTitle = new System.Windows.Forms.Panel();
            this.lblInformationTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblHead = new System.Windows.Forms.Label();
            this.txtHead = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.pnlAcademic = new System.Windows.Forms.Panel();
            this.pnlAcademicTitle = new System.Windows.Forms.Panel();
            this.lblAcademicTitle = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlInformation.SuspendLayout();
            this.pnlInformationTitle.SuspendLayout();
            this.pnlAcademic.SuspendLayout();
            this.pnlAcademicTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(783, 41);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblTitle.Location = new System.Drawing.Point(23, 9);
            this.lblTitle.Text = "Add Department";
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            // 
            // pnlInformation
            // 
            this.pnlInformation.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlInformation.Location = new System.Drawing.Point(12, 56);
            this.pnlInformation.Size = new System.Drawing.Size(759, 218);
            this.pnlInformation.Controls.Add(this.pnlInformationTitle);
            this.pnlInformation.Controls.Add(this.lblName);
            this.pnlInformation.Controls.Add(this.txtName);
            this.pnlInformation.Controls.Add(this.lblCode);
            this.pnlInformation.Controls.Add(this.txtCode);
            this.pnlInformation.Controls.Add(this.lblHead);
            this.pnlInformation.Controls.Add(this.txtHead);
            this.pnlInformation.Controls.Add(this.lblEmail);
            this.pnlInformation.Controls.Add(this.txtEmail);
            this.pnlInformation.Controls.Add(this.lblDescription);
            this.pnlInformation.Controls.Add(this.txtDescription);
            this.pnlInformation.Name = "pnlInformation";
            this.pnlInformation.TabIndex = 1;
            // 
            // pnlInformationTitle
            // 
            this.pnlInformationTitle.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.pnlInformationTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlInformationTitle.Size = new System.Drawing.Size(759, 41);
            this.pnlInformationTitle.Controls.Add(this.lblInformationTitle);
            this.pnlInformationTitle.Name = "pnlInformationTitle";
            this.pnlInformationTitle.TabIndex = 0;
            // 
            // lblInformationTitle
            // 
            this.lblInformationTitle.AutoSize = true;
            this.lblInformationTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblInformationTitle.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformationTitle.ForeColor = System.Drawing.Color.Black;
            this.lblInformationTitle.Location = new System.Drawing.Point(11, 12);
            this.lblInformationTitle.Text = "Department Information";
            this.lblInformationTitle.Name = "lblInformationTitle";
            this.lblInformationTitle.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(17, 50);
            this.lblName.Text = "Department Name : *";
            this.lblName.Name = "lblName";
            this.lblName.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(17, 68);
            this.txtName.Size = new System.Drawing.Size(344, 24);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.TabIndex = 2;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.BackColor = System.Drawing.Color.Transparent;
            this.lblCode.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.ForeColor = System.Drawing.Color.Black;
            this.lblCode.Location = new System.Drawing.Point(413, 50);
            this.lblCode.Text = "Department Code : *";
            this.lblCode.Name = "lblCode";
            this.lblCode.TabIndex = 3;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(413, 68);
            this.txtCode.Size = new System.Drawing.Size(321, 24);
            this.txtCode.MaxLength = 20;
            this.txtCode.Name = "txtCode";
            this.txtCode.TabIndex = 4;
            // 
            // lblHead
            // 
            this.lblHead.AutoSize = true;
            this.lblHead.BackColor = System.Drawing.Color.Transparent;
            this.lblHead.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHead.ForeColor = System.Drawing.Color.Black;
            this.lblHead.Location = new System.Drawing.Point(17, 110);
            this.lblHead.Text = "Department Head / Dean :";
            this.lblHead.Name = "lblHead";
            this.lblHead.TabIndex = 5;
            // 
            // txtHead
            // 
            this.txtHead.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHead.Location = new System.Drawing.Point(17, 128);
            this.txtHead.Size = new System.Drawing.Size(344, 24);
            this.txtHead.MaxLength = 100;
            this.txtHead.Name = "txtHead";
            this.txtHead.TabIndex = 6;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblEmail.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.Black;
            this.lblEmail.Location = new System.Drawing.Point(413, 110);
            this.lblEmail.Text = "Contact Email (Optional) :";
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(413, 128);
            this.txtEmail.Size = new System.Drawing.Size(321, 24);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.TabIndex = 8;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point(17, 162);
            this.lblDescription.Text = "Description :";
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.TabIndex = 9;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(17, 180);
            this.txtDescription.Size = new System.Drawing.Size(717, 24);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.TabIndex = 10;
            // 
            // pnlAcademic
            // 
            this.pnlAcademic.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlAcademic.Location = new System.Drawing.Point(12, 289);
            this.pnlAcademic.Size = new System.Drawing.Size(759, 100);
            this.pnlAcademic.Controls.Add(this.pnlAcademicTitle);
            this.pnlAcademic.Controls.Add(this.lblLevel);
            this.pnlAcademic.Controls.Add(this.cmbLevel);
            this.pnlAcademic.Name = "pnlAcademic";
            this.pnlAcademic.TabIndex = 2;
            // 
            // pnlAcademicTitle
            // 
            this.pnlAcademicTitle.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.pnlAcademicTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlAcademicTitle.Size = new System.Drawing.Size(759, 41);
            this.pnlAcademicTitle.Controls.Add(this.lblAcademicTitle);
            this.pnlAcademicTitle.Name = "pnlAcademicTitle";
            this.pnlAcademicTitle.TabIndex = 0;
            // 
            // lblAcademicTitle
            // 
            this.lblAcademicTitle.AutoSize = true;
            this.lblAcademicTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAcademicTitle.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAcademicTitle.ForeColor = System.Drawing.Color.Black;
            this.lblAcademicTitle.Location = new System.Drawing.Point(11, 12);
            this.lblAcademicTitle.Text = "Academic Information";
            this.lblAcademicTitle.Name = "lblAcademicTitle";
            this.lblAcademicTitle.TabIndex = 0;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblLevel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.ForeColor = System.Drawing.Color.Black;
            this.lblLevel.Location = new System.Drawing.Point(17, 52);
            this.lblLevel.Text = "Academic Level : *";
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.TabIndex = 1;
            // 
            // cmbLevel
            // 
            this.cmbLevel.Items.AddRange(new object[] {
            "Undergraduate",
            "Graduate",
            "Senior High School"});
            this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevel.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(17, 69);
            this.cmbLevel.Size = new System.Drawing.Size(344, 25);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(413, 399);
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(189, 216, 233);
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(531, 399);
            this.btnReset.Size = new System.Drawing.Size(112, 35);
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.Name = "btnReset";
            this.btnReset.TabIndex = 4;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(73, 118, 159);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(649, 399);
            this.btnSave.Size = new System.Drawing.Size(122, 35);
            this.btnSave.Text = "SAVE DEPARTMENT";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Name = "btnSave";
            this.btnSave.TabIndex = 5;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddDepartment
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(783, 446);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Department";
            this.AcceptButton = this.btnSave;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlInformation);
            this.Controls.Add(this.pnlAcademic);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Load += new System.EventHandler(this.AddDepartment_Load);
            this.Name = "AddDepartment";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInformation.ResumeLayout(false);
            this.pnlInformation.PerformLayout();
            this.pnlInformationTitle.ResumeLayout(false);
            this.pnlInformationTitle.PerformLayout();
            this.pnlAcademic.ResumeLayout(false);
            this.pnlAcademic.PerformLayout();
            this.pnlAcademicTitle.ResumeLayout(false);
            this.pnlAcademicTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlInformation;
        private System.Windows.Forms.Panel pnlInformationTitle;
        private System.Windows.Forms.Label lblInformationTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblHead;
        private System.Windows.Forms.TextBox txtHead;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Panel pnlAcademic;
        private System.Windows.Forms.Panel pnlAcademicTitle;
        private System.Windows.Forms.Label lblAcademicTitle;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
    }
}
