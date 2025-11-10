using Dapper;
using Microsoft.EntityFrameworkCore;
using TaskForge.Api.Domain.Projects;
using TaskForge.Api.Infrastructure.Data;
using TaskForge.Api.Infrastructure.Repositories;

namespace TaskForge.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddPersistence(services, configuration);

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") ??
                               throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

        services.AddScoped<IProjectRepository, ProjectRepository>();

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(
                connectionString
            )
        );

        // Reason ?
        SqlMapper.AddTypeHandler(
            new DateOnlyTypeHandler()
        );
    }
}