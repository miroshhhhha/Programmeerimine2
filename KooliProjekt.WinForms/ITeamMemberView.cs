using System.Collections.Generic;
using KooliProjekt.Models;

namespace KooliProjekt.WinForms
{
    public interface ITeamMemberView
    {
        void DisplayMembers(List<TeamMember> members);
        void ShowError(string message);
    }
}
