using F1PredictorAppLibrary.Interfaces;
using F1PredictorAppLibrary.Standings;

namespace F1PredictorAppLibrary.Functions;

public class SmartAiGenerator : ISmartAiGenerator
{
    private readonly IStandingsLoader standingsLoader;

    public SmartAiGenerator(IStandingsLoader standingsLoader)
    {
        this.standingsLoader = standingsLoader;
    }

    public string GenerateSmartAiPrediction(List<Prediction> predictions)
    {
        var weightedList = this.GetList();
        var AiPredictionFromList = predictions.Where(p => p.Name == "Smart AI").FirstOrDefault();
        if (AiPredictionFromList is null) throw new InvalidOperationException("Failed to find the Smart AI's prediction");

        var AIPrediction = this.GetRandomPrediction(weightedList);

        var updatedAIPrediction = new Prediction(
                    AiPredictionFromList.Name,
                    AIPrediction[0],
                    AIPrediction[1],
                    AIPrediction[2],
                    AiPredictionFromList.Points,
                    AiPredictionFromList.OnePointers,
                    AiPredictionFromList.TwoPointers,
                    AiPredictionFromList.ThreePointers,
                    AiPredictionFromList.Position);

        predictions.Remove(AiPredictionFromList);
        predictions.Add(updatedAIPrediction);

        var confirmationMessage =
            updatedAIPrediction.Name +
            " has predicted " +
            updatedAIPrediction.First +
            updatedAIPrediction.Second +
            updatedAIPrediction.Third;

        return confirmationMessage;
    }

    private List<string> GetList()
    {
        var weightedList = new List<string>();
        var standings = this.standingsLoader.GetStandings();
        foreach (var standing in standings)
        {
            this.AddToList(standing, weightedList);
        }

        return weightedList;
    }

    private List<string> AddToList(Entrant entrant, List<string> weightedList)
    {
        var quantity = 1;
        if (entrant.Position <= 10)
        {
            quantity = 11 - entrant.Position;
        }

        for (int i = 0; i < quantity; i++)
        {
            weightedList.Add(entrant.Driver);
        }

        return weightedList;
    }

    private List<string> GetRandomPrediction(List<string> weightedList)
    {
        var random = new Random();
        var randomPrediction = new List<string>();

        while (randomPrediction.Count < 3)
        {
            var randomIndex = random.Next(0, weightedList.Count);
            var driver = weightedList[randomIndex];
            randomPrediction.Add(driver);
            weightedList.RemoveAll(d => d == driver);
        }

        return randomPrediction;
    }
}
