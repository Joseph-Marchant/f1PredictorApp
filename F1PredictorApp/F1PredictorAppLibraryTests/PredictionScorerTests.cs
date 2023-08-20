namespace F1PredictorAppLibraryTests;

[TestClass]
public class PredictionScorerTests
{
    [TestMethod]
    public void ScorePredictions_3Pointer_String()
    {
        var subject = this.GetSubject();
        var expected = "Joe was right about VerHamPer.\n";
        var predictions = this.GetPredictionList();
        var raceReport = new List<string> { "Ver", "Ham", "Per" };

        var actual = subject.ScorePredictions(predictions, raceReport);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScorePredictions_3Pointer_Points()
    {
        var subject = this.GetSubject();
        var expected = 1;
        var predictions = this.GetPredictionList();
        var raceReport = new List<string> { "Ver", "Ham", "Per" };

        subject.ScorePredictions(predictions, raceReport);
        var actual = predictions[0].ThreePointers;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScorePredictions_2Pointer_Points()
    {
        var subject = this.GetSubject();
        var expected = 1;
        var predictions = this.GetPredictionList();
        var raceReport = new List<string> { "Ver", "Ham", "Lec" };

        subject.ScorePredictions(predictions, raceReport);
        var actual = predictions[0].TwoPointers;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ScorePredictions_1Pointer_Points()
    {
        var subject = this.GetSubject();
        var expected = 1;
        var predictions = this.GetPredictionList();
        var raceReport = new List<string> { "Ver", "Sai", "Lec" };

        subject.ScorePredictions(predictions, raceReport);
        var actual = predictions[0].OnePointers;

        Assert.AreEqual(expected, actual);
    }

    private IPredictionScorer GetSubject()
    {
        return new PredictionScorer();
    }

    private List<Prediction> GetPredictionList()
    {
        return new List<Prediction> 
        {
            new Prediction("Joe","Ver","Ham","Per", 0, 0, 0, 0, 0)
        };
    }
}
