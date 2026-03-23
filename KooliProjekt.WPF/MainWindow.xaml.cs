using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using KooliProjekt.Models;

namespace KooliProjekt.WPF
{
    public partial class MainWindow : Window
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoadProjects_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var projects = await _apiClient.GetAsync<Project>("ProjectsApi");
                ProjectsGrid.ItemsSource = projects;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading projects: {ex.Message}");
            }
        }

        private async void AddProject_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProjectWindow();
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                try
                {
                    await _apiClient.PostAsync("ProjectsApi", window.Project);
                    LoadProjects_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding project: {ex.Message}");
                }
            }
        }

        private async void EditProject_Click(object sender, RoutedEventArgs e)
        {
            var project = (sender as Button)?.DataContext as Project;
            if (project != null)
            {
                var window = new ProjectWindow(project);
                window.Owner = this;
                if (window.ShowDialog() == true)
                {
                    try
                    {
                        await _apiClient.PutAsync("ProjectsApi", project.Id, window.Project);
                        LoadProjects_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error editing project: {ex.Message}");
                    }
                }
            }
        }

        private async void DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            var project = (sender as Button)?.DataContext as Project;
            if (project != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {project.Name}?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _apiClient.DeleteAsync("ProjectsApi", project.Id);
                        LoadProjects_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting project: {ex.Message}");
                    }
                }
            }
        }
    }
}
