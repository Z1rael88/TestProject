using WebApplication1.Dto;
using WebApplication1.Dtos;
using WebApplication1.Dtos.TaskDtos;
using WebApplication1.Exceptions;
using WebApplication1.Interfaces.ProjectRepositories;
using WebApplication1.Interfaces.TaskRepositories;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class TaskService(IProjectService projectService, ITaskRepository taskRepository, IProjectRepository mockProjectRepository) : ITaskService
{
    public async Task<ICollection<TaskResponse?>> GetAllAsync(SearchDto searchDto)
    {
        var entities = await taskRepository.GetAllAsync(searchDto);
        var responses = entities.Select(p => p.TaskToResponse());
        if (!string.IsNullOrEmpty(searchDto.SearchTerm))
        {
            responses = responses
                .Where(p => p.Title.Contains(searchDto.SearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (!string.IsNullOrEmpty(searchDto.DescriptionTerm))
        {
            responses = responses
                .Where(p => p.Title.Contains(searchDto.DescriptionTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (responses.Count() == 0)
        {
            throw new NotFoundException("No projects found matching the search criteria.");
        }
        return responses.ToList();
    }


    public async Task<TaskResponse?>GetByIdAsync(Guid id)
    {
        var entity = taskRepository.GetByIdAsync(id);
        return await entity?.TaskToResponseAsync();
    }

    public async Task<TaskResponse> CreateAsync(TaskRequest taskRequest, Guid projectId)
    {
        var project = await projectService.GetByIdAsync(projectId);
        if (project.Id == null) return null;
        var taskEntity = new TaskModel
        {
            Title = taskRequest.Title,
            Description = taskRequest.Description,
            Status = taskRequest.Status,
            ProjectId = projectId
        };
        var createdTask = taskRepository.CreateAsync(taskEntity);
        return  await createdTask.TaskToResponseAsync();
    }

    public async Task DeleteAsync(Guid id)
    { 
        await taskRepository.DeleteAsync(id);
    }

    public async Task<TaskResponse> UpdateAsync(Guid id, TaskRequest taskRequest)
    {
        var task = taskRepository.UpdateAsync(id,taskRequest.TaskRequestToTaskModel());
        return await task.TaskToResponseAsync();
    }
}