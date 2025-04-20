using F1PredictionTracker.Services;

namespace F1PredictionTracker.Hosts.Console;

using System;

public class ConsoleUi(
    BuildUserPredictionService buildUserPredictionService,
    PredictionGenerationService predictionGenerationService,
    PredictionScoringService predictionScoringService,
    PredictionShowService predictionShowService,
    RaceResultEventService raceResultEventService)
{
    private const string QuitCommand = "quit";
    public async Task RunPredictionTrackerAsync()
    {
        while (true)
        {
            var function = this.GetFunction();
            var response = string.Empty;
            try
            {
                response = await this.PerformFunction(function);
                if (response == QuitCommand)
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                response = $"Error: {ex.Message}";
            }
            
            Console.WriteLine(response);
        }
    }

    private int GetFunction()
    {
        const string inputMessage = "Please choose a valid function from the following\n" +
            "1: Save Prediction\n" +
            "2: Generate AI Predictions\n" +
            "3: Show Scores\n" +
            "4: Score Predictions and Show Scores\n" + 
            "5: Score Predictions and Show Scores (Event Driven)\n" +
            "6: Quit\n" +
            "Function: ";
        Console.Write(inputMessage);
        var inputAsString = Console.ReadLine();
        var inputIsInt = int.TryParse(inputAsString, out var response);

        if (!inputIsInt || response < 1 || response > 6)
        {
            Console.WriteLine("Must choose between 1 and 6");
            return this.GetFunction();
        }

        return response;
    }
    
    private async Task<string> PerformFunction(int function)
    {
        var response = string.Empty;
        switch(function)
        {
            case 1:
                response = await buildUserPredictionService.BuildPredictionAsync();
                break;
            case 2:
                response = await predictionGenerationService.GeneratePredictionsAsync();
                break;
            case 3:
                response = predictionShowService.ShowPredictionScores();
                break;
            case 4:
                var scoreResponse = await predictionScoringService.ScorePredictions();
                var showResponse = predictionShowService.ShowPredictionScores();
                response = $"{scoreResponse}\n\n{showResponse}";
                break;
            case 5:
                response = await raceResultEventService.PollRaceResultAsync();
                break;
            case 6:
                response = QuitCommand;
                break;
            default:
                response = "Invalid Input";
                break;
        }
        
        return response;
    }
}
