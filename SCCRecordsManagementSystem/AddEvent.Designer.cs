namespace SCCRecordsManagementSystem
{
    partial class AddEvent
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpMorningEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpMorningStart = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDateOfEvent = new System.Windows.Forms.DateTimePicker();
            this.txtEventName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtVenue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnResetEvent = new System.Windows.Forms.Button();
            this.btnSaveEvent = new System.Windows.Forms.Button();
            this.dtpAfternoonEnd = new System.Windows.Forms.DateTimePicker();
            this.btnAfternoonStart = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpEveningEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpEveningStart = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(118)))), ((int)(((byte)(159)))));
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(-1, -1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(844, 44);
            this.panel5.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.GhostWhite;
            this.label1.Location = new System.Drawing.Point(23, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add Event";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.dtpEveningEnd);
            this.panel2.Controls.Add(this.dtpEveningStart);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.dtpAfternoonEnd);
            this.panel2.Controls.Add(this.btnAfternoonStart);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.dtpMorningEnd);
            this.panel2.Controls.Add(this.dtpMorningStart);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.dtpDateOfEvent);
            this.panel2.Controls.Add(this.txtEventName);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(12, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(755, 386);
            this.panel2.TabIndex = 30;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // dtpMorningEnd
            // 
            this.dtpMorningEnd.Location = new System.Drawing.Point(424, 132);
            this.dtpMorningEnd.Name = "dtpMorningEnd";
            this.dtpMorningEnd.Size = new System.Drawing.Size(299, 23);
            this.dtpMorningEnd.TabIndex = 45;
            // 
            // dtpMorningStart
            // 
            this.dtpMorningStart.Location = new System.Drawing.Point(30, 132);
            this.dtpMorningStart.Name = "dtpMorningStart";
            this.dtpMorningStart.Size = new System.Drawing.Size(315, 23);
            this.dtpMorningStart.TabIndex = 44;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Azure;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(755, 44);
            this.panel3.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "Event Information";
            // 
            // dtpDateOfEvent
            // 
            this.dtpDateOfEvent.Location = new System.Drawing.Point(424, 81);
            this.dtpDateOfEvent.Name = "dtpDateOfEvent";
            this.dtpDateOfEvent.Size = new System.Drawing.Size(299, 23);
            this.dtpDateOfEvent.TabIndex = 43;
            // 
            // txtEventName
            // 
            this.txtEventName.Location = new System.Drawing.Point(33, 81);
            this.txtEventName.Name = "txtEventName";
            this.txtEventName.Size = new System.Drawing.Size(312, 23);
            this.txtEventName.TabIndex = 42;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(421, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 15);
            this.label9.TabIndex = 41;
            this.label9.Text = "End Time : ";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Window;
            this.panel7.Controls.Add(this.txtVenue);
            this.panel7.Location = new System.Drawing.Point(33, 324);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(690, 39);
            this.panel7.TabIndex = 39;
            // 
            // txtVenue
            // 
            this.txtVenue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVenue.Location = new System.Drawing.Point(3, 12);
            this.txtVenue.Name = "txtVenue";
            this.txtVenue.Size = new System.Drawing.Size(684, 16);
            this.txtVenue.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(421, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 35;
            this.label6.Text = "Date :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 306);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 34;
            this.label5.Text = "Venue :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 33;
            this.label4.Text = "Morning:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 15);
            this.label3.TabIndex = 32;
            this.label3.Text = "Event Name :";
            // 
            // btnResetEvent
            // 
            this.btnResetEvent.Location = new System.Drawing.Point(537, 458);
            this.btnResetEvent.Name = "btnResetEvent";
            this.btnResetEvent.Size = new System.Drawing.Size(112, 37);
            this.btnResetEvent.TabIndex = 32;
            this.btnResetEvent.Text = "Reset";
            this.btnResetEvent.UseVisualStyleBackColor = true;
            // 
            // btnSaveEvent
            // 
            this.btnSaveEvent.Location = new System.Drawing.Point(655, 458);
            this.btnSaveEvent.Name = "btnSaveEvent";
            this.btnSaveEvent.Size = new System.Drawing.Size(112, 37);
            this.btnSaveEvent.TabIndex = 33;
            this.btnSaveEvent.Text = "Save Event";
            this.btnSaveEvent.UseVisualStyleBackColor = true;
            // 
            // dtpAfternoonEnd
            // 
            this.dtpAfternoonEnd.Location = new System.Drawing.Point(427, 186);
            this.dtpAfternoonEnd.Name = "dtpAfternoonEnd";
            this.dtpAfternoonEnd.Size = new System.Drawing.Size(299, 23);
            this.dtpAfternoonEnd.TabIndex = 49;
            // 
            // btnAfternoonStart
            // 
            this.btnAfternoonStart.Location = new System.Drawing.Point(33, 186);
            this.btnAfternoonStart.Name = "btnAfternoonStart";
            this.btnAfternoonStart.Size = new System.Drawing.Size(315, 23);
            this.btnAfternoonStart.TabIndex = 48;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(424, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 15);
            this.label7.TabIndex = 47;
            this.label7.Text = "End Time : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 15);
            this.label8.TabIndex = 46;
            this.label8.Text = "Afternoon:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // dtpEveningEnd
            // 
            this.dtpEveningEnd.Location = new System.Drawing.Point(427, 240);
            this.dtpEveningEnd.Name = "dtpEveningEnd";
            this.dtpEveningEnd.Size = new System.Drawing.Size(299, 23);
            this.dtpEveningEnd.TabIndex = 53;
            // 
            // dtpEveningStart
            // 
            this.dtpEveningStart.Location = new System.Drawing.Point(33, 240);
            this.dtpEveningStart.Name = "dtpEveningStart";
            this.dtpEveningStart.Size = new System.Drawing.Size(315, 23);
            this.dtpEveningStart.TabIndex = 52;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(424, 222);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 15);
            this.label10.TabIndex = 51;
            this.label10.Text = "End Time : ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 222);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 15);
            this.label11.TabIndex = 50;
            this.label11.Text = "Evening:";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(33, 274);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(122, 19);
            this.checkBox1.TabIndex = 54;
            this.checkBox1.Text = "Log out Needed";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // AddEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 509);
            this.Controls.Add(this.btnSaveEvent);
            this.Controls.Add(this.btnResetEvent);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Cambria", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "AddEvent";
            this.Text = "AddEvent";
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtVenue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpDateOfEvent;
        private System.Windows.Forms.TextBox txtEventName;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnResetEvent;
        private System.Windows.Forms.Button btnSaveEvent;
        private System.Windows.Forms.DateTimePicker dtpMorningEnd;
        private System.Windows.Forms.DateTimePicker dtpMorningStart;
        private System.Windows.Forms.DateTimePicker dtpAfternoonEnd;
        private System.Windows.Forms.DateTimePicker btnAfternoonStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpEveningEnd;
        private System.Windows.Forms.DateTimePicker dtpEveningStart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}