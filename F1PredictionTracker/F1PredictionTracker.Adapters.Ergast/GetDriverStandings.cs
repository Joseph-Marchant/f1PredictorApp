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
        var drivers = this.GetStandings(standings, round);
        
        return drivers;
    }

    private List<string> GetStandings(MRDataResponse? standings, int round)
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
        return driversStandings.Select(driver => driver.Driver.code).ToList();
    }
}
