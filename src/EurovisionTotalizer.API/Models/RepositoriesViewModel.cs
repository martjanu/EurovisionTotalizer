using EurovisionTotalizer.Domain.Models;

namespace EurovisionTotalizer.API.ViewModels;

public class RepositoriesViewModel
{
    public IEnumerable<Participant> Participants { get; set; } = new List<Participant>();
    public IEnumerable<Country> Countries { get; set; } = new List<Country>();

    public IEnumerable<Country> SemiFinal1Countries { get; set; } = new List<Country>();
    public IEnumerable<Country> SemiFinal2Countries { get; set; } = new List<Country>();
    public IEnumerable<Country> FinalCountries { get; set; } = new List<Country>();
    public List<TableViewModel> Tables { get; set; } = new();
}
