using EurovisionTotalizer.Domain.Enums;

namespace EurovisionTotalizer.Domain.Models;

public class SemifinalPrediction
{
    public Participant? Participant { get; set; }
    public PredictionType Type { get; set; } = PredictionType.DoesNotReachFinal;
    public Country? Country { get; set; }
    public int Points { get; set; } = 0;
}
