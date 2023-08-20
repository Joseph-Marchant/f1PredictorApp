namespace F1PredictorApp.Services.FileServices;

using F1PredictorApp.Models;
using F1PredictorAppLibrary.Interfaces;
using Newtonsoft.Json;

public class PredictionLoader //: IPredictionLoader
{
    public List<User> LoadPredictions()
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\predictions.json";
        using (var r = new StreamReader(path))
        {
            var json = r.ReadToEnd();
            if(json == null) throw new FileLoadException("JSON failed to read predictions");

            var users = JsonConvert.DeserializeObject<List<User>>(json);
            if (users == null) throw new FileLoadException("JSON failed to deserialise predictions");

            return users;
        }

        throw new FileLoadException("Could not load predictions");
    }
}
