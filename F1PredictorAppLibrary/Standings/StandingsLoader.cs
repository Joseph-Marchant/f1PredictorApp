using F1PredictorAppLibrary.Interfaces;
using Newtonsoft.Json;

namespace F1PredictorAppLibrary.Standings;

public class StandingsLoader : IStandingsLoader
{
    public List<Entrant> GetStandings()
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\Standings\standings.json";

        using (StreamReader r = new StreamReader(path))
        {
            var json = r.ReadToEnd();
            if (json == null) throw new FileLoadException("JSON failed to read");

            List<Entrant>? entrants = JsonConvert.DeserializeObject<List<Entrant>>(json);
            if (entrants == null) throw new FileLoadException("JSON failed to deserialise");

            entrants = entrants.OrderBy(e => e.Position).ToList();
            return entrants;
        }

        throw new FileLoadException("Could not load standings");
    }
}
