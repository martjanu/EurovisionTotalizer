using EurovisionTotalizer.Domain.Scorers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Calculators;

public class ScoreCalculator : IScoreCalculator
{
    private readonly IPredictionScorer<SemiFinalPrediction> _semifinalScorer;
    private readonly IPredictionScorer<FinalPrediction> _finalScorer;

    public ScoreCalculator(
        IPredictionScorer<SemiFinalPrediction> semifinalScorer,
        IPredictionScorer<FinalPrediction> finalScorer)
    {
        _semifinalScorer = semifinalScorer;
        _finalScorer = finalScorer;
    }

    public IEnumerable<Participant> ScoreSemifinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries)
    {
        if (participants == null) throw new ArgumentNullException(nameof(participants));
        if (countries == null) throw new ArgumentNullException(nameof(countries));
        if (!participants.Any() || !countries.Any()) return participants;

        foreach (var participant in participants)
        {
            foreach (var prediction in participant.SemifinalPredictions)
            {
                var country = countries.FirstOrDefault(c => c.Name == prediction.Country?.Name);
                _semifinalScorer.ScorePrediction(prediction, country, countries);
            }
        }

        return participants;
    }

    public IEnumerable<Participant> ScoreFinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries)
    {
        if (participants == null) throw new ArgumentNullException(nameof(participants));
        if (countries == null) throw new ArgumentNullException(nameof(countries));
        if (!participants.Any() || !countries.Any(c => c.IsInFinal)) return participants;

        foreach (var participant in participants)
        {
            foreach (var prediction in participant.FinalPredictions)
            {
                var country = countries.FirstOrDefault(c => c.Name == prediction.Country?.Name);
                _finalScorer.ScorePrediction(prediction, country, countries);
            }
        }

        return participants;
    }

    public IEnumerable<Participant> ResetAllPoints(IEnumerable<Participant> participants)
    {
        if (participants == null) throw new ArgumentNullException(nameof(participants));
        if (!participants.Any()) return participants;

        foreach (var participant in participants)
        {
            participant.TotalPoints = 0;
            participant.SemiFinal1Points = 0;
            participant.SemiFinal2Points = 0;
            participant.FinalPoints = 0;

            foreach (var sp in participant.SemifinalPredictions)
                sp.Points = 0;

            foreach (var fp in participant.FinalPredictions)
                fp.Points = 0;
        }

        return participants;
    }

    public IEnumerable<Participant> CalculateTotalPoints(IEnumerable<Participant> participants)
    {
        if (participants == null) throw new ArgumentNullException(nameof(participants));
        if (!participants.Any()) return participants;

        foreach (var participant in participants)
        {
            participant.TotalPoints = participant.SemifinalPredictions.Sum(p => p.Points) +
                                      participant.FinalPredictions.Sum(p => p.Points);

            participant.SemiFinal1Points = participant.SemifinalPredictions
                .Where(p => p.Country?.SemiFinal == Enums.SemiFinal.First)
                .Sum(p => p.Points);

            participant.SemiFinal2Points = participant.SemifinalPredictions
                .Where(p => p.Country?.SemiFinal == Enums.SemiFinal.Second)
                .Sum(p => p.Points);

            participant.FinalPoints = participant.FinalPredictions.Sum(p => p.Points);
        }

        return participants;
    }
}
