namespace EurovisionTotalizer.Domain.Rankers;

public class RankingFactory
{
    public IParticipantRanker CreateParticipantRanker(IRankingStrategy strategy) 
        => new ParticipantRanker(strategy);

     public IRankingStrategy CreateRankingStratagy()
        => new RankingStrategy();
}
