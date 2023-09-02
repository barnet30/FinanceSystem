using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceSystem.Data.Extensions;

public static class ServiceCollectionExtension
{
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