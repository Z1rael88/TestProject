using Application.Dtos.TaskDtos;
using Application.Interfaces;
using Domain.SearchParams;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(ITaskService taskService, IValidator<TaskRequest> validator, IValidator<CreateTaskRequest> createValidator) : ControllerBase
{
    private readonly ITaskService _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));

    [HttpGet]
    public async Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams)
    {
        return await _taskService.GetAllAsync(taskSearchParams);
    }

    [HttpGet("{id}")]
    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        return await _taskService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<TaskResponse> CreateTaskAsync([FromBody] CreateTaskRequest taskRequest)
    {
        await createValidator.ValidateAndThrowAsync(taskRequest);
        return await _taskService.CreateAsync(taskRequest);
    }

    [HttpPut("{id}")]
    public async Task<TaskResponse> UpdateTaskAsync(Guid id, TaskRequest taskRequest)
    {
        await validator.ValidateAndThrowAsync(taskRequest);
        return await _taskService.UpdateAsync(id, taskRequest);
    }

    [HttpDelete("{id}")]
    public async Task DeleteTaskAsync(Guid id)
    {
        await _taskService.DeleteAsync(id);
    }
}