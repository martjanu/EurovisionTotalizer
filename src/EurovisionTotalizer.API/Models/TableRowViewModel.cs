namespace EurovisionTotalizer.API.ViewModels;

public class TableRowViewModel
{
    public string CountryName { get; set; } = string.Empty;
    public Dictionary<string, string> Predictions { get; set; } = new();
}