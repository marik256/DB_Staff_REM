using System.Data.SqlClient;

namespace DB_Staff_REM
{
    class QueryHandler
    {
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

        private readonly DataContainer _dataContainer;

        internal QueryHandler(DataContainer dataContainer)
        {
            _dataContainer = dataContainer;
        }

        internal void ExecuteSelectQueryInDataTable(int idPosition)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var selectCommand = new SqlCommand(_selectString, connection);

                if (idPosition > 0)
                {
                    selectCommand.CommandText += $"WHERE p.Id = @Id";
                    selectCommand.Parameters.AddWithValue("@Id", idPosition);
                }
                _dataContainer.OutputTabte.Clear();
                using (var dataReader = selectCommand.ExecuteReader())
                {
                    _dataContainer.OutputTabte.Load(dataReader);
                }
            }
        }

        internal void InsertRowInStaff()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var insertCommand = new SqlCommand(_insertString, connection);
                
                insertCommand.Parameters.AddWithValue("@Surname", _dataContainer.CurrentEmployee.surname);
                insertCommand.Parameters.AddWithValue("@FirstName", _dataContainer.CurrentEmployee.firstName);
                insertCommand.Parameters.AddWithValue("@Patronimic", _dataContainer.CurrentEmployee.patronimic);
                insertCommand.Parameters.AddWithValue("@IdPosition", _dataContainer.CurrentEmployee.idPosition);
                insertCommand.Parameters.AddWithValue("@Address", _dataContainer.CurrentEmployee.address);

                insertCommand.ExecuteNonQuery();
            }
        }

        internal void DeleteRowInStaff(int IdEmployee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var deleteCommand = new SqlCommand(_deleteString, connection);

                deleteCommand.Parameters.AddWithValue("@Id", IdEmployee);

                deleteCommand.ExecuteNonQuery();
            }
        }

        internal void UpdateRowInStaff(int IdEmployee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var updateCommand = new SqlCommand(_updateString, connection);

                updateCommand.Parameters.AddWithValue("@Surname", _dataContainer.CurrentEmployee.surname);
                updateCommand.Parameters.AddWithValue("@FirstName", _dataContainer.CurrentEmployee.firstName);
                updateCommand.Parameters.AddWithValue("@Patronimic", _dataContainer.CurrentEmployee.patronimic);
                updateCommand.Parameters.AddWithValue("@IdPosition", _dataContainer.CurrentEmployee.idPosition);
                updateCommand.Parameters.AddWithValue("@Address", _dataContainer.CurrentEmployee.address);
                updateCommand.Parameters.AddWithValue("@Id", IdEmployee);

                updateCommand.ExecuteNonQuery();
            }
        }
    }
}
