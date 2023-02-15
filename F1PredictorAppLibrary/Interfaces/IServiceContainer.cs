using F1PredictorAppLibrary.Functions;

namespace F1PredictorAppLibrary.Interfaces
{
    public interface IServiceContainer
    {
        public INewPredictionSaver newPredictionSaver { get; }
        public IPredictionLoader predictionLoader { get; }
        public IPredictionGetter predictionGetter { get; }
        public IPredictionEditor predictionEditor { get; }
        public IPredictionGenerator predictionGenerator { get; }
        public IPredictionScorer predictionScorer { get; }
        public IScoreShower scoreShower { get; }
        public IDriverLineUpEditor driverLineUpEditor { get; }
        public IPredictionResetter predictionResetter { get; }
        public IPredictionSaver predictionSaver { get; }
        public INameGetter nameGetter { get; }
        public IDriverGetter driverGetter { get; }
    }   
}
