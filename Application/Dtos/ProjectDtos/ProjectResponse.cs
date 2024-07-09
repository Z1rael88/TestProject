using Application.Dtos.TaskDtos;

namespace Application.Dtos.ProjectDtos;

public class ProjectResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateOnly StartDate { get; set; }

    public IEnumerable<TaskResponse> Tasks { get; set; }
}