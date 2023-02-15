namespace F1PredictorAppLibrary;

using F1PredictorAppLibrary.Functions;
using F1PredictorAppLibrary.Interfaces;

public class ServiceContainer : IServiceContainer
{
    public ServiceContainer(
        INewPredictionSaver newPredictionSaver,
        IPredictionLoader predictionLoader,
        IPredictionGetter predictionGetter,
        IPredictionEditor predictionEditor,
        IPredictionGenerator predictionGenerator,
        IPredictionScorer predictionScorer,
        IScoreShower scoreShower,
        IDriverLineUpEditor driverLineUpEditor,
        IPredictionResetter predictionResetter,
        IPredictionSaver predictionSaver,
        INameGetter nameGetter,
        IDriverGetter driverGetter)
    {
        this.newPredictionSaver = newPredictionSaver;
        this.predictionLoader = predictionLoader;
        this.predictionGetter = predictionGetter;
        this.predictionEditor = predictionEditor;
        this.predictionGenerator = predictionGenerator;
        this.predictionScorer = predictionScorer;
        this.scoreShower = scoreShower;
        this.driverLineUpEditor = driverLineUpEditor;
        this.predictionResetter = predictionResetter;
        this.predictionSaver = predictionSaver;
        this.nameGetter = nameGetter;
        this.driverGetter = driverGetter;
    }

    public INewPredictionSaver newPredictionSaver { get; private set; }
    public IPredictionLoader predictionLoader { get; private set; }
    public IPredictionGetter predictionGetter { get; private set; }
    public IPredictionEditor predictionEditor { get; private set; }
    public IPredictionGenerator predictionGenerator { get; private set; }
    public IPredictionScorer predictionScorer { get; private set; }
    public IScoreShower scoreShower { get; private set; }
    public IDriverLineUpEditor driverLineUpEditor { get; private set; }
    public IPredictionResetter predictionResetter { get; private set; }
    public IPredictionSaver predictionSaver { get; private set; }
    public INameGetter nameGetter { get; private set; }
    public IDriverGetter driverGetter { get; private set; }
}
