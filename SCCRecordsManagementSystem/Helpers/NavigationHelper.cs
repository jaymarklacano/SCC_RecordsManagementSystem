using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    // Moves between the main pages. Only one page is open at a time.
    // The login form stays alive (hidden) behind the pages, so the program keeps running.
    public static class NavigationHelper
    {
        // True while we close a page ourselves, so that closing it does not end the program.
        private static bool isSwitchingPage = false;

        // Closes the current page and opens the next page at the same place on the screen.
        public static void OpenPage(Form currentPage, Form nextPage)
        {
            Point location = currentPage.Location;

            if (!ClosePage(currentPage))
            {
                // The page refused to close (for example the user chose to stay and save first).
                nextPage.Dispose();
                return;
            }

            nextPage.StartPosition = FormStartPosition.Manual;
            nextPage.Location = location;
            nextPage.Show();
        }

        // Closes the current page and shows the login form again.
        public static void Logout(Form currentPage)
        {
            Point location = currentPage.Location;

            if (!ClosePage(currentPage))
            {
                return;
            }

            Session.Username = "";

            LoginForm loginForm = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
            if (loginForm == null)
            {
                loginForm = new LoginForm();
            }

            loginForm.ClearFields();
            loginForm.StartPosition = FormStartPosition.Manual;
            loginForm.Location = location;
            loginForm.Show();
        }

        // Every page calls this once. Closing a page with the X button or Alt+F4 closes the whole program.
        public static void ExitWhenClosed(Form page)
        {
            page.FormClosed += delegate
            {
                if (!isSwitchingPage)
                {
                    Application.Exit();
                }
            };
        }

        // Returns true when the page was really closed.
        private static bool ClosePage(Form page)
        {
            isSwitchingPage = true;
            try
            {
                page.Close();
            }
            finally
            {
                isSwitchingPage = false;
            }

            return page.IsDisposed;
        }
    }
}
