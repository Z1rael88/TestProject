using Application.Dtos.TaskDtos;
using Application.Interfaces;
using Application.Specification;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TaskService(
    IRepository<TaskModel> taskRepository,
    IRepository<ProjectModel> projectRepository,
    IValidator<BaseTaskRequest> taskValidator,
    IValidator<CreateTaskRequest> createTaskValidator,
    IMapper mapper,
    ILogger<TaskService> logger,
    ICacheService cacheService) : ITaskService
{
    public async Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams)
    {
        logger.LogInformation("Started retrieving tasks from Service Layer");
        var cachedTasks = await cacheService.TryGetCacheDataAsync<IEnumerable<TaskResponse>>(taskSearchParams);
        if (cachedTasks != null)
        {
            logger.LogInformation($"Successfully retrieved tasks from cache from Service Layer");
            return cachedTasks;
        }

        var tasks = await taskRepository.GetAllAsync(new TaskSpecification(taskSearchParams));
        var mappedTasks = mapper.Map<IEnumerable<TaskResponse>>(tasks);
        await cacheService.SetCacheDataAsync(taskSearchParams, mappedTasks);
        logger.LogInformation("Successfully retrieved projects from Service Layer");
        return mappedTasks;
    }

    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        logger.LogInformation($"Started retrieving task with Id : {id} from Service Layer");
        var cachedTask = await cacheService.TryGetCacheDataAsync<TaskResponse>(id);
        if (cachedTask != null)
        {
            logger.LogInformation($"Successfully retrieved task with Id : {id} from cache from Service Layer");
            return cachedTask;
        }

        var task = await taskRepository.GetByIdAsync(id);
        var mappedTask = mapper.Map<TaskResponse>(task);
        await cacheService.SetCacheDataAsync(id, mappedTask);
        logger.LogInformation($"Successfully retrieved task with Id : {task.Id} from Service Layer");
        return mappedTask;
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest createTaskRequest)
    {
        logger.LogInformation("Started creating task from Service Layer");
        await createTaskValidator.ValidateAndThrowAsync(createTaskRequest);
        if (!await projectRepository.IsEntityExistsAsync(createTaskRequest.ProjectId))
        {
            throw new NotFoundException($"Project with {createTaskRequest.ProjectId} not found");
        }

        var task = mapper.Map<TaskModel>(createTaskRequest);
        var createdTask = await taskRepository.CreateAsync(task);
        var taskResponse = mapper.Map<TaskResponse>(createdTask);
        logger.LogInformation($"Successfully created task with Id : {createdTask.Id} from Service Layer");
        return taskResponse;
    }

    public async Task<TaskResponse> UpdateAsync(Guid id, BaseTaskRequest updateTaskRequest)
    {
        logger.LogInformation($"Started updating task with Id : {id} from Service Layer");
        await taskValidator.ValidateAndThrowAsync(updateTaskRequest);
        var task = await taskRepository.GetByIdAsync(id);
        mapper.Map(updateTaskRequest, task);
        var updatedTask = await taskRepository.UpdateAsync(task);
        var taskResponse = mapper.Map<TaskResponse>(updatedTask);
        await cacheService.TryGetCacheDataAsync<TaskResponse>(id);
        logger.LogInformation($"Successfully updated task with Id : {updatedTask.Id} from Service Layer");
        return taskResponse;
    }

    public async Task DeleteAsync(Guid id)
    {
        logger.LogInformation($"Started deleting task with Id : {id} from Service Layer");
        await taskRepository.DeleteAsync(id);
        await cacheService.RemoveCacheDataAsync<TaskResponse>(id);
        logger.LogInformation($"Successfully deleted task with Id : {id} from Service Layer");
    }
}