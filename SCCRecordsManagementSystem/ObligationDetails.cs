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
    // Small window that shows why a student has an obligation:
    // the list of absences and the points that were added, deducted or cleared.
    public partial class ObligationDetails : Form
    {
        // The student that is shown (sent by the Obligations form).
        private int studentId = 0;

        public ObligationDetails(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;
        }

        private void ObligationDetails_Load(object sender, EventArgs e)
        {
            bool found = LoadStudentInfo();

            if (found == false)
            {
                MessageBox.Show("This student no longer exists. The list will be refreshed.", "Student Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadAbsences();
            LoadAdjustments();

            // Only the Admin can delete an adjustment.
            if (AccountInfo.Instantiate.currentUser.Role == "Admin")
            {
                btnDeleteAdjustment.Enabled = true;
                lblAdminOnly.Text = "Select a row in the list above, then click Delete.";
            }
            else
            {
                btnDeleteAdjustment.Enabled = false;
                lblAdminOnly.Text = "Only the Admin can delete an adjustment.";
            }
        }

        // Shows the name, information and point totals of the student.
        // Returns false when the student is not found.
        private bool LoadStudentInfo()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", studentId);

            DataTable studentTable = Database.GetData(
                "SELECT student_number, full_name, department_code, year_level, status, " +
                "absent_count, absence_points, adjustment_points, balance " +
                "FROM obligation_summary WHERE student_id = @student", parameters);

            if (studentTable.Rows.Count == 0)
            {
                return false;
            }

            DataRow row = studentTable.Rows[0];

            lblStudentName.Text = row["full_name"].ToString();
            lblStudentInfo.Text = "ID Number : " + row["student_number"] +
                                  "   |   Department : " + row["department_code"] +
                                  "   |   Year Level : " + row["year_level"] +
                                  "   |   Status : " + row["status"];

            lblAbsentCount.Text = "Absences : " + row["absent_count"];
            lblAbsencePoints.Text = "Absence Points : " + row["absence_points"];
            lblAdjustmentPoints.Text = "Adjustments : " + row["adjustment_points"];
            lblBalance.Text = "Balance : " + row["balance"];

            if (Convert.ToInt32(row["balance"]) > 0)
            {
                lblBalance.ForeColor = Color.Firebrick;
            }
            else
            {
                lblBalance.ForeColor = Color.DarkGreen;
            }

            return true;
        }

        // Every session where the student was absent (only finished sessions are counted as absent).
        private void LoadAbsences()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", studentId);

            DataTable absenceTable = Database.GetData(
                "SELECT event_name AS 'Event', DATE_FORMAT(session_date, '%b %d, %Y') AS 'Date', " +
                "session_name AS 'Session', " +
                "CONCAT(DATE_FORMAT(starts_at, '%h:%i %p'), ' - ', DATE_FORMAT(ends_at, '%h:%i %p')) AS 'Time', " +
                "10 AS 'Points' " +
                "FROM attendance_records " +
                "WHERE student_id = @student AND status = 'Absent' " +
                "ORDER BY starts_at DESC", parameters);

            dtgAbsences.DataSource = absenceTable;

            if (absenceTable.Rows.Count == 0)
            {
                lblNoAbsence.Visible = true;
                lblNoAbsence.BringToFront();
            }
            else
            {
                lblNoAbsence.Visible = false;
            }
        }

        // Every point that was added, deducted or cleared, with the reason and the user who did it.
        private void LoadAdjustments()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@student", studentId);

            DataTable adjustmentTable = Database.GetData(
                "SELECT oa.adjustment_id, DATE_FORMAT(oa.created_at, '%b %d, %Y %h:%i %p') AS 'Date', " +
                "IF(oa.points > 0, 'Added', 'Deducted') AS 'Type', oa.points AS 'Points', " +
                "oa.reason AS 'Reason', COALESCE(a.username, '-') AS 'Recorded By' " +
                "FROM obligation_adjustments oa " +
                "LEFT JOIN accounts a ON a.account_id = oa.account_id " +
                "WHERE oa.student_id = @student " +
                "ORDER BY oa.created_at DESC, oa.adjustment_id DESC", parameters);

            dtgAdjustments.DataSource = adjustmentTable;

            if (dtgAdjustments.Columns.Contains("adjustment_id"))
            {
                dtgAdjustments.Columns["adjustment_id"].Visible = false;
            }

            if (dtgAdjustments.Columns.Contains("Reason"))
            {
                dtgAdjustments.Columns["Reason"].FillWeight = 200;
            }

            // Red = points added, green = points deducted.
            for (int i = 0; i < dtgAdjustments.Rows.Count; i++)
            {
                DataGridViewRow row = dtgAdjustments.Rows[i];

                if (Convert.ToInt32(row.Cells["Points"].Value) > 0)
                {
                    row.DefaultCellStyle.ForeColor = Color.Firebrick;
                }
                else
                {
                    row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                }
            }

            dtgAdjustments.ClearSelection();

            if (adjustmentTable.Rows.Count == 0)
            {
                lblNoAdjustment.Visible = true;
                lblNoAdjustment.BringToFront();
            }
            else
            {
                lblNoAdjustment.Visible = false;
            }
        }

        private void btnDeleteAdjustment_Click(object sender, EventArgs e)
        {
            if (AccountInfo.Instantiate.currentUser.Role != "Admin")
            {
                MessageBox.Show("Only the Admin can delete an adjustment.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtgAdjustments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an adjustment from the list first.", "No Adjustment Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dtgAdjustments.SelectedRows[0];
            int adjustmentId = Convert.ToInt32(row.Cells["adjustment_id"].Value);
            string points = row.Cells["Points"].Value.ToString();
            string reason = row.Cells["Reason"].Value.ToString();

            DialogResult answer = MessageBox.Show("Delete this adjustment?\n\nPoints: " + points + "\nReason: " + reason +
                "\n\nThe balance of the student will be calculated again.",
                "Delete Adjustment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@adjustment", adjustmentId);
            parameters.Add("@command", "delete");

            bool deleted = Database.saveUpdateDeleteData("DELETE FROM obligation_adjustments WHERE adjustment_id = @adjustment", parameters);

            if (deleted == true)
            {
                LoadStudentInfo();
                LoadAdjustments();
            }
        }

        private void btnCloseDetails_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
