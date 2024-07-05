using System.ComponentModel.DataAnnotations;
using WebApplication1.Constants;
using WebApplication1.Enums;

namespace WebApplication1.Models;

public class TaskModel : BaseModel
{
    [Required][MaxLength(ModelLimits.NameInputLimit)] public string Name { get; set; } = string.Empty;
    [Required][MaxLength(ModelLimits.DescriptionInputLimit)] public string Description { get; set; } = string.Empty;
    [Required] public Status Status { get; set; }
    public Guid ProjectId { get; set; }
}