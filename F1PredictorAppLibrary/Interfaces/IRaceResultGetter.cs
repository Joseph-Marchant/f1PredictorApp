using F1PredictorAppLibrary.Standings;

namespace F1PredictorAppLibrary.Interfaces
{
    public interface IRaceResultGetter
    {
        List<string> GetRaceResult(List<Entrant> entrants);
    }
}