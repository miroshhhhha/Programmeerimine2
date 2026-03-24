using System;
using System.Collections.Generic;
using System.Windows;
using KooliProjekt.Models;

namespace KooliProjekt.WPF
{
    public partial class ProjectTaskWindow : Window
    {
        private readonly ApiClient _apiClient = new ApiClient();
        public ProjectTask Task { get; private set; }

        public ProjectTaskWindow()
        {
            InitializeComponent();
            Task = new ProjectTask();
            LoadData();
        }

        public ProjectTaskWindow(ProjectTask task)
        {
            InitializeComponent();
            Task = new ProjectTask
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                Deadline = task.Deadline,
                ProjectId = task.ProjectId,
                TeamMemberId = task.TeamMemberId
            };

            TitleTextBox.Text = Task.Title;
            DescriptionTextBox.Text = Task.Description;
            StatusTextBox.Text = Task.Status;
            PriorityTextBox.Text = Task.Priority;
            DeadlinePicker.SelectedDate = Task.Deadline;

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var projects = await _apiClient.GetAsync<Project>("ProjectsApi");
                var members = await _apiClient.GetAsync<TeamMember>("TeamMembersApi");

                ProjectComboBox.ItemsSource = projects;
                MemberComboBox.ItemsSource = members;

                ProjectComboBox.SelectedValue = Task.ProjectId;
                MemberComboBox.SelectedValue = Task.TeamMemberId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading related data: {ex.Message}");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Title is required.");
                return;
            }

            if (ProjectComboBox.SelectedValue == null)
            {
                MessageBox.Show("Project is required.");
                return;
            }

            Task.Title = TitleTextBox.Text;
            Task.Description = DescriptionTextBox.Text;
            Task.Status = StatusTextBox.Text;
            Task.Priority = PriorityTextBox.Text;
            Task.Deadline = DeadlinePicker.SelectedDate;
            Task.ProjectId = (int)ProjectComboBox.SelectedValue;
            Task.TeamMemberId = (int?)MemberComboBox.SelectedValue;

            DialogResult = true;
        }
    }
}
