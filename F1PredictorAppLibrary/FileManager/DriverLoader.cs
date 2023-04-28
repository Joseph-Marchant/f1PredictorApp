using F1PredictorAppLibrary.Interfaces;
using Newtonsoft.Json;

namespace F1PredictorAppLibrary.FileManager;

public class DriverLoader : IDriverLoader
{
    public List<string> LoadDrivers()
    {
        var teams = this.LoadTeams();

        List<string> drivers = new List<string>();
        foreach(var team in teams)
        {
            drivers.Add(team.DriverOne);
            drivers.Add(team.DriverTwo);
        }

        return drivers;
    }

    public List<Team> LoadTeams()
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\drivers.json";
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
