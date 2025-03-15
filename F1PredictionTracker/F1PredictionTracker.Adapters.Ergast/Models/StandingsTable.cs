namespace F1PredictionTracker.Adapters.Ergast.Models;

public class StandingsTable
{
    public string season { get; init; }
    
    public string round { get; init; }
    
    public StandingsLists[] StandingsLists { get; init; }
}
