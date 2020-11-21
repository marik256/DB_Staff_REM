using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Staff_REM
{

    internal class EmployeeStorage
    {
        private readonly string _connectionString;

        private const string selectAllCommandText = @"SELECT s.Id AS [ID], s.Surname AS [Прізвище], s.FirstName AS [Ім'я],
                                                             s.Patronimic AS [По батькові], p.Position AS [Посада], s.Address AS [Адреса]
                                                      FROM Staff s
                                                        INNER JOIN Positions p
                                                        ON s.IdPosition = p.Id";

        private const string selectByPositionCommandText = selectAllCommandText + " WHERE p.Id = @PositionId";

        private const string insertCommandText = @"INSERT INTO [Staff] ([Surname], [FirstName], [Patronimic], [IdPosition], [Address])
                                                   VALUES (@Surname, @FirstName, @Patronimic, @IdPosition, @Address)";

        private const string deleteCommandText = @"DELETE Staff
                                                   WHERE Id = @Id";

        private const string updateCommandText = @"UPDATE Staff
                                                   SET Surname = @Surname, FirstName = @FirstName, Patronimic = @Patronimic,
                                                       IdPosition = @IdPosition, Address = @Address
                                                   WHERE Id = @Id";

        public EmployeeStorage(string connectionString) => _connectionString = connectionString;

        public void Insert(Employee employee)
        {
            using (var connection = CreateOpennedConnection())
            {
                var insertCommand = new SqlCommand(insertCommandText, connection);
                SetupMutationCommandParameters(insertCommand, employee);
                insertCommand.ExecuteNonQuery();
            }
        }

        public void Update(int employeeId, Employee employee)
        {
            using (var connection = CreateOpennedConnection())
            {
                var updateCommand = new SqlCommand(updateCommandText, connection);
                updateCommand.Parameters.AddWithValue("Id", employeeId);
                SetupMutationCommandParameters(updateCommand, employee);
                updateCommand.ExecuteNonQuery();
            }
        }

        public void Delete(int employeeId)
        {
            using (var connection = CreateOpennedConnection())
            {
                var deleteCommand = new SqlCommand(deleteCommandText, connection);
                deleteCommand.Parameters.AddWithValue("Id", employeeId);
                deleteCommand.ExecuteNonQuery();
            }
        }

        public IDataReader SelectAll()
        {
            var connection = CreateOpennedConnection();
            var selectAllCommand = new SqlCommand(selectAllCommandText, connection);
            return selectAllCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public IDataReader SelectByPosition(int positionId)
        {
            var connection = CreateOpennedConnection();
            var selectByPositionCommand = new SqlCommand(selectByPositionCommandText, connection);
            selectByPositionCommand.Parameters.AddWithValue("PositionId", positionId);
            return selectByPositionCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        private void SetupMutationCommandParameters(SqlCommand command, Employee employee)
        {
            command.Parameters.Add(new SqlParameter("@Surname", employee.Surname));
            command.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
            command.Parameters.Add(new SqlParameter("@Patronimic", employee.Patronimic));
            command.Parameters.Add(new SqlParameter("@IdPosition", employee.PositionId));
            command.Parameters.Add(new SqlParameter("@Address", employee.Address));
        }

        private SqlConnection CreateOpennedConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
