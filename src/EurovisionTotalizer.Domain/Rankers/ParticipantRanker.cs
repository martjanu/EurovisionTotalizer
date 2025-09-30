using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Rankers;

public class ParticipantRanker : IParticipantRanker
{
    private readonly IRankingStrategy _rankingStrategy;

    public ParticipantRanker(IRankingStrategy rankingStrategy)
    {
        _rankingStrategy = rankingStrategy ?? throw new ArgumentNullException(nameof(rankingStrategy));
    }

    public IEnumerable<Participant> GetRankedParticipants(IEnumerable<Participant> participants)
    {
        if (participants == null) throw new ArgumentNullException(nameof(participants));

        return _rankingStrategy.Rank(participants);
    }
}
