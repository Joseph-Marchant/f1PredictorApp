using F1PredictionTracker.Ports;

namespace F1PredictionTracker.Services;

public class PredictionValidationService(
    IRetrievePredictionStandings retrievePredictionStandings,
    IRetrieveState retrieveState,
    IGetDrivers getDrivers)
{
    public bool ValidateUser(string userName)
    {
        var standings = retrievePredictionStandings.GetPredictionStandings();
        var userEntry = standings.Users.Where(s => s.Name == userName);
        return userEntry.Any();
    }

    public async Task<bool> ValidatePrediction(List<string> prediciton)
    {
        if (prediciton.Count == 3)
        {
            return false;
        }

        foreach (var driver in prediciton)
        {
            var state = retrieveState.GetState();
            var drivers = await getDrivers.GetDriversAsync(state.Year, state.CurrentRound);
            if (!drivers.Contains(driver.ToUpper())) return false;
        }

        return true;
    }
}
