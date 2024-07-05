using Application.Dtos.TaskDtos;
using Domain.Models;
using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class TaskMappers
{
    public static TaskResponse TaskToResponse(this TaskModel task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Status = task.Status,
            ProjectId = task.ProjectId
        };
    }
    public static TaskModel TaskRequestToTaskModel(this TaskRequest request)
    {
        return new TaskModel
        {
            Name = request.Name,
            Description = request.Description,
            Status = request.Status,
        };
    }
}