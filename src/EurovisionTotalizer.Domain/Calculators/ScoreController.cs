using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Calculators;

public class ScoreController : IScoreController
{
    public IEnumerable<Participant> ScoreSemifinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries)
    {
        if (participants == null) throw new ArgumentNullException(nameof(participants));
        if (countries == null) throw new ArgumentNullException(nameof(countries));
        if (!participants.Any() || !countries.Any()) return participants;

        foreach (var participant in participants)
        {
            foreach (var prediction in participant.SemifinalPredictions)
            {
                prediction.Points = 0;

                if (prediction.Country == null) continue;

                var country = countries.FirstOrDefault(c => c.Name == prediction.Country.Name);
                if (prediction.Type == PredictionType.DoesNotReachFinal &&
                    country is { IsInFinal: false })
                {
                    prediction.Points = 1;
                }
            }
        }

        return participants;
    }

    public IEnumerable<Participant> ScoreFinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries)
    {
        if (participants == null) throw new ArgumentNullException(nameof(participants));
        if (countries == null) throw new ArgumentNullException(nameof(countries));
        if (!participants.Any() || !countries.Any(c => c.IsInFinal)) return participants;

        var totalCountries = countries.Count(c => c.IsInFinal);

        foreach (var participant in participants)
        {
            foreach (var prediction in participant.FinalPredictions)
            {
                prediction.Points = 0;

                if (prediction.Country == null) continue;

                var country = countries.FirstOrDefault(c => c.Name == prediction.Country.Name);
                if (country == null) continue;

                ScoreBottom3Predictions(prediction, country, totalCountries);
                ScoreTop10Predictions(prediction, country);
            }
        }

        return participants;
    }

    private void ScoreBottom3Predictions(FinalPrediction prediction, Country country, int totalCountries)
    {
        if (prediction.IsBottom3 && country.PlaceInFinal >= totalCountries - 2)
        {
            prediction.Points = 1;
        }
    }

    private void ScoreTop10Predictions(FinalPrediction prediction, Country country)
    {
        if (country.PlaceInFinal > 10) return;

        if (prediction.Place == country.PlaceInFinal)
        {
            prediction.Points = 2;
        }
        else if (Math.Abs(prediction.Place - country.PlaceInFinal) == 1)
        {
            prediction.Points = 1;
        }
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
            var semifinalPoints = participant.SemifinalPredictions.Sum(p => p.Points);

            var finalPoints = participant.FinalPredictions.Sum(p => p.Points);

            participant.TotalPoints = semifinalPoints + finalPoints;

            participant.SemiFinal1Points = participant.SemifinalPredictions
                .Where(p => p.Country?.SemiFinal == SemiFinal.First)
                .Sum(p => p.Points);

            participant.SemiFinal2Points = participant.SemifinalPredictions
                .Where(p => p.Country?.SemiFinal == SemiFinal.Second)
                .Sum(p => p.Points);

            participant.FinalPoints = finalPoints;
        }

        return participants;
    }

}
