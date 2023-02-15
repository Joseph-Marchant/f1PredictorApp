namespace F1PredictorAppConsoleUI;

using F1PredictorAppLibrary.Interfaces;

public class PredictorManager : IPredictorManager
{
    private readonly IFunctionHandler functionHandler;

    public PredictorManager(IFunctionHandler functionHandler)
    {
        this.functionHandler = functionHandler;
    }

    public void RunPredictionApp()
    {
        var function = this.GetFunction();

        var message = PerformFunction(function);

        Console.WriteLine(message);
    }

    private int GetFunction()
    {
        Console.Write("Input function: ");
        var inputAsString = Console.ReadLine();
        var inputIsInt = int.TryParse(inputAsString, out var response);

        if (!inputIsInt || response < 0 || response > 7)
        {
            Console.WriteLine("Please choose a valid function\n");
            return this.GetFunction();
        }

        return response;
    }

    private string PerformFunction(int function)
    {
        return function switch
        {
            1 => this.functionHandler.SavePrediction(),
            2 => this.functionHandler.EditPrediction(),
            3 => this.functionHandler.GenerateAiPrediction(),
            4 => this.functionHandler.ScorePredictions(),
            5 => this.functionHandler.ShowScores(),
            6 => this.functionHandler.EditDriverLineUp(),
            7 => this.functionHandler.ResetPredictions(),
            _ => "Invalid Input"
        };
    }
}
