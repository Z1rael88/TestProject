using WebApplication1.Dto;
using WebApplication1.Dtos.TaskDtos;
using WebApplication1.Interfaces;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class TaskService(IProjectService projectService, ITaskRepository taskRepository, IProjectRepository projectRepository) : ITaskService
{
    public async Task<ICollection<TaskResponse>> GetAllAsync()
    {
        var entities = await taskRepository.GetAllAsync();
        var responses = entities.Select(p => p.TaskToResponse());
        return responses.ToList();
    }

    public Task<TaskResponse> GetByIdAsync(Guid id)
    {
        var entity = taskRepository.GetByIdAsync(id);
        return entity.TaskToResponseAsync();
    }

    public Task<TaskResponse> CreateAsync(TaskRequest taskRequest, Guid projectId)
    {
        var project = projectService.GetByIdAsync(projectId);
        if (project == null) throw new Exception();
        var taskEntity = new TaskModel
        {
            Title = taskRequest.Title,
            Description = taskRequest.Description,
            Status = taskRequest.Status,
            ProjectId = projectId
        };
        var createdTask = taskRepository.CreateAsync(taskEntity);
        return createdTask.TaskToResponseAsync();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        return taskRepository.DeleteAsync(id);
    }

    public Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest)
    {
        var task =  taskRepository.UpdateAsync(id,taskRequest.TaskRequestToTaskModel());
        return task.TaskToResponseAsync();
    }
}