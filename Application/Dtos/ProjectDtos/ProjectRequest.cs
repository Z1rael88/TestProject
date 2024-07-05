using System.ComponentModel.DataAnnotations;
using Domain.Constants;

namespace Application.Dtos.ProjectDtos;

public record ProjectRequest
{
    [Required]
    [MaxLength(ModelLimits.NameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(ModelLimits.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;

    [Required] public DateOnly StartDate { get; set; }
}