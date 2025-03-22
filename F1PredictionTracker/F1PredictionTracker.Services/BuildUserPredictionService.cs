namespace F1PredictionTracker.Services;

public class BuildUserPredictionService(
    StorePredictionService storePredictionService,
    UserGetService userGetService,
    PredictionGetService predictionGetService)
{
    public async Task<string> BuildPredictionAsync()
    {
        var name = userGetService.GetUser();
        var predictionList = await predictionGetService.GetPredictionAsync();
        try
        {
            var response = storePredictionService.StorePrediction(name, predictionList);
            return response;
        }
        catch (ArgumentException ex)
        {
            if (ex.Message != "No user found")
            {
                return ex.Message;
            }
        }
        
        throw new InvalidOperationException("Critical Error adding prediction");
    }

    
}
