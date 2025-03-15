namespace F1PredictorAppLibrary.Functions;

using F1PredictorAppLibrary.Interfaces;

public class PredictionGenerator : IPredictionGenerator
{
    private readonly List<string> driverList;

    public PredictionGenerator(IDriverLoader driverLoader)
    {
        this.driverList = driverLoader.LoadDrivers();
    }

    public string GeneratePrediction(List<Prediction> predictions)
    {
        var AIPredictionFromList = predictions.Where(p => p.Name == "AI").FirstOrDefault();
        if (AIPredictionFromList is null) throw new InvalidOperationException("Failed to find the AI's prediction");

        var AIPrediction = this.GetRandomPrediction();

        var updatedAIPrediction = new Prediction(
                    AIPredictionFromList.Name,
                    AIPrediction[0],
                    AIPrediction[1],
                    AIPrediction[2],
                    AIPredictionFromList.Points,
                    AIPredictionFromList.OnePointers,
                    AIPredictionFromList.TwoPointers,
                    AIPredictionFromList.ThreePointers,
                    AIPredictionFromList.Position);

        predictions.Remove(AIPredictionFromList);
        predictions.Add(updatedAIPrediction);



        var confirmationMessage =
            updatedAIPrediction.Name +
            " has predicted " +
            updatedAIPrediction.First +
            updatedAIPrediction.Second +
            updatedAIPrediction.Third;

        return confirmationMessage;
    }

    private List<string> GetRandomPrediction()
    {
        var random = new Random();
        var randomPredictions = new List<string>();

        while (randomPredictions.Count < 3)
        {
            var randomIndex = random.Next(0, driverList.Count);
            randomPredictions.Add(driverList[randomIndex]);
            driverList.RemoveAt(randomIndex);
        }

        return randomPredictions;
    }
}
