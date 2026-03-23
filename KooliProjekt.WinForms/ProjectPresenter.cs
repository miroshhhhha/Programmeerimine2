using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Models;
using KooliProjekt.WinForms;

namespace KooliProjekt.WinForms
{
    public class ProjectPresenter
    {
        private readonly IProjectView _view;
        private readonly ApiClient _apiClient;

        public ProjectPresenter(IProjectView view)
        {
            _view = view;
            _apiClient = new ApiClient();
        }

        public async Task LoadProjects()
        {
            try
            {
                var projects = await _apiClient.GetAsync<Project>("ProjectsApi");
                _view.DisplayProjects(projects);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error loading projects: {ex.Message}");
            }
        }

        public async Task AddProject(Project project)
        {
            try
            {
                await _apiClient.PostAsync("ProjectsApi", project);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error adding project: {ex.Message}");
            }
        }

        public async Task EditProject(Project project)
        {
            try
            {
                await _apiClient.PutAsync("ProjectsApi", project.Id, project);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error editing project: {ex.Message}");
            }
        }

        public async Task DeleteProject(int id)
        {
            try
            {
                await _apiClient.DeleteAsync("ProjectsApi", id);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error deleting project: {ex.Message}");
            }
        }
    }
}
