using F1PredictionTracker.Ports;

namespace F1PredictionTracker.Services;

public class SmartAiGenerationService(
    IRetrieveState retrieveState,
    IGetDrivers getDrivers,
    IGetDriverStandings getDriverStandings,
    StorePredictionService storePredictionService)
{
    public async Task<string> GeneratePredictionsAsync()
    {
        var state = retrieveState.GetState();
        var driversStandings = await getDriverStandings.GetDriverStandingsAsync(state.Year, state.LastScoredRound);
        var currentDrivers = await getDrivers.GetDriversAsync(state.Year, state.CurrentRound);
        var drivers = this.GetWeightedDriversList(driversStandings, currentDrivers);
        var randomPrediction = this.GetRandomPrediction(drivers);
        var response = storePredictionService.StorePrediction("Smart AI", randomPrediction);
        return response;
    }

    private List<string> GetWeightedDriversList(List<string> standings, List<string> drivers)
    {
        if (standings.Count == 0)
        {
            return drivers;
        }
        
        var driversList = new List<string>();
        for (var i = 0; i < standings.Count; i++)
        {
            var driver = drivers.FirstOrDefault(d => d == standings[i]);
            if (driver is null)
            {
                continue;
            }
            
            var timesToAdd = drivers.Count / 2 - i;
            for (var j = 0; j < timesToAdd; j++)
            {
                driversList.Add(driver);
            }
        }
        var driversNotInStandings = drivers.Where(d => !standings.Contains(d)).ToList();
        driversList.AddRange(driversNotInStandings);

        return driversList;
    }

    private List<string> GetRandomPrediction(List<string> drivers)
    {
        var random = new Random();
        var randomPredictions = new List<string>();

        while (randomPredictions.Count < 3)
        {
            var randomIndex = random.Next(0, drivers.Count);
            var driver = drivers[randomIndex];
            randomPredictions.Add(driver);
            drivers = drivers.Where(d => d != driver).ToList();
        }

        return randomPredictions;
    }
}
