

using FinanceSystem.Search;
// ReSharper disable once CheckNamespace
using Grpc.Net.Client;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services, string searchGrpcServiceUrl)
    {
        try
        {
            var servicesTypes = typeof(ServiceCollectionExtensions).Assembly.GetTypes().Where(x =>
                x.Name.EndsWith("Service") && x is { IsAbstract: false, IsInterface: false });

            foreach (var service in servicesTypes)
            {
                if (service.GetInterfaces().Length > 0)
                {
                    var serviceInterface = service.GetInterfaces().First();
                    services.AddScoped(serviceInterface, service);
                }
                else
                    services.AddScoped(service);
            }
            
            services.AddGrpcClient<Greeter.GreeterClient>(opt =>
            {
                opt.Address = new Uri(searchGrpcServiceUrl);
            });

            services.AddGrpcClient<FinanceSystemSearch.FinanceSystemSearchClient>(opt =>
            {
                opt.Address = new Uri(searchGrpcServiceUrl);
            });
        }
        catch
        {
            // do nothing
        }
    }
}