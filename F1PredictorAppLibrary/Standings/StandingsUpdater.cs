namespace F1PredictorAppLibrary.Standings;

public class StandingsUpdater
{
    public string UpdateStandings(List<Entrant> entrants, List<String> topTen, string fastestLap, bool fullRace)
    {
        var pointAwards = new Dictionary<int, int>();
        if (fullRace) this.GetFullPoints(pointAwards);
        else this.GetSprintRacePoints(pointAwards);

        for(int i = 0; i < pointAwards.Count; i++)
        {
            var entrant = entrants.Where(e => e.Driver == topTen[i]).FirstOrDefault();
            if (entrant is null) throw new ArgumentException($"Could find {topTen[i]} in the standings");

            entrant.Points += pointAwards[i + 1];
            if (fullRace && topTen[i] == fastestLap) entrant.Points++;
        }

        this.OrderStandings(entrants);
        
        return "Standings Updated";
    }

    private void GetFullPoints(Dictionary<int, int> pointAwards)
    {
        pointAwards.Add(1, 25);
        pointAwards.Add(2, 18);
        pointAwards.Add(3, 15);
        pointAwards.Add(4, 12);
        pointAwards.Add(5, 10);
        pointAwards.Add(6, 8);
        pointAwards.Add(7, 6);
        pointAwards.Add(8, 4);
        pointAwards.Add(9, 2);
        pointAwards.Add(10, 1);
    }

    private void GetSprintRacePoints(Dictionary<int, int> pointAwards)
    {
        pointAwards.Add(1, 8);
        pointAwards.Add(2, 7);
        pointAwards.Add(3, 6);
        pointAwards.Add(4, 5);
        pointAwards.Add(5, 4);
        pointAwards.Add(6, 3);
        pointAwards.Add(7, 2);
        pointAwards.Add(8, 1);
    }

    private void OrderStandings(List<Entrant> entrants)
    {
        entrants.Sort((x, y) =>
        {
            var xScore = x.Points;
            var yScore = y.Points;
            if (xScore > yScore) return 1;
            else if (xScore < yScore) return -1;
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
}
