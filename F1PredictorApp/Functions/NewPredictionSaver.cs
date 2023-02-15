namespace F1PredictorApp.Functions;

using F1PredictorApp.Interfaces;

public class NewSavePrediction : INewPredictionSaver
{
    private List<Prediction> predictions;
    private readonly IPredictionSaver predictionSaver;

    public NewSavePrediction(IPredictionLoader predictionLoader, IPredictionSaver predictionSaver)
    {
        this.predictions = predictionLoader.LoadPredictions();
        this.predictionSaver = predictionSaver;
    }

    public string SavePrediction(Prediction newPrediction)
    {
        var prediction = this.predictions.Where(p => p.Name == newPrediction.Name).FirstOrDefault();
        if (prediction is null)
        {
            prediction = new Prediction();
            this.predictions.Add(prediction);
        }

        prediction.Name = newPrediction.Name;
        prediction.First = newPrediction.First;
        prediction.Second = newPrediction.Second;
        prediction.Third = newPrediction.Third;

        this.predictionSaver.SavePredictions(this.predictions);

        return $"{prediction.Name}'s prediction of {prediction.First}{prediction.Second}{prediction.Third} has been saved";
    }
}
