using F1PredictionTracker.Services;
using F1PredictionTracker.Adapters.FileStorage;
using F1PredictionTracker.Adapters.Ergast;
using F1PredictionTracker.Hosts.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddSingleton<ConsoleUi>()
    .AddErgastAdapter()
    .AddFileStorageAdapter()
    .AddServices();

var host = builder.Build();

var service = host.Services.GetService<ConsoleUi>() ?? throw new NullReferenceException("No Console UI has been found.");
await service.RunPredictionTrackerAsync();