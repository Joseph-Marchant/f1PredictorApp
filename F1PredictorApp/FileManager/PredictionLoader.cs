namespace F1PredictorApp.FileManager;

using CsvHelper;
using F1PredictorApp.Interfaces;
using System.Globalization;

public class PredictionLoader : IPredictionLoader
{
    public List<Prediction> LoadPredictions()
    {
        using var streamReader = File.OpenText("predictions.csv");
        using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);

        var predictions = csvReader.GetRecords<Prediction>();

        List<Prediction> result = new List<Prediction>();
        foreach (var prediction in predictions)
        {
            Console.WriteLine(prediction);
            result.Add(prediction);
        }

        return result;
    }
}
