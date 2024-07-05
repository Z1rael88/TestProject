using System.ComponentModel.DataAnnotations;
using WebApplication1.Constants;

namespace WebApplication1.Dtos.ProjectDtos;

public record ProjectRequest
{
    [Required]
    [MaxLength(ModelLimits.NameInputLimit)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(ModelLimits.DescriptionInputLimit)]
    public string Description { get; set; } = string.Empty;

    [Required] public DateOnly StartDate { get; set; }
}