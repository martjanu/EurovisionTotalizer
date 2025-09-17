using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Services;

public class RankingServices
{
    private readonly IParticipantRanker _participantRanker;

    public RankingServices(IParticipantRanker participantRanker)
    {
        _participantRanker = participantRanker;
    }

    public IEnumerable<Participant> GetRankedParticipants()
    {
        return _participantRanker.GetRankedParticipants();
    }
}
