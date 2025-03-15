namespace F1PredictorApp.Services.FileServices;

using F1PredictorApp.Models;
using Newtonsoft.Json;
using System.Reflection;

public class DriverFileService : IFileService<Driver>
{
    private string filePath;

    public DriverFileService()
    {
        var assembly = Assembly.GetExecutingAssembly().Location;
        this.filePath = Path.Combine(assembly, "FileService", "Data", "drivers.json");
    }

    public List<Driver> LoadData()
    {
        using var file = File.OpenRead(this.filePath);
        using var reader = new StreamReader(file);
        var json = reader.ReadToEnd();
        if (json == null) throw new FileLoadException("JSON failed to read drivers");

        var drivers = JsonConvert.DeserializeObject<List<Driver>>(json);
        if (drivers == null) throw new FileLoadException("JSON failed to deserialise drivers");

        return drivers;
    }

    public void SaveData(List<Driver> saveData)
    {
        var json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}
