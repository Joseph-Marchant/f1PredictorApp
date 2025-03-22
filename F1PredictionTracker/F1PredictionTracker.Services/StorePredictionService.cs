using F1PredictionTracker.Models;
using F1PredictionTracker.Ports;

namespace F1PredictionTracker.Services;

public class StorePredictionService(
    IRetrieveState retrieveState,
    IRetrievePredictions retrievePredictions,
    IStorePredictions storePredictions,
    IRetrievePredictionStandings retrievePredictionStandings)
{

    public string StorePrediction(string userName, List<string> prediction)
    {
        if (prediction.Count != 3)
        {
            throw new ArgumentException("Invalid number of predictions, must be three");
        }
        
        var standings = retrievePredictionStandings.GetPredictionStandings();
        var user = standings.Users.FirstOrDefault(s => s.Name == userName) ?? new User(userName);
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
