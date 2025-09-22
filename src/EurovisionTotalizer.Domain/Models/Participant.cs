namespace EurovisionTotalizer.Domain.Models;

public class Participant : IHasName
{
    public string Name { get; set; } = "Missing Name";
    public int TotalPoints { get; set; } = 0;
    public int FinalPoints { get; set; } = 0;
    public int SemiFinal1Points { get; set; } = 0;
    public int SemiFinal2Points { get; set; } = 0;
    public IEnumerable<SemifinalPrediction> SemifinalPredictions { get; set; } = Enumerable.Empty<SemifinalPrediction>();
    public IEnumerable<FinalPrediction> FinalPredictions { get; set; } = Enumerable.Empty<FinalPrediction>();
}
