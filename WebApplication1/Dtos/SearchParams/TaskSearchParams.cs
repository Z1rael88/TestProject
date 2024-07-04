using WebApplication1.Enums;

namespace WebApplication1.Dtos.SearchParams;

public record TaskSearchParams
{
    public string NameTerm { get; set; } = string.Empty;
    public string DescriptionTerm { get; set; }= string.Empty;
    public Status Status { get; set; }
    public Guid ProjectId { get; set; }
}