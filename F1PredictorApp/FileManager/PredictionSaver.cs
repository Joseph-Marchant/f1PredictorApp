namespace F1PredictorApp.FileManager;

using CsvHelper;
using F1PredictorApp.Interfaces;
using System.Globalization;

public class PredictionSaver : IPredictionSaver
{
    public void SavePredictions(List<Prediction> predictions)
    {
        using var streamWriter = new StreamWriter(Console.OpenStandardOutput());
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture);

        csvWriter.WriteHeader<Prediction>();
        csvWriter.NextRecord();
        csvWriter.WriteRecords(predictions);
    }
}
