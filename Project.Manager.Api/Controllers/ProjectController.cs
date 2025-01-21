using Microsoft.AspNetCore.Mvc;
using Project.Manager.Api.Models;
using Project.Manager.Api.Services;

namespace Project.Manager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectCommand _projectCommand;
        private readonly IProjectQuery _projectQuery;

        public ProjectsController(IProjectCommand projectCommand, IProjectQuery projectQuery)
        {
            _projectCommand = projectCommand;
            _projectQuery = projectQuery;
        }

        [HttpGet("projects")]
        public IActionResult GetProjects()
        {
            return Ok(_projectQuery.GetProjects());
        }

        [HttpGet("projects/{projectId}")]
        public IActionResult GetProjectById(int projectId)
        {
            var project = _projectQuery.GetProjectById(projectId);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost("projects")]
        public IActionResult CreateProject([FromBody] Project project)
        {
            _projectCommand.AddProject(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }
    }
}
