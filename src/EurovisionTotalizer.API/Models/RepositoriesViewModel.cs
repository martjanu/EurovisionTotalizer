using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.API.ViewModels;

public class RepositoriesViewModel
{
    public IEnumerable<Participant> Participants { get; set; } = Enumerable.Empty<Participant>();
    public IEnumerable<Country> Countries { get; set; } = Enumerable.Empty<Country>();
}
