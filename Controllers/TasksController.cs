using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Dtos.TaskDtos;
using WebApplication1.Interfaces.TaskRepositories;

namespace WebApplication1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TasksController(ITaskService taskService) : ControllerBase
{
    private readonly ITaskService _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));

    [HttpGet]
    public async Task<ICollection<TaskResponse>> GetAllAsync()
    {
        return await _taskService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<TaskResponse?> GetByIdAsync(Guid id)
    {
        return await _taskService.GetByIdAsync(id);
    }

    [HttpPost]
    public async  Task<TaskResponse?> CreateTaskAsync([FromBody]TaskRequest taskRequest,Guid projectId)
    {
        return await _taskService.CreateAsync(taskRequest, projectId);
    }

    [HttpPut("{id}")]
    public async Task<TaskResponse?> UpdateTaskAsync(Guid id, TaskRequest taskDto)
    {
        return await _taskService.UpdateAsync(id, taskDto);
    }

    [HttpDelete("{id}")]
    public async  Task<bool> DeleteTaskAsync(Guid id)
    {
        return await _taskService.DeleteAsync(id);
    }
}