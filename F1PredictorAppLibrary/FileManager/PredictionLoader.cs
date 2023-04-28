namespace F1PredictorAppLibrary.FileManager;

using F1PredictorAppLibrary.Interfaces;
using Newtonsoft.Json;

public class PredictionLoader : IPredictionLoader
{
    public List<Prediction> LoadPredictions()
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\predictions.json";
        using (var r = new StreamReader(path))
        {
            var json = r.ReadToEnd();
            if(json == null) throw new FileLoadException("JSON failed to read predictions");

            List<Prediction>? predictions = JsonConvert.DeserializeObject<List<Prediction>>(json);
            if (predictions == null) throw new FileLoadException("JSON failed to deserialise predictions");

            return predictions;
        }

        throw new FileLoadException("Could not load predictions");
    }
}
