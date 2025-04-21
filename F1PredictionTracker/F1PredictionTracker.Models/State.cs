namespace F1PredictionTracker.Models;

public class State
{
    public int LastScoredRound { get; set; }
    
    public int CurrentRound { get; set; }
    
    public required string Year { get; set; }
}
