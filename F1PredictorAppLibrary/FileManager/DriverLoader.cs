using CsvHelper;
using F1PredictorAppLibrary.Interfaces;
using System.Globalization;

namespace F1PredictorAppLibrary.FileManager;

public class DriverLoader : IDriverLoader
{
    public List<string> LoadDrivers()
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\drivers.csv";
        using var streamReader = new StreamReader(path);
        using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
        var teams = csvReader.GetRecords<Team>().ToList();

        var drivers = new List<string>();
        foreach (var team in teams)
        {
            drivers.Add(team.DriverOne);
            drivers.Add(team.DriverTwo);
        }

        return drivers;
    }

    public List<Team> LoadTeams()
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\drivers.csv";
        using var streamReader = new StreamReader(path);
        using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
        var teams = csvReader.GetRecords<Team>().ToList();
        return teams;
    }
}
