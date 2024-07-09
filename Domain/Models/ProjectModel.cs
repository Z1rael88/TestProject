namespace Domain.Models;

public class ProjectModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateOnly StartDate { get; init; }
    public ICollection<TaskModel> Tasks { get; init; } = new List<TaskModel>();
}