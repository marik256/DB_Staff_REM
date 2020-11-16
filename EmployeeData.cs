using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class EmployeeData : Form
    {
        public string Surname
        {
            get => surnameTextBox.Text;
            set => surnameTextBox.Text = value;
        }
        public string FirstName
        {
            get => firstNameTextBox.Text;
            set => firstNameTextBox.Text = value;
        }
        public string Patronimic
        {
            get => patronimicTextBox.Text;
            set => patronimicTextBox.Text = value;
        }
        public int Position
        {
            get => positionComboBox.SelectedIndex + 1;
        }
        public string PositionItem
        {
            set => positionComboBox.SelectedItem = value;
        }
        public string Address
        {
            get => addressTextBox.Text;
            set => addressTextBox.Text = value;
        }
        public Button ButtonConfirm => confirmButton;

        public EmployeeData()
        {
            InitializeComponent();
        }
    }
}
