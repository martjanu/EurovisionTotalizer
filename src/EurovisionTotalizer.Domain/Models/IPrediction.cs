using EurovisionTotalizer.Domain.Enums;

namespace EurovisionTotalizer.Domain.Models;

public interface IPrediction
{
    Country? Country { get; set; }
    int Points { get; set; }
    PredictionType Type { get; set; }
}