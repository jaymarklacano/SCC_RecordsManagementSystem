using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnStudentRecords_Click(object sender, EventArgs e)
        {
            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        private void btnOfficer_Click(object sender, EventArgs e)
        {
            OfficerRecords officerrRecords = new OfficerRecords();
            officerrRecords.Show();
            this.Close();
        }

        private void btnFaceRecognition_Click(object sender, EventArgs e)
        {
            FaceRecognition records = new FaceRecognition();
            records.Show();
            this.Close();
        }

        private void btnEventManagement_Click(object sender, EventArgs e)
        {
            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnAttendanceMonitoring_Click(object sender, EventArgs e)
        {
            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnReportAnalysis_Click(object sender, EventArgs e)
        {
            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        private void btnObligation_Click(object sender, EventArgs e)
        {
            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        private void btnRulesPermission_Click(object sender, EventArgs e)
        {
            RulesPermission rulesPermission = new RulesPermission();
            rulesPermission.Show();
            this.Close();
        }
    }
}
