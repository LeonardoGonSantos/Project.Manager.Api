using Microsoft.Extensions.DependencyInjection;
using Project.Manager.Api.Services;
using Project.Manager.Application.Handlers.Command;
using Project.Manager.Application.Handlers.Query;

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