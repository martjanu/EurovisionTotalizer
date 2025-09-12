using EurovisionTotalizer.Domain.Enums;

namespace EurovisionTotalizer.Domain.Models;

public class Country : IHasName
{
    public string Name { get; set; } = "Missing Name";
    public bool IsInFinal { get; set; } = false;
    public SemiFinal SemiFinal { get; set; }
    public int PlaceInFinal { get; set; } = 0;
}
