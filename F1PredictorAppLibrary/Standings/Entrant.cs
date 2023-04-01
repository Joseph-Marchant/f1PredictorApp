namespace F1PredictorAppLibrary.Standings;

public class Entrant
{
    public Entrant(string driver, int points, int position, List<PositionData> resultHistory)
    {
        this.Driver = driver;
        this.Points = points;
        this.Position = position;
        this.ResultHistory = resultHistory;
    }

    public string Driver { get; set; }
    public int Points { get; set; }
    public int Position { get; set; }
    public List<PositionData> ResultHistory { get; set; }
}
