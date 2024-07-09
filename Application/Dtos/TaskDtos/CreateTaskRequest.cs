namespace Application.Dtos.TaskDtos;

public class CreateTaskRequest : TaskRequest
{
    public Guid ProjectId { get; set; }
}