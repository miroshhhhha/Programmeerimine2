using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetPagedEventsAsync(int page, int pageSize)
        {
            return await _context.Event
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Event
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddEventAsync(Event @event)
        {
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(Event @event)
        {
            _context.Event.Update(@event);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event != null)
            {
                _context.Event.Remove(@event);
                await _context.SaveChangesAsync();
            }
        }
    }
    public interface IEventService
    {
        Task<List<Event>> GetPagedEventsAsync(int page, int pageSize);
        Task<Event> GetEventByIdAsync(int id);
        Task AddEventAsync(Event @event);
        Task UpdateEventAsync(Event @event);
        Task DeleteEventAsync(int id);
    }
}
