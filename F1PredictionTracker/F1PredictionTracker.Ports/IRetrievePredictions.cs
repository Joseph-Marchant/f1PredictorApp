using F1PredictionTracker.Models;

namespace F1PredictionTracker.Ports;

public interface IRetrievePredictions
{
    /// <summary>
    /// Gets a list of the predictions from the repository.
    /// </summary>
    /// <returns>A list of stored <see cref="Prediction"/>.</returns>
    IList<Prediction> GetPredictions();
}
