using Project.Manager.Api.Models;

namespace Project.Manager.Application.Handlers.Command
{
    public interface ITaskCommand
    {
        void AddTask(int projectId, TaskItem task);
        void UpdateTask(int projectId, TaskItem task);
        void RemoveTask(int projectId, int taskId);
    }
}