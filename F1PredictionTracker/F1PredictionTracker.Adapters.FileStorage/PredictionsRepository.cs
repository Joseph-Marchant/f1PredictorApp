using F1PredictionTracker.Models;
using F1PredictionTracker.Ports;
using Newtonsoft.Json;

namespace F1PredictionTracker.Adapters.FileStorage;

public class PredictionsRepository : IStorePredictions, IRetrievePredictions
{
    private readonly FileStorageConfig config;
    
    public PredictionsRepository(FileStorageConfig config)
    {
        this.config = config;
    }
    
    public void StorePredictions(IList<Prediction> predictions)
    {
        var json = JsonConvert.SerializeObject(predictions, Formatting.Indented);
        File.WriteAllText(config.PredictionsFilePath, json);
    }

    public void StoreDefaultPrediction(Prediction prediction, User user)
    {
        var existingPredictions = this.GetDefaultPredictions();
        var existingPrediction = existingPredictions.FirstOrDefault(p => p.Name == prediction.Name);
        if (existingPrediction != null)
        {
            existingPredictions.Remove(existingPrediction);
        }
        existingPredictions.Add(prediction);
        this.StoreDefaultPredictions(existingPredictions);
    }

    public IList<Prediction> GetPredictions()
    {
        using var readStream = File.OpenRead(this.config.PredictionsFilePath);
        var reader = new StreamReader(readStream);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<Prediction>>(json) ?? throw new JsonSerializationException("Unable to deserialize predictions");
    }
    
    public Prediction? GetDefaultPrediction(User user)
    {
        var predictions = this.GetDefaultPredictions();
        return predictions.FirstOrDefault(p => p.Name == user.Name);
    }

    private void StoreDefaultPredictions(IList<Prediction> predictions)
    {
        var json = JsonConvert.SerializeObject(predictions, Formatting.Indented);
        File.WriteAllText(config.DefaultPredictionsFilePath, json);
    }

    private IList<Prediction> GetDefaultPredictions()
    {
        using var readStream = File.OpenRead(this.config.DefaultPredictionsFilePath);
        var reader = new StreamReader(readStream);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<Prediction>>(json) ?? throw new JsonSerializationException("Unable to deserialize predictions");
    }
}
