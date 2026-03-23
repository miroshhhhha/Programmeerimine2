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

        public ProjectTasksController(IProjectTaskService taskService, IProjectService projectService)
        {
            _taskService = taskService;
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return View(tasks);
        }

        public async Task<IActionResult> Create()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Status,Priority,Deadline,ProjectId")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.AddTaskAsync(task);
                return RedirectToAction(nameof(Index));
            }
            var projects = await _projectService.GetAllProjectsAsync();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name", task.ProjectId);
            return View(task);
        }
    }
}
