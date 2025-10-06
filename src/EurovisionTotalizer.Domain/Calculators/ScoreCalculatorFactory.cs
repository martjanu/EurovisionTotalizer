using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Calculators;

public class ScoreCalculatorFactory : IScoreCalculatorFactory
{
    public IScoreCalculator CreateScoreController(
        IPredictionScorer<SemiFinalPrediction> semiFinalPredictions,
        IPredictionScorer<FinalPrediction> finalPredictions)
        => new ScoreCalculator(semiFinalPredictions, finalPredictions);
}
