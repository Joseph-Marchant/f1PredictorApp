using F1PredictorAppLibrary.FileManager;

namespace F1PredictorAppLibraryTests;

[TestClass]
public class PredictionGeneratorTests
{
    [TestMethod]
    public void GeneratePrediction_ReturnsValidPrediction()
    {
        var subject = this.GetSubject();
        var predictions = new List<Prediction>
        {
            new Prediction("AI", null, null, null, 0, 0, 0, 0, 0)
        };

        var actual = subject.GeneratePrediction(predictions);

        Assert.IsNotNull(actual);
    }

    [TestMethod]
    public void GeneratePrediction_ThrowsExceptionWithNoAI()
    {
        var subject = this.GetSubject();
        var predictions = new List<Prediction>();

        Assert.ThrowsException<ArgumentException>(() => subject.GeneratePrediction(predictions));
    }

    [TestMethod]
    public void GeneratePrediction_ThrowsExceptionWhenAIHasPrediction()
    {
        var subject = this.GetSubject();
        var predictions = new List<Prediction>
        {
            new Prediction("AI", "Ver", "Ham", "Per", 0, 0, 0, 0, 0)
        };

        Assert.ThrowsException<ArgumentException>(() => subject.GeneratePrediction(predictions));
    }

    private IPredictionGenerator GetSubject()
    {
        return new PredictionGenerator(new DriverLoader());
    }
}
