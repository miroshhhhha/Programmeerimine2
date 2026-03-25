using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using KooliProjekt.Models;

namespace KooliProjekt.WPF
{
    public class MainViewModel : BindableBase
    {
        private readonly IApiClient _apiClient;
        
        public ObservableCollection<Project> Projects { get; } = new ObservableCollection<Project>();
        public ObservableCollection<ProjectTask> ProjectTasks { get; } = new ObservableCollection<ProjectTask>();
        public ObservableCollection<TeamMember> TeamMembers { get; } = new ObservableCollection<TeamMember>();

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public MainViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task LoadProjectsAsync()
        {
            try
            {
                var result = await _apiClient.GetAsync<Project>("ProjectsApi");
                Projects.Clear();
                foreach (var item in result) Projects.Add(item);
                ErrorMessage = null;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading projects: {ex.Message}";
            }
        }

        public async Task LoadTasksAsync()
        {
            try
            {
                var result = await _apiClient.GetAsync<ProjectTask>("ProjectTasksApi");
                ProjectTasks.Clear();
                foreach (var item in result) ProjectTasks.Add(item);
                ErrorMessage = null;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading tasks: {ex.Message}";
            }
        }

        public async Task LoadMembersAsync()
        {
            try
            {
                var result = await _apiClient.GetAsync<TeamMember>("TeamMembersApi");
                TeamMembers.Clear();
                foreach (var item in result) TeamMembers.Add(item);
                ErrorMessage = null;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading members: {ex.Message}";
            }
        }

        public async Task<Result> AddProjectAsync(Project project)
        {
            try
            {
                await _apiClient.PostAsync("ProjectsApi", project);
                await LoadProjectsAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error adding project: {ex.Message}";
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> EditProjectAsync(int id, Project project)
        {
            try
            {
                await _apiClient.PutAsync("ProjectsApi", id, project);
                await LoadProjectsAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error editing project: {ex.Message}";
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> DeleteProjectAsync(int id)
        {
            try
            {
                await _apiClient.DeleteAsync("ProjectsApi", id);
                await LoadProjectsAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting project: {ex.Message}";
                return Result.Failure(ex.Message);
            }
        }
    }
}
