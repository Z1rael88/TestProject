using System.ComponentModel.DataAnnotations;
using Domain.Constants;
using Domain.Enums;

namespace Domain.Models;

public class TaskModel : BaseModel
{
    [Required]
    [MaxLength(ModelLimits.NameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(ModelLimits.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;

    [Required] public Status Status { get; set; }
    [Required] public Guid ProjectId { get; set; }
    public ProjectModel Project { get; set; }
}