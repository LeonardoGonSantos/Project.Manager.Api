using System;
using System.Linq;
using Project.Manager.Api.Models;

namespace Project.Manager.Application.Handlers.Command
{
    public class TaskCommand : ITaskCommand
    {
        private readonly DataDbContext _context;
        private readonly IProjectQuery _projectQuery;

        public TaskCommand(DataDbContext context, IProjectQuery projectQuery)
        {
            _context = context;
            _projectQuery = projectQuery;
        }    

        public void AddTask(int projectId, TaskItem task)
        {
            var project = _projectQuery.GetProjectById(projectId);
            if (project != null && project.Tasks.Count < 20)
            {
                project.Tasks.Add(task);
                _context.SaveChanges();
            }
        }

        public void UpdateTask(int projectId, TaskItem task)
        {
            var project = _projectQuery.GetProjectById(projectId);
            var existingTask = project?.Tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.DueDate = task.DueDate;
                existingTask.Status = task.Status;
                existingTask.Comments = task.Comments;
                existingTask.History.Add(new TaskHistory
                {
                    Change = "Task updated",
                    ChangedAt = DateTime.Now,
                    ChangedBy = "User"
                });
                _context.SaveChanges();
            }
        }

        public void RemoveTask(int projectId, int taskId)
        {
            var project = _projectQuery.GetProjectById(projectId);
            var task = project?.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                project.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }




}