namespace F1PredictorAppLibrary.Interfaces
{
    public interface IDriverLoader
    {
        List<string> LoadDrivers();
        List<Team> LoadTeams();
    }
}