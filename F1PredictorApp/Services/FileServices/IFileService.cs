namespace F1PredictorApp.Services.FileServices;

public interface IFileService<TServiceType>
{
    public List<TServiceType> LoadData();
    public void SaveData(List<TServiceType> saveData);
}
