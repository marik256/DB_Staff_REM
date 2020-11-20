using System;
using System.Data;
using System.Windows.Forms;

namespace DB_Staff_REM
{
    public partial class OutputForm : Form
    {
        private readonly DataContainer _dataContainer;

        private int currentRadioButtonTabIndex = 0;

        internal OutputForm()
        {
            InitializeComponent();
            _dataContainer = new DataContainer();
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
            _dataContainer.ExecuteSelectQuery(currentRadioButtonTabIndex);
            dataGridView.DataSource = _dataContainer.OutputTabte;
            SetHeaders();
        }

        private void SelectByPosition(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton.Checked)
                {
                    currentRadioButtonTabIndex = radioButton.TabIndex;
                    _dataContainer.ExecuteSelectQuery(currentRadioButtonTabIndex);
                    dataGridView.DataSource = _dataContainer.OutputTabte;
                }
            }
        }

        private DialogResult GetYesOrNoDialogResult(string title, string message)
        {
            return MessageBox.Show(title, message,
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            _dataContainer.ShowInputFormForInsert(currentRadioButtonTabIndex);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            _dataContainer.ShowInputFormForUpdate(((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row,
                                                  currentRadioButtonTabIndex);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (GetYesOrNoDialogResult("Ви впевнені, що хочете видалити запис?",
                                       "Видалення")
                == DialogResult.No)
            {
                return;
            }
            _dataContainer.ExecuteDeleteQuery((int)dataGridView.CurrentRow.Cells[0].Value,
                                              currentRadioButtonTabIndex);
        }
    }
}
