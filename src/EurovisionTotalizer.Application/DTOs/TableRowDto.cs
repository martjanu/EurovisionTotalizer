namespace EurovisionTotalizer.Application.DTOs;

public class TableRowDto
{
    public string CountryName { get; set; } = string.Empty;
    public Dictionary<string, string> Predictions { get; set; } = new();
}
