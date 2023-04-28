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
        while (true)
        {
            var function = this.GetFunction();
            var message = PerformFunction(function);
            Console.WriteLine("\n" + message + "\n");
        }
    }

    private int GetFunction()
    {
        var inputMessage = "Please choose a valid function from the following\n" +
                "1: Save Prediction\n" +
                "2: Edit Prediction\n" +
                "3: Generate AI Prediction\n" +
                "4: Score Prediction\n" +
                "5: Show Scores\n" +
                "6: Edit Driver Line Up\n" +
                "7: Reset Predictions\n" +
                "8: Get Standings\n" +
                "Function: ";

        Console.Write(inputMessage);
        var inputAsString = Console.ReadLine();
        var inputIsInt = int.TryParse(inputAsString, out var response);

        if (!inputIsInt || response < 0 || response > 9)
        {
            Console.WriteLine("Must choose between 1 and 8");
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
            8 => this.functionHandler.GetStandings(),
            _ => "Invalid Input"
        };
    }
}
