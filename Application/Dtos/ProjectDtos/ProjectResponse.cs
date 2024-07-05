using System.ComponentModel.DataAnnotations;
using Application.Dtos.TaskDtos;
using Domain.Constants;

namespace Application.Dtos.ProjectDtos;

public record ProjectResponse
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(ModelLimits.NameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(ModelLimits.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;

    [Required] public DateOnly StartDate { get; set; }

    public IEnumerable<TaskResponse> Tasks { get; set; }
}