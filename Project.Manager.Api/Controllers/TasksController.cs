using Microsoft.AspNetCore.Mvc;
using Project.Manager.Api.Models;
using Project.Manager.Api.Services;

namespace Project.Manager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("projects")]
        public IActionResult GetProjects()
        {
            return Ok(_taskService.GetProjects());
        }

        [HttpGet("projects/{projectId}/tasks")]
        public IActionResult GetTasks(int projectId)
        {
            var project = _taskService.GetProjectById(projectId);
            if (project == null) return NotFound();
            return Ok(project.Tasks);
        }

        [HttpPost("projects")]
        public IActionResult CreateProject([FromBody] Project project)
        {
            _taskService.AddProject(project);
            return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, project);
        }

        [HttpPost("projects/{projectId}/tasks")]
        public IActionResult CreateTask(int projectId, [FromBody] TaskItem task)
        {
            _taskService.AddTask(projectId, task);
            return CreatedAtAction(nameof(GetTasks), new { projectId = projectId, taskId = task.Id }, task);
        }

        [HttpPut("projects/{projectId}/tasks/{taskId}")]
        public IActionResult UpdateTask(int projectId, int taskId, [FromBody] TaskItem task)
        {
            task.Id = taskId;
            _taskService.UpdateTask(projectId, task);
            return NoContent();
        }

        [HttpDelete("projects/{projectId}/tasks/{taskId}")]
        public IActionResult DeleteTask(int projectId, int taskId)
        {
            _taskService.RemoveTask(projectId, taskId);
            return NoContent();
        }
    }
}