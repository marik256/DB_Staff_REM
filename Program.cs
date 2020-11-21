using DB_Staff_REM.EmployeeEditor;
using DB_Staff_REM.EmployeeRegistry;
using DB_Staff_REM.EventHub;
using DB_Staff_REM.Messages;
using System;
using System.Windows.Forms;

namespace DB_Staff_REM
{
    static class Program
    {
        private const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                                  AttachDbFilename=C:\Users\maryn\source\repos\DB_Staff_REM\StaffREM.mdf;
                                                  Integrated Security=True";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ConstructRegistryView());
        }

        private static EmployeeRegistryView ConstructRegistryView()
        {
            var eventHub = new InProcessEventHub();
            var registryModel = new EmployeeRegistryModel();
            var dataStorage = new EmployeeStorage(connectionString);
            var registryPresenter = new EmployeeRegistryPresenter(eventHub, registryModel, dataStorage);
            var editorFactory = new EmployeeEditorFactory(eventHub);
            var editEventsHandler = new EmployeeEditEventsHandler(editorFactory);

            eventHub.Subscribe<NewRowEditCompleted, EmployeeRegistryPresenter>(registryPresenter);
            eventHub.Subscribe<RowEditCompleted, EmployeeRegistryPresenter>(registryPresenter);
            eventHub.Subscribe<ShowNewRecordEdit, EmployeeEditEventsHandler>(editEventsHandler);
            eventHub.Subscribe<ShowRecordEdit, EmployeeEditEventsHandler>(editEventsHandler);

            var registryView = new EmployeeRegistryView(registryPresenter);
            return registryView;
        }
    }
}
