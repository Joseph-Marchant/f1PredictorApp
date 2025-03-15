namespace F1PredictorApp.Services.FileServices;

public interface IFileService<TDataType>
{
    public List<TDataType> LoadData();
    public void SaveData(List<TDataType> saveData);
}
