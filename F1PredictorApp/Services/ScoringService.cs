using F1PredictorApp.Models;

namespace F1PredictorApp.Services;

public class ScoringService() : IScoringService
{
    private List<Driver>? predictedDrivers;
    private List<Driver>? result;
    private List<Driver>? startingGrid;
    private Driver? fastestLap;

    public int GetScore(List<Driver> predictedDrivers, List<Driver> result, List<Driver> startingGrid, Driver? fastestLap)
    {
        this.predictedDrivers = predictedDrivers;
        this.result = result;
        this.startingGrid = startingGrid;
        this.fastestLap = fastestLap;

        var score = this.CorrectDrivers(this.result.Take(3).ToList())
            + this.CorrectDriverPosition(this.result.Take(3).ToList())
            + this.DriversAchievedMoreThanFourPositions()
            + this.DriverDidNotFinish()
            + this.DriverNotClassified()
            + this.ReversePodium();

        if (fastestLap != null)
        {
            score += this.FastestLapInPrediction(this.fastestLap);
        }

        this.predictedDrivers = null;
        this.result = null;
        this.startingGrid = null;
        this.fastestLap = null;
        return score;
    }

    private int CorrectDrivers(List<Driver> podiumDrivers)
    {
        var podiumNames = podiumDrivers.Select(x => x.Name).ToList();
        return this.predictedDrivers!.Where(x => podiumNames.Contains(x.Name)).Count();
    }

    private int CorrectDriverPosition(List<Driver> podiumDrivers)
    {
        var correctCount = 0;
        for (var i = 0; i < this.predictedDrivers!.Count; i++)
        {
            if (this.predictedDrivers[i].Name == podiumDrivers[i].Name) correctCount++;
        }

        return correctCount switch
        {
            1 => 1,
            2 => 3,
            3 => 9,
            _ => 0,
        };
    }

    private int FastestLapInPrediction(Driver? fastestLap)
    {
        return this.predictedDrivers!.Where(x => x.Name == fastestLap!.Name).Count();
    }

    private int DriversAchievedMoreThanFourPositions()
    {
        var score = 0;
        foreach (var driver in this.predictedDrivers!)
        {
            var startingPosition = this.startingGrid!.FindIndex(x => x.Name == driver.Name);
            var actualPosition = this.result!.FindIndex(x => x.Name == driver.Name);
            var diff = actualPosition - startingPosition;
            score += diff >= 5 ? diff - 5 : 0;
        }

        return score;
    }

    private int DriverDidNotFinish()
    {
        return 0;
    }

    private int DriverNotClassified()
    {
        return 0;
    }

    private int ReversePodium()
    {
        var reversePodium = this.result!.Skip(17).Take(3).Reverse().ToList();
        return (this.CorrectDrivers(reversePodium) + this.CorrectDriverPosition(reversePodium)) * -1;
    }
}
