using EurovisionTotalizer.Domain.Rankers;
using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Factories;

public class ParticipantRankerFactory
{
    public IParticipantRanker Create(IRankingStrategy strategy) 
        => new ParticipantRanker(strategy);
}
