using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class EmployeeEdit : Form
    {
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

        public EmployeeEdit()
        {
            InitializeComponent();
        }
    }
}
