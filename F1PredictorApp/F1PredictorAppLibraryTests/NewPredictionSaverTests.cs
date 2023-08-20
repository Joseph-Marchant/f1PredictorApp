namespace F1PredictorAppLibraryTests;

[TestClass]
public class NewPredictionSaverTests
{
    [TestMethod]
    public void SavePrediction_ValidInput_WithNameExisting()
    {
        var subject = this.GetSubject();
        var expected = "Joe's prediction of VerHamPer has been saved";
        var predictions = this.GetPredictionList();
        var newPrediction = this.GetPrediction();

        var actual = subject.SavePrediction(newPrediction, predictions);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SavePrediction_ValidInput_WithNameNotExisting()
    {
        var subject = this.GetSubject();
        var expected = "Joe's prediction of VerHamPer has been saved";
        var predictions = new List<Prediction>();
        var newPrediction = this.GetPrediction();

        var actual = subject.SavePrediction(newPrediction, predictions);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void SavePrediction_AlreadyHasAPrediction(int driverToAdd)
    {
        var subject = this.GetSubject();
        var newPrediction = this.GetPrediction();
        var predictions = new List<Prediction> { newPrediction };

        if (driverToAdd == 1)
        {
            newPrediction.First = "Ver";
        }
        else if (driverToAdd == 2)
        {
            newPrediction.Second = "Ham";
        }
        else
        {
            newPrediction.Third = "Per";
        }

        Assert.ThrowsException<ArgumentException>(() => subject.SavePrediction(newPrediction, predictions));
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    public void SavePrediction_MissingDirver(int driverToRemove)
    {
        var subject = this.GetSubject();
        var predictions = this.GetPredictionList();
        var newPrediction = this.GetPrediction();
        
        if (driverToRemove == 1)
        {
            newPrediction.First = null;
        }
        else if (driverToRemove == 2)
        {
            newPrediction.Second = null;
        }
        else
        {
            newPrediction.Third = null;
        }

        Assert.ThrowsException<ArgumentNullException>(() => subject.SavePrediction(newPrediction, predictions));
    }

    private List<Prediction> GetPredictionList()
    {
        return new List<Prediction> {
            new Prediction(
                "Joe",
                null,
                null,
                null,
                0,
                0,
                0,
                0,
                0
            )};
    }

    private Prediction GetPrediction()
    {
        return new Prediction("Joe", "Ver", "Ham", "Per", 0, 0, 0, 0, 0);
    }

    private INewPredictionSaver GetSubject()
    {
        return new NewPredictionSaver();
    }
}