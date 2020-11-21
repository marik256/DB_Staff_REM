using DB_Staff_REM.EmployeeEditor;
using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class EmployeeEditorView : Form
    {
        private readonly EmployeeEditorPresenter _presenter;

        internal EmployeeOperation EditorMode { get; set; }

        public Employee Employee
        {
            get
            {
                return new Employee
                {
                    Surname = surnameTextBox.Text,
                    FirstName = firstNameTextBox.Text,
                    Patronimic = patronimicTextBox.Text,
                    PositionId = positionComboBox.SelectedIndex + 1,
                    PositionTitle = positionComboBox.SelectedItem?.ToString(),
                    Address = addressTextBox.Text
                };
            }

            set
            {
                surnameTextBox.Text = value.Surname;
                firstNameTextBox.Text = value.FirstName;
                patronimicTextBox.Text = value.Patronimic;
                positionComboBox.SelectedItem = value.PositionTitle;
                addressTextBox.Text = value.Address;
            }
        }


        public Button ButtonConfirm => confirmButton;

        public EmployeeEditorView(EmployeeEditorPresenter presenter)
        {
            InitializeComponent();
            confirmButton.Click += ConfirmButton_Click;
            _presenter = presenter;
        }

        private void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            switch (EditorMode)
            {
                case EmployeeOperation.Insert:
                    var result = MessageBox.Show("Ви впевнені, що хочете додати новий запис?",
                                             "Додавання",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    _presenter.NewRowEditCompleted();
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
                    _presenter.RowEditCompleted();
                    break;
                default:
                    return;
            }
        }
    }
}
