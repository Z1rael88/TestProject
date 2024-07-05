using WebApplication1.Enums;

namespace WebApplication1.Dtos.SearchParams;

public record TaskSearchParams
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; }
    public Guid ProjectId { get; set; }
}