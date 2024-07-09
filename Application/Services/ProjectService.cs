using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using Application.Mappers;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;

namespace Application.Services;

public class ProjectService(IProjectRepository projectRepository, IValidator<ProjectRequest> projectValidator)
    : IProjectService
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
        await projectValidator.ValidateAndThrowAsync(projectRequest);
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
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = await projectRepository.GetByIdAsync(id);
        project.Name = projectRequest.Name;
        project.Description = projectRequest.Description;
        var updatedProject = await projectRepository.UpdateAsync(project);
        return updatedProject.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        await projectRepository.DeleteAsync(id);
    }
}