namespace F1PredictionTracker.Models;

public class PredictionStandings
{
    public PredictionStandings(int round, List<User> users)
    {
        this.Round = round;
        this.Users = users;
    }
    
    public int Round { get; set; }
    
    public List<User> Users { get; }
}
