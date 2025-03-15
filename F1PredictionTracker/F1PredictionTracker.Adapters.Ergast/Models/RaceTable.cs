namespace F1PredictionTracker.Adapters.Ergast.Models;

public class RaceTable
{
    public string season { get; init; }
    
    public string round { get; init; }
    
    public List<Race> Races { get; init; }
}
