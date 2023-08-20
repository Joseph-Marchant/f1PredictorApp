namespace F1PredictorAppLibraryTests;

[TestClass]
public class PredictionEditorTests
{
    [TestMethod]
    public void EditPrediction_ValidInput()
    {
        var subject = this.GetSubject();
        var expected = "Joe's prediction of VerHamPer has been changed to PerHamVer";
        var predictions = this.GetPredictionList();
        var name = "Joe";
        var newPredictionDrivers = new List<string> { "Per", "Ham", "Ver" };

        var actual = subject.EditPrediction(predictions, name, newPredictionDrivers);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EditPrediction_MissingPredictions()
    {
        var subject = this.GetSubject();
        var name = "Joe";
        var newPredictionDrivers = new List<string> { "Per", "Ham", "Ver" };

        Assert.ThrowsException<ArgumentNullException>(() => subject.EditPrediction(null, name, newPredictionDrivers));
    }

    [TestMethod]
    public void EditPrediction_MissingName()
    {
        var subject = this.GetSubject();
        var predictions = this.GetPredictionList();
        var newPredictionDrivers = new List<string> { "Per", "Ham", "Ver" };

        Assert.ThrowsException<ArgumentNullException>(() => subject.EditPrediction(predictions, null, newPredictionDrivers));
    }

    [TestMethod]
    public void EditPrediction_MissingDrivers()
    {
        var subject = this.GetSubject();
        var predictions = this.GetPredictionList();
        var name = "Joe";

        Assert.ThrowsException<ArgumentNullException>(() => subject.EditPrediction(predictions, name, null));
    }

    [TestMethod]
    public void EditPrediction_NameNotInPredictions()
    {
        var subject = this.GetSubject();
        var predictions = new List<Prediction>();
        var name = "Joe";
        var newPredictionDrivers = new List<string> { "Per", "Ham", "Ver" };

        Assert.ThrowsException<ArgumentException>(() => subject.EditPrediction(predictions, name, newPredictionDrivers));
    }

    [TestMethod]
    public void EditPrediction_NameHasNotMadeAPrediction()
    {
        var subject = this.GetSubject();
        var predictions = this.GetPredictionListWithNullPredictions();
        var name = "Joe";
        var newPredictionDrivers = new List<string> { "Per", "Ham", "Ver" };

        Assert.ThrowsException<ArgumentException>(() => subject.EditPrediction(predictions, name, newPredictionDrivers));
    }

    [TestMethod]
    public void EditPrediction_NotEnoughDrivers()
    {
        var subject = this.GetSubject();
        var predictions = this.GetPredictionList();
        var name = "Joe";
        var newPredictionDrivers = new List<string> { "Per", "Ham" };

        Assert.ThrowsException<ArgumentException>(() => subject.EditPrediction(predictions, name, newPredictionDrivers));
    }


    private IPredictionEditor GetSubject()
    {
        return new PredictionEditor();
    }

    private List<Prediction> GetPredictionList()
    {
        return new List<Prediction> {
            new Prediction("Joe", "Ver", "Ham", "Per", 0, 0, 0, 0, 0)
        };
    }

    private List<Prediction> GetPredictionListWithNullPredictions()
    {
        return new List<Prediction> {
            new Prediction("Joe", null, null, null, 0, 0, 0, 0, 0)
        };
    }
}
