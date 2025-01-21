namespace Project.Manager.Application.Handlers.Command
{
public interface IProjectCommand
{
    void AddProject(Project project);
    Project GetProjectById(int projectId);
}
}