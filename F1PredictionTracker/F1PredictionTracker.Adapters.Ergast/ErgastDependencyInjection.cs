using F1PredictionTracker.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace F1PredictionTracker.Adapters.Ergast;

public static class ErgastDependencyInjection
{
    public static IServiceCollection AddErgastAdapter(this IServiceCollection services)
    {
        services.AddScoped<ErgastConfig>();
        services.AddScoped<IGetRaceResult, GetRaceResult>();
        services.AddScoped<IGetDriverStandings, GetDriverStandings>();
        services.AddScoped<IGetDrivers, GetDrivers>();
        
        return services;
    }
}