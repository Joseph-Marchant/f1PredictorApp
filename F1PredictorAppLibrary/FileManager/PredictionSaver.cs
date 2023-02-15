namespace F1PredictorAppLibrary.FileManager;

using CsvHelper;
using F1PredictorAppLibrary.Interfaces;
using System.Globalization;
using System.IO;

public class PredictionSaver : IPredictionSaver
{
    public void SavePredictions(List<Prediction> predictions)
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\predictions.csv";
        using var streamWriter = new StreamWriter(path);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(predictions);
    }
}
