namespace F1PredictionTracker.Adapters.Ergast.Models;

public class FastestLap
{
    public string rank { get; init; }
    
    public string lap { get; init; }
    
    public Time Time { get; init; }
    
    public AverageSpeed AverageSpeed { get; init; }
}
