namespace F1PredictorAppConsoleUI;

using F1PredictorAppLibrary;
using F1PredictorAppLibrary.FileManager;
using F1PredictorAppLibrary.Functions;
using F1PredictorAppLibrary.InformationGetters;
using F1PredictorAppLibrary.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class Program
{

    static void Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IPredictorManager, PredictorManager>();
                services.AddTransient<IFunctionHandler, FunctionHandler>();
                services.AddTransient<INewPredictionSaver, NewPredictionSaver>();
                services.AddTransient<IPredictionEditor, PredictionEditor>();
                services.AddSingleton<IPredictionGenerator, PredictionGenerator>();
                services.AddSingleton<IPredictionScorer, PredictionScorer>();
                services.AddSingleton<IScoreShower, ScoreShower>();
                services.AddSingleton<IDriverLineUpEditor, DriverLineUpEditor>();
                services.AddSingleton<IPredictionResetter, PredictionResetter>();
                services.AddTransient<IPredictionSaver, PredictionSaver>();
                services.AddTransient<IPredictionLoader, PredictionLoader>();
                services.AddTransient<IDriverLoader, DriverLoader>();
                services.AddSingleton<IDriverSaver, DriverSaver>();
                services.AddSingleton<INameGetter, NameGetter>();
                services.AddSingleton<IDriverGetter, DriverGetter>();
                services.AddSingleton<IPredictionGetter,PredictionGetter>();
                services.AddSingleton<IServiceContainer, ServiceContainer>();
            })
            .Build();

        var service = host.Services.GetService<IPredictorManager>();
        if (service is not null) service.RunPredictionApp();
    }
}