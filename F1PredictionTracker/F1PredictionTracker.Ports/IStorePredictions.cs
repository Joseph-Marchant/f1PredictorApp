using F1PredictionTracker.Models;

namespace F1PredictionTracker.Ports;

public interface IStorePredictions
{
    /// <summary>
    /// Stores predictions in chosen repository.
    /// </summary>
    /// <param name="predictions">The predictions to store.</param>
    void StorePredictions(IList<Prediction> predictions);
    
    /// <summary>
    /// Stores the default prediction for a user in chosen repository.
    /// </summary>
    /// <param name="prediction">The predictions to store.</param>
    /// <param name="user">The user who owns the default prediction.</param>
    void StoreDefaultPrediction(Prediction prediction, User user);
}
