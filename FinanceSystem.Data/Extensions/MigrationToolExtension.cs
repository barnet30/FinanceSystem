using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FinanceSystem.Data.Extensions;

public class MigrationToolExtension
{
    private readonly IServiceProvider _rootServiceProvider;
    private readonly ILogger<MigrationToolExtension> _logger;
    
    public MigrationToolExtension(IServiceProvider rootServiceProvider)
    {
        _rootServiceProvider = rootServiceProvider;
        _logger = rootServiceProvider.GetRequiredService<ILogger<MigrationToolExtension>>();
    }
    
    public static void Execute(IServiceProvider serviceProvider)
        => new MigrationToolExtension(serviceProvider).Migrate();
    
    private void Migrate()
    {
        _logger.LogInformation("Creating scope...");

        try
        {
            using var scope = _rootServiceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<FinanceSystemDbContext>();

            _logger.LogInformation("Migrating DbContext '{DbContext}'...", dbContext.GetType());

            dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(2));
            dbContext.Database.Migrate();
            dbContext.Database.SetCommandTimeout(TimeSpan.FromSeconds(30));

            _logger.LogInformation("Migrate for DbContext '{DbContext}' is complete", dbContext.GetType());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while applying migration");
            throw;
        }

        _logger.LogInformation("Migrations are complete");
    }
}