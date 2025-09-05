using System.Collections.Generic;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Models.Enums;

namespace EurovisionTotalizer.Domain.Core.Checkers;

public class PointsController
{
    public void ScoreSemifinalPredictions(IEnumerable<SemifinalPrediction> predictions, SemiFinal semiFinal)
    {
        var semiFinalCountries = predictions.Where(p => p.Country!.SemiFinal == semiFinal);

        foreach (var prediction in semiFinalCountries)
        {
           if (prediction.Type == PredictionType.DoesNotReachFinal
                && !prediction.Country!.IsInFinal)
            {
                prediction.Points = 1;
            }
        }
    }

    public void ScoreFinalPredictions(IEnumerable<FinalPrediction> predictions)
    {
        foreach (var prediction in predictions)
        {
            
        }
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
