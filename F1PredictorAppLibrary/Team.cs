namespace F1PredictorAppLibrary;

public class Team
{
    public string TeamName { get; set; }
    public string DriverOne { get; set; }
    public string DriverTwo { get; set;}

    public Team(string TeamName, string DriverOne, string DriverTwo)
    {
        this.TeamName = TeamName;
        this.DriverOne = DriverOne;
        this.DriverTwo = DriverTwo;
    }
}
