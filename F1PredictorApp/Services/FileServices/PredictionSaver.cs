namespace F1PredictorApp.Services.FileServices;

using F1PredictorApp.Models;
using F1PredictorAppLibrary.Interfaces;
using Newtonsoft.Json;
using System.IO;

public class PredictionSaver //: IPredictionSaver
{
    public void SavePredictions(List<User> users)
    {
        var path = @"C:\Users\jwf_m\Documents\Code\F1PredictorApp\F1PredictorAppLibrary\FileManager\predictions.json";
        var json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}
