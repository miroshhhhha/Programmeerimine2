using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KooliProjekt.Services;
using KooliProjekt.Models;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly IProjectTaskService _taskService;
        private readonly IProjectService _projectService;
        private readonly ITeamMemberService _memberService;

        public ProjectTasksController(IProjectTaskService taskService, IProjectService projectService, ITeamMemberService memberService)
        {
            _taskService = taskService;
            _projectService = projectService;
            _memberService = memberService;
        }

        public async Task<IActionResult> Index(int page = 1, string searchTerm = null)
        {
            var pagedResult = await _taskService.GetTasksPagedAsync(page, 10, searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(pagedResult);
        }

        public async Task<IActionResult> Create()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            var members = await _memberService.GetAllMembersAsync();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name");
            ViewBag.TeamMemberId = new SelectList(members, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Status,Priority,Deadline,ProjectId,TeamMemberId")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.AddTaskAsync(task);
                return RedirectToAction(nameof(Index));
            }
            var projects = await _projectService.GetAllProjectsAsync();
            var members = await _memberService.GetAllMembersAsync();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name", task.ProjectId);
            ViewBag.TeamMemberId = new SelectList(members, "Id", "Name", task.TeamMemberId);
            return View(task);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            var projects = await _projectService.GetAllProjectsAsync();
            var members = await _memberService.GetAllMembersAsync();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name", task.ProjectId);
            ViewBag.TeamMemberId = new SelectList(members, "Id", "Name", task.TeamMemberId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,Priority,Deadline,ProjectId,TeamMemberId")] ProjectTask task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction(nameof(Index));
            }
            var projects = await _projectService.GetAllProjectsAsync();
            var members = await _memberService.GetAllMembersAsync();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name", task.ProjectId);
            ViewBag.TeamMemberId = new SelectList(members, "Id", "Name", task.TeamMemberId);
            return View(task);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
