using System.ComponentModel.DataAnnotations;
using WebApplication1.Constants;

namespace WebApplication1.Models;

public class ProjectModel : BaseModel
{
    [Required][MaxLength(ModelLimits.NameInputLimit)] public string Name { get; set; } = string.Empty;
    [Required][MaxLength(ModelLimits.DescriptionInputLimit)] public string Description { get; set; } = string.Empty;
    [Required] public DateOnly StartDate { get; init; }

    public ICollection<TaskModel> Tasks { get; init; } = new List<TaskModel>();
}