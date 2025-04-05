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
        if (prediciton.Count != 3)
        {
            return false;
        }

        var state = retrieveState.GetState();
        var drivers = await getDrivers.GetDriversAsync(state.Year, state.CurrentRound);
        return prediciton.All(driver => drivers.Contains(driver.ToUpper()));
    }
}
