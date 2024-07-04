using System.ComponentModel.DataAnnotations;
using WebApplication1.Enums;

namespace WebApplication1.Models;

public class TaskModel : BaseModel
{
    [Required] [MaxLength(12)] public string Title { get; set; } = string.Empty;
    [Required] [MaxLength(25)] public string Description { get; set; } = string.Empty;
    [Required] public Status Status { get; set; }
    public Guid ProjectId { get; set; }
}