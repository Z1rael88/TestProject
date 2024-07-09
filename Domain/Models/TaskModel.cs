using Domain.Enums;

namespace Domain.Models;

public class TaskModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Status Status { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectModel Project { get; set; }
}