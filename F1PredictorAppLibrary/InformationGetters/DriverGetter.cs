using F1PredictorAppLibrary.Interfaces;

namespace F1PredictorAppLibrary.InformationGetters;

public class DriverGetter : IDriverGetter
{
    private readonly List<string> driverList;

    public DriverGetter(IDriverLoader driverLoader)
    {
        driverList = driverLoader.LoadDrivers();
    }

    public List<string> GetDrivers(string requestMessage)
    {
        Console.Write(requestMessage);
        var drivers = Console.ReadLine();

        if (drivers is null) throw new ArgumentNullException("Drivers were null");

        var driversAsList = new List<string> { drivers.Substring(0, 3), drivers.Substring(3, 3), drivers.Substring(6, 3) };

        foreach (var driver in driversAsList)
        {
            if (!this.driverList.Contains(driver)) throw new ArgumentException("Driver was not found");
        }

        return driversAsList;
    }
}
