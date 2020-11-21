using System;
using System.Data;
using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class DBViewer : Form
    {
        private readonly DataTable _employeesTable = new DataTable();
        private readonly EmployeeStorage _employeeStorage;

        private int SelectedEmployeeId => Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);
        private Employee CurrentEmployee => new Employee
        {
            Surname = dataGridView.CurrentRow.Cells[1].Value.ToString(),
            FirstName = dataGridView.CurrentRow.Cells[2].Value.ToString(),
            Patronimic = dataGridView.CurrentRow.Cells[3].Value.ToString(),
            PositionTitle = dataGridView.CurrentRow.Cells[4].Value.ToString(),
            Address = dataGridView.CurrentRow.Cells[5].Value.ToString()
        };

        internal DBViewer(EmployeeStorage employeeStorage)
        {
            InitializeComponent();
            dataGridView.DataSource = _employeesTable;
            _employeeStorage = employeeStorage;
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
            ReloadDataGridView(storage => storage.SelectAll());
            SetHeaders();
        }

        private void SelectByPosition(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton.Checked)
                {
                    ReloadDataGridView(
                        radioButton == allRadioButton
                            ? (Func<EmployeeStorage, IDataReader>) (storage => storage.SelectAll())
                            : storage => storage.SelectByPosition(radioButton.TabIndex));
                }
            }
        }

        private void InsertRowInStaff()
        {
            _employeeStorage.Insert(EmployeeDataWindow.Employee);
            ReloadDataGridView(storage => storage.SelectAll());
        }

        private void DeleteRowInStaff()
        {
            _employeeStorage.Delete(SelectedEmployeeId);
            ReloadDataGridView(storage => storage.SelectAll());
        }

        private void UpdateRowInStaff()
        {
            _employeeStorage.Update(SelectedEmployeeId, EmployeeDataWindow.Employee);
            ReloadDataGridView(storage => storage.SelectAll());
        }

        public EmployeeEdit EmployeeDataWindow { get; set; } = null;

        private void CreateEmployeeDataWindow(EmployeeOperation operation)
        {
            if (EmployeeDataWindow == null)
            {
                EmployeeDataWindow = new EmployeeEdit();
                EmployeeDataWindow.Load += EmployeeDataWindowFormLoad;
                EmployeeDataWindow.FormClosing += EmployeeDataWindowFormClosing;
                EmployeeDataWindow.ButtonConfirm.Click += ButtonConfirmClick;
                EmployeeDataWindow.Show();
            }

            void EmployeeDataWindowFormLoad(object sender, EventArgs e)
            {
                if (operation == EmployeeOperation.Update)
                {
                    EmployeeDataWindow.Employee = CurrentEmployee;
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
                    case EmployeeOperation.Insert:
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
                    case EmployeeOperation.Update:
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
            CreateEmployeeDataWindow(EmployeeOperation.Insert);
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
            CreateEmployeeDataWindow(EmployeeOperation.Update);
        }

        private void ReloadDataGridView(Func<EmployeeStorage, IDataReader> refreshCommand)
        {
            _employeesTable.Clear();
            using (var employeesReader = refreshCommand(_employeeStorage))
            {
                _employeesTable.Load(employeesReader);
            }
        }
    }
}
