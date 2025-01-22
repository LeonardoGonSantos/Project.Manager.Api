using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Manager.Api.Models;
using Project.Manager.Application.Handlers.Command;
using System.Collections.Generic;
using System.Linq;

namespace Project.Manager.Api.Services
{
    public class ProjectQuery : IProjectQuery
    {
        private readonly DataDbContext _dbContext;
        private readonly ILogger<ProjectQuery> _logger;

        public ProjectQuery(DataDbContext dbContext, ILogger<ProjectQuery> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<Models.Project> GetProjects()
        {
            _logger.LogInformation("Fetching all projects from the database.");
            var projects = _dbContext.Projects.Include(p => p.Tasks).ToList();
            _logger.LogInformation("Fetched {Count} projects from the database.", projects.Count);
            return projects;
        }

        public Models.Project GetProjectById(int id)
        {
            _logger.LogInformation("Fetching project with ID {Id} from the database.", id);
            var project = _dbContext.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                _logger.LogWarning("Project with ID {Id} not found.", id);
            }
            else
            {
                _logger.LogInformation("Fetched project with ID {Id} from the database.", id);
            }
            return project;
        }
    }
}
