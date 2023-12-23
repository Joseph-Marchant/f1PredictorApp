namespace F1PredictorApp.Services;

public class ScoringService(List<string> predictedDrivers, List<string> podiumDrivers)
{
    // 1 point per correctly guessed driver
    // n point(s) for n dirvers in correct position (cumulative)
    // 1 point for predicted driver getting fastest lap
    // n point(s) for predicted driver finishing 4+n positions ahead of starting
    // -1 point if predicted driver does not see the chequred flag
    // -1 point if predicted driver is not a classified finisher
    // -1 point if driver is in bottom 3
    // -n point(s) for n drivers in bottom three reverse position (cumulative)

    private List<string> predictedDrivers { get; set; }
    private List<string> podiumDrivers { get; set; }
    private int CorrectDrivers()
    {
        return this.predictedDrivers.Where(d => this.podiumDrivers.Contains(d)).Count();
    }

    private int CorrectDriverPosition()
    {
        var correctCount = 0;
        for (var i = 0; i < this.predictedDrivers.Count; i++)
        {
            if (this.predictedDrivers[i] == this.podiumDrivers[i]) correctCount++;
        }

        return correctCount switch
        {
            1 => 1,
            2 => 3,
            3 => 9,
            _ => 0,
        };
    }

    private int FastestLapInPrediction(string fastestLap)
    {
        return this.predictedDrivers.Contains(fastestLap) ? 1 : 0;
    }

    private int DriversAchievedMoreThanFourPositions()
    {
        return 0;
    }
}
