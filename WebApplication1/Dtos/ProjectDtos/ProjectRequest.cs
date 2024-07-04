using System.ComponentModel.DataAnnotations;
using WebApplication1.Constants;

namespace WebApplication1.Dtos.ProjectDtos;

public record ProjectRequest
{
    [Required]
    [MaxLength(ModelLimits.NameLimit)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(ModelLimits.DescriptionLimit)]
    public string Description { get; set; } = string.Empty;

    [Required] public DateTime StartDate { get; set; }
}