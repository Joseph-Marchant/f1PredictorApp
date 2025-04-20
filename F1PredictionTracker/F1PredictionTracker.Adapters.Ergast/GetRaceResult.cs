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
        ArgumentNullException.ThrowIfNull(raceResult.MRData);
        ArgumentNullException.ThrowIfNull(raceResult.MRData.RaceTable);
        ArgumentNullException.ThrowIfNull(raceResult.MRData.RaceTable.Races);
        var raceResults = raceResult.MRData.RaceTable.Races.FirstOrDefault(race => race.round == round.ToString()) ?? throw new NullReferenceException("Race results could not be found.");
        ArgumentNullException.ThrowIfNull(raceResults.Results);
        return this.GetDriverCodes([1, 2, 3], raceResults.Results);
    }

    private List<string> GetDriverCodes(List<int> positions, List<Result> raceResults)
    {
        var drivers = new  List<string>();
        foreach (var position in positions)
        {
            var placedDriver = raceResults.FirstOrDefault(result => result.position == position.ToString())
                ?? throw new NullReferenceException($"{position.ToString()}{this.GetPositionSuffix(position.ToString())} place driver could not be found.");
            ArgumentNullException.ThrowIfNull(placedDriver.Driver);
            ArgumentNullException.ThrowIfNull(placedDriver.Driver.code);
            drivers.Add(placedDriver.Driver.code);
        }
        
        return drivers;
    }

    private string GetPositionSuffix(string position)
    {
        var lastDigit = int.Parse(position[^1].ToString());
        return lastDigit switch
        {
            1 => "st",
            2 => "nd",
            3 => "rd",
            _ => "th"
        };
    }
}
