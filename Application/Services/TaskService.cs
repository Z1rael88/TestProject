using Application.Dtos.TaskDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;

namespace Application.Services;

public class TaskService(
    IProjectRepository projectRepository,
    ITaskRepository taskRepository,
    IValidator<UpdateTaskRequest> taskValidator,
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

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest createTaskRequest)
    {
        await createTaskValidator.ValidateAndThrowAsync(createTaskRequest);
        if (!await projectRepository.IsProjectExistsAsync(createTaskRequest.ProjectId))
        {
            throw new NotFoundException($"Project with {createTaskRequest.ProjectId} not found");
        }
        var task = mapper.Map<TaskModel>(createTaskRequest);
        var createdTask = await taskRepository.CreateAsync(task);
        var response = mapper.Map<TaskResponse>(createdTask);
        return response;
    }

    public async Task DeleteAsync(Guid id)
    {
        await taskRepository.DeleteAsync(id);
    }

    public async Task<TaskResponse> UpdateAsync(Guid id, UpdateTaskRequest updateTaskRequest)
    {
        await taskValidator.ValidateAndThrowAsync(updateTaskRequest);
        var task = await taskRepository.GetByIdAsync(id);
        mapper.Map(updateTaskRequest, task);
        var updatedTask = await taskRepository.UpdateAsync(task);
        var response = mapper.Map<TaskResponse>(updatedTask);
        return response;
    }
}