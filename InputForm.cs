using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class InputForm : Form
    {
        private readonly DataContainer _dataContainer;
        private bool _inputFormIsRunning;
        private Operations _operation;

        internal InputForm(DataContainer dataContainer)
        {
            InitializeComponent();
            _dataContainer = dataContainer;
            _inputFormIsRunning = false;
        }

        private void FillInputFormFields()
        {
            surnameTextBox.Text = _dataContainer.CurrentEmployee.surname;
            firstNameTextBox.Text = _dataContainer.CurrentEmployee.firstName;
            patronimicTextBox.Text = _dataContainer.CurrentEmployee.patronimic;
            addressTextBox.Text = _dataContainer.CurrentEmployee.address;
            positionComboBox.SelectedIndex = _dataContainer.CurrentEmployee.idPosition;
        }

        internal void ShowInputForm(Operations operation)
        {
            if (_inputFormIsRunning)
            {
                return;
            }
            if (operation == Operations.Update)
            {
                FillInputFormFields();
            }
            _operation = operation;
            _inputFormIsRunning = true;
            Show();
        }

        private void CloseInputForm()
        {
            _inputFormIsRunning = false;
            MoveCompletedFieldsIntoContainer();
            Close();
        }

        private void MoveCompletedFieldsIntoContainer()
        {
            _dataContainer.CurrentEmployee = new Employee
            (
                surnameTextBox.Text,
                firstNameTextBox.Text,
                patronimicTextBox.Text,
                positionComboBox.SelectedIndex + 1,
                addressTextBox.Text
            );
        }

        internal int GetPositionIdByValue(object obj)
        {
            return positionComboBox.Items.IndexOf(obj);
        }

        private DialogResult GetYesOrNoDialogResult(string title, string message)
        {
            return MessageBox.Show(title, message,
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);
        }

        private void ConfirmAction()
        {
            switch (_operation)
            {
                case Operations.Insert:
                    if (GetYesOrNoDialogResult("Ви впевнені, що хочете додати новий запис?",
                                               "Додавання")
                        == DialogResult.No)
                    {
                        return;
                    }

                    CloseInputForm();
                    _dataContainer.ExecuteInsertQuery();
                    break;

                case Operations.Update:
                    if (GetYesOrNoDialogResult("Ви впевнені, що хочете редагувати запис?",
                                               "Редагування")
                        == DialogResult.No)
                    {
                        return;
                    }

                    CloseInputForm();
                    _dataContainer.ExecuteUpdateQuery();
                    break;

                default: return;
            }
        }

        private void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            ConfirmAction();
        }
    }
}
