namespace F1PredictorApp.Models;

public class Race
{
    public Race(string location, int raceNumber, bool sprintWeekend)
    {
        this.Location = location;
        this.RaceNumber = raceNumber;
        this.SprintWeekend = sprintWeekend;
        this.SprintWeekendStartingGrid = new List<Driver>();
        this.SprintWeekendResult = new List<Driver>();
        this.StartingGrid = new List<Driver>();
        this.Result = new List<Driver>();
        this.FastestLap = null;
    }

    public string Location { get; set; }

    public int RaceNumber { get; set; }

    public bool SprintWeekend { get; set; }

    public List<Driver> SprintWeekendStartingGrid { get; set; }

    public List<Driver> SprintWeekendResult { get; set; }

    public List<Driver> StartingGrid { get; set; }

    public List<Driver> Result { get; set; }

    public Driver? FastestLap { get; set; }

    public bool Completed { get; set; }
}
