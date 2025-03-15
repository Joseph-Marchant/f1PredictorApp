using F1PredictionTracker.Models;

namespace F1PredictionTracker.Ports;

public interface IRetrievePredictionStandings
{
    PredictionStandings GetPredictionStandings();
}
