// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this IServiceCollection services)
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
        }
        catch
        {
            // do nothing
        }
    }
}