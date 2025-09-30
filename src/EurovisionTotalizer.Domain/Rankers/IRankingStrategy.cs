using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Rankers
{
    public interface IRankingStrategy
    {
        IEnumerable<Participant> Rank(IEnumerable<Participant> participants);
    }
}