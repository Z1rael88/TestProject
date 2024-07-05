using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using Application.Mappers;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;

namespace Application.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        var projects = await projectRepository.GetAllAsync(projectSearchParams);
        var responses = projects.Select(p => p.ProjectToResponse());
        return responses;
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid id)
    {
        var project = await projectRepository.GetByIdAsync(id);
        if (project == null) throw new NotFoundException("Project with that Id not found");
        return project.ProjectToResponse();
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
        return createdProject.ProjectToResponse();
    }

    public async Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest)
    {
        var project = await projectRepository.GetByIdAsync(id);
        if (project == null) throw new NotFoundException("Project with that Id not found");
        project.Name = projectRequest.Name;
        project.Description = projectRequest.Description;
        var response = await projectRepository.UpdateAsync(id, projectRequest.ProjectRequestToTaskModel());
        return response.ProjectToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        await projectRepository.DeleteAsync(id);
    }
}