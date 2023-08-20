using F1PredictorApp.Models;
using F1PredictorAppLibrary.Interfaces;
using Newtonsoft.Json;

namespace F1PredictorApp.Services.FileServices;

public class StandingsLoader //: IStandingsLoader
{
    public List<Driver> GetStandings()
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\Standings\standings.json";

        using (var r = new StreamReader(path))
        {
            var json = r.ReadToEnd();
            if (json == null) throw new FileLoadException("JSON failed to read standings");

            List<Driver>? entrants = JsonConvert.DeserializeObject<List<Driver>>(json);
            if (entrants == null) throw new FileLoadException("JSON failed to deserialise standings");

            entrants = entrants.OrderBy(e => e.Position).ToList();
            return entrants;
        }

        throw new FileLoadException("Could not load standings");
    }

    public string SaveStandings(List<Driver> entrants)
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\Standings\standings.json";
        var json = JsonConvert.SerializeObject(entrants, Formatting.Indented);
        File.WriteAllText(path, json);

        return "Standings Updated";
    }


    public void AddDriver(string newDriver)
    {
        var entrants = this.GetStandings();
        var newEntrant = new Driver(newDriver, 0, entrants.Count + 1, this.BlankPositionData());
        entrants.Add(newEntrant);
        this.SaveStandings(entrants);
    }

    private List<PositionData> BlankPositionData()
    {
        return new List<PositionData>
        { 
            new PositionData("1", 0),
            new PositionData("2", 0),
            new PositionData("3", 0),
            new PositionData("4", 0),
            new PositionData("5", 0),
            new PositionData("6", 0),
            new PositionData("7", 0),
            new PositionData("8", 0),
            new PositionData("9", 0),
            new PositionData("10", 0),
            new PositionData("11", 0),
            new PositionData("12", 0),
            new PositionData("13", 0),
            new PositionData("14", 0),
            new PositionData("15", 0),
            new PositionData("16", 0),
            new PositionData("17", 0),
            new PositionData("18", 0),
            new PositionData("19", 0),
            new PositionData("20", 0),
        };

    }
}
