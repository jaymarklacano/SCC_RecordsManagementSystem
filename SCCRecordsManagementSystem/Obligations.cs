using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public class Obligations : ExtendedPageForm
    {
        private readonly ComboBox cmbEvent;
        private readonly ComboBox cmbDepartment;
        private readonly ComboBox cmbYear;
        private readonly TextBox txtSearch;
        private readonly CheckBox chkHasBalance;
        private readonly DataGridView grid;
        private readonly Button btnDeduct;
        private readonly Button btnClear;
        private readonly Label lblSummary;

        public Obligations() : base("Student Obligations", SidebarPage.Obligations)
        {
            ContentPanel.Controls.Add(PageControls.Label("Event:", 18, 15, 55));
            cmbEvent = PageControls.Combo(72, 13, 230);
            ContentPanel.Controls.Add(cmbEvent);
            ContentPanel.Controls.Add(PageControls.Label("Department:", 315, 15, 85));
            cmbDepartment = PageControls.Combo(402, 13, 150);
            ContentPanel.Controls.Add(cmbDepartment);
            ContentPanel.Controls.Add(PageControls.Label("Year:", 565, 15, 42));
            cmbYear = PageControls.Combo(607, 13, 110);
            cmbYear.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year" });
            cmbYear.SelectedIndex = 0;
            ContentPanel.Controls.Add(cmbYear);
            chkHasBalance = new CheckBox { AutoSize = true, Location = new Point(730, 17), Text = "Has balance" };
            ContentPanel.Controls.Add(chkHasBalance);

            ContentPanel.Controls.Add(PageControls.Label("Search:", 18, 55, 60));
            txtSearch = PageControls.TextBox(78, 53, 300);
            ContentPanel.Controls.Add(txtSearch);
            btnDeduct = PageControls.Button("DEDUCT POINTS", 755, 49, 125, true);
            btnDeduct.Click += DeductPoints;
            ContentPanel.Controls.Add(btnDeduct);
            btnClear = PageControls.Button("CLEAR TO ZERO", 885, 49, 100);
            btnClear.Click += ClearBalance;
            ContentPanel.Controls.Add(btnClear);
            Button history = PageControls.Button("ADJUSTMENT HISTORY", 565, 49, 180);
            history.Click += ShowHistory;
            ContentPanel.Controls.Add(history);

            grid = PageControls.Grid(18, 94, 967, 415);
            ContentPanel.Controls.Add(grid);
            lblSummary = PageControls.Label("", 18, 516, 967, 28);
            lblSummary.ForeColor = Color.DimGray;
            ContentPanel.Controls.Add(lblSummary);

            cmbEvent.SelectedIndexChanged += FilterChanged;
            cmbDepartment.SelectedIndexChanged += FilterChanged;
            cmbYear.SelectedIndexChanged += FilterChanged;
            txtSearch.TextChanged += FilterChanged;
            chkHasBalance.CheckedChanged += FilterChanged;
            LoadChoices();
        }

        private void LoadChoices()
        {
            try
            {
                PageControls.BindChoices(cmbEvent, EventService.GetChoices(), "event_id", "display_name", "All Events");
                PageControls.BindChoices(cmbDepartment, DepartmentService.GetAll(), "department_id", "department_code", "All Departments");
                LoadBalances();
            }
            catch (Exception ex)
            {
                lblSummary.Text = Database.GetFriendlyMessage(ex);
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && cmbEvent.DataSource != null)
            {
                LoadBalances();
            }
        }

        private void LoadBalances()
        {
            try
            {
                int eventId = PageControls.SelectedId(cmbEvent);
                int department = PageControls.SelectedId(cmbDepartment);
                string year = cmbYear.SelectedIndex <= 0 ? "" : cmbYear.Text;
                DataTable rows = ObligationService.GetBalances(eventId, department, year, txtSearch.Text, chkHasBalance.Checked);
                grid.DataSource = rows;
                grid.ClearSelection();
                if (grid.Columns.Contains("student_id"))
                {
                    grid.Columns["student_id"].Visible = false;
                }
                int absences = 0;
                int balance = 0;
                foreach (DataRow row in rows.Rows)
                {
                    absences += Convert.ToInt32(row["absent_count"]);
                    balance += Convert.ToInt32(row["balance"]);
                }
                lblSummary.Text = "Showing " + rows.Rows.Count + " student(s). " + absences +
                    " absent sessions in this view; outstanding balance: " + balance + " points. Each completed absence adds 10 points.";
                bool canAdjust = eventId > 0;
                btnDeduct.Enabled = canAdjust;
                btnClear.Enabled = canAdjust;
            }
            catch (Exception ex)
            {
                lblSummary.Text = Database.GetFriendlyMessage(ex);
            }
        }

        private DataRowView SelectedStudent()
        {
            return grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as DataRowView;
        }

        private void DeductPoints(object sender, EventArgs e)
        {
            DataRowView student = SelectedStudent();
            int eventId = PageControls.SelectedId(cmbEvent);
            if (student == null || eventId == 0)
            {
                MessageBox.Show("Select a student and one event first.", "Deduct Points", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int balance = Convert.ToInt32(student["balance"]);
            if (balance == 0)
            {
                MessageBox.Show("This student has no points to deduct.", "Deduct Points", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (PointAdjustmentDialog dialog = new PointAdjustmentDialog("Deduct Points", balance, true))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    ObligationService.DeductPoints(Convert.ToInt32(student["student_id"]), dialog.Points,
                        dialog.Reason, eventId);
                    LoadBalances();
                }
                catch (Exception ex)
                {
                    PageControls.ShowError(ex, "Deduct Points");
                }
            }
        }

        private void ClearBalance(object sender, EventArgs e)
        {
            DataRowView student = SelectedStudent();
            int eventId = PageControls.SelectedId(cmbEvent);
            if (student == null || eventId == 0)
            {
                MessageBox.Show("Select a student and one event first.", "Clear Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (PointAdjustmentDialog dialog = new PointAdjustmentDialog("Clear Balance to Zero",
                Convert.ToInt32(student["balance"]), false))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    ObligationService.ClearBalance(Convert.ToInt32(student["student_id"]), eventId, dialog.Reason);
                    LoadBalances();
                }
                catch (Exception ex)
                {
                    PageControls.ShowError(ex, "Clear Balance");
                }
            }
        }

        private void ShowHistory(object sender, EventArgs e)
        {
            DataRowView student = SelectedStudent();
            if (student == null)
            {
                MessageBox.Show("Select a student to view their adjustment history.", "Adjustment History",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable rows = ObligationService.GetHistory(Convert.ToInt32(student["student_id"]), PageControls.SelectedId(cmbEvent));
            using (Form dialog = new Form())
            {
                dialog.Text = "Adjustment History - " + student["full_name"];
                dialog.ClientSize = new Size(700, 360);
                dialog.StartPosition = FormStartPosition.CenterParent;
                DataGridView historyGrid = PageControls.Grid(10, 10, 680, 340);
                historyGrid.DataSource = rows;
                dialog.Controls.Add(historyGrid);
                dialog.ShowDialog(this);
            }
        }
    }

    internal sealed class PointAdjustmentDialog : Form
    {
        private readonly NumericUpDown number;
        private readonly TextBox reason;
        public int Points { get { return Decimal.ToInt32(number.Value); } }
        public string Reason { get { return reason.Text.Trim(); } }

        public PointAdjustmentDialog(string title, int maximum, bool askPoints)
        {
            Text = title;
            ClientSize = new Size(430, askPoints ? 205 : 165);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            int y = 16;
            if (askPoints)
            {
                Controls.Add(PageControls.Label("Points to deduct (max " + maximum + "):", 18, y, 250));
                number = new NumericUpDown
                {
                    Location = new Point(270, y),
                    Maximum = Math.Max(1, maximum),
                    Minimum = 1,
                    Size = new Size(130, 25),
                    Value = Math.Max(1, maximum)
                };
                Controls.Add(number);
                y += 42;
            }
            else
            {
                number = new NumericUpDown { Minimum = 0, Maximum = Math.Max(1, maximum), Value = maximum };
                y += 4;
            }

            Controls.Add(PageControls.Label("Reason (required):", 18, y, 200));
            reason = PageControls.TextBox(18, y + 28, 382);
            Controls.Add(reason);
            Button save = PageControls.Button("SAVE", 190, ClientSize.Height - 42, 100, true);
            save.Click += delegate
            {
                if (string.IsNullOrWhiteSpace(reason.Text))
                {
                    MessageBox.Show("Enter a reason for this adjustment.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult = DialogResult.OK;
                Close();
            };
            Controls.Add(save);
            Button cancel = PageControls.Button("CANCEL", 300, ClientSize.Height - 42, 100);
            cancel.Click += delegate { DialogResult = DialogResult.Cancel; Close(); };
            Controls.Add(cancel);
        }
    }
}
