using Application.Dtos.TaskDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TaskService(
    IProjectRepository projectRepository,
    ITaskRepository taskRepository,
    IValidator<UpdateTaskRequest> taskValidator,
    IValidator<CreateTaskRequest> createTaskValidator,
    IMapper mapper,
    ILogger<TaskService> logger) : ITaskService
{
    public async Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams)
    {
        logger.LogInformation("Started retrieving tasks");
        var tasks = await taskRepository.GetAllAsync(taskSearchParams);
        var responses = mapper.Map<IEnumerable<TaskResponse>>(tasks);
        logger.LogInformation("Successfully retrieved projects from Service Layer");
        return responses;
    }

    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        logger.LogInformation($"Started retrieving task with Id : {id} ");
        var task = await taskRepository.GetByIdAsync(id);
        var response = mapper.Map<TaskResponse>(task);
        logger.LogInformation($"Successfully retrieved task with Id : {task.Id} from Service Layer");
        return response;
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest createTaskRequest)
    {
        logger.LogInformation("Started creating task");
        await createTaskValidator.ValidateAndThrowAsync(createTaskRequest);
        if (!await projectRepository.IsProjectExistsAsync(createTaskRequest.ProjectId))
        {
            throw new NotFoundException($"Project with {createTaskRequest.ProjectId} not found");
        }

        var task = mapper.Map<TaskModel>(createTaskRequest);
        var createdTask = await taskRepository.CreateAsync(task);
        var response = mapper.Map<TaskResponse>(createdTask);
        logger.LogInformation($"Successfully created task with Id : {createdTask.Id} from Service Layer");
        return response;
    }

    public async Task<TaskResponse> UpdateAsync(Guid id, UpdateTaskRequest updateTaskRequest)
    {
        logger.LogInformation($"Started updating task with Id : {id}");
        await taskValidator.ValidateAndThrowAsync(updateTaskRequest);
        var task = await taskRepository.GetByIdAsync(id);
        mapper.Map(updateTaskRequest, task);
        var updatedTask = await taskRepository.UpdateAsync(task);
        var response = mapper.Map<TaskResponse>(updatedTask);
        logger.LogInformation($"Successfully updated task with Id : {updatedTask.Id} from Service Layer");
        return response;
    }

    public async Task DeleteAsync(Guid id)
    {
        logger.LogInformation($"Started deleting task with Id : {id}");
        await taskRepository.DeleteAsync(id);
        logger.LogInformation($"Successfully deleted task with Id : {id} from Service Layer");
    }
}