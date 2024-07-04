namespace WebApplication1.Dtos.SearchParams;

public record ProjectSearchParams
{
    public string NameTerm { get; set; } = string.Empty;
    public string DescriptionTerm { get; set; } = string.Empty;
}