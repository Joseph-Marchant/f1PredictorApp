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

        var currentPosition = 0;
        var positionIncrease = 1;
        for (var index = 0; index < orderedPredictions.Count; index ++)
        {
            var prediction = orderedPredictions[index];
            var tie = this.CheckForTie(orderedPredictions, prediction, index);
            if (tie)
            {
                prediction.Position = currentPosition;
                positionIncrease++;
            }
            else
            {
                prediction.Position = currentPosition + positionIncrease;
                positionIncrease = 1;
                currentPosition++;
            }
            scores += $"{this.GetPosition(prediction.Position)}: {prediction.Name} with {prediction.Points} points.\n";
        };

        return scores;
    }

    private bool CheckForTie(List<Prediction> orderedPredictions, Prediction prediction, int index)
    {
        if (index == 0)
        { 
            return false;
        }

        var previousPrediction = orderedPredictions[index - 1];
        if (previousPrediction.Points == prediction.Points
            && previousPrediction.ThreePointers == prediction.ThreePointers
            && previousPrediction.TwoPointers == prediction.TwoPointers
            && previousPrediction.OnePointers == prediction.OnePointers) 
        {
            return true;
        }

        return false;
    }

    private string GetPosition(int position)
    {
        return position switch
        {
            1 => "1st",
            2 => "2nd",
            3 => "3rd",
            _ => $"{position}th"
        };
    }
}
