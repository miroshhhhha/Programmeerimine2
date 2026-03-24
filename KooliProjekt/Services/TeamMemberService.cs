using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ApplicationDbContext _context;

        public TeamMemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TeamMember>> GetMembersPagedAsync(int page, int pageSize, string searchTerm = null)
        {
            var query = _context.TeamMembers.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m => m.Name.Contains(searchTerm) || m.Email.Contains(searchTerm));
            }

            return await query.OrderBy(m => m.Name).ToPagedResult(page, pageSize);
        }

        public async Task<List<TeamMember>> GetAllMembersAsync()
        {
            return await _context.TeamMembers.ToListAsync();
        }

        public async Task<TeamMember> GetMemberByIdAsync(int id)
        {
            return await _context.TeamMembers
                .Include(m => m.Tasks)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddMemberAsync(TeamMember member)
        {
            _context.TeamMembers.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMemberAsync(TeamMember member)
        {
            _context.TeamMembers.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member != null)
            {
                _context.TeamMembers.Remove(member);
                await _context.SaveChangesAsync();
            }
        }
    }
}
