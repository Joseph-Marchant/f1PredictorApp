namespace F1PredictionTracker.Ports;

public interface IGetDrivers
{
    Task<List<string>> GetDriversAsync(string year, int round);
}
