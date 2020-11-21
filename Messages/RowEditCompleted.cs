namespace DB_Staff_REM.Messages
{
    public class RowEditCompleted
    {
        public Employee RowData { get; }

        public RowEditCompleted(Employee rowData)
        {
            RowData = rowData;
        }
    }
}
