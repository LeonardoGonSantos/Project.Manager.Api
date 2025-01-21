namespace Project.Manager.Application.Handlers.Command
{
    public interface ITaskCommandService
    {
        void AddTask(int projectId, TaskItem task);
        void UpdateTask(int projectId, TaskItem task);
        void RemoveTask(int projectId, int taskId);
    }
}