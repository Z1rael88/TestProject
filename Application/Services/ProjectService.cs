using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProjectService(
    IProjectRepository projectRepository,
    IValidator<ProjectRequest> projectValidator,
    IMapper mapper,
    ILogger<ProjectService> logger)
    : IProjectService
{
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        logger.LogInformation("Started retrieving projects");
        var projects = await projectRepository.GetAllAsync(projectSearchParams);
        var responses = mapper.Map<IEnumerable<ProjectResponse>>(projects);
        logger.LogInformation("Successfully retrieved projects from Service Layer");
        return responses;
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid id)
    {
        logger.LogInformation($"Started retrieving project with Id : {id} ");
        var project = await projectRepository.GetByIdAsync(id);
        var response = mapper.Map<ProjectResponse>(project);
        logger.LogInformation($"Successfully retrieved project with Id : {project.Id} from Service Layer");
        return response;
    }

    public async Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest)
    {
        logger.LogInformation("Started creating project ");
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = mapper.Map<ProjectModel>(projectRequest);
        var createdProject = await projectRepository.CreateAsync(project);
        var response = mapper.Map<ProjectResponse>(createdProject);
        logger.LogInformation(
            $"Successfully created project with Id : {createdProject.Id} from Service Layer");
        return response;
    }

    public async Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest)
    {
        logger.LogInformation($"Started updating project with Id : {id}");
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = await projectRepository.GetByIdAsync(id);
        mapper.Map(projectRequest, project);
        var updatedProject = await projectRepository.UpdateAsync(project);
        var response = mapper.Map<ProjectResponse>(updatedProject);
        logger.LogInformation(
            $"Successfully updated project with Id : {updatedProject.Id} from Service Layer");
        return response;
    }

    public async Task DeleteAsync(Guid id)
    {
        logger.LogInformation($"Started deleting project with Id : {id}");
        await projectRepository.DeleteAsync(id);
        logger.LogInformation($"Successfully deleted project with Id : {id} from Service Layer");
    }
}