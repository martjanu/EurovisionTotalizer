using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;
using EurovisionTotalizer.Domain.Persistence.Repositories;


namespace EurovisionTotalizer.Domain.Factories;

public class ParticipantRankerFactory
{
    public IParticipantRanker Create(IJsonStorageRepository<Participant> participantStorage) 
        => new ParticipantRanker(participantStorage);
}
