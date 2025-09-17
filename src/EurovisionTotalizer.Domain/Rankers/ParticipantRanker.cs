using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Rankers;

public class ParticipantRanker : IParticipantRanker
{
    private IEnumerable<Participant> _participants;

    public ParticipantRanker(IEnumerable<Participant> participants)
    {
        _participants = participants;
    }

    public IEnumerable<Participant> GetRankedParticipants()
    {
        return _participants
            .OrderByDescending(p => p.TotalPoints)
            .ThenByDescending(p => p.FinalPoints)
            .ThenByDescending(p => p.SemiFinal1Points + p.SemiFinal2Points)
            .ThenByDescending(p => p.SemiFinal1Points);
    }
}
