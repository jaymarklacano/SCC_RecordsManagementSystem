using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectDatabase
{
    public partial class Information : Form
    {
        public Information()
        {
            InitializeComponent();
        }

        // Helper method to safely read and validate the typed ID number
        private bool TryGetSearchID(out int searchId)
        {
            searchId = 0;
            if (string.IsNullOrWhiteSpace(txtIDSearch.Text))
            {
                MessageBox.Show("Please enter an ID number first.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtIDSearch.Text.Trim(), out searchId))
            {
                MessageBox.Show("Please enter a valid numeric ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void btnDegree_Click(object sender, EventArgs e)
        {
            if (TryGetSearchID(out int searchId))
            {
                // Queries personal_info joined with educ_attianment and binds to dtgInformation
                ConnectDB.displayPersonalInfoWithEducation(dtgInformation, searchId);
            }

        }

        private void btnJob_Click(object sender, EventArgs e)
        {
            if (TryGetSearchID(out int searchId))
            {
                // Queries personal_info joined with job_info and binds to dtgInformation
                ConnectDB.displayPersonalInfoWithJob(dtgInformation, searchId);
            }
        }
    }
}
