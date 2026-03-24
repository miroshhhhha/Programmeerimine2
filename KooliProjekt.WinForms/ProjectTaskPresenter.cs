using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Models;

namespace KooliProjekt.WinForms
{
    public class ProjectTaskPresenter
    {
        private readonly IProjectTaskView _view;
        private readonly ApiClient _apiClient;

        public ProjectTaskPresenter(IProjectTaskView view)
        {
            _view = view;
            _apiClient = new ApiClient();
        }

        public async Task LoadTasks()
        {
            try
            {
                var tasks = await _apiClient.GetAsync<ProjectTask>("ProjectTasksApi");
                _view.DisplayTasks(tasks);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error loading tasks: {ex.Message}");
            }
        }

        public async Task AddTask(ProjectTask task)
        {
            try
            {
                await _apiClient.PostAsync("ProjectTasksApi", task);
                await LoadTasks();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error adding task: {ex.Message}");
            }
        }

        public async Task EditTask(ProjectTask task)
        {
            try
            {
                await _apiClient.PutAsync("ProjectTasksApi", task.Id, task);
                await LoadTasks();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error editing task: {ex.Message}");
            }
        }

        public async Task DeleteTask(int id)
        {
            try
            {
                await _apiClient.DeleteAsync("ProjectTasksApi", id);
                await LoadTasks();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error deleting task: {ex.Message}");
            }
        }
    }
}
