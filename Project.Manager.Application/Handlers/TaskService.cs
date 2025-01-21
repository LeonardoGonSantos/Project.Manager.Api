using Project.Manager.Api.Models;

namespace Project.Manager.Api.Services
{
    public class TaskService
    {
        private readonly List<Project> _projects = new List<Project>();

        public List<Project> GetProjects() => _projects;

        public Project GetProjectById(int id) => _projects.FirstOrDefault(p => p.Id == id);

        public void AddProject(Project project) => _projects.Add(project);

        public void AddTask(int projectId, TaskItem task)
        {
            var project = GetProjectById(projectId);
            if (project != null && project.Tasks.Count < 20)
            {
                project.Tasks.Add(task);
            }
        }

        public void UpdateTask(int projectId, TaskItem task)
        {
            var project = GetProjectById(projectId);
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
            }
        }

        public void RemoveTask(int projectId, int taskId)
        {
            var project = GetProjectById(projectId);
            var task = project?.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                project.Tasks.Remove(task);
            }
        }
    }
}