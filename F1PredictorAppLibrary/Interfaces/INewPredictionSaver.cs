namespace F1PredictorAppLibrary.Interfaces
{
    public interface INewPredictionSaver
    {
        string SavePrediction(Prediction newPrediction, List<Prediction> predictions);
    }
}