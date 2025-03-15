namespace F1PredictorApp.Services;

using F1PredictorApp.Models;
using F1PredictorApp.Services.FileServices;

public class DriverService : IDriverService
{
    private readonly IFileService<Driver> fileService;

    public DriverService(IFileService<Driver> fileService)
    {
        this.fileService = fileService;
    }

    public List<Driver> GetDrivers()
    {
        return this.fileService.LoadData();
    }

    public void SaveDrivers(List<Driver> drivers)
    {
        this.fileService.SaveData(drivers);
    }

    public void UpdateDriver(string newDriver, string oldDriver)
    {
        var drivers = this.GetDrivers();
        var driverToUpdate = drivers.Where(x => x.Name == oldDriver).FirstOrDefault() ?? throw new ArgumentNullException($"{oldDriver} not found");
        driverToUpdate.IsActive = false;
        drivers.Add(new Driver(newDriver));
        this.SaveDrivers(drivers);
    }

    public void UpdateDriverScores(List<Driver> result, List<int> points, Driver? fastestLap)
    {
        var drivers = this.GetDrivers();
        for (var i = 0; i < result.Count; i++)
        {
            var driver = drivers.Where(x => x.Name == result[i].Name).FirstOrDefault() ?? throw new ArgumentNullException($"${result[i].Name} not found");
            driver.Points += points[i];
            driver.ResultHistory.Add(i + 1);
            driver.ResultHistory.Sort((x, y) => x > y ? 1 : -1);

            if (fastestLap != null && fastestLap.Name == driver.Name)
            {
                driver.Points++;
            }
        }

        this.SaveDrivers(this.UpdateStandings(drivers));
    }

    private List<Driver> UpdateStandings(List<Driver> drivers)
    {
        drivers.Sort((x, y) =>
        {
            var xScore = x.Points;
            var yScore = y.Points;
            if (xScore > yScore) return -1;
            else if (xScore < yScore) return 1;
            else
            {
                for (int i = 0; i < x.ResultHistory.Count; i++)
                {
                    if (x.ResultHistory[i] > y.ResultHistory[i]) return 1;
                    else if (x.ResultHistory[i] < y.ResultHistory[i]) return -1;
                }

                throw new ArgumentException($"{x.Name} and {y.Name} cannot be seperated.");
            }
        });

        return drivers;
    }
}
