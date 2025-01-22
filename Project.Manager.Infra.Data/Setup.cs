using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Manager.Api.Models;

namespace Project.Manager.Infra.Data
{
    public static class Setup
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection SetupData(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataDbContext>(options =>
                options.UseSqlite(connectionString));

            return services;
        }
    }
}