using System.Reflection;

namespace F1PredictionTracker.Adapters.FileStorage;

public class FileStorageConfig
{
    public string PredictionsFilePath => GetFilePath("predictions.json");
    
    public string PredictionsStandingsFilePath => GetFilePath("predictionsStandings.json");
    
    public string StateFilePath => GetFilePath("state.json");

    private string GetFilePath(string fileName)
    {
        var solutionDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
        return Path.Combine(solutionDir, "F1PredictionTracker.Adapters.FileStorage", "StoredFiles", fileName);
    }
}
