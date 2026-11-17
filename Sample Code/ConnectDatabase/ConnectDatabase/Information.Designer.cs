namespace ConnectDatabase
{
    partial class Information
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
            this.dtgInformation = new System.Windows.Forms.DataGridView();
            this.txtIDSearch = new System.Windows.Forms.TextBox();
            this.btnDegree = new System.Windows.Forms.Button();
            this.btnJob = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgInformation)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgInformation
            // 
            this.dtgInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgInformation.Location = new System.Drawing.Point(102, 119);
            this.dtgInformation.Name = "dtgInformation";
            this.dtgInformation.RowHeadersWidth = 51;
            this.dtgInformation.RowTemplate.Height = 24;
            this.dtgInformation.Size = new System.Drawing.Size(632, 280);
            this.dtgInformation.TabIndex = 0;
            // 
            // txtIDSearch
            // 
            this.txtIDSearch.Location = new System.Drawing.Point(102, 76);
            this.txtIDSearch.Name = "txtIDSearch";
            this.txtIDSearch.Size = new System.Drawing.Size(301, 22);
            this.txtIDSearch.TabIndex = 1;
            // 
            // btnDegree
            // 
            this.btnDegree.Location = new System.Drawing.Point(433, 76);
            this.btnDegree.Name = "btnDegree";
            this.btnDegree.Size = new System.Drawing.Size(75, 23);
            this.btnDegree.TabIndex = 2;
            this.btnDegree.Text = "DEGREE";
            this.btnDegree.UseVisualStyleBackColor = true;
            this.btnDegree.Click += new System.EventHandler(this.btnDegree_Click);
            // 
            // btnJob
            // 
            this.btnJob.Location = new System.Drawing.Point(526, 76);
            this.btnJob.Name = "btnJob";
            this.btnJob.Size = new System.Drawing.Size(75, 23);
            this.btnJob.TabIndex = 3;
            this.btnJob.Text = "JOB";
            this.btnJob.UseVisualStyleBackColor = true;
            this.btnJob.Click += new System.EventHandler(this.btnJob_Click);
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnJob);
            this.Controls.Add(this.btnDegree);
            this.Controls.Add(this.txtIDSearch);
            this.Controls.Add(this.dtgInformation);
            this.Name = "Information";
            this.Text = "Information";
            ((System.ComponentModel.ISupportInitialize)(this.dtgInformation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgInformation;
        private System.Windows.Forms.TextBox txtIDSearch;
        private System.Windows.Forms.Button btnDegree;
        private System.Windows.Forms.Button btnJob;
    }
}