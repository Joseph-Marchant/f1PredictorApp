namespace F1PredictionTracker.Adapters.Ergast.Models;

public class DriverStandings
{
    public string position { get; init; }
    
    public string positionText { get; init; }
    
    public string points { get; init; }
    
    public string wins { get; init; }
    
    public Driver Driver { get; init; }
    
    public Constructor[] Constructors { get; init; }
}
