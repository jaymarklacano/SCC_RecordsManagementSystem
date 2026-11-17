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
                int activeStudents, presentToday, absentToday;
                AttendanceService.GetSummary(DateTime.Today, out activeStudents, out presentToday, out absentToday);
                DataTable todayRecords = AttendanceService.GetTodayRecords();

                lblTotalStudents.Text = StudentService.CountAll().ToString();
                lblTotalNote.Text = activeStudents + " active students";
                lblPresentToday.Text = presentToday.ToString();
                lblPresentNote.Text = "recorded by face recognition";
                lblAbsentToday.Text = absentToday.ToString();
                lblAbsentNote.Text = "not recorded yet today";

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
