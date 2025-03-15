namespace F1PredictionTracker.Models;

public class User
{
    public User(string name)
    {
        this.Name = name;
        this.Score = 0;
    } 
    
    public string Name { get; }
    
    public int Score { get; set; }
}
