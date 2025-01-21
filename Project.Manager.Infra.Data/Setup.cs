using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Manager.Infra.Data;

public static class Setup
{
    public static IServiceCollection SetupData(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}