using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Domain.Rankers
{
    public interface IParticipantRanker
    {
        IEnumerable<Participant> GetRankedParticipants();
    }
}