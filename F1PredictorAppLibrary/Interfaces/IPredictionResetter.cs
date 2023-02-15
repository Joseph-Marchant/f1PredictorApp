namespace F1PredictorAppLibrary.Interfaces
{
    public interface IPredictionResetter
    {
        string ResetPreditions(List<Prediction> predictions);
    }
}