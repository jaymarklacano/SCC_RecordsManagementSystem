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
    public partial class RulesPermission : Form
    {
        // true while the grid or combo boxes are being filled (so the change events do nothing).
        private bool isLoading = true;

        // The rule shown in the Rule Details panel. 0 means a new rule is being typed.
        private int selectedRuleId = 0;

        public RulesPermission()
        {
            InitializeComponent();
        }

        private void RulesPermission_Load(object sender, EventArgs e)
        {
            btnSavePermission.Enabled = false;
            isLoading = true;
            try
            {
                cmbRuleStatus.SelectedIndex = 0;
                cmbRole.SelectedIndex = 0;
                txtSearchRule.Text = "";
            }
            finally
            {
                isLoading = false;
            }

            LoadRules();
            clearRuleDetails();
            LoadRolePermissions();
            LoadPermissionSummary();
        }

        private void RulesPermission_FormClosed(object sender, FormClosedEventArgs e)
        {
            Database.exitIfNoFormIsOpen(this);
        }

        // =====================================================================
        // RULES
        // =====================================================================

        // Loads the rules into the grid using the search box and the Show filter.
        private void LoadRules()
        {
            // 0 = All Rules, 1 = Active Only, 2 = Inactive Only
            int statusFilter = cmbRuleStatus.SelectedIndex;
            if (statusFilter < 0)
            {
                statusFilter = 0;
            }

            string query = "SELECT rule_id AS 'Rule ID', description AS 'Description', " +
                           "IF(active = 1, 'Active', 'Inactive') AS 'Status', " +
                           "DATE_FORMAT(updated_at, '%b %d, %Y %h:%i %p') AS 'Last Updated' " +
                           "FROM rules " +
                           "WHERE description LIKE @search " +
                           "AND (@status = 0 OR (@status = 1 AND active = 1) OR (@status = 2 AND active = 0)) " +
                           "ORDER BY rule_id";

            MySqlConnection conn = null;
            DataTable ruleTable = new DataTable();

            try
            {
                conn = Database.GetConnection();
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearchRule.Text.Trim() + "%");
                cmd.Parameters.AddWithValue("@status", statusFilter);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ruleTable);

                adapter.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                dtgRules.DataSource = null;
                clearRuleDetails();
                lblNoRule.Text = "Rules unavailable.";
                lblNoRule.Visible = true;
                Database.showError("The rules could not be loaded.", ex);
                return;
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }

            isLoading = true;
            try
            {
                dtgRules.DataSource = ruleTable;

                if (dtgRules.Columns.Contains("Rule ID"))
                {
                    dtgRules.Columns["Rule ID"].FillWeight = 40;
                    dtgRules.Columns["Description"].FillWeight = 300;
                    dtgRules.Columns["Status"].FillWeight = 50;
                    dtgRules.Columns["Last Updated"].FillWeight = 90;
                }

                // Inactive rules are shown in gray so they are easy to spot.
                foreach (DataGridViewRow row in dtgRules.Rows)
                {
                    if (row.Cells["Status"].Value.ToString() == "Inactive")
                    {
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                    }
                }

                dtgRules.ClearSelection();
            }
            finally
            {
                isLoading = false;
            }

            // Show the empty message when nothing matches.
            if (ruleTable.Rows.Count == 0)
            {
                lblNoRule.Text = "No matching rules.";
                lblNoRule.Visible = true;
                lblNoRule.BringToFront();
            }
            else
            {
                lblNoRule.Visible = false;
            }
        }

        // Clears the Rule Details panel so a new rule can be typed.
        private void clearRuleDetails()
        {
            selectedRuleId = 0;
            txtRuleID.Text = "(New Rule)";
            txtDescriptionD.Text = "";
            chkRuleActive.Checked = true;
            btnSaveChanges.Text = "Add Rule";
            btnDeleteRule.Enabled = false;
            lblRuleDetails.Text = "Rule Details - New Rule";
        }

        // Shows the selected rule of the grid in the Rule Details panel.
        private void showSelectedRule()
        {
            if (dtgRules.SelectedRows.Count == 0)
            {
                return;
            }

            DataGridViewRow row = dtgRules.SelectedRows[0];

            selectedRuleId = Convert.ToInt32(row.Cells["Rule ID"].Value);
            txtRuleID.Text = selectedRuleId.ToString();
            txtDescriptionD.Text = row.Cells["Description"].Value.ToString();

            if (row.Cells["Status"].Value.ToString() == "Active")
            {
                chkRuleActive.Checked = true;
            }
            else
            {
                chkRuleActive.Checked = false;
            }

            btnSaveChanges.Text = "Save Changes";
            btnDeleteRule.Enabled = true;
            lblRuleDetails.Text = "Rule Details - Editing Rule #" + selectedRuleId;
        }

        private void dtgRules_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading)
            {
                return;
            }

            showSelectedRule();
        }

        private void ruleFilter_Changed(object sender, EventArgs e)
        {
            if (isLoading)
            {
                return;
            }

            LoadRules();
            clearRuleDetails();
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            isLoading = true;
            try
            {
                dtgRules.ClearSelection();
            }
            finally
            {
                isLoading = false;
            }

            clearRuleDetails();
            txtDescriptionD.Focus();
        }

        // Reset puts back the saved values of the selected rule (or clears a new rule).
        private void btnResetRules_Click(object sender, EventArgs e)
        {
            if (selectedRuleId == 0)
            {
                clearRuleDetails();
            }
            else
            {
                showSelectedRule();
            }

            txtDescriptionD.Focus();
        }

        // Checks the description before saving. Returns true when it is valid.
        private bool isRuleValid()
        {
            string description = txtDescriptionD.Text.Trim();

            if (description == "")
            {
                MessageBox.Show("Please enter the rule description.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescriptionD.Focus();
                return false;
            }

            if (description.Length < 5)
            {
                MessageBox.Show("The rule description is too short. Please describe the rule clearly.", "Invalid Description", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescriptionD.Focus();
                return false;
            }

            // The same rule must not be saved twice.
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@description", description);
            parameters.Add("@ruleId", selectedRuleId);

            int sameRules = Database.GetCount("SELECT COUNT(*) FROM rules WHERE description = @description AND rule_id <> @ruleId", parameters);

            if (sameRules > 0)
            {
                MessageBox.Show("This rule already exists.", "Duplicate Rule", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescriptionD.Focus();
                return false;
            }

            return true;
        }

        // Adds a new rule or saves the changes of the selected rule.
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (isRuleValid() == false)
                {
                    return;
                }
            }
            catch (MySqlException ex)
            {
                Database.showError("Could not check for an existing rule. No changes were saved.", ex);
                return;
            }

            int active = 0;
            if (chkRuleActive.Checked)
            {
                active = 1;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@description", txtDescriptionD.Text.Trim());
            parameters.Add("@active", active);

            bool saved;

            if (selectedRuleId == 0)
            {
                parameters.Add("@command", "The rule was added successfully.");
                saved = Database.saveUpdateDeleteData("INSERT INTO rules (description, active) VALUES (@description, @active)", parameters);
            }
            else
            {
                parameters.Add("@ruleId", selectedRuleId);
                parameters.Add("@command", "The rule was updated successfully.");
                saved = Database.saveUpdateDeleteData("UPDATE rules SET description = @description, active = @active WHERE rule_id = @ruleId", parameters);
            }

            if (saved)
            {
                LoadRules();
                clearRuleDetails();
            }
        }

        private void btnDeleteRule_Click(object sender, EventArgs e)
        {
            if (selectedRuleId == 0)
            {
                MessageBox.Show("Please select a rule from the list first.", "No Rule Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult answer = MessageBox.Show("Are you sure you want to delete this rule?\n\n" + txtDescriptionD.Text.Trim(),
                "Delete Rule", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@ruleId", selectedRuleId);
            parameters.Add("@command", "delete");

            bool deleted = Database.saveUpdateDeleteData("DELETE FROM rules WHERE rule_id = @ruleId", parameters);

            if (deleted)
            {
                LoadRules();
                clearRuleDetails();
            }
        }

        // =====================================================================
        // PERMISSIONS
        // =====================================================================

        // Ticks the menus that the selected role is allowed to open.
        private void LoadRolePermissions()
        {
            string role = cmbRole.Text;

            chkPermission.Enabled = false;
            btnSavePermission.Enabled = false;
            btnCheckAll.Enabled = false;
            btnUncheckAll.Enabled = false;
            // Uncheck everything first.
            for (int i = 0; i < chkPermission.Items.Count; i++)
            {
                chkPermission.SetItemChecked(i, false);
            }

            // The Admin can always open every menu, so the list is locked for the Admin.
            if (role == "Admin")
            {
                for (int i = 0; i < chkPermission.Items.Count; i++)
                {
                    chkPermission.SetItemChecked(i, true);
                }

                lblRoleNote.Text = "Admin has full access.";
                return;
            }

            lblRoleNote.Text = "Permissions not loaded. Select the role again to retry.";
            if (role != "Officer")
            {
                return;
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@role", role);

            DataTable permissionTable;
            try
            {
                permissionTable = Database.GetData("SELECT module, allowed FROM role_permissions WHERE role = @role", parameters);
            }
            catch (MySqlException ex)
            {
                Database.showError("Officer permissions could not be loaded. Editing is disabled.", ex);
                return;
            }

            foreach (DataRow row in permissionTable.Rows)
            {
                string module = row["module"].ToString();
                int index = chkPermission.Items.IndexOf(module);

                if (index >= 0 && Convert.ToInt32(row["allowed"]) == 1)
                {
                    chkPermission.SetItemChecked(index, true);
                }
            }
            chkPermission.Enabled = true;
            btnCheckAll.Enabled = true;
            btnUncheckAll.Enabled = true;
            btnSavePermission.Enabled = true;
            lblRoleNote.Text = "Tick the allowed menus.";
        }

        // Shows a table of every menu and which roles can open it.
        // The Admin can open every menu. Rules and Permission is always for the Admin only.
        private void LoadPermissionSummary()
        {
            string query = "SELECT module AS 'Menu', 'Allowed' AS 'Admin', " +
                           "CASE WHEN module = 'Rules and Permission' THEN 'Not Allowed (Admin only)' " +
                           "WHEN MAX(CASE WHEN role = 'Officer' THEN allowed END) = 1 THEN 'Allowed' " +
                           "ELSE 'Not Allowed' END AS 'Officer' " +
                           "FROM role_permissions " +
                           "GROUP BY module " +
                           "ORDER BY FIELD(module, 'Departments', 'Student Records', 'Officer Records', 'Face Recognition', " +
                           "'Event Management', 'Attendance Monitoring', 'Reports and Analysis', 'Obligations', 'Rules and Permission')";

            DataTable summaryTable = Database.GetData(query, new Dictionary<string, object>());

            dtgPermission.DataSource = summaryTable;

            // Color the Officer column so it is easy to read.
            foreach (DataGridViewRow gridRow in dtgPermission.Rows)
            {
                if (gridRow.Cells["Officer"].Value.ToString() == "Allowed")
                {
                    gridRow.Cells["Officer"].Style.ForeColor = Color.DarkGreen;
                }
                else
                {
                    gridRow.Cells["Officer"].Style.ForeColor = Color.Firebrick;
                }
            }

            dtgPermission.ClearSelection();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading)
            {
                return;
            }

            LoadRolePermissions();
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkPermission.Items.Count; i++)
            {
                chkPermission.SetItemChecked(i, true);
            }
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkPermission.Items.Count; i++)
            {
                chkPermission.SetItemChecked(i, false);
            }
        }

        // Saves the ticked menus of the selected role.
        private void btnSavePermission_Click(object sender, EventArgs e)
        {
            string role = cmbRole.Text;

            if (!btnSavePermission.Enabled || role != "Officer")
            {
                return;
            }

            DialogResult answer = MessageBox.Show("Save the menu permissions of the " + role + " role?\n\n" +
                "The changes will apply the next time an " + role + " opens a menu.",
                "Save Permissions", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            MySqlConnection conn = null;
            MySqlTransaction transaction = null;
            bool saved = false;

            try
            {
                conn = Database.GetConnection();
                conn.Open();

                // A transaction makes sure that all the menus are saved or none of them.
                transaction = conn.BeginTransaction();

                for (int i = 0; i < chkPermission.Items.Count; i++)
                {
                    int allowed = 0;
                    if (chkPermission.GetItemChecked(i))
                    {
                        allowed = 1;
                    }

                    // Insert the permission, or update it if the role and menu already exist.
                    string query = "INSERT INTO role_permissions (role, module, allowed) VALUES (@role, @module, @allowed) " +
                                   "ON DUPLICATE KEY UPDATE allowed = @allowed";

                    MySqlCommand cmd = new MySqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@module", chkPermission.Items[i].ToString());
                    cmd.Parameters.AddWithValue("@allowed", allowed);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

                transaction.Commit();
                saved = true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    {
                        // The connection is already closed, nothing to roll back.
                    }
                }

                Database.showError("The permissions could not be saved.", ex);
            }
            finally
            {
                if (conn != null) conn.Dispose();
            }

            if (saved)
            {
                MessageBox.Show("The permissions of the " + role + " role were saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRolePermissions();
                LoadPermissionSummary();
            }
        }

        // =====================================================================
        // SIDEBAR MENU
        // =====================================================================

        private void btnRulesLogout_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                Database.saveLogoutTime();

                lnkCreateAccount login = new lnkCreateAccount();
                login.Show();
                this.Close();
            }
        }

        private void btnRulesDash_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnRulesDept_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Departments") == false)
            {
                return;
            }

            Departments departments = new Departments();
            departments.Show();
            this.Close();
        }

        private void btnRulesStudent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Student Records") == false)
            {
                return;
            }

            StudentRecords studentRecords = new StudentRecords();
            studentRecords.Show();
            this.Close();
        }

        private void btnRulesOfficer_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Officer Records") == false)
            {
                return;
            }

            OfficerRecords officerRecords = new OfficerRecords();
            officerRecords.Show();
            this.Close();
        }

        private void btnRulesFaceRecog_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Face Recognition") == false)
            {
                return;
            }

            FaceRecognition faceRecognition = new FaceRecognition();
            faceRecognition.Show();
            this.Close();
        }

        private void btnRulesEvent_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Event Management") == false)
            {
                return;
            }

            EventManagement eventManagement = new EventManagement();
            eventManagement.Show();
            this.Close();
        }

        private void btnRulesAttendance_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Attendance Monitoring") == false)
            {
                return;
            }

            AttendanceMonitoring attendance = new AttendanceMonitoring();
            attendance.Show();
            this.Close();
        }

        private void btnRulesReports_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Reports and Analysis") == false)
            {
                return;
            }

            ReportsAndAnalysis report = new ReportsAndAnalysis();
            report.Show();
            this.Close();
        }

        private void btnRulesObli_Click(object sender, EventArgs e)
        {
            if (Database.isAllowed("Obligations") == false)
            {
                return;
            }

            Obligations obligations = new Obligations();
            obligations.Show();
            this.Close();
        }

        // This is the current page, so it only refreshes the lists.
        private void btnRulesPer_Click(object sender, EventArgs e)
        {
            LoadRules();
            clearRuleDetails();
            LoadRolePermissions();
            LoadPermissionSummary();
        }
    }
}
