using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;

namespace Application.Services;

public class ProjectService(
    IProjectRepository projectRepository,
    IValidator<ProjectRequest> projectValidator,
    IMapper mapper)
    : IProjectService
{
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        var projects = await projectRepository.GetAllAsync(projectSearchParams);
        var responses = mapper.Map<IEnumerable<ProjectResponse>>(projects);
        return responses;
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid id)
    {
        var project = await projectRepository.GetByIdAsync(id);
        var response = mapper.Map<ProjectResponse>(project);
        return response;
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
        var response = mapper.Map<ProjectResponse>(createdProject);
        return response;
    }

    public async Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest)
    {
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = await projectRepository.GetByIdAsync(id);
        mapper.Map(projectRequest, project);
        var updatedProject = await projectRepository.UpdateAsync(project);
        var response = mapper.Map<ProjectResponse>(updatedProject);
        return response;
    }

    public async Task DeleteAsync(Guid id)
    {
        await projectRepository.DeleteAsync(id);
    }
}