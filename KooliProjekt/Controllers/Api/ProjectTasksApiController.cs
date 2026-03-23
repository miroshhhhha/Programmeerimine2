using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Services;
using KooliProjekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksApiController : ControllerBase
    {
        private readonly IProjectTaskService _taskService;

        public ProjectTasksApiController(IProjectTaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasks()
        {
            return await _taskService.GetAllTasksAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTask>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectTask>> PostTask(ProjectTask task)
        {
            await _taskService.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, ProjectTask task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            await _taskService.UpdateTaskAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
