using F1PredictorApp.Models;

namespace F1PredictorApp.Services
{
    public interface IRaceService
    {
        Race GetNextRace();
        List<Race> GetRaces();
        void SaveRaces(List<Race> races);
        void SetResult(List<Driver> grid, bool featureRace, Driver? fastestLap);
        void SetStartingGrid(List<Driver> grid, bool featureRace);
    }
}