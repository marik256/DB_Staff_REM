using DB_Staff_REM.EventHub;
using DB_Staff_REM.Messages;

namespace DB_Staff_REM.EmployeeEditor
{
    public class EmployeeEditorPresenter
    {
        private readonly IEventHub _eventHub;
        private EmployeeEditorView _view;

        public EmployeeEditorPresenter(IEventHub eventHub)
        {
            _eventHub = eventHub;
        }

        public void BindView(EmployeeEditorView view)
        {
            _view = view;
            view.Employee = new Employee();
        }

        internal void NewRowEditCompleted()
        {
            var employee = _view.Employee;
            _view.Close();
            _eventHub.Publish(new NewRowEditCompleted(employee));
        }

        internal void RowEditCompleted()
        {
            var employee = _view.Employee;
            _view.Close();
            _eventHub.Publish(new RowEditCompleted(employee));
        }

        public void EditNewEmployee()
        {
            _view.EditorMode = EmployeeOperation.Insert;
            _view.Employee = new Employee();
            _view.Text = "Додати працівника";
            _view.Show();
        }

        public void EditEmployee(Employee employee)
        {
            _view.EditorMode = EmployeeOperation.Update;
            _view.Employee = employee;
            _view.Text = "Редагувати працівника";
            _view.Show();
        }
    }
}
