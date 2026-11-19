using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public partial class AttendanceMonitoring : Form
    {
        private ComboBox cmbEventFilter;
        private ComboBox cmbSessionFilter;
        private ComboBox cmbDepartmentFilter;
        private ComboBox cmbYearFilter;
        private ComboBox cmbStatusFilter;
        private DateTimePicker dtpSessionDate;
        private TextBox txtSearchFilter;
        private DataGridView eventGrid;
        private Label lblEventSummary;
        private Label lblEventCount;
        private bool loadingEventFilters;

        public AttendanceMonitoring()
        {
            InitializeComponent();
            NavigationHelper.ExitWhenClosed(this);
            BuildEventAttendanceView();
        }

        private void AttendanceMonitoring_Load(object sender, EventArgs e)
        {
            try
            {
                loadingEventFilters = true;
                PageControls.BindChoices(cmbEventFilter, EventService.GetChoices(),
                    "event_id", "display_name", "All Events");
                PageControls.BindChoices(cmbDepartmentFilter, DepartmentService.GetAll(),
                    "department_id", "department_code", "All Departments");
                LoadSessionChoices();
                loadingEventFilters = false;
                LoadEventRecords();
            }
            catch (Exception ex)
            {
                loadingEventFilters = false;
                lblEventSummary.Text = Database.GetFriendlyMessage(ex);
            }
        }

        private void BuildEventAttendanceView()
        {
            foreach (Control control in Controls)
            {
                if (control != sidebarMenu && control != pnlHeader)
                {
                    control.Visible = false;
                }
            }

            Controls.Add(PageControls.Label("Event:", 340, 66, 50));
            cmbEventFilter = PageControls.Combo(390, 64, 230);
            cmbEventFilter.SelectedIndexChanged += EventFilterChanged;
            Controls.Add(cmbEventFilter);

            Controls.Add(PageControls.Label("Session:", 635, 66, 60));
            cmbSessionFilter = PageControls.Combo(695, 64, 255);
            cmbSessionFilter.SelectedIndexChanged += EventFilterChanged;
            Controls.Add(cmbSessionFilter);

            Controls.Add(PageControls.Label("Date:", 965, 66, 40));
            dtpSessionDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(1005, 64),
                ShowCheckBox = true,
                Checked = false,
                Size = new Size(145, 25)
            };
            dtpSessionDate.ValueChanged += EventFilterChanged;
            Controls.Add(dtpSessionDate);

            Controls.Add(PageControls.Label("Department:", 340, 105, 85));
            cmbDepartmentFilter = PageControls.Combo(425, 103, 155);
            cmbDepartmentFilter.SelectedIndexChanged += EventFilterChanged;
            Controls.Add(cmbDepartmentFilter);
            Controls.Add(PageControls.Label("Year:", 590, 105, 40));
            cmbYearFilter = PageControls.Combo(630, 103, 110);
            cmbYearFilter.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year" });
            cmbYearFilter.SelectedIndex = 0;
            cmbYearFilter.SelectedIndexChanged += EventFilterChanged;
            Controls.Add(cmbYearFilter);
            Controls.Add(PageControls.Label("Status:", 750, 105, 50));
            cmbStatusFilter = PageControls.Combo(800, 103, 110);
            cmbStatusFilter.Items.AddRange(new object[] { "All", "Present", "Absent" });
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndexChanged += EventFilterChanged;
            Controls.Add(cmbStatusFilter);
            Controls.Add(PageControls.Label("Search:", 920, 105, 55));
            txtSearchFilter = PageControls.TextBox(975, 103, 310);
            txtSearchFilter.TextChanged += EventFilterChanged;
            Controls.Add(txtSearchFilter);

            lblEventSummary = PageControls.Label("", 340, 142, 945, 38);
            lblEventSummary.BackColor = Color.FromArgb(189, 216, 233);
            Controls.Add(lblEventSummary);
            eventGrid = PageControls.Grid(340, 188, 945, 385);
            Controls.Add(eventGrid);
            lblEventCount = PageControls.Label("", 340, 578, 945, 20);
            lblEventCount.Font = new Font("Cambria", 9F, FontStyle.Italic);
            lblEventCount.ForeColor = Color.DimGray;
            Controls.Add(lblEventCount);
        }

        private void LoadSessionChoices()
        {
            int eventId = PageControls.SelectedId(cmbEventFilter);
            DataTable sessions = EventService.GetSessions(eventId);
            PageControls.BindChoices(cmbSessionFilter, sessions, "session_id", "display_name", "All Sessions");
        }

        private void EventFilterChanged(object sender, EventArgs e)
        {
            if (loadingEventFilters || !IsHandleCreated)
            {
                return;
            }
            if (sender == cmbEventFilter)
            {
                try
                {
                    loadingEventFilters = true;
                    LoadSessionChoices();
                }
                catch (Exception ex)
                {
                    lblEventSummary.Text = Database.GetFriendlyMessage(ex);
                }
                finally
                {
                    loadingEventFilters = false;
                }
            }
            LoadEventRecords();
        }

        private void LoadEventRecords()
        {
            try
            {
                DateTime? date = dtpSessionDate.Checked ? (DateTime?)dtpSessionDate.Value.Date : null;
                string year = cmbYearFilter.SelectedIndex <= 0 ? "" : cmbYearFilter.Text;
                DataTable table = AttendanceService.GetRecords(
                    PageControls.SelectedId(cmbEventFilter),
                    PageControls.SelectedId(cmbSessionFilter),
                    date,
                    PageControls.SelectedId(cmbDepartmentFilter),
                    year,
                    txtSearchFilter.Text,
                    cmbStatusFilter.Text);
                eventGrid.DataSource = table;
                eventGrid.ClearSelection();
                if (eventGrid.Columns.Contains("session_id"))
                {
                    eventGrid.Columns["session_id"].Visible = false;
                }

                int present = 0;
                int absent = 0;
                foreach (DataRow row in table.Rows)
                {
                    if (row["status"].ToString() == "Present") present++;
                    else absent++;
                }
                lblEventSummary.Text = "Present: " + present + "      Absent: " + absent +
                    "      Absences appear after each session ends; incomplete sessions show present scans only.";
                lblEventCount.Text = "Showing " + table.Rows.Count + " event attendance row(s)";
            }
            catch (Exception ex)
            {
                eventGrid.DataSource = null;
                lblEventSummary.Text = Database.GetFriendlyMessage(ex);
                lblEventCount.Text = "";
            }
        }

        private void sidebarMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
