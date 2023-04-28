namespace F1PredictorAppLibrary.FileManager;

using CsvHelper;
using F1PredictorAppLibrary.Interfaces;
using Newtonsoft.Json;
using System.Globalization;

public class DriverSaver : IDriverSaver
{
    public void SaveDrivers(List<Team> teams)
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\drivers.json";
        var json = JsonConvert.SerializeObject(teams, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}
