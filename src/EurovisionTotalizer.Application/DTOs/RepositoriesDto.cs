using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Application.DTOs;

public class RepositoriesDto
{
    public IEnumerable<Participant> Participants { get; set; }
    public IEnumerable<Country> Countries { get; set; }
}
