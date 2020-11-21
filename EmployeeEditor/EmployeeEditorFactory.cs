using DB_Staff_REM.EventHub;

namespace DB_Staff_REM.EmployeeEditor
{
    internal class EmployeeEditorFactory
    {
        private readonly IEventHub _eventHub;

        public EmployeeEditorFactory(IEventHub eventHub)
        {
            _eventHub = eventHub;
        }

        public EmployeeEditorPresenter CreateNew()
        {
            var presenter = new EmployeeEditorPresenter(_eventHub);
            var view = new EmployeeEditorView(presenter);
            presenter.BindView(view);
            return presenter;
        }
    }
}
