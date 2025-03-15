namespace F1PredictionTracker.Adapters.Ergast;

public class ErgastConfig
{
    public string DriversUrl(string year, int round) => $"{this.BaseUrl}/{year}/{round}/drivers";
    
    public string DriversStandingsUrl(string year) => $"{this.BaseUrl}/{year}/driverstandings";
    
    public string ResultsUrl(string year, int round) => $"{this.BaseUrl}/{year}/{round}/results";

    private string BaseUrl => "https://api.jolpi.ca/ergast/f1";
}
