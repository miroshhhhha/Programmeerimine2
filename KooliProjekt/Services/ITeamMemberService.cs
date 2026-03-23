using KooliProjekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface ITeamMemberService
    {
        Task<List<TeamMember>> GetAllMembersAsync();
        Task<TeamMember> GetMemberByIdAsync(int id);
        Task AddMemberAsync(TeamMember member);
        Task UpdateMemberAsync(TeamMember member);
        Task DeleteMemberAsync(int id);
    }
}
