using F1PredictionTracker.Models;

namespace F1PredictionTracker.Ports;

public interface IStorePredictions
{
    /// <summary>
    /// Stores predictions in chosen repository.
    /// </summary>
    /// <param name="predictions">The predictions to store.</param>
    void StorePredictions(IList<Prediction> predictions);
}
