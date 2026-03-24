using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Models;

namespace KooliProjekt.WinForms
{
    public class TeamMemberPresenter
    {
        private readonly ITeamMemberView _view;
        private readonly ApiClient _apiClient;

        public TeamMemberPresenter(ITeamMemberView view)
        {
            _view = view;
            _apiClient = new ApiClient();
        }

        public async Task LoadMembers()
        {
            try
            {
                var members = await _apiClient.GetAsync<TeamMember>("TeamMembersApi");
                _view.DisplayMembers(members);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error loading members: {ex.Message}");
            }
        }

        public async Task AddMember(TeamMember member)
        {
            try
            {
                await _apiClient.PostAsync("TeamMembersApi", member);
                await LoadMembers();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error adding member: {ex.Message}");
            }
        }

        public async Task EditMember(TeamMember member)
        {
            try
            {
                await _apiClient.PutAsync("TeamMembersApi", member.Id, member);
                await LoadMembers();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error editing member: {ex.Message}");
            }
        }

        public async Task DeleteMember(int id)
        {
            try
            {
                await _apiClient.DeleteAsync("TeamMembersApi", id);
                await LoadMembers();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Error deleting member: {ex.Message}");
            }
        }
    }
}
