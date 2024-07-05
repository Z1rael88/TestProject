using Application.Dtos.TaskDtos;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;

namespace Application.Services;

public class TaskService(IProjectService projectService, ITaskRepository taskRepository) : ITaskService
{
    public async Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams)
    {
        var tasks = await taskRepository.GetAllAsync(taskSearchParams);
        var responses = tasks.Select(p => p.TaskToResponse());

        return responses.ToList();
    }


    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        if (task == null) throw new NotFoundException("Task with that Id not found");
        return task.TaskToResponse();
    }

    public async Task<TaskResponse> CreateAsync(TaskRequest taskRequest, Guid projectId)
    {
        var project = await projectService.GetByIdAsync(projectId);
        if (project == null) throw new NotFoundException("Task with that Id not found");
        var task = new TaskModel
        {
            Name = taskRequest.Name,
            Description = taskRequest.Description,
            Status = taskRequest.Status,
            ProjectId = projectId
        };
        var createdTask = await taskRepository.CreateAsync(task);
        return createdTask.TaskToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        await taskRepository.DeleteAsync(id);
    }

    public async Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest)
    {
        var task = await taskRepository.GetByIdAsync(id);
        task.Name = taskRequest.Name;
        task.Description = taskRequest.Description;
        task.Status = taskRequest.Status;
        var response = await taskRepository.UpdateAsync(id, taskRequest.TaskRequestToTaskModel());
        return response.TaskToResponse();
    }
}