using Application.Dtos.ProjectDtos;
using Application.Interfaces;
using Application.Specification;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Domain.SearchParams;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProjectService(
    IRepository<ProjectModel> projectRepository,
    IValidator<ProjectRequest> projectValidator,
    IMapper mapper,
    ILogger<ProjectService> logger,
    ICacheService cacheService)
    : IProjectService
{
    public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectSearchParams projectSearchParams)
    {
        logger.LogInformation("Started retrieving projects from Service Layer");
        var cachedProjects = await cacheService.TryGetCacheDataAsync<IEnumerable<ProjectResponse>>(projectSearchParams);
        if (cachedProjects != null)
        {
            logger.LogInformation("Successfully retrieved projects from cache from Service Layer");
            return cachedProjects;
        }

        var projects = await projectRepository.GetAllAsync(new ProjectWithTasksSpecifications(projectSearchParams));
        var mappedProjects = mapper.Map<IEnumerable<ProjectResponse>>(projects);
        await cacheService.SetCacheDataAsync(projectSearchParams, mappedProjects);
        logger.LogInformation("Successfully retrieved projects from Service Layer");
        return mappedProjects;
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid id)
    {
        logger.LogInformation($"Started retrieving project with Id : {id} from Service Layer");
        var cachedProject = await cacheService.TryGetCacheDataAsync<ProjectResponse>(id);
        if (cachedProject != null)
        {
            logger.LogInformation($"Successfully retrieved project with Id : {id} from cache from Service Layer");
            return cachedProject;
        }

        var project = await projectRepository.GetByIdAsync(id);
        var mappedProject = mapper.Map<ProjectResponse>(project);
        await cacheService.SetCacheDataAsync(id, mappedProject);
        logger.LogInformation($"Successfully retrieved project with Id : {project.Id} from Service Layer");
        return mappedProject;
    }

    public async Task<ProjectResponse> CreateAsync(ProjectRequest projectRequest)
    {
        logger.LogInformation("Started creating project from Service Layer");
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = mapper.Map<ProjectModel>(projectRequest);
        var createdProject = await projectRepository.CreateAsync(project);
        var projectResponse = mapper.Map<ProjectResponse>(createdProject);
        logger.LogInformation(
            $"Successfully created project with Id : {createdProject.Id} from Service Layer");
        return projectResponse;
    }

    public async Task<ProjectResponse> UpdateAsync(Guid id, ProjectRequest projectRequest)
    {
        logger.LogInformation($"Started updating project with Id : {id} from Service Layer");
        await projectValidator.ValidateAndThrowAsync(projectRequest);
        var project = await projectRepository.GetByIdAsync(id);
        mapper.Map(projectRequest, project);
        var updatedProject = await projectRepository.UpdateAsync(project);
        var projectResponse = mapper.Map<ProjectResponse>(updatedProject);
        await cacheService.SetCacheDataAsync(id, projectResponse);
        logger.LogInformation(
            $"Successfully updated project with Id : {updatedProject.Id} from Service Layer");
        return projectResponse;
    }

    public async Task DeleteAsync(Guid id)
    {
        logger.LogInformation($"Started deleting project with Id : {id} from Service Layer");
        await projectRepository.DeleteAsync(id);
        await cacheService.RemoveCacheDataAsync<ProjectResponse>(id);
        logger.LogInformation($"Successfully deleted project with Id : {id} from Service Layer");
    }
}