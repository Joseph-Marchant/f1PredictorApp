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
        var raceTable = raceResult.MRData.RaceTable ?? throw new NullReferenceException("Race table could not be found.");
        var raceResults = raceTable.Races.FirstOrDefault(race => race.round == round.ToString()) ?? throw new NullReferenceException("Race results could not be found.");
        var first = raceResults.Results.FirstOrDefault(result => result.position == "1") ?? throw new NullReferenceException("1st place driver could not be found.");
        var second = raceResults.Results.FirstOrDefault(result => result.position == "2") ?? throw new NullReferenceException("2nd place driver could not be found.");
        var third = raceResults.Results.FirstOrDefault(result => result.position == "3") ??  throw new NullReferenceException("3rd place driver could not be found.");
        
        return [first.Driver.code, second.Driver.code, third.Driver.code];
    }
}
