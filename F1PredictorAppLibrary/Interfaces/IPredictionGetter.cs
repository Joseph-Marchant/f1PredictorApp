namespace F1PredictorAppLibrary.Interfaces;

public interface IPredictionGetter
{
    Prediction GetPrediction(List<Prediction> predictions);
}