namespace F1PredictorAppLibrary.Interfaces
{
    public interface IPredictionSaver
    {
        void SavePredictions(List<Prediction> predictions);
    }
}