using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using Application.Mappers;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;

namespace Application.Services;

public class ProjectService(IProjectRepository projectRepository, ITaskRepository taskRepository) : IProjectService
{
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        var projects = await projectRepository.GetAllAsync(projectSearchParams);
        var responses = projects.Select(p => p.ToResponse());
        return responses;
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid id)
    {
        var project = await projectRepository.GetByIdAsync(id);
        return project.ToResponse();
    }

    public async Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest)
    {
        var project = new ProjectModel
        {
            Description = projectRequest.Description,
            Name = projectRequest.Name,
            StartDate = projectRequest.StartDate,
        };
        var createdProject = await projectRepository.CreateAsync(project);
        return createdProject.ToResponse();
    }

    public async Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest)
    {
        var project = await projectRepository.GetByIdAsync(id);
        project.Name = projectRequest.Name;
        project.Description = projectRequest.Description;
        var updatedProject = await projectRepository.UpdateAsync(project);
        return updatedProject.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var tasks = await taskRepository.GetAllAsync(new TaskSearchParams() { ProjectId = id });
        foreach (var task in tasks)
        {
            await taskRepository.DeleteAsync(task.Id);
        }
        await projectRepository.DeleteAsync(id);
    }
}