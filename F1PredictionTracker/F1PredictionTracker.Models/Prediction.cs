namespace F1PredictionTracker.Models;

using System.Text.Json.Serialization;

public class Prediction
{
    [JsonConstructor]
    public Prediction(string name, string first, string second, string third, int? round = null, int? score = null)
    {
        this.Name = name;
        this.Round = round;
        this.First = first;
        this.Second = second;
        this.Third = third;
        this.Score = score;
    }
    
    public string Name { get; set; }
    
    public int? Round { get; set; }
    
    public string First { get; set; }
    
    public string Second { get; set; }
    
    public string Third { get; set; }

    public int? Score { get; set; }
}
