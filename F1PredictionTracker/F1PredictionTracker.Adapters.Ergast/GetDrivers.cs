using F1PredictionTracker.Adapters.Ergast.Models;
using F1PredictionTracker.Models;
using F1PredictionTracker.Ports;
using Newtonsoft.Json;

namespace F1PredictionTracker.Adapters.Ergast;

public class GetDrivers : IGetDrivers
{
    private readonly ErgastConfig config;

    public GetDrivers(ErgastConfig config)
    {
        this.config = config;
    }

    public async Task<List<string>> GetDriversAsync(string year, int round)
    {
        using var client = new HttpClient();
        var url = new Uri(this.config.DriversUrl(year, round));
        var response = await client.GetAsync(url);
        await using var bodyStream = await response.Content.ReadAsStreamAsync();
        var reader = new StreamReader(bodyStream);
        var responseJson = await reader.ReadToEndAsync();
        var driversResponse = JsonConvert.DeserializeObject<MRDataResponse>(responseJson);
        var drivers = this.GetDriverCodes(driversResponse, round);
        
        return drivers;
    }

    private List<string> GetDriverCodes(MRDataResponse? driversResponse, int round)
    {
        ArgumentNullException.ThrowIfNull(driversResponse);
        var driversTable = driversResponse.MRData.DriverTable;
        ArgumentNullException.ThrowIfNull(driversTable);
        if (driversTable.round != round.ToString())
        {
            throw new InvalidDataException("Divers list doesn't match round");
        }
        
        var drivers = driversTable.drivers;
        ArgumentNullException.ThrowIfNull(drivers);
        var driverCodes = drivers.Select(driver => driver.code);
        
        return driverCodes.ToList();
    }
}
