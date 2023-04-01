namespace F1PredictorAppLibrary.Functions
{
    public interface ISmartAiGenerator
    {
        string GenerateSmartAiPrediction(List<Prediction> predictions);
    }
}