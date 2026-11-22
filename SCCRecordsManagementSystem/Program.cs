using System;
using System.Resources;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;

namespace SCCRecordsManagementSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Register before creating any windows. This also covers newly opened menus.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            lnkCreateAccount login;
            try
            {
                login = new lnkCreateAccount();
            }
            catch (Exception ex) when (ex is MissingManifestResourceException || ex is System.IO.FileNotFoundException ||
                ex is System.IO.FileLoadException || ex is BadImageFormatException || ex is TypeInitializationException)
            {
                Database.showError("The login screen could not be opened. Rebuild or reinstall the application.", ex);
                return;
            }

            Application.Run(login);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Do not attempt to run more UI or database operations after a fatal runtime error.
            if (e.Exception is OutOfMemoryException || e.Exception is AccessViolationException ||
                e.Exception is StackOverflowException || e.Exception is ThreadAbortException)
            {
                ExceptionDispatchInfo.Capture(e.Exception).Throw();
            }

            Database.showError("This action was interrupted. Refresh or reopen this screen. " +
                "If you were saving a record, check it before trying again.", e.Exception);
        }
    }
}
