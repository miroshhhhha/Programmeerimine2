namespace KooliProjekt.WinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ProjectList = new System.Windows.Forms.DataGridView();
            this.LoadButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Project Management";
            this.Name = "Form1";
            this.Controls.Add(this.ProjectList);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.AddButton);

            ((System.ComponentModel.ISupportInitialize)(this.ProjectList)).BeginInit();

            this.LoadButton.Location = new System.Drawing.Point(10, 10);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(100, 30);
            this.LoadButton.Text = "Load Projects";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadProjects_Click);

            this.AddButton.Location = new System.Drawing.Point(120, 10);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(100, 30);
            this.AddButton.Text = "Add Project";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);

            this.ProjectList.AllowUserToAddRows = false;
            this.ProjectList.AllowUserToDeleteRows = false;
            this.ProjectList.AutoGenerateColumns = false;
            this.ProjectList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProjectList.ReadOnly = true;
            this.ProjectList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProjectList.Location = new System.Drawing.Point(10, 70);
            this.ProjectList.Size = new System.Drawing.Size(760, 360);
            this.ProjectList.Name = "ProjectList";
            this.ProjectList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProjectList_CellContentClick);

            ((System.ComponentModel.ISupportInitialize)(this.ProjectList)).EndInit();

            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView ProjectList;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button AddButton;
    }
}
