using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class DBViewer : Form
    {
        private readonly DataSet dataSet = new DataSet("Employees");
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
                                                 WHERE Id = ";

        private readonly string updateString = @"UPDATE Staff
                                                 SET Surname = @Surname, FirstName = @FirstName, Patronimic = @Patronimic,
                                                 IdPosition = @IdPosition, Address = @Address
                                                 WHERE Id = ";

        public DBViewer()
        {
            InitializeComponent();
        }

        private void ReloadDataGridView(SqlDataAdapter adapter)
        {
            dataSet.Tables["Employees"]?.Clear();
            adapter.Fill(dataSet, "Employees");
            dataGridView.DataSource = dataSet.Tables["Employees"];
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
                SqlDataAdapter adapter = new SqlDataAdapter(selectString, connection)
                {
                    SelectCommand = new SqlCommand(selectString, connection)
                };
                adapter.SelectCommand.Connection.Open();
                adapter.SelectCommand.ExecuteNonQuery();

                ReloadDataGridView(adapter);
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
                        SqlDataAdapter adapter = new SqlDataAdapter(selectString, connection);
                        if (radioButton == allRadioButton)
                        {
                            adapter.SelectCommand = new SqlCommand(selectString, connection);
                        }
                        else
                        {
                            adapter.SelectCommand = new SqlCommand(selectString, connection);
                            adapter.SelectCommand.CommandText += $"WHERE p.Id = {radioButton.TabIndex}";
                        }
                        adapter.SelectCommand.Connection.Open();
                        adapter.SelectCommand.ExecuteNonQuery();
                        ReloadDataGridView(adapter);
                    }
                }
            }
        }

        private void InsertRowInStaff()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(selectString, connection)
                {
                    InsertCommand = new SqlCommand(insertString, connection)
                };
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Surname", EmployeeDataWindow.Surname));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FirstName", EmployeeDataWindow.FirstName));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Patronimic", EmployeeDataWindow.Patronimic));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@IdPosition", EmployeeDataWindow.Position));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Address", EmployeeDataWindow.Address));

                adapter.InsertCommand.Connection.Open();
                adapter.InsertCommand.ExecuteNonQuery();
                ReloadDataGridView(adapter);
            }
        }

        private void DeleteRowInStaff()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(selectString, connection)
                {
                    DeleteCommand = new SqlCommand(deleteString, connection)
                };
                adapter.DeleteCommand.CommandText += $"{dataGridView.CurrentRow.Cells[0].Value}";
                adapter.DeleteCommand.Connection.Open();
                adapter.DeleteCommand.ExecuteNonQuery();
                ReloadDataGridView(adapter);
            }
        }

        private void UpdateRowInStaff()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(selectString, connection)
                {
                    UpdateCommand = new SqlCommand(updateString, connection)
                };
                adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Surname", EmployeeDataWindow.Surname));
                adapter.UpdateCommand.Parameters.Add(new SqlParameter("@FirstName", EmployeeDataWindow.FirstName));
                adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Patronimic", EmployeeDataWindow.Patronimic));
                adapter.UpdateCommand.Parameters.Add(new SqlParameter("@IdPosition", EmployeeDataWindow.Position));
                adapter.UpdateCommand.Parameters.Add(new SqlParameter("@Address", EmployeeDataWindow.Address));
                adapter.UpdateCommand.CommandText += $"{dataGridView.CurrentRow.Cells[0].Value}";

                adapter.UpdateCommand.Connection.Open();
                adapter.UpdateCommand.ExecuteNonQuery();
                ReloadDataGridView(adapter);
            }
        }

        public EmployeeData EmployeeDataWindow { get; set; } = null;

        private void CreateEmployeeDataWindow(string operation)
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
                if (operation == "Update")
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
                DialogResult result;
                switch (operation)
                {
                    case "Insert":
                        result = MessageBox.Show("Ви впевнені, що хочете додати новий запис?",
                                                 "Додавання",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                        InsertRowInStaff();
                        EmployeeDataWindow.Close();
                        break;
                    case "Update":
                        result = MessageBox.Show("Ви впевнені, що хочете редагувати запис?",
                                                 "Редагування",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                        UpdateRowInStaff();
                        EmployeeDataWindow.Close();
                        break;
                    default:
                        return;
                }
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            CreateEmployeeDataWindow("Insert");
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ви впевнені, що хочете видалити запис?",
                                                  "Видалення",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }
            DeleteRowInStaff();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            CreateEmployeeDataWindow("Update");
        }
    }
}
