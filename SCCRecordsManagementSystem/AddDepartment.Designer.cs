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
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRequiredNote = new System.Windows.Forms.Label();
            this.btnAddDept = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtDeptCode = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDeptHead = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeptName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAcademicTitle = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbAcademicLevel = new System.Windows.Forms.ComboBox();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddDept
            // 
            this.btnAddDept.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDept.Location = new System.Drawing.Point(649, 399);
            this.btnAddDept.Name = "btnAddDept";
            this.btnAddDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDept.Size = new System.Drawing.Size(122, 35);
            this.btnAddDept.TabIndex = 2;
            this.btnAddDept.Text = "Save";
            this.btnAddDept.UseVisualStyleBackColor = true;
            this.btnAddDept.Click += new System.EventHandler(this.btnAddDept_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(531, 399);
            this.btnReset.Name = "btnReset";
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Size = new System.Drawing.Size(112, 35);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.lblFormTitle);
            this.panel5.Location = new System.Drawing.Point(-1, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(784, 41);
            this.panel5.TabIndex = 34;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblFormTitle.Location = new System.Drawing.Point(23, 9);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(151, 21);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Add Department";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtDeptCode);
            this.panel2.Controls.Add(this.txtDescription);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtDeptHead);
            this.panel2.Controls.Add(this.txtEmail);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.txtDeptName);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(12, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(759, 218);
            this.panel2.TabIndex = 0;
            // 
            // txtDeptCode
            // 
            this.txtDeptCode.Location = new System.Drawing.Point(413, 67);
            this.txtDeptCode.Name = "txtDeptCode";
            this.txtDeptCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDeptCode.MaxLength = 20;
            this.txtDeptCode.Size = new System.Drawing.Size(321, 23);
            this.txtDeptCode.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(17, 179);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Size = new System.Drawing.Size(710, 23);
            this.txtDescription.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 15);
            this.label7.TabIndex = 48;
            this.label7.Text = "Description (Optional) :";
            // 
            // txtDeptHead
            // 
            this.txtDeptHead.Location = new System.Drawing.Point(14, 126);
            this.txtDeptHead.Name = "txtDeptHead";
            this.txtDeptHead.MaxLength = 100;
            this.txtDeptHead.Size = new System.Drawing.Size(347, 23);
            this.txtDeptHead.TabIndex = 2;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(413, 126);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Size = new System.Drawing.Size(321, 23);
            this.txtEmail.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(759, 41);
            this.panel3.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "Department Information";
            // 
            // txtDeptName
            // 
            this.txtDeptName.Location = new System.Drawing.Point(17, 67);
            this.txtDeptName.Name = "txtDeptName";
            this.txtDeptName.MaxLength = 100;
            this.txtDeptName.Size = new System.Drawing.Size(344, 23);
            this.txtDeptName.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(410, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 15);
            this.label6.TabIndex = 35;
            this.label6.Text = "Department Code * :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 15);
            this.label5.TabIndex = 34;
            this.label5.Text = "Contact Email (Optional)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 15);
            this.label4.TabIndex = 33;
            this.label4.Text = "Department Head / Dean * :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 15);
            this.label3.TabIndex = 32;
            this.label3.Text = "Department Name * :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbAcademicLevel);
            this.panel1.Location = new System.Drawing.Point(12, 289);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(759, 104);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Azure;
            this.panel4.Controls.Add(this.lblAcademicTitle);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(759, 44);
            this.panel4.TabIndex = 51;
            // 
            // lblAcademicTitle
            // 
            this.lblAcademicTitle.AutoSize = true;
            this.lblAcademicTitle.Location = new System.Drawing.Point(11, 13);
            this.lblAcademicTitle.Name = "lblAcademicTitle";
            this.lblAcademicTitle.Size = new System.Drawing.Size(118, 15);
            this.lblAcademicTitle.TabIndex = 31;
            this.lblAcademicTitle.Text = "Academic Information";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 15);
            this.label8.TabIndex = 50;
            this.label8.Text = "Academic Level * :";
            // 
            // cmbAcademicLevel
            // 
            this.cmbAcademicLevel.FormattingEnabled = true;
            this.cmbAcademicLevel.Items.AddRange(new object[] {
            "Undergraduate",
            "Graduate",
            "Postgraduate"});
            this.cmbAcademicLevel.Location = new System.Drawing.Point(14, 70);
            this.cmbAcademicLevel.Name = "cmbAcademicLevel";
            this.cmbAcademicLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAcademicLevel.Size = new System.Drawing.Size(347, 23);
            this.cmbAcademicLevel.TabIndex = 0;
            // 
            // lblRequiredNote
            // 
            this.lblRequiredNote.BackColor = System.Drawing.Color.Transparent;
            this.lblRequiredNote.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequiredNote.ForeColor = System.Drawing.Color.DimGray;
            this.lblRequiredNote.Location = new System.Drawing.Point(12, 408);
            this.lblRequiredNote.Name = "lblRequiredNote";
            this.lblRequiredNote.Size = new System.Drawing.Size(390, 22);
            this.lblRequiredNote.TabIndex = 0;
            this.lblRequiredNote.Text = "Fields marked with * are required.";
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(413, 399);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 446);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblRequiredNote);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAddDept);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "AddDepartment";
            this.CancelButton = this.btnCancel;
            this.AcceptButton = this.btnAddDept;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Department";
            this.Load += new System.EventHandler(this.AddDepartment_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRequiredNote;

        private System.Windows.Forms.Button btnAddDept;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeptName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDeptHead;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbAcademicLevel;
        private System.Windows.Forms.TextBox txtDeptCode;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblAcademicTitle;
    }
}