using Domain.Enums;

namespace Application.Dtos.TaskDtos;

public class BaseTaskRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Status Status { get; set; }
}