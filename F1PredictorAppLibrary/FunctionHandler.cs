namespace F1PredictorAppLibrary;

using F1PredictorAppLibrary.Interfaces;

public class FunctionHandler : IFunctionHandler
{
    private readonly IServiceContainer serviceContainer;

    public FunctionHandler(IServiceContainer serviceContainer)
    {
        this.serviceContainer = serviceContainer;
    }

    public string SavePrediction()
    {
        var predictions = this.LoadPredictions();
        try
        {
            var newPrediction = this.serviceContainer.predictionGetter.GetPrediction(predictions);
            var responseMessage = this.serviceContainer.newPredictionSaver.SavePrediction(newPrediction, predictions);
            this.serviceContainer.predictionSaver.SavePredictions(predictions);
            return responseMessage;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string EditPrediction()
    {
        var predictions = this.LoadPredictions();
        try
        {
            var name = this.serviceContainer.nameGetter.GetName(predictions, "Whose prediction would you like to change: ");
            var newPredictionDrivers = this.serviceContainer.driverGetter.GetDrivers("New Prediction: ");
            var responseMessage = this.serviceContainer.predictionEditor.EditPrediction(predictions, name, newPredictionDrivers);
            this.serviceContainer.predictionSaver.SavePredictions(predictions);
            return responseMessage;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string GenerateAiPrediction()
    {
        var predictions = this.LoadPredictions();
        try
        {
            var responseMessage = this.serviceContainer.predictionGenerator.GeneratePrediction(predictions);
            this.serviceContainer.predictionSaver.SavePredictions(predictions);
            return responseMessage;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string ScorePredictions()
    {
        var predictions = this.LoadPredictions();
        var result = this.serviceContainer.driverGetter.GetDrivers("Result: ");
        try
        {
            var responseMessage = this.serviceContainer.predictionScorer.ScorePredictions(predictions, result);
            this.serviceContainer.predictionSaver.SavePredictions(predictions);
            return responseMessage;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string ShowScores()
    {
        var predictions = this.LoadPredictions();
        try
        {
            return this.serviceContainer.scoreShower.ShowScores(predictions);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string EditDriverLineUp()
    {
        try
        {
            return this.serviceContainer.driverLineUpEditor.EditDriverLineUp();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string ResetPredictions()
    {
        var predictions = this.LoadPredictions();
        try
        {
            return this.serviceContainer.predictionResetter.ResetPreditions(predictions);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private List<Prediction> LoadPredictions()
    {
        return this.serviceContainer.predictionLoader.LoadPredictions();
    }
}
