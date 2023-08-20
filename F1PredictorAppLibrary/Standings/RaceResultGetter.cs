using F1PredictorAppLibrary.Interfaces;

namespace F1PredictorAppLibrary.Standings;

public class RaceResultGetter : IRaceResultGetter
{
    private IStandingsUpdater standingsUpdater;
    private readonly List<string> driverList;

    public RaceResultGetter(IStandingsUpdater standingsUpdater, IDriverLoader driverLoader)
    {
        this.standingsUpdater = standingsUpdater;
        this.driverList = driverLoader.LoadDrivers();
    }

    public List<string> GetRaceResult(List<Entrant> entrants)
    {
        var result = this.GetDrivers();
        var fullRace = this.FullRace();
        var fastestLap = this.FastestLap(fullRace);
        this.standingsUpdater.UpdateStandings(entrants, result, fastestLap, fullRace);
        return result;
    }

    private bool FullRace()
    {
        Console.WriteLine("The race was a");
        Console.WriteLine("1: Full Race");
        Console.WriteLine("2: Sprint Race");
        Console.Write("=> ");
        var input = Console.ReadLine();
        if (input == null)
        {
            return this.FullRaceError();
        }

        var inputAsInt = int.Parse(input);
        return inputAsInt switch
        {
            1 => true,
            2 => false,
            _ => this.FullRaceError()
        };
    }

    private bool FullRaceError()
    {
        Console.WriteLine("Invalid Input");
        return this.FullRace();
    }

    private string FastestLap(bool fullRace)
    {
        if (!fullRace)
        {
            return string.Empty;
        }

        Console.Write("Fastest Lap: ");
        var driver = Console.ReadLine();

        if (driver is null) throw new ArgumentNullException("Drivers were null");
        if (driver.Length != 3) throw new ArgumentException(nameof(driver), "Invalid driver");
        if (!this.driverList.Contains(driver)) throw new ArgumentException(nameof(driver), "Invalid driver");

        return driver;
    }

    private List<string> GetDrivers()
    {
        Console.Write("Results: ");
        var drivers = Console.ReadLine();

        if (drivers == null) throw new ArgumentNullException("Drivers were null");

        var driversAsList = new List<string>();

        for (var i = 0; i < drivers.Length; i += 3)
        {
            driversAsList.Add(drivers.Substring(i, 3));
        }

        foreach (var driver in driversAsList)
        {
            if (!this.driverList.Contains(driver)) throw new ArgumentException("Driver was not found");
        }

        return driversAsList;
    }
}
