using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.API.ViewModels;

public class LeaderboardViewModel
{
    public IEnumerable<Participant> Participants { get; set; } = Enumerable.Empty<Participant>();
}
