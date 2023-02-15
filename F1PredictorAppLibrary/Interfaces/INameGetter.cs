namespace F1PredictorAppLibrary.Interfaces
{
    public interface INameGetter
    {
        string GetName(List<Prediction> predictions, string requestMessage);
    }
}