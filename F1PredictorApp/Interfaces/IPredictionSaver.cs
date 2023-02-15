namespace F1PredictorApp.Interfaces
{
    public interface IPredictionSaver
    {
        void SavePredictions(List<Prediction> predictions);
    }
}