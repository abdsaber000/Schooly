using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
namespace SchoolManagement.Application.Extensions;


public static class ServiceCollection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator();
        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
    
}
