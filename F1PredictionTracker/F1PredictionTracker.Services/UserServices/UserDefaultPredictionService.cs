namespace F1PredictionTracker.Services;

public class UserDefaultPredictionService(
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
            var response = storePredictionService.StoreDefaultPrediction(name, predictionList);
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
