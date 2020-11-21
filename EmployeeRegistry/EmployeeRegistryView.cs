using DB_Staff_REM.EmployeeRegistry;
using System;
using System.Data;
using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class EmployeeRegistryView : Form
    {
        private readonly EmployeeRegistryPresenter _presenter;

        public int CurrentEmployeeId => Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);
        public string CurrentSurname => dataGridView.CurrentRow.Cells[1].Value.ToString();
        public string CurrentFirstName => dataGridView.CurrentRow.Cells[2].Value.ToString();
        public string CurrentPatronimic => dataGridView.CurrentRow.Cells[3].Value.ToString();
        public string CurrentPositionTitle => dataGridView.CurrentRow.Cells[4].Value.ToString();
        public string CurrentAddress => dataGridView.CurrentRow.Cells[5].Value.ToString();

        internal EmployeeRegistryView(EmployeeRegistryPresenter presenter)
        {
            InitializeComponent();
            _presenter = presenter;
            _presenter.BindView(this);
        }


        public void AttachGridCollection(DataTable table) => dataGridView.DataSource = table;

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
            _presenter.FillAllEmployees();
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
                        _presenter.FillAllEmployees();
                    }
                    else
                    {
                        _presenter.FillEmployeesByPosition(radioButton.TabIndex);
                    }
                }
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            _presenter.ShowInsertEmployeeDialog();
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
            _presenter.DeleteCurrentEmployee();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            _presenter.ShowUpdateEmployeeDialog();
        }
    }
}
