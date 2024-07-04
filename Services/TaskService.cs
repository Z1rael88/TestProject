using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.TaskDtos;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class TaskService(IProjectService projectService, IMockTaskRepository mockTaskRepository, IMockProjectRepository mockProjectRepository) : ITaskService
{
    public List<TaskResponse?> GetAll(SearchDto searchDto)
    {
        var entities = mockTaskRepository.GetAll().Select(p => p.TaskToResponse());
        if (entities == null) return null;
        if (!string.IsNullOrEmpty(searchDto.SearchTerm))
        {
            entities = entities
                .Where(p => p.Title.Contains(searchDto.SearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (!string.IsNullOrEmpty(searchDto.DescriptionTerm))
        {
            entities = entities
                .Where(p => p.Title.Contains(searchDto.DescriptionTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (entities.Count() == 0)
        {
            throw new NotFoundException("No projects found matching the search criteria.");
        }
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