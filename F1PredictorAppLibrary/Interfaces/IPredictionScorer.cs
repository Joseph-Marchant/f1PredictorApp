namespace F1PredictorAppLibrary.Interfaces
{
    public interface IPredictionScorer
    {
        string ScorePredictions(List<Prediction> predictions, List<string> result);
    }
}