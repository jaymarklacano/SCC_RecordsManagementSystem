using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public class OfficerRecords : ExtendedPageForm
    {
        private readonly TextBox txtSearch;
        private readonly DataGridView grid;
        private readonly Label lblStatus;

        public OfficerRecords() : base("Officer Records", SidebarPage.Officers)
        {
            if (!Session.IsAdmin)
            {
                MessageBox.Show("Only an administrator can manage officer records.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BeginInvoke(new MethodInvoker(Close));
                return;
            }

            ContentPanel.Controls.Add(PageControls.Label("Search:", 20, 17, 60));
            txtSearch = PageControls.TextBox(78, 16, 300);
            txtSearch.TextChanged += delegate { LoadOfficers(); };
            ContentPanel.Controls.Add(txtSearch);

            Button add = PageControls.Button("ADD OFFICER", 840, 13, 125, true);
            add.Click += AddOfficer;
            ContentPanel.Controls.Add(add);
            Button edit = PageControls.Button("EDIT", 720, 13, 105);
            edit.Click += EditOfficer;
            ContentPanel.Controls.Add(edit);
            Button delete = PageControls.Button("DELETE", 605, 13, 105);
            delete.Click += DeleteOfficer;
            ContentPanel.Controls.Add(delete);

            grid = PageControls.Grid(20, 58, 945, 460);
            grid.CellDoubleClick += delegate { EditOfficer(this, EventArgs.Empty); };
            ContentPanel.Controls.Add(grid);
            lblStatus = PageControls.Label("", 20, 524, 945, 26);
            lblStatus.ForeColor = Color.DimGray;
            ContentPanel.Controls.Add(lblStatus);
            LoadOfficers();
        }

        private void LoadOfficers()
        {
            try
            {
                grid.DataSource = OfficerService.Search(txtSearch.Text);
                string[] hiddenColumns = { "officer_id", "account_id", "first_name", "middle_name",
                    "last_name", "department_id", "gender", "email", "contact_number" };
                foreach (string column in hiddenColumns)
                {
                    if (grid.Columns.Contains(column)) grid.Columns[column].Visible = false;
                }
                grid.Columns["officer_number"].HeaderText = "Officer No.";
                grid.Columns["full_name"].HeaderText = "Name";
                grid.Columns["department_code"].HeaderText = "Department";
                grid.Columns["year_level"].HeaderText = "Year";
                grid.Columns["school_year"].HeaderText = "School Year";
                grid.Columns["username"].HeaderText = "Login";
                grid.ClearSelection();
                lblStatus.Text = "Officer accounts can sign in using the same login screen. Only admins can create or reset them.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = Database.GetFriendlyMessage(ex);
            }
        }

        private DataRowView SelectedOfficer()
        {
            return grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as DataRowView;
        }

        private void AddOfficer(object sender, EventArgs e)
        {
            using (OfficerEditor editor = new OfficerEditor(null))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    LoadOfficers();
                }
            }
        }

        private void EditOfficer(object sender, EventArgs e)
        {
            DataRowView selected = SelectedOfficer();
            if (selected == null)
            {
                MessageBox.Show("Select an officer first.", "Edit Officer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (OfficerEditor editor = new OfficerEditor(selected))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    LoadOfficers();
                }
            }
        }

        private void DeleteOfficer(object sender, EventArgs e)
        {
            DataRowView selected = SelectedOfficer();
            if (selected == null)
            {
                MessageBox.Show("Select an officer first.", "Delete Officer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Delete " + selected["full_name"] + " and disable the officer's login?",
                "Delete Officer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                OfficerService.Delete(Convert.ToInt32(selected["officer_id"]));
                LoadOfficers();
            }
            catch (Exception ex)
            {
                PageControls.ShowError(ex, "Delete Officer");
            }
        }
    }

    internal sealed class OfficerEditor : Form
    {
        private readonly int officerId;
        private readonly int accountId;
        private readonly TextBox txtUsername;
        private readonly TextBox txtPassword;
        private readonly TextBox txtNumber;
        private readonly TextBox txtFirst;
        private readonly TextBox txtMiddle;
        private readonly TextBox txtLast;
        private readonly ComboBox cmbDepartment;
        private readonly ComboBox cmbYear;
        private readonly ComboBox cmbGender;
        private readonly TextBox txtEmail;
        private readonly TextBox txtContact;
        private readonly TextBox txtPosition;
        private readonly TextBox txtSchoolYear;

        public OfficerEditor(DataRowView row)
        {
            officerId = row == null ? 0 : Convert.ToInt32(row["officer_id"]);
            accountId = row == null ? 0 : Convert.ToInt32(row["account_id"]);
            Text = officerId == 0 ? "Create Officer and Login" : "Edit Officer";
            ClientSize = new Size(650, 625);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Font = new Font("Cambria", 10F);

            AddLabel("Username *", 20, 18);
            txtUsername = AddText(145, 18, 180);
            AddLabel("Password *", 350, 18);
            txtPassword = AddText(445, 18, 180);
            txtPassword.UseSystemPasswordChar = true;
            AddLabel("Officer No. *", 20, 60);
            txtNumber = AddText(145, 60, 180);
            AddLabel("Position *", 350, 60);
            txtPosition = AddText(445, 60, 180);
            AddLabel("First name *", 20, 102);
            txtFirst = AddText(145, 102, 180);
            AddLabel("Middle name", 350, 102);
            txtMiddle = AddText(445, 102, 180);
            AddLabel("Last name *", 20, 144);
            txtLast = AddText(145, 144, 180);
            AddLabel("Department *", 350, 144);
            cmbDepartment = AddCombo(445, 144, 180);
            AddLabel("Year level *", 20, 186);
            cmbYear = AddCombo(145, 186, 180);
            cmbYear.Items.AddRange(StudentService.YearLevels);
            cmbYear.SelectedIndex = 0;
            AddLabel("Gender *", 350, 186);
            cmbGender = AddCombo(445, 186, 180);
            cmbGender.Items.AddRange(StudentService.Genders);
            cmbGender.SelectedIndex = 0;
            AddLabel("Email", 20, 228);
            txtEmail = AddText(145, 228, 180);
            AddLabel("Contact", 350, 228);
            txtContact = AddText(445, 228, 180);
            AddLabel("School year *", 20, 270);
            txtSchoolYear = AddText(145, 270, 180, "2026-2027");

            Label passwordNote = new Label
            {
                Location = new Point(445, 46),
                Size = new Size(180, 34),
                Font = new Font("Cambria", 8.5F),
                ForeColor = Color.DimGray,
                Text = officerId == 0 ? "At least 6 characters" : "Leave blank to keep current password"
            };
            Controls.Add(passwordNote);

            Button save = PageControls.Button(officerId == 0 ? "CREATE OFFICER" : "SAVE CHANGES", 365, 555, 130, true);
            save.Click += SaveOfficer;
            Controls.Add(save);
            Button cancel = PageControls.Button("CANCEL", 510, 555, 115);
            cancel.Click += delegate { DialogResult = DialogResult.Cancel; Close(); };
            Controls.Add(cancel);

            if (row != null)
            {
                LoadOfficer(row);
            }
        }

        private void LoadOfficer(DataRowView row)
        {
            txtUsername.Text = row["username"].ToString();
            txtNumber.Text = row["officer_number"].ToString();
            txtFirst.Text = row["first_name"].ToString();
            txtMiddle.Text = row["middle_name"] == DBNull.Value ? "" : row["middle_name"].ToString();
            txtLast.Text = row["last_name"].ToString();
            cmbDepartment.SelectedValue = Convert.ToInt32(row["department_id"]);
            cmbYear.SelectedItem = row["year_level"].ToString();
            cmbGender.SelectedItem = row["gender"].ToString();
            txtEmail.Text = row["email"] == DBNull.Value ? "" : row["email"].ToString();
            txtContact.Text = row["contact_number"] == DBNull.Value ? "" : row["contact_number"].ToString();
            txtPosition.Text = row["position"].ToString();
            txtSchoolYear.Text = row["school_year"].ToString();
        }

        private void SaveOfficer(object sender, EventArgs e)
        {
            try
            {
                int department = PageControls.SelectedId(cmbDepartment);
                if (officerId == 0)
                {
                    OfficerService.Create(txtUsername.Text, txtPassword.Text, txtNumber.Text, txtFirst.Text,
                        txtMiddle.Text, txtLast.Text, department, cmbYear.Text, cmbGender.Text,
                        txtEmail.Text, txtContact.Text, txtPosition.Text, txtSchoolYear.Text);
                }
                else
                {
                    OfficerService.Update(officerId, accountId, txtUsername.Text, txtPassword.Text, txtNumber.Text,
                        txtFirst.Text, txtMiddle.Text, txtLast.Text, department, cmbYear.Text, cmbGender.Text,
                        txtEmail.Text, txtContact.Text, txtPosition.Text, txtSchoolYear.Text);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                PageControls.ShowError(ex, "Save Officer");
            }
        }

        private void AddLabel(string text, int x, int y)
        {
            Controls.Add(PageControls.Label(text, x, y, 120));
        }

        private TextBox AddText(int x, int y, int width, string initial = "")
        {
            TextBox text = PageControls.TextBox(x, y, width, initial);
            Controls.Add(text);
            return text;
        }

        private ComboBox AddCombo(int x, int y, int width)
        {
            ComboBox combo = PageControls.Combo(x, y, width);
            Controls.Add(combo);
            return combo;
        }
    }
}
