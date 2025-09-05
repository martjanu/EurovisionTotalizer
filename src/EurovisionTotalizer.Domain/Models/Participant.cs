using EurovisionTotalizer.Domain.Interfaces;

namespace EurovisionTotalizer.Domain.Models;

public class Participant : IHasName
{
    public string Name { get; set; } = "Missing Name";
    public int TotalPoints { get; set; } = 0;
    public IEnumerable<SemifinalPrediction>? Predictions { get; set; }
}
