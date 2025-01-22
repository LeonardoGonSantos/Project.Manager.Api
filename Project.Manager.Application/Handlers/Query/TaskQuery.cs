using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Manager.Api.Models;
using Project.Manager.Application.Handlers.Command;

namespace Project.Manager.Application.Handlers.Query
{
    public class TaskQuery : ITaskQuery
    {
        private readonly DataDbContext _dbContext;
        private readonly ILogger<TaskQuery> _logger;

        public TaskQuery(DataDbContext dbContext, ILogger<TaskQuery> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Api.Models.Project GetTasksByProject(int id)
        {
            _logger.LogInformation("Getting tasks for project with ID {ProjectId}", id);
            var project = _dbContext.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                _logger.LogWarning("Project with ID {ProjectId} not found", id);
            }
            else
            {
                _logger.LogInformation("Project with ID {ProjectId} found with {TaskCount} tasks", id, project.Tasks.Count);
            }
            return project;
        }
    }
}
