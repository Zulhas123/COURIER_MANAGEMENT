using CourierManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CourierManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ITrackingIdGenerator, TrackingIdGenerator>();
        services.AddScoped<IParcelTypeService, ParcelTypeService>();
        services.AddScoped<IParcelService, ParcelService>();
        return services;
    }
}

