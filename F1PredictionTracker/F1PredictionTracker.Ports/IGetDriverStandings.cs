namespace F1PredictionTracker.Ports;

public interface IGetDriverStandings
{
    Task<List<string>> GetDriverStandingsAsync(string year, int round);
}
