using Project.Manager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Manager.Api.Services
{
    public class ProjectQuery
    {
        private readonly DbContext _dbContext;

        public ProjectQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Project> GetProjects() => _dbContext.Projects.Include(p => p.Tasks).ToList();

    }
}
