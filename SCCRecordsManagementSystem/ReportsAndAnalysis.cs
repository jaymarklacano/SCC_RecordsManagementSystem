using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public class ReportsAndAnalysis : ExtendedPageForm
    {
        private readonly ComboBox cmbEvent;
        private readonly ComboBox cmbDepartment;
        private readonly DateTimePicker dtpFrom;
        private readonly DateTimePicker dtpTo;
        private readonly TabControl tabs;
        private readonly DataGridView attendanceGrid;
        private readonly DataGridView absenceGrid;
        private readonly Label lblSummary;

        public ReportsAndAnalysis() : base("Reports & Analysis", SidebarPage.Reports)
        {
            ContentPanel.Controls.Add(PageControls.Label("Event:", 18, 15, 50));
            cmbEvent = PageControls.Combo(68, 13, 220);
            ContentPanel.Controls.Add(cmbEvent);
            ContentPanel.Controls.Add(PageControls.Label("Department:", 300, 15, 85));
            cmbDepartment = PageControls.Combo(386, 13, 145);
            ContentPanel.Controls.Add(cmbDepartment);
            ContentPanel.Controls.Add(PageControls.Label("From:", 542, 15, 45));
            dtpFrom = DateTimePicker(587, 13, 120);
            ContentPanel.Controls.Add(dtpFrom);
            ContentPanel.Controls.Add(PageControls.Label("To:", 715, 15, 30));
            dtpTo = DateTimePicker(745, 13, 120);
            ContentPanel.Controls.Add(dtpTo);
            Button refresh = PageControls.Button("REFRESH", 875, 11, 105, true);
            refresh.Click += delegate { LoadReport(); };
            ContentPanel.Controls.Add(refresh);

            lblSummary = PageControls.Label("", 18, 48, 960, 28);
            ContentPanel.Controls.Add(lblSummary);

            tabs = new TabControl { Location = new System.Drawing.Point(18, 82), Size = new System.Drawing.Size(967, 465) };
            TabPage attendanceTab = new TabPage("Attendance by Event / Session");
            TabPage absenceTab = new TabPage("Students with Most Absences");
            attendanceGrid = PageControls.Grid(5, 5, 950, 410);
            absenceGrid = PageControls.Grid(5, 5, 950, 410);
            attendanceTab.Controls.Add(attendanceGrid);
            absenceTab.Controls.Add(absenceGrid);
            tabs.TabPages.Add(attendanceTab);
            tabs.TabPages.Add(absenceTab);
            ContentPanel.Controls.Add(tabs);

            Button export = PageControls.Button("EXPORT CURRENT TAB TO CSV", 730, 510, 250);
            export.Click += ExportCurrentTab;
            ContentPanel.Controls.Add(export);

            LoadChoices();
        }

        private static DateTimePicker DateTimePicker(int x, int y, int width)
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(width, 25)
            };
        }

        private void LoadChoices()
        {
            try
            {
                PageControls.BindChoices(cmbEvent, EventService.GetChoices(), "event_id", "display_name", "All Events");
                PageControls.BindChoices(cmbDepartment, DepartmentService.GetAll(), "department_id", "department_code", "All Departments");
                dtpFrom.Value = DateTime.Today.AddDays(-30);
                dtpTo.Value = DateTime.Today;
                LoadReport();
            }
            catch (Exception ex)
            {
                lblSummary.Text = Database.GetFriendlyMessage(ex);
            }
        }

        private void LoadReport()
        {
            try
            {
                int eventId = PageControls.SelectedId(cmbEvent);
                int departmentId = PageControls.SelectedId(cmbDepartment);
                DataTable summary = ReportService.GetAttendanceReport(eventId, departmentId, dtpFrom.Value, dtpTo.Value);
                DataTable absences = ReportService.GetMostAbsentStudents(eventId, dtpFrom.Value, dtpTo.Value);
                attendanceGrid.DataSource = summary;
                absenceGrid.DataSource = absences;

                int totalEligible = 0;
                int totalPresent = 0;
                int totalAbsent = 0;
                int points = 0;
                foreach (DataRow row in summary.Rows)
                {
                    totalEligible += Convert.ToInt32(row["eligible_students"]);
                    totalPresent += Convert.ToInt32(row["present_count"]);
                    totalAbsent += Convert.ToInt32(row["absent_count"]);
                    points += Convert.ToInt32(row["obligation_points"]);
                }
                double rate = totalEligible == 0 ? 0 : (100.0 * totalPresent / totalEligible);
                lblSummary.Text = "Completed sessions: " + summary.Rows.Count + "    Attendance: " +
                    rate.ToString("0.0") + "% (" + totalPresent + " present / " + totalAbsent +
                    " absent rows)    Absence points generated: " + points;
            }
            catch (Exception ex)
            {
                lblSummary.Text = Database.GetFriendlyMessage(ex);
            }
        }

        private void ExportCurrentTab(object sender, EventArgs e)
        {
            DataGridView selected = tabs.SelectedIndex == 0 ? attendanceGrid : absenceGrid;
            DataTable table = selected.DataSource as DataTable;
            if (table == null || table.Rows.Count == 0)
            {
                MessageBox.Show("There are no rows to export.", "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog save = new SaveFileDialog())
            {
                save.Filter = "CSV files (*.csv)|*.csv";
                save.FileName = tabs.SelectedIndex == 0 ? "attendance-report.csv" : "absence-report.csv";
                if (save.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(save.FileName, false, new UTF8Encoding(true)))
                    {
                        for (int column = 0; column < table.Columns.Count; column++)
                        {
                            if (column > 0) writer.Write(",");
                            writer.Write(EscapeCsv(table.Columns[column].ColumnName));
                        }
                        writer.WriteLine();
                        foreach (DataRow row in table.Rows)
                        {
                            for (int column = 0; column < table.Columns.Count; column++)
                            {
                                if (column > 0) writer.Write(",");
                                writer.Write(EscapeCsv(row[column] == DBNull.Value ? "" : row[column].ToString()));
                            }
                            writer.WriteLine();
                        }
                    }
                    MessageBox.Show("Report exported successfully.", "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    PageControls.ShowError(ex, "Export CSV");
                }
            }
        }

        private static string EscapeCsv(string value)
        {
            return "\"" + (value ?? "").Replace("\"", "\"\"") + "\"";
        }
    }
}
