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
        Attendance,
        Events,
        Officers,
        Obligations,
        Reports
    }

    // The left menu used by every page. It is a UserControl so we write it only once.
    public partial class SidebarMenu : UserControl
    {
        private SidebarPage currentPage = SidebarPage.None;
        private readonly System.Collections.Generic.Dictionary<SidebarPage, Button> extraButtons =
            new System.Collections.Generic.Dictionary<SidebarPage, Button>();

        public SidebarMenu()
        {
            InitializeComponent();
            lblUser.Text = "Signed in as " + Session.Username + " (" + Session.Role + ")";
            AddExtraButton(SidebarPage.Events, "Event Management", 343, delegate
            {
                if (!(FindForm() is EventManagement))
                {
                    NavigationHelper.OpenPage(FindForm(), new EventManagement());
                }
            });
            AddExtraButton(SidebarPage.Officers, "Officer Records", 387, delegate
            {
                if (!(FindForm() is OfficerRecords))
                {
                    NavigationHelper.OpenPage(FindForm(), new OfficerRecords());
                }
            });
            AddExtraButton(SidebarPage.Obligations, "Student Obligations", 431, delegate
            {
                if (!(FindForm() is Obligations))
                {
                    NavigationHelper.OpenPage(FindForm(), new Obligations());
                }
            });
            AddExtraButton(SidebarPage.Reports, "Reports & Analysis", 475, delegate
            {
                if (!(FindForm() is ReportsAndAnalysis))
                {
                    NavigationHelper.OpenPage(FindForm(), new ReportsAndAnalysis());
                }
            });
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
                foreach (System.Collections.Generic.KeyValuePair<SidebarPage, Button> item in extraButtons)
                {
                    HighlightButton(item.Value, item.Key);
                }
            }
        }

        private void HighlightButton(Button button, SidebarPage page)
        {
            button.BackColor = (currentPage == page) ? Color.FromArgb(73, 118, 159) : Color.Transparent;
        }

        private void AddExtraButton(SidebarPage page, string text, int top, EventHandler click)
        {
            Button button = new Button();
            button.BackColor = Color.Transparent;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 80, 120);
            button.Font = new Font("Cambria", 11F, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.Location = new Point(22, top);
            button.Size = new Size(277, 38);
            button.Text = "   " + text;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Cursor = Cursors.Hand;
            button.Visible = page != SidebarPage.Officers || Session.IsAdmin;
            button.Click += click;
            Controls.Add(button);
            extraButtons.Add(page, button);
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

        private void SidebarMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
