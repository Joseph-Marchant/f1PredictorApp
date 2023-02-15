namespace F1PredictorAppLibrary.Interfaces
{
    public interface IPredictionGenerator
    {
        string GeneratePrediction(List<Prediction> predictions);
    }
}