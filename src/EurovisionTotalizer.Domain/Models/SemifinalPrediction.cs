using EurovisionTotalizer.Domain.Enums;

namespace EurovisionTotalizer.Domain.Models;

public class SemiFinalPrediction : IPrediction
{
    public PredictionType Type { get; set; } = PredictionType.DoesNotReachFinal;
    public Country? Country { get; set; }
    public int Points { get; set; } = 0;
}
