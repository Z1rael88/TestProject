using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.TaskDtos;
using WebApplication1.Interfaces.TaskRepositories;

namespace WebApplication1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TasksController(ITaskService taskService) : ControllerBase
{
    private readonly ITaskService _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));

    [HttpGet]
    public List<TaskResponse> GetAll([FromQuery] SearchDto searchDto)
    {
        return _taskService.GetAll(searchDto);
    }

    [HttpGet("{id}")]
    public TaskResponse? GetById(Guid id)
    {
        return _taskService.GetById(id);
    }

    [HttpPost]
    public TaskResponse? CreateTask([FromBody]TaskRequest taskRequest,Guid projectId)
    {
        return _taskService.Create(taskRequest, projectId);
    }

    [HttpPut("{id}")]
    public TaskResponse? UpdateTask(Guid id, TaskRequest taskDto)
    {
        return _taskService.Update(id, taskDto);
    }

    [HttpDelete("{id}")]
    public bool DeleteTask(Guid id)
    {
        return _taskService.Delete(id);
    }
}