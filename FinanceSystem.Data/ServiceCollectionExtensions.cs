using FinanceSystem.Data;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        try
        {
            var repositories = typeof(ServiceCollectionExtensions).Assembly.GetTypes().Where(x =>
                x.Name.EndsWith("Repository") && x is { IsAbstract: false, IsInterface: false });

            foreach (var repository in repositories)
            {
                if (repository.GetInterfaces().Length > 0)
                {
                    foreach (var @interface in repository.GetInterfaces())
                        services.AddScoped(@interface, repository);
                }
                else
                    services.AddScoped(repository);
            }
        }
        catch
        {
            // do nothing
        }
    }
    
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IDbContext, FinanceSystemDbContext>(options =>
        {
            options.UseNpgsql(connectionString, action =>
            {
                action.UseNetTopologySuite();
                action.EnableRetryOnFailure(3);
                action.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
        
        return services;
    }
}