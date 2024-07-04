namespace WebApplication1.Dtos.SearchParams;

public record TaskSearchParams
{
    public string NameTerm { get; set; } = string.Empty;
    public string DescriptionTerm { get; set; }= string.Empty;
    public string Status { get; set; }= string.Empty;
}