using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SCCRecordsManagementSystem
{
    // Home page: totals for today, a pie chart and the list of students who were recorded today.
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            NavigationHelper.ExitWhenClosed(this);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Welcome, " + Session.Username + "!";
            SetupChart();
            ShowClock();
            LoadDashboard();
        }

        // Runs every second to update the date and time.
        private void timerClock_Tick(object sender, EventArgs e)
        {
            ShowClock();
        }

        // Runs every 15 seconds so new attendance appears without clicking anything.
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void btnStartAttendance_Click(object sender, EventArgs e)
        {
            NavigationHelper.OpenPage(this, new FaceRecognition());
        }

        private void ShowClock()
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy   h:mm:ss tt", CultureInfo.InvariantCulture);
        }

        private void LoadDashboard()
        {
            try
            {
                int activeStudents = Database.GetCount("SELECT COUNT(*) FROM students WHERE status = 'Active'");
                int presentToday = 0;
                int absentToday = 0;
                DataTable todayRecords = new DataTable();
                DataRow activeSession = EventService.GetCurrentSession();

                if (activeSession != null)
                {
                    int sessionId = Convert.ToInt32(activeSession["session_id"]);
                    DataTable summary = AttendanceService.GetSessionSummary(sessionId);
                    if (summary.Rows.Count > 0)
                    {
                        presentToday = summary.Rows[0]["present"] == DBNull.Value ? 0 : Convert.ToInt32(summary.Rows[0]["present"]);
                        absentToday = summary.Rows[0]["absent"] == DBNull.Value ? 0 : Convert.ToInt32(summary.Rows[0]["absent"]);
                    }
                    todayRecords = AttendanceService.GetSessionPresentRecords(sessionId);
                    lblTodayTitle.Text = activeSession["display_name"].ToString();
                    lblPresentNote.Text = "logged in this session";
                    lblAbsentNote.Text = "after session end";
                }
                else
                {
                    lblTodayTitle.Text = "No active event session";
                    lblPresentNote.Text = "no session is open now";
                    lblAbsentNote.Text = "shown after a session ends";
                }

                lblTotalStudents.Text = activeStudents.ToString();
                lblTotalNote.Text = "active students | " + ReportService.GetOutstandingPoints() + " points due";
                lblPresentToday.Text = presentToday.ToString();
                lblAbsentToday.Text = absentToday.ToString();

                dgvToday.DataSource = todayRecords;
                lblEmptyToday.Visible = (todayRecords.Rows.Count == 0);
                dgvToday.ClearSelection();

                ShowChart(presentToday, absentToday);

                lblStatus.ForeColor = Color.DimGray;
                lblStatus.Text = "Last updated: " + DateTime.Now.ToString("h:mm:ss tt", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                // No message box here, because this method also runs by itself every 15 seconds.
                lblStatus.ForeColor = Color.Firebrick;
                lblStatus.Text = Database.GetFriendlyMessage(ex);
            }
        }

        // ---------- Pie chart ----------

        private void SetupChart()
        {
            chartAttendance.ChartAreas.Clear();
            chartAttendance.Legends.Clear();
            chartAttendance.Series.Clear();

            ChartArea area = new ChartArea("MainArea");
            area.BackColor = Color.LightSteelBlue;
            chartAttendance.ChartAreas.Add(area);

            Legend legend = new Legend("MainLegend");
            legend.Docking = Docking.Bottom;
            legend.BackColor = Color.LightSteelBlue;
            legend.Font = new Font("Cambria", 10F, FontStyle.Bold);
            chartAttendance.Legends.Add(legend);

            Series series = new Series("Attendance");
            series.ChartType = SeriesChartType.Pie;
            series.ChartArea = "MainArea";
            series.Legend = "MainLegend";
            series.IsValueShownAsLabel = true;
            series.Font = new Font("Cambria", 12F, FontStyle.Bold);
            series.LabelForeColor = Color.White;
            chartAttendance.Series.Add(series);
        }

        private void ShowChart(int presentToday, int absentToday)
        {
            Series series = chartAttendance.Series["Attendance"];
            series.Points.Clear();

            bool hasData = (presentToday + absentToday) > 0;
            chartAttendance.Visible = hasData;
            lblChartEmpty.Visible = !hasData;

            if (presentToday > 0)
            {
                int index = series.Points.AddXY("Present", presentToday);
                series.Points[index].Color = Color.SeaGreen;
            }

            if (absentToday > 0)
            {
                int index = series.Points.AddXY("Absent", absentToday);
                series.Points[index].Color = Color.Firebrick;
            }
        }
    }
}
