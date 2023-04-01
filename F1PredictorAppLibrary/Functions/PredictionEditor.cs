namespace F1PredictorAppLibrary.Functions;

using F1PredictorAppLibrary.Interfaces;
using System.Text;

public class PredictionEditor : IPredictionEditor
{
    public string EditPrediction(List<Prediction> predictions, string name, List<string> newPredictionDrivers)
    {
        var predictionFromList = RetrivePrediction(predictions, name);
        var updatedPrediction = UpdatePrediction(predictions, newPredictionDrivers, predictionFromList);

        var confirmationMessage = new StringBuilder();
        confirmationMessage.Append(updatedPrediction.Name);
        confirmationMessage.Append("'s prediction of ");
        confirmationMessage.Append(predictionFromList.First);
        confirmationMessage.Append(predictionFromList.Second);
        confirmationMessage.Append(predictionFromList.Third);
        confirmationMessage.Append(" has been changed to ");
        confirmationMessage.Append(updatedPrediction.First);
        confirmationMessage.Append(updatedPrediction.Second);
        confirmationMessage.Append(updatedPrediction.Third);

        return confirmationMessage.ToString();
    }

    private Prediction RetrivePrediction(List<Prediction> predictions, string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("No name was passed through");
        var predictionFromList = predictions.Where(p => p.Name == name).FirstOrDefault();
        if (predictionFromList is null) throw new ArgumentException("Person could not be found");

        if (predictionFromList.First is null
            || predictionFromList.Second is null
            || predictionFromList.Third is null) throw new ArgumentException("Person had no prediction saved");

        return predictionFromList;
    }

    private Prediction UpdatePrediction(List<Prediction> predictions, List<string> newPredictionDrivers, Prediction predictionFromList)
    {
        if (newPredictionDrivers is null) throw new ArgumentNullException("No new dirvers were given");
        if (newPredictionDrivers.Count != 3) throw new ArgumentException("Not enough drivers input");

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
