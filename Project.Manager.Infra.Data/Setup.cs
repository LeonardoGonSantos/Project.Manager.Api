using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class Setup
{
    [ExcludeFromCodeCoverage]
    public static IServiceCollection SetupData(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}