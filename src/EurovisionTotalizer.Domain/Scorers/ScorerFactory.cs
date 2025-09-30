using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Scorers;

public class ScorerFactory : IScorerFactory
{
    public IPredictionScorer<FinalPrediction> CreateBottom3PredictionScorer()
        => new Bottom3PredictionScorer();

    public IPredictionScorer<FinalPrediction> CreateTop10PredictionScorer()
        => new Top10PredictionScorer();

    public IPredictionScorer<FinalPrediction> CreateFinalPredictionScorer()
        => new FinalPredictionScorer(CreateBottom3PredictionScorer(), CreateTop10PredictionScorer());

    public IPredictionScorer<SemiFinalPrediction> CreateSemiFinalPredictionScorer()
        => new SemiFinalPredictionScorer();
}
