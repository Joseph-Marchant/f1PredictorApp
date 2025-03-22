using Microsoft.Extensions.DependencyInjection;

namespace F1PredictionTracker.Services;

public static class ServicesDi
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<PredictionScoringService>();
        services.AddSingleton<ActualAiGenerationService>();
        services.AddSingleton<RandomAiGenerationService>();
        services.AddSingleton<SmartAiGenerationService>();
        services.AddSingleton<StorePredictionService>();
        services.AddSingleton<BuildUserPredictionService>();
        services.AddSingleton<PredictionGenerationService>();
        services.AddSingleton<PredictionValidationService>();
        services.AddSingleton<PredictionShowService>();
        services.AddSingleton<PredictionGetService>();
        services.AddSingleton<UserGetService>();
        
        return services;
    }
}
