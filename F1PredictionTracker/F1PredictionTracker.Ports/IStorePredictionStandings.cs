using F1PredictionTracker.Models;

namespace F1PredictionTracker.Ports;

public interface IStorePredictionStandings
{
    void StorePredictionStandings(PredictionStandings standings);
}
