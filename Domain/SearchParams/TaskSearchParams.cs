using Domain.Enums;

namespace Domain.SearchParams;

public record TaskSearchParams
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Status? Status { get; set; }
}