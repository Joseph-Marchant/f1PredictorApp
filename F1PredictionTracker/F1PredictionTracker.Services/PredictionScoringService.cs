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
        state.LastScoredRound = state.CurrentRound;
        state.CurrentRound += 1;
        storeState.SaveState(state);
        predictionsStandings.Round = state.CurrentRound;
        storePredictionStandings.StorePredictionStandings(predictionsStandings);
        return string.Join("\n", responses);
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
        var presenceScore = 0;
        var prescenceBonus = 0;
        var positionScore = 0;
        var positionBonus = 0;

        if (raceResults.Contains(prediction.First))
        {
            presenceScore += 1;
            positionScore += raceResults[0] == prediction.First ? 1 : 0;
        }

        if (raceResults.Contains(prediction.Second))
        {
            presenceScore += 1;
            prescenceBonus += presenceScore > 1 ? 1 : 0;
            positionScore += raceResults[1] == prediction.Second ? 1 : 0;
            positionBonus += positionScore > 1 ? 1 : 0;
        }

        if (raceResults.Contains(prediction.Third))
        {
            presenceScore += 1;
            prescenceBonus += presenceScore > 1 ? 1 : 0;
            positionScore += raceResults[2] == prediction.Third ? 1 : 0;
            positionBonus += positionScore > 1 ? 1 : 0;
        }
        
        return presenceScore + prescenceBonus + positionScore + positionBonus;
    }
}
