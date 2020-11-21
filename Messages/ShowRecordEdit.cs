namespace DB_Staff_REM.Messages
{
    public class ShowRecordEdit
    {
        public Employee RecordData { get; }

        public ShowRecordEdit(Employee recordData)
        {
            RecordData = recordData;
        }
    }
}
