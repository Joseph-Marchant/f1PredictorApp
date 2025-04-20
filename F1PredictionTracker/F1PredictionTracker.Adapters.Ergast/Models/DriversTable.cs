namespace F1PredictionTracker.Adapters.Ergast.Models;

public class DriversTable
{
    public string? season { get; init; }
    
    public string? round { get; init; }
    
    public Driver[]? drivers { get; init; }
}
