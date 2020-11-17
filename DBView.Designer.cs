namespace DB_Staff_REM
{
    partial class DBView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.allRadioButton = new System.Windows.Forms.RadioButton();
            this.driverRadioButton = new System.Windows.Forms.RadioButton();
            this.locksmithRadioButton = new System.Windows.Forms.RadioButton();
            this.technicianRadioButton = new System.Windows.Forms.RadioButton();
            this.controllerRadioButton = new System.Windows.Forms.RadioButton();
            this.electricianRadioButton = new System.Windows.Forms.RadioButton();
            this.engineerRadioButton = new System.Windows.Forms.RadioButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.updateButton = new System.Windows.Forms.Button();
            this.insertButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.allRadioButton);
            this.groupBox.Controls.Add(this.driverRadioButton);
            this.groupBox.Controls.Add(this.locksmithRadioButton);
            this.groupBox.Controls.Add(this.technicianRadioButton);
            this.groupBox.Controls.Add(this.controllerRadioButton);
            this.groupBox.Controls.Add(this.electricianRadioButton);
            this.groupBox.Controls.Add(this.engineerRadioButton);
            this.groupBox.Location = new System.Drawing.Point(945, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(200, 268);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Посади";
            // 
            // allRadioButton
            // 
            this.allRadioButton.AutoSize = true;
            this.allRadioButton.Checked = true;
            this.allRadioButton.Location = new System.Drawing.Point(6, 25);
            this.allRadioButton.Name = "allRadioButton";
            this.allRadioButton.Size = new System.Drawing.Size(99, 21);
            this.allRadioButton.TabIndex = 0;
            this.allRadioButton.TabStop = true;
            this.allRadioButton.Text = "Усі посади";
            this.allRadioButton.UseVisualStyleBackColor = true;
            this.allRadioButton.CheckedChanged += new System.EventHandler(this.SelectByPosition);
            // 
            // driverRadioButton
            // 
            this.driverRadioButton.AutoSize = true;
            this.driverRadioButton.Location = new System.Drawing.Point(6, 235);
            this.driverRadioButton.Name = "driverRadioButton";
            this.driverRadioButton.Size = new System.Drawing.Size(65, 21);
            this.driverRadioButton.TabIndex = 6;
            this.driverRadioButton.Text = "Водій";
            this.driverRadioButton.UseVisualStyleBackColor = true;
            this.driverRadioButton.CheckedChanged += new System.EventHandler(this.SelectByPosition);
            // 
            // locksmithRadioButton
            // 
            this.locksmithRadioButton.AutoSize = true;
            this.locksmithRadioButton.Location = new System.Drawing.Point(6, 200);
            this.locksmithRadioButton.Name = "locksmithRadioButton";
            this.locksmithRadioButton.Size = new System.Drawing.Size(79, 21);
            this.locksmithRadioButton.TabIndex = 5;
            this.locksmithRadioButton.Text = "Слюсар";
            this.locksmithRadioButton.UseVisualStyleBackColor = true;
            this.locksmithRadioButton.CheckedChanged += new System.EventHandler(this.SelectByPosition);
            // 
            // technicianRadioButton
            // 
            this.technicianRadioButton.AutoSize = true;
            this.technicianRadioButton.Location = new System.Drawing.Point(6, 165);
            this.technicianRadioButton.Name = "technicianRadioButton";
            this.technicianRadioButton.Size = new System.Drawing.Size(70, 21);
            this.technicianRadioButton.TabIndex = 4;
            this.technicianRadioButton.Text = "Технік";
            this.technicianRadioButton.UseVisualStyleBackColor = true;
            this.technicianRadioButton.CheckedChanged += new System.EventHandler(this.SelectByPosition);
            // 
            // controllerRadioButton
            // 
            this.controllerRadioButton.AutoSize = true;
            this.controllerRadioButton.Location = new System.Drawing.Point(6, 130);
            this.controllerRadioButton.Name = "controllerRadioButton";
            this.controllerRadioButton.Size = new System.Drawing.Size(101, 21);
            this.controllerRadioButton.TabIndex = 3;
            this.controllerRadioButton.Text = "Контролер";
            this.controllerRadioButton.UseVisualStyleBackColor = true;
            this.controllerRadioButton.CheckedChanged += new System.EventHandler(this.SelectByPosition);
            // 
            // electricianRadioButton
            // 
            this.electricianRadioButton.AutoSize = true;
            this.electricianRadioButton.Location = new System.Drawing.Point(6, 95);
            this.electricianRadioButton.Name = "electricianRadioButton";
            this.electricianRadioButton.Size = new System.Drawing.Size(132, 21);
            this.electricianRadioButton.TabIndex = 2;
            this.electricianRadioButton.Text = "Електромонтер";
            this.electricianRadioButton.UseVisualStyleBackColor = true;
            this.electricianRadioButton.CheckedChanged += new System.EventHandler(this.SelectByPosition);
            // 
            // engineerRadioButton
            // 
            this.engineerRadioButton.AutoSize = true;
            this.engineerRadioButton.Location = new System.Drawing.Point(6, 60);
            this.engineerRadioButton.Name = "engineerRadioButton";
            this.engineerRadioButton.Size = new System.Drawing.Size(81, 21);
            this.engineerRadioButton.TabIndex = 1;
            this.engineerRadioButton.Text = "Інженер";
            this.engineerRadioButton.UseVisualStyleBackColor = true;
            this.engineerRadioButton.CheckedChanged += new System.EventHandler(this.SelectByPosition);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(900, 500);
            this.dataGridView.TabIndex = 2;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(945, 332);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(200, 34);
            this.updateButton.TabIndex = 3;
            this.updateButton.Text = "Редагувати";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // insertButton
            // 
            this.insertButton.Location = new System.Drawing.Point(945, 394);
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size(200, 34);
            this.insertButton.TabIndex = 4;
            this.insertButton.Text = "Додати запис";
            this.insertButton.UseVisualStyleBackColor = true;
            this.insertButton.Click += new System.EventHandler(this.InsertButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(945, 456);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(200, 34);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Видалити";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // DBViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 523);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.insertButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBox);
            this.Name = "DBViewer";
            this.Text = "Працівники РЕМ";
            this.Load += new System.EventHandler(this.DBViewer_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton engineerRadioButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.RadioButton driverRadioButton;
        private System.Windows.Forms.RadioButton locksmithRadioButton;
        private System.Windows.Forms.RadioButton technicianRadioButton;
        private System.Windows.Forms.RadioButton controllerRadioButton;
        private System.Windows.Forms.RadioButton electricianRadioButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.RadioButton allRadioButton;
    }
}

