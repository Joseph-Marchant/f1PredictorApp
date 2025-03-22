namespace F1PredictionTracker.Services;

public class PredictionGetService(PredictionValidationService predictionValidationService)
{
    public async Task<List<string>> GetPredictionAsync()
    {
        var predictionList = new List<string>();
        while (predictionList.Count == 0)
        {
            var prediction = this.GetInput("Prediction: ", "Please enter a prediction: ");
            if (prediction.Length != 9)
            {
                Console.WriteLine("Prediction is not of right length. Enter three driver codes without a space, e.g. HamVerBot");
                continue;
            }
            
            predictionList.AddRange(this.ParsePrediction(prediction));
            if (await predictionValidationService.ValidatePrediction(predictionList))
            {
                break;
            }
            
            Console.WriteLine("Prediction was invalid.");
            predictionList = new List<string>();
        }
        
        return predictionList;
    }
    
    private string GetInput(string inputPrompt, string nullResponse)
    {
        var input = string.Empty;
        Console.Write(inputPrompt);
        while (string.IsNullOrWhiteSpace(input))
        {
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(nullResponse);
            }
        }

        return input;
    }

    private List<string> ParsePrediction(string prediction)
    {
        if (prediction.Length != 9)
        {
            throw new InvalidOperationException("Prediction is not of right length. Enter three driver codes without a space, e.g. HamVerBot");
        }
        
        var driversAsList = new List<string> { prediction.Substring(0, 3), prediction.Substring(3, 3), prediction.Substring(6, 3) };
        return driversAsList;
    }
}
