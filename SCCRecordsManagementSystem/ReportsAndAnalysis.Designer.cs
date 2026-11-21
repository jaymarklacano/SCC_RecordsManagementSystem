namespace SCCRecordsManagementSystem
{
    partial class ReportsAndAnalysis
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOrganizer = new System.Windows.Forms.Label();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblVenue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.dtpEventTime = new System.Windows.Forms.DateTimePicker();
            this.cmpReportType = new System.Windows.Forms.ComboBox();
            this.cmbEventSelect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chtAttendanceDepartment = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtgRecord = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtAttendanceDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecord)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(321, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(985, 48);
            this.panel5.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.GhostWhite;
            this.label1.Location = new System.Drawing.Point(107, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reports and Analysis";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(319, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Event Date :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(571, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Report Type :";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblOrganizer);
            this.panel1.Controls.Add(this.lblSchedule);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblVenue);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnGenerateReport);
            this.panel1.Controls.Add(this.dtpEventTime);
            this.panel1.Controls.Add(this.cmpReportType);
            this.panel1.Controls.Add(this.cmbEventSelect);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(330, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 160);
            this.panel1.TabIndex = 12;
            // 
            // lblOrganizer
            // 
            this.lblOrganizer.AutoSize = true;
            this.lblOrganizer.Location = new System.Drawing.Point(648, 121);
            this.lblOrganizer.Name = "lblOrganizer";
            this.lblOrganizer.Size = new System.Drawing.Size(51, 16);
            this.lblOrganizer.TabIndex = 22;
            this.lblOrganizer.Text = "label10";
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.Location = new System.Drawing.Point(395, 121);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(44, 16);
            this.lblSchedule.TabIndex = 21;
            this.lblSchedule.Text = "label9";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(571, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 16);
            this.label8.TabIndex = 20;
            this.label8.Text = "Organizer :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(319, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 16);
            this.label7.TabIndex = 19;
            this.label7.Text = "Schedule :";
            // 
            // lblVenue
            // 
            this.lblVenue.AutoSize = true;
            this.lblVenue.Location = new System.Drawing.Point(102, 121);
            this.lblVenue.Name = "lblVenue";
            this.lblVenue.Size = new System.Drawing.Size(44, 16);
            this.lblVenue.TabIndex = 18;
            this.lblVenue.Text = "label6";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Venue :";
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(802, 24);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(126, 58);
            this.btnGenerateReport.TabIndex = 16;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            // 
            // dtpEventTime
            // 
            this.dtpEventTime.Location = new System.Drawing.Point(322, 51);
            this.dtpEventTime.Name = "dtpEventTime";
            this.dtpEventTime.Size = new System.Drawing.Size(200, 22);
            this.dtpEventTime.TabIndex = 15;
            // 
            // cmpReportType
            // 
            this.cmpReportType.FormattingEnabled = true;
            this.cmpReportType.Location = new System.Drawing.Point(574, 49);
            this.cmpReportType.Name = "cmpReportType";
            this.cmpReportType.Size = new System.Drawing.Size(121, 24);
            this.cmpReportType.TabIndex = 14;
            // 
            // cmbEventSelect
            // 
            this.cmbEventSelect.FormattingEnabled = true;
            this.cmbEventSelect.Location = new System.Drawing.Point(47, 51);
            this.cmbEventSelect.Name = "cmbEventSelect";
            this.cmbEventSelect.Size = new System.Drawing.Size(225, 24);
            this.cmbEventSelect.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Select / Search Event Name :";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.checkBox2);
            this.panel3.Controls.Add(this.checkBox1);
            this.panel3.Controls.Add(this.chtAttendanceDepartment);
            this.panel3.Location = new System.Drawing.Point(331, 288);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(962, 172);
            this.panel3.TabIndex = 28;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(843, 13);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(71, 20);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Absent";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(749, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 20);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Present";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chtAttendanceDepartment
            // 
            chartArea1.Name = "ChartArea1";
            this.chtAttendanceDepartment.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chtAttendanceDepartment.Legends.Add(legend1);
            this.chtAttendanceDepartment.Location = new System.Drawing.Point(9, 39);
            this.chtAttendanceDepartment.Name = "chtAttendanceDepartment";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chtAttendanceDepartment.Series.Add(series1);
            this.chtAttendanceDepartment.Size = new System.Drawing.Size(919, 124);
            this.chtAttendanceDepartment.TabIndex = 0;
            this.chtAttendanceDepartment.Text = "chart1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(336, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(261, 16);
            this.label11.TabIndex = 23;
            this.label11.Text = "Event Search and Attendance Filter Criteria";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(337, 269);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(172, 16);
            this.label12.TabIndex = 29;
            this.label12.Text = "Attendance Per Department";
            // 
            // dtgRecord
            // 
            this.dtgRecord.BackgroundColor = System.Drawing.Color.LightSlateGray;
            this.dtgRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRecord.Location = new System.Drawing.Point(330, 466);
            this.dtgRecord.Name = "dtgRecord";
            this.dtgRecord.RowHeadersWidth = 51;
            this.dtgRecord.RowTemplate.Height = 24;
            this.dtgRecord.Size = new System.Drawing.Size(962, 141);
            this.dtgRecord.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(57)))));
            this.panel2.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._904439575_1911191946735306_1039497458727327980_n1;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.button12);
            this.panel2.Controls.Add(this.button13);
            this.panel2.Controls.Add(this.button14);
            this.panel2.Controls.Add(this.button15);
            this.panel2.Controls.Add(this.button16);
            this.panel2.Controls.Add(this.button17);
            this.panel2.Controls.Add(this.button18);
            this.panel2.Controls.Add(this.button19);
            this.panel2.Controls.Add(this.button20);
            this.panel2.Controls.Add(this.button21);
            this.panel2.Controls.Add(this.button22);
            this.panel2.Location = new System.Drawing.Point(-6, -3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 624);
            this.panel2.TabIndex = 27;
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.Transparent;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(75)))), ((int)(((byte)(80)))));
            this.button12.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_log_out_24__2_;
            this.button12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button12.Location = new System.Drawing.Point(22, 560);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(277, 38);
            this.button12.TabIndex = 11;
            this.button12.Text = "   LogOut";
            this.button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button12.UseVisualStyleBackColor = false;
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.Transparent;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button13.Image = global::SCCRecordsManagementSystem.Properties.Resources.house__1_;
            this.button13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button13.Location = new System.Drawing.Point(22, 123);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(277, 36);
            this.button13.TabIndex = 1;
            this.button13.Text = "   Dashboard";
            this.button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button13.UseVisualStyleBackColor = false;
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.Transparent;
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button14.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button14.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button14.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_rules_book_24;
            this.button14.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button14.Location = new System.Drawing.Point(22, 465);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(277, 36);
            this.button14.TabIndex = 10;
            this.button14.Text = "   Rules and Permission";
            this.button14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button14.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button14.UseVisualStyleBackColor = false;
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.Transparent;
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button15.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button15.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button15.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_user_24;
            this.button15.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button15.Location = new System.Drawing.Point(22, 427);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(277, 36);
            this.button15.TabIndex = 9;
            this.button15.Text = "Obligations";
            this.button15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button15.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button15.UseVisualStyleBackColor = false;
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.Transparent;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button16.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button16.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button16.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_analysis_24;
            this.button16.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button16.Location = new System.Drawing.Point(22, 389);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(277, 36);
            this.button16.TabIndex = 8;
            this.button16.Text = "   Reports and Analysis";
            this.button16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button16.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button16.UseVisualStyleBackColor = false;
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.Transparent;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button17.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button17.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button17.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_pass_fail_24;
            this.button17.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button17.Location = new System.Drawing.Point(22, 351);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(277, 36);
            this.button17.TabIndex = 7;
            this.button17.Text = "   Attendance Monitoring";
            this.button17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button17.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button17.UseVisualStyleBackColor = false;
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.Transparent;
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button18.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button18.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button18.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_event_accepted_tentatively_24;
            this.button18.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button18.Location = new System.Drawing.Point(22, 313);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(277, 36);
            this.button18.TabIndex = 6;
            this.button18.Text = "   Event Management";
            this.button18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button18.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button18.UseVisualStyleBackColor = false;
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.Transparent;
            this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button19.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button19.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button19.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_facial_recognition_24;
            this.button19.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button19.Location = new System.Drawing.Point(22, 275);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(277, 36);
            this.button19.TabIndex = 5;
            this.button19.Text = "   Face Recognition";
            this.button19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button19.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button19.UseVisualStyleBackColor = false;
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.Color.Transparent;
            this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button20.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button20.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button20.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_management_24;
            this.button20.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button20.Location = new System.Drawing.Point(22, 237);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(277, 36);
            this.button20.TabIndex = 4;
            this.button20.Text = "   Officer Records";
            this.button20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button20.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button20.UseVisualStyleBackColor = false;
            // 
            // button21
            // 
            this.button21.BackColor = System.Drawing.Color.Transparent;
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button21.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button21.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button21.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_what_i_do_241;
            this.button21.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button21.Location = new System.Drawing.Point(22, 199);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(277, 36);
            this.button21.TabIndex = 3;
            this.button21.Text = "   Student Records";
            this.button21.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button21.UseVisualStyleBackColor = false;
            // 
            // button22
            // 
            this.button22.BackColor = System.Drawing.Color.Transparent;
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button22.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button22.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button22.Image = global::SCCRecordsManagementSystem.Properties.Resources.icons8_building_24__1_;
            this.button22.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button22.Location = new System.Drawing.Point(22, 161);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(277, 36);
            this.button22.TabIndex = 2;
            this.button22.Text = "   Departments";
            this.button22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button22.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button22.UseVisualStyleBackColor = false;
            // 
            // ReportsAndAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SCCRecordsManagementSystem.Properties.Resources._891752403_1143943308365188_7410099486200290872_n;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1305, 619);
            this.Controls.Add(this.dtgRecord);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReportsAndAnalysis";
            this.Text = "ReportsAndAnalysis";
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtAttendanceDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRecord)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpEventTime;
        private System.Windows.Forms.ComboBox cmpReportType;
        private System.Windows.Forms.ComboBox cmbEventSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblVenue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOrganizer;
        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtAttendanceDepartment;
        private System.Windows.Forms.DataGridView dtgRecord;
    }
}