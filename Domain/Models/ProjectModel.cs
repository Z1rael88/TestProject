using System.ComponentModel.DataAnnotations;
using Domain.Constants;
using WebApplication1.Models;

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

    public ICollection<TaskModel> Tasks { get; init; } = new List<TaskModel>();
}