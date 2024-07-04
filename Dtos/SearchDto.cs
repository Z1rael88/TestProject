namespace WebApplication1.Dtos;

public record SearchDto
{
    public string SearchTerm { get; set; } = string.Empty;
    public string DescriptionTerm { get; set; } = string.Empty;
}