using Microsoft.Extensions.DependencyInjection;

namespace Project.Manager.Application
{
    public static class Setup
    {
        public static void SetupApplication(this IServiceCollection services)
        {
services.AddScoped<IProjectCommand, ProjectCommand>();
services.AddScoped<ITaskCommand, TaskCommand>();

services.AddScoped<IProjectQuery, ProjectQuery>();
services.AddScoped<ITaskQuery, TaskQuery>();
        }
    }
}