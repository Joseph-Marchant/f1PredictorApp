namespace F1PredictorAppLibrary.FileManager;

using CsvHelper;
using F1PredictorAppLibrary.Interfaces;
using System.Globalization;

public class PredictionLoader : IPredictionLoader
{
    public List<Prediction> LoadPredictions()
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\predictions.csv";
        using var streamReader = new StreamReader(path);
        using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
        var predictions = csvReader.GetRecords<Prediction>().ToList();

        return predictions;
    }
}
