using Application.CacheKeys;
using Application.Dtos.TaskDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TaskService(
    IProjectRepository projectRepository,
    ITaskRepository taskRepository,
    IValidator<UpdateTaskRequest> taskValidator,
    IValidator<CreateTaskRequest> createTaskValidator,
    IMapper mapper,
    ILogger<TaskService> logger,
    IMemoryCache memoryCache) : ITaskService
{
    public async Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams)
    {
        var cacheKey = TaskCacheKeyCreator.GetTaskCacheKey();
        logger.LogInformation("Started retrieving tasks from Service Layer");
        if (memoryCache.TryGetValue(cacheKey, out IEnumerable<TaskResponse>? cachedTaskResponses) &&
            cachedTaskResponses != null)
        {
            logger.LogInformation("Successfully retrieved tasks from cache from Service Layer");
            return cachedTaskResponses;
        }
        var tasks = await taskRepository.GetAllAsync(taskSearchParams);
        var mappedTasks = mapper.Map<IEnumerable<TaskResponse>>(tasks);
        var taskResponses = memoryCache.Set(cacheKey, mappedTasks, TimeSpan.FromSeconds(30));
        logger.LogInformation("Successfully retrieved projects from Service Layer");
        return taskResponses;
    }

    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        var cacheKey = TaskCacheKeyCreator.GetTaskCacheKey();
        logger.LogInformation($"Started retrieving task with Id : {id} from Service Layer");
        if (memoryCache.TryGetValue(cacheKey, out TaskResponse? cachedTaskResponse) && cachedTaskResponse != null)
        {
            logger.LogInformation("Successfully retrieved tasks from cache from Service Layer");
            return cachedTaskResponse;
        }
        var task = await taskRepository.GetByIdAsync(id);
        var taskResponse = mapper.Map<TaskResponse>(task);
        logger.LogInformation($"Successfully retrieved task with Id : {task.Id} from Service Layer");
        return taskResponse;
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest createTaskRequest)
    {
        logger.LogInformation("Started creating task from Service Layer");
        await createTaskValidator.ValidateAndThrowAsync(createTaskRequest);
        if (!await projectRepository.IsProjectExistsAsync(createTaskRequest.ProjectId))
        {
            throw new NotFoundException($"Project with {createTaskRequest.ProjectId} not found");
        }

        var task = mapper.Map<TaskModel>(createTaskRequest);
        var createdTask = await taskRepository.CreateAsync(task);
        var taskResponse = mapper.Map<TaskResponse>(createdTask);
        logger.LogInformation($"Successfully created task with Id : {createdTask.Id} from Service Layer");
        return taskResponse;
    }

    public async Task<TaskResponse> UpdateAsync(Guid id, UpdateTaskRequest updateTaskRequest)
    {
        logger.LogInformation($"Started updating task with Id : {id} from Service Layer");
        await taskValidator.ValidateAndThrowAsync(updateTaskRequest);
        var task = await taskRepository.GetByIdAsync(id);
        mapper.Map(updateTaskRequest, task);
        var updatedTask = await taskRepository.UpdateAsync(task);
        var taskResponse = mapper.Map<TaskResponse>(updatedTask);
        var cacheKey = TaskCacheKeyCreator.GetTaskCacheKey();
        memoryCache.Set(cacheKey, taskResponse, TimeSpan.FromSeconds(30));
        logger.LogInformation($"Successfully updated task with Id : {updatedTask.Id} from Service Layer");
        return taskResponse;
    }

    public async Task DeleteAsync(Guid id)
    {
        var cacheKey = TaskCacheKeyCreator.GetTaskCacheKey();
        logger.LogInformation($"Started deleting task with Id : {id} from Service Layer");
        await taskRepository.DeleteAsync(id);
        memoryCache.Remove(cacheKey);
        logger.LogInformation($"Successfully deleted task with Id : {id} from Service Layer");
    }
}