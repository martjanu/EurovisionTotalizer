using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Checkers
{
    public interface IPointsController
    {
        int GetFinalPoints(Participant participant, IEnumerable<FinalPrediction> predictions);
        int GetSemiFinalPoints(SemiFinal semiFinal, Participant participant, IEnumerable<SemifinalPrediction> predictions);
        void ResetAllPoints(IEnumerable<Participant> participants);
        void ScoreFinalPredictions(IEnumerable<FinalPrediction> predictions);
        void ScoreSemifinalPredictions(IEnumerable<SemifinalPrediction> predictions, SemiFinal semiFinal);
    }
}