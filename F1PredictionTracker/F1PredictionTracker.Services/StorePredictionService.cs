using F1PredictionTracker.Models;
using F1PredictionTracker.Ports;

namespace F1PredictionTracker.Services;

public class StorePredictionService(
    IRetrieveState retrieveState,
    IRetrievePredictions retrievePredictions,
    IStorePredictions storePredictions,
    IRetrievePredictionStandings retrievePredictionStandings)
{

    public async Task<string> StorePredictionAsync(string userName, List<string> prediction, bool errorOnNewUser)
    {
        if (prediction.Count != 3)
        {
            throw new ArgumentException("Invalid number of predictions, must be three");
        }
        
        var standings = retrievePredictionStandings.GetPredictionStandings();
        var userEntry = standings.Users.Where(s => s.Name == userName);
        if (userEntry.Count() > 1)
        {
            throw new ArgumentException("Duplicate users exist");
        }

        if (userEntry.Count() == 0 && errorOnNewUser)
        {
            throw new ArgumentException("No user found");
        }

        var user = userEntry.Count() == 0 ? new User(userName) : userEntry.First();
        var state = retrieveState.GetState();
        var predictions = retrievePredictions.GetPredictions();
        if (predictions.FirstOrDefault(p => p.Round == state.CurrentRound && p.Name == user.Name) != null)
        {
            throw new ArgumentException($"User has already made a prediction for round {state.CurrentRound}");
        }
        
        predictions.Add(new Prediction(userName, state.CurrentRound, prediction[0].ToUpper(), prediction[1].ToUpper(), prediction[2].ToUpper()));
        storePredictions.StorePredictions(predictions);
        return $"{userName}'s prediction of {prediction[0].ToPascalCase()}{prediction[1].ToPascalCase()}{prediction[2].ToPascalCase()} was successfully stored";
    }
}
