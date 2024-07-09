using Application.Dtos.TaskDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;

namespace Application.Services;

public class TaskService(
    IProjectService projectService,
    ITaskRepository taskRepository,
    IValidator<TaskRequest> taskValidator,
    IValidator<CreateTaskRequest> createTaskValidator,
    IMapper mapper) : ITaskService
{
    public async Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams)
    {
        var tasks = await taskRepository.GetAllAsync(taskSearchParams);
        var responses = mapper.Map<IEnumerable<TaskResponse>>(tasks);
        return responses;
    }

    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        var response = mapper.Map<TaskResponse>(task);
        return response;
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest taskRequest)
    {
        await createTaskValidator.ValidateAndThrowAsync(taskRequest);
        var project = await projectService.GetByIdAsync(taskRequest.ProjectId);
        var task = new TaskModel
        {
            Name = taskRequest.Name,
            Description = taskRequest.Description,
            Status = taskRequest.Status,
            ProjectId = project.Id,
        };
        var createdTask = await taskRepository.CreateAsync(task);
        var response = mapper.Map<TaskResponse>(createdTask);
        return response;
    }

    public async Task DeleteAsync(Guid id)
    {
        await taskRepository.DeleteAsync(id);
    }

    public async Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest)
    {
        await taskValidator.ValidateAndThrowAsync(taskRequest);
        var task = await taskRepository.GetByIdAsync(id);
        task.Name = taskRequest.Name;
        task.Description = taskRequest.Description;
        task.Status = taskRequest.Status;
        var updatedTask = await taskRepository.UpdateAsync(task);
        var response = mapper.Map<TaskResponse>(updatedTask);
        return response;
    }
}