namespace F1PredictorAppLibrary.Functions;

public class ScoreShower : IScoreShower
{
    public string ShowScores(List<Prediction> predictions)
    {
        var scores = string.Empty;
        foreach (var prediction in predictions)
        {
            scores += $"{prediction.Name} has {prediction.Points}.\n";
        }

        return scores;
    }
}
