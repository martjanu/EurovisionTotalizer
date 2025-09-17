using EurovisionTotalizer.Domain.Calculators;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Enums;

namespace EurovisionTotalizer.Domain.Services;

public class ScoreCalculationServices
{
    private readonly IScoreController _scoreController;

    public ScoreCalculationServices(IScoreController scoreController)
    {
        _scoreController = scoreController;
    }

    public int GetFinalPoints(Participant participant, IEnumerable<FinalPrediction> predictions)
    {
        return _scoreController.GetFinalPoints(participant, predictions);
    }

    public int GetSemiFinalPoints(SemiFinal semiFinal, Participant participant, IEnumerable<SemifinalPrediction> predictions)
    {
        return _scoreController.GetSemiFinalPoints(semiFinal, participant, predictions);
    }

    public void ResetAllPoints(IEnumerable<Participant> participants)
    {
        _scoreController.ResetAllPoints(participants);
    }
}
