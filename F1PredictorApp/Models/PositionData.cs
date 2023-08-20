namespace F1PredictorApp.Models;

public class PositionData
{
    public PositionData(string position, int quantity) 
    { 
        this.Position = position;
        this.Quantity = quantity;
    }
    public string Position { get; set; }
    public int Quantity { get; set; }
}
