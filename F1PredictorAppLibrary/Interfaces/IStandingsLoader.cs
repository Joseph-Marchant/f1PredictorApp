using F1PredictorAppLibrary.Standings;

namespace F1PredictorAppLibrary.Interfaces
{
    public interface IStandingsLoader
    {
        List<Entrant> GetStandings();
        string SaveStandings(List<Entrant> entrants);
        void AddDriver(string newDriver);
    }
}