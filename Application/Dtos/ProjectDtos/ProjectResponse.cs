using Application.Dtos.TaskDtos;

namespace Application.Dtos.ProjectDtos;

public class ProjectResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public IEnumerable<TaskResponse> Tasks { get; set; }
}