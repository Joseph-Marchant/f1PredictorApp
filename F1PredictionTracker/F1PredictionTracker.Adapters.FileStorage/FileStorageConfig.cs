using System.Reflection;

namespace F1PredictionTracker.Adapters.FileStorage;

public class FileStorageConfig
{
    public string PredictionsFilePath => GetFilePath("predictions.json");
    
    public string PredictionsStandingsFilePath => GetFilePath("predictionsStandings.json");
    
    public string StateFilePath => GetFilePath("state.json");

    private string GetFilePath(string fileName)
    {
        var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new NullReferenceException("Unable to get executing assembly location");
        return Path.Combine(directory, "StoredFiles", fileName);
    }
}
