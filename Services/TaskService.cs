using WebApplication1.Dto;
using WebApplication1.Dtos.TaskDtos;
using WebApplication1.Interfaces;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class TaskService(IProjectService projectService, IMockTaskRepository mockTaskRepository) : ITaskService
{
    public List<TaskResponse?> GetAll()
    {
        var entities = mockTaskRepository.GetAll().Select(t => t.TaskToResponse());
        return entities.ToList();
    }

    public TaskResponse? GetById(Guid id)
    {
        var entity = mockTaskRepository.GetById(id);
        return entity?.TaskToResponse();
    }

    public TaskResponse? Create(TaskRequest taskRequest, Guid projectId)
    {
        var project = projectService.GetById(projectId);
        if (project.Id == null) return null;
        var taskEntity = new TaskModel
        {
            Title = taskRequest.Title,
            Description = taskRequest.Description,
            Status = taskRequest.Status,
            ProjectId = projectId 
        };
        project.Tasks.Add(taskEntity.TaskToResponse());
        var createdTask = mockTaskRepository.Create(taskEntity);
        return createdTask.TaskToResponse();
    }

    public bool Delete(Guid id)
    {
        return mockTaskRepository.Delete(id);
    }

    public TaskResponse? Update(Guid id, TaskRequest taskRequest)
    {
        var task =  mockTaskRepository.Update(id,taskRequest.TaskRequestToTaskModel());
        return task?.TaskToResponse();
    }
}