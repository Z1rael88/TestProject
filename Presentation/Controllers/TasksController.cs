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
    public async Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams)
    {
        var responses = await _taskService.GetAllAsync(taskSearchParams);
        logger.LogInformation(
            $"Successfully retrieved tasks from {nameof(GetAllAsync)} request");
        return responses;
    }

    [HttpGet("{id}")]
    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        var response = await _taskService.GetByIdAsync(id);
        logger.LogInformation(
            $"Successfully retrieved task with id {id} from {nameof(GetByIdAsync)} request");
        return response;
    }

    [HttpPost]
    public async Task<TaskResponse> CreateTaskAsync([FromBody] CreateTaskRequest taskRequest)
    {
        var response = await _taskService.CreateAsync(taskRequest);
        logger.LogInformation(
            $"Successfully created task from {nameof(CreateTaskAsync)} request");
        return response;
    }

    [HttpPut("{id}")]
    public async Task<TaskResponse> UpdateTaskAsync(Guid id, UpdateTaskRequest updateTaskRequest)
    {
        var response = await _taskService.UpdateAsync(id, updateTaskRequest);
        logger.LogInformation(
            $"Successfully updated task with id {id} from {nameof(UpdateTaskAsync)} request");
        return response;
    }

    [HttpDelete("{id}")]
    public async Task DeleteTaskAsync(Guid id)
    {
        await _taskService.DeleteAsync(id);
        logger.LogInformation(
            $"Successfully deleted task with id {id} from {nameof(DeleteTaskAsync)} request");
    }
}