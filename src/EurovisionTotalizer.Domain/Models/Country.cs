using EurovisionTotalizer.Domain.Models.Enums;

namespace EurovisionTotalizer.Domain.Models;

public class Country
{
    public string Name { get; set; } = "Missing Name";
    public bool IsInFinal { get; set; } = false;
    public SemiFinal SemiFinal { get; set; }
}
