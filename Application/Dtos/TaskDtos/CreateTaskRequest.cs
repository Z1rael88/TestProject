namespace Application.Dtos.TaskDtos;

public record CreateTaskRequest : TaskRequest
{
    public Guid ProjectId { get; set; }
}