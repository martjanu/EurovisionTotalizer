namespace EurovisionTotalizer.Domain.Models;

public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; } = "Missing Name";
    public int TotalPoints { get; set; } = 0;
    public IEnumerable<Prediction>? Predictions { get; set; }
}
