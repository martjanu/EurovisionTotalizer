using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;

namespace EurovisionTotalizer.Domain.Rankers;

public class ParticipantRanker : IParticipantRanker
{
    private readonly IJsonStorageRepository<Participant> _participantRepository;

    public ParticipantRanker(IJsonStorageRepository<Participant> participantRepository)
    {
        _participantRepository = participantRepository ?? throw new ArgumentNullException(nameof(participantRepository));
    }

    public IEnumerable<Participant> GetRankedParticipants(IEnumerable<Participant> participants)
    {
        return participants
            .OrderByDescending(p => p.TotalPoints)
            .ThenByDescending(p => p.FinalPoints)
            .ThenByDescending(p => p.SemiFinal1Points + p.SemiFinal2Points)
            .ThenByDescending(p => p.SemiFinal1Points);
    }
}
