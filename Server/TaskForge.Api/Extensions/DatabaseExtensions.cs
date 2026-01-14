using Microsoft.EntityFrameworkCore;
using TaskForge.Api.Infrastructure;

namespace TaskForge.Api.Extensions;

public static class DatabaseExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        const int maxRetries = 10;
        for (var retry = 0; retry < maxRetries; retry++)
        {
            try
            {
                logger.LogInformation("Applying database migrations...");
                dbContext.Database.Migrate();
                logger.LogInformation("Database migrations applied successfully");
                return;
            }
            catch (Exception ex) when (retry < maxRetries - 1)
            {
                logger.LogWarning("Database not ready, retrying in 3 seconds... ({Attempt}/{MaxRetries})", 
                    retry + 1, maxRetries);
                Thread.Sleep(3000);
            }
        }
    }
}