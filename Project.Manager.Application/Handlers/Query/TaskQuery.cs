using System.Linq;
using Project.Manager.Api.Models;
using Project.Manager.Application.Handlers.Command;
using Project.Manager.Domain.Models;

namespace Project.Manager.Application.Handlers.Query
{
    public class TaskQuery : ITaskQuery
    {
        private readonly DataDbContext _dbContext;

        public TaskQuery(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Api.Models.Project GetTasksByProject(int id)
        {
            return _dbContext.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id);
        }
    }
}
