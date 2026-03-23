using KooliProjekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IProjectService
    {
        Task<PagedResult<Project>> GetProjectsPagedAsync(int page, int pageSize, string searchTerm = null);
        Task<List<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task AddProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int id);
    }
}
