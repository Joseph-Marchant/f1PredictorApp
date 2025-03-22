namespace F1PredictionTracker.Services;

public class ActualAiGenerationService(
    StorePredictionService storePredictionService,
    PredictionGetService predictionGetService)
{
    public async Task<string> GeneratePredictionsAsync()
    {
        var prediction = await predictionGetService.GetPredictionAsync();
        var response = storePredictionService.StorePrediction("Actual AI", prediction);
        return response;
    }
}
