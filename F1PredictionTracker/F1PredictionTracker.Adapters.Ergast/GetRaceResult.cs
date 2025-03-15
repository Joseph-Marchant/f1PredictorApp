using F1PredictionTracker.Adapters.Ergast.Models;
using F1PredictionTracker.Models;
using F1PredictionTracker.Ports;
using Newtonsoft.Json;

namespace F1PredictionTracker.Adapters.Ergast;

public class GetRaceResult : IGetRaceResult
{
    private readonly ErgastConfig config;

    public GetRaceResult(ErgastConfig config)
    {
        this.config = config;
    }

    public async Task<RaceResult> GetRaceResultAsync(string year, int round)
    {
        using var client = new HttpClient();
        var url = new Uri(this.config.ResultsUrl(year, round));
        var response = await client.GetAsync(url);
        await using var bodyStream = await response.Content.ReadAsStreamAsync();
        var reader = new StreamReader(bodyStream);
        var responseJson = await reader.ReadToEndAsync();
        var raceResult = JsonConvert.DeserializeObject<MRDataResponse>(responseJson);
        var podium = this.GetPodiumResult(raceResult, round);
        
        return new RaceResult(round, podium);
    }

    private List<string> GetPodiumResult(MRDataResponse? raceResult, int round)
    {
        ArgumentNullException.ThrowIfNull(raceResult);
        var raceTable = raceResult.MRData.RaceTable;
        ArgumentNullException.ThrowIfNull(raceTable);
        var raceResults = raceTable.Races.FirstOrDefault(race => race.round == round.ToString());
        ArgumentNullException.ThrowIfNull(raceResults);
        var first = raceResults.Results.FirstOrDefault(result => result.position == "1");
        ArgumentNullException.ThrowIfNull(first);
        var second = raceResults.Results.FirstOrDefault(result => result.position == "2");
        ArgumentNullException.ThrowIfNull(second);
        var third = raceResults.Results.FirstOrDefault(result => result.position == "3");
        ArgumentNullException.ThrowIfNull(third);
        
        return [first.Driver.code, second.Driver.code, third.Driver.code];
    }
}
