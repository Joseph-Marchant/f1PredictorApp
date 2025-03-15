using F1PredictionTracker.Models;

namespace F1PredictionTracker.Ports;

public interface IStoreState
{
    void SaveState(State state);
}
