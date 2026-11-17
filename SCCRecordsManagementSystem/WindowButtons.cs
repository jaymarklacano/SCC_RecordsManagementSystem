using System;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Minimize and close buttons. Our forms have no title bar, so each page shows these two buttons.
    public partial class WindowButtons : UserControl
    {
        public WindowButtons()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            FindForm().WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
    }
}
