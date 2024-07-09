using Domain.Enums;

namespace Application.Dtos.TaskDtos;

public class TaskResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Status Status { get; set; }
    public Guid ProjectId { get; set; }
}