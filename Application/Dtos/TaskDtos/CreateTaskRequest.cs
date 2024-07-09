namespace Application.Dtos.TaskDtos;

public class CreateTaskRequest : UpdateTaskRequest
{
    public Guid ProjectId { get; set; }
}