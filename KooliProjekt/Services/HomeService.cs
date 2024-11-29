using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _context;

        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Home>> GetAllHomesAsync()
        {
            try
            {
                return await _context.Homes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error fetching homes", ex);
            }
        }

        public async Task<Home> GetHomeByIdAsync(int id)
        {
            try
            {
                return await _context.Homes
                    .FirstOrDefaultAsync(h => h.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching home with ID {id}", ex);
            }
        }

        public async Task AddHomeAsync(Home home)
        {
            try
            {
                _context.Homes.Add(home);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error adding home", ex);
            }
        }

        public async Task UpdateHomeAsync(Home home)
        {
            try
            {
                _context.Homes.Update(home);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating home", ex);
            }
        }

        public async Task DeleteHomeAsync(int id)
        {
            try
            {
                var home = await _context.Homes.FindAsync(id);
                if (home != null)
                {
                    _context.Homes.Remove(home);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Home with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting home with ID {id}", ex);
            }
        }
    }
    public interface IHomeService
    {
        Task<List<Home>> GetAllHomesAsync();
        Task<Home> GetHomeByIdAsync(int id);
        Task AddHomeAsync(Home home);
        Task UpdateHomeAsync(Home home);
        Task DeleteHomeAsync(int id);
    }
}
