using System.ComponentModel.DataAnnotations;
using WebApplication1.Constants;
using WebApplication1.Enums;

namespace WebApplication1.Dtos.TaskDtos;

public record TaskResponse
{
    [Required] public Guid Id { get; set; }
    [Required] [MaxLength(ModelLimits.NameLimit)] public string Title { get; set; } = string.Empty;
    [Required] [MaxLength(ModelLimits.DescriptionLimit)] public string Description { get; set; } = string.Empty;
    [Required] public Status Status { get; set; }

    public Guid ProjectId { get; set; }
}