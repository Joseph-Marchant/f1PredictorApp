using F1PredictionTracker.Ports;

namespace F1PredictionTracker.Services;

public class RandomAiGenerationService(
    IRetrieveState retrieveState,
    IGetDrivers getDrivers,
    StorePredictionService storePredictionService)
{
    public async Task<string> GeneratePredictionsAsync()
    {
        var state = retrieveState.GetState();
        var drivers = await getDrivers.GetDriversAsync(state.Year, state.CurrentRound);
        var randomPrediction = this.GetRandomPrediction(drivers);
        var response = await storePredictionService.StorePredictionAsync("Random AI", randomPrediction, false);
        return response;
    }

    private List<string> GetRandomPrediction(List<string> drivers)
    {
        var random = new Random();
        var randomPredictions = new List<string>();

        while (randomPredictions.Count < 3)
        {
            var randomIndex = random.Next(0, drivers.Count);
            randomPredictions.Add(drivers[randomIndex]);
            drivers.RemoveAt(randomIndex);
        }

        return randomPredictions;
    }
}
