namespace F1PredictorAppLibrary;

public class Prediction
{
    public string Name { get; set; }
    public string First { get; set; }
    public string Second { get; set; }
    public string Third { get; set; }
    public int Points { get; set; }
    public int OnePointers { get; set; }
    public int TwoPointers { get; set; }
    public int ThreePointers { get; set; }

    public Prediction(string name, string first, string second, string third, int points, int onePointers, int twoPointers, int thirdPointers)
    {
        this.Name = name;
        this.First = first;
        this.Second = second;
        this.Third = third;
        this.Points = points;
        this.OnePointers = onePointers;
        this.TwoPointers = twoPointers;
        this.ThreePointers = thirdPointers;
    }

    public Prediction()
    {
        this.Name = string.Empty;
        this.First = null;
        this.Second = null;
        this.Third = null;
        this.Points = 0;
        this.OnePointers = 0;
        this.TwoPointers = 0;
        this.ThreePointers = 0;
    }
}
