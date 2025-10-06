using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.API.ViewModels;

public class LeaderboardViewModel
{
    public List<Participant> Participants { get; set; } = new List<Participant>();
}
