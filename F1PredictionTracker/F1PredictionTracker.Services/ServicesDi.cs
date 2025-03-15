using Microsoft.Extensions.DependencyInjection;

namespace F1PredictionTracker.Services;

public static class ServicesDi
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<PredictionScoringService>();
        services.AddSingleton<RandomAiGenerationService>();
        services.AddSingleton<SmartAiGenerationService>();
        services.AddSingleton<StorePredictionService>();
        services.AddSingleton<PredictionGetService>();
        services.AddSingleton<PredictionGenerationService>();
        services.AddSingleton<PredictionShowService>();
        
        return services;
    }
}
