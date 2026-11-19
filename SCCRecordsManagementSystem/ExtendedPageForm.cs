using System.Drawing;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    public class ExtendedPageForm : Form
    {
        protected readonly Panel ContentPanel;


        public ExtendedPageForm(): this("Extended Page", SidebarPage.None)
        {

        }

        public ExtendedPageForm(string title, SidebarPage page)
        {
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Azure;
            ClientSize = new Size(1305, 619);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            Text = title;

            SidebarMenu menu = new SidebarMenu();
            menu.Location = new Point(0, 0);
            menu.Size = new Size(320, 619);
            menu.CurrentPage = page;
            Controls.Add(menu);

            Panel header = new Panel();
            header.BackColor = Color.FromArgb(73, 118, 159);
            header.Location = new Point(320, 0);
            header.Size = new Size(985, 50);
            Controls.Add(header);

            Label pageTitle = new Label();
            pageTitle.AutoSize = true;
            pageTitle.Font = new Font("Cambria", 14F, FontStyle.Bold);
            pageTitle.ForeColor = Color.GhostWhite;
            pageTitle.Location = new Point(24, 13);
            pageTitle.Text = title;
            header.Controls.Add(pageTitle);

            WindowButtons windowButtons = new WindowButtons();
            windowButtons.Location = new Point(891, 8);
            windowButtons.Size = new Size(90, 34);
            header.Controls.Add(windowButtons);

            ContentPanel = new Panel();
            ContentPanel.BackColor = Color.Azure;
            ContentPanel.Location = new Point(320, 50);
            ContentPanel.Size = new Size(985, 569);
            Controls.Add(ContentPanel);

            NavigationHelper.ExitWhenClosed(this);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ExtendedPageForm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "ExtendedPageForm";
            this.Load += new System.EventHandler(this.ExtendedPageForm_Load);
            this.ResumeLayout(false);

        }

        private void ExtendedPageForm_Load(object sender, System.EventArgs e)
        {

        }
    }

    internal static class PageControls
    {
        public static Label Label(string text, int x, int y, int width, int height = 24)
        {
            return new Label
            {
                AutoSize = false,
                Font = new Font("Cambria", 10F, FontStyle.Bold),
                ForeColor = Color.MidnightBlue,
                Location = new Point(x, y),
                Size = new Size(width, height),
                Text = text,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        public static TextBox TextBox(int x, int y, int width, string text = "")
        {
            return new TextBox
            {
                Font = new Font("Cambria", 10F),
                Location = new Point(x, y),
                Size = new Size(width, 25),
                Text = text
            };
        }

        public static ComboBox Combo(int x, int y, int width)
        {
            return new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Cambria", 10F),
                Location = new Point(x, y),
                Size = new Size(width, 26)
            };
        }

        public static Button Button(string text, int x, int y, int width, bool primary = false)
        {
            return new Button
            {
                BackColor = primary ? Color.FromArgb(73, 118, 159) : Color.FromArgb(189, 216, 233),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Cambria", 9.5F, FontStyle.Bold),
                ForeColor = primary ? Color.White : Color.Black,
                Location = new Point(x, y),
                Size = new Size(width, 32),
                Text = text,
                UseVisualStyleBackColor = false,
                Cursor = Cursors.Hand
            };
        }

        public static DataGridView Grid(int x, int y, int width, int height)
        {
            return new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(73, 118, 159),
                    ForeColor = Color.White,
                    Font = new Font("Cambria", 9.5F, FontStyle.Bold)
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Cambria", 9.5F),
                    SelectionBackColor = Color.FromArgb(189, 216, 233),
                    SelectionForeColor = Color.Black
                },
                EnableHeadersVisualStyles = false,
                Location = new Point(x, y),
                MultiSelect = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Size = new Size(width, height)
            };
        }

        public static void BindChoices(ComboBox combo, System.Data.DataTable table, string idColumn,
            string displayColumn, string firstLabel, int firstId = 0)
        {
            System.Data.DataRow all = table.NewRow();
            all[idColumn] = firstId;
            all[displayColumn] = firstLabel;
            table.Rows.InsertAt(all, 0);
            combo.DataSource = table;
            combo.ValueMember = idColumn;
            combo.DisplayMember = displayColumn;
            combo.SelectedIndex = 0;
        }

        public static int SelectedId(ComboBox combo)
        {
            if (combo.SelectedValue == null || combo.SelectedValue is System.Data.DataRowView)
            {
                return 0;
            }
            return System.Convert.ToInt32(combo.SelectedValue);
        }

        public static void ShowError(System.Exception exception, string title)
        {
            string message = exception is System.ArgumentException || exception is System.InvalidOperationException ||
                exception is System.UnauthorizedAccessException
                ? exception.Message : Database.GetFriendlyMessage(exception);
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
