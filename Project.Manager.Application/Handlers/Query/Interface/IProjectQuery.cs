namespace Project.Manager.Application.Handlers.Command
{
public interface IProjectQuery
{
List<Api.Models.Project> GetProjects();

Api.Models.Project GetProjectById(int id);
}
}