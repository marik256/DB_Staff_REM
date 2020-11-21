using System.Data;

namespace DB_Staff_REM.EmployeeRegistry
{
    public class EmployeeRegistryModel
    {
        public DataTable Employees { get; } = new DataTable(); 
    }
}
