using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project.Manager.Application.Models;

namespace Project.Manager.Application.Handlers.Command
{
    public class ProjectCommand
    {
        private readonly DbContext _context;

        public ProjectCommand(DbContext context)
        {
            _context = context;
        }

        public void AddProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        private Project GetProjectById(int projectId)
        {
            return _context.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == projectId);
        }
    }

}