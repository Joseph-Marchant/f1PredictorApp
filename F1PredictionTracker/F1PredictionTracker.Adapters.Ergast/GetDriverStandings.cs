using F1PredictionTracker.Adapters.Ergast.Models;
using F1PredictionTracker.Models;
using F1PredictionTracker.Ports;
using Newtonsoft.Json;

namespace F1PredictionTracker.Adapters.Ergast;

public class GetDriverStandings : IGetDriverStandings
{
    private readonly ErgastConfig config;

    public GetDriverStandings(ErgastConfig config)
    {
        this.config = config;
    }

    public async Task<List<string>> GetDriverStandingsAsync(string year, int round)
    {
        using var client = new HttpClient();
        var url = new Uri(this.config.DriversStandingsUrl(year));
        var response = await client.GetAsync(url);
        await using var bodyStream = await response.Content.ReadAsStreamAsync();
        var reader = new StreamReader(bodyStream);
        var responseJson = await reader.ReadToEndAsync();
        var standings = JsonConvert.DeserializeObject<MRDataResponse>(responseJson);
        var drivers = this.GetPodiumResult(standings, round);
        
        return drivers;
    }

    private List<string> GetPodiumResult(MRDataResponse? standings, int round)
    {
        var driversInOrder = new List<string>();
        ArgumentNullException.ThrowIfNull(standings);
        var standingsTable = standings.MRData.StandingsTable;
        ArgumentNullException.ThrowIfNull(standingsTable);
        var standingsList = standingsTable.StandingsLists.FirstOrDefault();
        if (standingsList == null && standingsTable.round == null)
        {
            return driversInOrder;
        }

        ArgumentNullException.ThrowIfNull(standingsList);
        if (standingsList.round != round.ToString())
        {
            throw new InvalidOperationException("Standings table does not match round");
        }
        
        var driversStandings = standingsList.DriverStandings;
        ArgumentNullException.ThrowIfNull(driversStandings);
        for (var i = 0; i < driversStandings.Length; i++)
        {
            var driver = driversStandings.Where(driver => driver.position == i.ToString()).Select(driver => driver.Driver.code).FirstOrDefault();
            if (string.IsNullOrEmpty(driver))
            {
                throw new ArgumentNullException($"Cannot find standing for P{i}");
            }

            driversInOrder.Add(driver);
        }
        
        return driversInOrder;
    }
}
