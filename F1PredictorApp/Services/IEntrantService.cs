using F1PredictorApp.Models;

namespace F1PredictorApp.Services
{
    public interface IEntrantService
    {
        List<Entrant> GetEntrants();
        Entrant GetEntrant(string name);
        void SavePrediction(string name, Race race, List<Driver> predictionList);
        void SaveEntrants(List<Entrant> entrants);
        void ScorePredictions(Race race, bool featureRace);
    }
}