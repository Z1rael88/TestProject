using WebApplication1.Dto;
using WebApplication1.Dtos.TaskDtos;
using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class TaskMappers
{
    public static async Task<TaskResponse> TaskToResponseAsync(this Task<TaskModel> task)
    {
        TaskModel taskModel = await task.ConfigureAwait(false);
        if (taskModel == null)
        {
            throw new ArgumentNullException(nameof(taskModel), "Task model cannot be null.");
        }

        return new TaskResponse
        {
            Id = taskModel.Id,
            Title = taskModel.Title,
            Description = taskModel.Description,
            Status = taskModel.Status,
            ProjectId = taskModel.ProjectId
        };
    }
    public static TaskResponse TaskToResponse(this TaskModel task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            ProjectId = task.ProjectId
        };
    }
    public static async  Task<TaskRequest> TaskToRequest(this Task<TaskModel> task)
    {
        var taskModel = await task;
        return new TaskRequest
        {
            Title = taskModel.Title,
            Description = taskModel.Description,
            Status = taskModel.Status
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