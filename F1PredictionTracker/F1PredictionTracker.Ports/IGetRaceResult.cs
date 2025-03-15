using F1PredictionTracker.Models;

namespace F1PredictionTracker.Ports;

public interface IGetRaceResult
{
    Task<RaceResult> GetRaceResultAsync(string year, int round);
}
