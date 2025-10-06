namespace EurovisionTotalizer.API.ViewModels;

public class TableViewModel
{
    public string Title { get; set; } = string.Empty;
    public List<TableRowViewModel> Rows { get; set; } = new();
    public List<string> ParticipantNames { get; set; } = new();
}