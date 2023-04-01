namespace F1PredictorAppLibrary.Functions;

using F1PredictorAppLibrary.Interfaces;

public class PredictionScorer : IPredictionScorer
{
    public string ScorePredictions(List<Prediction> predictions, List<string> raceResult)
    {
        var raceReport = string.Empty;
        foreach (var prediction in predictions)
        {
            var pointCount = 0;
            var result = string.Empty;

            if (prediction.First == raceResult[0])
            {
                pointCount++;
                result += prediction.First;
            }

            if (prediction.Second == raceResult[1])
            {
                pointCount++;
                result += prediction.Second;
            }

            if (prediction.Third == raceResult[2])
            {
                pointCount++;
                result += prediction.Third;
            }

            prediction.Points += pointCount;

            if (pointCount == 1) prediction.OnePointers += 1;
            else if (pointCount == 2) prediction.TwoPointers += 1;
            else if (pointCount == 3) prediction.ThreePointers += 1;

            if (result == string.Empty) raceReport += $"{prediction.Name} did not get any right.";
            else raceReport += $"{prediction.Name} was right about {result}.";
        }

        return raceReport;
    }
}
