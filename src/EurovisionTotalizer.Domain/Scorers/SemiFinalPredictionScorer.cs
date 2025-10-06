using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Scorers;

public class SemiFinalPredictionScorer : IPredictionScorer<SemiFinalPrediction>
{
    public void ScorePrediction(SemiFinalPrediction prediction, Country country, IEnumerable<Country> allCountries)
    {
        prediction.Points = 0;

        if (prediction.Country == null || country == null) return;

        if (prediction.Type == PredictionType.DoesNotReachFinal && !country.IsInFinal)
        {
            prediction.Points = 1;
        }
    }
}
