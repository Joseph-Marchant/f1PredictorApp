namespace F1PredictionTracker.Adapters.Ergast.Models;

public class MRData
{
    public string? xmlns { get; init; }
    
    public string? series { get; init; }
    
    public string? url { get; init; }
    
    public string? limit { get; init; }
    
    public string? offset { get; init; }
    
    public string? total { get; init; }
    
    public RaceTable? RaceTable { get; init; } 
    
    public StandingsTable? StandingsTable { get; init; }
    
    public DriversTable? DriverTable { get; init; }
}
