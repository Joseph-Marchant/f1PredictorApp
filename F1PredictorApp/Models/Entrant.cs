namespace F1PredictorApp.Models;

public class Entrant
{
    public Entrant(string name)
    {
        this.Name = name;
        this.Predictions = new List<Prediction>();
        this.Score = 0;
        this.Position = 0;
    }

    public string Name { get; set; }

    public List<Prediction> Predictions { get; set; }

    public int Score { get; set; }

    public int Position { get; set; }
}
