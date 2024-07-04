using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.ProjectDtos;

public record ProjectRequest
{
    public string Name { get; set; } = string.Empty;
    [Required] [MaxLength(50)] public string Description { get; set; } = string.Empty;
    [Required] public DateTime StartDate { get; set; }
}