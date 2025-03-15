namespace F1PredictorApp.Services;

using F1PredictorApp.Models;
using F1PredictorApp.Services.FileServices;

public class RaceService(IFileService<Race> fileService) : IRaceService
{
    private readonly IFileService<Race> fileService = fileService;

    public List<Race> GetRaces()
    {
        return this.fileService.LoadData();
    }

    public Race GetNextRace()
    {
        var races = this.GetRaces();
        return races.Where(x => !x.Completed).OrderBy(x => x.RaceNumber).FirstOrDefault();
    }

    public void SetStartingGrid(List<Driver> grid, bool featureRace)
    {
        var races = this.GetRaces();
        var race = races.Where(x => !x.Completed).OrderBy(x => x.RaceNumber).FirstOrDefault();
        if (featureRace)
        {
            race.StartingGrid = grid;
        }
        else
        {
            race.SprintWeekendStartingGrid = grid;
        }

        this.SaveRaces(races);
    }

    public void SetResult(List<Driver> grid, bool featureRace, Driver? fastestLap)
    {
        var races = this.GetRaces();
        var race = races.Where(x => !x.Completed).OrderBy(x => x.RaceNumber).FirstOrDefault();
        if (featureRace)
        {
            race.Result = grid;
            race.FastestLap = fastestLap;
        }
        else
        {
            race.SprintWeekendResult = grid;
        }

        this.SaveRaces(races);
    }

    public void SaveRaces(List<Race> races)
    {
        this.fileService.SaveData(races);
    }
}
