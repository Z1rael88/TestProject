
namespace WebApplication1.Dtos.SearchParams;

public record ProjectSearchParams
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly? StartDate { get; set; } 
    public DateOnly? StartDateFrom { get; set; }
    public DateOnly? StartDateTo { get; set; }
}