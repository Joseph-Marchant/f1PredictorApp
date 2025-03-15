using F1PredictorApp.Models;

namespace F1PredictorApp.Services
{
    public interface IDriverService
    {
        List<Driver> GetDrivers();
        void SaveDrivers(List<Driver> drivers);
        void UpdateDriver(string newDriver, string oldDriver);
        void UpdateDriverScores(List<Driver> result, List<int> points, Driver? fastestLap);
    }
}