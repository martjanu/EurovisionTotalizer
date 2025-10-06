using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Scorers;

public interface IPredictionScorer<TPrediction>
{
    void ScorePrediction(TPrediction prediction, Country country, IEnumerable<Country> allCountries);
}
