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
        private Label summary;
        public LoginHistory()
        {
            InitializeComponent();
            Program.PrepareWindow(this); Program.Grid(dataGridView1);
            label1.Text = "Login History";
            summary = Program.AddLabel(this, "", 12, 465, 590, 45);
            Program.AddButton(this, "Refresh", 610, 465, 100, delegate { LoadHistory(); });
            Program.AddButton(this, "Export CSV", 720, 465, 135, delegate {
                try { Database.RequireAdmin(); Program.ExportGrid(this, dataGridView1, "scc-logins.csv"); }
                catch (Exception ex) { Program.Error(ex); }
            });
            dateTimePicker1.Format = dateTimePicker2.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Value = DateTime.Today.AddDays(-30);
            button1.Click += delegate { Close(); };
            textBox1.TextChanged += delegate { LoadHistory(); };
            dateTimePicker1.ValueChanged += delegate { LoadHistory(); }; dateTimePicker2.ValueChanged += delegate { LoadHistory(); };
            Shown += delegate { LoadHistory(); };
        }

        private void LoadHistory()
        {
            try
            {
                Database.RequireAdmin();
                if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date) throw new ArgumentException("Choose a valid date range.");
                dataGridView1.DataSource = Database.GetData(@"SELECT username,logged_in,logged_out,
                    CASE WHEN logged_out IS NULL THEN 'No explicit logout' ELSE 'Logged out' END AS status FROM login_history
                    WHERE username LIKE @q AND logged_in>=@first AND logged_in<@after ORDER BY logged_in DESC",
                    new Dictionary<string, object> { { "@q", "%" + textBox1.Text.Trim() + "%" },
                        { "@first", dateTimePicker1.Value.Date }, { "@after", dateTimePicker2.Value.Date.AddDays(1) } });
                summary.Text = dataGridView1.Rows.Count == 0 ? "No successful logins match these filters." :
                    dataGridView1.Rows.Count + " login(s). No explicit logout does not necessarily mean the account is online.";
            }
            catch (Exception ex) { dataGridView1.DataSource = null; summary.Text = Database.ErrorMessage(ex); }
        }
    }
}
