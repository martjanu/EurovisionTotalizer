using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Calculators;

public class ScoreController : IScoreController
{
    public void ScoreSemifinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries)
    {
        var countryNames = new HashSet<string>(countries.Select(c => c.Name));

        foreach (var participant in participants)
        {
            foreach (var prediction in participant.SemifinalPredictions)
            {
                if (prediction.Type == PredictionType.DoesNotReachFinal
                    && !prediction.Country!.IsInFinal
                    && countryNames.Contains(prediction.Country.Name)) // čia patikrinimas
                {
                    prediction.Points = 1;
                }
            }
        }
    }

    public void ScoreFinalPredictions(IEnumerable<Participant> participants, IEnumerable<Country> countries)
    {
        foreach (var participant in participants)
        {
            foreach (var prediction in participant.FinalPredictions)
            {
                var country = countries.Where(c => c.Name == prediction.Country?.Name).FirstOrDefault();
                ScoreBoottum3Predictions(prediction, country);
                ScoreTop10Predictions(prediction, country);
            }
        }
    }

    private void ScoreBoottum3Predictions(FinalPrediction prediction, Country country)
    {
        if (prediction.IsBottom3
            && prediction.PlaceInFinal == country.PlaceInFinal)
        {
            prediction.Points = 1;
        }
    }

    private void ScoreTop10Predictions(FinalPrediction prediction, Country country)
    {
        if (prediction.Country != country) return;

        if (prediction.PlaceInFinal == country.PlaceInFinal)
        {
            prediction.Points = 2;
        }
        else if (prediction.PlaceInFinal + 1 == country.PlaceInFinal
            || prediction.PlaceInFinal - 1 == country.PlaceInFinal)
        {
            prediction.Points = 1;
        }
    }

    public void ResetAllPoints(IEnumerable<Participant> participants)
    {
        foreach (var participant in participants)
        {
            participant.TotalPoints = 0;
        }
    }
}
