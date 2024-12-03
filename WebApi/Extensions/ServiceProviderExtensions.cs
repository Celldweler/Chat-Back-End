using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Extensions;

public static class ServiceProviderExtensions
{
    public static void ApplyDbMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDataContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Applying database migrations...");
            ctx.Database.Migrate();
            logger.LogInformation("Database migrations applied successfully.");
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Migration failed: {ex.Message}");
            throw;
        }
    }
}
