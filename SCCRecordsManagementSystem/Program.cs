using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Globalization;

namespace SCCRecordsManagementSystem
{
    internal static class Program
    {
        internal static Form LoginWindow;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginWindow = new lnkCreateAccount();
            Application.Run(LoginWindow);
        }

        internal static void Error(Exception error)
        {
            MessageBox.Show(Database.ErrorMessage(error), "SCC Records", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        internal static Button AddButton(Control parent, string text, int x, int y, int width, EventHandler click)
        {
            Button button = new Button { Text = text, Location = new Point(x, y), Size = new Size(width, 30),
                Font = new Font("Cambria", 10), BackColor = Color.LightSteelBlue, FlatStyle = FlatStyle.Flat };
            button.Click += click;
            parent.Controls.Add(button);
            button.BringToFront();
            return button;
        }

        internal static Label AddLabel(Control parent, string text, int x, int y, int width, int height = 25)
        {
            Label label = new Label { Text = text, Location = new Point(x, y), Size = new Size(width, height),
                Font = new Font("Cambria", 10), ForeColor = Color.MidnightBlue };
            parent.Controls.Add(label);
            label.BringToFront();
            return label;
        }

        internal static void Grid(DataGridView grid)
        {
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grid.BackgroundColor = Color.White;
            grid.AllowUserToOrderColumns = true;
            grid.DefaultCellStyle.NullValue = "-";
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            grid.DataBindingComplete += delegate {
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    column.HeaderText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(column.Name.Replace('_', ' '));
                    if (column.Name == "student_number") column.HeaderText = "Student ID";
                    if (column.Name == "full_name") column.HeaderText = "Full Name";
                    if (column.Name == "time_in") column.HeaderText = "Log In";
                    if (column.Name == "time_out") column.HeaderText = "Log Out";
                    if (column.ValueType == typeof(DateTime))
                        column.DefaultCellStyle.Format = column.Name.EndsWith("_date") ? "MMM d, yyyy" : "MMM d, yyyy h:mm tt";
                    column.MinimumWidth = 80;
                }
                grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            };
        }

        internal static void ExportGrid(Form owner, DataGridView grid, string filename)
        {
            if (grid.Rows.Count == 0) throw new InvalidOperationException("There are no records to export.");
            using (SaveFileDialog save = new SaveFileDialog { Filter = "CSV file|*.csv", FileName = filename })
            {
                if (save.ShowDialog(owner) != DialogResult.OK) return;
                using (StreamWriter writer = new StreamWriter(save.FileName, false, new UTF8Encoding(true)))
                {
                    var columns = grid.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).OrderBy(c => c.DisplayIndex).ToArray();
                    writer.WriteLine(string.Join(",", columns.Select(c => Csv(c.HeaderText))));
                    foreach (DataGridViewRow row in grid.Rows)
                        writer.WriteLine(string.Join(",", columns.Select(c => Csv(Convert.ToString(row.Cells[c.Index].FormattedValue)))));
                }
                MessageBox.Show("Records exported.", "SCC Records");
            }
        }

        internal static string Csv(string value)
        {
            string start = value.TrimStart();
            if ((start.Length > 0 && "=+-@".Contains(start[0])) || value.StartsWith("\t") || value.StartsWith("\r"))
                value = "'" + value;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        internal static void Choices(ComboBox combo, DataTable table, string id, string name, string all = null)
        {
            if (all != null)
            {
                DataRow row = table.NewRow();
                row[id] = 0;
                row[name] = all;
                table.Rows.InsertAt(row, 0);
            }
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.DisplayMember = name;
            combo.ValueMember = id;
            combo.DataSource = table;
        }

        internal static int SelectedId(ComboBox combo)
        {
            return combo.SelectedValue == null || combo.SelectedValue is DataRowView
                ? 0 : Convert.ToInt32(combo.SelectedValue);
        }

        internal static DataRow SelectedRow(DataGridView grid)
        {
            DataRowView row = grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as DataRowView;
            if (row == null) throw new InvalidOperationException("Select a record first.");
            return row.Row;
        }

        internal static void PrepareWindow(Form form, bool page = false)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            if (form.FormBorderStyle == FormBorderStyle.None)
            {
                Button close = AddButton(form, "X", form.ClientSize.Width - 38, 4, 32, delegate { form.Close(); });
                close.BringToFront();
            }
            if (page)
                form.FormClosed += delegate { if (form.Tag == null) Application.Exit(); };
        }

        internal static void OpenPage(Form current, string permission, Func<Form> create)
        {
            try
            {
                Database.Require(permission);
                Form next = create();
                next.Show();
                current.Tag = "Navigation";
                current.Close();
            }
            catch (Exception ex) { Error(ex); }
        }

        internal static void Logout(Form current)
        {
            if (MessageBox.Show("Log out?", "SCC Records", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                Database.Execute("UPDATE login_history SET logged_out=NOW() WHERE login_id=@id",
                    new Dictionary<string, object> { { "@id", Database.LoginId } });
                Database.AccountId = 0; Database.Role = ""; Database.Username = ""; Database.LoginId = 0;
                LoginWindow.Show();
                current.Tag = "Logout";
                current.Close();
            }
            catch (Exception ex) { Error(ex); }
        }
    }
}
