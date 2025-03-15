namespace F1PredictorAppLibrary.Functions;

using F1PredictorAppLibrary.Interfaces;

public class NewPredictionSaver : INewPredictionSaver
{
    public string SavePrediction(Prediction newPrediction, List<Prediction> predictions)
    {
        var prediction = predictions.Where(p => p.Name == newPrediction.Name).FirstOrDefault();
        if (prediction is null)
        {
            prediction = new Prediction();
            predictions.Add(prediction);
        }

        if (!string.IsNullOrEmpty(prediction.First) || !string.IsNullOrEmpty(prediction.Second) || !string.IsNullOrEmpty(prediction.Third))
        {
            throw new ArgumentException($"{prediction.Name} already has a prediction saved");
        }

        if (newPrediction.First is null || newPrediction.Second is null || newPrediction.Third is null)
        {
            throw new ArgumentNullException($"Prediction contained null elements");
        }

        prediction.Name = newPrediction.Name;
        prediction.First = newPrediction.First;
        prediction.Second = newPrediction.Second;
        prediction.Third = newPrediction.Third;

        return $"{prediction.Name} has predicted {prediction.First}{prediction.Second}{prediction.Third}";
    }
}
