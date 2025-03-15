using F1PredictionTracker.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace F1PredictionTracker.Adapters.FileStorage;

public static class FileStorageDependencyInjection
{
    public static IServiceCollection AddFileStorageAdapter(this IServiceCollection services)
    {
        services.AddScoped<FileStorageConfig>();
        services.AddScoped<IStorePredictions, PredictionsRepository>();
        services.AddScoped<IRetrievePredictions, PredictionsRepository>();
        services.AddScoped<IStorePredictionStandings, PredictionStandingsRepository>();
        services.AddScoped<IRetrievePredictionStandings, PredictionStandingsRepository>();
        services.AddScoped<IStoreState, StateRepository>();
        services.AddScoped<IRetrieveState, StateRepository>();
        
        return services;
    }
}
