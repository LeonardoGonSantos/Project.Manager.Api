using Microsoft.EntityFrameworkCore;
using Project.Manager.Api.Models;
using Project.Manager.Application.Handlers.Command;

namespace Project.Manager.Api.Services
{
    public class ProjectQuery : IProjectQuery
    {
        private readonly DataDbContext _dbContext;

        public ProjectQuery(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Models.Project> GetProjects() => _dbContext.Projects.Include(p => p.Tasks).ToList();
        public Models.Project GetProjectById(int id) => _dbContext.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id);
    }
}
