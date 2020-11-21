namespace DB_Staff_REM.Messages
{
    public class NewRowEditCompleted
    {
        public Employee RowData { get; }

        public NewRowEditCompleted(Employee rowData)
        {
            RowData = rowData;
        }
    }
}
