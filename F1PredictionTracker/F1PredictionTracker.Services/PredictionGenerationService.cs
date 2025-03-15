namespace F1PredictionTracker.Services;

public class PredictionGenerationService(
    RandomAiGenerationService randomAiGenerationService,
    SmartAiGenerationService smartAiGenerationService)
{
    public async Task<string> GeneratePredictionsAsync()
    {
        var response = new List<string>();
        response.Add(await randomAiGenerationService.GeneratePredictionsAsync());
        response.Add(await smartAiGenerationService.GeneratePredictionsAsync());
        
        return string.Join('\n', response);
    }
}
