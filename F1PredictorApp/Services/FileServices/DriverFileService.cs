using F1PredictorApp.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace F1PredictorApp.Services.FileServices;

public class DriverFileService : IFileService<Driver>
{
    public List<Driver> LoadData()
    {
        var teams = this.LoadTeams();

        List<Driver> drivers = new List<Driver>();
        foreach (var team in teams)
        {
            
        }

        return drivers;
    }

    public void SaveData(List<Driver> saveData)
    {

    }

    private List<Team> LoadTeams()
    {
        var assembly = Assembly.GetExecutingAssembly().Location;
        var path = Path.Combine(assembly, "FileService", "Data", "drivers.json");
        using (var r = new StreamReader(path))
        {
            var json = r.ReadToEnd();
            if (json == null) throw new FileLoadException("JSON failed to read drivers");

            List<Team>? drivers = JsonConvert.DeserializeObject<List<Team>>(json);
            if (drivers == null) throw new FileLoadException("JSON failed to deserialise drivers");

            return drivers;
        }

        throw new FileLoadException("Could not load predictions");
    }
}
