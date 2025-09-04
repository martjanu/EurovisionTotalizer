using EurovisionTotalizer.Domain.Models.Enums;

namespace EurovisionTotalizer.Domain.Models;

public class Prediction
{
    public int Id { get; set; }
    public Participant? Participant { get; set; }
    public PredictionType Type { get; set; } = PredictionType.DoesNotReachFinal;
    public Country? Country { get; set; }
    public int Points { get; set; } = 0;
}
