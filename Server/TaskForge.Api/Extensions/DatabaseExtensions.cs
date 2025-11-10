using Microsoft.EntityFrameworkCore;
using TaskForge.Api.Infrastructure;

namespace TaskForge.Api.Extensions;

public static class DatabaseExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}