using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // The pages that can be highlighted in the menu.
    public enum SidebarPage
    {
        None,
        Dashboard,
        Students,
        Departments,
        FaceRecognition,
        Attendance
    }

    // The left menu used by every page. It is a UserControl so we write it only once.
    public partial class SidebarMenu : UserControl
    {
        private SidebarPage currentPage = SidebarPage.None;

        public SidebarMenu()
        {
            InitializeComponent();
            lblUser.Text = "Signed in as " + Session.Username;
        }

        // The button of this page is shown in a different color.
        [DefaultValue(SidebarPage.None)]
        public SidebarPage CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                HighlightButton(btnDashboard, SidebarPage.Dashboard);
                HighlightButton(btnStudents, SidebarPage.Students);
                HighlightButton(btnDepartments, SidebarPage.Departments);
                HighlightButton(btnFaceRecognition, SidebarPage.FaceRecognition);
                HighlightButton(btnAttendance, SidebarPage.Attendance);
            }
        }

        private void HighlightButton(Button button, SidebarPage page)
        {
            button.BackColor = (currentPage == page) ? Color.FromArgb(73, 118, 159) : Color.Transparent;
        }

        // Each button opens its page, unless we are already on that page.
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            if (!(FindForm() is Dashboard))
            {
                NavigationHelper.OpenPage(FindForm(), new Dashboard());
            }
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            if (!(FindForm() is StudentRecords))
            {
                NavigationHelper.OpenPage(FindForm(), new StudentRecords());
            }
        }

        private void btnDepartments_Click(object sender, EventArgs e)
        {
            if (!(FindForm() is Departments))
            {
                NavigationHelper.OpenPage(FindForm(), new Departments());
            }
        }

        private void btnFaceRecognition_Click(object sender, EventArgs e)
        {
            if (!(FindForm() is FaceRecognition))
            {
                NavigationHelper.OpenPage(FindForm(), new FaceRecognition());
            }
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            if (!(FindForm() is AttendanceMonitoring))
            {
                NavigationHelper.OpenPage(FindForm(), new AttendanceMonitoring());
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you want to log out?", "Log Out",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                NavigationHelper.Logout(FindForm());
            }
        }
    }
}
