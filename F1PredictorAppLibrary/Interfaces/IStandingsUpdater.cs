using F1PredictorAppLibrary.Standings;

namespace F1PredictorAppLibrary.Interfaces
{
    public interface IStandingsUpdater
    {
        void UpdateStandings(List<Entrant> entrants, List<string> result, string fastestLap, bool fullRace);
    }
}