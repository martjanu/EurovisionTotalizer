namespace EurovisionTotalizer.Domain.Models;

public class Participant
{
    public string Name { get; set; } = "Missing Name";
    public int Points { get; set; } = 0;
    public IEnumerable<Prediction>? Predictions { get; set; }
}
