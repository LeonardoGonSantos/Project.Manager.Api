namespace Project.Manager.Application.Handlers.Command
{
    public interface IProjectCommand
    {
        void AddProject(Api.Models.Project project);
        Api.Models.Project GetProjectById(int projectId);
    }
}