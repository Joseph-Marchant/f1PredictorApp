namespace F1PredictionTracker.Adapters.Ergast.Models;

public class StandingsLists
{
    public string season { get; init; }
    
    public string round { get; init; }
    
    public DriverStandings[] DriverStandings { get; init; }
}
