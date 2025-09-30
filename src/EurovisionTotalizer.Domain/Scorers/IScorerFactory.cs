using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Scorers
{
    public interface IScorerFactory
    {
        IPredictionScorer<FinalPrediction> CreateBottom3PredictionScorer();
        IPredictionScorer<FinalPrediction> CreateFinalPredictionScorer();
        IPredictionScorer<SemiFinalPrediction> CreateSemiFinalPredictionScorer();
        IPredictionScorer<FinalPrediction> CreateTop10PredictionScorer();
    }
}