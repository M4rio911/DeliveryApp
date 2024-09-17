using Microsoft.Extensions.DependencyInjection;

namespace DeliveryApp.Infrastructure;

public static class InfastructureConfiguration
{
    public static IServiceCollection Configure(IServiceCollection services)
    {
        //services.AddScoped<>();

        return services;
    }
}
