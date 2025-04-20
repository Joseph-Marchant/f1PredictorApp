namespace F1PredictionTracker.Services;

public class RaceResultEventService(PredictionScoringService predictionScoringService)
{
    public async Task<string> PollRaceResultAsync()
    {
        // TODO: Add validation check to ensure round has not already been scored.
        var response = string.Empty;
        while (response == string.Empty)
        {
            try
            {
                Console.WriteLine($"Attempting to pull race result at {DateTime.Now.TimeOfDay.ToString()}");
                response = await predictionScoringService.ScorePredictions();
            }
            catch (NullReferenceException nrex)
            {
                if (nrex.Message != "Race results could not be found.")
                {
                    throw;
                }

                const int pollingInterval = 15;
                Console.WriteLine($"Result data not yet available, retrying in {pollingInterval} minutes.");
                Thread.Sleep(TimeSpan.FromMinutes(pollingInterval));
            }
        }

        return response;
    }
}
