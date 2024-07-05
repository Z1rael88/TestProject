using Application.Dtos.TaskDtos;
using Application.Interfaces;
using Application.Mappers;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;

namespace Application.Services;

public class TaskService(IProjectService projectService, ITaskRepository taskRepository) : ITaskService
{
    public async Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams)
    {
        var tasks = await taskRepository.GetAllAsync(taskSearchParams);
        var responses = tasks.Select(p => p.ToResponse());

        return responses;
    }

    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        return task.ToResponse();
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest taskRequest)
    {
        var project = await projectService.GetByIdAsync(taskRequest.ProjectId);
        var task = new TaskModel
        {
            Name = taskRequest.Name,
            Description = taskRequest.Description,
            Status = taskRequest.Status,
            ProjectId = project.Id
        };
        var createdTask = await taskRepository.CreateAsync(task);
        return createdTask.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        await taskRepository.DeleteAsync(id);
    }

    public async Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest)
    {
        var task = await taskRepository.GetByIdAsync(id);
        task.Name = taskRequest.Name;
        task.Description = taskRequest.Description;
        task.Status = taskRequest.Status;
        var updatedTask = await taskRepository.UpdateAsync(task);
        return updatedTask.ToResponse();
    }
}