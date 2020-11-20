using System.Data;

namespace DB_Staff_REM
{
    class DataContainer
    {
        private InputForm _inputForm;
        private readonly QueryHandler _queryHandler;
        
        internal Employee CurrentEmployee { get; set; }

        internal int CurrentIdEmloyee { get; set; }

        internal int CurrentIdPosition { get; set; }

        internal DataTable OutputTabte { get; set; }

        internal DataContainer()
        {
            _queryHandler = new QueryHandler(this);
            OutputTabte = new DataTable();
        }

        internal void ExecuteSelectQuery(int idPosition)
        {
            _queryHandler.ExecuteSelectQueryInDataTable(idPosition);
        }

        internal void ExecuteInsertQuery()
        {
            _queryHandler.InsertRowInStaff();
            _queryHandler.ExecuteSelectQueryInDataTable(CurrentIdPosition);
        }

        internal void ExecuteUpdateQuery()
        {
            _queryHandler.UpdateRowInStaff(CurrentIdEmloyee);
            _queryHandler.ExecuteSelectQueryInDataTable(CurrentIdPosition);
            
        }

        internal void ExecuteDeleteQuery(int IdEmployee, int IdPosition)
        {
            _queryHandler.DeleteRowInStaff(IdEmployee);
            _queryHandler.ExecuteSelectQueryInDataTable(IdPosition);
        }

        internal void ShowInputFormForInsert(int IdPosition)
        {
            _inputForm = new InputForm(this);
            _inputForm.ShowInputForm(Operations.Insert);
            CurrentIdPosition = IdPosition;
        }

        internal void ShowInputFormForUpdate(DataRow currentRow, int IdPosition)
        {
            _inputForm = new InputForm(this);
            FillCurrentEmployeeFromRow(currentRow);
            _inputForm.ShowInputForm(Operations.Update);
            CurrentIdPosition = IdPosition;
        }

        internal void FillCurrentEmployeeFromRow(DataRow row)
        {
            CurrentIdEmloyee = (int)row.ItemArray[0];
            CurrentEmployee = new Employee
            (
                row.ItemArray[1].ToString(),
                row.ItemArray[2].ToString(),
                row.ItemArray[3].ToString(),
                _inputForm.GetPositionIdByValue(row.ItemArray[4]),
                row.ItemArray[5].ToString()
            );
        }
    }

    internal struct Employee
    {
        internal string surname, firstName, patronimic, address;
        internal int idPosition;

        internal Employee(string surname, string firstName, string patronimic, int idPosition, string address)
        {
            this.surname = surname;
            this.firstName = firstName;
            this.patronimic = patronimic;
            this.idPosition = idPosition;
            this.address = address;
        }
    }

    enum Operations
    {
        Insert,
        Update
    }
}
