using EurovisionTotalizer.Domain.Enums;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Checkers;

public class PointsController
{
    private readonly Dictionary<string, Country> _countries;

    public PointsController(IEnumerable<Country> countries)
    {
        _countries = countries.ToDictionary(c => c.Name);
    }

    public void ScoreSemifinalPredictions(IEnumerable<SemifinalPrediction> predictions, SemiFinal semiFinal)
    {
        foreach (var prediction in predictions)
        {
           if (prediction.Type == PredictionType.DoesNotReachFinal
                && !prediction.Country!.IsInFinal
                && _countries.ContainsKey(prediction.Country.Name))
            {
                prediction.Points = 1;
            }
        }
    }

    public void ScoreFinalPredictions(IEnumerable<FinalPrediction> predictions)
    {
        foreach (var prediction in predictions)
        {
            prediction = ScoreBoottum3Predictions(prediction);
            prediction = ScoreTop10Predictions(prediction);
        }
    }

    private Prediction ScoreBoottum3Predictions(FinalPrediction prediction)
    {
        return prediction;
    }

    private Prediction ScoreTop10Predictions(FinalPrediction prediction)
    {
        return prediction;
    }

    public int GetSemiFinalPoints(SemiFinal semiFinal, Participant participant, IEnumerable<SemifinalPrediction> predictions)
    {
        return predictions
            .Where(p => p.Participant == participant
                        && p.Country!.SemiFinal == semiFinal)
            .Sum(p => p.Points);  
    }

    public int GetFinalPoints(Participant participant, IEnumerable<FinalPrediction> predictions)
    {
        return predictions
            .Where(p => p.Participant == participant)
            .Sum(p => p.Points);
    }

    public void ResetAllPoints(IEnumerable<Participant> participants)
    {
        foreach (var participant in participants)
        {
            participant.TotalPoints = 0;
        }
    }
}
