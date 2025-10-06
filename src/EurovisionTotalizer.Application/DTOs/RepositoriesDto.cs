using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.Application.DTOs;

public class RepositoriesDto
{
    public IEnumerable<Participant> Participants { get; set; } = new List<Participant>();
    public IEnumerable<Country> Countries { get; set; } = new List<Country>();
    public List<TableDto> Tables { get; set; } = new();
}
