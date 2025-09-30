using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Models;
using System.Linq;

namespace EurovisionTotalizer.Domain.Scorers;

public class FinalPredictionScorer : IPredictionScorer<FinalPrediction>
{
    private readonly IPredictionScorer<FinalPrediction> _bottom3Scorer;
    private readonly IPredictionScorer<FinalPrediction> _top10Scorer;

    public FinalPredictionScorer(IPredictionScorer<FinalPrediction> bottom3Scorer, IPredictionScorer<FinalPrediction> top10Scorer)
    {
        _bottom3Scorer = bottom3Scorer;
        _top10Scorer = top10Scorer;
    }

    public void ScorePrediction(FinalPrediction prediction, Country country, IEnumerable<Country> allCountries)
    {
        prediction.Points = 0;

        if (prediction.Country == null || country == null) return;

        _bottom3Scorer.ScorePrediction(prediction, country, allCountries);
        _top10Scorer.ScorePrediction(prediction, country, allCountries);
    }
}
