using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Manager.Application.Models;

namespace Project.Manager.Application.Handlers.Query
{
    public class TaskQuery
    {
        private readonly DbContext _dbContext;

        public TaskQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Project GetTasksByProject(int id)
        {
            return _dbContext.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id);
        }
    }
}
