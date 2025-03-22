using F1PredictionTracker.Services;

namespace F1PredictionTracker.Hosts.Console;

using System;

public class ConsoleUi(
    BuildUserPredictionService buildUserPredictionService,
    PredictionGenerationService predictionGenerationService,
    PredictionScoringService predictionScoringService,
    PredictionShowService predictionShowService)
{
    private const string QuitCommand = "quit";
    public async Task RunPredictionTrackerAsync()
    {
        while (true)
        {
            var function = this.GetFunction();
            var resposne = await this.PerformFunction(function);
            if (resposne == QuitCommand)
            {
                break;
            }

            Console.WriteLine(resposne);
        }
    }

    private int GetFunction()
    {
        var inputMessage = "Please choose a valid function from the following\n" +
            "1: Save Prediction\n" +
            "2: Generate AI Predictions\n" +
            "3: Score Prediction\n" +
            "4: Show Scores\n" +
            "5: Quit\n" +
            "Function: ";
        Console.Write(inputMessage);
        var inputAsString = Console.ReadLine();
        var inputIsInt = int.TryParse(inputAsString, out var response);

        if (!inputIsInt || response < 1 || response > 5)
        {
            Console.WriteLine("Must choose between 1 and 5");
            return this.GetFunction();
        }

        return response;
    }
    
    private async Task<string> PerformFunction(int function)
    {
        return function switch
        {
            1 => await buildUserPredictionService.BuildPredictionAsync(),
            2 => await predictionGenerationService.GeneratePredictionsAsync(),
            3 => await predictionScoringService.ScorePredictions(),
            4 => await predictionShowService.ShowPredictionScoresAsync(),
            5 => QuitCommand,
            _ => "Invalid Input"
        };
    }
}
