using F1PredictionTracker.Models;
using F1PredictionTracker.Ports;
using Newtonsoft.Json;

namespace F1PredictionTracker.Adapters.FileStorage;

public class StateRepository : IStoreState, IRetrieveState
{
    private readonly FileStorageConfig config;
    
    public StateRepository(FileStorageConfig config)
    {
        this.config = config;
    }
    
    public void SaveState(State state)
    {
        var json = JsonConvert.SerializeObject(state, Formatting.Indented);
        File.WriteAllText(config.StateFilePath, json);
    }

    public State GetState()
    {
        using var readStream = File.OpenRead(this.config.StateFilePath);
        var reader = new StreamReader(readStream);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<State>(json) ?? throw new JsonSerializationException("Unable to deserialize predictions");
    }
}
