namespace F1PredictorApp.Services.FileServices;

using F1PredictorApp.Models;
using Newtonsoft.Json;
using System.Reflection;

public class EntrantFileService : IFileService<Entrant>
{
    private string filePath;

    public EntrantFileService()
    {
        var assembly = Assembly.GetExecutingAssembly().Location;
        this.filePath = Path.Combine(assembly, "FileService", "Data", "entrants.json");
    }

    public List<Entrant> LoadData()
    {
        using var file = File.OpenRead(this.filePath);
        using var reader = new StreamReader(file);
        var json = reader.ReadToEnd();
        if (json == null) throw new FileLoadException("JSON failed to read entrants");

        var entrants = JsonConvert.DeserializeObject<List<Entrant>>(json);
        if (entrants == null) throw new FileLoadException("JSON failed to deserialise entrants");

        return entrants;
    }

    public void SaveData(List<Entrant> saveData)
    {
        var json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}
