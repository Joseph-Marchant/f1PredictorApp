namespace F1PredictorAppLibrary.Functions;

public class ScoreShower : IScoreShower
{
    public string ShowScores(List<Prediction> predictions)
    {
        var scores = string.Empty;
        var orderedPredictions = predictions
            .OrderByDescending(p => p.Points)
            .ThenByDescending(p => p.ThreePointers)
            .ThenByDescending(p => p.TwoPointers)
            .ThenByDescending(p => p.OnePointers)
            .ThenBy(p => p.Name)
            .ToList();

        // TODO Add position markings

        foreach (var prediction in orderedPredictions)
        {
            scores += $"{prediction.Name} has {prediction.Points}.\n";
        }

        return scores;
    }
}
