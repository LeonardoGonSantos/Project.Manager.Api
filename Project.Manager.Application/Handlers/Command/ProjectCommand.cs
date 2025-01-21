using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Manager.Api.Models;

namespace Project.Manager.Application.Handlers.Command
{
    public class ProjectCommand : IProjectCommand
    {
        private readonly DataDbContext _context;

        public ProjectCommand(DataDbContext context)
        {
            _context = context;
        }

        public void AddProject(Api.Models.Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public Api.Models.Project GetProjectById(int projectId)
        {
            return _context.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == projectId);
        }
    }

}