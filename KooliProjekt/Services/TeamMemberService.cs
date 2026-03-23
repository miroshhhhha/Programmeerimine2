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
