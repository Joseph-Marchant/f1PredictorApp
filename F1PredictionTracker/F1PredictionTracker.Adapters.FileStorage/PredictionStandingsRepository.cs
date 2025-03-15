using F1PredictionTracker.Models;
using F1PredictionTracker.Ports;
using Newtonsoft.Json;

namespace F1PredictionTracker.Adapters.FileStorage;

public class PredictionStandingsRepository : IStorePredictionStandings, IRetrievePredictionStandings
{
    private readonly FileStorageConfig config;
    
    public PredictionStandingsRepository(FileStorageConfig config)
    {
        this.config = config;
    }

    public void StorePredictionStandings(PredictionStandings standings)
    {
        var json = JsonConvert.SerializeObject(standings, Formatting.Indented);
        File.WriteAllText(config.PredictionsStandingsFilePath, json);
    }

    public PredictionStandings GetPredictionStandings()
    {
        using var readStream = File.OpenRead(this.config.PredictionsStandingsFilePath);
        var reader = new StreamReader(readStream);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<PredictionStandings>(json) ?? throw new JsonSerializationException("Unable to deserialize prediction standings.");
    }
}
