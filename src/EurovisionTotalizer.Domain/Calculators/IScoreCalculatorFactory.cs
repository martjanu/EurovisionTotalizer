using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Scorers;

namespace EurovisionTotalizer.Domain.Calculators
{
    public interface IScoreCalculatorFactory
    {
        IScoreCalculator CreateScoreController(IPredictionScorer<SemiFinalPrediction> semiFinalPredictions, IPredictionScorer<FinalPrediction> finalPredictions);
    }
}