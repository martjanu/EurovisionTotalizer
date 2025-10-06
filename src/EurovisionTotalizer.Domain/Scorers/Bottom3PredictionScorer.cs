using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Scorers;

public class Bottom3PredictionScorer : IPredictionScorer<FinalPrediction>
{
    public void ScorePrediction(FinalPrediction prediction, Country country, IEnumerable<Country> allCountries)
    {
        int totalCountries = allCountries.Count(c => c.IsInFinal);

        if (prediction.IsBottom3 && country.PlaceInFinal >= totalCountries - 2)
        {
            prediction.Points = 1;
        }
    }
}
