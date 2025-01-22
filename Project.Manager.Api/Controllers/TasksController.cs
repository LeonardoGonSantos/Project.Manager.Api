using Microsoft.AspNetCore.Mvc;
using Project.Manager.Api.Models;
using Project.Manager.Api.Services;
using Project.Manager.Application.Handlers.Command;

namespace Project.Manager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskCommand _taskCommand;
        private readonly ITaskQuery _taskQuery;

        public TasksController(ITaskCommand taskCommand, ITaskQuery taskQuery)
        {
            _taskCommand = taskCommand;
            _taskQuery = taskQuery;
        }

        [HttpGet("projects/{projectId}/tasks")]
        public IActionResult GetTasks(int projectId)
        {
            var tasks = _taskQuery.GetTasksByProject(projectId);
            if (tasks == null) return NotFound();
            return Ok(tasks);
        }

        [HttpPost("projects/{projectId}/tasks")]
        public IActionResult CreateTask(int projectId, [FromBody] TaskItem task)
        {
            _taskCommand.AddTask(projectId, task);
            return CreatedAtAction(nameof(GetTasks), new { projectId = projectId, taskId = task.Id }, task);
        }

        [HttpPut("projects/{projectId}/tasks/{taskId}")]
        public IActionResult UpdateTask(int projectId, int taskId, [FromBody] TaskItem task)
        {
            task.Id = taskId;
            _taskCommand.UpdateTask(projectId, task);
            return NoContent();
        }

        [HttpDelete("projects/{projectId}/tasks/{taskId}")]
        public IActionResult DeleteTask(int projectId, int taskId)
        {
            _taskCommand.RemoveTask(projectId, taskId);
            return NoContent();
        }
    }
}
