using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Project.Manager.Api.Models;

namespace Project.Manager.Application.Handlers.Command
{
    public class TaskCommand : ITaskCommand
    {
        private readonly DataDbContext _context;
        private readonly IProjectQuery _projectQuery;
        private readonly ILogger<TaskCommand> _logger;

        public TaskCommand(DataDbContext context, IProjectQuery projectQuery, ILogger<TaskCommand> logger)
        {
            _context = context;
            _projectQuery = projectQuery;
            _logger = logger;
        }    

        public void AddTask(int projectId, TaskItem task)
        {
            _logger.LogInformation($"Adding task to project {projectId}");
            var project = _projectQuery.GetProjectById(projectId);
            if (project != null && project.Tasks.Count < 20)
            {
                project.Tasks.Add(task);
                _context.SaveChanges();
                _logger.LogInformation($"Task added to project {projectId}");
            }
            else
            {
                _logger.LogWarning($"Failed to add task to project {projectId}");
                throw new Exception("Project not found or task limit reached");
            }
        }

        public void UpdateTask(int projectId, TaskItem task)
        {
            _logger.LogInformation($"Updating task {task.Id} in project {projectId}");
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
                _logger.LogInformation($"Task {task.Id} updated in project {projectId}");
            }
            else
            {
                _logger.LogWarning($"Failed to update task {task.Id} in project {projectId}");
            }
        }

        public void RemoveTask(int projectId, int taskId)
        {
            _logger.LogInformation($"Removing task {taskId} from project {projectId}");
            var project = _projectQuery.GetProjectById(projectId);
            var task = project?.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                project.Tasks.Remove(task);
                _context.SaveChanges();
                _logger.LogInformation($"Task {taskId} removed from project {projectId}");
            }
            else
            {
                _logger.LogWarning($"Failed to remove task {taskId} from project {projectId}");
            }
        }
    }
}