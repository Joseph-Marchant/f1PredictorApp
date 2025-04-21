namespace F1PredictionTracker.Adapters.Ergast.Models;

public class Race
{
    public string? season { get; init; }
    
    public string? round { get; init; }
    
    public string? url { get; init; }
    
    public string? raceName { get; init; }
    
    public Circuit? Circuit { get; init; }
    
    public string? date { get; init; }
    
    public string? time { get; init; }
    
    public List<Result>? Results { get; init; }
}
