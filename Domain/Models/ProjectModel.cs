using System.ComponentModel.DataAnnotations;
using Domain.Constants;

namespace Domain.Models;

public class ProjectModel : BaseModel
{
    [Required]
    [MaxLength(ModelLimits.NameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(ModelLimits.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;

    [Required] public DateOnly StartDate { get; init; }
    [Required] public ICollection<TaskModel> Tasks { get; init; } = new List<TaskModel>();
}