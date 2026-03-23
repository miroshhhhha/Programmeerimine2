using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Services;
using KooliProjekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersApiController : ControllerBase
    {
        private readonly ITeamMemberService _memberService;

        public TeamMembersApiController(ITeamMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetMembers()
        {
            return await _memberService.GetAllMembersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMember>> GetMember(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return member;
        }

        [HttpPost]
        public async Task<ActionResult<TeamMember>> PostMember(TeamMember member)
        {
            await _memberService.AddMemberAsync(member);
            return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, TeamMember member)
        {
            if (id != member.Id)
            {
                return BadRequest();
            }

            await _memberService.UpdateMemberAsync(member);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            await _memberService.DeleteMemberAsync(id);
            return NoContent();
        }
    }
}
