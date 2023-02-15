namespace F1PredictorAppLibrary.Functions;

using F1PredictorAppLibrary.Interfaces;

public class PredictionEditor : IPredictionEditor
{
    public string EditPrediction(List<Prediction> predictions, string name, List<string> newPredictionDrivers)
    {
        var predictionFromList = RetrivePrediction(predictions, name);
        var updatedPrediction = UpdatePrediction(predictions, newPredictionDrivers, predictionFromList);

        var confirmationMessage = 
            updatedPrediction.Name +
            "'s prediction of " +
            predictionFromList.First +
            predictionFromList.Second +
            predictionFromList.Third +
            " has been changed to " +
            updatedPrediction.First +
            updatedPrediction.Second +
            updatedPrediction.Third;

        return confirmationMessage;
    }

    private Prediction RetrivePrediction(List<Prediction> predictions, string name)
    {
        var predictionFromList = predictions.Where(p => p.Name == name).FirstOrDefault();
        if (predictionFromList is null) throw new ArgumentException("Person could not be found");

        if (predictionFromList.First is null
            || predictionFromList.Second is null
            || predictionFromList.Third is null) throw new ArgumentException("Person had no prediction saved");

        return predictionFromList;
    }

    private Prediction UpdatePrediction(List<Prediction> predictions, List<string> newPredictionDrivers, Prediction predictionFromList)
    {
        var updatedPrediction = new Prediction(
                    predictionFromList.Name,
                    newPredictionDrivers[0],
                    newPredictionDrivers[1],
                    newPredictionDrivers[2],
                    predictionFromList.Points,
                    predictionFromList.OnePointers,
                    predictionFromList.TwoPointers,
                    predictionFromList.ThreePointers);

        predictions.Remove(predictionFromList);
        predictions.Add(updatedPrediction);
        return updatedPrediction;
    }
}
