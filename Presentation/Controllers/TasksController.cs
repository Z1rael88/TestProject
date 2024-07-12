using Application.Dtos.TaskDtos;
using Application.Interfaces;
using Domain.SearchParams;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(
    ITaskService taskService,
    ILogger<TasksController> logger) : ControllerBase
{
    private readonly ITaskService _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));

    [HttpGet]
    public async Task<IEnumerable<TaskResponse>> GetAllAsync([FromQuery] TaskSearchParams taskSearchParams)
    {
        logger.LogInformation($"Started retrieving tasks from {nameof(GetAllAsync)} request");
        var responses = await _taskService.GetAllAsync(taskSearchParams);
        logger.LogInformation(
            $"Successfully retrieved tasks from {nameof(GetAllAsync)} request");
        return responses;
    }

    [HttpGet("{id}")]
    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        logger.LogInformation($"Started retrieving task with Id : {id} from {nameof(GetAllAsync)} request");
        var response = await _taskService.GetByIdAsync(id);
        logger.LogInformation(
            $"Successfully retrieved task with Id : {id} from {nameof(GetByIdAsync)} request");
        return response;
    }

    [HttpPost]
    public async Task<TaskResponse> CreateTaskAsync([FromBody] CreateTaskRequest taskRequest)
    {
        logger.LogInformation($"Started creating task from {nameof(GetAllAsync)} request");
        var response = await _taskService.CreateAsync(taskRequest);
        logger.LogInformation(
            $"Successfully created task with Id : {response.Id} from {nameof(CreateTaskAsync)} request");
        return response;
    }

    [HttpPut("{id}")]
    public async Task<TaskResponse> UpdateTaskAsync(Guid id, BaseTaskRequest updateTaskRequest)
    {
        logger.LogInformation($"Started updating task with Id : {id} from {nameof(GetAllAsync)} request");
        var response = await _taskService.UpdateAsync(id, updateTaskRequest);
        logger.LogInformation(
            $"Successfully updated task with Id : {id} from {nameof(UpdateTaskAsync)} request");
        return response;
    }

    [HttpDelete("{id}")]
    public async Task DeleteTaskAsync(Guid id)
    {
        logger.LogInformation($"Started deleting task with Id : {id} from {nameof(GetAllAsync)} request");
        await _taskService.DeleteAsync(id);
        logger.LogInformation(
            $"Successfully deleted task with Id : {id} from {nameof(DeleteTaskAsync)} request");
    }
}