namespace F1PredictorAppLibrary;

public class Team
{
    public string TeamName { get; set; }
    public string DriverOne { get; set; }
    public string DriverTwo { get; set;}

    public Team(string teamName, string dirverOne, string dirverTwo)
    {
        TeamName = teamName;
        DriverOne = dirverOne;
        DriverTwo = dirverTwo;
    }
}
