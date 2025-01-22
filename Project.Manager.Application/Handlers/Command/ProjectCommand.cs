using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Manager.Api.Models;

namespace Project.Manager.Application.Handlers.Command
{
    public class ProjectCommand : IProjectCommand
    {
        private readonly DataDbContext _context;
        private readonly ILogger<ProjectCommand> _logger;

        public ProjectCommand(DataDbContext context, ILogger<ProjectCommand> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int AddProject(Api.Models.Project project)
        {
            _logger.LogInformation("Adding a new project with name: {ProjectName}", project.Name);
            _context.Projects.Add(project);
            _context.SaveChanges();
            _logger.LogInformation("Project added successfully with ID: {ProjectId}", project.Id);
            return project.Id;
        }

        public Api.Models.Project GetProjectById(int projectId)
        {
            _logger.LogInformation("Fetching project with ID: {ProjectId}", projectId);
            var project = _context.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == projectId);
            if (project != null)
            {
                _logger.LogInformation("Project found with ID: {ProjectId}", projectId);
            }
            else
            {
                _logger.LogWarning("Project not found with ID: {ProjectId}", projectId);
            }
            return project;
        }
    }
}