using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public class EventManagement : ExtendedPageForm
    {
        private readonly TextBox txtSearch;
        private readonly DataGridView grid;
        private readonly Label lblStatus;

        public EventManagement() : base("Event Management", SidebarPage.Events)
        {
            ContentPanel.Controls.Add(PageControls.Label("Search:", 20, 17, 60));
            txtSearch = PageControls.TextBox(78, 16, 280);
            txtSearch.TextChanged += delegate { LoadEvents(); };
            ContentPanel.Controls.Add(txtSearch);

            Button add = PageControls.Button("ADD EVENT", 775, 13, 115, true);
            add.Click += AddEvent;
            ContentPanel.Controls.Add(add);
            Button edit = PageControls.Button("EDIT", 650, 13, 110);
            edit.Click += EditEvent;
            ContentPanel.Controls.Add(edit);
            Button delete = PageControls.Button("DELETE", 530, 13, 105);
            delete.Click += DeleteEvent;
            ContentPanel.Controls.Add(delete);

            grid = PageControls.Grid(20, 58, 945, 460);
            grid.CellDoubleClick += delegate { EditEvent(this, EventArgs.Empty); };
            ContentPanel.Controls.Add(grid);

            lblStatus = PageControls.Label("", 20, 524, 945, 28);
            lblStatus.ForeColor = Color.DimGray;
            ContentPanel.Controls.Add(lblStatus);
            LoadEvents();
        }

        private void LoadEvents()
        {
            try
            {
                grid.DataSource = EventService.Search(txtSearch.Text);
                string[] hiddenColumns = { "event_id", "department_id", "requires_logout", "morning_enabled",
                    "morning_start", "morning_end", "afternoon_enabled", "afternoon_start", "afternoon_end" };
                foreach (string column in hiddenColumns)
                {
                    if (grid.Columns.Contains(column)) grid.Columns[column].Visible = false;
                }
                grid.Columns["event_name"].HeaderText = "Event";
                grid.Columns["start_date"].HeaderText = "Start Date";
                grid.Columns["end_date"].HeaderText = "End Date";
                grid.Columns["department_code"].HeaderText = "Department";
                grid.Columns["session_count"].HeaderText = "Sessions";
                grid.ClearSelection();
                lblStatus.Text = "Select an event to edit or delete. Each event creates its enabled morning/afternoon sessions for every event date.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = Database.GetFriendlyMessage(ex);
            }
        }

        private int SelectedEventId()
        {
            if (grid.CurrentRow == null || grid.CurrentRow.IsNewRow)
            {
                return 0;
            }
            return Convert.ToInt32(grid.CurrentRow.Cells["event_id"].Value);
        }

        private void AddEvent(object sender, EventArgs e)
        {
            using (EventEditor editor = new EventEditor(null))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    LoadEvents();
                }
            }
        }

        private void EditEvent(object sender, EventArgs e)
        {
            if (SelectedEventId() == 0)
            {
                MessageBox.Show("Select an event first.", "Edit Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (EventEditor editor = new EventEditor(grid.CurrentRow.DataBoundItem as DataRowView))
            {
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    LoadEvents();
                }
            }
        }

        private void DeleteEvent(object sender, EventArgs e)
        {
            int id = SelectedEventId();
            if (id == 0)
            {
                MessageBox.Show("Select an event first.", "Delete Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string name = grid.CurrentRow.Cells["event_name"].Value.ToString();
            if (MessageBox.Show("Delete " + name + " and its attendance records?", "Delete Event",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                EventService.Delete(id);
                LoadEvents();
            }
            catch (Exception ex)
            {
                PageControls.ShowError(ex, "Delete Event");
            }
        }
    }

    internal sealed class EventEditor : Form
    {
        private readonly int eventId;
        private readonly TextBox txtName;
        private readonly TextBox txtVenue;
        private readonly DateTimePicker dtpStart;
        private readonly DateTimePicker dtpEnd;
        private readonly ComboBox cmbDepartment;
        private readonly CheckBox chkLogout;
        private readonly CheckBox chkMorning;
        private readonly CheckBox chkAfternoon;
        private readonly DateTimePicker morningStart;
        private readonly DateTimePicker morningEnd;
        private readonly DateTimePicker afternoonStart;
        private readonly DateTimePicker afternoonEnd;

        public EventEditor(DataRowView row)
        {
            eventId = row == null ? 0 : Convert.ToInt32(row["event_id"]);
            Text = eventId == 0 ? "Create Event" : "Edit Event";
            ClientSize = new Size(540, 515);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Font = new Font("Cambria", 10F);

            AddLabel("Event name *", 20, 18);
            txtName = AddText(160, 18, 350);
            AddLabel("Venue", 20, 56);
            txtVenue = AddText(160, 56, 350);
            AddLabel("Start date *", 20, 94);
            dtpStart = AddDate(160, 94);
            AddLabel("End date *", 20, 132);
            dtpEnd = AddDate(160, 132);
            AddLabel("Department", 20, 170);
            cmbDepartment = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(160, 170),
                Size = new Size(350, 25)
            };
            DataTable departments = DepartmentService.GetAll();
            DataRow allDepartments = departments.NewRow();
            allDepartments["department_id"] = 0;
            allDepartments["department_code"] = "All Departments";
            departments.Rows.InsertAt(allDepartments, 0);
            cmbDepartment.DataSource = departments;
            cmbDepartment.ValueMember = "department_id";
            cmbDepartment.DisplayMember = "department_code";
            Controls.Add(cmbDepartment);

            chkLogout = AddCheck("Require students to log out", 160, 205);
            chkLogout.Checked = false;
            chkMorning = AddCheck("Morning session", 20, 248);
            morningStart = AddTime(160, 245, "08:00");
            morningEnd = AddTime(330, 245, "12:00");
            chkAfternoon = AddCheck("Afternoon session", 20, 285);
            afternoonStart = AddTime(160, 282, "13:00");
            afternoonEnd = AddTime(330, 282, "17:00");
            AddLabel("Start", 160, 315, 80);
            AddLabel("End", 330, 315, 80);

            Label note = new Label
            {
                Location = new Point(20, 350),
                Size = new Size(490, 48),
                ForeColor = Color.DimGray,
                Text = "A multi-day event creates the selected sessions for every day. Events with attendance or completed sessions cannot be rescheduled."
            };
            Controls.Add(note);

            Button save = PageControls.Button(eventId == 0 ? "CREATE EVENT" : "SAVE CHANGES", 270, 445, 130, true);
            save.Click += SaveEvent;
            Controls.Add(save);
            Button cancel = PageControls.Button("CANCEL", 410, 445, 100);
            cancel.Click += delegate { DialogResult = DialogResult.Cancel; Close(); };
            Controls.Add(cancel);

            if (row != null)
            {
                LoadExisting(row);
            }
        }

        private void LoadExisting(DataRowView row)
        {
            txtName.Text = row["event_name"].ToString();
            txtVenue.Text = row["venue"] == DBNull.Value ? "" : row["venue"].ToString();
            dtpStart.Value = Convert.ToDateTime(row["start_date"]);
            dtpEnd.Value = Convert.ToDateTime(row["end_date"]);
            chkLogout.Checked = Convert.ToInt32(row["requires_logout"]) == 1;
            chkMorning.Checked = Convert.ToInt32(row["morning_enabled"]) == 1;
            chkAfternoon.Checked = Convert.ToInt32(row["afternoon_enabled"]) == 1;
            morningStart.Value = DateTime.Today.Add((TimeSpan)row["morning_start"]);
            morningEnd.Value = DateTime.Today.Add((TimeSpan)row["morning_end"]);
            afternoonStart.Value = DateTime.Today.Add((TimeSpan)row["afternoon_start"]);
            afternoonEnd.Value = DateTime.Today.Add((TimeSpan)row["afternoon_end"]);
            if (row["department_id"] != DBNull.Value)
            {
                cmbDepartment.SelectedValue = Convert.ToInt32(row["department_id"]);
            }
        }

        private void SaveEvent(object sender, EventArgs e)
        {
            try
            {
                int department = PageControls.SelectedId(cmbDepartment);
                if (eventId == 0)
                {
                    EventService.Create(txtName.Text, txtVenue.Text, dtpStart.Value, dtpEnd.Value, department,
                        chkLogout.Checked, chkMorning.Checked, morningStart.Value.TimeOfDay, morningEnd.Value.TimeOfDay,
                        chkAfternoon.Checked, afternoonStart.Value.TimeOfDay, afternoonEnd.Value.TimeOfDay);
                }
                else
                {
                    EventService.Update(eventId, txtName.Text, txtVenue.Text, dtpStart.Value, dtpEnd.Value, department,
                        chkLogout.Checked, chkMorning.Checked, morningStart.Value.TimeOfDay, morningEnd.Value.TimeOfDay,
                        chkAfternoon.Checked, afternoonStart.Value.TimeOfDay, afternoonEnd.Value.TimeOfDay);
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                PageControls.ShowError(ex, "Save Event");
            }
        }

        private void AddLabel(string text, int x, int y, int width = 130)
        {
            Controls.Add(PageControls.Label(text, x, y, width));
        }

        private TextBox AddText(int x, int y, int width)
        {
            TextBox text = PageControls.TextBox(x, y, width);
            Controls.Add(text);
            return text;
        }

        private CheckBox AddCheck(string text, int x, int y)
        {
            CheckBox check = new CheckBox
            {
                AutoSize = true,
                Checked = true,
                Location = new Point(x, y),
                Text = text
            };
            Controls.Add(check);
            return check;
        }

        private DateTimePicker AddDate(int x, int y)
        {
            DateTimePicker picker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(x, y),
                Size = new Size(170, 25)
            };
            Controls.Add(picker);
            return picker;
        }

        private DateTimePicker AddTime(int x, int y, string value)
        {
            DateTimePicker picker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "hh:mm tt",
                ShowUpDown = true,
                Location = new Point(x, y),
                Size = new Size(150, 25),
                Value = DateTime.Today.Add(TimeSpan.Parse(value))
            };
            Controls.Add(picker);
            return picker;
        }
    }
}
