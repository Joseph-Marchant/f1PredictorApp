namespace F1PredictorAppLibrary.Interfaces
{
    public interface IDriverGetter
    {
        List<string> GetDrivers(string requestMessage);
    }
}