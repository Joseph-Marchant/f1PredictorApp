using F1PredictionTracker.Models;

namespace F1PredictionTracker.Ports;

public interface IRetrieveState
{
    State GetState();
}
