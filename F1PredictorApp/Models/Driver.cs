namespace F1PredictorApp.Models;

public class Driver
{
    public Driver(string name, int points, int position, List<PositionData> resultHistory)
    {
        this.Name = name;
        this.Points = points;
        this.Position = position;
        this.ResultHistory = resultHistory;
    }

    public string Name { get; set; }
    public int Points { get; set; }
    public int Position { get; set; }
    public List<PositionData> ResultHistory { get; set; }
}
