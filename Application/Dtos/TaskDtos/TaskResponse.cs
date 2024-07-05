using System.ComponentModel.DataAnnotations;
using Domain.Constants;
using Domain.Enums;

namespace Application.Dtos.TaskDtos;

public record TaskResponse
{
    [Required] public Guid Id { get; set; }

    [Required]
    [MaxLength(ModelLimits.NameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(ModelLimits.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;

    [Required] public Status Status { get; set; }

    public Guid ProjectId { get; set; }
}