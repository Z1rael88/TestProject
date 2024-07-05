using Application.Dtos.TaskDtos;
using Domain.Models;

namespace Application.Mappers;

public static class TaskMappers
{
    public static TaskResponse ToResponse(this TaskModel task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description,
            Status = task.Status,
        };
    }
}