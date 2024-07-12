namespace Application.Dtos.TaskDtos;

public class CreateTaskRequest : BaseTaskRequest
{
    public Guid ProjectId { get; set; }
}