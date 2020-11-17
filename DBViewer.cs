using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class DBViewer : Form
    {
        private readonly DataTable dataTable = new DataTable();
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                                     AttachDbFilename=C:\Users\maryn\source\repos\DB_Staff_REM\StaffREM.mdf;
                                                     Integrated Security=True";

        private readonly string selectString = @"SELECT s.Id AS [ID], s.Surname AS [Прізвище], s.FirstName AS [Ім'я],
                                                 s.Patronimic AS [По батькові], p.Position AS [Посада], s.Address AS [Адреса]
                                                 FROM Staff s
                                                 INNER JOIN Positions p
                                                 ON s.IdPosition = p.Id ";

        private readonly string insertString = @"INSERT INTO [Staff] ([Surname], [FirstName], [Patronimic], [IdPosition], [Address])
                                                 VALUES (@Surname, @FirstName, @Patronimic, @IdPosition, @Address)";

        private readonly string deleteString = @"DELETE Staff
                                                 WHERE Id = @Id";

        private readonly string updateString = @"UPDATE Staff
                                                 SET Surname = @Surname, FirstName = @FirstName, Patronimic = @Patronimic,
                                                 IdPosition = @IdPosition, Address = @Address
                                                 WHERE Id = @Id";

        private SqlCommand currentSelectCommand;

        public DBViewer()
        {
            InitializeComponent();
        }

        private void ReloadDataGridView(SqlConnection connection)
        {
            currentSelectCommand.Connection = connection;
            dataTable.Clear();
            using (var dataReader = currentSelectCommand.ExecuteReader())
            {
                dataTable.Load(dataReader);
            }
            dataGridView.DataSource = dataTable;
        }

        private void SetHeaders()
        {
            dataGridView.Columns[0].Width = 30;
            dataGridView.Columns[1].Width = 105;
            dataGridView.Columns[2].Width = 105;
            dataGridView.Columns[3].Width = 105;
            dataGridView.Columns[4].Width = 120;
            dataGridView.Columns[5].Width = 380;
        }

        private void DBViewer_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                currentSelectCommand = connection.CreateCommand();
                currentSelectCommand.CommandText = selectString;
                ReloadDataGridView(connection);
            }
            SetHeaders();
        }

        private void SelectByPosition(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton.Checked)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        currentSelectCommand = connection.CreateCommand();
                        if (radioButton == allRadioButton)
                        {
                            currentSelectCommand.CommandText = selectString;
                        }
                        else
                        {
                            currentSelectCommand.CommandText = selectString + $"WHERE p.Id = @Id";
                            currentSelectCommand.Parameters.AddWithValue("@Id", radioButton.TabIndex);
                        }
                        ReloadDataGridView(connection);
                    }
                }
            }
        }

        private void InsertRowInStaff()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var insertCommand = new SqlCommand(insertString, connection);
                insertCommand.Parameters.AddWithValue("@Surname", EmployeeDataWindow.Surname);
                insertCommand.Parameters.AddWithValue("@FirstName", EmployeeDataWindow.FirstName);
                insertCommand.Parameters.AddWithValue("@Patronimic", EmployeeDataWindow.Patronimic);
                insertCommand.Parameters.AddWithValue("@IdPosition", EmployeeDataWindow.Position);
                insertCommand.Parameters.AddWithValue("@Address", EmployeeDataWindow.Address);

                insertCommand.ExecuteNonQuery();
                ReloadDataGridView(connection);
            }
        }

        private void DeleteRowInStaff()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var deleteCommand = connection.CreateCommand();
                deleteCommand.CommandText = deleteString;

                deleteCommand.Parameters.AddWithValue("@Id", dataGridView.CurrentRow.Cells[0].Value);
                
                deleteCommand.ExecuteNonQuery();
                ReloadDataGridView(connection);
            }
        }

        private void UpdateRowInStaff()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var updateCommand = connection.CreateCommand();
                updateCommand.CommandText = updateString;

                updateCommand.Parameters.AddWithValue("@Surname", EmployeeDataWindow.Surname);
                updateCommand.Parameters.AddWithValue("@FirstName", EmployeeDataWindow.FirstName);
                updateCommand.Parameters.AddWithValue("@Patronimic", EmployeeDataWindow.Patronimic);
                updateCommand.Parameters.AddWithValue("@IdPosition", EmployeeDataWindow.Position);
                updateCommand.Parameters.AddWithValue("@Address", EmployeeDataWindow.Address);
                updateCommand.Parameters.AddWithValue("@Id", dataGridView.CurrentRow.Cells[0].Value);

                updateCommand.ExecuteNonQuery();
                ReloadDataGridView(connection);
            }
        }

        private DialogResult GetYesOrNoDialogResult(string title, string message)
        {
            return MessageBox.Show(title, message,
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);
        }

        public EmployeeData EmployeeDataWindow { get; set; } = null;

        enum Operations
        {
            Insert,
            Update
        }

        private void CreateEmployeeDataWindow(Operations operation)
        {
            if (EmployeeDataWindow == null)
            {
                EmployeeDataWindow = new EmployeeData();
                EmployeeDataWindow.Load += EmployeeDataWindowFormLoad;
                EmployeeDataWindow.FormClosing += EmployeeDataWindowFormClosing;
                EmployeeDataWindow.ButtonConfirm.Click += ButtonConfirmClick;
                EmployeeDataWindow.Show();
            }

            void EmployeeDataWindowFormLoad(object sender, EventArgs e)
            {
                if (operation == Operations.Update)
                {
                    EmployeeDataWindow.Surname = dataGridView.CurrentRow.Cells[1].Value.ToString();
                    EmployeeDataWindow.FirstName = dataGridView.CurrentRow.Cells[2].Value.ToString();
                    EmployeeDataWindow.Patronimic = dataGridView.CurrentRow.Cells[3].Value.ToString();
                    EmployeeDataWindow.PositionItem = dataGridView.CurrentRow.Cells[4].Value.ToString();
                    EmployeeDataWindow.Address = dataGridView.CurrentRow.Cells[5].Value.ToString();
                }
            }

            void EmployeeDataWindowFormClosing(object sender, FormClosingEventArgs e)
            {
                EmployeeDataWindow.Load -= EmployeeDataWindowFormLoad;
                EmployeeDataWindow.FormClosing -= EmployeeDataWindowFormClosing;
                EmployeeDataWindow.ButtonConfirm.Click -= ButtonConfirmClick;
                EmployeeDataWindow = null;
            }

            void ButtonConfirmClick(object sender, EventArgs e)
            {
                switch (operation)
                {
                    case Operations.Insert:
                        if (GetYesOrNoDialogResult("Ви впевнені, що хочете додати новий запис?",
                                                   "Додавання") 
                            == DialogResult.No)
                        {
                            return;
                        }
                        InsertRowInStaff();
                        break;
                    
                    case Operations.Update:
                        if (GetYesOrNoDialogResult("Ви впевнені, що хочете редагувати запис?",
                                                   "Редагування")
                            == DialogResult.No)
                        {
                            return;
                        }
                        UpdateRowInStaff();
                        break;

                    default: return;
                }
                EmployeeDataWindow.Close();
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            CreateEmployeeDataWindow(Operations.Insert);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (GetYesOrNoDialogResult("Ви впевнені, що хочете видалити запис?",
                                       "Видалення")
                == DialogResult.No)
            {
                return;
            }
            DeleteRowInStaff();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            CreateEmployeeDataWindow(Operations.Update);
        }
    }
}
