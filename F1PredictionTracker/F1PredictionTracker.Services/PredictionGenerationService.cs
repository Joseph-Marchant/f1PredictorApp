namespace F1PredictionTracker.Services;

public class PredictionGenerationService(
    ActualAiGenerationService actualAiGenerationService,
    RandomAiGenerationService randomAiGenerationService,
    SmartAiGenerationService smartAiGenerationService)
{
    public async Task<string> GeneratePredictionsAsync()
    {
        var response = new List<string>();
        response.Add(await actualAiGenerationService.GeneratePredictionsAsync());
        response.Add(await randomAiGenerationService.GeneratePredictionsAsync());
        response.Add(await smartAiGenerationService.GeneratePredictionsAsync());
        
        return string.Join('\n', response);
    }
}
