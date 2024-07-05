using WebApplication1.Dtos.SearchParams;
using WebApplication1.Dtos.TaskDtos;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class TaskService(IProjectService projectService, ITaskRepository taskRepository) : ITaskService
{
    public async Task<IEnumerable<TaskResponse>> GetAllAsync(TaskSearchParams taskSearchParams)
    {
        var entities = await taskRepository.GetAllAsync(taskSearchParams);
        var responses = entities.Select(p => p.TaskToResponse());

        return responses.ToList();
    }


    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        var entity = await taskRepository.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException();
        return entity.TaskToResponse();
    }

    public async Task<TaskResponse> CreateAsync(TaskRequest taskRequest, Guid projectId)
    {
        var project = await projectService.GetByIdAsync(projectId);
        if (project == null) throw new NotFoundException();
        var taskEntity = new TaskModel
        {
            Name = taskRequest.Title,
            Description = taskRequest.Description,
            Status = taskRequest.Status,
            ProjectId = projectId
        };
        var createdTask = await taskRepository.CreateAsync(taskEntity);
        return createdTask.TaskToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        await taskRepository.DeleteAsync(id);
    }

    public async Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest)
    {
        var entity = await taskRepository.UpdateAsync(id, taskRequest.TaskRequestToTaskModel());
        entity.Name = entity.Name;
        entity.Description = entity.Description;
        entity.Status = entity.Status;
        entity.ProjectId = entity.ProjectId;
        return entity.TaskToResponse();
    }
}