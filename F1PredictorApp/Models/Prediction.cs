namespace F1PredictorApp.Models;

public class Prediction
{
    public Prediction(string race, Driver first, Driver second, Driver third)
    {
        this.Race = race;
        this.First = first;
        this.Second = second;
        this.Third = third;
        this.Scored = false;
    }

    public string Race { get; set; }

    public Driver First { get; set; }

    public Driver Second { get; set; }

    public Driver Third { get; set; }

    public bool Scored {  get; set; }
}
