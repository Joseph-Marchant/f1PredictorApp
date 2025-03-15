namespace F1PredictorApp.Functions;

using F1PredictorApp.Services;

public class GenerateAiPredictionFunction
{
    private readonly IDriverService driverService;
    private readonly IEntrantService entrantService;
    private readonly IRaceService raceService;

    public GenerateAiPredictionFunction(IDriverService driverService, IEntrantService entrantService, IRaceService raceService)
    {
        this.driverService = driverService;
        this.entrantService = entrantService;
        this.raceService = raceService;
    }

    public void GenerateAiPredictions()
    {
        this.SaveAiPrediction();
    }

    private void SaveAiPrediction()
    {
        var ai = this.entrantService.GetEntrant("AI");
        var drivers = this.driverService.GetDrivers();
        var driverList = drivers.Select(x => x.Name).ToList();
        var prediction = this.GetRandomPrediction(driverList);
        var predictedDrivers = drivers.Where(x => prediction.Contains(x.Name)).ToList();
        var race = this.raceService.GetNextRace();
        this.entrantService.SavePrediction("AI", race, predictedDrivers);
    }

    private void SaveSmartAiPrediction()
    {
        var ai = this.entrantService.GetEntrant("AI");
        var drivers = this.driverService.GetDrivers();
        var driverList = drivers.Select(x => x.Name).ToList();
        var prediction = this.GetRandomPrediction(driverList);
        var predictedDrivers = drivers.Where(x => prediction.Contains(x.Name)).ToList();
        var race = this.raceService.GetNextRace();
        this.entrantService.SavePrediction("AI", race, predictedDrivers);
    }

    private List<string> GetRandomPrediction(List<string> driverList)
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
