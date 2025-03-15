using F1PredictionTracker.Ports;

namespace F1PredictionTracker.Services;

public class PredictionGetService(
    IRetrieveState retrieveState,
    IGetDrivers getDrivers,
    StorePredictionService storePredictionService)
{
    public async Task<string> GetPredictionAsync()
    {
        var name = this.GetInput("Name: ", "Please enter a name: ");
        var prediction = this.GetInput("Prediction: ", "Please enter a prediction: ");
        if (prediction.Length != 9)
        {
            throw new InvalidOperationException("Prediction is not of right length. Enter three driver codes without a space, e.g. HamVerBot");
        }

        List<string> predictionList;
        try
        {
            predictionList = await this.ParsePrediction(prediction);
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message;
        }

        try
        {
            var response = await storePredictionService.StorePredictionAsync(name, predictionList, true);
            return response;
        }
        catch (ArgumentException ex)
        {
            if (ex.Message != "No user found")
            {
                return ex.Message;
            }
        }

        if (this.ConfirmAddNewUser(name))
        {
            var newUserReponse = await storePredictionService.StorePredictionAsync(name, predictionList, false);
            return newUserReponse;
        }
        
        throw new InvalidOperationException("Critical Error adding prediction");
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

    private async Task<List<string>> ParsePrediction(string prediction)
    {
        if (prediction.Length != 9)
        {
            throw new InvalidOperationException("Prediction is not of right length. Enter three driver codes without a space, e.g. HamVerBot");
        }
        
        var state = retrieveState.GetState();
        var drivers = await getDrivers.GetDriversAsync(state.Year, state.CurrentRound);
        var driversAsList = new List<string> { prediction.Substring(0, 3), prediction.Substring(3, 3), prediction.Substring(6, 3) };
        foreach (var driver in driversAsList)
        {
            if (!drivers.Contains(driver.ToUpper())) throw new InvalidOperationException($"Driver was not found: {driver}");
        }
        
        return driversAsList;
    }

    private bool ConfirmAddNewUser(string name)
    {
        var repsonse = string.Empty;
        var prompt = $"Add new user {name}? Y/N: ";
        while (string.IsNullOrWhiteSpace(repsonse))
        {
            repsonse = this.GetInput(prompt, prompt);
            if (repsonse.ToUpper() != "Y" && repsonse.ToUpper() != "N")
            {
                repsonse = string.Empty;
            }
        }
        
        return repsonse.ToUpper() == "Y";
    }
}
