using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Rankers;
public class RankingStrategy : IRankingStrategy
{
    public IEnumerable<Participant> Rank(IEnumerable<Participant> participants)
    {
        if (participants == null) throw new ArgumentNullException(nameof(participants));

        return participants
            .OrderByDescending(p => p.TotalPoints)
            .ThenByDescending(p => p.FinalPoints)
            .ThenByDescending(p => p.SemiFinal1Points + p.SemiFinal2Points)
            .ThenByDescending(p => p.SemiFinal1Points);
    }
}