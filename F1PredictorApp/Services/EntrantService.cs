namespace F1PredictorApp.Services;

using F1PredictorApp.Models;
using F1PredictorApp.Services.FileServices;

public class EntrantService : IEntrantService
{
    private readonly IFileService<Entrant> fileService;
    private readonly IScoringService scoringService;

    public EntrantService(IFileService<Entrant> fileService, IScoringService scoringService)
    {
        this.fileService = fileService;
        this.scoringService = scoringService;
    }

    public List<Entrant> GetEntrants()
    {
        return this.fileService.LoadData();
    }

    public Entrant GetEntrant(string name)
    {
        return this.GetEntrants().Where(x => x.Name == name).FirstOrDefault();
    }

    public void SavePrediction(string name, Race race, List<Driver> predictionList)
    {
        var entrants = this.GetEntrants();
        var prediction = new Prediction(race.Location, predictionList[0], predictionList[1], predictionList[2]);
        var entrant = entrants.Where(x => x.Name == name).FirstOrDefault() ?? throw new ArgumentNullException($"{name} not found");
        entrant.Predictions.Add(prediction);
        this.SaveEntrants(entrants);
    }

    public void SaveEntrants(List<Entrant> entrants)
    {
        this.fileService.SaveData(entrants);
    }

    public void ScorePredictions(Race race, bool featureRace)
    {
        var entrants = this.GetEntrants();
        foreach (var entrant in entrants)
        {
            var prediction = entrant.Predictions.Where(x => !x.Scored).FirstOrDefault();
            if (prediction is null)
            {
                continue;
            }

            var predictedDrivers = new List<Driver> { prediction.First, prediction.Second, prediction.Third };
            if (featureRace)
            {
                entrant.Score += this.scoringService.GetScore(predictedDrivers, race.Result, race.StartingGrid, race.FastestLap);
            }
        }

        this.SaveEntrants(this.OrderEntrants(entrants));
    }

    private List<Entrant> OrderEntrants(List<Entrant> entrants)
    {
        entrants.Sort((x, y) => x.Score > y.Score ? -1 : 1);
        return entrants;
    }
}
