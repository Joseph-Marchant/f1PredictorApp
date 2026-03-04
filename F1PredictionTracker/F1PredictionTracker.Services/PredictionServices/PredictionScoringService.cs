using System.ComponentModel;
using F1PredictionTracker.Models;
using F1PredictionTracker.Ports;

namespace F1PredictionTracker.Services;

public class PredictionScoringService(
    IRetrievePredictions retrievePredictions,
    IStorePredictions storePredictions,
    IRetrieveState retrieveState,
    IStoreState storeState,
    IRetrievePredictionStandings retrievePredictionStandings,
    IStorePredictionStandings storePredictionStandings,
    IGetRaceResult getRaceResult)
{
    public async Task<string> ScorePredictions()
    {
        var state = retrieveState.GetState();
        var raceResult = await getRaceResult.GetRaceResultAsync(state.Year, state.CurrentRound);
        var predictions = retrievePredictions.GetPredictions();
        var predictionsStandings = retrievePredictionStandings.GetPredictionStandings();
        this.AddDefaultPredictions(predictions,  predictionsStandings);
        var responses = new List<string> {$"F1 Prediction Results {state.Year}-{state.CurrentRound}:"};
        foreach (var prediction in predictions)
        {
            var predictionScore = this.ScorePrediction(prediction, raceResult.Podium);
            var user = this.GetUser(predictionsStandings, prediction.Name);
            user.Score += predictionScore;
            var pointOrPoints = predictionScore == 1 ? "point" : "points";
            responses.Add($"{user.Name} scored {predictionScore} {pointOrPoints}.");
        }

        storePredictions.StorePredictions(new List<Prediction>());
        state.SeasonPredictions[state.CurrentRound.ToString()] = predictions;
        state.LastScoredRound = state.CurrentRound;
        state.CurrentRound += 1;
        storeState.SaveState(state);
        predictionsStandings.Round = state.CurrentRound;
        storePredictionStandings.StorePredictionStandings(predictionsStandings);
        return string.Join("\n", responses);
    }

    private void AddDefaultPredictions(IList<Prediction> predictions, PredictionStandings predictionsStandings)
    {
        foreach (var user in predictionsStandings.Users)
        {
            var prediction = predictions.FirstOrDefault(p => p.Name == user.Name);
            if (prediction != null)
            {
                continue;
            }

            var defaultPrediction = retrievePredictions.GetDefaultPrediction(user);
            if (defaultPrediction == null)
            {
                continue;
            }
            
            predictions.Add(defaultPrediction);
        }
    }

    private User GetUser(PredictionStandings predictionStandings, string userName)
    {
        var storedUser = predictionStandings.Users.FirstOrDefault(u => u.Name == userName);
        if (storedUser == null)
        {
            storedUser = new User(userName);
            predictionStandings.Users.Add(storedUser);
        }
            
        return storedUser;
    }

    private int ScorePrediction(Prediction prediction, List<string> raceResults)
    {
        var drivers = new List<string> { prediction.First, prediction.Second, prediction.Third };
        var score = 0;
        var onPodium = 0;
        var perfect = 0;
        foreach (var driver in drivers)
        {
            if (!raceResults.Contains(driver))
            {
                continue;
            }
            var driverPosition = raceResults.IndexOf(driver);
            var predictionPosition = drivers.IndexOf(driver);
            var diff = Math.Abs(driverPosition - predictionPosition);
            switch (diff)
            {
                case 0:
                    score += 3;
                    onPodium += 1;
                    perfect += 1;
                    break;
                case 1:
                    score += 2;
                    onPodium += 1;
                    break;
                case 2:
                    score += 3;
                    onPodium += 1;
                    break;
                default:
                    throw new InvalidOperationException("An error occured during prediction calculation");
            }
        }
        
        score += onPodium == 3 ? 3 : 0;
        score += perfect == 3 ? 5 : 0;
        return score;
    }
}
