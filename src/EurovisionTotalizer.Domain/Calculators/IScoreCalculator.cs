using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Calculators;

public interface IScoreCalculator
{
    IEnumerable<Participant> ScoreSemifinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries);
    IEnumerable<Participant> ScoreFinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries);
    IEnumerable<Participant> ResetAllPoints(IEnumerable<Participant> participants);
    IEnumerable<Participant> CalculateTotalPoints(IEnumerable<Participant> participants);
}
