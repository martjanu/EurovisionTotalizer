namespace EurovisionTotalizer.Application.DTOs;

public class TableDto
{
    public string Title { get; set; } = string.Empty;

    public List<string> ParticipantNames { get; set; } = new();
    public List<TableRowDto> Rows { get; set; } = new();
}
