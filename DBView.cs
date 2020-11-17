using System;
using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class DBView : Form
    {
        private readonly DBModel _model;

        public DBView()
        {
            InitializeComponent();
            _model = new DBModel(this);
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
            dataGridView.DataSource = _model.SelectRowsInStaff(null);
            SetHeaders();
        }

        private void SelectByPosition(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton.Checked)
                {
                    if (radioButton == allRadioButton)
                    {
                        dataGridView.DataSource = _model.SelectRowsInStaff(null);
                    }
                    else
                    {
                        dataGridView.DataSource = _model.SelectRowsInStaff(radioButton.TabIndex);
                    }
                }
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
                        dataGridView.DataSource = _model.InsertRowInStaff();
                        break;
                    
                    case Operations.Update:
                        if (GetYesOrNoDialogResult("Ви впевнені, що хочете редагувати запис?",
                                                   "Редагування")
                            == DialogResult.No)
                        {
                            return;
                        }
                        dataGridView.DataSource = _model.UpdateRowInStaff(dataGridView.CurrentRow.Cells[0].Value);
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
            dataGridView.DataSource = _model.DeleteRowInStaff(dataGridView.CurrentRow.Cells[0].Value.ToString());
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            CreateEmployeeDataWindow(Operations.Update);
        }
    }
}
