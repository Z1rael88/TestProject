using WebApplication1.Dto;
using WebApplication1.Dtos.TaskDtos;
using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class TaskMappers
{
    public static TaskResponse? TaskToResponse(this TaskModel task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status
        };
    }
    public static TaskRequest TaskToRequest(this TaskModel task)
    {
        return new TaskRequest
        {
            Title = task.Title,
            Description = task.Description,
            Status = task.Status
        };
    }

    public static TaskModel TaskRequestToTaskModel(this TaskRequest request)
    {
        return new TaskModel
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
        };
    }
}