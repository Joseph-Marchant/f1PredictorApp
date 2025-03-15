namespace F1PredictorApp.Services;

using F1PredictorApp.Models;

public interface IScoringService
{
    int GetScore(List<Driver> predictedDrivers, List<Driver> result, List<Driver> startingGrid, Driver? fastestLap);
}