using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class PlansService : IPlansService
    {
        private readonly ApplicationDbContext _context;

        public PlansService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Plan>> GetAllPlansAsync()
        {
            try
            {
                return (List<Plan>)_context.Plans;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error fetching plans", ex);
            }
        }

        public async Task<Plan> GetPlanByIdAsync(int id)
        {
            try
            {
                return await _context.Plan
                    .FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching plan with ID {id}", ex);
            }
        }

        public async Task AddPlanAsync(Plan plan)
        {
            try
            {
                _context.Plan.Add(plan);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error adding plan", ex);
            }
        }

        public async Task UpdatePlanAsync(Plan plan)
        {
            try
            {
                _context.Plan.Update(plan);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating plan", ex);
            }
        }

        public async Task DeletePlanAsync(int id)
        {
            try
            {
                var plan = await _context.Plan.FindAsync(id);
                if (plan != null)
                {
                    _context.Plan.Remove(plan);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Plan with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting plan with ID {id}", ex);
            }
        }
    }
    public interface IPlansService
    {
        Task<List<Plan>> GetAllPlansAsync();
        Task<Plan> GetPlanByIdAsync(int id);
        Task AddPlanAsync(Plan plan);
        Task UpdatePlanAsync(Plan plan);
        Task DeletePlanAsync(int id);
    }
}
