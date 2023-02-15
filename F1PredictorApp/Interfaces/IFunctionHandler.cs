namespace F1PredictorApp.Interfaces
{
    public interface IFunctionHandler
    {
        string EditDriverLineUp();
        string EditPrediction();
        string GenerateAiPrediction();
        string SavePrediction();
        string ScorePredictions();
        string SetGrandPrix();
        string ShowScores();
    }
}