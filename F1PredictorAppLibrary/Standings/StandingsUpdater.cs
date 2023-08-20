using F1PredictorAppLibrary.Interfaces;

namespace F1PredictorAppLibrary.Standings;

public class StandingsUpdater : IStandingsUpdater
{
    public StandingsUpdater()
    {
        this.pointAwards = new Dictionary<int, int>();
    }

    private Dictionary<int, int> pointAwards { get; set; }

    public void UpdateStandings(List<Entrant> entrants, List<string> result, string fastestLap, bool fullRace)
    {
        this.GetFullPoints(fullRace);

        for (int i = 0; i < result.Count; i++)
        {
            var position = i + 1;
            var entrant = entrants.Where(e => e.Driver == result[i]).FirstOrDefault();
            if (entrant is null) throw new ArgumentException($"Could find {result[i]} in the standings");

            this.UpdateDriversStandings(entrant, position, fullRace && result[i] == fastestLap);
        }

        this.OrderStandings(entrants);
        this.UpdatePositions(entrants);
    }

    private void GetFullPoints(bool fullRace)
    {
        if (fullRace)
        {
            this.pointAwards.Add(1, 25);
            this.pointAwards.Add(2, 18);
            this.pointAwards.Add(3, 15);
            this.pointAwards.Add(4, 12);
            this.pointAwards.Add(5, 10);
            this.pointAwards.Add(6, 8);
            this.pointAwards.Add(7, 6);
            this.pointAwards.Add(8, 4);
            this.pointAwards.Add(9, 2);
            this.pointAwards.Add(10, 1);
        }
        else
        {
            this.pointAwards.Add(1, 8);
            this.pointAwards.Add(2, 7);
            this.pointAwards.Add(3, 6);
            this.pointAwards.Add(4, 5);
            this.pointAwards.Add(5, 4);
            this.pointAwards.Add(6, 3);
            this.pointAwards.Add(7, 2);
            this.pointAwards.Add(8, 1);
        }
    }

    private void UpdateDriversStandings(Entrant entrant, int position, bool addFastestLap)
    {
        var points = this.pointAwards.Keys.Contains(position) ? pointAwards[position] : 0;
        entrant.Points += points;
        if (addFastestLap) entrant.Points++;

        var resultHistory = entrant.ResultHistory.Where(r => r.Position == position.ToString()).FirstOrDefault();
        resultHistory.Quantity++;
    }

    private void OrderStandings(List<Entrant> entrants)
    {
        entrants.Sort((x, y) =>
        {
            var xScore = x.Points;
            var yScore = y.Points;
            if (xScore > yScore) return -1;
            else if (xScore < yScore) return 1;
            else
            {
                for (int i = 0; i < x.ResultHistory.Count; i++)
                {
                    var postion = i + 1.ToString();
                    if (x.ResultHistory[i].Quantity > y.ResultHistory[i].Quantity) return 1;
                    else if (x.ResultHistory[i].Quantity < y.ResultHistory[i].Quantity) return -1;
                }

                throw new ArgumentException($"{x.Driver} and {y.Driver} cannot be seperated.");
            }
        });
    }

    private void UpdatePositions(List<Entrant> entrants)
    {
        for (var i = 0; i < entrants.Count; i++)
        {
            var entrant = entrants[i];
            entrant.Position = i + 1;
        }
    }
}
