using  EurovisionTotalizer.Domain.Rankers;

namespace EurovisionTotalizer.Domain.Factories;

public class RankingStrategyFactory
{
    public IRankingStrategy Create()
        => new RankingStrategy();
}
