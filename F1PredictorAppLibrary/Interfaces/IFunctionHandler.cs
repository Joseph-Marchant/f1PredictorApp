namespace F1PredictorAppLibrary.Interfaces
{
    public interface IFunctionHandler
    {
        string EditDriverLineUp();
        string EditPrediction();
        string GenerateAiPrediction();
        string SavePrediction();
        string ScorePredictions();
        string ResetPredictions();
        string ShowScores();
        string GetStandings();
    }
}