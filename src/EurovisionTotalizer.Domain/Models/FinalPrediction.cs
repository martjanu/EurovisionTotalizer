using EurovisionTotalizer.Domain.Enums;

namespace EurovisionTotalizer.Domain.Models;

public class FinalPrediction : IPrediction
{ 
    public PredictionType Type { get; set; } = PredictionType.Last3InFinal;
    public Country? Country { get; set; }
    public int Place { get; set; }
    public bool IsBottom3 { get; set; } = false;
    public int Points { get; set; } = 0;
}
