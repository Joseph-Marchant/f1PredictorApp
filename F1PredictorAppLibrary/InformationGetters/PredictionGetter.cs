namespace F1PredictorAppConsoleUI;

using F1PredictorAppLibrary;
using F1PredictorAppLibrary.Interfaces;

public class PredictionGetter : IPredictionGetter
{
    private readonly INameGetter nameGetter;
    private readonly IDriverGetter driverGetter;
    public PredictionGetter(INameGetter nameGetter, IDriverGetter driverGetter)
    {
        this.nameGetter = nameGetter;
        this.driverGetter = driverGetter;
    }

    public Prediction GetPrediction(List<Prediction> predictions)
    {
        var name = nameGetter.GetName(predictions, "Name: ");

        var prediction = driverGetter.GetDrivers("Prediction: ");

        var newPrediction = new Prediction(name, prediction[0], prediction[1], prediction[2], 0, 0, 0, 0, 0);

        return newPrediction;
    }
}
