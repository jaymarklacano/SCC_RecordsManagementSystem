using MySql.Data.MySqlClient;
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
    public partial class LoginHistory : Form
    {
        // true while the filters are being changed by the code (so the change events do nothing).
        private bool isLoading = false;

        public LoginHistory()
        {
            InitializeComponent();
        }

        private void LoginHistory_Load(object sender, EventArgs e)
        {
            isLoading = true;
            txtUserName.Text = "";
            cmbRoleFilter.SelectedIndex = 0;
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Today;
            isLoading = false;

            LoadLoginHistory();
        }

        // Loads the login records using the search box, role and date range.
        private void LoadLoginHistory()
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (startDate > endDate)
            {
                dtgLoginRecords.DataSource = null;
                lblNoRecord.Visible = false;
                lblListInfo.ForeColor = Color.Firebrick;
                lblListInfo.Text = "The start date must be on or before the end date.";
                return;
            }

            string role = "";
            if (cmbRoleFilter.SelectedIndex > 0)
            {
                role = cmbRoleFilter.Text;
            }

            string query = "SELECT lh.login_id, lh.username AS 'Username', " +
                           "CASE WHEN lh.account_id IS NULL THEN '(Deleted Account)' " +
                           "WHEN o.officer_id IS NULL THEN 'Administrator' " +
                           "ELSE CONCAT(o.first_name, ' ', o.last_name) END AS 'Name', " +
                           "lh.role AS 'Role', " +
                           "DATE_FORMAT(lh.logged_in, '%b %d, %Y %h:%i %p') AS 'Logged In', " +
                           "COALESCE(DATE_FORMAT(lh.logged_out, '%b %d, %Y %h:%i %p'), '-') AS 'Logged Out', " +
                           "CASE WHEN lh.logged_out IS NULL THEN '-' " +
                           "ELSE CONCAT(FLOOR(TIMESTAMPDIFF(MINUTE, lh.logged_in, lh.logged_out) / 60), ' hr ', " +
                           "MOD(TIMESTAMPDIFF(MINUTE, lh.logged_in, lh.logged_out), 60), ' min') END AS 'Duration', " +
                           "CASE WHEN lh.login_id = @currentLogin THEN 'Current Session' " +
                           "WHEN lh.logged_out IS NULL THEN 'No log out recorded' " +
                           "ELSE 'Logged Out' END AS 'Status' " +
                           "FROM login_history lh " +
                           "LEFT JOIN officers o ON o.account_id = lh.account_id " +
                           "WHERE (lh.username LIKE @search OR CONCAT(COALESCE(o.first_name, ''), ' ', COALESCE(o.last_name, '')) LIKE @search) " +
                           "AND (@role = '' OR lh.role = @role) " +
                           "AND lh.logged_in >= @startDate AND lh.logged_in < @endDate " +
                           "ORDER BY lh.logged_in DESC";

            MySqlConnection conn = Database.GetConnection();
            DataTable loginTable = new DataTable();

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@currentLogin", AccountInfo.Instantiate.currentUser.LoginId);
                cmd.Parameters.AddWithValue("@search", "%" + txtUserName.Text.Trim() + "%");
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@startDate", startDate);
                // Add one day so the whole end date is included.
                cmd.Parameters.AddWithValue("@endDate", endDate.AddDays(1));

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(loginTable);

                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Database.showError("The login history could not be loaded.", ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            dtgLoginRecords.DataSource = loginTable;

            if (dtgLoginRecords.Columns.Contains("login_id"))
            {
                dtgLoginRecords.Columns["login_id"].Visible = false;
                dtgLoginRecords.Columns["Name"].FillWeight = 130;
                dtgLoginRecords.Columns["Logged In"].FillWeight = 130;
                dtgLoginRecords.Columns["Logged Out"].FillWeight = 130;
                dtgLoginRecords.Columns["Status"].FillWeight = 120;
            }

            // Count the logins of each role and the number of different accounts.
            int adminLogins = 0;
            int officerLogins = 0;
            List<string> usernames = new List<string>();

            foreach (DataRow row in loginTable.Rows)
            {
                if (row["Role"].ToString() == "Admin")
                {
                    adminLogins = adminLogins + 1;
                }
                else
                {
                    officerLogins = officerLogins + 1;
                }

                string username = row["Username"].ToString();
                if (usernames.Contains(username) == false)
                {
                    usernames.Add(username);
                }
            }

            // Color the status so it is easy to read.
            foreach (DataGridViewRow gridRow in dtgLoginRecords.Rows)
            {
                string status = gridRow.Cells["Status"].Value.ToString();

                if (status == "Current Session")
                {
                    gridRow.Cells["Status"].Style.ForeColor = Color.DarkGreen;
                }
                else if (status == "No log out recorded")
                {
                    gridRow.Cells["Status"].Style.ForeColor = Color.DarkOrange;
                }
            }

            dtgLoginRecords.ClearSelection();

            lblListInfo.ForeColor = Color.FromArgb(0, 29, 57);
            lblListInfo.Text = "Showing " + loginTable.Rows.Count + " login(s) from " +
                               startDate.ToString("MMM dd, yyyy") + " to " + endDate.ToString("MMM dd, yyyy") + "\n" +
                               "Admin logins: " + adminLogins + "     Officer logins: " + officerLogins +
                               "     Different accounts: " + usernames.Count;

            if (loginTable.Rows.Count == 0)
            {
                lblNoRecord.Visible = true;
                lblNoRecord.BringToFront();
            }
            else
            {
                lblNoRecord.Visible = false;
            }
        }

        private void filter_Changed(object sender, EventArgs e)
        {
            if (isLoading)
            {
                return;
            }

            LoadLoginHistory();
        }

        // Changes both dates at once and loads the list one time only.
        private void setDateRange(DateTime startDate, DateTime endDate)
        {
            isLoading = true;
            dtpStartDate.Value = startDate;
            dtpEndDate.Value = endDate;
            isLoading = false;

            LoadLoginHistory();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            setDateRange(DateTime.Today, DateTime.Today);
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            setDateRange(DateTime.Today.AddDays(-6), DateTime.Today);
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            setDateRange(DateTime.Today.AddDays(-29), DateTime.Today);
        }

        // Clears the search and role, and shows every login from the very first one.
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            DateTime firstDate = DateTime.Today;

            object firstLogin = Database.GetValue("SELECT MIN(logged_in) FROM login_history", new Dictionary<string, object>());

            if (firstLogin != null && firstLogin != DBNull.Value)
            {
                firstDate = Convert.ToDateTime(firstLogin).Date;
            }

            isLoading = true;
            txtUserName.Text = "";
            cmbRoleFilter.SelectedIndex = 0;
            isLoading = false;

            setDateRange(firstDate, DateTime.Today);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLoginHistory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
