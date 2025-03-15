using F1PredictionTracker.Ports;
using System.Text;

namespace F1PredictionTracker.Services;

public class PredictionShowService(
    IRetrievePredictionStandings retrievePredictionStandings)
{
    public async Task<string> ShowPredictionScoresAsync()
    {
        var standings = retrievePredictionStandings.GetPredictionStandings();
        standings.Users.Sort((p1, p2) => p1.Score.CompareTo(p2.Score));
        var sb = new StringBuilder();
        var position = 1;
        for (var i = 0; i < standings.Users.Count; i++)
        {
            var score = standings.Users[i].Score;
            var users = standings.Users.Where(u => u.Score == score);
            foreach (var user in users)
            {
                sb.AppendLine($"{position}{this.GetPostionSuffix(position)}: {user.Name} with score: {score}");
            }
            
            i += users.Count() - 1;
            position = i + 1;
        }
        
        return sb.ToString();
    }

    private string GetPostionSuffix(int position)
    {
        var endInt = position.ToString().Last();
        return endInt switch
        {
            '1' => "st",
            '2' => "nd",
            '3' => "rd",
            _ => "th",
        };
    }
}
