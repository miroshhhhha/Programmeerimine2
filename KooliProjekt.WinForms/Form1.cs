using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KooliProjekt.Models;
using KooliProjekt.WinForms;
using System.Drawing;
using System.Linq;

namespace KooliProjekt.WinForms
{
    public partial class Form1 : Form, IProjectView
    {
        private ProjectPresenter _presenter;
        private ApiClient _apiClient;

        public Form1()
        {
            InitializeComponent();
            _apiClient = new ApiClient();
            _presenter = new ProjectPresenter(this);
            InitializeDataGridView();
            LoadProjects_Click(null, null);
        }

        private async void LoadProjects_Click(object sender, EventArgs e)
        {
            try
            {
                var projects = await _apiClient.GetAsync<Project>("ProjectsApi");
                ProjectList.DataSource = projects;
            }
            catch (Exception ex)
            {
                ShowError($"Error loading projects: {ex.Message}");
            }
        }

        public void DisplayProjects(List<Project> projects)
        {
            ProjectList.DataSource = projects;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void AddButton_Click(object sender, EventArgs e)
        {
            var projectWindow = new KooliProjekt.WPF.ProjectWindow();
            var result = projectWindow.ShowDialog();
            if (result.HasValue)
            {
                await _presenter.AddProject(projectWindow.Project);
                LoadProjects_Click(null, null);
            }
        }

        private void InitializeDataGridView()
        {
            ProjectList.AutoGenerateColumns = false;
            ProjectList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ProjectList.AllowUserToAddRows = false;
            ProjectList.AllowUserToDeleteRows = false;
            ProjectList.ReadOnly = true;

            ProjectList.Columns.Clear();

            ProjectList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", ReadOnly = true, Width = 50 });
            ProjectList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Name", ReadOnly = true, Width = 200 });
            ProjectList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Start Date", ReadOnly = true, Width = 100, DefaultCellStyle = { Format = "dd.MM.yyyy" } });
            ProjectList.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EndDate", HeaderText = "End Date", ReadOnly = true, Width = 100, DefaultCellStyle = { Format = "dd.MM.yyyy" } });

            var actionsColumn = new DataGridViewButtonColumn
            {
                Name = "Actions",
                HeaderText = "Actions",
                Text = "Edit / Delete",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            ProjectList.Columns.Add(actionsColumn);
        }

        private async void ProjectList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == ProjectList.Columns["Actions"].Index)
            {
                if (ProjectList.Rows[e.RowIndex].DataBoundItem is Project selectedProject)
                {
                    var choice = MessageBox.Show($"What do you want to do with project '{selectedProject.Name}'?", "Project Actions", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (choice == DialogResult.Yes)
                    {
                        var projectWindow = new KooliProjekt.WPF.ProjectWindow(selectedProject);
                        var result = projectWindow.ShowDialog();
                        if (result.HasValue)
                        {
                            await _presenter.EditProject(projectWindow.Project);
                            LoadProjects_Click(null, null);
                        }
                    }
                    else if (choice == DialogResult.No)
                    {
                        var confirmResult = MessageBox.Show($"Are you sure you want to delete {selectedProject.Name}?", "Confirm Delete", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes)
                        {
                            await _presenter.DeleteProject(selectedProject.Id);
                            LoadProjects_Click(null, null);
                        }
                    }
                }
            }
        }
    }
}
