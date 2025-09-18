using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Calculators
{
    public interface IScoreController
    {
        void ResetAllPoints(IEnumerable<Participant> participants);
        void ScoreFinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries);
        void ScoreSemifinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries);
    }
}