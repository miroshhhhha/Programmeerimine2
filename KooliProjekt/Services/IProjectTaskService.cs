using KooliProjekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IProjectTaskService
    {
        Task<List<ProjectTask>> GetAllTasksAsync();
        Task<ProjectTask> GetTaskByIdAsync(int id);
        Task AddTaskAsync(ProjectTask task);
        Task UpdateTaskAsync(ProjectTask task);
        Task DeleteTaskAsync(int id);
    }
}
