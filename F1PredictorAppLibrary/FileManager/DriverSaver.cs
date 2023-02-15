namespace F1PredictorAppLibrary.FileManager;

using CsvHelper;
using F1PredictorAppLibrary.Interfaces;
using System.Globalization;

public class DriverSaver : IDriverSaver
{
    public void SaveDrivers(List<Team> teams)
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\drivers.csv";
        using var streamWriter = new StreamWriter(path);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(teams);
    }
}
