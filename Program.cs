using System;
using System.Windows.Forms;

namespace DB_Staff_REM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                              AttachDbFilename=C:\Users\maryn\source\repos\DB_Staff_REM\StaffREM.mdf;
                                              Integrated Security=True";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DBViewer(new EmployeeStorage(connectionString)));
        }
    }
}
