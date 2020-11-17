using System.Data;
using System.Data.SqlClient;

namespace DB_Staff_REM
{
    class DBModel
    {
        private readonly DataTable _dataTable = new DataTable();
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                                      AttachDbFilename=C:\Users\maryn\source\repos\DB_Staff_REM\StaffREM.mdf;
                                                      Integrated Security=True";

        private readonly string _selectString = @"SELECT s.Id AS [ID], s.Surname AS [Прізвище], s.FirstName AS [Ім'я],
                                                  s.Patronimic AS [По батькові], p.Position AS [Посада], s.Address AS [Адреса]
                                                  FROM Staff s
                                                  INNER JOIN Positions p
                                                  ON s.IdPosition = p.Id ";

        private readonly string _insertString = @"INSERT INTO [Staff] ([Surname], [FirstName], [Patronimic], [IdPosition], [Address])
                                                  VALUES (@Surname, @FirstName, @Patronimic, @IdPosition, @Address)";

        private readonly string _deleteString = @"DELETE Staff
                                                  WHERE Id = @Id";

        private readonly string _updateString = @"UPDATE Staff
                                                  SET Surname = @Surname, FirstName = @FirstName, Patronimic = @Patronimic,
                                                  IdPosition = @IdPosition, Address = @Address
                                                  WHERE Id = @Id";

        private SqlCommand _currentSelectCommand;
        private readonly DBView _view;

        public DBModel(DBView view)
        {
            _view = view;
        }

        private DataTable ReloadDataGridView(SqlConnection connection)
        {
            _currentSelectCommand.Connection = connection;
            _dataTable.Clear();
            using (var dataReader = _currentSelectCommand.ExecuteReader())
            {
                _dataTable.Load(dataReader);
            }
            return _dataTable;
        }

        internal DataTable SelectRowsInStaff(object positionID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                _currentSelectCommand = connection.CreateCommand();
                _currentSelectCommand.CommandText = _selectString + $"WHERE p.Id = @Id";
                _currentSelectCommand.Parameters.AddWithValue("@Id", positionID);
                return ReloadDataGridView(connection);
            }
        }

        internal DataTable SelectRowsInStaff()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                _currentSelectCommand = connection.CreateCommand();
                _currentSelectCommand.CommandText = _selectString;
                return ReloadDataGridView(connection);
            }
        }

        internal DataTable InsertRowInStaff()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var insertCommand = new SqlCommand(_insertString, connection);
                insertCommand.Parameters.AddWithValue("@Surname", _view.EmployeeDataWindow.Surname);
                insertCommand.Parameters.AddWithValue("@FirstName", _view.EmployeeDataWindow.FirstName);
                insertCommand.Parameters.AddWithValue("@Patronimic", _view.EmployeeDataWindow.Patronimic);
                insertCommand.Parameters.AddWithValue("@IdPosition", _view.EmployeeDataWindow.Position);
                insertCommand.Parameters.AddWithValue("@Address", _view.EmployeeDataWindow.Address);

                insertCommand.ExecuteNonQuery();
                return ReloadDataGridView(connection);
            }
        }

        internal DataTable DeleteRowInStaff(string employeeID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var deleteCommand = connection.CreateCommand();
                deleteCommand.CommandText = _deleteString;

                deleteCommand.Parameters.AddWithValue("@Id", employeeID);

                deleteCommand.ExecuteNonQuery();
                return ReloadDataGridView(connection);
            }
        }

        internal DataTable UpdateRowInStaff(object employeeID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var updateCommand = connection.CreateCommand();
                updateCommand.CommandText = _updateString;

                updateCommand.Parameters.AddWithValue("@Surname", _view.EmployeeDataWindow.Surname);
                updateCommand.Parameters.AddWithValue("@FirstName", _view.EmployeeDataWindow.FirstName);
                updateCommand.Parameters.AddWithValue("@Patronimic", _view.EmployeeDataWindow.Patronimic);
                updateCommand.Parameters.AddWithValue("@IdPosition", _view.EmployeeDataWindow.Position);
                updateCommand.Parameters.AddWithValue("@Address", _view.EmployeeDataWindow.Address);
                updateCommand.Parameters.AddWithValue("@Id", employeeID);

                updateCommand.ExecuteNonQuery();
                return ReloadDataGridView(connection);
            }
        }
    }
}
