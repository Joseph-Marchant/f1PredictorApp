namespace F1PredictionTracker.Models;

public class RaceResult
{
    public RaceResult(int round, List<string> podium)
    {
        this.Round = round;
        this.Podium = podium;
    }
    
    public int Round { get; init; }
    
    public List<string> Podium { get; init; }
}
