using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly ApplicationDbContext _context;

        public ProjectTaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectTask>> GetAllTasksAsync()
        {
            return await _context.ProjectTasks
                .Include(t => t.Project)
                .ToListAsync();
        }

        public async Task<ProjectTask> GetTaskByIdAsync(int id)
        {
            return await _context.ProjectTasks
                .Include(t => t.Project)
                .Include(t => t.AssignedMember)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTaskAsync(ProjectTask task)
        {
            _context.ProjectTasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(ProjectTask task)
        {
            _context.ProjectTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
