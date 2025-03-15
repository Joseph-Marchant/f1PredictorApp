namespace F1PredictionTracker.Models;

public class Prediction
{
    public Prediction(string name, int round, string first, string second, string third)
    {
        this.Name = name;
        this.Round = round;
        this.First = first;
        this.Second = second;
        this.Third = third;
    }
    
    public string Name { get; }
    
    public int Round { get; }
    
    public string First { get; }
    
    public string Second { get; }
    
    public string Third { get; }
}
