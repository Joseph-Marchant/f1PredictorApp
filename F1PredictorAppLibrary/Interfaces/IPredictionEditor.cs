using F1PredictorAppLibrary;

namespace F1PredictorAppLibrary.Interfaces
{
    public interface IPredictionEditor
    {
        string EditPrediction(List<Prediction> predictions, string name, List<string> newPredictionDrivers);
    }
}