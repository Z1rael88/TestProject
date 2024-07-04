using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class ProjectModel : BaseModel
{
    [Required] [MaxLength(12)] public string Name { get; set; } = string.Empty;
    [Required] [MaxLength(50)] public string Description { get; set; } = string.Empty;
    [Required] public DateTime StartDate { get; init; }

    public ICollection<TaskModel> Tasks { get; init; } = new List<TaskModel>();
}