namespace F1PredictionTracker.Adapters.Ergast.Models;

public class Result
{
    public string? number { get; init; }
    
    public string? position { get; init; }
    
    public string? positionText { get; init; }
    
    public string? points { get; init; }
    
    public Driver? Driver { get; init; }
    
    public Constructor? Constructor { get; init; }
    
    public string? grid { get; init; }
    
    public string? laps { get; init; }
    
    public string? status { get; init; }
    
    public Time? Time { get; init; }
    
    public FastestLap? FastestLap { get; init; }
}
