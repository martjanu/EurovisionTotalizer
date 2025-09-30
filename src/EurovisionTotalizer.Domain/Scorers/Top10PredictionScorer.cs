using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Scorers;

public class Top10PredictionScorer : IPredictionScorer<FinalPrediction>
{
    public void ScorePrediction(FinalPrediction prediction, Country country, IEnumerable<Country> allCountries)
    {
        if (country.PlaceInFinal > 10)
        {
            prediction.Points = 0;
            return;
        }

        if (prediction.Place == country.PlaceInFinal)
        {
            prediction.Points = 2;
        }
        else if (Math.Abs(prediction.Place - country.PlaceInFinal) == 1)
        {
            prediction.Points = 1;
        }
        else
        {
            prediction.Points = 0;
        }
    }
}
