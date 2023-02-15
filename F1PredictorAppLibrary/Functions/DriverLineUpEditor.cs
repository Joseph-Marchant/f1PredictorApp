using F1PredictorAppLibrary.Interfaces;

namespace F1PredictorAppLibrary.Functions;

public class DriverLineUpEditor : IDriverLineUpEditor
{
    private readonly IDriverLoader driverLoader;
    private readonly IDriverSaver driverSaver;
    public DriverLineUpEditor(IDriverLoader driverLoader, IDriverSaver driverSaver)
    {
        this.driverLoader = driverLoader;
        this.driverSaver = driverSaver;
    }

    public string EditDriverLineUp()
    {
        var teams = this.driverLoader.LoadTeams();

        var teamOfDriver = this.GetTeam();
        var team = teams.Where(t => t.TeamName == teamOfDriver).FirstOrDefault();
        if (team is null) throw new ArgumentException("Invald Team");

        var driverToReplace = this.GetDriver("Driver To Replace: ");
        var newDriver = this.GetDriver("New Driver: ");

        if (team.DriverOne == driverToReplace) team.DriverOne = newDriver;
        else if (team.DriverTwo == driverToReplace) team.DriverTwo = newDriver;
        else throw new ArgumentException("Invalid Driver");

        this.driverSaver.SaveDrivers(teams);

        return $"{driverToReplace} has been replaced by {newDriver}";
    }

    private string GetTeam()
    {
        Console.Write("Which team's line up would you like to edit: ");
        var input = Console.ReadLine();
        if (input is null) throw new ArgumentNullException("No team entered");
        return input;
    }
    
    private string GetDriver(string message)
    {
        Console.Write(message);
        var input = Console.ReadLine();
        if (input is null) throw new ArgumentNullException("No driver entered");
        return input;
    }
}
