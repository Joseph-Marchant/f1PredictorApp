using F1PredictorAppLibrary.Interfaces;

namespace F1PredictorAppLibrary.InformationGetters;

public class NameGetter : INameGetter
{
    public string GetName(List<Prediction> predictions, string requestMessage)
    {
        Console.Write(requestMessage);
        var name = Console.ReadLine();

        if (name is null) throw new ArgumentNullException("Name was null");

        foreach (var prediction in predictions)
        {
            if (prediction.Name == name) return name;
        }

        throw new ArgumentException("Name was not found");
    }
}
