using DB_Staff_REM.EventHub;
using DB_Staff_REM.Messages;

namespace DB_Staff_REM.EmployeeEditor
{
    internal class EmployeeEditEventsHandler : IEventHandler<ShowNewRecordEdit>, IEventHandler<ShowRecordEdit>
    {
        private readonly EmployeeEditorFactory _editorFactory;

        public EmployeeEditEventsHandler(EmployeeEditorFactory editorFactory)
        {
            _editorFactory = editorFactory;
        }

        void IEventHandler<ShowNewRecordEdit>.Handle(ShowNewRecordEdit message)
        {
            var editor = _editorFactory.CreateNew();
            editor.EditNewEmployee();
        }

        void IEventHandler<ShowRecordEdit>.Handle(ShowRecordEdit message)
        {
            var editor = _editorFactory.CreateNew();
            editor.EditEmployee(message.RecordData);
        }
    }
}
