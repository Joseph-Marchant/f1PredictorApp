namespace F1PredictorApp.Models;

public class Driver
{
    public Driver(string name)
    {
        this.Name = name;
        this.IsActive = true;
        this.Points = 0;
        this.Position = 0;
        this.ResultHistory = new List<int>();
    }

    public string Name { get; set; }
    public bool IsActive { get; set; }
    public int Points { get; set; }
    public int Position { get; set; }
    public List<int> ResultHistory { get; set; }
}
