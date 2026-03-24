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
            LoadAll();
        }

        private void LoadAll()
        {
            LoadProjects_Click(null, null);
            LoadTasks_Click(null, null);
            LoadMembers_Click(null, null);
        }

        #region Projects

        private async void LoadProjects_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var projects = await _apiClient.GetAsync<Project>("ProjectsApi");
                ProjectsGrid.ItemsSource = projects;
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private async void AddProject_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProjectWindow { Owner = this };
            if (win.ShowDialog() == true)
            {
                await _apiClient.PostAsync("ProjectsApi", win.Project);
                LoadProjects_Click(null, null);
            }
        }

        private async void EditProject_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Project p)
            {
                var win = new ProjectWindow(p) { Owner = this };
                if (win.ShowDialog() == true)
                {
                    await _apiClient.PutAsync("ProjectsApi", p.Id, win.Project);
                    LoadProjects_Click(null, null);
                }
            }
        }

        private async void DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Project p && MessageBox.Show($"Delete {p.Name}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _apiClient.DeleteAsync("ProjectsApi", p.Id);
                LoadProjects_Click(null, null);
            }
        }

        #endregion

        #region Tasks

        private async void LoadTasks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tasks = await _apiClient.GetAsync<ProjectTask>("ProjectTasksApi");
                TasksGrid.ItemsSource = tasks;
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private async void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var win = new ProjectTaskWindow { Owner = this };
            if (win.ShowDialog() == true)
            {
                await _apiClient.PostAsync("ProjectTasksApi", win.Task);
                LoadTasks_Click(null, null);
            }
        }

        private async void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is ProjectTask t)
            {
                var win = new ProjectTaskWindow(t) { Owner = this };
                if (win.ShowDialog() == true)
                {
                    await _apiClient.PutAsync("ProjectTasksApi", t.Id, win.Task);
                    LoadTasks_Click(null, null);
                }
            }
        }

        private async void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is ProjectTask t && MessageBox.Show($"Delete {t.Title}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _apiClient.DeleteAsync("ProjectTasksApi", t.Id);
                LoadTasks_Click(null, null);
            }
        }

        #endregion

        #region Team Members

        private async void LoadMembers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var members = await _apiClient.GetAsync<TeamMember>("TeamMembersApi");
                MembersGrid.ItemsSource = members;
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private async void AddMember_Click(object sender, RoutedEventArgs e)
        {
            var win = new TeamMemberWindow { Owner = this };
            if (win.ShowDialog() == true)
            {
                await _apiClient.PostAsync("TeamMembersApi", win.TeamMember);
                LoadMembers_Click(null, null);
            }
        }

        private async void EditMember_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is TeamMember m)
            {
                var win = new TeamMemberWindow(m) { Owner = this };
                if (win.ShowDialog() == true)
                {
                    await _apiClient.PutAsync("TeamMembersApi", m.Id, win.TeamMember);
                    LoadMembers_Click(null, null);
                }
            }
        }

        private async void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is TeamMember m && MessageBox.Show($"Delete {m.Name}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await _apiClient.DeleteAsync("TeamMembersApi", m.Id);
                LoadMembers_Click(null, null);
            }
        }

        #endregion
    }
}
