using F1PredictorAppLibrary.Interfaces;

namespace F1PredictorAppLibrary.Functions;

public class PredictionResetter : IPredictionResetter
{
    private readonly IPredictionSaver predictionSaver;

    public PredictionResetter(IPredictionSaver predictionSaver)
    {
        this.predictionSaver = predictionSaver;
    }

    public string ResetPreditions(List<Prediction> predictions)
    {
        foreach (var prediction in predictions)
        {
            prediction.First = string.Empty;
            prediction.Second = string.Empty;
            prediction.Third = string.Empty;
        }

        this.predictionSaver.SavePredictions(predictions);

        return "Predictions reset";
    }
}
